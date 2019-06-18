using System;

namespace PomoTimerApp
{
    //Task 数据结构
    public class Task
    {
        //类属性定义，C#3.5自动属性写法
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public int pomoCount { get;set; }
        public bool Done { get; set; } = false;
    }
}