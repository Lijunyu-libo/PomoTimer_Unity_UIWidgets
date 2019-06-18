using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;

namespace PomoTimerApp
{
    public class MenuDrawer:StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            //获取参数需要使用storeconnect，并配置需要获取的数据项
            return new StoreConnector<AppState, PageMode>(
                converter: state => state.pageMode,
                builder: ((buildContext, model, dispatcher) =>
                    new Container(
                        width: 200,
                        child: new Drawer(
                            child: new Column(
                                children: new List<Widget>()
                                {
                                    new Container(
                                        height: 60,
                                        color: Colors.teal
                                    ),
                                    new GestureDetector(
                                        child:new Container(
                                            color:model == PageMode.List?Colors.grey[300] :Colors.white,
                                            child:new ListTile(
                                                leading: new Icon(Icons.list),
                                                title: new Text("TASK")
                                            )
                                        ),
                                        onTap: () =>
                                        {
                                            if (model==PageMode.List)
                                            {
                                                
                                            }
                                            else
                                            {
                                                dispatcher.dispatch(new ChangeToListAction());
                                            }
                                            
                                        }
                                    
                                    
                                    ),
                                    new GestureDetector(
                                        child:new Container(
                                            color:model == PageMode.Finished?Colors.grey[300]:Colors.white,
                                            child:new ListTile(
                                                leading: new Icon(Icons.done),
                                                title: new Text("FINISH")
                                            )
                                        ),
                                        onTap: () =>
                                        {
                                            if (model==PageMode.Finished)
                                            {
                                                
                                            }
                                            else
                                            {
                                                dispatcher.dispatch(new ChangeToFinisedAction());
                                            }
                                            
                                        }
                                        
                                    )
                                }

                            )
                            
                        )
                    )
                
                //
                
                ));

        }
    }
}