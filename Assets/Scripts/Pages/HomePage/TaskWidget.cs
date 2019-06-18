using System;
using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.widgets;

namespace PomoTimerApp
{
    //封装Task卡片
    public class TaskWidget:StatelessWidget
    {
        private Task mTaskData;
        //委托
        private Action mOnRemove;
        private Action mOnComplete;

        public TaskWidget(Task taskData, Action onRemove,Action onComplete)
        {
            mTaskData = taskData;
            mOnRemove = onRemove;
            mOnComplete = onComplete;
        }
        public override Widget build(BuildContext context)
        {
            return new Container(
                color:Colors.white,
                child:new Card(
                    child:new ListTile(
                        leading: new IconButton(
                            icon:new Icon(Icons.check_circle_outline,color:mTaskData.Done?Colors.black54:Colors.teal),
                            onPressed: () =>
                            {
                                //触发完成
                                mOnComplete();
                            }),
                        title:new Container(
                            height:72,
                            child:new Row(
                                children:new List<Widget>()
                                {
                                    new Padding(
                                        padding:EdgeInsets.only(left:8),
                                        child:new Column(
                                            //水平居左对其
                                            crossAxisAlignment:CrossAxisAlignment.start,
                                            //垂直居中对其
                                            mainAxisAlignment:MainAxisAlignment.center,
                                    children:new List<Widget>()
                                            {
                                                new Text(mTaskData.Title,style:new TextStyle(color:Colors.black,fontSize:18)),
                                                new Text(mTaskData.Description,style:new TextStyle(color:Colors.black45,fontSize:16)),
                                                new Text($"{mTaskData.pomoCount} Pomodoro",style:new TextStyle(color:Colors.teal,fontSize:16))
                                                
                                            }
                                            )
                                        
                                        )
                                    
                                   
                                    
                                }
                                )),
                        trailing:new IconButton(icon:new Icon(Icons.delete,color:Theme.of(context).primaryColor,
                    size:32),onPressed:()=>
                        {
                    //触发Action并传入数据
                    mOnRemove();
                        })
                        )
                    )
            );
        }
    }
}