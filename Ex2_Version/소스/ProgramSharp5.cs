using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;                                                         // Thread
using System.Threading.Tasks;
using System.Dynamic;                                                           // ExpandoObject

namespace Ex2_Version
{
    public partial class Program
    {
        //---------------------------------------------------------------------
        //                      C# 5.0
        //---------------------------------------------------------------------
        static public void runCSharp5()                                         
        {
            Console.WriteLine("\n\n\t\tC# 5.0");

            runAsync();
            Console.WriteLine("\tCaller information");
        }

        //---------------------------------------------------------------------
        //                      어싱크
        //---------------------------------------------------------------------
        static void runAsync()
        {
            Console.WriteLine("\tAsync");
        }
    }

    public partial class Form1
    {
        private async void runThread()
        {
            var task = Task<int>.Run(() => LongCalcAsync(10));
            int sum = await task;

            this.label1.Text = "Sum = " + sum;
            this.button1.Enabled = true;
        }

        private int LongCalcAsync(int times)
        {
            int result = 0;
            for (int i = 0; i < times; i++)
            {
                result += i;
                Thread.Sleep(1000);
            }
            return result;
        }

        private async void runUIThread()
        {
            int sum = await LongCalcUIAsync(10);

            this.label2.Text = "Sum = " + sum;
            this.button1.Enabled = true;
        }

        private async Task<int> LongCalcUIAsync(int times)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            int result = 0;
            for (int i = 0; i < times; i++)
            {
                result += i;
                await Task.Delay(1000);
            }
            return result;
        }

        private async void runContinueWith()
        {
            var task = Task<int>.Run( () => LongCalcUIAsync(10) );

            task.ContinueWith( x =>                                             // await task와 동일한 효과
            {
                this.label3.Text = "Sum = " + task.Result;
                this.button1.Enabled = true;
            },
            TaskScheduler.FromCurrentSynchronizationContext() );
        }

    }
}