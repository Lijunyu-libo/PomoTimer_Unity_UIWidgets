using System.Collections.Generic;
using System.Linq;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;

namespace PomoTimerApp
{
    public class TaskListPage:StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new  Container(
                child: new Center(
                    //通过Store Connector连接Redux，进行state,viewModels参数设置
                    child: new StoreConnector<AppState, List<Task>>(
                        //过滤，未完成taskData
                        converter: state => state.Tasks.Where(task => !task.Done).ToList(),
                        builder: ((buildContext, model, dispatcher) =>
                        {
                            if (model.Count > 0)
                            {
                                //return new Text($"HAVE {model.Count} TASKS", style: new TextStyle(fontSize: 20));
                                return ListView.builder(
                                    itemCount: model.Count,
                                    itemBuilder: ((context1, index) =>
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
                                                    taskData.Done = true;
                                                    //触发并传递值进去
                                                    dispatcher.dispatch(new UpdateTaskAction(taskData));

                                                }
                                            ),
                                            onTap: () =>
                                            {
                                                //跳转并传递参数到TimerPage页面
                                                Navigator.of(context)
                                                    .push(new MaterialPageRoute(builder: (buildContext1 =>
                                                        new TimerPage(taskData)))).Then(onResolved: result =>
                                                    {
                                                        var task = result as Task;
                                                        if (task != null)
                                                        {
                                                            dispatcher.dispatch(new UpdateTaskAction(task));
                                                        }

                                                    });

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
            );
        }
    }
}