using System;
using System.Runtime.InteropServices;

namespace dotnet_libcpdf
{
    class Program
    {
        /* CHAPTER 0. Preliminaries */
        [DllImport("libcpdf.so")] static extern void cpdf_startup(IntPtr[] ptr);
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_version();
        [DllImport("libcpdf.so")] static extern void cpdf_setFast();
        [DllImport("libcpdf.so")] static extern void cpdf_setSlow();
        [DllImport("libcpdf.so")] static extern int cpdf_lastError();
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_lastErrorString();
        [DllImport("libcpdf.so")] static extern void cpdf_clearError();
        [DllImport("libcpdf.so")] static extern void cpdf_onExit();

        static void Main(string[] args)
        {
            IntPtr[] camlargs = {};
            cpdf_startup(camlargs);
            Console.WriteLine(Marshal.PtrToStringAuto(cpdf_version()));
            cpdf_setSlow();
            cpdf_setFast();
            //Console.WriteLine("lastError = %i\n", cpdf_lastError());
            //Console.WriteLine("lastErrorString = %s\n", Marshal.PtrToStringAuto(cpdf_lastErrorString()));
            cpdf_onExit();
        }
    }
}
