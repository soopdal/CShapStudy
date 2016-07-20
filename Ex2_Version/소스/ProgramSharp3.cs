using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2_Version
{
    public partial class Program
    {
        //---------------------------------------------------------------------
        //                      C# 3.0
        //---------------------------------------------------------------------
        static public void runCSharp3()                                         
        {
            Console.WriteLine("\n\n\t\tC# 3.0");

            runPartialMethod();
            runLamdaExpression();
            runAnonymousType();                                                 // 무명타입   
            runExtendedMethod();
            runLINQ();
            runExpressionTree();
        }

        //---------------------------------------------------------------------
        //                      파셜 메소드
        //---------------------------------------------------------------------
        static partial void runPartialMethod();                                 // 다른 곳에 구현부가 없으면 컴파일러가 이 줄을 제거한다.

        //---------------------------------------------------------------------
        //                      람다식
        //---------------------------------------------------------------------
        static void runLamdaExpression()
        {
            Console.WriteLine("\tLamda expression");
            // ()                  => Console.Write("No");
            // (p)                 => Console.Write(p);
            // (s, e)              => Console.Write(e);
            // (string s, int i)   => Console.Write(s, i);

            // var proj = db.Projects.Where(prop => prop.Name == strName);      // LINQ (Language Integrated Query)
        }

        //---------------------------------------------------------------------
        //                       익명타입 
        //---------------------------------------------------------------------
        static void runAnonymousType()                                                                            
        {
            Console.WriteLine("\tAnonymous type");

            var v = new[]
            {
                new { Name = "Lee",     Age = 22 },                             // 익명타입은 new {}
                new { Name = "Kim",     Age = 33 },
                new { Name = "Part",    Age = 44 },
            };

            var under30 = v.FirstOrDefault(p => p.Age > 30);
            if (under30 != null)
            {
                Console.WriteLine(under30.Name);
            }
        }

        //---------------------------------------------------------------------
        //                      확장메소드
        //---------------------------------------------------------------------
        static void runExtendedMethod()                                         // 확장메소드                                    
        {
            Console.WriteLine("\tExtended method");

            string s = "This is a Test";
            string s2 = s.ToChangeCase();                                       // bool found = s.Found('z');
        }

        //---------------------------------------------------------------------
        //                      링크
        //---------------------------------------------------------------------
        static void runLINQ()                                         // 확장메소드                                    
        {
            Console.WriteLine("\tLINQ");
        }

        //---------------------------------------------------------------------
        //                      표현트리
        //---------------------------------------------------------------------
        static void runExpressionTree()                                         // 확장메소드                                    
        {
            Console.WriteLine("\tExpression tree");
        }
    }

    public partial class Program
    {
        static partial void runPartialMethod()
        {
            Console.WriteLine("\tPartial method is private void return only");
        }
    }

    public static class ExClass
    {
        public static string ToChangeCase(this String str)
        {
            StringBuilder builder = new StringBuilder();
            foreach( var ch in str )
            {
                if (ch > 'A' && ch <= 'Z')
                    builder.Append((char)('a' + ch - 'A'));
                else if (ch >= 'a' && ch <= 'x')
                    builder.Append((char)('A' + ch - 'a'));
                else
                    builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
