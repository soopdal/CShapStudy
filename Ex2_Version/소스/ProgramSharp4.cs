using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;                                                           // ExpandoObject

namespace Ex2_Version
{
    public partial class Program
    {
        //---------------------------------------------------------------------
        //                      C# 4.0
        //---------------------------------------------------------------------
        static public void runCSharp4()                                         
        {
            Console.WriteLine("\n\n\t\tC# 4.0");

            runDynamic();
            runNamedParameter();
            runParams();
            runIndexedProperty();
        }

        //---------------------------------------------------------------------
        //                      다이나믹
        //---------------------------------------------------------------------
        static void runDynamic()
        {
            Console.WriteLine("\tDynamic");

            dynamic v = 1;
            Console.WriteLine(v.GetType());

            v = "abc";
            Console.WriteLine(v.GetType());

            object o = 10;                                                          // o = o + 20;
            o = (int)o + 20;

            dynamic d = 10;
            d = d + 20;

            Console.WriteLine("");
            ExpandoClass expando = new ExpandoClass();
            expando.run();
        }

        class Class1
        {
            public void Run()
            {
                dynamic t = new { Name = "Kim", Age = 25 };

                var c = new Class2();
                c.Run(t);
            }
        }

        class Class2
        {
            public void Run(dynamic o)
            {
                Console.WriteLine(o.Name);
                Console.WriteLine(o.Age);
            }
        }

        public class ExpandoClass
        {
            public void run()
            {
                dynamic person = new ExpandoObject();
                person.Name = "Kim";
                person.Age = 10;                                                // dynamic person = new { Name = "Kim", Age = 25 };     // 밑에 runDynamic()에서 에러가 뜬다.

                person.Display = (Action)(() =>
               {
                   Console.WriteLine("{0} {1}", person.Name, person.Age);
               });

                person.ChangeAge = (Action<int>)((age) =>
               {
                   person.Age = age;
                   if (person.AgeChanged != null)
                       person.AgeChanged(this, EventArgs.Empty);
               });

                person.AgeChanged = null;
                person.AgeChanged += new EventHandler(OnAgeChanged);

                runDynamic(person);

                var dict = (IDictionary<string, object>)person;

                foreach (var d in dict)
                {
                    Console.WriteLine("{0} : {1}", d.Key, d.Value);
                }
            }

            private void OnAgeChanged(object sender, EventArgs e)
            {
                Console.WriteLine("Age changed");
            }

            public void runDynamic(dynamic d)
            {
                d.Display();
                d.ChangeAge(20);
                d.Display();
            }
        }

        //---------------------------------------------------------------------
        //                      네임드 파라미터
        //---------------------------------------------------------------------
        static void runNamedParameter()
        {
            Console.WriteLine("\tNamed parameter");
            runNamedParameter(name: "John", age: 10, score: 90);
        }

        static void runNamedParameter(int age, string name, int score)
        {
        }

        //---------------------------------------------------------------------
        //                      C# 파람스
        //---------------------------------------------------------------------
        static void runParams()
        {
            Console.WriteLine("\tParams");
            int s = runParams(1, 2, 3, 4);
            s = runParams(6, 7, 8, 9, 10, 11);
        }

        static int runParams(params int[] values)
        {
            return 0;
        }

        //---------------------------------------------------------------------
        //                      인덱스드 프라퍼티
        //---------------------------------------------------------------------
        static void runIndexedProperty()
        {
            Console.WriteLine("Indexed property");
        }

        int[] values = new int[1];
        public int this[int index]                                              // 인덱서는 this만 사용하고, 하나만 있을 수 있다.
        {
            get { return values[index]; }
            set { values[index] = value; }
        }

        //public Address this[string name]
        //{
        //    get { return _theValues[name]; }
        //    set { _theValues[name] = value; }
        //}
    }
}