using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ex4_WPF
{                                                               
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private IntPtr _hpHandle;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var wordsize = IntPtr.Size;
            if (wordsize != 4)
            {
                //are we in 32 or 64 bit land? doesn't matter!
            }

            var datSize = Marshal.SizeOf(typeof(TestData));
            _hpHandle = Heap.HeapCreate(0, UIntPtr.Zero, UIntPtr.Zero);
            var ptr = Heap.HeapAlloc(_hpHandle, 0, (UIntPtr)datSize);

            var dataOrig = new TestData { data1 = 1, data2 = 2 };
            Marshal.StructureToPtr(dataOrig, ptr, true);
            var tryd1 = Marshal.ReadInt32(ptr);
            var tryd2 = Marshal.ReadInt32(ptr + 4);                                                 // change the memory window contents in the debugger:  repeat this stmt by using Set Next Statement

            var dataCopy = Marshal.PtrToStructure(ptr, typeof(TestData));
            Heap.HeapFree(_hpHandle, 0, ptr);
            this.Closed += new EventHandler(on_close);
        }

        private void on_close(object sender, EventArgs e)
        {
            Heap.HeapDestroy(_hpHandle);
        }
    }

    struct TestData
    {
        public int data1;
        public int data2;

        public override string ToString()
        {
            return data1.ToString() + " " + data2.ToString();
        }
    }

    public class Heap
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr HeapCreate(uint flOptions, UIntPtr dwInitialsize, UIntPtr dwMaximumSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr HeapAlloc(IntPtr hHeap, uint dwFlags, UIntPtr dwSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool HeapFree(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool HeapDestroy(IntPtr hHeap);
    }
}

