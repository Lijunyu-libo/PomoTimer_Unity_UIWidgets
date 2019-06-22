using System.Collections.Generic;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.widgets;

namespace PomoTimerApp
{
    public class TimerSlider:StatefulWidget
    {
        
        public override State createState()
        {
            return new TimerSliderState();
        }

        class TimerSliderState:State<TimerSlider>
        {
            private int timerDuration = 25;
            public override Widget build(BuildContext context)
            {

                return new StoreConnector<AppState,int>(
                    converter:state=>state.timerDuration,
                    builder: (BuildContext, model, dispatcher) =>
                    {
                        //timerDuration = model;
                        return new Row(
                            children: new List<Widget>
                            {
                                new Text("TimerDuration"),
                                new Unity.UIWidgets.material.Slider(min: 1, max: 35,value: timerDuration,
                                    onChanged: value => this.setState(()=>timerDuration = (int)value),
                                    onChangeEnd: value =>
                                    {
                                        dispatcher.dispatch(new ChangeTimerDurationAction(timerDuration));
                                    }),
                                new Text($"{timerDuration.ToString()} Minutes")

                            }

                        );
                    }

                ); 
                    
                    
                    
            }
        }
    }
}