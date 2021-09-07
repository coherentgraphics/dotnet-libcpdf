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
        [DllImport("libcpdf.so")] static extern void cpdf_deletePdf(int pdf);
        [DllImport("libcpdf.so")] static extern void cpdf_replacePdf(int pdf, int pdf2);
        [DllImport("libcpdf.so")] static extern int cpdf_startEnumeratePDFs();
        [DllImport("libcpdf.so")] static extern int cpdf_enumeratePDFsKey(int n);
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_enumeratePDFsInfo(int n);
        [DllImport("libcpdf.so")] static extern void cpdf_endEnumeratePDFs();
        [DllImport("libcpdf.so")] static extern double cpdf_ptOfCm(double i);
        [DllImport("libcpdf.so")] static extern double cpdf_ptOfMm(double i);
        [DllImport("libcpdf.so")] static extern double cpdf_ptOfIn(double i);
        [DllImport("libcpdf.so")] static extern double cpdf_cmOfPt(double i);
        [DllImport("libcpdf.so")] static extern double cpdf_mmOfPt(double i);
        [DllImport("libcpdf.so")] static extern double cpdf_inOfPt(double i);
        [DllImport("libcpdf.so")] static extern int cpdf_parsePagespec(int pdf, string pagespec);
        [DllImport("libcpdf.so")] static extern int cpdf_validatePagespec(string pagespec);
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_stringOfPagespec(int pdf, int r);
        [DllImport("libcpdf.so")] static extern int cpdf_blankRange();
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        [DllImport("libcpdf.so")] static extern int cpdf_range(int f, int t);
        [DllImport("libcpdf.so")] static extern int cpdf_all(int pdf);
        [DllImport("libcpdf.so")] static extern int cpdf_even(int r);
        [DllImport("libcpdf.so")] static extern int cpdf_odd(int r);
        [DllImport("libcpdf.so")] static extern int cpdf_rangeUnion(int a, int b);
        [DllImport("libcpdf.so")] static extern int cpdf_difference(int a, int b);
        [DllImport("libcpdf.so")] static extern int cpdf_removeDuplicates(int r);
        [DllImport("libcpdf.so")] static extern int cpdf_rangeLength(int r);
        [DllImport("libcpdf.so")] static extern int cpdf_rangeGet(int r, int n);
        [DllImport("libcpdf.so")] static extern int cpdf_rangeAdd(int r, int page);
        [DllImport("libcpdf.so")] static extern int cpdf_isInRange(int r, int page);
        [DllImport("libcpdf.so")] static extern int cpdf_pages(int pdf);
        [DllImport("libcpdf.so")] static extern int cpdf_pagesFast(string password, string filename);
        /* CHAPTER 2. Merging and Splitting */

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
            cpdf_deletePdf(pdf);
            cpdf_replacePdf(pdf3, pdf4);
            int n = cpdf_startEnumeratePDFs();
            for (int x = 0; x < n; x++)
            {
               int key = cpdf_enumeratePDFsKey(x);
               string info = Marshal.PtrToStringAuto(cpdf_enumeratePDFsInfo(x));
            }
            cpdf_endEnumeratePDFs();
            Console.WriteLine("{0:N}", cpdf_ptOfCm(1.0));
            Console.WriteLine("{0:N}", cpdf_ptOfMm(1.0));
            Console.WriteLine("{0:N}", cpdf_ptOfIn(1.0));
            Console.WriteLine("{0:N}", cpdf_cmOfPt(1.0));
            Console.WriteLine("{0:N}", cpdf_mmOfPt(1.0));
            Console.WriteLine("{0:N}", cpdf_inOfPt(1.0));
            int r = cpdf_parsePagespec(pdf3, "1-2,5-end");
            int valid = cpdf_validatePagespec("1-2");
            Console.WriteLine(Marshal.PtrToStringAuto(cpdf_stringOfPagespec(pdf3, r)));
            int b = cpdf_blankRange();
            cpdf_deleteRange(b);
            int range = cpdf_range(1, 10);
            int all = cpdf_all(pdf3);
            int even = cpdf_even(all);
            int odd = cpdf_odd(all);
            int union = cpdf_rangeUnion(even, odd);
            int diff = cpdf_difference(even, odd);
            int revdup = cpdf_removeDuplicates(even);
            int length = cpdf_rangeLength(even);
            int rangeget = cpdf_rangeGet(even, 1);
            int isin = cpdf_isInRange(even, 2);
        }
    }
}
