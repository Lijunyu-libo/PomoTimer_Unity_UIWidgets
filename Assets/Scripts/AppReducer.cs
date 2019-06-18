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
            }
                    
            return state;
        }
        
    }
}