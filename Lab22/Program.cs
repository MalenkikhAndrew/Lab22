using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Action<Task<int[]>> action1 = new Action<Task<int[]>>(MaxArray);
            Task task2 = task1.ContinueWith(action1);

            Action<Task<int[]>> action2 = new Action<Task<int[]>>(SumArray);
            Task task3 = task1.ContinueWith(action2);

            task1.Start();
            Console.ReadKey();
        }

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
                Console.WriteLine($"{array[i]}");
            }
            return array;
        }
        static void MaxArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = 0;
            foreach (int x in array)
            {
                if (x > max)
                    max = x;

            }
            Console.WriteLine($"\nМаксимальное число в масиве: {max} ");
        }
        static void SumArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int sum = 0;
            foreach (int a in array)
            {
                sum += a;
            }
            Console.WriteLine($"\nСумма чисел в массиве равна:: {sum} ");
        }
    }
}

