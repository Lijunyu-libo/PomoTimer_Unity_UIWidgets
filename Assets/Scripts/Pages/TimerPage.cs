using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using Unity.UIWidgets.animation;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Debug = UnityEngine.Debug;

namespace PomoTimerApp
{
    public class TimerPage : StatefulWidget
        {
            //使得可获取
            public Task TaskData { get; }
            
            public int timerDuration { get; }
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

        class TimerPageState : SingleTickerProviderStateMixin<TimerPage>
        {
            public readonly static TimeSpan DELAY = TimeSpan.FromMilliseconds(100);
            private string mTimerText = "25:00";
            private Stopwatch mStopwatch = null;
            private Unity.UIWidgets.async.Timer mTimer = null;
            private string mButtonText = "START";
            public override void initState()
            {
                mStopwatch = new Stopwatch();
                mController = new AnimationController(duration:TimeSpan.FromMinutes(1),vsync:this);
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
                mController.dispose();
                mStopwatch.Stop();
                mTimer.cancel();
            }

            private float mBegin = 50;
            private AnimationController mController;
            private Animation<float> mHeightSize;
            public override Widget build(BuildContext context)
            { 
                mHeightSize = new FloatTween(
                    mBegin,
                    MediaQuery.of(context).size.height-65).animate(
                    new CurvedAnimation(mController,Curves.easeInOut));
                var size = new Size(MediaQuery.of(context).size.width, mHeightSize.value * 0.9f);
                    return new Scaffold(
                        backgroundColor:Colors.white,
                        body:new Material(
                            child: new Stack(
                        children:new List<Widget>
                        {
                            new AnimatedBuilder(
                                animation:mController,
                                builder:((buildContext, child) =>
                                {
                                    return new WaveAnimation(size: size,
                                        color: Theme.of(context).primaryColor);
                                })
                                ),
                            
                            new Container(
                                alignment:Alignment.topCenter,
                                padding:EdgeInsets.only(left:4,top:10,right:4),
                                child:new Row(
                                    mainAxisAlignment:MainAxisAlignment.spaceBetween,
                                    children:new List<Widget>()
                                    {
                                        new IconButton(
                                            icon:new Icon(Icons.arrow_back,color:Theme.of(context).primaryColor,size:32),
                                            onPressed:()=>
                                        {
                                            if (mTimerText!="00:00")
                                            {
                                                widget.TaskData.pomoCount++;
                                                Navigator.pop(context, widget.TaskData);
                                            }
                                            else
                                            {
                                                Navigator.pop(context);
                                            }
                                            
                                        }),
                                        new IconButton(
                                            icon:new Icon(Icons.done_all,color:Theme.of(context).primaryColor,size:32),
                                            onPressed:()=>
                                            {
                                                if (mTimerText!="00:00")
                                                {
                                                    widget.TaskData.Done = true;
                                                    Navigator.pop(context, widget.TaskData);
                                                }
                                                else
                                                {
                                                    Navigator.pop(context);
                                                }
                                            
                                            })
                                    }
                                )
                                ),
                            new Align(
                                alignment:Alignment.topCenter,
                                child:new Container(
                                    margin:EdgeInsets.only(top:100),
                                    //获取数据
                                    child:new Text(widget.TaskData.Title,style:new TextStyle(
                                        color:Theme.of(context).primaryColor,
                                        fontSize:32
                                        //fontWeight:FontWeight.bold
                                        ))
                                    )),
                            
                            new Align(
                                alignment:Alignment.center,
                                child:new Text(mTimerText,style:new TextStyle(
                                    color:Theme.of(context).primaryColor,
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
                                                //时间控制
                                                mStopwatch.Start();
                                                //动画控制
                                                mController.forward();
                                            }
                                            else
                                            {
                                                mStopwatch.Stop();
                                                mController.stop(false);
                                            }
                                           
                                        }
                                        )
                                    
                                    )),
                        }
                        )
                            
                            )
                        );
            }
        }
}