using System.Collections.Generic;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.service;
using Unity.UIWidgets.widgets;
using Debug = UnityEngine.Debug;

namespace PomoTimerApp
{
    public class NewTaskPage : StatefulWidget
    {

        public override State createState()
        {
            return new NewTaskState();
        }
    }

    class NewTaskState:State<NewTaskPage>
        {
            static int  TEXTFILED_MAX_LENGTH = 24;
            private bool mSaveButtonVisiable = false;
            private TextEditingController mTitleController;
            private TextEditingController mDescriptionController;
            public override void initState()
            {
                base.initState();
                mTitleController = new TextEditingController();
                mDescriptionController = new TextEditingController();
            }
            //保存按钮方法
            void SaveClose()
            {
                var titleText = mTitleController.text.Trim();
                var descoriptionText = mDescriptionController.text.Trim();
                if (titleText.isEmpty())
                {
                    return;
                }
                else
                {
                    //给state中属性赋值
                    Navigator.pop(context, new Task
                    {
                        Title = titleText,
                        Description = descoriptionText
                            
                    });
                    Debug.Log(titleText+descoriptionText);
                }
            }

            public override Widget build(BuildContext context)
            {
            
            var saveButton = new IconButton(
                icon:new Icon(Icons.save,size:32,color:Theme.of(context).primaryColor),
                onPressed: SaveClose
            );
            return new Scaffold(
                body: new Material(
                    child: new ListView(
                        children: new List<Widget>()
                        {
                            new Row(
                                children: new List<Widget>()
                                {
                                    new Padding(
                                        padding: EdgeInsets.symmetric(vertical:10),
                                        child:new Row(
                                            mainAxisAlignment:MainAxisAlignment.spaceBetween,
                                            children:new List<Widget>()
                                            {
                                                new IconButton(icon: new Icon(Icons.arrow_back,size:32,color:Theme.of(context).primaryColor),
                                                    onPressed: () =>
                                                    {
                                                        Navigator.pop(context);
                                                    }),
                                                mSaveButtonVisiable? saveButton as Widget: new Container() as Widget
                                            }
                                        )
                                    ),
                                }

                            ),
                            //Titile
                            new TextField(
                                maxLength:TEXTFILED_MAX_LENGTH,
                                style:new TextStyle(fontSize:24),
                                controller:mTitleController,
                                decoration:new InputDecoration(
                                    filled:true,
                                    fillColor:Colors.white,
                                    hintText:"Task Title",
                                    contentPadding:EdgeInsets.only(10,2,10,2)
                                    ),
                                onChanged:(text =>
                                {
                                    var needShowSaveButton = text.Trim().Length > 0;
                                    if (needShowSaveButton!= mSaveButtonVisiable )
                                    {
                                        setState(() => { mSaveButtonVisiable = needShowSaveButton; });
                                    }

                                })
                                ),
                            //Description
                            new TextField(
                                maxLines:5,
                                controller:mDescriptionController,
                                keyboardType:TextInputType.multiline,
                                style:new TextStyle(fontSize:18),
                                decoration:new InputDecoration(
                                    border:InputBorder.none,
                                    filled:true,
                                    fillColor:Colors.white,
                                    hintText:"Description",
                                    contentPadding:EdgeInsets.only(10,2,10,2)
                                )
                            )
                        }
                    )


                )

            );
        }
        }
        
        

 
    }
