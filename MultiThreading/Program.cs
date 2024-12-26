internal class Program
{
    private static object _lock = new object();
    private static void Main(string[] args)
    {
        var t1 = Task.Run(() =>
        {
            for (int i = 0; i < 30; i++)
            {
                AppendText("file1.txt", i + 1, "L1");
            }
        });
       

        var t2 = Task.Run(() =>
        {
            for (int i = 0; i < 10; i++)
            {
                AppendText("file1.txt", i + 1, "L2");
            }
        });

        var t3 = Task.Run(() =>
        {
            for (int i = 0; i < 20; i++)
            {
                AppendText("file1.txt", i + 1, "L3");
            }
        });
        
        var tasks = new Task[] { t1, t2, t3 };

        Console.WriteLine($"{tasks.Length} Task started");
        Console.WriteLine("Enter your name");
        var name = Console.ReadLine();
        Console.WriteLine($"Welcome {name}. Your tasks are running. Please wait.");
        
        Task.WaitAll(tasks);
        Console.WriteLine("All Tasks completed");
    }

    public static void AppendText(string fileName, int loopCycle, string uniqueName)
    {
        Thread.Sleep(500);
        //Console.WriteLine($"From {uniqueName} Loop Cycle {loopCycle}");
        lock (_lock)
        {
            File.AppendAllText(fileName, uniqueName + " - " + loopCycle + " - " + new DateTime() + " - " + Guid.NewGuid().ToString() + Environment.NewLine);
        }
    }
}