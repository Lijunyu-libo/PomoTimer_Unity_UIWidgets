using System.Collections.Generic;
using QFramework.UIWidgets.ReduxPersist;

namespace PomoTimerApp
{
    //定义Redux中的State
    public class AppState:AbstractPersistState<AppState>
    {
        public List<Task> Tasks = new List<Task>();
    }
}