using System;
using System.Reflection;

class Runnable : Attribute
{
}
class MyTasks
{
    [Runnable]
    public void Task1()
    {
        Console.WriteLine("Task 1 executed");
    }

    [Runnable]
    public void Task2()
    {
        Console.WriteLine("Task 2 executed");
    }

    public void NotRun()
    {
        Console.WriteLine("This should NOT run");
    }
}

class Program
{
    static void Main()
    {
        // create object
        MyTasks obj = new MyTasks();

        // get all methods
        MethodInfo[] methods = typeof(MyTasks).GetMethods();

        foreach (MethodInfo m in methods)
        {
            // check if method has [Runnable]
            if (m.GetCustomAttribute(typeof(Runnable)) != null)
            {
                Console.WriteLine("Running: " + m.Name);

                // call method
                m.Invoke(obj, null);
            }
        }
    }
}