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
        [DllImport("libcpdf.so")] static extern int cpdf_fromFileLazy(string filename, string userpw);
        [DllImport("libcpdf.so")] static extern void cpdf_toFile(int pdf, string filename, int linearize, int make_id);
        [DllImport("libcpdf.so")] static extern int cpdf_blankDocument(double w, double h, int pages);
        [DllImport("libcpdf.so")] static extern int cpdf_blankDocumentPaper(int papersize, int pages);
        static void Main(string[] args)
        {
            int cpdf_false = 0;
            int cpdf_true = 1;

            int cpdf_a0portrait = 0;
            int cpdf_a1portrait = 1;
            int cpdf_a2portrait = 2;
            int cpdf_a3portrait = 3;
            int cpdf_a4portrait = 4;
            int cpdf_a5portrait = 5;
            int cpdf_a0landscape = 6;
            int cpdf_a1landscape = 7;
            int cpdf_a2landscape = 8;
            int cpdf_a3landscape = 9;
            int cpdf_a4landscape = 10;
            int cpdf_a5landscape = 11;
            int cpdf_usletterportrait = 12;
            int cpdf_usletterlandscape = 13;
            int cpdf_uslegalportrait = 14;
            int cpdf_uslegallandscape = 15;

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
            int pdf2 = cpdf_fromFileLazy("testinputs/cpdflibmanual.pdf", "");
            //FIXME fromMemory
            //FIXME fromMemoryLazy
            int pdf3 = cpdf_blankDocument(153.5, 234.2, 50);
            int pdf4 = cpdf_blankDocumentPaper(cpdf_a4landscape, 50);
            cpdf_toFile(pdf3, "testoutputs/blank.pdf", cpdf_false, cpdf_true);
            cpdf_toFile(pdf4, "testoutputs/blankpaper.pdf", cpdf_false, cpdf_true);
            cpdf_toFile(pdf, "testoutputs/out.pdf", cpdf_false, cpdf_true);
        }
    }
}
