using System;
using System.Runtime.InteropServices;

namespace dotnet_libcpdf
{
    class Program
    {
        [DllImport("libcpdf.so")]
        static extern void cpdf_startup(IntPtr[] ptr);
        [DllImport("libcpdf.so")]
        static extern IntPtr cpdf_version();
        [DllImport("libcpdf.so")]
        static extern void cpdf_setFast();
        [DllImport("libcpdf.so")]
        static extern void cpdf_setSlow();
        static void Main(string[] args)
        {
            IntPtr[] camlargs = {};
            cpdf_startup(camlargs);
            Console.WriteLine("Hello World!");
            Console.WriteLine(Marshal.PtrToStringAuto(cpdf_version()));
        }
    }
}
