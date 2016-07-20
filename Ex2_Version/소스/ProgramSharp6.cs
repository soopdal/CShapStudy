using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;                                                         // Thread
using System.Threading.Tasks;
using System.Dynamic;                                                           // ExpandoObject
using static System.Console;
using System.ComponentModel;                                                    // Win32Exception

namespace Ex2_Version
{
    public partial class Program
    {
        //---------------------------------------------------------------------
        //                      C# 6.0
        //---------------------------------------------------------------------
        static public void runCSharp6()                                         
        {
            Console.WriteLine("\n\n\t\tC# 6.0");

            runExpressionLevel();
            runStatementLevel();
            runClassMemberLevel();
        }

        //---------------------------------------------------------------------
        //                      표현식 레벨
        //---------------------------------------------------------------------
        static void runExpressionLevel()
        {
            Console.WriteLine("\n\tExpression Level");

            runNullConditionalOperator();
            runNullCoalescingOperator();                                        // Coalesce 컬레스 합치다.
            runStringInterpolation();
            runDictionaryInitializer();
            runNameOfOperator();
        }

        //---------------------------------------------------------------------
        //                      문장레벨
        //---------------------------------------------------------------------
        static void runStatementLevel()
        {
            Console.WriteLine("\n\tStatement level");

            runUsingStaticMethod();
            runCatchAwait();
            runExceptionFilter();
        }

        //---------------------------------------------------------------------
        //                      클래스 멤버 레벨
        //---------------------------------------------------------------------
        static void runClassMemberLevel()
        {
            Console.WriteLine("\n\tClass member level");

            runAutoPropertyInitializer();
            runExpressionBodiedMember();
        }

        ////---------------------------------------------------------------------
        ////                      다이나믹
        ////---------------------------------------------------------------------
        //static void runDynamic()
        //{
        //    Console.WriteLine("\nDynamic");
        //}

        //---------------------------------------------------------------------
        //                      널 조건 연산자
        //---------------------------------------------------------------------
        class Customer
        {
            public int? Age = null;

            void add() { ++Age;  }
        }

        static void runNullConditionalOperator()
        {
            Console.WriteLine("\tNull conditional operator");

            List<int> rows      = new List<int>();
            int? count          = rows?.Count;
            Console.WriteLine("count {0}", count);

            Customer[] customers    = new Customer[1];
            Customer c              = customers?[0];
            int? age                = customers?[0]?.Age;
            Console.WriteLine("age {0}", age);
        }

        //---------------------------------------------------------------------
        //                      널 컬리싱 연산자
        //---------------------------------------------------------------------
        static void runNullCoalescingOperator()
        {
            Console.WriteLine("\tNull coalescing operator");

            List<int> rows  = new List<int>();
            int count       = rows?.Count ?? 0;
            Console.WriteLine("count {0}", count);
        }

        public class DalButton
        {
            public event EventHandler clicked;

            public void ClickOld()
            {
                var tempClicked = clicked;                                      // 쓰레드 안정성 때문에
                if (tempClicked != null)
                    tempClicked(this, null);
            }

            public void ClickCSharp6()
            {
                clicked?.Invoke(this, null);
            }

        }

        //---------------------------------------------------------------------
        //                      스트링 내삽
        //---------------------------------------------------------------------
        static void runStringInterpolation()
        {
            Rectangle r = new Rectangle();
            r.Height    = 10;
            r.Width     = 20;
            string s = $"{r.Height} x {r.Width} = { (r.Height * r.Width)}";
            Console.WriteLine(s);
        }

        class Rectangle
        {
            public int Width, Height;
        }

        //---------------------------------------------------------------------
        //                      사전 초기화
        //---------------------------------------------------------------------
        static void runDictionaryInitializer()
        {
            Console.WriteLine("\tDictionaryInitializer");

            var scores = new Dictionary<string, int>()
            {
                { "kim", 100 },
                { "lee",  90 }
            };
            int sc = scores["lee"];

            var newScores = new Dictionary<string, int>()
            {
                ["kim"] = 100,
                ["lee"] = 90
            };
            int score = newScores["lee"];

            var a = new[] { 1, 2, 3 };

            var l = new List<int>(a) { [2] = 9 };                               // List는 인덱서를 지원하므로 Dictionary Initializer 사용가능
        }

