using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex3_Grammer
{
    class Program
    {
        static void Main(string[] args)
        {
            runJaggedArray();
        }

        static void runJaggedArray()                                            // 고정된 Rectangular 배열과 가변되는 Jagged 배열
        {
            int[][] a = new int[3][];

            a[0] = new int[2];
            a[1] = new int[3] { 1, 2, 3 };
            a[2] = new int[4] { 1, 2, 3, 4 };
        }
    }
}
