using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace CoherentGraphics
{
public class Cpdf
{
#pragma warning disable 414


    static int a1portrait = 1;
    static int a2portrait = 2;
    static int a3portrait = 3;
    static int a4portrait = 4;
    static int a5portrait = 5;
    static int a0landscape = 6;
    static int a1landscape = 7;
    static int a2landscape = 8;
    static int a3landscape = 9;
    static int a4landscape = 10;
    static int a5landscape = 11;
    static int usletterportrait = 12;
    static int usletterlandscape = 13;
    static int uslegalportrait = 14;
    static int uslegallandscape = 15;

    static int noEdit = 0;
    static int noPrint = 1;
    static int noCopy = 2;
    static int noAnnot = 3;
    static int noForms = 4;
    static int noExtract = 5;
    static int noAssemble = 6;
    static int noHqPrint = 7;

    static int pdf40bit = 0;
    static int pdf128bit = 1;
    static int aes128bitfalse = 2;
    static int aes128bittrue = 3;
    static int aes256bitfalse = 4;
    static int aes256bittrue = 5;
    static int aes256bitisofalse = 6;
    static int aes256bitisotrue = 7;

    static int posCentre = 0;
    static int posLeft = 1;
    static int posRight = 2;
    static int top = 3;
    static int topLeft = 4;
    static int topRight = 5;
    static int left = 6;
    static int bottomLeft = 7;
    static int bottom = 8;
    static int bottomRight = 9;
    static int right = 10;
    static int diagonal = 11;
    static int reverseDiagonal = 12;

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

    static int timesRoman = 0;
    static int timesBold = 1;
    static int timesItalic = 2;
    static int timesBoldItalic = 3;
    static int helvetica = 4;
    static int helveticaBold = 5;
    static int helveticaOblique = 6;
    static int helveticaBoldOblique = 7;
    static int courier = 8;
    static int courierBold = 9;
    static int courierOblique = 10;
    static int courierBoldOblique = 11;

    static int leftJustify = 0;
    static int CentreJustify = 1;
    static int RightJustify = 2;

    static int singlePage = 0;
    static int oneColumn = 1;
    static int twoColumnLeft = 2;
    static int twoColumnRight = 3;
    static int twoPageLeft = 4;
    static int twoPageRight = 5;

    static int useNone = 0;
    static int useOutlines = 1;
    static int useThumbs = 2;
    static int useOC = 3;
    static int useAttachments = 4;

    static int decimalArabic = 0;
    static int uppercaseRoman = 1;
    static int lowercaseRoman = 2;
    static int uppercaseLetters = 4;
    static int lowercaseLetters = 5;

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

    /* CHAPTER 0. Preliminaries */

    public static void startup()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_startup(IntPtr[] argv);
        IntPtr[] args = {};
        cpdf_startup(args);
        checkerror();
    }

    public static string version()
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_version();
        string s = Marshal.PtrToStringUTF8(cpdf_version());
        checkerror();
        return s;
    }

    public static void setFast()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setFast();
        cpdf_setFast();
        checkerror();
    }

    public static void setSlow()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setSlow();
        cpdf_setSlow();
        checkerror();
    }

    public static int lastError()
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fLastError();
        return cpdf_fLastError();
    }

    public static string lastErrorString()
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_fLastErrorString();
        return Marshal.PtrToStringUTF8(cpdf_fLastErrorString());
    }

    public static void clearError()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_clearError();
        cpdf_clearError();
    }

    public static void onExit()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_onExit();
        cpdf_onExit();
        checkerror();
    }


    /* CHAPTER 1. Basics */

    public static Pdf fromFile(string filename, string userpw)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fromFile(string filename, string userpw);
        int res =  cpdf_fromFile(filename, userpw);
        checkerror();
        return new Pdf(res);
    }

    public static Pdf fromFileLazy(string filename, string userpw)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fromFileLazy(string filename, string userpw);
        int res = cpdf_fromFileLazy(filename, userpw);
        checkerror();
        return new Pdf(res);
    }

    public static Pdf fromMemory(byte[] data, string userpw)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fromMemory(byte[] data, int length, string userpw);
        int pdf = cpdf_fromMemory(data, data.Length, userpw);
        checkerror();
        return new Pdf(pdf);
    }

    //For this one, the caller must use AllocHGlobal / Marshal.Copy / FreeHGlobal itself. It must not free the memory until the PDF is also gone.
    public static Pdf fromMemoryLazy(IntPtr data, int length, string userpw)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fromMemoryLazy(IntPtr data, int length, string userpw);
        int pdf = cpdf_fromMemoryLazy(data, length, userpw);
        checkerror();
        return new Pdf(pdf);
    }

    public static int startEnumeratePDFs()
    {
        [DllImport("libcpdf.so")] static extern int cpdf_startEnumeratePDFs();
        int res = cpdf_startEnumeratePDFs();
        checkerror();
        return res;
    }

    public static int enumeratePDFsKey(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_enumeratePDFsKey(int n);
        int res = cpdf_enumeratePDFsKey(n);
        checkerror();
        return res;
    }

    public static string enumeratePDFsInfo(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_enumeratePDFsInfo(int n);
        string res = Marshal.PtrToStringUTF8(cpdf_enumeratePDFsInfo(n));
        checkerror();
        return res;
    }

    public static void endEnumeratePDFs()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endEnumeratePDFs();
        cpdf_endEnumeratePDFs();
        checkerror();
    }

    public static double ptOfCm(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_ptOfCm(double i);
        double res = cpdf_ptOfCm(i);
        checkerror();
        return res;
    }

    public static double ptOfMm(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_ptOfMm(double i);
        double res = cpdf_ptOfMm(i);
        checkerror();
        return res;
    }

    public static double ptOfIn(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_ptOfIn(double i);
        double res = cpdf_ptOfIn(i);
        checkerror();
        return res;
    }

    public static double cmOfPt(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_cmOfPt(double i);
        double res = cpdf_cmOfPt(i);
        checkerror();
        return res;
    }

    public static double mmOfPt(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_mmOfPt(double i);
        double res = cpdf_mmOfPt(i);
        checkerror();
        return res;
    }

    public static double inOfPt(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_inOfPt(double i);
        double res = cpdf_inOfPt(i);
        checkerror();
        return res;
    }

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

    public static bool validatePagespec(string pagespec)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_validatePagespec(string pagespec);
        int res = cpdf_validatePagespec(pagespec);
        checkerror();
        return (res > 0);
    }

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

    public static List<int> blankRange()
    {
        return new List<int>();
    }

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

    public static int pages(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_pages(int pdf);
        int res = cpdf_pages(pdf.pdf);
        checkerror();
        return res;
    }

    public static int pagesFast(string password, string filename)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_pagesFast(string password, string filename);
        int res = cpdf_pagesFast(password, filename);
        checkerror();
        return res;
    }

    public static void toFile(Pdf pdf, string filename, bool linearize, bool make_id)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_toFile(int pdf, string filename, int linearize, int make_id);
        cpdf_toFile(pdf.pdf, filename, linearize ? 1 : 0, make_id ? 1 : 0);
        checkerror();
    }

    public static void toFileExt(Pdf pdf, string filename, bool linearize, bool make_id, bool preserve_objstm, bool generate_objstm, bool compress_objstm)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_toFileExt(int pdf, string filename, int linearize, int make_id, int preserve_objstm, int generate_objstm, int compress_objstm);
        cpdf_toFileExt(pdf.pdf, filename, linearize ? 1 : 0, make_id ? 1 : 0, preserve_objstm ? 1 : 0, generate_objstm ? 1 : 0, compress_objstm ? 1 : 0);
        checkerror();
    }

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

    public static bool isEncrypted(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_isEncrypted(int pdf);
        int res = cpdf_isEncrypted(pdf.pdf);
        checkerror();
        return (res > 0);
    }

    public static void decryptPdf(Pdf pdf, string userpw)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_decryptPdf(int pdf, string userpw);
        cpdf_decryptPdf(pdf.pdf, userpw);
        checkerror();
    }

    public static void decryptPdfOwner(Pdf pdf, string ownerpw)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_decryptPdfOwner(int pdf, string ownerpw);
        cpdf_decryptPdfOwner(pdf.pdf, ownerpw);
        checkerror();
    }

    public static void toFileEncrypted(Pdf pdf, int encryption_method, List<int> permissions, string ownerpw, string userpw, bool linearize, bool makeid, string filename)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_toFileEncrypted(int pdf, int encryption_method, int[] permissions, int permission_length, string ownerpw, string userpw, int linearize, int makeid, string filename);
        cpdf_toFileEncrypted(pdf.pdf, encryption_method, permissions.ToArray(), permissions.Count, ownerpw, userpw, linearize ? 1 : 0, makeid ? 1 : 0, filename);
        checkerror();
    }

    public static void toFileEncryptedExt(Pdf pdf, int encryption_method, List<int> permissions, string ownerpw, string userpw, bool linearize, bool makeid, bool preserve_objstm, bool generate_objstm, bool compress_objstm, string filename)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_toFileEncryptedExt(int pdf, int encryption_method, int[] permissions, int permission_length, string ownerpw, string userpw, int linearize, int makeid, int preserve_objstm, int generate_objstm, int compress_objstm, string filename);
        cpdf_toFileEncryptedExt(pdf.pdf, encryption_method, permissions.ToArray(), permissions.Count, ownerpw, userpw, linearize ? 1 : 0, makeid ? 1 : 0, preserve_objstm ? 1 : 0, generate_objstm ? 1 : 0, compress_objstm ? 1 : 0, filename);
        checkerror();
    }

    public static bool hasPermission(Pdf pdf, int permission)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_hasPermission(int pdf, int permission);
        int res = cpdf_hasPermission(pdf.pdf, permission);
        checkerror();
        return (res > 0);
    }

    public static int encryptionKind(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_encryptionKind(int pdf);
        int res = cpdf_encryptionKind(pdf.pdf);
        return res;
    }

    /* CHAPTER 2. Merging and Splitting */

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
    public static void scalePages(Pdf pdf, List<int> range, double sx, double sy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_scalePages(int pdf, int range, double sx, double sy);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_scalePages(pdf.pdf, rn, sx, sy);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void scaleToFit(Pdf pdf, List<int> range, double sx, double sy, double scale)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_scaleToFit(int pdf, int range, double sx, double sy, double scale);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_scaleToFit(pdf.pdf, rn, sx, sy, scale);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void scaleToFitPaper(Pdf pdf, List<int> range, int pagesize, double scale)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_scaleToFitPaper(int pdf, int range, int pagesize, double scale);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_scaleToFitPaper(pdf.pdf, rn, pagesize, scale);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void scaleContents(Pdf pdf, List<int> range, position position, double scale)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_scaleContents(int pdf, int range, position position, double scale);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_scaleContents(pdf.pdf, rn, position, scale);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void shiftContents(Pdf pdf, List<int> range, double dx, double dy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_shiftContents(int pdf, int range, double dx, double dy);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_shiftContents(pdf.pdf, rn, dx, dy);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void rotate(Pdf pdf, List<int> range, int rotation)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_rotate(int pdf, int range, int rotation);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_rotate(pdf.pdf, rn, rotation);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void rotateBy(Pdf pdf, List<int> range, int rotation)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_rotateBy(int pdf, int range, int rotation);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_rotateBy(pdf.pdf, rn, rotation);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void rotateContents(Pdf pdf, List<int> range, double angle)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_rotateContents(int pdf, int range, double angle);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_rotateContents(pdf.pdf, rn, angle);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void upright(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_upright(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_upright(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void hFlip(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_hFlip(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_hFlip(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void vFlip(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_vFlip(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_vFlip(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void crop(Pdf pdf, List<int> range, double x, double y, double w, double h)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_crop(int pdf, int range, double x, double y, double w, double h);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_crop(pdf.pdf, rn, x, y, w, h);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void removeCrop(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeCrop(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_removeCrop(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void removeTrim(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeTrim(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_removeTrim(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void removeArt(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeArt(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_removeArt(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void removeBleed(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeBleed(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_removeBleed(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void trimMarks(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_trimMarks(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_trimMarks(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void showBoxes(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_showBoxes(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_showBoxes(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

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
    public static void compress(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_compress(int pdf);
        cpdf_compress(pdf.pdf);
        checkerror();
    }

    public static void decompress(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_decompress(int pdf);
        cpdf_decompress(pdf.pdf);
        checkerror();
    }

    public static void squeezeInMemory(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_squeezeInMemory(int pdf);
        cpdf_squeezeInMemory(pdf.pdf);
        checkerror();
    }

    /* CHAPTER 6. Bookmarks */
    public static void startGetBookmarkInfo(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_startGetBookmarkInfo(int pdf);
        cpdf_startGetBookmarkInfo(pdf.pdf);
        checkerror();
    }

    public static int numberBookmarks()
    {
        [DllImport("libcpdf.so")] static extern int cpdf_numberBookmarks();
        int res = cpdf_numberBookmarks();
        checkerror();
        return res;
    }

    public static int getBookmarkLevel(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getBookmarkLevel(int n);
        int res = cpdf_getBookmarkLevel(n);
        checkerror();
        return res;
    }

    public static int getBookmarkPage(Pdf pdf, int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getBookmarkPage(int pdf, int n);
        int res = cpdf_getBookmarkPage(pdf.pdf, n);
        checkerror();
        return res;
    }

    public static string getBookmarkText(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getBookmarkText(int n);
        string res = Marshal.PtrToStringUTF8(cpdf_getBookmarkText(n));
        checkerror();
        return res;
    }

    public static bool getBookmarkOpenStatus(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getBookmarkOpenStatus(int n);
        int res = cpdf_getBookmarkOpenStatus(n);
        checkerror();
        return (res > 0);
    }

    public static void endGetBookmarkInfo()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endGetBookmarkInfo();
        cpdf_endGetBookmarkInfo();
        checkerror();
    }

    public static void startSetBookmarkInfo(int nummarks)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_startSetBookmarkInfo(int nummarks);
        cpdf_startSetBookmarkInfo(nummarks);
        checkerror();
    }

    public static void setBookmarkLevel(int n, int level)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBookmarkLevel(int n, int level);
        cpdf_setBookmarkLevel(n, level);
        checkerror();
    }

    public static void setBookmarkPage(Pdf pdf, int n, int targetpage)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBookmarkPage(int pdf, int n, int targetpage);
        cpdf_setBookmarkPage(pdf.pdf, n, targetpage);
        checkerror();
    }

    public static void setBookmarkOpenStatus(int n, bool status)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBookmarkOpenStatus(int n, int status);
        cpdf_setBookmarkOpenStatus(n, status ? 1 : 0);
        checkerror();
    }

    public static void setBookmarkText(int n, string text)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBookmarkText(int n, string text);
        cpdf_setBookmarkText(n, text);
        checkerror();
    }

    public static void endSetBookmarkInfo(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endSetBookmarkInfo(int pdf);
        cpdf_endSetBookmarkInfo(pdf.pdf);
        checkerror();
    }

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

    public static void setBookmarksJSON(Pdf pdf, byte[] data)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBookmarksJSON(int pdf, byte[] data, int length);
        cpdf_setBookmarksJSON(pdf.pdf, data, data.Length);
        checkerror();
    }

    public static void tableOfContents(Pdf pdf, int font, double fontsize, string title, bool bookmark)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_tableOfContents(int pdf, int font, double fontsize, string title, int bookmark);
        cpdf_tableOfContents(pdf.pdf, font, fontsize, title, bookmark ? 1 : 0);
        checkerror();
    }

    /* CHAPTER 7. Presentations */
    /* Not included in the library version. */

    /* CHAPTER 8. Logos, Watermarks and Stamps */

    public static void stampOn(Pdf stamp_pdf, Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_stampOn(int stamp_pdf, int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_stampOn(stamp_pdf.pdf, pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void stampUnder(Pdf stamp_pdf, Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_stampUnder(int stamp_pdf, int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_stampUnder(stamp_pdf.pdf, pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void stampExtended(Pdf pdf, Pdf pdf2, List<int> range, bool isover, bool scale_stamp_to_fit, position position, bool relative_to_cropbox)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_stampExtended(int pdf, int pdf2, int range, int isover, int scale_stamp_to_fit, position position, int relative_to_cropbox);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_stampExtended(pdf.pdf, pdf2.pdf, rn, isover ? 1 : 0, scale_stamp_to_fit ? 1 : 0, position, relative_to_cropbox ? 1 : 0);
        cpdf_deleteRange(rn);
        checkerror();
    }

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

    public static void removeText(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeText(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_removeText(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static int textWidth(int font, string text)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_textWidth(int font, string text);
        int res = cpdf_textWidth(font, text);
        checkerror();
        return res;
    }

    public static void addContent(string content, bool before, Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_addContent(string content, int before, int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_addContent(content, before ? 1 : 0, pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

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
    static public void impose(Pdf pdf, double x, double y, bool fit, bool columns, bool rtl, bool btt, bool center, double margin, double spacing, double linewidth)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_impose(int pdf, double x, double y, int fit, int columns, int rtl, int btt, int center, double margin, double spacing, double linewidth);
        cpdf_impose(pdf.pdf, x, y, fit ? 1 : 0, columns ? 1 : 0, rtl ? 1 : 0, btt ? 1 : 0, center ? 1 : 0, margin, spacing, linewidth);
        checkerror();
    }

    static public void twoUp(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_twoUp(int pdf);
        cpdf_twoUp(pdf.pdf);
        checkerror();
    }

    static public void twoUpStack(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_twoUpStack(int pdf);
        cpdf_twoUpStack(pdf.pdf);
        checkerror();
    }

    static public void padBefore(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_padBefore(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_padBefore(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    static public void padAfter(Pdf pdf, List<int> range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_padAfter(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_padAfter(pdf.pdf, rn);
        cpdf_deleteRange(rn);
        checkerror();
    }

    static public void padEvery(Pdf pdf, int n)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_padEvery(int pdf, int n);
        cpdf_padEvery(pdf.pdf, n);
        checkerror();
    }

    static public void padMultiple(Pdf pdf, int n)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_padMultiple(int pdf, int n);
        cpdf_padMultiple(pdf.pdf, n);
        checkerror();
    }

    static public void padMultipleBefore(Pdf pdf, int n)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_padMultipleBefore(int pdf, int n);
        cpdf_padMultipleBefore(pdf.pdf, n);
        checkerror();
    }

    /* CHAPTER 10. Annotations */
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

    public static bool isLinearized(string filename)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_isLinearized(string filename);
        int res = cpdf_isLinearized(filename);
        checkerror();
        return (res > 0);
    }

    public static int getVersion(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getVersion(int pdf);
        int res = cpdf_getVersion(pdf.pdf);
        checkerror();
        return res;
    }

    public static int getMajorVersion(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getMajorVersion(int pdf);
        int res = cpdf_getMajorVersion(pdf.pdf);
        checkerror();
        return res;
    }

    public static string getTitle(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getTitle(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getTitle(pdf.pdf));
        checkerror();
        return res;
    }

    public static string getAuthor(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getAuthor(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getAuthor(pdf.pdf));
        checkerror();
        return res;
    }

    public static string getSubject(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getSubject(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getSubject(pdf.pdf));
        checkerror();
        return res;
    }

    public static string getKeywords(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getKeywords(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getKeywords(pdf.pdf));
        checkerror();
        return res;
    }

    public static string getCreator(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getCreator(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getCreator(pdf.pdf));
        checkerror();
        return res;
    }

    public static string getProducer(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getProducer(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getProducer(pdf.pdf));
        checkerror();
        return res;
    }

    public static string getCreationDate(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getCreationDate(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getCreationDate(pdf.pdf));
        checkerror();
        return res;
    }

    public static string getModificationDate(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getModificationDate(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getModificationDate(pdf.pdf));
        checkerror();
        return res;
    }

    public static string getTitleXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getTitleXMP(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getTitleXMP(pdf.pdf));
        checkerror();
        return res;
    }

    public static string getAuthorXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getAuthorXMP(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getAuthorXMP(pdf.pdf));
        checkerror();
        return res;
    }

    public static string getSubjectXMP (Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getSubjectXMP(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getSubjectXMP(pdf.pdf));
        checkerror();
        return res;
    }

    public static string getKeywordsXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getKeywordsXMP(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getKeywordsXMP(pdf.pdf));
        checkerror();
        return res;
    }

    public static string getCreatorXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getCreatorXMP(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getCreatorXMP(pdf.pdf));
        checkerror();
        return res;
    }

    public static string getProducerXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getProducerXMP(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getProducerXMP(pdf.pdf));
        checkerror();
        return res;
    }

    public static string getCreationDateXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getCreationDateXMP(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getCreationDateXMP(pdf.pdf));
        checkerror();
        return res;
    }

    public static string getModificationDateXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getModificationDateXMP(int pdf);
        string res = Marshal.PtrToStringUTF8(cpdf_getModificationDateXMP(pdf.pdf));
        checkerror();
        return res;
    }

    public static void setTitle(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setTitle(int pdf, string s);
        cpdf_setTitle(pdf.pdf, s);
        checkerror();
    }

    public static void setAuthor(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setAuthor(int pdf, string s);
        cpdf_setAuthor(pdf.pdf, s);
        checkerror();
    }

    public static void setSubject(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setSubject(int pdf, string s);
        cpdf_setSubject(pdf.pdf, s);
        checkerror();
    }

    public static void setKeywords(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setKeywords(int pdf, string s);
        cpdf_setKeywords(pdf.pdf, s);
        checkerror();
    }

    public static void setCreator(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setCreator(int pdf, string s);
        cpdf_setCreator(pdf.pdf, s);
        checkerror();
    }

    public static void setProducer(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setProducer(int pdf, string s);
        cpdf_setProducer(pdf.pdf, s);
        checkerror();
    }

    public static void setCreationDate(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setCreationDate(int pdf, string s);
        cpdf_setCreationDate(pdf.pdf, s);
        checkerror();
    }

    public static void setModificationDate(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setModificationDate(int pdf, string s);
        cpdf_setModificationDate(pdf.pdf, s);
        checkerror();
    }

    public static void setTitleXMP(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setTitleXMP(int pdf, string s);
        cpdf_setTitleXMP(pdf.pdf, s);
        checkerror();
    }

    public static void setAuthorXMP(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setAuthorXMP(int pdf, string s);
        cpdf_setAuthorXMP(pdf.pdf, s);
        checkerror();
    }

    public static void setSubjectXMP(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setSubjectXMP(int pdf, string s);
        cpdf_setSubjectXMP(pdf.pdf, s);
        checkerror();
    }

    public static void setKeywordsXMP(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setKeywordsXMP(int pdf, string s);
        cpdf_setKeywordsXMP(pdf.pdf, s);
        checkerror();
    }

    public static void setCreatorXMP(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setCreatorXMP(int pdf, string s);
        cpdf_setCreatorXMP(pdf.pdf, s);
        checkerror();
    }

    public static void setProducerXMP(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setProducerXMP(int pdf, string s);
        cpdf_setProducerXMP(pdf.pdf, s);
        checkerror();
    }

    public static void setCreationDateXMP(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setCreationDateXMP(int pdf, string s);
        cpdf_setCreationDateXMP(pdf.pdf, s);
        checkerror();
    }

    public static void setModificationDateXMP(Pdf pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setModificationDateXMP(int pdf, string s);
        cpdf_setModificationDateXMP(pdf.pdf, s);
        checkerror();
    }

    public static void getDateComponents(string datestring, ref int year, ref int month, ref int day, ref int hour, ref int minute, ref int second, ref int hour_offset, ref int minute_offset)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getDateComponents(string datestring, ref int year, ref int month, ref int day, ref int hour, ref int minute, ref int second, ref int hour_offset, ref int minute_offset);
        cpdf_getDateComponents(datestring, ref year, ref month, ref day, ref hour, ref minute, ref second, ref hour_offset, ref minute_offset);
        checkerror();
    }

    public static string dateStringOfComponents(int y, int m, int d, int h, int min, int sec, int hour_offset, int minute_offset)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_dateStringOfComponents(int y, int m, int d, int h, int min, int sec, int hour_offset, int minute_offset);
        string res = Marshal.PtrToStringUTF8(cpdf_dateStringOfComponents(y, m, d, h, min, sec, hour_offset, minute_offset));
        checkerror();
        return res;
    }

    public static int getPageRotation(Pdf pdf, int pagenumber)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getPageRotation(int pdf, int pagenumber);
        int res = cpdf_getPageRotation(pdf.pdf, pagenumber);
        checkerror();
        return res;
    }

    public static bool hasBox(Pdf pdf, int pagenumber, string boxname)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_hasBox(int pdf, int pagenumber, string boxname);
        int res = cpdf_hasBox(pdf.pdf, pagenumber, boxname);
        checkerror();
        return (res > 0);
    }

    public static void getMediaBox(Pdf pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getMediaBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy);
        cpdf_getMediaBox(pdf.pdf, pagenumber, ref minx, ref maxx, ref miny, ref maxy);
        checkerror();
    }

    public static void getCropBox(Pdf pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getCropBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy);
        cpdf_getCropBox(pdf.pdf, pagenumber, ref minx, ref maxx, ref miny, ref maxy);
        checkerror();
    }

    public static void getTrimBox(Pdf pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getTrimBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy);
        cpdf_getTrimBox(pdf.pdf, pagenumber, ref minx, ref maxx, ref miny, ref maxy);
        checkerror();
    }

    public static void getArtBox(Pdf pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getArtBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy);
        cpdf_getArtBox(pdf.pdf, pagenumber, ref minx, ref maxx, ref miny, ref maxy);
        checkerror();
    }

    public static void getBleedBox(Pdf pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getBleedBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy);
        cpdf_getBleedBox(pdf.pdf, pagenumber, ref minx, ref maxx, ref miny, ref maxy);
        checkerror();
    }

    public static void setMediabox(Pdf pdf, List<int> range, double minx, double maxx, double miny, double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setMediabox(int pdf, int r, double minx, double maxx, double miny, double maxy);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_setMediabox(pdf.pdf, rn, minx, maxx, miny, maxy);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void setCropBox(Pdf pdf, List<int> range, double minx, double maxx, double miny, double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setCropBox(int pdf, int r, double minx, double maxx, double miny, double maxy);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_setCropBox(pdf.pdf, rn, minx, maxx, miny, maxy);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void setTrimBox(Pdf pdf, List<int> range, double minx, double maxx, double miny, double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setTrimBox(int pdf, int r, double minx, double maxx, double miny, double maxy);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_setTrimBox(pdf.pdf, rn, minx, maxx, miny, maxy);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void setArtBox(Pdf pdf, List<int> range, double minx, double maxx, double miny, double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setArtBox(int pdf, int r, double minx, double maxx, double miny, double maxy);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_setArtBox(pdf.pdf, rn, minx, maxx, miny, maxy);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void setBleedBox(Pdf pdf, List<int> range, double minx, double maxx, double miny, double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBleedBox(int pdf, int r, double minx, double maxx, double miny, double maxy);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_setBleedBox(pdf.pdf, rn, minx, maxx, miny, maxy);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void markTrapped(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_markTrapped(int pdf);
        cpdf_markTrapped(pdf.pdf);
        checkerror();
    }

    public static void markUntrapped(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_markUntrapped(int pdf);
        cpdf_markUntrapped(pdf.pdf);
        checkerror();
    }

    public static void markTrappedXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_markTrappedXMP(int pdf);
        cpdf_markTrappedXMP(pdf.pdf);
        checkerror();
    }

    public static void markUntrappedXMP(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_markUntrappedXMP(int pdf);
        cpdf_markUntrappedXMP(pdf.pdf);
        checkerror();
    }

    public static void setPageLayout(Pdf pdf, int layout)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setPageLayout(int pdf, int layout);
        cpdf_setPageLayout(pdf.pdf, layout);
        checkerror();
    }

    public static void setPageMode(Pdf pdf, int mode)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setPageMode(int pdf, int mode);
        cpdf_setPageMode(pdf.pdf, mode);
        checkerror();
    }

    public static void hideToolbar(Pdf pdf, bool flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_hideToolbar(int pdf, int flag);
        cpdf_hideToolbar(pdf.pdf, flag ? 1 : 0);
        checkerror();
    }

    public static void hideMenubar(Pdf pdf, bool flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_hideMenubar(int pdf, int flag);
        cpdf_hideMenubar(pdf.pdf, flag ? 1 : 0);
        checkerror();
    }

    public static void hideWindowUi(Pdf pdf, bool flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_hideWindowUi(int pdf, int flag);
        cpdf_hideWindowUi(pdf.pdf, flag ? 1 : 0);
        checkerror();
    }

    public static void fitWindow(Pdf pdf, bool flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_fitWindow(int pdf, int flag);
        cpdf_fitWindow(pdf.pdf, flag ? 1 : 0);
        checkerror();
    }

    public static void centerWindow(Pdf pdf, bool flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_centerWindow(int pdf, int flag);
        cpdf_centerWindow(pdf.pdf, flag ? 1 : 0);
        checkerror();
    }

    public static void displayDocTitle(Pdf pdf, bool flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_displayDocTitle(int pdf, int flag);
        cpdf_displayDocTitle(pdf.pdf, flag ? 1 : 0);
        checkerror();
    }

    public static void openAtPage(Pdf pdf, bool fit, int pagenumber)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_openAtPage(int pdf, int fit, int pagenumber);
        cpdf_openAtPage(pdf.pdf, fit ? 1 : 0, pagenumber);
        checkerror();
    }

    public static void setMetadataFromFile(Pdf pdf, string filename)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setMetadataFromFile(int pdf, string filename);
        cpdf_setMetadataFromFile(pdf.pdf, filename);
        checkerror();
    }

    public static void setMetadataFromByteArray(Pdf pdf, byte[] data)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setMetadataFromByteArray(int pdf, byte[] data, int length);
        cpdf_setMetadataFromByteArray(pdf.pdf, data, data.Length);
        checkerror();
    }

    public static void removeMetadata(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeMetadata(int pdf);
        cpdf_removeMetadata(pdf.pdf);
        checkerror();
    }

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

    public static void createMetadata(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_createMetadata(int pdf);
        cpdf_createMetadata(pdf.pdf);
        checkerror();
    }

    public static void setMetadataDate(Pdf pdf, string date)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setMetadataDate(int pdf, string date);
        cpdf_setMetadataDate(pdf.pdf, date);
        checkerror();
    }

    public static void addPageLabels(Pdf pdf, int style, string prefix, int offset, List<int> range, bool progress)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_addPageLabels(int pdf, int style, string prefix, int offset, int range, int progress);
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        int rn = range_of_list(range);
        cpdf_addPageLabels(pdf.pdf, style, prefix, offset, rn, progress ? 1 : 0);
        cpdf_deleteRange(rn);
        checkerror();
    }

    public static void removePageLabels(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removePageLabels(int pdf);
        cpdf_removePageLabels(pdf.pdf);
        checkerror();
    }

    public static string getPageLabelStringForPage(Pdf pdf, int pagenumber)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getPageLabelStringForPage(int pdf, int pagenumber);
        string res = Marshal.PtrToStringUTF8(cpdf_getPageLabelStringForPage(pdf.pdf, pagenumber));
        checkerror();
        return res;
    }

    public static int startGetPageLabels(Pdf pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_startGetPageLabels(int pdf);
        int res = cpdf_startGetPageLabels(pdf.pdf);
        checkerror();
        return res;
    }

    public static int getPageLabelStyle(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getPageLabelStyle(int n);
        int res = cpdf_getPageLabelStyle(n);
        checkerror();
        return res;
    }

    public static string getPageLabelPrefix(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getPageLabelPrefix(int n);
        string res = Marshal.PtrToStringUTF8(cpdf_getPageLabelPrefix(n));
        checkerror();
        return res;
    }

    public static int getPageLabelOffset(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getPageLabelOffset(int n);
        int res = cpdf_getPageLabelOffset(n);
        checkerror();
        return res;
    }

    public static int getPageLabelRange(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getPageLabelRange(int n);
        int res = cpdf_getPageLabelRange(n);
        checkerror();
        return res;
    }

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

    public static void chapter0()
    {
        /* CHAPTER 0. Preliminaries */
        Console.WriteLine("***** CHAPTER 0. Preliminaries");
        Console.WriteLine("---cpdf_startup()");
        startup();
        Console.WriteLine("---cpdf_version()");
        Console.WriteLine("version = {0}", version());
        Console.WriteLine("---cpdf_setFast()");
        setFast();
        Console.WriteLine("---cpdf_setSlow()");
        setSlow();
        Console.WriteLine("---cpdf_clearError()");
        clearError();
    }

    public static void chapter1()
    {
        /* CHAPTER 1. Basics */
        Console.WriteLine("***** CHAPTER 1. Basics");
        Console.WriteLine("---cpdf_fromFile()");
        Pdf pdf = fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_fromFileLazy()");
        Pdf pdf2 = fromFileLazy("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_toMemory()");
        byte[] mempdf = toMemory(pdf, false, false);
        Console.WriteLine("---cpdf_fromMemory()");
        Pdf frommem = fromMemory(mempdf, "");
        toFile(frommem, "testoutputs/01fromMemory.pdf", false, false);
        Console.WriteLine("---cpdf_fromMemoryLazy()");
        IntPtr ptr = Marshal.AllocHGlobal(mempdf.Length);
        Marshal.Copy(mempdf, 0, ptr, mempdf.Length);
        Pdf frommemlazy = fromMemoryLazy(ptr, mempdf.Length, "");
        toFile(frommemlazy, "testoutputs/01fromMemoryLazy.pdf", false, false);
        Pdf pdf3 = blankDocument(153.5, 234.2, 50);
        Pdf pdf4 = blankDocumentPaper(a4landscape, 50);
        Console.WriteLine("---cpdf: enumerate PDFs");
        int n = startEnumeratePDFs();
        for (int x = 0; x < n; x++)
        {
            int key = enumeratePDFsKey(x);
            string info = enumeratePDFsInfo(x);
        }
        endEnumeratePDFs();
        Console.WriteLine("---cpdf_ptOfIn()");
        Console.WriteLine($"One inch is {ptOfIn(1.0):0.000000} points");
        Console.WriteLine("---cpdf_ptOfCm()");
        Console.WriteLine($"One centimetre is {ptOfCm(1.0):0.000000} points");
        Console.WriteLine("---cpdf_ptOfMm()");
        Console.WriteLine($"One millimetre is {ptOfMm(1.0):0.000000} points");
        Console.WriteLine("---cpdf_inOfPt()");
        Console.WriteLine($"One point is {inOfPt(1.0):0.000000} inches");
        Console.WriteLine("---cpdf_cmOfPt()");
        Console.WriteLine($"One point is {cmOfPt(1.0):0.000000} centimetres");
        Console.WriteLine("---cpdf_mmOfPt()");
        Console.WriteLine($"One point is {mmOfPt(1.0):0.000000} millimetres");
        Console.WriteLine("---cpdf_range()");
        List<int> _range = range(1, 10);
        Console.WriteLine("---cpdf_all()");
        List<int> _all = all(pdf3);
        Console.WriteLine("---cpdf_even()");
        List<int> _even = even(_all);
        Console.WriteLine("---cpdf_odd()");
        List<int> _odd = odd(_all);
        Console.WriteLine("---cpdf_rangeUnion()");
        List<int> union = rangeUnion(_even, _odd);
        Console.WriteLine("---cpdf_difference()");
        List<int> diff = difference(_even, _odd);
        Console.WriteLine("---cpdf_removeDuplicates()");
        List<int> revdup = removeDuplicates(_even);
        Console.WriteLine("---cpdf_rangeLength()");
        int length = rangeLength(_even);
        Console.WriteLine("---cpdf_rangeGet()");
        int rangeget = rangeGet(_even, 1);
        Console.WriteLine("---cpdf_rangeAdd()");
        List<int> rangeadd = rangeAdd(_even, 20);
        Console.WriteLine("---cpdf_isInRange()");
        bool isin = isInRange(_even, 2);
        Console.WriteLine("---cpdf_parsePagespec()");
        List<int> r = parsePagespec(pdf3, "1-5");
        Console.WriteLine("---cpdf_validatePagespec()");
        bool valid = validatePagespec("1-4,5,6");
        Console.WriteLine($"Validating pagespec gives {(valid ? 1 : 0)}");
        Console.WriteLine("---cpdf_stringOfPagespec()");
        string ps = stringOfPagespec(pdf3, r);
        Console.WriteLine($"String of pagespec is {ps}");
        Console.WriteLine("---cpdf_blankRange()");
        List<int> b = blankRange();
        Pdf pdf10 = fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_pages()");
        int _pages = pages(pdf10);
        Console.WriteLine($"Pages = {_pages}");
        Console.WriteLine("---cpdf_pagesFast()");
        int pagesfast = pagesFast("", "testinputs/cpdflibmanual.pdf");
        Console.WriteLine($"Pages = {_pages}");
        Console.WriteLine("---cpdf_toFile()");
        toFile(pdf10, "testoutputs/01tofile.pdf", false, false);
        Console.WriteLine("---cpdf_toFileExt()");
        toFileExt(pdf10, "testoutputs/01tofileext.pdf", false, true, true, true, true);
        Console.WriteLine("---cpdf_isEncrypted()");
        bool isenc = isEncrypted(pdf10);
        Console.WriteLine($"isencrypted:{(isenc ? 1 : 0)}");
        Console.WriteLine("---cpdf_isLinearized()");
        bool lin = isLinearized("testinputs/cpdfmanual.pdf");
        Console.WriteLine($"islinearized:{(lin ? 1 : 0)}");
        using (Pdf pdf400 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pdf401 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        {
            List<int> permissions = new List<int> {noEdit};
            Console.WriteLine("---cpdf_toFileEncrypted()");
            toFileEncrypted(pdf400, pdf40bit, permissions, "owner", "user", false, false, "testoutputs/01encrypted.pdf");
            Console.WriteLine("---cpdf_toFileEncryptedExt()");
            toFileEncryptedExt(pdf401, pdf40bit, permissions, "owner", "user", false, false, true, true, true, "testoutputs/01encryptedext.pdf");
            Console.WriteLine("---cpdf_hasPermission()");
        }
        using (Pdf pdfenc = fromFile("testoutputs/01encrypted.pdf", "user"))
        {
            bool hasnoedit = hasPermission(pdfenc, noEdit);
            bool hasnocopy = hasPermission(pdfenc, noCopy);
            Console.WriteLine($"Haspermission {(hasnoedit ? 1 : 0)}, {(hasnocopy ? 1 : 0)}");
            Console.WriteLine("---cpdf_encryptionKind()");
            int enckind = encryptionKind(pdfenc);
            Console.WriteLine($"encryption kind is {enckind}");
        }
        Console.WriteLine("---cpdf_decryptPdf()");
        decryptPdf(pdf10, "");
        Console.WriteLine("---cpdf_decryptPdfOwner()");
        decryptPdfOwner(pdf10, "");
        frommem.Dispose();
        frommemlazy.Dispose();
        pdf.Dispose();
        pdf2.Dispose();
        pdf3.Dispose();
        pdf4.Dispose();
        pdf10.Dispose();
    }

    public static void chapter2()
    {
        /* CHAPTER 2. Merging and Splitting */
        Console.WriteLine("***** CHAPTER 2. Merging and Splitting");
        using (Pdf pdf11 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        {
            List<int> selectrange = range(1, 3);
            Console.WriteLine("---cpdf_mergeSimple()");
            Pdf[] arr = new [] {pdf11, pdf11, pdf11};
            List<Pdf> arr_list = new List<Pdf> {};
            arr_list.AddRange(arr);
            Pdf merged = mergeSimple(arr_list);
            toFile(merged, "testoutputs/02merged.pdf", false, true);
            merged.Dispose();
            Console.WriteLine("---cpdf_merge()");
            Pdf merged2 = merge(arr_list, false, false);
            toFile(merged2, "testoutputs/02merged2.pdf", false, true);
            merged2.Dispose();
            Console.WriteLine("---cpdf_mergeSame()");
            List<List<int>> ranges = new List<List<int>> {all(pdf11), all(pdf11), all(pdf11)};
            Pdf merged3 = mergeSame(arr_list, false, false, ranges);
            toFile(merged3, "testoutputs/02merged3.pdf", false, false);
            merged3.Dispose();
            Console.WriteLine("---cpdf_selectPages()");
            Pdf pdf12 = selectPages(pdf11, selectrange);
            toFile(pdf12, "testoutputs/02selected.pdf", false, false);
            pdf12.Dispose();
        }
    }

    public static void chapter3()
    {
        /* CHAPTER 3. Pages */
        Console.WriteLine("***** CHAPTER 3. Pages");
        using (Pdf pagespdf1 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf2 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf3 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf4 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf5 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf6 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf7 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf8 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf9 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf10 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf11 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf12 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf13 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf14 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf15 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf16 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf17 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf18 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf pagespdf19 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        {
            Console.WriteLine("---cpdf_scalePages()");
            scalePages(pagespdf1, all(pagespdf1), 1.5, 1.8);
            toFile(pagespdf1, "testoutputs/03scalepages.pdf", false, false);
            Console.WriteLine("---cpdf_scaleToFit()");
            scaleToFit(pagespdf2, all(pagespdf2), 1.5, 1.8, 0.9);
            toFile(pagespdf2, "testoutputs/03scaletofit.pdf", false, false);
            Console.WriteLine("---cpdf_scaleToFitPaper()");
            scaleToFitPaper(pagespdf3, all(pagespdf3), a4portrait, 0.8);
            toFile(pagespdf3, "testoutputs/03scaletofitpaper.pdf", false, false);
            Console.WriteLine("---cpdf_scaleContents()");
            position position = new position (topLeft, 20.0, 20.0);
            scaleContents(pagespdf4, all(pagespdf4), position, 2.0);
            toFile(pagespdf4, "testoutputs/03scalecontents.pdf", false, false);
            Console.WriteLine("---cpdf_shiftContents()");
            shiftContents(pagespdf5, all(pagespdf5), 1.5, 1.25);
            toFile(pagespdf5, "testoutputs/03shiftcontents.pdf", false, false);
            Console.WriteLine("---cpdf_rotate()");
            rotate(pagespdf6, all(pagespdf6), 90);
            toFile(pagespdf6, "testoutputs/03rotate.pdf", false, false);
            Console.WriteLine("---cpdf_rotateBy()");
            rotateBy(pagespdf7, all(pagespdf7), 90);
            toFile(pagespdf7, "testoutputs/03rotateby.pdf", false, false);
            Console.WriteLine("---cpdf_rotateContents()");
            rotateContents(pagespdf8, all(pagespdf8), 35.0);
            toFile(pagespdf8, "testoutputs/03rotatecontents.pdf", false, false);
            Console.WriteLine("---cpdf_upright()");
            upright(pagespdf9, all(pagespdf9));
            toFile(pagespdf9, "testoutputs/03upright.pdf", false, false);
            Console.WriteLine("---cpdf_hFlip()");
            hFlip(pagespdf10, all(pagespdf10));
            toFile(pagespdf10, "testoutputs/03hflip.pdf", false, false);
            Console.WriteLine("---cpdf_vFlip()");
            vFlip(pagespdf11, all(pagespdf11));
            toFile(pagespdf11, "testoutputs/03vflip.pdf", false, false);
            Console.WriteLine("---cpdf_crop()");
            crop(pagespdf12, all(pagespdf12), 0.0, 0.0, 400.0, 500.0);
            toFile(pagespdf12, "testoutputs/03crop.pdf", false, false);
            Console.WriteLine("---cpdf_trimMarks()");
            trimMarks(pagespdf13, all(pagespdf13));
            toFile(pagespdf13, "testoutputs/03trim_marks.pdf", false, false);
            Console.WriteLine("---cpdf_showBoxes()");
            showBoxes(pagespdf14, all(pagespdf14));
            toFile(pagespdf14, "testoutputs/03show_boxes.pdf", false, false);
            Console.WriteLine("---cpdf_hardBox()");
            hardBox(pagespdf15, all(pagespdf15), "/MediaBox");
            toFile(pagespdf15, "testoutputs/03hard_box.pdf", false, false);
            Console.WriteLine("---cpdf_removeCrop()");
            removeCrop(pagespdf16, all(pagespdf16));
            toFile(pagespdf16, "testoutputs/03remove_crop.pdf", false, false);
            Console.WriteLine("---cpdf_removeTrim()");
            removeTrim(pagespdf17, all(pagespdf17));
            toFile(pagespdf17, "testoutputs/03remove_trim.pdf", false, false);
            Console.WriteLine("---cpdf_removeArt()");
            removeArt(pagespdf18, all(pagespdf18));
            toFile(pagespdf18, "testoutputs/03remove_art.pdf", false, false);
            Console.WriteLine("---cpdf_removeBleed()");
            removeBleed(pagespdf19, all(pagespdf19));
            toFile(pagespdf19, "testoutputs/03remove_bleed.pdf", false, false);
        }
    }

    public static void chapter4()
    {
        /* CHAPTER 4. Encryption */
        /* Encryption covered under Chapter 1 in cpdflib. */
    }

    public static void chapter5()
    {
        /* CHAPTER 5. Compression */
        Console.WriteLine("***** CHAPTER 5. Compression");
        using (Pdf pdf16 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        {
            Console.WriteLine("---cpdf_compress()");
            compress(pdf16);
            toFile(pdf16, "testoutputs/05compressed.pdf", false, false);
            Console.WriteLine("---cpdf_decompress()");
            decompress(pdf16);
            toFile(pdf16, "testoutputs/05decompressed.pdf", false, false);
            Console.WriteLine("---cpdf_squeezeInMemory()");
            squeezeInMemory(pdf16);
            toFile(pdf16, "testoutputs/05squeezedinmemory.pdf", false, false);
        }
    }

    public static void chapter6()
    {
        /* CHAPTER 6. Bookmarks */
        Console.WriteLine("***** CHAPTER 6. Bookmarks");
        Pdf pdf17 = fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf: get bookmarks");
        startGetBookmarkInfo(pdf17);
        int nb = numberBookmarks();
        Console.WriteLine($"There are {nb} bookmarks");
        for (int b2 = 0; b2 < nb; b2++)
        {
            int level = getBookmarkLevel(b2);
            int page = getBookmarkPage(pdf17, b2);
            string text = getBookmarkText(b2);
            bool open = getBookmarkOpenStatus(b2);
            Console.WriteLine($"Bookmark at level {level} points to page {page} and has text \"{text}\" and open {(open ? 1 : 0)}");
        }
        endGetBookmarkInfo();
        Console.WriteLine("---cpdf: set bookmarks");
        startSetBookmarkInfo(1);
        setBookmarkLevel(0, 0);
        setBookmarkPage(pdf17, 0, 20);
        setBookmarkOpenStatus(0, true);
        setBookmarkText(0, "New bookmark!");
        endSetBookmarkInfo(pdf17);
        toFile(pdf17, "testoutputs/06newmarks.pdf", false, false);
        pdf17.Dispose();
        Console.WriteLine("---cpdf_getBookmarksJSON()");
        Pdf marksjson = fromFile("testinputs/cpdflibmanual.pdf", "");
        byte[] marksdata = getBookmarksJSON(marksjson);
        Console.WriteLine($"Contains {marksdata.Length} bytes of data");
        Console.WriteLine("---cpdf_setBookmarksJSON()");
        setBookmarksJSON(marksjson, marksdata);
        toFile(marksjson, "testoutputs/06jsonmarks.pdf", false, false);
        marksjson.Dispose();
        Console.WriteLine("---cpdf_tableOfContents()");
        Pdf tocpdf = fromFile("testinputs/cpdflibmanual.pdf", "");
        tableOfContents(tocpdf, timesRoman, 12.0, "Table of Contents", false);
        toFile(tocpdf, "testoutputs/06toc.pdf", false, false);
        tocpdf.Dispose();
    }

    public static void chapter7()
    {
        /* CHAPTER 7. Presentations */
        /* Not included in the library version. */
    }

    public static void chapter8()
    {
        /* CHAPTER 8. Logos, Watermarks and Stamps */
        Console.WriteLine("***** CHAPTER 8. Logos, Watermarks and Stamps");
        Pdf textfile = fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_addText()");
        position pos = new position (topLeft, 20.0, 20.0);
        addText(false,
                        textfile,
                        all(textfile),
                        "Some Text~~~~~~~~~~!",
                        pos,
                        1.0,
                        1,
                        timesRoman,
                        20.0,
                        0.5,
                        0.5,
                        0.5,
                        false,
                        false,
                        true,
                        0.5,
                        leftJustify,
                        false,
                        false,
                        "",
                        1.0,
                        false);
        Console.WriteLine("---cpdf_addTextSimple()");
        addTextSimple(textfile, all(textfile), "The text!", pos, timesRoman, 50.0);
        toFile(textfile, "testoutputs/08added_text.pdf", false, false);
        Console.WriteLine("---cpdf_removeText()");
        removeText(textfile, all(textfile));
        toFile(textfile, "testoutputs/08removed_text.pdf", false, false);
        textfile.Dispose();
        Console.WriteLine("---cpdf_textWidth()");
        int w = textWidth(timesRoman, "What is the width of this?");
        Pdf stamp = fromFile("testinputs/logo.pdf", "");
        Pdf stampee = fromFile("testinputs/cpdflibmanual.pdf", "");
        List<int> stamp_range = all(stamp);
        Console.WriteLine("---cpdf_stampOn()");
        stampOn(stamp, stampee, stamp_range);
        Console.WriteLine("---cpdf_stampUnder()");
        stampUnder(stamp, stampee, stamp_range);
        position spos = new position (topLeft, 20.0, 20.0);
        Console.WriteLine("---cpdf_stampExtended()");
        stampExtended(stamp, stampee, stamp_range, true, true, spos, true);
        toFile(stamp, "testoutputs/08stamp_after.pdf", false, false);
        toFile(stampee, "testoutputs/08stampee_after.pdf", false, false);
        stamp.Dispose();
        stampee.Dispose();
        Pdf c1 = fromFile("testinputs/logo.pdf", "");
        Pdf c2 = fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_combinePages()");
        Pdf c3 = combinePages(c1, c2);
        toFile(c3, "testoutputs/08c3after.pdf", false, false);
        Console.WriteLine("---cpdf_stampAsXObject()");
        c1.Dispose();
        c2.Dispose();
        c3.Dispose();
        Pdf undoc = fromFile("testinputs/cpdflibmanual.pdf", "");
        Pdf ulogo = fromFile("testinputs/logo.pdf", "");
        string name = stampAsXObject(undoc, all(undoc), ulogo);
        string content = $"q 1 0 0 1 100 100 cm {name} Do Q q 1 0 0 1 300 300 cm {name} Do Q q 1 0 0 1 500 500 cm {name} Do Q";
        Console.WriteLine("---cpdf_addContent()");
        addContent(content, true, undoc, all(undoc));
        toFile(undoc, "testoutputs/08demo.pdf", false, false);
        undoc.Dispose();
        ulogo.Dispose();
    }

    public static void chapter9()
    {
        /* CHAPTER 9. Multipage facilities */
        Console.WriteLine("***** CHAPTER 9. Multipage facilities");
        using (Pdf mp = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf mp2 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf mp25 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf mp26 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf mp3 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf mp4 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf mp5 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf mp6 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf mp7 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        {
            Console.WriteLine("---cpdf_twoUp()");
            twoUp(mp);
            toFile(mp, "testoutputs/09mp.pdf", false, false);
            Console.WriteLine("---cpdf_twoUpStack()");
            twoUpStack(mp2);
            toFile(mp2, "testoutputs/09mp2.pdf", false, false);
            Console.WriteLine("---cpdf_impose()");
            impose(mp25, 5.0, 4.0, false, false, false, false, false, 40.0, 20.0, 2.0);
            toFile(mp25, "testoutputs/09mp25.pdf", false, false);
            impose(mp26, 2000.0, 1000.0, true, false, false, false, false, 40.0, 20.0, 2.0);
            toFile(mp26, "testoutputs/09mp26.pdf", false, false);
            Console.WriteLine("---cpdf_padBefore()");
            padBefore(mp3, range(1, 10));
            toFile(mp3, "testoutputs/09mp3.pdf", false, false);
            Console.WriteLine("---cpdf_padAfter()");
            padAfter(mp4, range(1, 10));
            toFile(mp4, "testoutputs/09mp4.pdf", false, false);
            Console.WriteLine("---cpdf_padEvery()");
            padEvery(mp5, 5);
            toFile(mp5, "testoutputs/09mp5.pdf", false, false);
            Console.WriteLine("---cpdf_padMultiple()");
            padMultiple(mp6, 10);
            toFile(mp6, "testoutputs/09mp6.pdf", false, false);
            Console.WriteLine("---cpdf_padMultipleBefore()");
            padMultipleBefore(mp7, 23);
            toFile(mp7, "testoutputs/09mp7.pdf", false, false);
        }
    }

    public static void chapter10()
    {
        /* CHAPTER 10. Annotations */
        Console.WriteLine("***** CHAPTER 10. Annotations");
        Console.WriteLine("---cpdf_annotationsJSON()");
        using (Pdf annot = fromFile("testinputs/cpdflibmanual.pdf", ""))
        {
            byte[] annotjson = annotationsJSON(annot);
            Console.WriteLine($"Contains {annotjson.Length} bytes of data");
        }
    }

    public static void chapter11()
    {
        /* CHAPTER 11. Document Information and Metadata */
        Console.WriteLine("***** CHAPTER 11. Document Information and Metadata");
        using (Pdf pdf30 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        {
            Console.WriteLine("---cpdf_getVersion()");
            int v = getVersion(pdf30);
            Console.WriteLine($"minor version:{v}");
            Console.WriteLine("---cpdf_getMajorVersion()");
            int v2 = getMajorVersion(pdf30);
            Console.WriteLine($"major version:{v2}");
            Console.WriteLine("---cpdf_getTitle()");
            string title = getTitle(pdf30);
            Console.WriteLine($"title: {title}");
            Console.WriteLine("---cpdf_getAuthor()");
            string author = getAuthor(pdf30);
            Console.WriteLine($"author: {author}");
            Console.WriteLine("---cpdf_getSubject()");
            string subject = getSubject(pdf30);
            Console.WriteLine($"subject: {subject}");
            Console.WriteLine("---cpdf_getKeywords()");
            string keywords = getKeywords(pdf30);
            Console.WriteLine($"keywords: {keywords}");
            Console.WriteLine("---cpdf_getCreator()");
            string creator = getCreator(pdf30);
            Console.WriteLine($"creator: {creator}");
            Console.WriteLine("---cpdf_getProducer()");
            string producer = getProducer(pdf30);
            Console.WriteLine($"producer: {producer}");
            Console.WriteLine("---cpdf_getCreationDate()");
            string creationdate = getCreationDate(pdf30);
            Console.WriteLine($"creationdate: {creationdate}");
            Console.WriteLine("---cpdf_getModificationDate()");
            string modificationdate = getModificationDate(pdf30);
            Console.WriteLine($"modificationdate: {modificationdate}");
            Console.WriteLine("---cpdf_getTitleXMP()");
            string titlexmp = getTitleXMP(pdf30);
            Console.WriteLine($"titleXMP: {titlexmp}");
            Console.WriteLine("---cpdf_getAuthorXMP()");
            string authorxmp = getAuthorXMP(pdf30);
            Console.WriteLine($"authorXMP: {authorxmp}");
            Console.WriteLine("---cpdf_getSubjectXMP()");
            string subjectxmp = getSubjectXMP(pdf30);
            Console.WriteLine($"subjectXMP: {subjectxmp}");
            Console.WriteLine("---cpdf_getKeywordsXMP()");
            string keywordsxmp = getKeywordsXMP(pdf30);
            Console.WriteLine($"keywordsXMP: {keywordsxmp}");
            Console.WriteLine("---cpdf_getCreatorXMP()");
            string creatorxmp = getCreatorXMP(pdf30);
            Console.WriteLine($"creatorXMP: {creatorxmp}");
            Console.WriteLine("---cpdf_getProducerXMP()");
            string producerxmp = getProducerXMP(pdf30);
            Console.WriteLine($"producerXMP: {producerxmp}");
            Console.WriteLine("---cpdf_getCreationDateXMP()");
            string creationdatexmp = getCreationDateXMP(pdf30);
            Console.WriteLine($"creationdateXMP: {creationdatexmp}");
            Console.WriteLine("---cpdf_getModificationDateXMP()");
            string modificationdatexmp = getModificationDateXMP(pdf30);
            Console.WriteLine($"modificationdateXMP: {modificationdatexmp}");
            Console.WriteLine("---cpdf_setTitle()");
            setTitle(pdf30, "title");
            Console.WriteLine("---cpdf_setAuthor()");
            setAuthor(pdf30, "author");
            Console.WriteLine("---cpdf_setSubject()");
            setSubject(pdf30, "subject");
            Console.WriteLine("---cpdf_setKeywords()");
            setKeywords(pdf30, "keywords");
            Console.WriteLine("---cpdf_setCreator()");
            setCreator(pdf30, "creator");
            Console.WriteLine("---cpdf_setProducer()");
            setProducer(pdf30, "producer");
            Console.WriteLine("---cpdf_setCreationDate()");
            setCreationDate(pdf30, "now");
            Console.WriteLine("---cpdf_setModificationDate()");
            setModificationDate(pdf30, "now");
            Console.WriteLine("---cpdf_setTitleXMP()");
            setTitleXMP(pdf30, "title");
            Console.WriteLine("---cpdf_setAuthorXMP()");
            setAuthorXMP(pdf30, "author");
            Console.WriteLine("---cpdf_setSubjectXMP()");
            setSubjectXMP(pdf30, "subject");
            Console.WriteLine("---cpdf_setKeywordsXMP()");
            setKeywordsXMP(pdf30, "keywords");
            Console.WriteLine("---cpdf_setCreatorXMP()");
            setCreatorXMP(pdf30, "creator");
            Console.WriteLine("---cpdf_setProducerXMP()");
            setProducerXMP(pdf30, "producer");
            Console.WriteLine("---cpdf_setCreationDateXMP()");
            setCreationDateXMP(pdf30, "now");
            Console.WriteLine("---cpdf_setModificationDateXMP()");
            setModificationDateXMP(pdf30, "now");
            toFile(pdf30, "testoutputs/11setinfo.pdf", false, false);
            int year = 0;
            int month = 0;
            int day = 0;
            int hour = 0;
            int minute = 0;
            int second = 0;
            int hour_offset = 0;
            int minute_offset = 0;
            Console.WriteLine("---cpdf_getDateComponents()");
            getDateComponents("D:20061108125017Z", ref year, ref month, ref day, ref hour, ref minute, ref second, ref hour_offset, ref minute_offset);
            Console.WriteLine($"D:20061108125017Z = {year}, {month}, {day}, {hour}, {minute}, {second}, {hour_offset}, {minute_offset}");
            Console.WriteLine("---cpdf_dateStringOfComponents()");
            string datestr = dateStringOfComponents(year, month, day, hour, minute, second, hour_offset, minute_offset);
            Console.WriteLine(datestr);
            Console.WriteLine("---cpdf_getPageRotation()");
            int rot = getPageRotation(pdf30, 1);
            Console.WriteLine($"/Rotate on page 1 = {rot}");
            Console.WriteLine("---cpdf_hasBox()");
            bool hasbox = hasBox(pdf30, 1, "/CropBox");
            Console.WriteLine($"hasbox: {(hasbox ? 1 : 0)}");
            double mb_minx = 0.0;
            double mb_maxx = 0.0;
            double mb_miny = 0.0;
            double mb_maxy = 0.0;
            double cb_minx = 0.0;
            double cb_maxx = 0.0;
            double cb_miny = 0.0;
            double cb_maxy = 0.0;
            double tb_minx = 0.0;
            double tb_maxx = 0.0;
            double tb_miny = 0.0;
            double tb_maxy = 0.0;
            double ab_minx = 0.0;
            double ab_maxx = 0.0;
            double ab_miny = 0.0;
            double ab_maxy = 0.0;
            double bb_minx = 0.0;
            double bb_maxx = 0.0;
            double bb_miny = 0.0;
            double bb_maxy = 0.0;
            Console.WriteLine("---cpdf_getMediaBox()");
            getMediaBox(pdf30, 1, ref mb_minx, ref mb_maxx, ref mb_miny, ref mb_maxy);
            Console.WriteLine($"Media: {mb_minx:0.000000} {mb_maxx:0.000000} {mb_miny:0.000000} {mb_maxy:0.000000}");
            Console.WriteLine("---cpdf_getCropBox()");
            getCropBox(pdf30, 1, ref cb_minx, ref cb_maxx, ref cb_miny, ref cb_maxy);
            Console.WriteLine($"Crop: {cb_minx:0.000000} {cb_maxx:0.000000} {cb_miny:0.000000} {cb_maxy:0.000000}");
            Console.WriteLine("---cpdf_getBleedBox()");
            getBleedBox(pdf30, 1, ref bb_minx, ref bb_maxx, ref bb_miny, ref bb_maxy);
            Console.WriteLine($"Bleed: {bb_minx:0.000000} {bb_maxx:0.000000} {bb_miny:0.000000} {bb_maxy:0.000000}");
            Console.WriteLine("---cpdf_getArtBox()");
            getArtBox(pdf30, 1, ref ab_minx, ref ab_maxx, ref ab_miny, ref ab_maxy);
            Console.WriteLine($"Art: {ab_minx:0.000000} {ab_maxx:0.000000} {ab_miny:0.000000} {ab_maxy:0.000000}");
            Console.WriteLine("---cpdf_getTrimBox()");
            getTrimBox(pdf30, 1, ref tb_minx, ref tb_maxx, ref tb_miny, ref tb_maxy);
            Console.WriteLine($"Trim: {tb_minx:0.000000} {tb_maxx:0.000000} {tb_miny:0.000000} {tb_maxy:0.000000}");
            Console.WriteLine("---cpdf_setMediaBox()");
            setMediabox(pdf30, all(pdf30), 100, 500, 150, 550);
            Console.WriteLine("---cpdf_setCropBox()");
            setCropBox(pdf30, all(pdf30), 100, 500, 150, 550);
            Console.WriteLine("---cpdf_setTrimBox()");
            setTrimBox(pdf30, all(pdf30), 100, 500, 150, 550);
            Console.WriteLine("---cpdf_setArtBox()");
            setArtBox(pdf30, all(pdf30), 100, 500, 150, 550);
            Console.WriteLine("---cpdf_setBleedBox()");
            setBleedBox(pdf30, all(pdf30), 100, 500, 150, 550);
            toFile(pdf30, "testoutputs/11setboxes.pdf", false, false);
            Console.WriteLine("---cpdf_markTrapped()");
            markTrapped(pdf30);
            Console.WriteLine("---cpdf_markTrappedXMP()");
            markTrappedXMP(pdf30);
            toFile(pdf30, "testoutputs/11trapped.pdf", false, false);
            Console.WriteLine("---cpdf_markUntrapped()");
            markUntrapped(pdf30);
            Console.WriteLine("---cpdf_markUntrappedXMP()");
            markUntrappedXMP(pdf30);
            toFile(pdf30, "testoutputs/11untrapped.pdf", false, false);
            Console.WriteLine("---cpdf_setPageLayout()");
            setPageLayout(pdf30, twoColumnLeft);
            Console.WriteLine("---cpdf_setPageMode()");
            setPageMode(pdf30, useOutlines);
            Console.WriteLine("---cpdf_hideToolbar()");
            hideToolbar(pdf30, true);
            Console.WriteLine("---cpdf_hideMenubar()");
            hideMenubar(pdf30, true);
            Console.WriteLine("---cpdf_hideWindowUi()");
            hideWindowUi(pdf30, true);
            Console.WriteLine("---cpdf_fitWindow()");
            fitWindow(pdf30, true);
            Console.WriteLine("---cpdf_centerWindow()");
            centerWindow(pdf30, true);
            Console.WriteLine("---cpdf_displayDocTitle()");
            displayDocTitle(pdf30, true);
            Console.WriteLine("---cpdf_openAtPage()");
            openAtPage(pdf30, true, 4);
            toFile(pdf30, "testoutputs/11open.pdf", false, false);
            Console.WriteLine("---cpdf_setMetadataFromFile()");
            setMetadataFromFile(pdf30, "testinputs/cpdflibmanual.pdf");
            toFile(pdf30, "testoutputs/11metadata1.pdf", false, false);
            Console.WriteLine("---cpdf_setMetadataFromByteArray()");
            byte[] md = Encoding.ASCII.GetBytes("BYTEARRAY");
            setMetadataFromByteArray(pdf30, md);
            toFile(pdf30, "testoutputs/11metadata2.pdf", false, false);
            Console.WriteLine("---cpdf_getMetadata()");
            byte[] metadata = getMetadata(pdf30);
            Console.WriteLine("---cpdf_removeMetadata()");
            removeMetadata(pdf30);
            Console.WriteLine("---cpdf_createMetadata()");
            createMetadata(pdf30);
            toFile(pdf30, "testoutputs/11metadata3.pdf", false, false);
            Console.WriteLine("---cpdf_setMetadataDate()");
            setMetadataDate(pdf30, "now");
            toFile(pdf30, "testoutputs/11metadata4.pdf", false, false);
            Console.WriteLine("---cpdf_addPageLabels()");
            addPageLabels(pdf30, uppercaseRoman, "PREFIX-", 1, all(pdf30), false);
            Console.WriteLine("---cpdf: get page labels");
            int pls = startGetPageLabels(pdf30);
            Console.WriteLine($"There are {pls} labels");
            for (int plsc = 0; plsc < pls; plsc++)
            {
                int style = getPageLabelStyle(plsc);
                string prefix = getPageLabelPrefix(plsc);
                int offset = getPageLabelOffset(plsc);
                int lab_range = getPageLabelRange(plsc);
                Console.WriteLine($"Page label: {style}, {prefix}, {offset}, {lab_range}");
            }
            endGetPageLabels();
            Console.WriteLine("---cpdf_removePageLabels()");
            removePageLabels(pdf30);
            toFile(pdf30, "testoutputs/11pagelabels.pdf", false, false);
            Console.WriteLine("---cpdf_getPageLabelStringForPage()");
            string pl = getPageLabelStringForPage(pdf30, 1);
            Console.WriteLine($"Label string is {pl}");
        }
    }

    public static void chapter12()
    {
        /* CHAPTER 12. File Attachments */
        Console.WriteLine("***** CHAPTER 12. File Attachments");
        using (Pdf attachments = fromFile("testinputs/has_attachments.pdf", ""))
        {
            Console.WriteLine("---cpdf_attachFile()");
            attachFile("testinputs/image.pdf", attachments);
            Console.WriteLine("---cpdf_attachFileToPage()");
            attachFileToPage("testinputs/image.pdf", attachments, 1);
            Console.WriteLine("---cpdf_attachFileFromMemory()");
            byte[] empty = {};
            attachFileFromMemory(empty, "metadata.txt", attachments);
            Console.WriteLine("---cpdf_attachFileToPageFromMemory()");
            attachFileToPageFromMemory(empty, "metadata.txt", attachments, 1);
            toFile(attachments, "testoutputs/12with_attachments.pdf", false, false);
            Console.WriteLine("---cpdf: get attachments");
            startGetAttachments(attachments);
            int n_a = numberGetAttachments();
            Console.WriteLine($"There are {n_a} attachments to get");
            for (int aa = 0; aa < n_a; aa++)
            {
                string a_n = getAttachmentName(aa);
                Console.WriteLine($"Attachment {aa} is named {a_n}");
                int a_page = getAttachmentPage(aa);
                Console.WriteLine($"It is on page {a_page}");
                byte[] a_data = getAttachmentData(aa);
                Console.WriteLine($"Contains {a_data.Length} bytes of data");
            }
            endGetAttachments();
            Console.WriteLine("---cpdf_removeAttachedFiles()");
            removeAttachedFiles(attachments);
            toFile(attachments, "testoutputs/12removed_attachments.pdf", false, false);
        }
    }

    public static void chapter13()
    {
        /* CHAPTER 13. Images. */
        Console.WriteLine("***** CHAPTER 13. Images");
        Console.WriteLine("---cpdf: get image resolution");
        using (Pdf image_pdf = fromFile("testinputs/image.pdf", ""))
        {
            int im_n = startGetImageResolution(image_pdf, 2.0);
            for (int im = 0; im < im_n; im++)
            {
                int im_p = getImageResolutionPageNumber(im);
                string im_name = getImageResolutionImageName(im);
                int im_xp = getImageResolutionXPixels(im);
                int im_yp = getImageResolutionYPixels(im);
                double im_xres = getImageResolutionXRes(im);
                double im_yres = getImageResolutionYRes(im);
                Console.WriteLine($"IMAGE: {im_p}, {im_name}, {im_xp}, {im_yp}, {im_xres:00.000000}, {im_yres:00.000000}");
            }
            endGetImageResolution();
        }
    }

    public static void chapter14()
    {
        /* CHAPTER 14. Fonts. */
        Console.WriteLine("***** CHAPTER 14. Fonts");
        Console.WriteLine("---cpdf: Get Fonts");
        using (Pdf fonts = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf fonts2 = fromFile("testinputs/frontmatter.pdf", ""))
        {
            startGetFontInfo(fonts);
            int n_fonts = numberFonts();
            for (int ff = 0; ff < n_fonts; ff++)
            {
                int page = getFontPage(ff);
                string f_name = getFontName(ff);
                string type = getFontType(ff);
                string encoding = getFontEncoding(ff);
                Console.WriteLine("Page {0}, font {1} has type {2} and encoding {3}", page, f_name, type, encoding);
            }
            endGetFontInfo();
            Console.WriteLine("---cpdf_removeFonts()");
            removeFonts(fonts);
            toFile(fonts, "testoutputs/14remove_fonts.pdf", false, false);
            Console.WriteLine("---cpdf_copyFont()");
            copyFont(fonts, fonts2, all(fonts), 1, "/Font");
        }
    }

    public static void chapter15()
    {
        /* CHAPTER 15. PDF and JSON */
        Console.WriteLine("***** CHAPTER 15. PDF and JSON");
        using (Pdf jsonpdf = fromFile("testinputs/cpdflibmanual.pdf", ""))
        {
            Console.WriteLine("---cpdf_outputJSON()");
            outputJSON("testoutputs/15json.json", false, false, false, jsonpdf);
            outputJSON("testoutputs/15jsonnostream.json", false, true, false, jsonpdf);
            outputJSON("testoutputs/15jsonparsed.json", true, false, false, jsonpdf);
            outputJSON("testoutputs/15jsondecomp.json", false, false, true, jsonpdf);
            Console.WriteLine("---cpdf_fromJSON()");
            Pdf fromjsonpdf = fromJSON("testoutputs/15jsonparsed.json");
            toFile(fromjsonpdf, "testoutputs/15fromjson.pdf", false, false);
            Console.WriteLine("---cpdf_outputJSONMemory()");
            byte[] jbuf = outputJSONMemory(fromjsonpdf, false, false, false);
            fromjsonpdf.Dispose();
            Console.WriteLine("---cpdf_fromJSONMemory()");
            Pdf jfrommem = fromJSONMemory(jbuf);
            toFile(jfrommem, "testoutputs/15fromJSONMemory.pdf", false, false);
            jfrommem.Dispose();
        }
    }

    public static void chapter16()
    {
        /* CHAPTER 16. Optional Content Groups */
        Console.WriteLine("***** CHAPTER 16. Optional Content Groups");
        using (Pdf ocg = fromFile("testinputs/has_ocgs.pdf", ""))
        {
          Console.WriteLine("---cpdf: Get OCG List");
          int n2 = startGetOCGList(ocg);
          for(int x = 0; x < n2; x++)
          {
              Console.WriteLine(OCGListEntry(x));
          }
          endGetOCGList();
          Console.WriteLine("---cpdf_OCGCoalesce()");
          OCGCoalesce(ocg);
          Console.WriteLine("---cpdf_OCGRename()");
          OCGRename(ocg, "From", "To");
          Console.WriteLine("---cpdf_OCGOrderAll()");
          OCGOrderAll(ocg);
        }
    }

    public static void chapter17()
    {
        /* CHAPTER 17. Creating New PDFs */
        Console.WriteLine("***** CHAPTER 17. Creating New PDFs");
        Console.WriteLine("---cpdf_blankDocument()");
        Console.WriteLine("---cpdf_blankDocumentPaper()");
        using (Pdf new1 = blankDocument(100.0, 200.0, 20))
        using (Pdf new2 = blankDocumentPaper(a4portrait, 10))
        {
            toFile(new1, "testoutputs/01blank.pdf", false, false);
            toFile(new2, "testoutputs/01blanka4.pdf", false, false);
        }
        Console.WriteLine("---cpdf_textToPDF()");
        Console.WriteLine("---cpdf_textToPDFPaper()");
        using (Pdf ttpdf = textToPDF(500.0, 600.0, timesItalic, 8.0, "../cpdflib-source/cpdflibtest.c"))
        using (Pdf ttpdfpaper = textToPDFPaper(a4portrait, timesBoldItalic, 10.0, "../cpdflib-source/cpdflibtest.c"))
        {
            toFile(ttpdf, "testoutputs/01ttpdf.pdf", false, false);
            toFile(ttpdfpaper, "testoutputs/01ttpdfpaper.pdf", false, false);
        }
    }

    public static void chapter18()
    {
        /* CHAPTER 18. Miscellaneous */
        Console.WriteLine("***** CHAPTER 18. Miscellaneous");
        using (Pdf misc = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf misc2 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf misc3 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf misc4 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf misc5 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf misc6 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf misc7 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf misc8 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf misc9 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf misc10 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf misc11 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf misc12 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf misc13 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf misc14 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf misc15 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf misc16 = fromFile("testinputs/cpdflibmanual.pdf", ""))
        using (Pdf misclogo = fromFile("testinputs/logo.pdf", ""))
        {
            Console.WriteLine("---cpdf_draft()");
            draft(misc, all(misc), true);
            toFile(misc, "testoutputs/17draft.pdf", false, false);
            Console.WriteLine("---cpdf_removeAllText()");
            removeAllText(misc2, all(misc2));
            toFile(misc2, "testoutputs/17removealltext.pdf", false, false);
            Console.WriteLine("---cpdf_blackText()");
            blackText(misc3, all(misc3));
            toFile(misc3, "testoutputs/17blacktext.pdf", false, false);
            Console.WriteLine("---cpdf_blackLines()");
            blackLines(misc4, all(misc4));
            toFile(misc4, "testoutputs/17blacklines.pdf", false, false);
            Console.WriteLine("---cpdf_blackFills()");
            blackFills(misc5, all(misc5));
            toFile(misc5, "testoutputs/17blackfills.pdf", false, false);
            Console.WriteLine("---cpdf_thinLines()");
            thinLines(misc6, all(misc6), 2.0);
            toFile(misc6, "testoutputs/17thinlines.pdf", false, false);
            Console.WriteLine("---cpdf_copyId()");
            copyId(misclogo, misc7);
            toFile(misc7, "testoutputs/17copyid.pdf", false, false);
            Console.WriteLine("---cpdf_removeId()");
            removeId(misc8);
            toFile(misc8, "testoutputs/17removeid.pdf", false, false);
            Console.WriteLine("---cpdf_setVersion()");
            setVersion(misc9, 1);
            toFile(misc9, "testoutputs/17setversion.pdf", false, false);
            Console.WriteLine("---cpdf_setFullVersion()");
            setFullVersion(misc10, 2, 0);
            toFile(misc10, "testoutputs/17setfullversion.pdf", false, false);
            Console.WriteLine("---cpdf_removeDictEntry()");
            removeDictEntry(misc11, "/Producer");
            toFile(misc11, "testoutputs/17removedictentry.pdf", false, false);
            Console.WriteLine("---cpdf_removeDictEntrySearch()");
            removeDictEntrySearch(misc13, "/Producer", "1");
            toFile(misc13, "testoutputs/17removedictentrysearch.pdf", false, false);
            Console.WriteLine("---cpdf_replaceDictEntry()");
            replaceDictEntry(misc14, "/Producer", "{\"I\" : 1}");
            toFile(misc14, "testoutputs/17replacedictentry.pdf", false, false);
            Console.WriteLine("---cpdf_replaceDictEntrySearch()");
            replaceDictEntrySearch(misc15, "/Producer", "1", "2");
            toFile(misc15, "testoutputs/17replacedictentrysearch.pdf", false, false);
            Console.WriteLine("---cpdf_getDictEntries()");
            byte[] entries = getDictEntries(misc16, "/Producer");
            Console.WriteLine($"length of entries data = {entries.Length}");
            Console.WriteLine("---cpdf_removeClipping()");
            removeClipping(misc12, all(misc12));
            toFile(misc12, "testoutputs/17removeclipping.pdf", false, false);
        }
    }

    static void Main(string[] args)
    {
        chapter0();
        chapter1();
        chapter2();
        chapter3();
        chapter4();
        chapter5();
        chapter6();
        chapter7();
        chapter8();
        chapter9();
        chapter10();
        chapter11();
        chapter12();
        chapter13();
        chapter14();
        chapter15();
        chapter16();
        chapter17();
        chapter18();
    }
}
}
