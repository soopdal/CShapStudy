
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex5_Exception
{
    /// <summary>
    /// Heap Corruption Exception 0xC0000374    /// https://blogs.msdn.microsoft.com/calvin_hsia/2015/01/30/heap-corruption-exception-0xc0000374/
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            for (int iter = 0; iter < 1000; iter++)
            {
                var highContrastStruct = new NativeMethods.HIGHCONTRAST();
                highContrastStruct.cbSize = Marshal.SizeOf(highContrastStruct);
                var isHighContrast = NativeMethods.SystemParametersInfo(
                    NativeMethods.SPI_GETHIGHCONTRAST,
                    highContrastStruct.cbSize,
                    ref highContrastStruct,
                    fWinIni: 0
                    );
                var IsHighContrast = highContrastStruct.dwFlags & NativeMethods.HCF_HIGHCONTRASTON;
#if SHOW_PINVOKE_BUG
                var name = highContrastStruct.lpszDefaultScheme;
#else
                var name = Marshal.PtrToStringUni(highContrastStruct.lpszDefaultScheme);
#endif

                var txt = string.Format("IsHighContrast = {0}  Name = {1}",
                    IsHighContrast,
                    name);
                Write(txt);

                var types = typeof(int).Assembly.GetTypes();
                var lstAddr = new List<IntPtr>();
                var hp = NativeMethods.GetProcessHeap();
                int cntr = 0;
                foreach (var typ in types)
                {
                    cntr++;
                    var str = typ.Name;
                    Write(string.Format("Iter = {0} Cntr = {1} {2}", iter, cntr, str));
                    var enc = new System.Text.UnicodeEncoding();
                    var bytes = enc.GetBytes(str);
                    
                    Array.Resize<byte>(ref bytes, bytes.Length + 2);                                // add trailing null terminator (for Unicode, 2 null bytes)
                    var addr = NativeMethods.HeapAlloc(hp, 0, bytes.Length);                        // allocate some memory 
                    
                    lstAddr.Add(addr);                                                              // add the address to the list of memory locations we've allocated // so we can free them later
                    Marshal.Copy(bytes, 0, addr, bytes.Length);

                    //uncomment this code to see another heap corruption
                    // this one allocates 1000 bytes, then frees it
                    // then copies data to it
                    CorruptHeapAlloc();
                }
                Array.ForEach<IntPtr>(lstAddr.ToArray(), addr =>
                {
                    //now free the allocations
                    NativeMethods.HeapFree(hp, 0, addr);
                });
            }
        }

        private static void CorruptHeapAlloc()
        {
            // Disable vshost process: Project->Properties ->Debug->Enable the Visual Studio Host Process
            // Enable native debugging : Project->Properties ->Debug->Enable native code debugging
            // Disable Just My Code Debugging: Tools->Options->Debugging->General
            // Debug->Exceptions-> enable both Common Langauge Runtime, Win32 Exceptions


            var hp = NativeMethods.GetProcessHeap();
            var addr = NativeMethods.HeapAlloc(hp, 0, 1000);

            Marshal.FreeCoTaskMem(addr); // or NativeMethods.HeapFree
            var enc = new System.Text.UnicodeEncoding();
            // create a string of 1000 "a"
            var str = new string('a', 1000);
            var bytes = enc.GetBytes(str);
            // copy the bytes to some address to which we have write access 
            // but that we don't own.
            Marshal.Copy(bytes, startIndex: 0, destination: addr + 800, length: 800);
        }

        private static void Write(string txt)
        {
            //            Debug.WriteLine(txt);
            Console.WriteLine(txt);
        }
    }
    public class NativeMethods
    {
        public const int HCF_HIGHCONTRASTON = 0x0001;

        public const int SPI_GETHIGHCONTRAST = 0x0042;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct HIGHCONTRAST
        {
            public int cbSize;
            public int dwFlags;
#if SHOW_PINVOKE_BUG
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszDefaultScheme;
#else
            public IntPtr lpszDefaultScheme;
#endif
            /*
            /*/
            //*/
        }


        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal extern static bool SystemParametersInfo(
            int uiAction,
            int uiParam,
            ref HIGHCONTRAST pvParam,
            int fWinIni);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr HeapAlloc(IntPtr hHeap, uint dwFlags, int dwSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool HeapFree(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcessHeap();
    }
}
