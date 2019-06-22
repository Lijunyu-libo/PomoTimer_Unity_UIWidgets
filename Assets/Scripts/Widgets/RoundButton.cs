using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;
namespace PomoTimerApp
{
    public class RoundButton:StatefulWidget
    {
        public RoundButton(string text)
        {
            buttonText = text;
        }
        //定义抽象函数
        
        public string buttonText { get;}

        public override State createState()
        {
            return new RoundButtonState();
        }

        class RoundButtonState:State<RoundButton>
        {
            public override Widget build(BuildContext context)
            {
                return new Container(
                    width:150,
                    height:150,
                    decoration:new BoxDecoration(
                        color:Theme.of(context).primaryColor,
                        borderRadius:BorderRadius.all(100),
                        boxShadow:new List<BoxShadow>()
                        {
                            new BoxShadow(
                                color:Colors.grey,
                                blurRadius:0f
                                )
                        }
                        ),
                    child:new Center(
                        child:new Text(
                            widget.buttonText,
                            style:new TextStyle(color:Colors.white,fontSize:30))
                    )
                    );
            }
        }
    }
}