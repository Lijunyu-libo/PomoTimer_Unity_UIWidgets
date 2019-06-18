namespace PomoTimerApp
{
    public class AddTaskAction
    {
        //构造函数，并注入Task类型参数
        public AddTaskAction(Task task)
        {

            Task = task;
        }
        
        public Task Task { get; }
    }
    
    public class RemoveTaskAction
    {
        //构造函数，并注入Task类型参数
        public RemoveTaskAction(Task task)
        {

            Task = task;
        }
        
        public Task Task { get; }
    }
    
    public class CompleteTaskAction
    {
        //构造函数，并注入Task类型参数
        public CompleteTaskAction(Task task)
        {

            Task = task;
        }
        
        public Task Task { get; }
    }
}