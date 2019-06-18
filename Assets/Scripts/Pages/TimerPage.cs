using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Debug = UnityEngine.Debug;

namespace PomoTimerApp
{
    public class TimerPage : StatefulWidget
        {
            //使得可获取
            public Task TaskData { get; }
            
            //抽象函数 可以引用
            public TimerPage(Task taskData)
            {
                TaskData = taskData;
            }

            public override State createState()
            {
                return new TimerPageState();
            }
        }

        class TimerPageState : State<TimerPage>
        {
            public readonly static TimeSpan DELAY = TimeSpan.FromMilliseconds(100);
            private string mTimerText = "25:00";
            private Stopwatch mStopwatch = null;
            private Unity.UIWidgets.async.Timer mTimer = null;
            private string mButtonText = "START";
            public override void initState()
            {
                mStopwatch = new Stopwatch();
                
                mTimer = Window.instance.periodic(duration: DELAY, () =>
                {
                    var minutes = (int) mStopwatch.Elapsed.Minutes;
                    //判断返回时间
                    if (minutes==1)
                    {
                        Debug.Log("Times is up!");
                        //到时暂停并返回
                        //mStopwatch.Stop();
                        setState(() => mTimerText = "00:00");
                        //是否能返回上一个页面
                        if (Navigator.canPop(context))
                        {
                            widget.TaskData.pomoCount++;
                            //返回上一个页面并带参数
                            Navigator.pop(context, widget.TaskData);
                        }
                        
                        return;
                    }
                    Debug.LogFormat("Total Memory:{0:###,###,###,##0}bytes",GC.GetTotalMemory(true));
                    if (mStopwatch.IsRunning)
                    {
                        
                        //每次初始化时实现时间更新
                        //setState单向更新数据
                        setState(() =>
                        {
                            mButtonText = "PAUSE";
                            //mTimerText = String.Format("{0}:{1}",24-stopwatch.Elapsed.Minutes,60-stopwatch.Elapsed.Seconds);
                            string minText = (25 - mStopwatch.Elapsed.Minutes - 1).ToString().PadLeft(2, '0');
                            string secondText = (60 - mStopwatch.Elapsed.Seconds - 1).ToString().PadLeft(2, '0');
                            mTimerText = minText + ":" + secondText;
                            //mTimerText = $"{25-mStopwatch.Elapsed.Minutes-1}:{60-mStopwatch.Elapsed.Seconds-1}";

                        });
                    }
                    else
                    {
                        setState(() =>
                        {
                            mButtonText = "START";
                        });
                        
                    }
                    
                });
            }

            public override void dispose()
            {
                base.dispose();
                mStopwatch.Stop();
                mTimer.cancel();
            }

            public override Widget build(BuildContext context)
            {

                    return new Stack(
                        children:new List<Widget>
                        {
                            new Align(
                                alignment:Alignment.topCenter,
                                child:new Container(
                                    margin:EdgeInsets.only(top:100),
                                    //获取数据
                                    child:new Text(widget.TaskData.Title,style:new TextStyle(
                                        color:Colors.white,
                                        fontSize:50.0f,
                                        fontWeight:FontWeight.bold))
                                    )),
                            
                            new Align(
                                alignment:Alignment.center,
                                child:new Text(mTimerText,style:new TextStyle(
                                    color:Colors.white,
                                    fontSize:66.0f,
                                    fontWeight:FontWeight.bold))),
                            new Align(
                                alignment:Alignment.bottomCenter,
                                child:new Container(
                                    margin:EdgeInsets.only(bottom:100),
                                    //child:new Text("START")
                                    //添加点击事件
                                    child:new GestureDetector(
                                        //引用RoundButton
                                        child:new RoundButton(mButtonText),
                                        onTap: () =>
                                        {
                                            if (!mStopwatch.IsRunning)
                                            {
                                                mStopwatch.Start();
                                            }
                                            else
                                            {
                                                mStopwatch.Stop();
                                            }
                                           
                                        }
                                        )
                                    
                                    )),
                        }
                        
                        );
            }
        }
}