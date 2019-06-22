namespace PomoTimerApp
{
    public class AppReducer
    {
        public static AppState Reduce(AppState state, object action)
        {
            switch (action)
            {
                //判断类型是否相同，并赋值
                case  AddTaskAction addTaskAction:
                    //list类型添加
                    state.Tasks.Add(addTaskAction.Task);
                    return state;
                case  RemoveTaskAction removeTaskAction:
                    //list类型删除
                    state.Tasks.Remove(removeTaskAction.Task);
                    return state;
                case  UpdateTaskAction updateTaskAction:
                    //赋值
                    updateTaskAction.Task.Done = true;
                    return state;
                //页面选择赋值
                case  ChangeToListAction _:
                    state.pageMode = PageMode.List;
                    return state;
                case  ChangeToFinisedAction _:
                    state.pageMode = PageMode.Finished;
                    return state;
                case  ChangeToSettingAction _:
                    state.pageMode = PageMode.Setting;
                    return state;
                //
                case  ChangeThemeColorAction changeThemeColorAction:
                    state.themeColor = changeThemeColorAction.color;
                    return state;
                case  ChangeTimerDurationAction changeTimerDurationAction:
                    state.timerDuration = changeTimerDurationAction.min;
                    return state;
            }
                    
            return state;
        }
        
    }
}