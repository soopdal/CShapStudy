


using System;
using System.Collections;                                                       // IEnumerator
using System.Collections.Generic;                                               // Generaic, List<T>, Dictionary<T>, LinkedList<T>
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Ex2_Version
{
    partial class Program
    {
        //---------------------------------------------------------------------
        //                      C# 2.0
        //---------------------------------------------------------------------
        static void runCSharp2()                                                
        {
            Console.WriteLine("\n\n\t\tC# 2.0");

            runGeneric();                                                       // 제너릭
            runConstraint();                                                    // 제너릭 제한
            runAnonymousMethod();                                               // 무명 메소드

            runNullableType();                                                  // 널러블 타입
            runPartialType();
            runYield();
            runCovarianceAndContravariance();
        }

        //---------------------------------------------------------------------
        //                      제너릭     C# 2.0
        //---------------------------------------------------------------------
        static void runGeneric()
        {
            Console.WriteLine("\tGeneric");

            List<string> nameList = new List<string>();
            nameList.Add("홍길동");
            nameList.Add("이태백");

            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic["길동"] = 100;
            dic["태백"] = 90;

            Stack<int> numbers = new Stack<int>();
            numbers.Push(1);
            int number = numbers.Pop();

            Console.WriteLine("pop number {0}", number);
        }

        //---------------------------------------------------------------------
        //                      제너릭 예   C# 2.0
        //---------------------------------------------------------------------
        class Stack<T>
        {
            T[] elements;
            int pos;                                                            // 전역변수는 초기화 될 것

            public Stack()
            {
                elements = new T[100];
            }

            public void Push(T element)
            {
                elements[++pos] = element;
            }

            public T Pop()
            {
                return elements[pos--];
            }
        }

        Stack<int> numberStack = new Stack<int>();
        Stack<string> nameStack = new Stack<string>();

        //---------------------------------------------------------------------
        //                      제너릭 제한사항    C# 2.0
        //---------------------------------------------------------------------
        static void runConstraint()
        {
            Console.WriteLine("\tGeneric constraint");
        }

        //---------------------------------------------------------------------
        //                      제너릭 제한사항 예  C# 2.0
        //---------------------------------------------------------------------
        class Struct<T> where T : struct { }             // T는 밸류타입
        class Class<T> where T : class { }             // T는 레퍼런스타입
        class Constructor<T> where T : new() { }             // T는 디폴트 생성자를 가져야 함

        class Base { }
        class Derived<T> where T : Base { }             // T는 Base클래스의 파생클래스이어야 함

        interface Interface<T> { }
        class InterfaceOwner<T> where T : Interface<T> { }             // T는 Interface의 인터페이스를 가져야 함

        class Complex<T> where T : Base, Interface<T>, new() { }             // 좀 더 복잡한 제약들

        //---------------------------------------------------------------------
        //                      무명 메소드  C# 2.0
        //---------------------------------------------------------------------
        public delegate int SumDelegate(int a, int b);                          // delegate 타입
        //SumDelegate sumDel = new SumDelegate(mySum);                          // delegate 사용

        static Form1 form;

        static void runAnonymousMethod()
        {
            Console.WriteLine("\tAnonymous method");
            form = new Form1();
            Application.Run(form);
        }

        //---------------------------------------------------------------------
        //                      Nullable Type  C# 2.0
        // int나 DateTime과 같은 Value Type은 일반적으로 Null을 가질 수 없지만, C# 2.0부터 이러한 타입에 Null을 가질 수 있게 하였다.
        //---------------------------------------------------------------------
        static void runNullableType()
        {
            Console.WriteLine("\tNullable type");

            string s;                                                           // Null을 가질 수 있는 문자열
            s = null;
            Console.WriteLine("s = \"{0}\"", s);

            int? i = null;                                                      // ?를 타입명 뒤에 붙이면 Nullable Type이 된다.
            i = 101;
            Console.WriteLine("i = {0}", i);

            bool? b = null;
            b = !!b;
            Console.WriteLine("b = {0}", b);

            Nullable<int> j = null;
            j = 10;
            int k = j.Value;                                                      // Value타입으로 바꿀 때는 Value 속성을 사용한다.
            Console.WriteLine("k = {0}", k);
        }

        //---------------------------------------------------------------------
        //                      Partial Type  C# 2.0
        //---------------------------------------------------------------------
        partial class PartialClass                                              // Partial Class
        {
            public void Run() { }
        }

        partial class PartialClass
        {
            public void Get() { }
        }

        partial class PartialClass
        {
            public void Put() { }
        }

        partial struct PartialStruct
        {
            public int      ID;
            public string   name;                                               // 밑에 경고 때문에 여기로 옮겨옴
        }

        partial struct PartialStruct
        {                                                                       // public string name;      partial 구조체 'Program.PartialStruct'의 여러 선언에서 필드 간 순서가 정의되어 있지 않습니다.순서를 지정하려면 모든 인스턴스 필드가 같은 선언에 있어야 합니다.Ex2_Version C:\project\Tup2\test\C#Study\Ex2_Version\Ex2_Version\ProgramSharp2.cs	164	활성
            public PartialStruct(int id, string name)
            {
                this.ID     = id;
                this.name   = name;
            }
        }

        partial interface PartialInterface
        {
            string Name { get; set;  }
        }

        partial interface PartialInterface
        {
            void Do();
        }

        public class DoClass : PartialInterface
        {
            public string Name { get; set;  }
            public void Do()    { }
        }

        static void runPartialType()
        {
            Console.WriteLine("\tPartial type");
        }

        //---------------------------------------------------------------------
        //                      Yield  C# 2.0
        //---------------------------------------------------------------------
        static void runYield()
        {
            Console.WriteLine("\tYield");
            foreach (int number in getNumber())
                Console.WriteLine(number);

            var list = new DalList();

            foreach( var item in list )
                Console.WriteLine(item);

            IEnumerator it = list.GetEnumerator();
            it.MoveNext();
            Console.WriteLine(it.Current);

            it.MoveNext();
            Console.WriteLine(it.Current);
        }

        static IEnumerable<int> getNumber()
        {
            yield return 10;                                                    // 첫 번째 루프에서 리턴되는 값
            yield return 20;                                                    // 두 번째 루프에서 리턴되는 값
            yield return 30;                                                    // 세 번째 루프에서 리턴되는 값
        }

        public class DalList
        {
            private int[] data = { 1, 2, 3, 4, 5 };

            public IEnumerator GetEnumerator()
            {
                int i = 0;
                while(i < data.Length)
                {
                    yield return data[i];
                    i++;
                }
            }
        }

        static void runCovarianceAndContravariance()                            // 코베리언스, 콘트라베리언스
        {
            Console.WriteLine("\t공변성과 역공변성");
        }
    }
}
