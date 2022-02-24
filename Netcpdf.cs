using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace CoherentGraphics
{
public class Cpdf
{
#pragma warning disable 414


    public static int a1portrait = 1;
    public static int a2portrait = 2;
    public static int a3portrait = 3;
    public static int a4portrait = 4;
    public static int a5portrait = 5;
    public static int a0landscape = 6;
    public static int a1landscape = 7;
    public static int a2landscape = 8;
    public static int a3landscape = 9;
    public static int a4landscape = 10;
    public static int a5landscape = 11;
    public static int usletterportrait = 12;
    public static int usletterlandscape = 13;
    public static int uslegalportrait = 14;
    public static int uslegallandscape = 15;

    public static int noEdit = 0;
    public static int noPrint = 1;
    public static int noCopy = 2;
    public static int noAnnot = 3;
    public static int noForms = 4;
    public static int noExtract = 5;
    public static int noAssemble = 6;
    public static int noHqPrint = 7;

    public static int pdf40bit = 0;
    public static int pdf128bit = 1;
    public static int aes128bitfalse = 2;
    public static int aes128bittrue = 3;
    public static int aes256bitfalse = 4;
    public static int aes256bittrue = 5;
    public static int aes256bitisofalse = 6;
    public static int aes256bitisotrue = 7;

    public static int posCentre = 0;
    public static int posLeft = 1;
    public static int posRight = 2;
    public static int top = 3;
    public static int topLeft = 4;
    public static int topRight = 5;
    public static int left = 6;
    public static int bottomLeft = 7;
    public static int bottom = 8;
    public static int bottomRight = 9;
    public static int right = 10;
    public static int diagonal = 11;
    public static int reverseDiagonal = 12;

    public struct position
    {
        public int anchor;
        public double coord1;
        public double coord2;

        public position(int anchor, double coord1, double coord2)
        {
            this.anchor = anchor;
            this.coord1 = coord1;
            this.coord2 = coord2;
        }
    }

    public static int timesRoman = 0;
    public static int timesBold = 1;
    public static int timesItalic = 2;
    public static int timesBoldItalic = 3;
    public static int helvetica = 4;
    public static int helveticaBold = 5;
    public static int helveticaOblique = 6;
    public static int helveticaBoldOblique = 7;
    public static int courier = 8;
    public static int courierBold = 9;
    public static int courierOblique = 10;
    public static int courierBoldOblique = 11;

    public static int leftJustify = 0;
    public static int CentreJustify = 1;
    public static int RightJustify = 2;

    public static int singlePage = 0;
    public static int oneColumn = 1;
    public static int twoColumnLeft = 2;
    public static int twoColumnRight = 3;
    public static int twoPageLeft = 4;
    public static int twoPageRight = 5;

    public static int useNone = 0;
    public static int useOutlines = 1;
    public static int useThumbs = 2;
    public static int useOC = 3;
    public static int useAttachments = 4;

    public static int decimalArabic = 0;
    public static int uppercaseRoman = 1;
    public static int lowercaseRoman = 2;
    public static int uppercaseLetters = 4;
    public static int lowercaseLetters = 5;

#pragma warning restore 414

    public class Pdf: IDisposable
    {
        public int pdf = -1;
        private bool disposed = false;

        public Pdf(int pdf)
        {
            this.pdf = pdf;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                [DllImport("libcpdf.so")] static extern void cpdf_deletePdf(int pdf);
                //Console.WriteLine($"**************deleting PDF {this.pdf}");
                cpdf_deletePdf(this.pdf);
                this.pdf = -1;
                disposed = true;
            }
        }

        ~Pdf()
        {
            Dispose(disposing: false);
        }
    }

    public class CPDFError : Exception
    {
        public CPDFError(string error) : base(error)
        {
        }
    }

    public static void checkerror()
    {
        if (lastError() != 0)
        {
            clearError();
            throw new CPDFError(lastErrorString());
        }
    }

    public static List<int> list_of_range(int r)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_rangeLength(int r);
        [DllImport("libcpdf.so")] static extern int cpdf_rangeGet(int r, int n);
        List<int> l = new List<int>();
        for (int x = 0; x < cpdf_rangeLength(r); x++)
        {
            l.Add(cpdf_rangeGet(r, x));
        }
        checkerror();
        return l;
    }

    public static int range_of_list(List<int> l)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        [DllImport("libcpdf.so")] static extern int cpdf_blankRange();
        [DllImport("libcpdf.so")] static extern int cpdf_rangeAdd(int r, int n);
        int r = cpdf_blankRange();
        for (int x = 0; x < l.Count; x++)
        {
            int rn = cpdf_rangeAdd(r, l[x]);
            cpdf_deleteRange(r);
            r = rn;
        }
        checkerror();
        return r;
    }

    /* CHAPTER 0. Preliminaries */

    /// <summary>
    /// Initialises the CPDF library. Must be called before any other function.
    /// </summary>
    public static void startup()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_startup(IntPtr[] argv);
        IntPtr[] args = {};
        cpdf_startup(args);
        checkerror();
    }

    /// <summary>
    /// Returns a string giving the version number of the CPDF library.
    /// </summary>
    public static string version()
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_version();
        string s = Marshal.PtrToStringUTF8(cpdf_version());
        checkerror();
        return s;
    }
   
    /// <summary>
    /// Some operations have a fast mode. The default is 'slow' mode, which works
    /// even on old-fashioned files. For more details, see section 1.13 of the
    /// CPDF manual. This function sets the mode to fast globally.
    /// </summary>
    public static void setFast()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setFast();
        cpdf_setFast();
        checkerror();
    }

    /// <summary>
    /// Some operations have a fast mode. The default is 'slow' mode, which works
    /// even on old-fashioned files. For more details, see section 1.13 of the
    /// CPDF manual. This functions sets the mode to slow globally.
    /// </summary>
    public static void setSlow()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setSlow();
        cpdf_setSlow();
        checkerror();
    }

    /// <summary>
    /// Not to be called directly. Errors in .NET cpdf are raised by exceptions.
    /// </summary>
    public static int lastError()
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fLastError();
        return cpdf_fLastError();
    }

    /// <summary>
    /// Not to be called directly. Errors in .NET cpdf are raised by exceptions.
    /// </summary>
    public static string lastErrorString()
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_fLastErrorString();
        return Marshal.PtrToStringUTF8(cpdf_fLastErrorString());
    }

    /// <summary>
    /// Not to be called directly. Errors in .NET cpdf are raised by exceptions.
    /// </summary>
    public static void clearError()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_clearError();
        cpdf_clearError();
    }

    /// <summary>
    /// onExit is a debug function which prints some information about
    /// resource usage. This can be used to detect if PDFs or ranges are being
    /// deallocated properly. Contrary to its name, it may be run at any time.
    /// </summary>
    public static void onExit()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_onExit();
        cpdf_onExit();
        checkerror();
    }


    /* CHAPTER 1. Basics */

    /// <summary>
    /// fromFile(filename, userpw) loads a PDF file from a given file. Supply
    /// a user password (possibly blank) in case the file is encrypted. It won't be
    /// decrypted, but sometimes the password is needed just to load the file.
    /// </summary>
    public static Pdf fromFile(string filename, string userpw)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fromFile(string filename, string userpw);
        int res =  cpdf_fromFile(filename, userpw);
        checkerror();
        return new Pdf(res);
    }

    /// <summary>
    /// fromFileLazy(pdf, userpw) loads a PDF from a file, doing only minimal
    /// parsing. The objects will be read and parsed when they are actually
    /// needed. Use this when the whole file won't be required. Also supply a user
    /// password (possibly blank) in case the file is encrypted. It won't be
    /// decrypted, but sometimes the password is needed just to load the file.
    /// </summary>
    public static Pdf fromFileLazy(string filename, string userpw)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fromFileLazy(string filename, string userpw);
        int res = cpdf_fromFileLazy(filename, userpw);
        checkerror();
        return new Pdf(res);
    }

    /// <summary>
    /// fromMemory(data, userpw) loads a file from memory given any user password.
    /// </summary>
    public static Pdf fromMemory(byte[] data, string userpw)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fromMemory(byte[] data, int length, string userpw);
        int pdf = cpdf_fromMemory(data, data.Length, userpw);
        checkerror();
        return new Pdf(pdf);
    }

    /// <summary>
    /// fromMemory(data, length, userpw) loads a file from memory, given a
    /// pointer and a length, and the user password, but lazily like
    /// fromFileLazy. The caller must use AllocHGlobal / Marshal.Copy / FreeHGlobal
    /// itself. It must not free the memory until the PDF is also gone.
    /// </summary>
    public static Pdf fromMemoryLazy(IntPtr data, int length, string userpw)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fromMemoryLazy(IntPtr data, int length, string userpw);
        int pdf = cpdf_fromMemoryLazy(data, length, userpw);
        checkerror();
        return new Pdf(pdf);
    }

    /// <summary>
    /// To enumerate the list of currently allocated PDFs, call
    /// startEnumeratePDFs which gives the number, n, of PDFs allocated, then
    /// enumeratePDFsInfo and enumeratePDFsKey with index numbers from
    /// 0...(n - 1). Call endEnumeratePDFs to clean up.
    /// </summary>
    public static int startEnumeratePDFs()
    {
        [DllImport("libcpdf.so")] static extern int cpdf_startEnumeratePDFs();
        int res = cpdf_startEnumeratePDFs();
        checkerror();
        return res;
    }

    /// <summary>
    /// To enumerate the list of currently allocated PDFs, call
    /// startEnumeratePDFs which gives the number, n, of PDFs allocated, then
    /// enumeratePDFsInfo and enumeratePDFsKey with index numbers from
    /// 0...(n - 1). Call endEnumeratePDFs to clean up.
    /// </summary>
    public static int enumeratePDFsKey(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_enumeratePDFsKey(int n);
        int res = cpdf_enumeratePDFsKey(n);
        checkerror();
        return res;
    }

    /// <summary>
    /// To enumerate the list of currently allocated PDFs, call
    /// startEnumeratePDFs which gives the number, n, of PDFs allocated, then
    /// enumeratePDFsInfo and enumeratePDFsKey with index numbers from
    /// 0...(n - 1). Call endEnumeratePDFs to clean up.
    /// </summary>
    public static string enumeratePDFsInfo(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_enumeratePDFsInfo(int n);
        string res = Marshal.PtrToStringUTF8(cpdf_enumeratePDFsInfo(n));
        checkerror();
        return res;
    }

    /// <summary>
    /// To enumerate the list of currently allocated PDFs, call
    /// startEnumeratePDFs which gives the number, n, of PDFs allocated, then
    /// enumeratePDFsInfo and enumeratePDFsKey with index numbers from
    /// 0...(n - 1). Call endEnumeratePDFs to clean up.
    /// </summary>
    public static void endEnumeratePDFs()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endEnumeratePDFs();
        cpdf_endEnumeratePDFs();
        checkerror();
    }

    /// <summary>
    /// Convert a figure in centimetres to points (72 points to 1 inch)
    /// </summary>
    public static double ptOfCm(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_ptOfCm(double i);
        double res = cpdf_ptOfCm(i);
        checkerror();
        return res;
    }

    /// <summary>
    /// Convert a figure in millimetres to points (72 points to 1 inch)
    /// </summary>
    public static double ptOfMm(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_ptOfMm(double i);
        double res = cpdf_ptOfMm(i);
        checkerror();
        return res;
    }

    /// <summary>
    /// Convert a figure in inches to points (72 points to 1 inch)
    /// </summary>
    public static double ptOfIn(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_ptOfIn(double i);
        double res = cpdf_ptOfIn(i);
        checkerror();
        return res;
    }

    /// <summary>
    /// Convert a figure in points to centimetres (72 points to 1 inch)
    /// </summary>
    public static double cmOfPt(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_cmOfPt(double i);
        double res = cpdf_cmOfPt(i);
        checkerror();
        return res;
    }

    /// <summary>
    /// Convert a figure in points to millimetres (72 points to 1 inch)
    /// </summary>
    public static double mmOfPt(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_mmOfPt(double i);
        double res = cpdf_mmOfPt(i);
        checkerror();
        return res;
    }

    /// <summary>
    /// Convert a figure in points to inches (72 points to 1 inch)
    /// </summary>
    public static double inOfPt(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_inOfPt(double i);
        double res = cpdf_inOfPt(i);
        checkerror();
        return res;
    }

    /// <summary>
    /// parsePagespec(pdf, range) parses a page specification with reference
    /// to a given PDF (the PDF is supplied so that page ranges which reference
    /// pages which do not exist are rejected).
    /// </summary>
    public static List<int> parsePagespec(Pdf pdf, string pagespec)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_parsePagespec(int pdf, string pagespec);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int r = cpdf_parsePagespec(pdf.pdf, pagespec);
        List<int> r_out = list_of_range(r);
        cpdf_deleteRange(r);
        checkerror();
        return r_out;
    }

    /// <summary>
    /// validatePagespec(range) validates a page specification so far as is
    /// possible in the absence of the actual document. Result is true if valid.
    /// </summary>
    public static bool validatePagespec(string pagespec)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_validatePagespec(string pagespec);
        int res = cpdf_validatePagespec(pagespec);
        checkerror();
        return (res > 0);
    }

    /// <summary>
    /// stringOfPagespec(pdf, range) builds a page specification from a page
    /// range. For example, the range containing 1,2,3,6,7,8 in a document of 8
    /// pages might yield "1-3,6-end"
    /// </summary>
    public static string stringOfPagespec(Pdf pdf, List<int> r)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_stringOfPagespec(int pdf, int r);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(r);
        string s = Marshal.PtrToStringUTF8(cpdf_stringOfPagespec(pdf.pdf, rn));
        cpdf_deleteRange(rn);
        checkerror();
        return s;
    }

    /// <summary>
    /// blankRange() creates a range with no pages in.
    /// </summary>
    public static List<int> blankRange()
    {
        return new List<int>();
    }

    /// <summary>
    /// range(from, to) builds a range from one page to another inclusive. For
    /// example, cpdf_range(3,7) gives the range 3,4,5,6,7
    /// </summary>
    public static List<int> range(int f, int t)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_range(int f, int t);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = cpdf_range(f, t);
        List<int> l = list_of_range(rn);
        cpdf_deleteRange(rn);
        checkerror();
        return l;
    }

    /// <summary>
    /// all(pdf) is the range containing all the pages in a given document.
    /// </summary>
    public static List<int> all(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_all(int pdf);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = cpdf_all(pdf.pdf);
        List<int> r = list_of_range(rn);
        cpdf_deleteRange(rn);
        checkerror();
        return r;
    }

    /// <summary>
    /// even(range) makes a range which contains just the even pages of
    /// another range.
    /// </summary>
    public static List<int> even(List<int> r_in)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_even(int pdf);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int range_in = range_of_list(r_in);
        int rn = cpdf_even(range_in);
        List<int> r = list_of_range(rn);
        cpdf_deleteRange(rn);
        cpdf_deleteRange(range_in);
        checkerror();
        return r;
    }

    /// <summary>
    /// odd(range) makes a range which contains just the odd pages of another
    /// range.
    /// </summary>
    public static List<int> odd(List<int> r_in)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_odd(int pdf);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int range_in = range_of_list(r_in);
        int rn = cpdf_odd(range_in);
        List<int> r = list_of_range(rn);
        cpdf_deleteRange(rn);
        cpdf_deleteRange(range_in);
        checkerror();
        return r;
    }

    /// <summary>
    /// rangeUnion(a, b) makes the union of two ranges giving a range
    /// containing the pages in range a and range b.
    /// </summary>
    public static List<int> rangeUnion(List<int> a, List<int> b)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_rangeUnion(int a, int b);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int a_r = range_of_list(a);
        int b_r = range_of_list(b);
        int rn = cpdf_rangeUnion(a_r, b_r);
        List<int> r = list_of_range(rn);
        cpdf_deleteRange(a_r);
        cpdf_deleteRange(b_r);
        cpdf_deleteRange(rn);
        checkerror();
        return r;
    }

    /// <summary>
    /// difference(a, b) makes the difference of two ranges, giving a range
    /// containing all the pages in a except for those which are also in b.
    /// </summary>
    public static List<int> difference(List<int> a, List<int> b)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_difference(int a, int b);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int a_r = range_of_list(a);
        int b_r = range_of_list(b);
        int rn = cpdf_difference(a_r, b_r);
        List<int> r = list_of_range(rn);
        cpdf_deleteRange(a_r);
        cpdf_deleteRange(b_r);
        cpdf_deleteRange(rn);
        checkerror();
        return r;
    }

    /// <summary>
    /// removeDuplicates(range) deduplicates a range, making a new one.
    /// </summary>
    public static List<int> removeDuplicates(List<int> a)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_removeDuplicates(int r);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int a_r = range_of_list(a);
        int rn = cpdf_removeDuplicates(a_r);
        List<int> r = list_of_range(rn);
        cpdf_deleteRange(a_r);
        cpdf_deleteRange(rn);
        checkerror();
        return r;
    }

    /// <summary>
    /// rangeLength gives the number of pages in a range.
    /// </summary>
    public static int rangeLength(List<int> r)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_rangeLength(int r);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(r);
        int l = cpdf_rangeLength(rn);
        cpdf_deleteRange(rn);
        checkerror();
        return l;
    }

    /// </summary>
    /// rangeGet(range, n) gets the page number at position n in a range,
    /// where n runs from 0 to rangeLength - 1.
    /// </summary>
    public static int rangeGet(List<int> r, int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_rangeGet(int r, int n);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(r);
        int n_out = cpdf_rangeGet(rn, n);
        cpdf_deleteRange(rn);
        checkerror();
        return n_out;
    }

    /// <summary>
    /// rangeAdd(range, page) adds the page to a range, if it is not already
    /// there.
    /// </summary>
    public static List<int> rangeAdd(List<int> r, int page)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_rangeAdd(int r, int page);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(r);
        int r2 = cpdf_rangeAdd(rn, page);
        List<int> r_out = list_of_range(r2);
        cpdf_deleteRange(rn);
        cpdf_deleteRange(r2);
        checkerror();
        return r_out;
    }

    /// <summary>
    /// isInRange(range, page) returns true if the page is in the range,
    /// false otherwise.
    /// </summary>
    public static bool isInRange(List<int> r, int page)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_isInRange(int r, int page);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(r);
        int res = cpdf_isInRange(rn, page);
        cpdf_deleteRange(rn);
        checkerror();
        return (res > 0);
    }

    /// <summary>
    /// pages(pdf) returns the number of pages in a PDF.
    /// </summary>
    public static int pages(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_pages(int pdf);
        int res = cpdf_pages(pdf.pdf);
        checkerror();
        return res;
    }

    /// <summary>
    /// pagesFast(password, filename) returns the number of pages in a given
    /// PDF, with given user encryption password. It tries to do this as fast as
    /// possible, without loading the whole file.
    /// </summary>
    public static int pagesFast(string password, string filename)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_pagesFast(string password, string filename);
        int res = cpdf_pagesFast(password, filename);
        checkerror();
        return res;
    }

    /// <summary>
    /// toFile (pdf, filename, linearize, make_id) writes the file to a given
    /// filename. If linearize is true, it will be linearized if a linearizer is
    /// available. If make_id is true, it will be given a new ID.
    /// </summary>
    public static void toFile(Pdf pdf, string filename, bool linearize, bool make_id)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_toFile(int pdf, string filename, int linearize, int make_id);
        cpdf_toFile(pdf.pdf, filename, linearize ? 1 : 0, make_id ? 1 : 0);
        checkerror();
    }

    /// <summary>
    /// toFileExt (pdf, filename, linearize, make_id, preserve_objstm,
    /// generate_objstm, compress_objstm) writes the file to a given filename. If
    /// make_id is true, it will be given a new ID.  If preserve_objstm is true,
    /// existing object streams will be preserved. If generate_objstm is true,
    /// object streams will be generated even if not originally present. If
    /// compress_objstm is true, object streams will be compressed (what we
    /// usually want). WARNING: the pdf argument will be invalid after this call,
    /// and should be not be used again.
    /// </summary>
    public static void toFileExt(Pdf pdf, string filename, bool linearize, bool make_id, bool preserve_objstm, bool generate_objstm, bool compress_objstm)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_toFileExt(int pdf, string filename, int linearize, int make_id, int preserve_objstm, int generate_objstm, int compress_objstm);
        cpdf_toFileExt(pdf.pdf, filename, linearize ? 1 : 0, make_id ? 1 : 0, preserve_objstm ? 1 : 0, generate_objstm ? 1 : 0, compress_objstm ? 1 : 0);
        checkerror();
    }

    /// <summary>
    /// toFileMemory (pdf, linearize, make_id) writes a PDF file it
    /// and returns as an array of bytes.
    /// </summary>
    public static byte[] toMemory(Pdf pdf, bool linearize, bool makeid)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_toMemory(int pdf, int linearize, int makeid, ref int len);
        int len = 0;
        IntPtr data = cpdf_toMemory(pdf.pdf, linearize ? 1 : 0, makeid ? 1 : 0, ref len);
        var databytes = new byte[len];
        Marshal.Copy(data, databytes, 0, len);
        [DllImport("libcpdf.so")] static extern void cpdf_free(IntPtr ptr);
        cpdf_free(data);
        checkerror();
        return databytes;
    }

    /// <summary>
    /// isEncrypted(pdf) returns true if a documented is encrypted, false
    /// otherwise.
    /// </summary>
    public static bool isEncrypted(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_isEncrypted(int pdf);
        int res = cpdf_isEncrypted(pdf.pdf);
        checkerror();
        return (res > 0);
    }

    /// <summary>
    /// decryptPdf(pdf, userpw) attempts to decrypt a PDF using the given
    /// user password. An exception is raised if the decryption fails.
    /// </summary>
    public static void decryptPdf(Pdf pdf, string userpw)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_decryptPdf(int pdf, string userpw);
        cpdf_decryptPdf(pdf.pdf, userpw);
        checkerror();
    }

    /// <summary>
    /// decryptPdfOwner(pdf, ownerpw) attempts to decrypt a PDF using the
    /// given owner password. The error code is non-zero if the decryption fails.
    /// </summary>
    public static void decryptPdfOwner(Pdf pdf, string ownerpw)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_decryptPdfOwner(int pdf, string ownerpw);
        cpdf_decryptPdfOwner(pdf.pdf, ownerpw);
        checkerror();
    }

    /// <summary>
    /// toFileEncrypted(pdf, encryption_method, permissions,
    /// permission_length, owner_password, user password, linearize, makeid,
    /// filename) writes a file as encrypted.
    /// </summary>
    public static void toFileEncrypted(Pdf pdf, int encryption_method, List<int> permissions, string ownerpw, string userpw, bool linearize, bool makeid, string filename)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_toFileEncrypted(int pdf, int encryption_method, int[] permissions, int permission_length, string ownerpw, string userpw, int linearize, int makeid, string filename);
        cpdf_toFileEncrypted(pdf.pdf, encryption_method, permissions.ToArray(), permissions.Count, ownerpw, userpw, linearize ? 1 : 0, makeid ? 1 : 0, filename);
        checkerror();
    }

    /// <summary>
    /// toFileEncryptedExt(pdf, encryption_method, permissions,
    /// permission_length, owner_password, user_password, linearize, makeid,
    /// preserve_objstm, generate_objstm, compress_objstm, filename) WARNING: the
    /// pdf argument will be invalid after this call, and should not be used again.
    /// </summary>
    public static void toFileEncryptedExt(Pdf pdf, int encryption_method, List<int> permissions, string ownerpw, string userpw, bool linearize, bool makeid, bool preserve_objstm, bool generate_objstm, bool compress_objstm, string filename)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_toFileEncryptedExt(int pdf, int encryption_method, int[] permissions, int permission_length, string ownerpw, string userpw, int linearize, int makeid, int preserve_objstm, int generate_objstm, int compress_objstm, string filename);
        cpdf_toFileEncryptedExt(pdf.pdf, encryption_method, permissions.ToArray(), permissions.Count, ownerpw, userpw, linearize ? 1 : 0, makeid ? 1 : 0, preserve_objstm ? 1 : 0, generate_objstm ? 1 : 0, compress_objstm ? 1 : 0, filename);
        checkerror();
    }

    /// <summary>
    /// hasPermission(pdf, permission) returns true if the given permission
    /// (restriction) is present.
    /// </summary>
    public static bool hasPermission(Pdf pdf, int permission)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_hasPermission(int pdf, int permission);
        int res = cpdf_hasPermission(pdf.pdf, permission);
        checkerror();
        return (res > 0);
    }

    /// <summary>
    /// encryptionKind(pdf) return the encryption method currently in use on
    /// a document.
    /// </summary>
    public static int encryptionKind(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_encryptionKind(int pdf);
        int res = cpdf_encryptionKind(pdf.pdf);
        return res;
    }

    /* CHAPTER 2. Merging and Splitting */

    /// <summary>
    /// mergeSimple(pdfs, length) given a list of PDFs,
    /// merges the files into a new one, which is returned.
    /// </summary>
    public static Pdf mergeSimple(List<Pdf> pdfs)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_mergeSimple(int[] pdfs, int length);
        List<int> c_pdfs_lst = new List<int>();
        for (int x = 0; x < pdfs.Count; x++)
        {
            c_pdfs_lst.Add(pdfs[x].pdf);
        }
        int res = cpdf_mergeSimple(c_pdfs_lst.ToArray(), pdfs.Count);
        checkerror();
        return new Pdf(res);
    }

    /// <summary>
    /// merge(pdfs, retain_numbering, remove_duplicate_fonts) merges the
    /// PDFs. If retain_numbering is true page labels are not rewritten. If
    /// remove_duplicate_fonts is true, duplicate fonts are merged. This is useful
    /// when the source documents for merging originate from the same source.
    /// </summary>
    public static Pdf merge(List<Pdf> pdfs, bool retain_numbering, bool remove_duplicate_fonts)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_merge(int[] pdfs, int length, int retain_numbering, int remove_duplicate_fonts);
        List<int> c_pdfs_lst = new List<int>();
        for (int x = 0; x < pdfs.Count; x++)
        {
            c_pdfs_lst.Add(pdfs[x].pdf);
        }
        int res = cpdf_merge(c_pdfs_lst.ToArray(), pdfs.Count, retain_numbering ? 1 : 0, remove_duplicate_fonts ? 1 : 0);
        checkerror();
        return new Pdf(res);
    }

    /// <summary>
    /// mergeSame(pdfs, retain_numbering, remove_duplicate_fonts,
    /// ranges) is the same as merge, except that it has an additional
    /// argument - an array of page ranges. This is used to select the pages to
    /// pick from each PDF. This avoids duplication of information when multiple
    /// discrete parts of a source PDF are included.
    /// </summary>
    public static Pdf mergeSame(List<Pdf> pdfs, bool retain_numbering, bool remove_duplicate_fonts, List<List<int>> ranges)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_mergeSame(int[] pdfs, int length, int retain_numbering, int remove_duplicate_fonts, int[] ranges);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        List<int> c_pdfs_lst = new List<int>();
        for (int x = 0; x < pdfs.Count; x++)
        {
            c_pdfs_lst.Add(pdfs[x].pdf);
        }
        int[] c_pdfs = c_pdfs_lst.ToArray();
        List<int> int_ranges = ranges.ConvertAll(range_of_list);
        int[] c_ranges = int_ranges.ToArray();
        int result = cpdf_mergeSame(c_pdfs, pdfs.Count, retain_numbering ? 1 : 0, remove_duplicate_fonts ? 1 : 0, c_ranges);
        for (int x = 0; x < c_ranges.Length; x++) {
            cpdf_deleteRange(c_ranges[x]);
        }
        checkerror();
        return new Pdf(result);
    }

    /// <summary>
    /// selectPages(pdf, range) returns a new document which just those pages
    /// in the page range.
    /// </summary>
    public static Pdf selectPages(Pdf pdf, List<int> r)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_selectPages(int pdf, int r);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(r);
        int res = cpdf_selectPages(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
        return new Pdf(res);
    }

    /* CHAPTER 3. Pages */

    /// <summary>
    /// scalePages(pdf, range, x scale, y scale) scales the page dimensions
    /// and content by the given scale, about (0, 0). Other boxes (crop etc. are
    /// altered as appropriate)
    /// </summary>
    public static void scalePages(Pdf pdf, List<int> range, double sx, double sy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_scalePages(int pdf, int range, double sx, double sy);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_scalePages(pdf.pdf, rn, sx, sy);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// scaleToFit(pdf, range, width, height, scale) scales the content to fit
    /// new page dimensions (width x height) multiplied by scale (typically 1.0).
    /// Other boxes (crop etc. are altered as appropriate)
    /// </summary>
    public static void scaleToFit(Pdf pdf, List<int> range, double sx, double sy, double scale)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_scaleToFit(int pdf, int range, double sx, double sy, double scale);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_scaleToFit(pdf.pdf, rn, sx, sy, scale);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// scaleToFitPaper(pdf, range, papersize, scale) scales the page content
    /// to fit the given page size, possibly multiplied by scale (typically 1.0)
    /// </summary>
    public static void scaleToFitPaper(Pdf pdf, List<int> range, int pagesize, double scale)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_scaleToFitPaper(int pdf, int range, int pagesize, double scale);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_scaleToFitPaper(pdf.pdf, rn, pagesize, scale);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// scaleContents(pdf, range, position, scale) scales the contents of the
    /// pages in the range about the point given by the cpdf_position, by the
    /// scale given.
    /// </summary>
    public static void scaleContents(Pdf pdf, List<int> range, position position, double scale)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_scaleContents(int pdf, int range, position position, double scale);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_scaleContents(pdf.pdf, rn, position, scale);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// shiftContents(pdf, range, dx, dy) shifts the content of the pages in
    /// the range.
    /// </summary>
    public static void shiftContents(Pdf pdf, List<int> range, double dx, double dy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_shiftContents(int pdf, int range, double dx, double dy);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_shiftContents(pdf.pdf, rn, dx, dy);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// rotate(pdf, range, rotation) changes the viewing rotation to an
    /// absolute value. Appropriate rotations are 0, 90, 180, 270.
    /// </summary>
    public static void rotate(Pdf pdf, List<int> range, int rotation)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_rotate(int pdf, int range, int rotation);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_rotate(pdf.pdf, rn, rotation);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// rotateContents(pdf, range, angle) rotates the content about the
    /// centre of the page by the given number of degrees, in a clockwise
    /// direction.
    /// </summary>
    public static void rotateBy(Pdf pdf, List<int> range, int rotation)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_rotateBy(int pdf, int range, int rotation);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_rotateBy(pdf.pdf, rn, rotation);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// rotateContents(pdf, range, angle) rotates the content about the
    /// centre of the page by the given number of degrees, in a clockwise
    /// direction.
    /// </summary>
    public static void rotateContents(Pdf pdf, List<int> range, double angle)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_rotateContents(int pdf, int range, double angle);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_rotateContents(pdf.pdf, rn, angle);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// upright(pdf, range) changes the viewing rotation of the pages in the
    /// range, counter-rotating the dimensions and content such that there is no
    /// visual change.
    /// </summary>
    public static void upright(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_upright(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_upright(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// hFlip(pdf, range) flips horizontally the pages in the range.
    /// </summary>
    public static void hFlip(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_hFlip(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_hFlip(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// vFlip(pdf, range) flips vertically the pages in the range.
    /// </summary>
    public static void vFlip(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_vFlip(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_vFlip(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// crop(pdf, range, x, y, w, h) crops a page, replacing any existing
    /// crop box. The dimensions are in points.
    /// </summary>
    public static void crop(Pdf pdf, List<int> range, double x, double y, double w, double h)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_crop(int pdf, int range, double x, double y, double w, double h);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_crop(pdf.pdf, rn, x, y, w, h);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// removeCrop(pdf, range) removes any crop box from pages in the range.
    /// </summary>
    public static void removeCrop(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeCrop(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_removeCrop(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// removeTrim(pdf, range) removes any trim box from pages in the range.
    /// </summary>
    public static void removeTrim(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeTrim(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_removeTrim(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// removeArt(pdf, range) removes any art box from pages in the range.
    /// </summary>
    public static void removeArt(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeArt(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_removeArt(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// removeBleed(pdf, range) removes any bleed box from pages in the range.
    /// </summary>
    public static void removeBleed(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeBleed(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_removeBleed(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// trimMarks(pdf, range) adds trim marks to the given pages, if the
    /// trimbox exists.
    /// </summary>
    public static void trimMarks(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_trimMarks(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_trimMarks(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// showBoxes(pdf, range) shows the boxes on the given pages, for debug.
    /// </summary>
    public static void showBoxes(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_showBoxes(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_showBoxes(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// hardBox makes a given box a 'hard box' i.e clips it explicitly.
    /// </summary>
    public static void hardBox(Pdf pdf, List<int> range, string boxname)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_hardBox(int pdf, int range, string boxname);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_hardBox(pdf.pdf, rn, boxname);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /* CHAPTER 4. Encryption */
    /* Encryption covered under Chapter 1 in cpdflib. */

    /* CHAPTER 5. Compression */
 
    /// <summary>
    /// compress(pdf) compresses any uncompressed streams in the given PDF
    /// using the Flate algorithm.
    /// </summary>
    public static void compress(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_compress(int pdf);
        cpdf_compress(pdf.pdf);
        checkerror();
    }

    /// <summary>
    /// decompress(pdf) decompresses any streams in the given PDF, so long as
    /// the compression method is supported.
    /// </summary>
    public static void decompress(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_decompress(int pdf);
        cpdf_decompress(pdf.pdf);
        checkerror();
    }

    /// <summary>
    /// squeezeToMemory(pdf) squeezes a pdf in memory.
    /// </summary>
    public static void squeezeInMemory(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_squeezeInMemory(int pdf);
        cpdf_squeezeInMemory(pdf.pdf);
        checkerror();
    }

    /* CHAPTER 6. Bookmarks */


    /// <summary>
    /// startGetBookmarkInfo(pdf) starts the bookmark retrieval process for a
    /// given PDF.
    /// </summary>
    public static void startGetBookmarkInfo(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_startGetBookmarkInfo(int pdf);
        cpdf_startGetBookmarkInfo(pdf.pdf);
        checkerror();
    }

    /// <summary>
    /// numberBookmarks gets the number of bookmarks for the PDF given to
    /// startGetBookmarkInfo.
    /// </summary>
    public static int numberBookmarks()
    {
        [DllImport("libcpdf.so")] static extern int cpdf_numberBookmarks();
        int res = cpdf_numberBookmarks();
        checkerror();
        return res;
    }

    /// <summary>
    /// getBookmarkLevel(serial) get bookmark level for the given bookmark
    /// (0...(n - 1)).
    /// </summary>
    public static int getBookmarkLevel(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getBookmarkLevel(int n);
        int res = cpdf_getBookmarkLevel(n);
        checkerror();
        return res;
    }

    /// <summary>
    /// getBookmarkPage gets the bookmark target page for the given PDF
    /// (which must be the same as the PDF passed to startSetBookmarkInfo)
    /// and bookmark (0...(n - 1)).
    /// </summary>
    public static int getBookmarkPage(Pdf pdf, int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getBookmarkPage(int pdf, int n);
        int res = cpdf_getBookmarkPage(pdf.pdf, n);
        checkerror();
        return res;
    }

    /// <summary>
    /// getBookmarkText returns the text of bookmark (0...(n - 1)).
    /// </summary>
    public static string getBookmarkText(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getBookmarkText(int n);
        string res = Marshal.PtrToStringUTF8(cpdf_getBookmarkText(n));
        checkerror();
        return res;
    }

    /// <summary>
    /// getBookmarkOpenStatus(pdf) is true if the bookmark is open.
    /// </summary>
    public static bool getBookmarkOpenStatus(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getBookmarkOpenStatus(int n);
        int res = cpdf_getBookmarkOpenStatus(n);
        checkerror();
        return (res > 0);
    }
    
    /// <summary>
    /// endGetBookmarkInfo ends the bookmark retrieval process, cleaning up.
    /// </summary>
    public static void endGetBookmarkInfo()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endGetBookmarkInfo();
        cpdf_endGetBookmarkInfo();
        checkerror();
    }

    /// <summary>
    /// startGetBookmarkInfo(n) start the bookmark setting process for n
    /// bookmarks.
    /// </summary>
    public static void startSetBookmarkInfo(int nummarks)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_startSetBookmarkInfo(int nummarks);
        cpdf_startSetBookmarkInfo(nummarks);
        checkerror();
    }

    /// <summary>
    /// setBookmarkLevel(n, level) set bookmark level for the given bookmark
    /// (0...(n - 1)).
    /// </summary>
    public static void setBookmarkLevel(int n, int level)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBookmarkLevel(int n, int level);
        cpdf_setBookmarkLevel(n, level);
        checkerror();
    }

    /// <summary>
    /// setBookmarkPage(pdf, bookmark, targetpage) sets the bookmark target
    /// page for the given PDF (which must be the same as the PDF to be passed to
    /// endSetBookmarkInfo) and bookmark (0...(n - 1)).
    /// </summary>
    public static void setBookmarkPage(Pdf pdf, int n, int targetpage)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBookmarkPage(int pdf, int n, int targetpage);
        cpdf_setBookmarkPage(pdf.pdf, n, targetpage);
        checkerror();
    }

    /// <summary>
    /// setBookmarkText(n, text) sets the text of bookmark (0...(n - 1)).
    /// </summary>
    public static void setBookmarkOpenStatus(int n, bool status)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBookmarkOpenStatus(int n, int status);
        cpdf_setBookmarkOpenStatus(n, status ? 1 : 0);
        checkerror();
    }

    /// <summary>
    /// setBookmarkText(n, text) sets the text of bookmark (0...(n - 1)).
    /// </summary>
    public static void setBookmarkText(int n, string text)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBookmarkText(int n, string text);
        cpdf_setBookmarkText(n, text);
        checkerror();
    }

    /// <summary>
    /// endSetBookmarkInfo(pdf) end the bookmark setting process, writing the
    /// bookmarks to the given PDF.
    /// </summary>
    public static void endSetBookmarkInfo(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endSetBookmarkInfo(int pdf);
        cpdf_endSetBookmarkInfo(pdf.pdf);
        checkerror();
    }

    /// <summary>
    /// getBookmarksJSON(pdf) returns the bookmark data in JSON format.
    /// </summary>
    static public byte[] getBookmarksJSON(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getBookmarksJSON(int pdf, ref int len);
        int len = 0;
        IntPtr data = cpdf_getBookmarksJSON(pdf.pdf, ref len);
        var databytes = new byte[len];
        Marshal.Copy(data, databytes, 0, len);
        [DllImport("libcpdf.so")] static extern void cpdf_free(IntPtr ptr);
        cpdf_free(data);
        checkerror();
        return databytes;
    }

    /// <summary>
    /// setBookmarksJSON(pdf, data) sets the bookmarks from JSON bookmark data.
    /// </summary>
    public static void setBookmarksJSON(Pdf pdf, byte[] data)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBookmarksJSON(int pdf, byte[] data, int length);
        cpdf_setBookmarksJSON(pdf.pdf, data, data.Length);
        checkerror();
    }

    /// <summary>
    /// tableOfContents(pdf, font, fontsize, title, bookmark) typesets a table
    /// of contents from existing bookmarks and prepends it to the document. If
    /// bookmark is set, the table of contents gets its own bookmark.
    /// </summary>
    public static void tableOfContents(Pdf pdf, int font, double fontsize, string title, bool bookmark)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_tableOfContents(int pdf, int font, double fontsize, string title, int bookmark);
        cpdf_tableOfContents(pdf.pdf, font, fontsize, title, bookmark ? 1 : 0);
        checkerror();
    }

    /* CHAPTER 7. Presentations */
    /* Not included in the library version. */

    /* CHAPTER 8. Logos, Watermarks and Stamps */

    /// <summary>
    /// stampOn(stamp_pdf, pdf, range) stamps stamp_pdf on top of all the
    /// pages in the document which are in the range. The stamp is placed with its
    /// origin at the origin of the target document.
    /// </summary>
    public static void stampOn(Pdf stamp_pdf, Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_stampOn(int stamp_pdf, int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_stampOn(stamp_pdf.pdf, pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// stampUnder(stamp_pdf, pdf, range) stamps stamp_pdf under all the
    /// pages in the document which are in the range. The stamp is placed with its
    /// origin at the origin of the target document.
    /// </summary>
    public static void stampUnder(Pdf stamp_pdf, Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_stampUnder(int stamp_pdf, int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_stampUnder(stamp_pdf.pdf, pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// stampExtended(pdf, pdf2, range, isover, scale_stamp_to_fit, pos,
    /// relative_to_cropbox) is a stamping function with extra features. - isover
    /// true, pdf goes over pdf2, isover false, pdf goes under pdf2 -
    /// scale_stamp_to_fit scales the stamp to fit the page - pos gives the
    /// position to put the stamp - relative_to_cropbox: if true, pos is relative
    /// to cropbox not mediabox.
    /// </summary>
    public static void stampExtended(Pdf pdf, Pdf pdf2, List<int> range, bool isover, bool scale_stamp_to_fit, position position, bool relative_to_cropbox)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_stampExtended(int pdf, int pdf2, int range, int isover, int scale_stamp_to_fit, position position, int relative_to_cropbox);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_stampExtended(pdf.pdf, pdf2.pdf, rn, isover ? 1 : 0, scale_stamp_to_fit ? 1 : 0, position, relative_to_cropbox ? 1 : 0);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// combinePages(under, over) combines the PDFs page-by-page, putting
    /// each page of 'over' over each page of 'under'.
    /// </summary>
    public static Pdf combinePages(Pdf under, Pdf over)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_combinePages(int under, int over);
        int res = cpdf_combinePages(under.pdf, over.pdf);
        checkerror();
        return new Pdf(res);
    }

    public static void addText(bool metrics, Pdf pdf, List<int> range, string text, position position, double linespacing, int bates, int font, double fontsize, double r, double g, double b, bool underneath, bool relative_to_cropbox, bool outline, double opacity, int justification, bool midline, bool topline, string filename, double linewidth, bool embed_fonts)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_addText(int metrics, int pdf, int range, string text, position position, double linespacing, int bates, int font, double fontsize, double r, double g, double b, int underneath, int relative_to_cropbox, int outline, double opacity, int justification, int midline, int topline, string filename, double linewidth, int embed_fonts);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_addText(metrics ? 1 : 0, pdf.pdf, rn, text, position, linespacing, bates, font, fontsize, r, g, b, underneath ? 1 : 0, relative_to_cropbox ? 1 : 0, outline ? 1 : 0, opacity, justification, midline ? 1 : 0, topline ? 1 : 0, filename, linewidth, embed_fonts ? 1 : 0);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void addTextSimple(Pdf pdf, List<int> range, string text, position position, int font, double fontsize)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_addTextSimple(int pdf, int range, string text, position position, int font, double fontsize);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_addTextSimple(pdf.pdf, rn, text, position, font, fontsize);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// removeText(pdf, range) will remove any text added by libcpdf from the
    /// given pages.
    /// </summary>
    public static void removeText(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeText(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_removeText(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// Return the width of a given string in the given font in thousandths of a
    /// point.
    /// </summary>
    public static int textWidth(int font, string text)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_textWidth(int font, string text);
        int res = cpdf_textWidth(font, text);
        checkerror();
        return res;
    }

    /// <summary>
    /// addContent(content, before, pdf, range) adds page content before (if
    /// true) or after (if false) the existing content to pages in the given range
    /// in the given PDF.
    /// </summary>
    public static void addContent(string content, bool before, Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_addContent(string content, int before, int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_addContent(content, before ? 1 : 0, pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// stampAsXObject(pdf, range, stamp_pdf) stamps stamp_pdf onto the pages
    /// in the given range in pdf as a shared Form XObject. The name of the
    /// newly-created XObject is returned.
    /// </summary>
    public static string stampAsXObject(Pdf pdf, List<int> range, Pdf stamp_pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_stampAsXObject(int pdf, int range, int stamp_pdf);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        string s = Marshal.PtrToStringUTF8(cpdf_stampAsXObject(pdf.pdf, rn, stamp_pdf.pdf));
        cpdf_deleteRange(rn);
        checkerror();
        return s;
    }

    /* CHAPTER 9. Multipage facilities */

    /// <summary>
    /// impose(pdf, x, y, fit, columns, rtl, btt, center, margin, spacing,
    /// linewidth) imposes a PDF. There are two modes: imposing x * y, or imposing
    /// to fit a page of size x * y. This is controlled by fit. Columns imposes by
    /// columns rather than rows. rtl is right-to-left, btt bottom-to-top. Center is
    /// unused for now. Margin is the margin around the output, spacing the spacing
    /// between imposed inputs.
    /// </summary>
    static public void impose(Pdf pdf, double x, double y, bool fit, bool columns, bool rtl, bool btt, bool center, double margin, double spacing, double linewidth)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_impose(int pdf, double x, double y, int fit, int columns, int rtl, int btt, int center, double margin, double spacing, double linewidth);
        cpdf_impose(pdf.pdf, x, y, fit ? 1 : 0, columns ? 1 : 0, rtl ? 1 : 0, btt ? 1 : 0, center ? 1 : 0, margin, spacing, linewidth);
        checkerror();
    }

    /// <summary>
    /// Impose a document two up. twoUp does so by retaining the existing
    /// page size, scaling pages down. twoUpStack does so by doubling the
    /// page size, to fit two pages on one.
    /// </summary>
    static public void twoUp(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_twoUp(int pdf);
        cpdf_twoUp(pdf.pdf);
        checkerror();
    }

    /// <summary>
    /// Impose a document two up. twoUp does so by retaining the existing
    /// page size, scaling pages down. twoUpStack does so by doubling the
    /// page size, to fit two pages on one.
    /// </summary>
    static public void twoUpStack(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_twoUpStack(int pdf);
        cpdf_twoUpStack(pdf.pdf);
        checkerror();
    }

    /// <summary>
    /// padBefore(pdf, range) adds a blank page before each page in the given
    /// range.
    /// </summary>
    static public void padBefore(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_padBefore(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_padBefore(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// padAfter(pdf, range) adds a blank page after each page in the given
    /// range.
    /// </summary>
    static public void padAfter(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_padAfter(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_padAfter(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// pageEvery(pdf, n) adds a blank page after every n pages.
    /// </summary>
    static public void padEvery(Pdf pdf, int n)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_padEvery(int pdf, int n);
        cpdf_padEvery(pdf.pdf, n);
        checkerror();
    }

    /// <summary>
    /// padMultiple(pdf, n) adds pages at the end to pad the file to a
    /// multiple of n pages in length.
    /// </summary>
    static public void padMultiple(Pdf pdf, int n)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_padMultiple(int pdf, int n);
        cpdf_padMultiple(pdf.pdf, n);
        checkerror();
    }

    /// <summary>
    /// padMultiple(pdf, n) adds pages at the beginning to pad the file to a
    /// multiple of n pages in length.
    /// </summary>
    static public void padMultipleBefore(Pdf pdf, int n)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_padMultipleBefore(int pdf, int n);
        cpdf_padMultipleBefore(pdf.pdf, n);
        checkerror();
    }

    /* CHAPTER 10. Annotations */

    /// <summary>
    /// Return the annotations from a PDF in JSON format
    /// </summary>
    static public byte[] annotationsJSON(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_annotationsJSON(int pdf, ref int len);
        int len = 0;
        IntPtr data = cpdf_annotationsJSON(pdf.pdf, ref len);
        var databytes = new byte[len];
        Marshal.Copy(data, databytes, 0, len);
        [DllImport("libcpdf.so")] static extern void cpdf_free(IntPtr ptr);
        cpdf_free(data);
        checkerror();
        return databytes;
    }

    /* CHAPTER 11. Document Information and Metadata */

    /// <summary>
    /// isLinearized(filename) finds out if a document is linearized as
    /// quickly as possible without loading it.
    /// </summary>
    public static bool isLinearized(string filename)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_isLinearized(string filename);
        int res = cpdf_isLinearized(filename);
        checkerror();
        return (res > 0);
    }

    /// <summary>
    /// getVersion(pdf) returns the minor version number of a document.
    /// </summary>
    public static int getVersion(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getVersion(int pdf);
        int res = cpdf_getVersion(pdf.pdf);
        checkerror();
        return res;
    }

    /// <summary>
    /// getMajorVersion(pdf) returns the minor version number of a document.
    /// </summary>
    public static int getMajorVersion(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getMajorVersion(int pdf);
        int res = cpdf_getMajorVersion(pdf.pdf);
        checkerror();
        return res;
    }

    /// <summary>
    /// getTitle(pdf) returns the title of a document.
    /// </summary>
    public static string getTitle(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getTitle(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getTitle(pdf.pdf));
        checkerror();
        return res;
    }

    /// <summary>
    /// getAuthor(pdf) returns the author of a document.
    /// </summary>
    public static string getAuthor(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getAuthor(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getAuthor(pdf.pdf));
        checkerror();
        return res;
    }

    /// <summary>
    /// getSubject(pdf) returns the subject of a document.
    /// </summary>
    public static string getSubject(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getSubject(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getSubject(pdf.pdf));
        checkerror();
        return res;
    }

    /// <summary>
    /// getKeywords(pdf) returns the keywords of a document.
    /// </summary>
    public static string getKeywords(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getKeywords(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getKeywords(pdf.pdf));
        checkerror();
        return res;
    }

    /// <summary>
    /// getCreator(pdf) returns the creator of a document.
    /// </summary>
    public static string getCreator(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getCreator(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getCreator(pdf.pdf));
        checkerror();
        return res;
    }

    /// <summary>
    /// getProducer(pdf) returns the producer of a document.
    /// </summary>
    public static string getProducer(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getProducer(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getProducer(pdf.pdf));
        checkerror();
        return res;
    }

    /// <summary>
    /// getCreationDate(pdf) returns the creation date of a document.
    /// </summary>
    public static string getCreationDate(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getCreationDate(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getCreationDate(pdf.pdf));
        checkerror();
        return res;
    }

    /// <summary>
    /// getModificationDate(pdf) returns the modification date of a document.
    /// </summary>
    public static string getModificationDate(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getModificationDate(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getModificationDate(pdf.pdf));
        checkerror();
        return res;
    }

    /// <summary>
    /// getTitleXMP(pdf) returns the XMP title of a document.
    /// </summary>
    public static string getTitleXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getTitleXMP(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getTitleXMP(pdf.pdf));
        checkerror();
        return res;
    }

    /// <summary>
    /// getAuthorXMP(pdf) returns the XMP author of a document.
    /// </summary>
    public static string getAuthorXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getAuthorXMP(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getAuthorXMP(pdf.pdf));
        checkerror();
        return res;
    }

    /// <summary>
    /// getSubjectXMP(pdf) returns the XMP subject of a document.
    /// </summary>
    public static string getSubjectXMP (Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getSubjectXMP(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getSubjectXMP(pdf.pdf));
        checkerror();
        return res;
    }

    /// <summary>
    /// getKeywordsXMP(pdf) returns the XMP keywords of a document.
    /// </summary>
    public static string getKeywordsXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getKeywordsXMP(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getKeywordsXMP(pdf.pdf));
        checkerror();
        return res;
    }

    /// <summary>
    /// getCreatorXMP(pdf) returns the XMP creator of a document.
    /// </summary>
    public static string getCreatorXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getCreatorXMP(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getCreatorXMP(pdf.pdf));
        checkerror();
        return res;
    }

    /// <summary>
    /// getProducerXMP(pdf) returns the XMP producer of a document.
    /// </summary>
    public static string getProducerXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getProducerXMP(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getProducerXMP(pdf.pdf));
        checkerror();
        return res;
    }

    /// <summary>
    /// getCreationDateXMP(pdf) returns the XMP creation date of a document.
    /// </summary>
    public static string getCreationDateXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getCreationDateXMP(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getCreationDateXMP(pdf.pdf));
        checkerror();
        return res;
    }

    /// <summary>
    /// getModificationDateXMP(pdf) returns the XMP modification date of a document.
    /// </summary>
    public static string getModificationDateXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getModificationDateXMP(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getModificationDateXMP(pdf.pdf));
        checkerror();
        return res;
    }

    /// <summary>
    /// setTitle(pdf) sets the title of a document.
    /// </summary>
    public static void setTitle(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setTitle(int pdf, string s);
        cpdf_setTitle(pdf.pdf, s);
        checkerror();
    }

    /// <summary>
    /// setAuthor(pdf) sets the author of a document.
    /// </summary>
    public static void setAuthor(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setAuthor(int pdf, string s);
        cpdf_setAuthor(pdf.pdf, s);
        checkerror();
    }

    /// <summary>
    /// setSubject(pdf) sets the subject of a document.
    /// </summary>
    public static void setSubject(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setSubject(int pdf, string s);
        cpdf_setSubject(pdf.pdf, s);
        checkerror();
    }

    /// <summary>
    /// setKeywords(pdf) sets the keywords of a document.
    /// </summary>
    public static void setKeywords(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setKeywords(int pdf, string s);
        cpdf_setKeywords(pdf.pdf, s);
        checkerror();
    }

    /// <summary>
    /// setCreator(pdf) sets the creator of a document.
    /// </summary>
    public static void setCreator(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setCreator(int pdf, string s);
        cpdf_setCreator(pdf.pdf, s);
        checkerror();
    }

    /// <summary>
    /// setProducer(pdf) sets the producer of a document.
    /// </summary>
    public static void setProducer(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setProducer(int pdf, string s);
        cpdf_setProducer(pdf.pdf, s);
        checkerror();
    }

    /// <summary>
    /// setCreationDate(pdf) sets the creation date of a document.
    /// </summary>
    public static void setCreationDate(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setCreationDate(int pdf, string s);
        cpdf_setCreationDate(pdf.pdf, s);
        checkerror();
    }

    /// <summary>
    /// setModificationDate(pdf) sets the modification date of a document.
    /// </summary>
    public static void setModificationDate(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setModificationDate(int pdf, string s);
        cpdf_setModificationDate(pdf.pdf, s);
        checkerror();
    }

    /// <summary>
    /// setTitleXMP(pdf) sets the XMP title of a document.
    /// </summary>
    public static void setTitleXMP(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setTitleXMP(int pdf, string s);
        cpdf_setTitleXMP(pdf.pdf, s);
        checkerror();
    }

    /// <summary>
    /// setAuthorXMP(pdf) sets the XMP author of a document.
    /// </summary>
    public static void setAuthorXMP(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setAuthorXMP(int pdf, string s);
        cpdf_setAuthorXMP(pdf.pdf, s);
        checkerror();
    }

    /// <summary>
    /// setSubjectXMP(pdf) sets the XMP subject of a document.
    /// </summary>
    public static void setSubjectXMP(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setSubjectXMP(int pdf, string s);
        cpdf_setSubjectXMP(pdf.pdf, s);
        checkerror();
    }

    /// <summary>
    /// setKeywordsXMP(pdf) sets the XMP keywords of a document.
    /// </summary>
    public static void setKeywordsXMP(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setKeywordsXMP(int pdf, string s);
        cpdf_setKeywordsXMP(pdf.pdf, s);
        checkerror();
    }

    /// <summary>
    /// setCreatorXMP(pdf) sets the XMP creator of a document.
    /// </summary>
    public static void setCreatorXMP(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setCreatorXMP(int pdf, string s);
        cpdf_setCreatorXMP(pdf.pdf, s);
        checkerror();
    }

    /// <summary>
    /// setProducerXMP(pdf) sets the XMP producer of a document.
    /// </summary>
    public static void setProducerXMP(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setProducerXMP(int pdf, string s);
        cpdf_setProducerXMP(pdf.pdf, s);
        checkerror();
    }

    /// <summary>
    /// setCreationDateXMP(pdf) sets the XMP creation date of a document.
    /// </summary>
    public static void setCreationDateXMP(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setCreationDateXMP(int pdf, string s);
        cpdf_setCreationDateXMP(pdf.pdf, s);
        checkerror();
    }

    /// <summary>
    /// setModificationDateXMP(pdf) sets the XMP modification date of a document.
    /// </summary>
    public static void setModificationDateXMP(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setModificationDateXMP(int pdf, string s);
        cpdf_setModificationDateXMP(pdf.pdf, s);
        checkerror();
    }

    /// <summary>
    /// getDateComponents(datestring, year, month, day, hour, minute, second,
    /// hour_offset, minute_offset) returns the components from a PDF date string.
    /// </summary>
    public static void getDateComponents(string datestring, ref int year, ref int month, ref int day, ref int hour, ref int minute, ref int second, ref int hour_offset, ref int minute_offset)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getDateComponents(string datestring, ref int year, ref int month, ref int day, ref int hour, ref int minute, ref int second, ref int hour_offset, ref int minute_offset);
        cpdf_getDateComponents(datestring, ref year, ref month, ref day, ref hour, ref minute, ref second, ref hour_offset, ref minute_offset);
        checkerror();
    }

    /// <summary>
    /// dateStringOfComponents(year, month, day, hour, minute, second,
    /// hour_offset, minute_offset) builds a PDF date string from individual
    /// components.
    /// </summary>
    public static string dateStringOfComponents(int y, int m, int d, int h, int min, int sec, int hour_offset, int minute_offset)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_dateStringOfComponents(int y, int m, int d, int h, int min, int sec, int hour_offset, int minute_offset);
        string res = Marshal.PtrToStringUTF8(cpdf_dateStringOfComponents(y, m, d, h, min, sec, hour_offset, minute_offset));
        checkerror();
        return res;
    }

    /// <summary>
    /// getPageRotation(pdf, pagenumber) gets the viewing rotation for a
    /// given page.
    /// </summary>
    public static int getPageRotation(Pdf pdf, int pagenumber)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getPageRotation(int pdf, int pagenumber);
        int res = cpdf_getPageRotation(pdf.pdf, pagenumber);
        checkerror();
        return res;
    }

    /// <summary>
    /// hasBox(pdf, pagenumber, boxname) returns true, if that page has the
    /// given box. E.g "/CropBox".
    /// </summary>
    public static bool hasBox(Pdf pdf, int pagenumber, string boxname)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_hasBox(int pdf, int pagenumber, string boxname);
        int res = cpdf_hasBox(pdf.pdf, pagenumber, boxname);
        checkerror();
        return (res > 0);
    }

    /// <summary>
    /// These functions get a box given the document, page number, min x, max x,
    /// min y, max y in points. Only succeeds if such a box exists, as checked by
    /// hasBox.
    /// </summary>
    public static void getMediaBox(Pdf pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getMediaBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy);
        cpdf_getMediaBox(pdf.pdf, pagenumber, ref minx, ref maxx, ref miny, ref maxy);
        checkerror();
    }

    /// <summary>
    /// These functions get a box given the document, page number, min x, max x,
    /// min y, max y in points. Only succeeds if such a box exists, as checked by
    /// hasBox.
    /// </summary>
    public static void getCropBox(Pdf pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getCropBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy);
        cpdf_getCropBox(pdf.pdf, pagenumber, ref minx, ref maxx, ref miny, ref maxy);
        checkerror();
    }

    /// <summary>
    /// These functions get a box given the document, page number, min x, max x,
    /// min y, max y in points. Only succeeds if such a box exists, as checked by
    /// hasBox.
    /// </summary>
    public static void getTrimBox(Pdf pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getTrimBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy);
        cpdf_getTrimBox(pdf.pdf, pagenumber, ref minx, ref maxx, ref miny, ref maxy);
        checkerror();
    }

    /// <summary>
    /// These functions get a box given the document, page number, min x, max x,
    /// min y, max y in points. Only succeeds if such a box exists, as checked by
    /// hasBox.
    /// </summary>
    public static void getArtBox(Pdf pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getArtBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy);
        cpdf_getArtBox(pdf.pdf, pagenumber, ref minx, ref maxx, ref miny, ref maxy);
        checkerror();
    }

    /// <summary>
    /// These functions get a box given the document, page number, min x, max x,
    /// min y, max y in points. Only succeeds if such a box exists, as checked by
    /// hasBox.
    /// </summary>
    public static void getBleedBox(Pdf pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getBleedBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy);
        cpdf_getBleedBox(pdf.pdf, pagenumber, ref minx, ref maxx, ref miny, ref maxy);
        checkerror();
    }

    /// <summary>
    /// These functions set a box given the document, page range, min x, max x,
    /// min y, max y in points.
    /// </summary>
    public static void setMediabox(Pdf pdf, List<int> range, double minx, double maxx, double miny, double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setMediabox(int pdf, int r, double minx, double maxx, double miny, double maxy);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_setMediabox(pdf.pdf, rn, minx, maxx, miny, maxy);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// These functions set a box given the document, page range, min x, max x,
    /// min y, max y in points.
    /// </summary>
    public static void setCropBox(Pdf pdf, List<int> range, double minx, double maxx, double miny, double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setCropBox(int pdf, int r, double minx, double maxx, double miny, double maxy);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_setCropBox(pdf.pdf, rn, minx, maxx, miny, maxy);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// These functions set a box given the document, page range, min x, max x,
    /// min y, max y in points.
    /// </summary>
    public static void setTrimBox(Pdf pdf, List<int> range, double minx, double maxx, double miny, double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setTrimBox(int pdf, int r, double minx, double maxx, double miny, double maxy);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_setTrimBox(pdf.pdf, rn, minx, maxx, miny, maxy);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// These functions set a box given the document, page range, min x, max x,
    /// min y, max y in points.
    /// </summary>
    public static void setArtBox(Pdf pdf, List<int> range, double minx, double maxx, double miny, double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setArtBox(int pdf, int r, double minx, double maxx, double miny, double maxy);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_setArtBox(pdf.pdf, rn, minx, maxx, miny, maxy);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// These functions set a box given the document, page range, min x, max x,
    /// min y, max y in points.
    /// </summary>
    public static void setBleedBox(Pdf pdf, List<int> range, double minx, double maxx, double miny, double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBleedBox(int pdf, int r, double minx, double maxx, double miny, double maxy);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_setBleedBox(pdf.pdf, rn, minx, maxx, miny, maxy);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// markTrapped(pdf) marks a document as trapped.
    /// </summary>
    public static void markTrapped(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_markTrapped(int pdf);
        cpdf_markTrapped(pdf.pdf);
        checkerror();
    }

    /// <summary>
    /// markUntrapped(pdf) marks a document as untrapped.
    /// </summary>
    public static void markUntrapped(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_markUntrapped(int pdf);
        cpdf_markUntrapped(pdf.pdf);
        checkerror();
    }

    /// <summary>
    /// markTrappedXMP(pdf) marks a document as trapped in XMP metadata.
    /// </summary>
    public static void markTrappedXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_markTrappedXMP(int pdf);
        cpdf_markTrappedXMP(pdf.pdf);
        checkerror();
    }

    /// <summary>
    /// markUntrappedXMP(pdf) marks a document as untrapped in XMP metadata.
    /// </summary>
    public static void markUntrappedXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_markUntrappedXMP(int pdf);
        cpdf_markUntrappedXMP(pdf.pdf);
        checkerror();
    }

    /// <summary>
    /// setPageLayout(pdf, layout) sets the page layout for a document.
    /// </summary>
    public static void setPageLayout(Pdf pdf, int layout)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setPageLayout(int pdf, int layout);
        cpdf_setPageLayout(pdf.pdf, layout);
        checkerror();
    }

    /// <summary>
    /// setPageMode(pdf, mode) sets the page mode for a document.
    /// </summary>
    public static void setPageMode(Pdf pdf, int mode)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setPageMode(int pdf, int mode);
        cpdf_setPageMode(pdf.pdf, mode);
        checkerror();
    }

    /// <summary>
    /// hideToolbar(pdf, flag) sets the hide toolbar flag.
    /// </summary>
    public static void hideToolbar(Pdf pdf, bool flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_hideToolbar(int pdf, int flag);
        cpdf_hideToolbar(pdf.pdf, flag ? 1 : 0);
        checkerror();
    }

    /// <summary>
    /// hideMenubar(pdf, flag) sets the hide menubar flag.
    /// </summary>
    public static void hideMenubar(Pdf pdf, bool flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_hideMenubar(int pdf, int flag);
        cpdf_hideMenubar(pdf.pdf, flag ? 1 : 0);
        checkerror();
    }

    /// <summary>
    /// hideWindowUi(pdf, flag) sets the hide window UI flag.
    /// </summary>
    public static void hideWindowUi(Pdf pdf, bool flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_hideWindowUi(int pdf, int flag);
        cpdf_hideWindowUi(pdf.pdf, flag ? 1 : 0);
        checkerror();
    }

    /// <summary>
    /// fitWindow(pdf, flag) sets the fit window flag.
    /// </summary>
    public static void fitWindow(Pdf pdf, bool flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_fitWindow(int pdf, int flag);
        cpdf_fitWindow(pdf.pdf, flag ? 1 : 0);
        checkerror();
    }

    /// <summary>
    /// centerWindow(pdf, flag) sets the center window flag.
    /// </summary>
    public static void centerWindow(Pdf pdf, bool flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_centerWindow(int pdf, int flag);
        cpdf_centerWindow(pdf.pdf, flag ? 1 : 0);
        checkerror();
    }

    /// <summary>
    /// displayDocTitle(pdf, flag) sets the display doc title flag.
    /// </summary>
    public static void displayDocTitle(Pdf pdf, bool flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_displayDocTitle(int pdf, int flag);
        cpdf_displayDocTitle(pdf.pdf, flag ? 1 : 0);
        checkerror();
    }

    /// <summary>
    /// openAtPage(pdf, fit, pagenumber) sets the PDF to open, possibly with
    /// zoom-to-fit, at the given page number.
    /// </summary>
    public static void openAtPage(Pdf pdf, bool fit, int pagenumber)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_openAtPage(int pdf, int fit, int pagenumber);
        cpdf_openAtPage(pdf.pdf, fit ? 1 : 0, pagenumber);
        checkerror();
    }

    /// <summary>
    /// setMetadataFromFile(pdf, filename) set the XMP metadata of a
    /// document, given a file name.
    /// </summary>
    public static void setMetadataFromFile(Pdf pdf, string filename)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setMetadataFromFile(int pdf, string filename);
        cpdf_setMetadataFromFile(pdf.pdf, filename);
        checkerror();
    }

    /// <summary>
    /// setMetadataFromByteArray(pdf, data) set the XMP metadata from
    /// an array of bytes.
    /// </summary>
    public static void setMetadataFromByteArray(Pdf pdf, byte[] data)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setMetadataFromByteArray(int pdf, byte[] data, int length);
        cpdf_setMetadataFromByteArray(pdf.pdf, data, data.Length);
        checkerror();
    }

    /// <summary>
    /// removeMetadata(pdf) removes the XMP metadata from a document.
    /// </summary>
    public static void removeMetadata(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeMetadata(int pdf);
        cpdf_removeMetadata(pdf.pdf);
        checkerror();
    }

    /// <summary>
    /// createMetadata(pdf) builds fresh metadata as best it can from
    /// existing metadata in the document.
    /// </summary>
    public static byte[] getMetadata(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getMetadata(int pdf, ref int len);
        int len = 0;
        IntPtr data = cpdf_getMetadata(pdf.pdf, ref len);
        var databytes = new byte[len];
        Marshal.Copy(data, databytes, 0, len);
        [DllImport("libcpdf.so")] static extern void cpdf_free(IntPtr ptr);
        cpdf_free(data);
        checkerror();
        return databytes;
    }

    /// <summary>
    /// createMetadata(pdf) builds fresh metadata as best it can from
    /// existing metadata in the document.
    /// </summary>
    public static void createMetadata(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_createMetadata(int pdf);
        cpdf_createMetadata(pdf.pdf);
        checkerror();
    }

    /// <summary>
    /// setMetadataDate(pdf, date) sets the metadata date for a PDF. The date
    /// is given in PDF date format -- cpdf will convert it to XMP format. The
    /// date 'now' means now.
    /// </summary>
    public static void setMetadataDate(Pdf pdf, string date)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setMetadataDate(int pdf, string date);
        cpdf_setMetadataDate(pdf.pdf, date);
        checkerror();
    }

    /// <summary>
    /// Add page labels. addPageLabels(pdf, style, prefix, offset, range, progress).
    /// The prefix is prefix text for each label. The range is the page range the
    /// labels apply to. Offset can be used to shift the numbering up or down.
    /// </summary>
    public static void addPageLabels(Pdf pdf, int style, string prefix, int offset, List<int> range, bool progress)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_addPageLabels(int pdf, int style, string prefix, int offset, int range, int progress);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_addPageLabels(pdf.pdf, style, prefix, offset, rn, progress ? 1 : 0);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /// <summary>
    /// removePageLabels(pdf) removes the page labels from the document.
    /// </summary>
    public static void removePageLabels(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removePageLabels(int pdf);
        cpdf_removePageLabels(pdf.pdf);
        checkerror();
    }

    /// <summary>
    /// getPageLabelStringForPage(pdf, page number) calculates the full label
    /// string for a given page, and returns it.
    /// </summary>
    public static string getPageLabelStringForPage(Pdf pdf, int pagenumber)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getPageLabelStringForPage(int pdf, int pagenumber);
        string res = Marshal.PtrToStringUTF8(cpdf_getPageLabelStringForPage(pdf.pdf, pagenumber));
        checkerror();
        return res;
    }

    /// <summary>
    /// Get page label data. Call cpdf_startGetPageLabels to find out how many
    /// there are, then use these serial numbers to get the style, prefix, offset
    /// and start value (note not a range). Call cpdf_endGetPageLabels to clean up.
    ///
    /// For example, a document might have five pages of introduction with roman
    /// numerals, followed by the rest of the pages in decimal arabic, numbered from
    /// one:
    ///
    /// labelstyle = LowercaseRoman
    /// labelprefix = ""
    /// startpage = 1
    /// startvalue = 1
    ///
    /// labelstyle = DecimalArabic
    /// labelprefix = ""
    /// startpage = 6
    /// startvalue = 1
    /// </summary>
    public static int startGetPageLabels(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_startGetPageLabels(int pdf);
        int res = cpdf_startGetPageLabels(pdf.pdf);
        checkerror();
        return res;
    }

    /// <summary>
    /// Get page label data. Call cpdf_startGetPageLabels to find out how many
    /// there are, then use these serial numbers to get the style, prefix, offset
    /// and start value (note not a range). Call cpdf_endGetPageLabels to clean up.
    ///
    /// For example, a document might have five pages of introduction with roman
    /// numerals, followed by the rest of the pages in decimal arabic, numbered from
    /// one:
    ///
    /// labelstyle = LowercaseRoman
    /// labelprefix = ""
    /// startpage = 1
    /// startvalue = 1
    ///
    /// labelstyle = DecimalArabic
    /// labelprefix = ""
    /// startpage = 6
    /// startvalue = 1
    /// </summary>
    public static int getPageLabelStyle(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getPageLabelStyle(int n);
        int res = cpdf_getPageLabelStyle(n);
        checkerror();
        return res;
    }

    /// <summary>
    /// Get page label data. Call cpdf_startGetPageLabels to find out how many
    /// there are, then use these serial numbers to get the style, prefix, offset
    /// and start value (note not a range). Call cpdf_endGetPageLabels to clean up.
    ///
    /// For example, a document might have five pages of introduction with roman
    /// numerals, followed by the rest of the pages in decimal arabic, numbered from
    /// one:
    ///
    /// labelstyle = LowercaseRoman
    /// labelprefix = ""
    /// startpage = 1
    /// startvalue = 1
    ///
    /// labelstyle = DecimalArabic
    /// labelprefix = ""
    /// startpage = 6
    /// startvalue = 1
    /// </summary>
    public static string getPageLabelPrefix(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getPageLabelPrefix(int n);
        string res = Marshal.PtrToStringUTF8(cpdf_getPageLabelPrefix(n));
        checkerror();
        return res;
    }

    /// <summary>
    /// Get page label data. Call cpdf_startGetPageLabels to find out how many
    /// there are, then use these serial numbers to get the style, prefix, offset
    /// and start value (note not a range). Call cpdf_endGetPageLabels to clean up.
    ///
    /// For example, a document might have five pages of introduction with roman
    /// numerals, followed by the rest of the pages in decimal arabic, numbered from
    /// one:
    ///
    /// labelstyle = LowercaseRoman
    /// labelprefix = ""
    /// startpage = 1
    /// startvalue = 1
    ///
    /// labelstyle = DecimalArabic
    /// labelprefix = ""
    /// startpage = 6
    /// startvalue = 1
    /// </summary>
    public static int getPageLabelOffset(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getPageLabelOffset(int n);
        int res = cpdf_getPageLabelOffset(n);
        checkerror();
        return res;
    }

    /// <summary>
    /// Get page label data. Call cpdf_startGetPageLabels to find out how many
    /// there are, then use these serial numbers to get the style, prefix, offset
    /// and start value (note not a range). Call cpdf_endGetPageLabels to clean up.
    ///
    /// For example, a document might have five pages of introduction with roman
    /// numerals, followed by the rest of the pages in decimal arabic, numbered from
    /// one:
    ///
    /// labelstyle = LowercaseRoman
    /// labelprefix = ""
    /// startpage = 1
    /// startvalue = 1
    ///
    /// labelstyle = DecimalArabic
    /// labelprefix = ""
    /// startpage = 6
    /// startvalue = 1
    /// </summary>
    public static int getPageLabelRange(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getPageLabelRange(int n);
        int res = cpdf_getPageLabelRange(n);
        checkerror();
        return res;
    }

    /// <summary>
    /// Get page label data. Call cpdf_startGetPageLabels to find out how many
    /// there are, then use these serial numbers to get the style, prefix, offset
    /// and start value (note not a range). Call cpdf_endGetPageLabels to clean up.
    ///
    /// For example, a document might have five pages of introduction with roman
    /// numerals, followed by the rest of the pages in decimal arabic, numbered from
    /// one:
    ///
    /// labelstyle = LowercaseRoman
    /// labelprefix = ""
    /// startpage = 1
    /// startvalue = 1
    ///
    /// labelstyle = DecimalArabic
    /// labelprefix = ""
    /// startpage = 6
    /// startvalue = 1
    /// </summary>
    public static void endGetPageLabels()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endGetPageLabels();
        cpdf_endGetPageLabels();
        checkerror();
    }

    /* CHAPTER 12. File Attachments */

    public static void attachFile(string filename, Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_attachFile(string filename, int pdf);
        cpdf_attachFile(filename, pdf.pdf);
        checkerror();
    }

    public static void attachFileToPage(string filename, Pdf pdf, int pagenumber)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_attachFileToPage(string filename, int pdf, int pagenumber);
        cpdf_attachFileToPage(filename, pdf.pdf, pagenumber);
        checkerror();
    }

    public static void attachFileFromMemory(byte[] data, string name, Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_attachFileFromMemory(byte[] data, int length, string name, int pdf);
        cpdf_attachFileFromMemory(data, data.Length, name, pdf.pdf);
        checkerror();
    }

    public static void attachFileToPageFromMemory(byte[] data, string name, Pdf pdf, int pagenumber)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_attachFileToPageFromMemory(byte[] data, int length, string name, int pdf, int pagenumber);
        cpdf_attachFileToPageFromMemory(data, data.Length, name, pdf.pdf, pagenumber);
        checkerror();
    }

    public static void removeAttachedFiles(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeAttachedFiles(int pdf);
        cpdf_removeAttachedFiles(pdf.pdf);
        checkerror();
    }

    public static void startGetAttachments(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_startGetAttachments(int pdf);
        cpdf_startGetAttachments(pdf.pdf);
        checkerror();
    }

    public static int numberGetAttachments()
    {
        [DllImport("libcpdf.so")] static extern int cpdf_numberGetAttachments();
        int res =cpdf_numberGetAttachments();
        checkerror();
        return res;
    }

    public static string getAttachmentName(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getAttachmentName(int n);
        string res = Marshal.PtrToStringUTF8(cpdf_getAttachmentName(n));
        checkerror();
        return res;
    }

    public static int getAttachmentPage(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getAttachmentPage(int n);
        int res =cpdf_getAttachmentPage(n);
        checkerror();
        return res;
    }

    public static byte[] getAttachmentData(int serial)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getAttachmentData(int serial, ref int len);
        int len = 0;
        IntPtr data = cpdf_getAttachmentData(serial, ref len);
        var databytes = new byte[len];
        Marshal.Copy(data, databytes, 0, len);
        [DllImport("libcpdf.so")] static extern void cpdf_free(IntPtr ptr);
        cpdf_free(data);
        checkerror();
        return databytes;
    }

    public static void endGetAttachments()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endGetAttachments();
        cpdf_endGetAttachments();
        checkerror();
    }

    /* CHAPTER 13. Images. */

    public static int startGetImageResolution(Pdf pdf, double min_required_resolution)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_startGetImageResolution(int pdf, double min_required_resolution);
        int res = cpdf_startGetImageResolution(pdf.pdf, min_required_resolution);
        checkerror();
        return res;
    }

    public static int getImageResolutionPageNumber(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getImageResolutionPageNumber(int n);
        int res = cpdf_getImageResolutionPageNumber(n);
        checkerror();
        return res;
    }

    public static string getImageResolutionImageName(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getImageResolutionImageName(int n);
        string res = Marshal.PtrToStringUTF8(cpdf_getImageResolutionImageName(n));
        checkerror();
        return res;
    }

    public static int getImageResolutionXPixels(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getImageResolutionXPixels(int n);
        int res = cpdf_getImageResolutionXPixels(n);
        checkerror();
        return res;
    }

    public static int getImageResolutionYPixels(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getImageResolutionYPixels(int n);
        int res = cpdf_getImageResolutionYPixels(n);
        checkerror();
        return res;
    }

    public static double getImageResolutionXRes(int n)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_getImageResolutionXRes(int n);
        double res = cpdf_getImageResolutionXRes(n);
        checkerror();
        return res;
    }

    public static double getImageResolutionYRes(int n)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_getImageResolutionYRes(int n);
        double res = cpdf_getImageResolutionYRes(n);
        checkerror();
        return res;
    }

    public static void endGetImageResolution()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endGetImageResolution();
        cpdf_endGetImageResolution();
        checkerror();
    }

    /* CHAPTER 14. Fonts. */

    public static void startGetFontInfo(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_startGetFontInfo(int pdf);
        cpdf_startGetFontInfo(pdf.pdf);
        checkerror();
    }

    public static int numberFonts()
    {
        [DllImport("libcpdf.so")] static extern int cpdf_numberFonts();
        int res = cpdf_numberFonts();
        checkerror();
        return res;
    }

    public static int getFontPage(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getFontPage(int n);
        int res = cpdf_getFontPage(n);
        checkerror();
        return res;
    }

    public static string getFontName(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getFontName(int n);
        string res = Marshal.PtrToStringUTF8(cpdf_getFontName(n));
        checkerror();
        return res;
    }

    public static string getFontType(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getFontType(int n);
        string res = Marshal.PtrToStringUTF8(cpdf_getFontType(n));
        checkerror();
        return res;
    }

    public static string getFontEncoding(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getFontEncoding(int n);
        string res = Marshal.PtrToStringUTF8(cpdf_getFontEncoding(n));
        checkerror();
        return res;
    }

    public static void endGetFontInfo()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endGetFontInfo();
        cpdf_endGetFontInfo();
        checkerror();
    }

    public static void removeFonts(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeFonts(int pdf);
        cpdf_removeFonts(pdf.pdf);
        checkerror();
    }

    public static void copyFont(Pdf docfrom, Pdf docto, List<int> range, int pagenumber, string fontname)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_copyFont(int docfrom, int docto, int range, int pagenumber, string fontname);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_copyFont(docfrom.pdf, docto.pdf, rn, pagenumber, fontname);
        cpdf_deleteRange(rn);
        checkerror();
    }

    /* CHAPTER 15. PDF and JSON */
    public static void outputJSON(string filename, bool parse_content, bool no_stream_data, bool decompress_streams, Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_outputJSON(string filename, int parse_content, int no_stream_data, int decompress_streams, int pdf);
        cpdf_outputJSON(filename, parse_content ? 1 : 0, no_stream_data ? 1 : 0, decompress_streams ? 1 : 0, pdf.pdf);
        checkerror();
    }

    public static byte[] outputJSONMemory(Pdf pdf, bool parse_content, bool no_stream_data, bool decompress_streams)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_outputJSONMemory(int pdf, int parse_content, int no_stream_data, int decompress_streams, ref int len);
        int len = 0;
        IntPtr data = cpdf_outputJSONMemory(pdf.pdf, parse_content ? 1 : 0, no_stream_data ? 1 : 0, decompress_streams ? 1 : 0, ref len);
        var databytes = new byte[len];
        Marshal.Copy(data, databytes, 0, len);
        [DllImport("libcpdf.so")] static extern void cpdf_free(IntPtr ptr);
        cpdf_free(data);
        checkerror();
        return databytes;
    }

    public static Pdf fromJSON(string filename)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fromJSON(string filename);
        int res = cpdf_fromJSON(filename);
        checkerror();
        return new Pdf(res);
    }

    public static Pdf fromJSONMemory(byte[] data)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fromJSONMemory(byte[] data, int length);
        int pdf = cpdf_fromJSONMemory(data, data.Length);
        checkerror();
        return new Pdf(pdf);
    }

    /* CHAPTER 16. Optional Content Groups */
    public static int startGetOCGList(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_startGetOCGList(int pdf);
        int res = cpdf_startGetOCGList(pdf.pdf);
        checkerror();
        return res;
    }

    public static string OCGListEntry(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_OCGListEntry(int n);
        string res = Marshal.PtrToStringUTF8(cpdf_OCGListEntry(n));
        checkerror();
        return res;
    }

    public static void endGetOCGList()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endGetOCGList();
        cpdf_endGetOCGList();
        checkerror();
    }

    public static void OCGRename(Pdf pdf, string name_from, string name_to)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_OCGRename(int pdf, string name_from, string name_to);
        cpdf_OCGRename(pdf.pdf, name_from, name_to);
        checkerror();
    }

    public static void OCGOrderAll(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_OCGOrderAll(int pdf);
        cpdf_OCGOrderAll(pdf.pdf);
        checkerror();
    }

    public static void OCGCoalesce(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_OCGCoalesce(int pdf);
        cpdf_OCGCoalesce(pdf.pdf);
        checkerror();
    }


    /* CHAPTER 17. Creating New PDFs */
    public static Pdf blankDocument(double w, double h, int pages)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_blankDocument(double w, double h, int pages);
        int res = cpdf_blankDocument(w, h, pages);
        checkerror();
        return new Pdf(res);
    }

    public static Pdf blankDocumentPaper(int papersize, int pages)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_blankDocumentPaper(int papersize, int pages);
        int res = cpdf_blankDocumentPaper(papersize, pages);
        checkerror();
        return new Pdf(res);
    }

    public static Pdf textToPDF(double w, double h, int font, double fontsize, string filename)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_textToPDF(double w, double h, int font, double fontsize, string filename);
        int res = cpdf_textToPDF(w, h, font, fontsize, filename);
        checkerror();
        return new Pdf(res);
    }

    public static Pdf textToPDFPaper(int papersize, int font, double fontsize, string filename)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_textToPDFPaper(int papersize, int font, double fontsize, string filename);
        int res = cpdf_textToPDFPaper(papersize, font, fontsize, filename);
        checkerror();
        return new Pdf(res);
    }


    /* CHAPTER 18. Miscellaneous */
    public static void draft(Pdf pdf, List<int> range, bool boxes)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_draft(int pdf, int range, int boxes);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_draft(pdf.pdf, rn, boxes ? 1 : 0);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void removeAllText(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeAllText(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_removeAllText(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void blackText(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_blackText(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_blackText(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void blackLines(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_blackLines(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_blackLines(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void blackFills(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_blackFills(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_blackFills(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void thinLines(Pdf pdf, List<int> range, double min_thickness)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_thinLines(int pdf, int range, double min_thickness);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_thinLines(pdf.pdf, rn, min_thickness);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void copyId(Pdf pdf_from, Pdf pdf_to)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_copyId(int pdf_from, int pdf_to);
        cpdf_copyId(pdf_from.pdf, pdf_to.pdf);
        checkerror();
    }

    public static void removeId(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeId(int pdf);
        cpdf_removeId(pdf.pdf);
        checkerror();
    }

    public static void setVersion(Pdf pdf, int version)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setVersion(int pdf, int version);
        cpdf_setVersion(pdf.pdf, version);
        checkerror();
    }

    public static void setFullVersion(Pdf pdf, int major, int minor)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setFullVersion(int pdf, int major, int minor);
        cpdf_setFullVersion(pdf.pdf, major, minor);
        checkerror();
    }

    public static void removeDictEntry(Pdf pdf, string key)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeDictEntry(int pdf, string key);
        cpdf_removeDictEntry(pdf.pdf, key);
        checkerror();
    }

    public static void removeDictEntrySearch(Pdf pdf, string key, string searchterm)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeDictEntrySearch(int pdf, string key, string searchterm);
        cpdf_removeDictEntrySearch(pdf.pdf, key, searchterm);
        checkerror();
    }

    public static void replaceDictEntry(Pdf pdf, string key, string newvalue)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_replaceDictEntry(int pdf, string key, string newvalue);
        cpdf_replaceDictEntry(pdf.pdf, key, newvalue);
        checkerror();
    }

    public static void replaceDictEntrySearch(Pdf pdf, string key, string newvalue, string searchterm)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_replaceDictEntrySearch(int pdf, string key, string newvalue, string searchterm);
        cpdf_replaceDictEntrySearch(pdf.pdf, key, newvalue, searchterm);
        checkerror();
    }

    public static void removeClipping(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeClipping(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_removeClipping(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static byte[] getDictEntries(Pdf pdf, string key)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getDictEntries(int pdf, string key, ref int retlen);
        int len = 0;
        IntPtr data = cpdf_getDictEntries(pdf.pdf, key, ref len);
        var databytes = new byte[len];
        Marshal.Copy(data, databytes, 0, len);
        [DllImport("libcpdf.so")] static extern void cpdf_free(IntPtr ptr);
        cpdf_free(data);
        checkerror();
        return databytes;
    }

    static void Main(string[] args)
    {
    }
}
}
