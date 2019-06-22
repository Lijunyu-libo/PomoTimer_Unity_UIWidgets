using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using UnityEngine;

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
                                        color: Theme.of(context).primaryColor
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
                                            //Debug.Log(model.ToString());
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
                                            //Debug.Log(model.ToString());
                                            if (model==PageMode.Finished)
                                            {
                                                
                                            }
                                            else
                                            {
                                                dispatcher.dispatch(new ChangeToFinisedAction());
                                                
                                            }
                                            
                                        }
                                        
                                    ),
                                    new GestureDetector(
                                        child:new Container(
                                            color:model == PageMode.Setting?Colors.grey[300]:Colors.white,
                                            child:new ListTile(
                                                leading: new Icon(Icons.settings),
                                                title: new Text("SETTING")
                                            )
                                        ),
                                        onTap: () =>
                                        {
                                            //Debug.Log(model.ToString());
                                            if (model==PageMode.Setting)
                                            {
                                                
                                            }
                                            else
                                            {
                                                dispatcher.dispatch(new ChangeToSettingAction());
                                                
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