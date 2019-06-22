using System.Collections.Generic;
using System.Linq;
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
                    drawer: new MenuDrawer(),
                    //Redux部分获取数据
                    floatingActionButton:
                    new StoreConnector<AppState, AppState>(
                        converter: state => state,
                        builder: ((buildContext1, model, dispatcher) =>
                        {
                            if (model.pageMode == PageMode.List )
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
                                
                            }
                            else
                            {
                                return null;
                            }
                            
                        })

                    )

                    ,
                    appBar: new AppBar(
                        //获取Pagemode
                        title: new StoreConnector<AppState,PageMode>(
                            converter:state=>state.pageMode,
                            builder:((buildContext, model, dispatcher) =>
                                {
                                    //判断类型显示标题
                                    //return new Text( model == PageMode.List?"TASK PAGE":"FINISH PAGE");
                                    //静态扩展方式显示标题
                                    return new Text(model.Titiled());
                                }
                            ))
                    ),
                    //获取值需要使用connector，配置state和ViewModel
                    body: new StoreConnector<AppState, PageMode>(
                        converter: state => state.pageMode,
                        builder: (buildContext, model, dispatcher) =>
                        {
                            if (model == PageMode.List)
                            {
                                return new TaskListPage();
                            }

                            else if (model == PageMode.Finished)
                            {
                                return new FinishListPage();
                            }
                            else
                            {
                               return new SettingPage();
                            }
                            
                        }
                    ));

            }
        }
    }
}