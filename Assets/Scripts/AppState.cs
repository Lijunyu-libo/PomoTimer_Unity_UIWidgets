using System.Collections.Generic;
using QFramework.UIWidgets.ReduxPersist;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;

namespace PomoTimerApp
{
    public enum PageMode
    {
        List,
        Finished,
        Setting
        
    }

    public static class PageModeToTitle
    {
        //静态扩展
        public static string Titiled(this PageMode mode)
        {
            switch (mode)
            {
                case PageMode.List:
                    return "TASK";
                case PageMode.Finished:
                    return "FINISHED";
                case PageMode.Setting:
                    return "SETTING";
            }

            return null;
        }
        
    }

    public enum ThemeColors
    {
        Red,
        Teal,
        Blue,
        Grey,
        White
        
    }

    public static class ThemeColorToMaterialColor
    {
        public static MaterialColor ToMaterialColor(this ThemeColors color)
        {
            switch (color)
            {
                case ThemeColors.Red:
                return Colors.red;
                case ThemeColors.Teal:
                    return Colors.teal;
                case ThemeColors.Blue:
                    return Colors.blue;
                case ThemeColors.Grey:
                    return Colors.grey;
                
            }
            return Colors.teal;

        }
        
        
    }
    //定义Redux中的State
    public class AppState:AbstractPersistState<AppState>
    {
        //全局变量
        public List<Task> Tasks = new List<Task>();
        public PageMode pageMode = PageMode.List;
        public ThemeColors themeColor = ThemeColors.Red;
        public int timerDuration;
    }
}