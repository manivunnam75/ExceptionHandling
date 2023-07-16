namespace MultiThreadExceptionHandling
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                // Start multiple tasks to perform different tasks concurrently
                Task task1 = Task.Run(() => DoWork("Task 1"));
                Task task2 = Task.Run(() => DoWork("Task 2"));
                Task task3 = Task.Run(() => DoWork("Task 3"));

                // Wait for all tasks to complete
                Task.WaitAll(task1, task2, task3);

                Console.WriteLine("All tasks completed successfully.");
            }
            catch (AggregateException ex)
            {
                // Log and handle the exceptions
                foreach (Exception innerEx in ex.InnerExceptions)
                {
                    Console.WriteLine($"An exception occurred: {innerEx.Message}");
                }

                // Perform appropriate error handling
                // For example, display an error message to the user or take fallback actions
                Console.WriteLine("Oops! Something went wrong.");
            }
        }

        public static void DoWork(string taskName)
        {
            try
            {
                // Simulate some work
                Thread.Sleep(2000);

                // Throw an exception for task 2
                if (taskName == "Task 2")
                {
                    throw new InvalidOperationException("Something went wrong in Task 2.");
                }

                Console.WriteLine($"{taskName} completed successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An exception occurred within {taskName}: {ex.Message}");

                // Rethrow the exception to be captured by the calling thread
                throw;
            }
        }
    }

}
