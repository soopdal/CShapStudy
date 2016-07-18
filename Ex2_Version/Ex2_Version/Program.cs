
using System.Collections.Generic;                                               // Generaic, List<T>, Dictionary<T>, LinkedList<T>

using System;
//using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex2_Version
{
    class Program
    {
        static void Main(string[] args)
        {
            runCSharp2();
        }

        //---------------------------------------------------------------------
        //                      C# 2.0
        //---------------------------------------------------------------------
        static void runCSharp2()                                                // C# 2.0
        {
            runGeneric();                                                       // 제너릭
            runConstraint();                                                    // 제너릭 제한
            runAnonymousMethod();                                               // 무명 메소드
            runNullableType();                                                  // 널러블 타입
        }

        //---------------------------------------------------------------------
        //                      제너릭     C# 2.0
        //---------------------------------------------------------------------
        static void runGeneric()
        {
            System.Console.WriteLine("C# 2.0 Generic");

            List<string> nameList = new List<string>();
            nameList.Add("홍길동");
            nameList.Add("이태백");

            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic["길동"] = 100;
            dic["태백"] = 90;

            Stack<int> numbers = new Stack<int>();
            numbers.Push(1);
            int number = numbers.Pop();

            System.Console.WriteLine("pop number {0}", number);
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
            System.Console.WriteLine("Generic constraint");
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
            System.Console.WriteLine("Anonymous method");
            form = new Form1();
            Application.Run(form);
        }

        //---------------------------------------------------------------------
        //                      Nullable Type  C# 2.0
        // int나 DateTime과 같은 Value Type은 일반적으로 Null을 가질 수 없지만, C# 2.0부터 이러한 타입에 Null을 가질 수 있게 하였다.
        //---------------------------------------------------------------------
        static void runNullableType()
        {
            System.Console.WriteLine("Nullable type");

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
    }
}
