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
        /* CHAPTER 1. Basics */
        [DllImport("libcpdf.so")] static extern int cpdf_fromFile(string filename, string userpw);
        [DllImport("libcpdf.so")] static extern void cpdf_toFile(int pdf, string filename, int linearize, int make_id);
        static void Main(string[] args)
        {
            /* CHAPTER 0. Preliminaries */
            IntPtr[] camlargs = {};
            cpdf_startup(camlargs);
            Console.WriteLine(Marshal.PtrToStringAuto(cpdf_version()));
            cpdf_setSlow();
            cpdf_setFast();
            //Console.WriteLine("lastError = %i\n", cpdf_lastError());
            //Console.WriteLine("lastErrorString = %s\n", Marshal.PtrToStringAuto(cpdf_lastErrorString()));
            cpdf_onExit();
            /* CHAPTER 1. Basics */
            int pdf = cpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
            int cpdf_false = 0;
            int cpdf_true = 1;
            cpdf_toFile(pdf, "testoutputs/out.pdf", cpdf_false, cpdf_true);
        }
    }
}
