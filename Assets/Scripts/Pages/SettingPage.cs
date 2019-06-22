using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;
using Slider = UnityEngine.UI.Slider;

namespace PomoTimerApp
{
    public class SettingPage:StatelessWidget
    {
        
        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState,object>(
                converter:state=>null,
                builder:((buildContext, model, dispatcher) => 
            new Container(
                margin:EdgeInsets.symmetric(horizontal:10,vertical:10),
                child:new ListView(
                    
                    children:new List<Widget>
                    {
                        //使用封装
                        new ThemeColorButton("Red",Colors.red, () =>
                        {
                            dispatcher.dispatch(new ChangeThemeColorAction(ThemeColors.Red));
                        }),
                        new Divider(),
                        new GestureDetector(
                            onTap: () =>
                            {
                                dispatcher.dispatch(new ChangeThemeColorAction(ThemeColors.Teal));
                            },
                            child:new Container(
                            width:MediaQuery.of(context).size.width,
                            height:200,
                            color:Colors.teal,
                            child:new Center(child:new Text("TealTheme",style:new TextStyle(color:Colors.white))))),
                        new Divider(),
                        new GestureDetector(
                            onTap: () =>
                            {
                                dispatcher.dispatch(new ChangeThemeColorAction(ThemeColors.Blue));
                            },
                            child: new Container(
                            width:MediaQuery.of(context).size.width,
                            height:200,
                            color:Colors.blue,
                            child:new Center(child:new Text("BlueTheme",style:new TextStyle(color:Colors.white))
                            ))),
                        new Divider(),
                        new TimerSlider()
                        
                        
                        
                    }
                ))));
        }
    }
}