        //---------------------------------------------------------------------
        //                      nameof 연산자
        //---------------------------------------------------------------------
        static void runNameOfOperator()
        {
            Console.WriteLine("\tnameof operator");

            try
            {
                int id = 0;
                throw new ArgumentException("Invalid argument", nameof(id));
            }
            catch(ArgumentException e)
            {
                e.Equals(null);
            }

            Person objPerson = new Person();
            Console.WriteLine("{0} : {1}", nameof(objPerson.Height), objPerson.Height);
            Run();
        }

        class Person
        {
            public int Height = 0;
        }

        static void Run()
        {
            Log(nameof(Run) + " : Started");
        }

        static void Log(string message)
        {
            Console.WriteLine(message);
        }

        //---------------------------------------------------------------------
        //                      using static 메소드
        //---------------------------------------------------------------------
        static void runUsingStaticMethod()
        {
            WriteLine("\tUsing static method");
        }

        //---------------------------------------------------------------------
        //                      using static 메소드
        //---------------------------------------------------------------------
        static async void runCatchAwait()
        {
            try
            {
                Request req = new Request();
                var response = await req.GetResponseAsync();
                throw new Exception();
            }
            catch(Exception ex)
            {
                await Log(ex);
            }
            WriteLine("\tUsing static method");
        }
        
        static async Task<int> Log(Exception ex)
        {
            WriteLine(nameof(ex));
            await Task<int>.Delay(1);
            return 0;
        }

        class Request
        {
            public async Task<int> GetResponseAsync()
            {
                await Task<int>.Delay(1);
                return 0;
            }
        }

        //---------------------------------------------------------------------
        //                      예외 필터
        //---------------------------------------------------------------------
        static void runExceptionFilter()
        {
            Console.WriteLine("\tException filter");
            try
            {
                throw new Win32Exception(0x10);
            }
            catch(Win32Exception ex) when (ex.NativeErrorCode == 0x10)
            {
                Log(ex);
            }
        }

        static void Log(Win32Exception ex)
        {
            WriteLine(nameof(ex));
        }

        //---------------------------------------------------------------------
        //                      자동 속성 초기화
        //---------------------------------------------------------------------
        static void runAutoPropertyInitializer()
        {
            Console.WriteLine("\tAuto property initializer\n\tGetter only");

            NeoPerson person = new NeoPerson();

            WriteLine(person.Name);
            WriteLine(person.Nickname);
            WriteLine(person.Age);
        }

        class NeoPerson
        {
            public string   Name { get; set; } = "(No name)";
            public string   Nickname { get;  }
            public int      Age { get;  }

            public bool     Enabled { get; } = true;
            public int      Level { get;  }

            public NeoPerson()
            {
                this.Enabled = true;
            }
        }
        //---------------------------------------------------------------------
        //                      표현몸체 멤버
        //---------------------------------------------------------------------
        static void runExpressionBodiedMember()
        {
            Console.WriteLine("\tExpression-bodied member");
        }

        class Human
        {
            int     Height = 0, Width = 0;                                      // 기본 값 0을 사용한다는 경고가 안나오게 할당함
            int     X = 0, Y = 0;
            string  data = null, FirstName = null, LastName = null;

            DB      db  = new DB();

            public int AreaOld
            {
                get
                {
                    return Height * Width;
                }
            }

            public int AreaNew => Height * Width;

            public Point Move(int x, int y) => new Point(X + x, Y + y);

            public void Print() => WriteLine(data);

            public string Name => FirstName + " " + LastName;

            public Customer this[int id] => db.FindCustomer(id);                 //  public static Complex operator + (Complex a, Complex b) => a.Add(b);
        }

        class Point
        {
            public Point(int x, int y) { }
        }

        class DB
        {
            Customer customer = new Customer();
            public Customer FindCustomer(int id)        { return customer; }
        }

        class Complex
        {                                                                       // static Complex complex;
            public Complex Add(int number)              { return this;  }       // static Complex operator + (Complex com)     { return complex;  }
        }
    }
}