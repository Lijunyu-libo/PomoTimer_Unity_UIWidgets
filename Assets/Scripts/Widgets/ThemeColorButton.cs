using System;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace PomoTimerApp
{
    public class ThemeColorButton:StatelessWidget
    {
        public string title { get; }
        public Color color { get; }
        public Action onClick { get; }

        public ThemeColorButton(string title, Color color, Action onClick)
        {
            this.title = title;
            this.color = color;
            this.onClick = onClick;
        }
        public override Widget build(BuildContext context)
        {
            return new GestureDetector(
                onTap: () => { onClick(); },
                child: new Container(
                    width: MediaQuery.of(context).size.width,
                    height: 200,
                    color: color,
                    child: new Center(child: new Text(title, style: new TextStyle(color: Colors.white))
                    )));
        }
    }
}