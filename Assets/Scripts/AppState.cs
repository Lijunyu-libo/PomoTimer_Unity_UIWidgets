using System.Collections.Generic;
using QFramework.UIWidgets.ReduxPersist;

namespace PomoTimerApp
{
    public enum PageMode
    {
        List,
        Finished
        
    }
    //定义Redux中的State
    public class AppState:AbstractPersistState<AppState>
    {
        public List<Task> Tasks = new List<Task>();
        public PageMode pageMode = PageMode.List;
    }
}