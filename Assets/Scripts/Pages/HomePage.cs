using System.Collections.Generic;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace PomoTimerApp
{
    public class HomePage:StatefulWidget
    {
        public override State createState()
        {
            return new HomePageState();
        }
        
        class HomePageState:State<HomePage>
        {
            
            public override Widget build(BuildContext context)
            {
                return new Scaffold(
                    //Redux部分获取数据
                    floatingActionButton:
                    new StoreConnector<AppState,AppState>(
                        converter:state=>state,
                        builder:((buildContext1, model, dispatcher) =>
                        {
                            return new FloatingActionButton(
                                child: new Icon(Icons.add),
                                onPressed: () =>
                                {
                                    Navigator.push(
                                        context,
                                        new MaterialPageRoute(builder: buildContext => { return new NewTaskPage(); }
                                        )).Then(result =>
                                    {
                                        //发送添加事件
                                        var newTask = result as Task;
                                        if (newTask != null)
                                        {
                                            //发送Action事件
                                            dispatcher.dispatch(new AddTaskAction(newTask));
                                        }
                                    });
                                }
                            );
                        })
                        
                    )
                    
                   ,
                    appBar:new AppBar(
                        title:new Text("HOME PAGE")
                        ),
                    body:new Container(
                        
                        child:new Center(
                            //通过Store Connector连接Redux，进行state,viewModels参数设置
                            child:new StoreConnector<AppState,List<Task>>(
                                converter:state=>state.Tasks,
                                builder:((buildContext, model, dispatcher) =>
                                {
                                    if (model.Count > 0)
                                    {
                                        //return new Text($"HAVE {model.Count} TASKS", style: new TextStyle(fontSize: 20));
                                        return ListView.builder(
                                            itemCount:model.Count,
                                            itemBuilder:((context1, index) =>
                                            {
                                                var taskData = model[index];
//                                                return new ListTile(
//                                                    title:new Text(taskData.Title),
//                                                    trailing:new IconButton(icon:new Icon(Icons.delete,color:Theme.of(context).primaryColor,
//                                                        size:32),onPressed:()=>
//                                                        {
//                                                            //触发Action并传入数据
//                                                            dispatcher.dispatch(new RemoveTaskAction(taskData));
//                                                        })
//                                                    
//                                                    );

                                                //添加点击事件
                                                return new InkWell(
                                                    child: new TaskWidget(taskData, onRemove: () =>
                                                        {
                                                            //触发并传递值进去
                                                            dispatcher.dispatch(new RemoveTaskAction(taskData));
                                                        },
                                                        onComplete: () =>
                                                        {
                                                            //触发并传递值进去
                                                            dispatcher.dispatch(new CompleteTaskAction(taskData));

                                                        }
                                                    ),
                                                    onTap: () =>
                                                    {
                                                        //跳转并传递参数到TimerPage页面
                                                        Navigator.of(context)
                                                            .push(new MaterialPageRoute(builder: (buildContext1 =>
                                                                new TimerPage(taskData))));

                                                    }
                                                );

                                            })


                                        );

                                    }
                                    else
                                    {
                                        return new Text("NO TASK NOW", style: new TextStyle(fontSize: 20));
                                    }

                                })
                                
                                )
                            
                           
                            ) 
                        )
                    );
                    
                    
                
            }
        }
    }
}