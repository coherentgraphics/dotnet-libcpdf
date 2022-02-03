using System;
using System.Text;
using System.Runtime.InteropServices;

namespace dotnet_libcpdf
{
class Program
{
#pragma warning disable 414

    static int netcpdf_false = 0;
    static int netcpdf_true = 1;

    static int netcpdf_a0portrait = 0;
    static int netcpdf_a1portrait = 1;
    static int netcpdf_a2portrait = 2;
    static int netcpdf_a3portrait = 3;
    static int netcpdf_a4portrait = 4;
    static int netcpdf_a5portrait = 5;
    static int netcpdf_a0landscape = 6;
    static int netcpdf_a1landscape = 7;
    static int netcpdf_a2landscape = 8;
    static int netcpdf_a3landscape = 9;
    static int netcpdf_a4landscape = 10;
    static int netcpdf_a5landscape = 11;
    static int netcpdf_usletterportrait = 12;
    static int netcpdf_usletterlandscape = 13;
    static int netcpdf_uslegalportrait = 14;
    static int netcpdf_uslegallandscape = 15;

    static int netcpdf_noEdit = 0;
    static int netcpdf_noPrint = 1;
    static int netcpdf_noCopy = 2;
    static int netcpdf_noAnnot = 3;
    static int netcpdf_noForms = 4;
    static int netcpdf_noExtract = 5;
    static int netcpdf_noAssemble = 6;
    static int netcpdf_noHqPrint = 7;

    static int netcpdf_pdf40bit = 0;
    static int netcpdf_pdf128bit = 1;
    static int netcpdf_aes128bitfalse = 2;
    static int netcpdf_aes128bittrue = 3;
    static int netcpdf_aes256bitfalse = 4;
    static int netcpdf_aes256bittrue = 5;
    static int netcpdf_aes256bitisofalse = 6;
    static int netcpdf_aes256bitisotrue = 7;

    static int netcpdf_posCentre = 0;
    static int netcpdf_posLeft = 1;
    static int netcpdf_posRight = 2;
    static int netcpdf_top = 3;
    static int netcpdf_topLeft = 4;
    static int netcpdf_topRight = 5;
    static int netcpdf_left = 6;
    static int netcpdf_bottomLeft = 7;
    static int netcpdf_bottom = 8;
    static int netcpdf_bottomRight = 9;
    static int netcpdf_right = 10;
    static int netcpdf_diagonal = 11;
    static int netcpdf_reverseDiagonal = 12;

    public struct netcpdf_position
    {
        public int netcpdf_anchor;
        public double netcpdf_coord1;
        public double netcpdf_coord2;

        public netcpdf_position(int netcpdf_anchor, double netcpdf_coord1, double netcpdf_coord2)
        {
            this.netcpdf_anchor = netcpdf_anchor;
            this.netcpdf_coord1 = netcpdf_coord1;
            this.netcpdf_coord2 = netcpdf_coord2;
        }
    }

    static int netcpdf_timesRoman = 0;
    static int netcpdf_timesBold = 1;
    static int netcpdf_timesItalic = 2;
    static int netcpdf_timesBoldItalic = 3;
    static int netcpdf_helvetica = 4;
    static int netcpdf_helveticaBold = 5;
    static int netcpdf_helveticaOblique = 6;
    static int netcpdf_helveticaBoldOblique = 7;
    static int netcpdf_courier = 8;
    static int netcpdf_courierBold = 9;
    static int netcpdf_courierOblique = 10;
    static int netcpdf_courierBoldOblique = 11;

    static int netcpdf_leftJustify = 0;
    static int netcpdf_CentreJustify = 1;
    static int netcpdf_RightJustify = 2;

    static int netcpdf_singlePage = 0;
    static int netcpdf_oneColumn = 1;
    static int netcpdf_twoColumnLeft = 2;
    static int netcpdf_twoColumnRight = 3;
    static int netcpdf_twoPageLeft = 4;
    static int netcpdf_twoPageRight = 5;

    static int netcpdf_useNone = 0;
    static int netcpdf_useOutlines = 1;
    static int netcpdf_useThumbs = 2;
    static int netcpdf_useOC = 3;
    static int netcpdf_useAttachments = 4;

    static int netcpdf_decimalArabic = 0;
    static int netcpdf_uppercaseRoman = 1;
    static int netcpdf_lowercaseRoman = 2;
    static int netcpdf_uppercaseLetters = 4;
    static int netcpdf_lowercaseLetters = 5;

#pragma warning restore 414

    /* CHAPTER 0. Preliminaries */

    public static void netcpdf_startup()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_startup(IntPtr[] argv);
        IntPtr[] args = {};
        cpdf_startup(args);
    }

    public static string netcpdf_version()
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_version();
        return Marshal.PtrToStringAuto(cpdf_version());
    }

    public static void netcpdf_setFast()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setFast();
        cpdf_setFast();
    }

    public static void netcpdf_setSlow()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setSlow();
        cpdf_setSlow();
    }

    public static int netcpdf_lastError()
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fLastError();
        return cpdf_fLastError();
    }

    public static string netcpdf_lastErrorString()
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_fLastErrorString();
        return Marshal.PtrToStringAuto(cpdf_fLastErrorString());
    }

    public static void netcpdf_clearError()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_clearError();
        cpdf_clearError();
    }

    public static void netcpdf_onExit()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_onExit();
        cpdf_onExit();
    }


    /* CHAPTER 1. Basics */

    public static int netcpdf_fromFile(string filename, string userpw)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fromFile(string filename, string userpw);
        return cpdf_fromFile(filename, userpw);
    }

    public static int netcpdf_fromFileLazy(string filename, string userpw)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fromFileLazy(string filename, string userpw);
        return cpdf_fromFileLazy(filename, userpw);
    }

    public static int netcpdf_fromMemory(byte[] data, string userpw)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fromMemory(IntPtr data, int length, string userpw);
        IntPtr ptr = Marshal.AllocHGlobal(data.Length);
        Marshal.Copy(data, 0, ptr, data.Length);
        int pdf = cpdf_fromMemory(ptr, data.Length, userpw);
        Marshal.FreeHGlobal(ptr);
        return pdf;
    }

    public static int netcpdf_fromMemoryPtr(IntPtr data, int length, string userpw)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fromMemory(IntPtr data, int length, string userpw);
        return cpdf_fromMemory(data, length, userpw);
    }

    public static int netcpdf_fromMemoryPtrLazy(IntPtr data, int length, string userpw)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fromMemoryLazy(IntPtr data, int length, string userpw);
        return cpdf_fromMemoryLazy(data, length, userpw);
    }


    public static void netcpdf_deletePdf(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_deletePdf(int pdf);
        cpdf_deletePdf(pdf);
    }

    public static void netcpdf_replacePdf(int pdf, int pdf2)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_replacePdf(int pdf, int pdf2);
        cpdf_replacePdf(pdf, pdf2);
    }

    public static int netcpdf_startEnumeratePDFs()
    {
        [DllImport("libcpdf.so")] static extern int cpdf_startEnumeratePDFs();
        return cpdf_startEnumeratePDFs();
    }

    public static int netcpdf_enumeratePDFsKey(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_enumeratePDFsKey(int n);
        return cpdf_enumeratePDFsKey(n);
    }

    public static string netcpdf_enumeratePDFsInfo(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_enumeratePDFsInfo(int n);
        return Marshal.PtrToStringAuto(cpdf_enumeratePDFsInfo(n));
    }

    public static void netcpdf_endEnumeratePDFs()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endEnumeratePDFs();
        cpdf_endEnumeratePDFs();
    }

    public static double netcpdf_ptOfCm(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_ptOfCm(double i);
        return cpdf_ptOfCm(i);
    }

    public static double netcpdf_ptOfMm(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_ptOfMm(double i);
        return cpdf_ptOfMm(i);
    }

    public static double netcpdf_ptOfIn(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_ptOfIn(double i);
        return cpdf_ptOfIn(i);
    }

    public static double netcpdf_cmOfPt(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_cmOfPt(double i);
        return cpdf_cmOfPt(i);
    }

    public static double netcpdf_mmOfPt(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_mmOfPt(double i);
        return cpdf_mmOfPt(i);
    }

    public static double netcpdf_inOfPt(double i)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_inOfPt(double i);
        return cpdf_inOfPt(i);
    }

    public static int netcpdf_parsePagespec(int pdf, string pagespec)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_parsePagespec(int pdf, string pagespec);
        return cpdf_parsePagespec(pdf, pagespec);
    }

    public static int netcpdf_validatePagespec(string pagespec)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_validatePagespec(string pagespec);
        return cpdf_validatePagespec(pagespec);
    }

    public static string netcpdf_stringOfPagespec(int pdf, int r)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_stringOfPagespec(int pdf, int r);
        return Marshal.PtrToStringAuto(cpdf_stringOfPagespec(pdf, r));
    }

    public static int netcpdf_blankRange()
    {
        [DllImport("libcpdf.so")] static extern int cpdf_blankRange();
        return cpdf_blankRange();
    }

    public static void netcpdf_deleteRange(int r)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_deleteRange(int r);
        cpdf_deleteRange(r);
    }

    public static int netcpdf_range(int f, int t)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_range(int f, int t);
        return cpdf_range(f, t);
    }

    public static int netcpdf_all(int pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_all(int pdf);
        return cpdf_all(pdf);
    }

    public static int netcpdf_even(int pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_even(int pdf);
        return cpdf_even(pdf);
    }

    public static int netcpdf_odd(int pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_odd(int pdf);
        return cpdf_odd(pdf);
    }

    public static int netcpdf_rangeUnion(int a, int b)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_rangeUnion(int a, int b);
        return cpdf_rangeUnion(a, b);
    }

    public static int netcpdf_difference(int a, int b)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_difference(int a, int b);
        return cpdf_difference(a, b);
    }

    public static int netcpdf_removeDuplicates(int r)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_removeDuplicates(int r);
        return cpdf_removeDuplicates(r);
    }

    public static int netcpdf_rangeLength(int r)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_rangeLength(int r);
        return cpdf_rangeLength(r);
    }

    public static int netcpdf_rangeGet(int r, int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_rangeGet(int r, int n);
        return cpdf_rangeGet(r, n);
    }

    public static int netcpdf_rangeAdd(int r, int page)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_rangeAdd(int r, int page);
        return cpdf_rangeAdd(r, page);
    }

    public static int netcpdf_isInRange(int r, int page)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_isInRange(int r, int page);
        return cpdf_isInRange(r, page);
    }

    public static int netcpdf_pages(int pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_pages(int pdf);
        return cpdf_pages(pdf);
    }

    public static int netcpdf_pagesFast(string password, string filename)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_pagesFast(string password, string filename);
        return cpdf_pagesFast(password, filename);
    }

    public static void netcpdf_toFile(int pdf, string filename, int linearize, int make_id)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_toFile(int pdf, string filename, int linearize, int make_id);
        cpdf_toFile(pdf, filename, linearize, make_id);
    }

    public static void netcpdf_toFileExt(int pdf, string filename, int linearize, int make_id, int preserve_objstm, int generate_objstm, int compress_objstm)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_toFileExt(int pdf, string filename, int linearize, int make_id, int preserve_objstm, int generate_objstm, int compress_objstm);
        cpdf_toFileExt(pdf, filename, linearize, make_id, preserve_objstm, generate_objstm, compress_objstm);
    }

    public static byte[] netcpdf_toMemory(int pdf, int linearize, int makeid)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_toMemory(int pdf, int linearize, int makeid, ref int length);
        int length = 0;
        IntPtr ptr = cpdf_toMemory(pdf, linearize, makeid, ref length);
        byte[] data = new byte[length];
        //Marshal.copy(ptr, 0, data, length);
        //FIXME: Free the memory in C - we need to export a free-ing function in cpdflibwrapper.h
        return data;
    }

    public static int netcpdf_isEncrypted(int pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_isEncrypted(int pdf);
        return cpdf_isEncrypted(pdf);
    }

    public static void netcpdf_decryptPdf(int pdf, string userpw)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_decryptPdf(int pdf, string userpw);
        cpdf_decryptPdf(pdf, userpw);
    }

    public static void netcpdf_decryptPdfOwner(int pdf, string ownerpw)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_decryptPdfOwner(int pdf, string ownerpw);
        cpdf_decryptPdfOwner(pdf, ownerpw);
    }

    public static void netcpdf_toFileEncrypted(int pdf, int encryption_method, int[] permissions, int permission_length, string ownerpw, string userpw, int linearize, int makeid, string filename)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_toFileEncrypted(int pdf, int encryption_method, int[] permissions, int permission_length, string ownerpw, string userpw, int linearize, int makeid, string filename);
        cpdf_toFileEncrypted(pdf, encryption_method, permissions, permission_length, ownerpw, userpw, linearize, makeid, filename);
    }

    public static void netcpdf_toFileEncryptedExt(int pdf, int encryption_method, int[] permissions, int permission_length, string ownerpw, string userpw, int linearize, int makeid, int preserve_objstm, int generate_objstm, int compress_objstm, string filename)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_toFileEncryptedExt(int pdf, int encryption_method, int[] permissions, int permission_length, string ownerpw, string userpw, int linearize, int makeid, int preserve_objstm, int generate_objstm, int compress_objstm, string filename);
        cpdf_toFileEncryptedExt(pdf, encryption_method, permissions, permission_length, ownerpw, userpw, linearize, makeid, preserve_objstm, generate_objstm, compress_objstm, filename);
    }

    public static int netcpdf_hasPermission(int pdf, int permission)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_hasPermission(int pdf, int permission);
        return cpdf_hasPermission(pdf, permission);
    }

    public static int netcpdf_encryptionKind(int pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_encryptionKind(int pdf);
        return cpdf_encryptionKind(pdf);
    }

    /* CHAPTER 2. Merging and Splitting */

    public static int netcpdf_mergeSimple(int[] pdfs, int length)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_mergeSimple(int[] pdfs, int length);
        return cpdf_mergeSimple(pdfs, length);
    }

    public static int netcpdf_merge(int[] pdfs, int length, int retain_numbering, int remove_duplicate_fonts)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_merge(int[] pdfs, int length, int retain_numbering, int remove_duplicate_fonts);
        return cpdf_merge(pdfs, length, retain_numbering, remove_duplicate_fonts);
    }

    public static int netcpdf_mergeSame(int[] pdfs, int length, int retain_numbering, int remove_duplicate_fonts, int[] ranges)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_mergeSame(int[] pdfs, int length, int retain_numbering, int remove_duplicate_fonts, int[] ranges);
        return cpdf_mergeSame(pdfs, length, retain_numbering, remove_duplicate_fonts, ranges);
    }

    public static int netcpdf_selectPages(int pdf, int r)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_selectPages(int pdf, int r);
        return cpdf_selectPages(pdf, r);
    }

    /* CHAPTER 3. Pages */
    public static void netcpdf_scalePages(int pdf, int range, double sx, double sy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_scalePages(int pdf, int range, double sx, double sy);
        cpdf_scalePages(pdf, range, sx, sy);
    }

    public static void netcpdf_scaleToFit(int pdf, int range, double sx, double sy, double scale)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_scaleToFit(int pdf, int range, double sx, double sy, double scale);
        cpdf_scaleToFit(pdf, range, sx, sy, scale);
    }

    public static void netcpdf_scaleToFitPaper(int pdf, int range, int pagesize, double scale)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_scaleToFitPaper(int pdf, int range, int pagesize, double scale);
        cpdf_scaleToFitPaper(pdf, range, pagesize, scale);
    }

    public static void netcpdf_scaleContents(int pdf, int range, netcpdf_position position, double scale)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_scaleContents(int pdf, int range, netcpdf_position position, double scale);
        cpdf_scaleContents(pdf, range, position, scale);
    }

    public static void netcpdf_shiftContents(int pdf, int range, double dx, double dy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_shiftContents(int pdf, int range, double dx, double dy);
        cpdf_shiftContents(pdf, range, dx, dy);
    }

    public static void netcpdf_rotate(int pdf, int range, int rotation)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_rotate(int pdf, int range, int rotation);
        cpdf_rotate(pdf, range, rotation);
    }

    public static void netcpdf_rotateBy(int pdf, int range, int rotation)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_rotateBy(int pdf, int range, int rotation);
        cpdf_rotateBy(pdf, range, rotation);
    }

    public static void netcpdf_rotateContents(int pdf, int range, double angle)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_rotateContents(int pdf, int range, double angle);
        cpdf_rotateContents(pdf, range, angle);
    }

    public static void netcpdf_upright(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_upright(int pdf, int range);
        cpdf_upright(pdf, range);
    }

    public static void netcpdf_hFlip(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_hFlip(int pdf, int range);
        cpdf_hFlip(pdf, range);
    }

    public static void netcpdf_vFlip(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_vFlip(int pdf, int range);
        cpdf_vFlip(pdf, range);
    }

    public static void netcpdf_crop(int pdf, int range, double x, double y, double w, double h)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_crop(int pdf, int range, double x, double y, double w, double h);
        cpdf_crop(pdf, range, x, y, w, h);
    }

    public static void netcpdf_removeCrop(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeCrop(int pdf, int range);
        cpdf_removeCrop(pdf, range);
    }

    public static void netcpdf_removeTrim(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeTrim(int pdf, int range);
        cpdf_removeTrim(pdf, range);
    }

    public static void netcpdf_removeArt(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeArt(int pdf, int range);
        cpdf_removeArt(pdf, range);
    }

    public static void netcpdf_removeBleed(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeBleed(int pdf, int range);
        cpdf_removeBleed(pdf, range);
    }

    public static void netcpdf_trimMarks(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_trimMarks(int pdf, int range);
        cpdf_trimMarks(pdf, range);
    }

    public static void netcpdf_showBoxes(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_showBoxes(int pdf, int range);
        cpdf_showBoxes(pdf, range);
    }

    public static void netcpdf_hardBox(int pdf, int range, string boxname)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_hardBox(int pdf, int range, string boxname);
        cpdf_hardBox(pdf, range, boxname);
    }

    /* CHAPTER 4. Encryption */
    /* Encryption covered under Chapter 1 in cpdflib. */

    /* CHAPTER 5. Compression */
    public static void netcpdf_compress(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_compress(int pdf);
        cpdf_compress(pdf);
    }

    public static void netcpdf_decompress(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_decompress(int pdf);
        cpdf_decompress(pdf);
    }

    public static void netcpdf_squeezeInMemory(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_squeezeInMemory(int pdf);
        cpdf_squeezeInMemory(pdf);
    }

    /* CHAPTER 6. Bookmarks */
    public static void netcpdf_startGetBookmarkInfo(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_startGetBookmarkInfo(int pdf);
        cpdf_startGetBookmarkInfo(pdf);
    }

    public static int netcpdf_numberBookmarks()
    {
        [DllImport("libcpdf.so")] static extern int cpdf_numberBookmarks();
        return cpdf_numberBookmarks();
    }

    public static int netcpdf_getBookmarkLevel(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getBookmarkLevel(int n);
        return cpdf_getBookmarkLevel(n);
    }

    public static int netcpdf_getBookmarkPage(int pdf, int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getBookmarkPage(int pdf, int n);
        return cpdf_getBookmarkPage(pdf, n);
    }

    public static string netcpdf_getBookmarkText(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getBookmarkText(int n);
        return Marshal.PtrToStringAuto(cpdf_getBookmarkText(n));
    }

    public static int netcpdf_getBookmarkOpenStatus(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getBookmarkOpenStatus(int n);
        return cpdf_getBookmarkOpenStatus(n);
    }

    public static void netcpdf_endGetBookmarkInfo()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endGetBookmarkInfo();
        cpdf_endGetBookmarkInfo();
    }

    public static void netcpdf_startSetBookmarkInfo(int nummarks)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_startSetBookmarkInfo(int nummarks);
        cpdf_startSetBookmarkInfo(nummarks);
    }

    public static void netcpdf_setBookmarkLevel(int n, int level)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBookmarkLevel(int n, int level);
        cpdf_setBookmarkLevel(n, level);
    }

    public static void netcpdf_setBookmarkPage(int pdf, int n, int targetpage)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBookmarkPage(int pdf, int n, int targetpage);
        cpdf_setBookmarkPage(pdf, n, targetpage);
    }

    public static void netcpdf_setBookmarkOpenStatus(int n, int status)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBookmarkOpenStatus(int n, int status);
        cpdf_setBookmarkOpenStatus(n, status);
    }

    public static void netcpdf_setBookmarkText(int n, string text)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBookmarkText(int n, string text);
        cpdf_setBookmarkText(n, text);
    }

    public static void netcpdf_endSetBookmarkInfo(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endSetBookmarkInfo(int pdf);
        cpdf_endSetBookmarkInfo(pdf);
    }

    public static void netcpdf_tableOfContents(int pdf, int font, double fontsize, string title, int bookmark)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_tableOfContents(int pdf, int font, double fontsize, string title, int bookmark);
        cpdf_tableOfContents(pdf, font, fontsize, title, bookmark);
    }
    /* CHAPTER 7. Presentations */
    /* Not included in the library version. */

    /* CHAPTER 8. Logos, Watermarks and Stamps */

    public static void netcpdf_stampOn(int stamp_pdf, int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_stampOn(int stamp_pdf, int pdf, int range);
        cpdf_stampOn(stamp_pdf, pdf, range);
    }

    public static void netcpdf_stampUnder(int stamp_pdf, int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_stampUnder(int stamp_pdf, int pdf, int range);
        cpdf_stampUnder(stamp_pdf, pdf, range);
    }

    public static void netcpdf_stampExtended(int pdf, int pdf2, int range, int isover, int scale_stamp_to_fit, netcpdf_position position, int relative_to_cropbox)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_stampExtended(int pdf, int pdf2, int range, int isover, int scale_stamp_to_fit, netcpdf_position position, int relative_to_cropbox);
        cpdf_stampExtended(pdf, pdf2, range, isover, scale_stamp_to_fit, position, relative_to_cropbox);
    }

    public static int netcpdf_combinePages(int under, int over)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_combinePages(int under, int over);
        return cpdf_combinePages(under, over);
    }

    public static void netcpdf_addText(int metrics, int pdf, int range, string text, netcpdf_position position, double linespacing, int bates, int font, double fontsize, double r, double g, double b, int underneath, int relative_to_cropbox, int outline, double opacity, int justification, int midline, int topline, string filename, double linewidth, int embed_fonts)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_addText(int metrics, int pdf, int range, string text, netcpdf_position position, double linespacing, int bates, int font, double fontsize, double r, double g, double b, int underneath, int relative_to_cropbox, int outline, double opacity, int justification, int midline, int topline, string filename, double linewidth, int embed_fonts);
        cpdf_addText(metrics, pdf, range, text, position, linespacing, bates, font, fontsize, r, g, b, underneath, relative_to_cropbox, outline, opacity, justification, midline, topline, filename, linewidth, embed_fonts);
    }

    public static void netcpdf_addTextSimple(int pdf, int range, string text, netcpdf_position position, int font, double fontsize)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_addTextSimple(int pdf, int range, string text, netcpdf_position position, int font, double fontsize);
        cpdf_addTextSimple(pdf, range, text, position, font, fontsize);
    }

    public static void netcpdf_removeText(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeText(int pdf, int range);
        cpdf_removeText(pdf, range);
    }

    public static int netcpdf_textWidth(int font, string text)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_textWidth(int font, string text);
        return cpdf_textWidth(font, text);
    }

    public static void netcpdf_addContent(string content, int before, int range, int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_addContent(string content, int before, int range, int pdf);
        cpdf_addContent(content, before, range, pdf);
    }

    public static string netcpdf_stampAsXObject(int pdf, int range, int stamp_pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_stampAsXObject(int pdf, int range, int stamp_pdf);
        return Marshal.PtrToStringAuto(cpdf_stampAsXObject(pdf, range, stamp_pdf));
    }

    /* CHAPTER 9. Multipage facilities */
    static public void netcpdf_impose(int pdf, double x, double y, int fit, int columns, int rtl, int btt, int center, double margin, double spacing, double linewidth)
    {
      [DllImport("libcpdf.so")] static extern void cpdf_impose(int pdf, double x, double y, int fit, int columns, int rtl, int btt, int center, double margin, double spacing, double linewidth);
      cpdf_impose(pdf, x, y, fit, columns, rtl, btt, center, margin, spacing, linewidth);
    }

    static public void netcpdf_twoUp(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_twoUp(int pdf);
        cpdf_twoUp(pdf);
    }

    static public void netcpdf_twoUpStack(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_twoUpStack(int pdf);
        cpdf_twoUpStack(pdf);
    }

    static public void netcpdf_padBefore(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_padBefore(int pdf, int range);
        cpdf_padBefore(pdf, range);
    }

    static public void netcpdf_padAfter(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_padAfter(int pdf, int range);
        cpdf_padAfter(pdf, range);
    }

    static public void netcpdf_padEvery(int pdf, int n)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_padEvery(int pdf, int n);
        cpdf_padEvery(pdf, n);
    }

    static public void netcpdf_padMultiple(int pdf, int n)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_padMultiple(int pdf, int n);
        cpdf_padMultiple(pdf, n);
    }

    static public void netcpdf_padMultipleBefore(int pdf, int n)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_padMultipleBefore(int pdf, int n);
        cpdf_padMultipleBefore(pdf, n);
    }

    /* CHAPTER 10. Annotations */
    /* Not in the library version */

    /* CHAPTER 11. Document Information and Metadata */

    public static int netcpdf_isLinearized(string filename)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_isLinearized(string filename);
        return cpdf_isLinearized(filename);
    }

    public static int netcpdf_getVersion(int pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getVersion(int pdf);
        return cpdf_getVersion(pdf);
    }

    public static int netcpdf_getMajorVersion(int pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getMajorVersion(int pdf);
        return cpdf_getMajorVersion(pdf);
    }

    public static string netcpdf_getTitle(int pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getTitle(int pdf);
        return Marshal.PtrToStringAuto(cpdf_getTitle(pdf));
    }

    public static string netcpdf_getAuthor(int pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getAuthor(int pdf);
        return Marshal.PtrToStringAuto(cpdf_getAuthor(pdf));
    }

    public static string netcpdf_getSubject (int pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getSubject(int pdf);
        return Marshal.PtrToStringAuto(cpdf_getSubject(pdf));
    }

    public static string netcpdf_getKeywords(int pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getKeywords(int pdf);
        return Marshal.PtrToStringAuto(cpdf_getKeywords(pdf));
    }

    public static string netcpdf_getCreator(int pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getCreator(int pdf);
        return Marshal.PtrToStringAuto(cpdf_getCreator(pdf));
    }

    public static string netcpdf_getProducer(int pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getProducer(int pdf);
        return Marshal.PtrToStringAuto(cpdf_getProducer(pdf));
    }

    public static string netcpdf_getCreationDate(int pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getCreationDate(int pdf);
        return Marshal.PtrToStringAuto(cpdf_getCreationDate(pdf));
    }

    public static string netcpdf_getModificationDate(int pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getModificationDate(int pdf);
        return Marshal.PtrToStringAuto(cpdf_getModificationDate(pdf));
    }

    public static string netcpdf_getTitleXMP(int pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getTitleXMP(int pdf);
        return Marshal.PtrToStringAuto(cpdf_getTitleXMP(pdf));
    }

    public static string netcpdf_getAuthorXMP(int pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getAuthorXMP(int pdf);
        return Marshal.PtrToStringAuto(cpdf_getAuthorXMP(pdf));
    }

    public static string netcpdf_getSubjectXMP (int pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getSubjectXMP(int pdf);
        return Marshal.PtrToStringAuto(cpdf_getSubjectXMP(pdf));
    }

    public static string netcpdf_getKeywordsXMP(int pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getKeywordsXMP(int pdf);
        return Marshal.PtrToStringAuto(cpdf_getKeywordsXMP(pdf));
    }

    public static string netcpdf_getCreatorXMP(int pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getCreatorXMP(int pdf);
        return Marshal.PtrToStringAuto(cpdf_getCreatorXMP(pdf));
    }

    public static string netcpdf_getProducerXMP(int pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getProducerXMP(int pdf);
        return Marshal.PtrToStringAuto(cpdf_getProducerXMP(pdf));
    }

    public static string netcpdf_getCreationDateXMP(int pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getCreationDateXMP(int pdf);
        return Marshal.PtrToStringAuto(cpdf_getCreationDateXMP(pdf));
    }

    public static string netcpdf_getModificationDateXMP(int pdf)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getModificationDateXMP(int pdf);
        return Marshal.PtrToStringAuto(cpdf_getModificationDateXMP(pdf));
    }

    public static void netcpdf_setTitle(int pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setTitle(int pdf, string s);
        cpdf_setTitle(pdf, s);
    }

    public static void netcpdf_setAuthor(int pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setAuthor(int pdf, string s);
        cpdf_setAuthor(pdf, s);
    }

    public static void netcpdf_setSubject(int pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setSubject(int pdf, string s);
        cpdf_setSubject(pdf, s);
    }

    public static void netcpdf_setKeywords(int pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setKeywords(int pdf, string s);
        cpdf_setKeywords(pdf, s);
    }

    public static void netcpdf_setCreator(int pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setCreator(int pdf, string s);
        cpdf_setCreator(pdf, s);
    }

    public static void netcpdf_setProducer(int pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setProducer(int pdf, string s);
        cpdf_setProducer(pdf, s);
    }

    public static void netcpdf_setCreationDate(int pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setCreationDate(int pdf, string s);
        cpdf_setCreationDate(pdf, s);
    }

    public static void netcpdf_setModificationDate(int pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setModificationDate(int pdf, string s);
        cpdf_setModificationDate(pdf, s);
    }

    public static void netcpdf_setTitleXMP(int pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setTitleXMP(int pdf, string s);
        cpdf_setTitleXMP(pdf, s);
    }

    public static void netcpdf_setAuthorXMP(int pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setAuthorXMP(int pdf, string s);
        cpdf_setAuthorXMP(pdf, s);
    }

    public static void netcpdf_setSubjectXMP(int pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setSubjectXMP(int pdf, string s);
        cpdf_setSubjectXMP(pdf, s);
    }

    public static void netcpdf_setKeywordsXMP(int pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setKeywordsXMP(int pdf, string s);
        cpdf_setKeywordsXMP(pdf, s);
    }

    public static void netcpdf_setCreatorXMP(int pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setCreatorXMP(int pdf, string s);
        cpdf_setCreatorXMP(pdf, s);
    }

    public static void netcpdf_setProducerXMP(int pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setProducerXMP(int pdf, string s);
        cpdf_setProducerXMP(pdf, s);
    }

    public static void netcpdf_setCreationDateXMP(int pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setCreationDateXMP(int pdf, string s);
        cpdf_setCreationDateXMP(pdf, s);
    }

    public static void netcpdf_setModificationDateXMP(int pdf, string s)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setModificationDateXMP(int pdf, string s);
        cpdf_setModificationDateXMP(pdf, s);
    }

    public static void netcpdf_getDateComponents(string datestring, ref int year, ref int month, ref int day, ref int hour, ref int minute, ref int second, ref int hour_offset, ref int minute_offset)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getDateComponents(string datestring, ref int year, ref int month, ref int day, ref int hour, ref int minute, ref int second, ref int hour_offset, ref int minute_offset);
        cpdf_getDateComponents(datestring, ref year, ref month, ref day, ref hour, ref minute, ref second, ref hour_offset, ref minute_offset);
    }

    public static string netcpdf_dateStringOfComponents(int y, int m, int d, int h, int min, int sec, int hour_offset, int minute_offset)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_dateStringOfComponents(int y, int m, int d, int h, int min, int sec, int hour_offset, int minute_offset);
        return Marshal.PtrToStringAuto(cpdf_dateStringOfComponents(y, m, d, h, min, sec, hour_offset, minute_offset));
    }

    public static int netcpdf_getPageRotation(int pdf, int pagenumber)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getPageRotation(int pdf, int pagenumber);
        return cpdf_getPageRotation(pdf, pagenumber);
    }

    public static int netcpdf_hasBox(int pdf, int pagenumber, string boxname)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_hasBox(int pdf, int pagenumber, string boxname);
        return cpdf_hasBox(pdf, pagenumber, boxname);
    }

    public static void netcpdf_getMediaBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getMediaBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy);
        cpdf_getMediaBox(pdf, pagenumber, ref minx, ref maxx, ref miny, ref maxy);
    }

    public static void netcpdf_getCropBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getCropBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy);
        cpdf_getCropBox(pdf, pagenumber, ref minx, ref maxx, ref miny, ref maxy);
    }

    public static void netcpdf_getTrimBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getTrimBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy);
        cpdf_getTrimBox(pdf, pagenumber, ref minx, ref maxx, ref miny, ref maxy);
    }

    public static void netcpdf_getArtBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getArtBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy);
        cpdf_getArtBox(pdf, pagenumber, ref minx, ref maxx, ref miny, ref maxy);
    }

    public static void netcpdf_getBleedBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_getBleedBox(int pdf, int pagenumber, ref double minx, ref double maxx, ref double miny, ref double maxy);
        cpdf_getBleedBox(pdf, pagenumber, ref minx, ref maxx, ref miny, ref maxy);
    }

    public static void netcpdf_setMediabox(int pdf, int r, double minx, double maxx, double miny, double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setMediabox(int pdf, int r, double minx, double maxx, double miny, double maxy);
        cpdf_setMediabox(pdf, r, minx, maxx, miny, maxy);
    }

    public static void netcpdf_setCropBox(int pdf, int r, double minx, double maxx, double miny, double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setCropBox(int pdf, int r, double minx, double maxx, double miny, double maxy);
        cpdf_setCropBox(pdf, r, minx, maxx, miny, maxy);
    }

    public static void netcpdf_setTrimBox(int pdf, int r, double minx, double maxx, double miny, double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setTrimBox(int pdf, int r, double minx, double maxx, double miny, double maxy);
        cpdf_setTrimBox(pdf, r, minx, maxx, miny, maxy);
    }

    public static void netcpdf_setArtBox(int pdf, int r, double minx, double maxx, double miny, double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setArtBox(int pdf, int r, double minx, double maxx, double miny, double maxy);
        cpdf_setArtBox(pdf, r, minx, maxx, miny, maxy);
    }

    public static void netcpdf_setBleedBox(int pdf, int r, double minx, double maxx, double miny, double maxy)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setBleedBox(int pdf, int r, double minx, double maxx, double miny, double maxy);
        cpdf_setBleedBox(pdf, r, minx, maxx, miny, maxy);
    }

    public static void netcpdf_markTrapped(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_markTrapped(int pdf);
        cpdf_markTrapped(pdf);
    }

    public static void netcpdf_markUntrapped(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_markUntrapped(int pdf);
        cpdf_markUntrapped(pdf);
    }

    public static void netcpdf_markTrappedXMP(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_markTrappedXMP(int pdf);
        cpdf_markTrappedXMP(pdf);
    }

    public static void netcpdf_markUntrappedXMP(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_markUntrappedXMP(int pdf);
        cpdf_markUntrappedXMP(pdf);
    }

    public static void netcpdf_setPageLayout(int pdf, int layout)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setPageLayout(int pdf, int layout);
        cpdf_setPageLayout(pdf, layout);
    }

    public static void netcpdf_setPageMode(int pdf, int mode)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setPageMode(int pdf, int mode);
        cpdf_setPageMode(pdf, mode);
    }

    public static void netcpdf_hideToolbar(int pdf, int flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_hideToolbar(int pdf, int flag);
        cpdf_hideToolbar(pdf, flag);
    }

    public static void netcpdf_hideMenubar(int pdf, int flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_hideMenubar(int pdf, int flag);
        cpdf_hideMenubar(pdf, flag);
    }

    public static void netcpdf_hideWindowUi(int pdf, int flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_hideWindowUi(int pdf, int flag);
        cpdf_hideWindowUi(pdf, flag);
    }

    public static void netcpdf_fitWindow(int pdf, int flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_fitWindow(int pdf, int flag);
        cpdf_fitWindow(pdf, flag);
    }

    public static void netcpdf_centerWindow(int pdf, int flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_centerWindow(int pdf, int flag);
        cpdf_centerWindow(pdf, flag);
    }

    public static void netcpdf_displayDocTitle(int pdf, int flag)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_displayDocTitle(int pdf, int flag);
        cpdf_displayDocTitle(pdf, flag);
    }

    public static void netcpdf_openAtPage(int pdf, int fit, int pagenumber)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_openAtPage(int pdf, int fit, int pagenumber);
        cpdf_openAtPage(pdf, fit, pagenumber);
    }

    public static void netcpdf_setMetadataFromFile(int pdf, string filename)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setMetadataFromFile(int pdf, string filename);
        cpdf_setMetadataFromFile(pdf, filename);
    }

    //FIXME setMetadataFromByteArray
    //FIXME getMetadata

    public static void netcpdf_removeMetadata(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeMetadata(int pdf);
        cpdf_removeMetadata(pdf);
    }

    public static void netcpdf_createMetadata(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_createMetadata(int pdf);
        cpdf_createMetadata(pdf);
    }

    public static void netcpdf_setMetadataDate(int pdf, string date)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setMetadataDate(int pdf, string date);
        cpdf_setMetadataDate(pdf, date);
    }

    public static void netcpdf_addPageLabels(int pdf, int style, string prefix, int offset, int range, int progress)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_addPageLabels(int pdf, int style, string prefix, int offset, int range, int progress);
        cpdf_addPageLabels(pdf, style, prefix, offset, range, progress);
    }

    public static void netcpdf_removePageLabels(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removePageLabels(int pdf);
        cpdf_removePageLabels(pdf);
    }

    public static string netcpdf_getPageLabelStringForPage(int pdf, int pagenumber)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getPageLabelStringForPage(int pdf, int pagenumber);
        return Marshal.PtrToStringAuto(cpdf_getPageLabelStringForPage(pdf, pagenumber));
    }

    public static int netcpdf_startGetPageLabels(int pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_startGetPageLabels(int pdf);
        return cpdf_startGetPageLabels(pdf);
    }

    public static int netcpdf_getPageLabelStyle(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getPageLabelStyle(int n);
        return cpdf_getPageLabelStyle(n);
    }

    public static string netcpdf_getPageLabelPrefix(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getPageLabelPrefix(int n);
        return Marshal.PtrToStringAuto(cpdf_getPageLabelPrefix(n));
    }

    public static int netcpdf_getPageLabelOffset(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getPageLabelOffset(int n);
        return cpdf_getPageLabelOffset(n);
    }

    public static int netcpdf_getPageLabelRange(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getPageLabelRange(int n);
        return cpdf_getPageLabelRange(n);
    }

    public static void netcpdf_endGetPageLabels()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endGetPageLabels();
        cpdf_endGetPageLabels();
    }

    /* CHAPTER 12. File Attachments */

    public static void netcpdf_attachFile(string filename, int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_attachFile(string filename, int pdf);
        cpdf_attachFile(filename, pdf);
    }

    public static void netcpdf_attachFileToPage(string filename, int pdf, int pagenumber)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_attachFileToPage(string filename, int pdf, int pagenumber);
        cpdf_attachFileToPage(filename, pdf, pagenumber);
    }

    //FIXME cpdf_attachFileFromMemory / cpdf_attachFileToPageFromMemory

    public static void netcpdf_removeAttachedFiles(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeAttachedFiles(int pdf);
        cpdf_removeAttachedFiles(pdf);
    }

    public static void netcpdf_startGetAttachments(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_startGetAttachments(int pdf);
        cpdf_startGetAttachments(pdf);
    }

    public static int netcpdf_numberGetAttachments()
    {
        [DllImport("libcpdf.so")] static extern int cpdf_numberGetAttachments();
        return cpdf_numberGetAttachments();
    }

    public static string netcpdf_getAttachmentName(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getAttachmentName(int n);
        return Marshal.PtrToStringAuto(cpdf_getAttachmentName(n));
    }

    public static int netcpdf_getAttachmentPage(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getAttachmentPage(int n);
        return cpdf_getAttachmentPage(n);
    }

    //FIXME free()
    public static byte[] netcpdf_getAttachmentData(int serial)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getAttachmentData(int serial, ref int len);
        int len = 0;
        IntPtr data = cpdf_getAttachmentData(serial, ref len);
        var databytes = new byte[len];
        Marshal.Copy(data, databytes, 0, len);
        return databytes;
    }

    public static void netcpdf_endGetAttachments()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endGetAttachments();
        cpdf_endGetAttachments();
    }

    /* CHAPTER 13. Images. */

    public static int netcpdf_startGetImageResolution(int pdf, double min_required_resolution)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_startGetImageResolution(int pdf, double min_required_resolution);
        return cpdf_startGetImageResolution(pdf, min_required_resolution);
    }

    public static int netcpdf_getImageResolutionPageNumber(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getImageResolutionPageNumber(int n);
        return cpdf_getImageResolutionPageNumber(n);
    }

    public static string netcpdf_getImageResolutionImageName(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getImageResolutionImageName(int n);
        return Marshal.PtrToStringAuto(cpdf_getImageResolutionImageName(n));
    }

    public static int netcpdf_getImageResolutionXPixels(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getImageResolutionXPixels(int n);
        return cpdf_getImageResolutionXPixels(n);
    }

    public static int netcpdf_getImageResolutionYPixels(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getImageResolutionYPixels(int n);
        return cpdf_getImageResolutionYPixels(n);
    }

    public static double netcpdf_getImageResolutionXRes(int n)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_getImageResolutionXRes(int n);
        return cpdf_getImageResolutionXRes(n);
    }

    public static double netcpdf_getImageResolutionYRes(int n)
    {
        [DllImport("libcpdf.so")] static extern double cpdf_getImageResolutionYRes(int n);
        return cpdf_getImageResolutionYRes(n);
    }

    public static void netcpdf_endGetImageResolution()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endGetImageResolution();
        cpdf_endGetImageResolution();
    }

    /* CHAPTER 14. Fonts. */

    public static void netcpdf_startGetFontInfo(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_startGetFontInfo(int pdf);
        cpdf_startGetFontInfo(pdf);
    }

    public static int netcpdf_numberFonts()
    {
        [DllImport("libcpdf.so")] static extern int cpdf_numberFonts();
        return cpdf_numberFonts();
    }

    public static int netcpdf_getFontPage(int n)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_getFontPage(int n);
        return cpdf_getFontPage(n);
    }

    public static string netcpdf_getFontName(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getFontName(int n);
        return Marshal.PtrToStringAuto(cpdf_getFontName(n));
    }

    public static string netcpdf_getFontType(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getFontType(int n);
        return Marshal.PtrToStringAuto(cpdf_getFontType(n));
    }

    public static string netcpdf_getFontEncoding(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getFontEncoding(int n);
        return Marshal.PtrToStringAuto(cpdf_getFontEncoding(n));
    }

    public static void netcpdf_endGetFontInfo()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endGetFontInfo();
        cpdf_endGetFontInfo();
    }

    public static void netcpdf_removeFonts(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeFonts(int pdf);
        cpdf_removeFonts(pdf);
    }

    public static void netcpdf_copyFont(int docfrom, int docto, int range, int pagenumber, string fontname)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_copyFont(int docfrom, int docto, int range, int pagenumber, string fontname);
        cpdf_copyFont(docfrom, docto, range, pagenumber, fontname);
    }

    /* CHAPTER 15. PDF and JSON */
    public static void netcpdf_outputJSON(string filename, int parse_content, int no_stream_data, int decompress_streams, int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_outputJSON(string filename, int parse_content, int no_stream_data, int decompress_streams, int pdf);
        cpdf_outputJSON(filename, parse_content, no_stream_data, decompress_streams, pdf);
    }

    public static int netcpdf_fromJSON(string filename)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_fromJSON(string filename);
        return cpdf_fromJSON(filename);
    }

    /* CHAPTER 16. Optional Content Groups */
    public static int netcpdf_startGetOCGList(int pdf)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_startGetOCGList(int pdf);
        return cpdf_startGetOCGList(pdf);
    }

    public static string netcpdf_OCGListEntry(int n)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_OCGListEntry(int n);
        return Marshal.PtrToStringAuto(cpdf_OCGListEntry(n));
    }

    public static void netcpdf_endGetOCGList()
    {
        [DllImport("libcpdf.so")] static extern void cpdf_endGetOCGList();
        cpdf_endGetOCGList();
    }

    public static void netcpdf_OCGRename(int pdf, string name_from, string name_to)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_OCGRename(int pdf, string name_from, string name_to);
        cpdf_OCGRename(pdf, name_from, name_to);
    }

    public static void netcpdf_OCGOrderAll(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_OCGOrderAll(int pdf);
        cpdf_OCGOrderAll(pdf);
    }

    public static void netcpdf_OCGCoalesce(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_OCGCoalesce(int pdf);
        cpdf_OCGCoalesce(pdf);
    }


    /* CHAPTER 17. Creating New PDFs */
    public static int netcpdf_blankDocument(double w, double h, int pages)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_blankDocument(double w, double h, int pages);
        return cpdf_blankDocument(w, h, pages);
    }

    public static int netcpdf_blankDocumentPaper(int papersize, int pages)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_blankDocumentPaper(int papersize, int pages);
        return cpdf_blankDocumentPaper(papersize, pages);
    }

    public static int netcpdf_textToPDF(double w, double h, int font, double fontsize, string filename)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_textToPDF(double w, double h, int font, double fontsize, string filename);
        return cpdf_textToPDF(w, h, font, fontsize, filename);
    }

    public static int netcpdf_textToPDFPaper(int papersize, int font, double fontsize, string filename)
    {
        [DllImport("libcpdf.so")] static extern int cpdf_textToPDFPaper(int papersize, int font, double fontsize, string filename);
        return cpdf_textToPDFPaper(papersize, font, fontsize, filename);
    }


    /* CHAPTER 18. Miscellaneous */
    public static void netcpdf_draft(int pdf, int range, int boxes)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_draft(int pdf, int range, int boxes);
        cpdf_draft(pdf, range, boxes);
    }

    public static void netcpdf_removeAllText(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeAllText(int pdf, int range);
        cpdf_removeAllText(pdf, range);
    }

    public static void netcpdf_blackText(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_blackText(int pdf, int range);
        cpdf_blackText(pdf, range);
    }

    public static void netcpdf_blackLines(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_blackLines(int pdf, int range);
        cpdf_blackLines(pdf, range);
    }

    public static void netcpdf_blackFills(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_blackFills(int pdf, int range);
        cpdf_blackFills(pdf, range);
    }

    public static void netcpdf_thinLines(int pdf, int range, double min_thickness)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_thinLines(int pdf, int range, double min_thickness);
        cpdf_thinLines(pdf, range, min_thickness);
    }

    public static void netcpdf_copyId(int pdf_from, int pdf_to)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_copyId(int pdf_from, int pdf_to);
        cpdf_copyId(pdf_from, pdf_to);
    }

    public static void netcpdf_removeId(int pdf)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeId(int pdf);
        cpdf_removeId(pdf);
    }

    public static void netcpdf_setVersion(int pdf, int version)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setVersion(int pdf, int version);
        cpdf_setVersion(pdf, version);
    }

    public static void netcpdf_setFullVersion(int pdf, int major, int minor)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_setFullVersion(int pdf, int major, int minor);
        cpdf_setFullVersion(pdf, major, minor);
    }

    public static void netcpdf_removeDictEntry(int pdf, string key)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeDictEntry(int pdf, string key);
        cpdf_removeDictEntry(pdf, key);
    }

    public static void netcpdf_removeDictEntrySearch(int pdf, string key, string searchterm)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeDictEntrySearch(int pdf, string key, string searchterm);
        cpdf_removeDictEntrySearch(pdf, key, searchterm);
    }

    public static void netcpdf_replaceDictEntry(int pdf, string key, string newvalue)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_replaceDictEntry(int pdf, string key, string newvalue);
        cpdf_replaceDictEntry(pdf, key, newvalue);
    }

    public static void netcpdf_replaceDictEntrySearch(int pdf, string key, string newvalue, string searchterm)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_replaceDictEntrySearch(int pdf, string key, string newvalue, string searchterm);
        cpdf_replaceDictEntrySearch(pdf, key, newvalue, searchterm);
    }

    public static void netcpdf_removeClipping(int pdf, int range)
    {
        [DllImport("libcpdf.so")] static extern void cpdf_removeClipping(int pdf, int range);
        cpdf_removeClipping(pdf, range);
    }

    //FIXME NEED TO ADD A FREEING FUNCTION HERE
    public static byte[] netcpdf_getDictEntries(int pdf, string key)
    {
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getDictEntries(int pdf, string key, ref int retlen);
        int len = 0;
        IntPtr data = cpdf_getDictEntries(pdf, key, ref len);
        var databytes = new byte[len];
        Marshal.Copy(data, databytes, 0, len);
        return databytes;
    }

    static void Main(string[] args)
    {

        /* CHAPTER 0. Preliminaries */
        Console.WriteLine("***** CHAPTER 0. Preliminaries");
        Console.WriteLine("---cpdf_startup()");
        netcpdf_startup();
        Console.WriteLine("---cpdf_version()");
        Console.WriteLine("version = {0}", netcpdf_version());
        Console.WriteLine("---cpdf_setFast()");
        netcpdf_setFast();
        Console.WriteLine("---cpdf_setSlow()");
        netcpdf_setSlow();
        Console.WriteLine("---cpdf_clearError()");
        netcpdf_clearError();

        /* CHAPTER 1. Basics */
        Console.WriteLine("***** CHAPTER 1. Basics");
        Console.WriteLine("---cpdf_fromFile()");
        int pdf = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_fromFileLazy()");
        int pdf2 = netcpdf_fromFileLazy("testinputs/cpdflibmanual.pdf", "");
        //FIXME fromMemory
        //FIXME fromMemoryLazy
        int pdf3 = netcpdf_blankDocument(153.5, 234.2, 50);
        int pdf4 = netcpdf_blankDocumentPaper(netcpdf_a4landscape, 50);
        netcpdf_deletePdf(pdf);
        netcpdf_replacePdf(pdf3, pdf4);
        Console.WriteLine("---cpdf: enumerate PDFs");
        int n = netcpdf_startEnumeratePDFs();
        for (int x = 0; x < n; x++)
        {
            int key = netcpdf_enumeratePDFsKey(x);
            string info = netcpdf_enumeratePDFsInfo(x);
        }
        netcpdf_endEnumeratePDFs();
        Console.WriteLine("---cpdf_ptOfIn()");
        Console.WriteLine($"One inch is {netcpdf_ptOfIn(1.0):0.000000} points");
        Console.WriteLine("---cpdf_ptOfCm()");
        Console.WriteLine($"One centimetre is {netcpdf_ptOfCm(1.0):0.000000} points");
        Console.WriteLine("---cpdf_ptOfMm()");
        Console.WriteLine($"One millimetre is {netcpdf_ptOfMm(1.0):0.000000} points");
        Console.WriteLine("---cpdf_inOfPt()");
        Console.WriteLine($"One point is {netcpdf_inOfPt(1.0):0.000000} inches");
        Console.WriteLine("---cpdf_cmOfPt()");
        Console.WriteLine($"One point is {netcpdf_cmOfPt(1.0):0.000000} centimetres");
        Console.WriteLine("---cpdf_mmOfPt()");
        Console.WriteLine($"One point is {netcpdf_mmOfPt(1.0):0.000000} millimetres");
        Console.WriteLine("---cpdf_range()");
        int range = netcpdf_range(1, 10);
        Console.WriteLine("---cpdf_all()");
        int all = netcpdf_all(pdf3);
        Console.WriteLine("---cpdf_even()");
        int even = netcpdf_even(all);
        Console.WriteLine("---cpdf_odd()");
        int odd = netcpdf_odd(all);
        Console.WriteLine("---cpdf_rangeUnion()");
        int union = netcpdf_rangeUnion(even, odd);
        Console.WriteLine("---cpdf_difference()");
        int diff = netcpdf_difference(even, odd);
        Console.WriteLine("---cpdf_removeDuplicates()");
        int revdup = netcpdf_removeDuplicates(even);
        Console.WriteLine("---cpdf_rangeLength()");
        int length = netcpdf_rangeLength(even);
        Console.WriteLine("---cpdf_rangeGet()");
        int rangeget = netcpdf_rangeGet(even, 1);
        Console.WriteLine("---cpdf_rangeAdd()");
        int rangeadd = netcpdf_rangeAdd(even, 20);
        Console.WriteLine("---cpdf_isInRange()");
        int isin = netcpdf_isInRange(even, 2);
        Console.WriteLine("---cpdf_parsePagespec()");
        int r = netcpdf_parsePagespec(pdf3, "1-5");
        Console.WriteLine("---cpdf_validatePagespec()");
        int valid = netcpdf_validatePagespec("1-4,5,6");
        Console.WriteLine($"Validating pagespec gives {valid}");
        Console.WriteLine("---cpdf_stringOfPagespec()");
        string ps = netcpdf_stringOfPagespec(pdf3, r);
        Console.WriteLine($"String of pagespec is {ps}");
        Console.WriteLine("---cpdf_blankRange()");
        int b = netcpdf_blankRange();

        netcpdf_deleteRange(b);

        int pdf10 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_pages()");
        int pages = netcpdf_pages(pdf10);
        Console.WriteLine($"Pages = {pages}");
        Console.WriteLine("---cpdf_pagesFast()");
        int pagesfast = netcpdf_pagesFast("", "testinputs/cpdflibmanual.pdf");
        Console.WriteLine($"Pages = {pages}");
        Console.WriteLine("---cpdf_toFile()");
        netcpdf_toFile(pdf10, "testoutputs/01tofile.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_toFileExt()");
        netcpdf_toFileExt(pdf10, "testoutputs/01tofileext.pdf", netcpdf_false, netcpdf_true, netcpdf_true, netcpdf_true, netcpdf_true);
        Console.WriteLine("---cpdf_isEncrypted()");
        int isenc = netcpdf_isEncrypted(pdf10);
        Console.WriteLine($"isencrypted:{isenc}");
        Console.WriteLine("---cpdf_isLinearized()");
        int lin = netcpdf_isLinearized("testinputs/cpdfmanual.pdf");
        Console.WriteLine($"islinearized:{lin}");

        int pdf400 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pdf401 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int[] permissions = new [] {netcpdf_noEdit};
        Console.WriteLine("---cpdf_toFileEncrypted()");
        netcpdf_toFileEncrypted(pdf400, netcpdf_pdf40bit, permissions, permissions.Length, "owner", "user", netcpdf_false, netcpdf_false, "testoutputs/01encrypted.pdf");
        Console.WriteLine("---cpdf_toFileEncryptedExt()");
        netcpdf_toFileEncryptedExt(pdf401, netcpdf_pdf40bit, permissions, permissions.Length, "owner", "user", netcpdf_false, netcpdf_false, netcpdf_true, netcpdf_true, netcpdf_true, "testoutputs/01encryptedext.pdf");
        Console.WriteLine("---cpdf_hasPermission()");
        int pdfenc = netcpdf_fromFile("testoutputs/01encrypted.pdf", "user");
        int hasnoedit = netcpdf_hasPermission(pdfenc, netcpdf_noEdit);
        int hasnocopy = netcpdf_hasPermission(pdfenc, netcpdf_noCopy);
        Console.WriteLine($"Haspermission {hasnoedit}, {hasnocopy}");
        Console.WriteLine("---cpdf_encryptionKind()");
        int enckind = netcpdf_encryptionKind(pdfenc);
        Console.WriteLine($"encryption kind is {enckind}");
        Console.WriteLine("---cpdf_decryptPdf()");
        netcpdf_decryptPdf(pdf10, "");
        Console.WriteLine("---cpdf_decryptPdfOwner()");
        netcpdf_decryptPdfOwner(pdf10, "");

        /* CHAPTER 2. Merging and Splitting */
        Console.WriteLine("***** CHAPTER 2. Merging and Splitting");
        int pdf11 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int selectrange = netcpdf_range(1, 3);
        Console.WriteLine("---cpdf_mergeSimple()");
        int[] arr = new [] {pdf11, pdf11, pdf11};
        int merged = netcpdf_mergeSimple(arr, arr.Length);
        netcpdf_toFile(merged, "testoutputs/02merged.pdf", netcpdf_false, netcpdf_true);
        Console.WriteLine("---cpdf_merge()");
        int merged2 = netcpdf_merge(arr, arr.Length, netcpdf_false, netcpdf_false);
        netcpdf_toFile(merged2, "testoutputs/02merged2.pdf", netcpdf_false, netcpdf_true);
        Console.WriteLine("---cpdf_mergeSame()");
        int[] ranges = new [] {netcpdf_all(pdf11), netcpdf_all(pdf11), netcpdf_all(pdf11)};
        int merged3 = netcpdf_mergeSame(arr, arr.Length, netcpdf_false, netcpdf_false, ranges);
        netcpdf_toFile(merged3, "testoutputs/02merged3.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_selectPages()");
        int pdf12 = netcpdf_selectPages(pdf11, selectrange);
        netcpdf_toFile(pdf12, "testoutputs/02selected.pdf", netcpdf_false, netcpdf_false);

        /* CHAPTER 3. Pages */
        Console.WriteLine("***** CHAPTER 3. Pages");
        int pagespdf1 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf2 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf3 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf4 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf5 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf6 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf7 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf8 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf9 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf10 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf11 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf12 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf13 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf14 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf15 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf16 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf17 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf18 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pagespdf19 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_scalePages()");
        netcpdf_scalePages(pagespdf1, netcpdf_all(pagespdf1), 1.5, 1.8);
        netcpdf_toFile(pagespdf1, "testoutputs/03scalepages.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_scaleToFit()");
        netcpdf_scaleToFit(pagespdf2, netcpdf_all(pagespdf2), 1.5, 1.8, 0.9);
        netcpdf_toFile(pagespdf2, "testoutputs/03scaletofit.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_scaleToFitPaper()");
        netcpdf_scaleToFitPaper(pagespdf3, netcpdf_all(pagespdf3), netcpdf_a4portrait, 0.8);
        netcpdf_toFile(pagespdf3, "testoutputs/03scaletofitpaper.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_scaleContents()");
        netcpdf_position position = new netcpdf_position (netcpdf_topLeft, 20.0, 20.0);
        netcpdf_scaleContents(pagespdf4, netcpdf_all(pagespdf4), position, 2.0);
        netcpdf_toFile(pagespdf4, "testoutputs/03scalecontents.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_shiftContents()");
        netcpdf_shiftContents(pagespdf5, netcpdf_all(pagespdf5), 1.5, 1.25);
        netcpdf_toFile(pagespdf5, "testoutputs/03shiftcontents.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_rotate()");
        netcpdf_rotate(pagespdf6, netcpdf_all(pagespdf6), 90);
        netcpdf_toFile(pagespdf6, "testoutputs/03rotate.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_rotateBy()");
        netcpdf_rotateBy(pagespdf7, netcpdf_all(pagespdf7), 90);
        netcpdf_toFile(pagespdf7, "testoutputs/03rotateby.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_rotateContents()");
        netcpdf_rotateContents(pagespdf8, netcpdf_all(pagespdf8), 35.0);
        netcpdf_toFile(pagespdf8, "testoutputs/03rotatecontents.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_upright()");
        netcpdf_upright(pagespdf9, netcpdf_all(pagespdf9));
        netcpdf_toFile(pagespdf9, "testoutputs/03upright.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_hFlip()");
        netcpdf_hFlip(pagespdf10, netcpdf_all(pagespdf10));
        netcpdf_toFile(pagespdf10, "testoutputs/03hflip.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_vFlip()");
        netcpdf_vFlip(pagespdf11, netcpdf_all(pagespdf11));
        netcpdf_toFile(pagespdf11, "testoutputs/03vflip.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_crop()");
        netcpdf_crop(pagespdf12, netcpdf_all(pagespdf12), 0.0, 0.0, 400.0, 500.0);
        netcpdf_toFile(pagespdf12, "testoutputs/03crop.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_trimMarks()");
        netcpdf_trimMarks(pagespdf13, netcpdf_all(pagespdf13));
        netcpdf_toFile(pagespdf13, "testoutputs/03trim_marks.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_showBoxes()");
        netcpdf_showBoxes(pagespdf14, netcpdf_all(pagespdf14));
        netcpdf_toFile(pagespdf14, "testoutputs/03show_boxes.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_hardBox()");
        netcpdf_hardBox(pagespdf15, netcpdf_all(pagespdf15), "/MediaBox");
        netcpdf_toFile(pagespdf15, "testoutputs/03hard_box.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_removeCrop()");
        netcpdf_removeCrop(pagespdf16, netcpdf_all(pagespdf16));
        netcpdf_toFile(pagespdf16, "testoutputs/03remove_crop.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_removeTrim()");
        netcpdf_removeTrim(pagespdf17, netcpdf_all(pagespdf17));
        netcpdf_toFile(pagespdf17, "testoutputs/03remove_trim.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_removeArt()");
        netcpdf_removeArt(pagespdf18, netcpdf_all(pagespdf18));
        netcpdf_toFile(pagespdf18, "testoutputs/03remove_art.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_removeBleed()");
        netcpdf_removeBleed(pagespdf19, netcpdf_all(pagespdf19));
        netcpdf_toFile(pagespdf19, "testoutputs/03remove_bleed.pdf", netcpdf_false, netcpdf_false);


        /* CHAPTER 4. Encryption */
        /* Encryption covered under Chapter 1 in cpdflib. */

        /* CHAPTER 5. Compression */
        Console.WriteLine("***** CHAPTER 5. Compression");
        int pdf16 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_compress()");
        netcpdf_compress(pdf16);
        netcpdf_toFile(pdf16, "testoutputs/05compressed.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_decompress()");
        netcpdf_decompress(pdf16);
        netcpdf_toFile(pdf16, "testoutputs/05decompressed.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_squeezeInMemory()");
        netcpdf_squeezeInMemory(pdf16);
        netcpdf_toFile(pdf16, "testoutputs/05squeezedinmemory.pdf", netcpdf_false, netcpdf_false);

        /* CHAPTER 6. Bookmarks */
        Console.WriteLine("***** CHAPTER 6. Bookmarks");
        int pdf17 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf: get bookmarks");
        netcpdf_startGetBookmarkInfo(pdf17);
        int nb = netcpdf_numberBookmarks();
        Console.WriteLine($"There are {nb} bookmarks");
        for (int b2 = 0; b2 < nb; b2++)
        {
            int level = netcpdf_getBookmarkLevel(b2);
            int page = netcpdf_getBookmarkPage(pdf17, b2);
            string text = netcpdf_getBookmarkText(b2);
            int open = netcpdf_getBookmarkOpenStatus(b2);
            Console.WriteLine($"Bookmark at level {level} points to page {page} and has text \"{text}\" and open {open}");
        }
        netcpdf_endGetBookmarkInfo();
        Console.WriteLine("---cpdf: set bookmarks");
        netcpdf_startSetBookmarkInfo(1);
        netcpdf_setBookmarkLevel(0, 0);
        netcpdf_setBookmarkPage(pdf17, 0, 1);
        netcpdf_setBookmarkOpenStatus(0, 0);
        netcpdf_setBookmarkText(0, "The text");
        netcpdf_endSetBookmarkInfo(pdf17);
        Console.WriteLine("---cpdf_tableOfContents()");
        int tocpdf = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        netcpdf_tableOfContents(tocpdf, netcpdf_timesRoman, 12.0, "Table of Contents", netcpdf_false);
        netcpdf_toFile(tocpdf, "testoutputs/06toc.pdf", netcpdf_false, netcpdf_false);

        /* CHAPTER 7. Presentations */
        /* Not included in the library version. */

        /* CHAPTER 8. Logos, Watermarks and Stamps */
        Console.WriteLine("***** CHAPTER 8. Logos, Watermarks and Stamps");
        int textfile = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_addText()");
        netcpdf_position pos = new netcpdf_position (netcpdf_topLeft, 20.0, 20.0);
        netcpdf_addText(netcpdf_false,
                        textfile,
                        netcpdf_all(textfile),
                        "Some Text~~~~~~~~~~!",
                        pos,
                        1.0,
                        1,
                        netcpdf_timesRoman,
                        20.0,
                        0.5,
                        0.5,
                        0.5,
                        netcpdf_false,
                        netcpdf_false,
                        netcpdf_true,
                        0.5,
                        netcpdf_leftJustify,
                        netcpdf_false,
                        netcpdf_false,
                        "",
                        1.0,
                        netcpdf_false);
        Console.WriteLine("---cpdf_addTextSimple()");
        netcpdf_addTextSimple(textfile, netcpdf_all(textfile), "The text!", pos, netcpdf_timesRoman, 50.0);
        netcpdf_toFile(textfile, "testoutputs/08added_text.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_removeText()");
        netcpdf_removeText(textfile, netcpdf_all(textfile));
        netcpdf_toFile(textfile, "testoutputs/08removed_text.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_textWidth()");
        int w = netcpdf_textWidth(netcpdf_timesRoman, "What is the width of this?");
        int stamp = netcpdf_fromFile("testinputs/logo.pdf", "");
        int stampee = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int stamp_range = netcpdf_all(stamp);
        Console.WriteLine("---cpdf_stampOn()");
        netcpdf_stampOn(stamp, stampee, stamp_range);
        Console.WriteLine("---cpdf_stampUnder()");
        netcpdf_stampUnder(stamp, stampee, stamp_range);
        netcpdf_position spos = new netcpdf_position (netcpdf_topLeft, 20.0, 20.0);
        Console.WriteLine("---cpdf_stampExtended()");
        netcpdf_stampExtended(stamp, stampee, stamp_range, netcpdf_true, netcpdf_true, spos, netcpdf_true);
        netcpdf_toFile(stamp, "testoutputs/08stamp_after.pdf", netcpdf_false, netcpdf_false);
        netcpdf_toFile(stampee, "testoutputs/08stampee_after.pdf", netcpdf_false, netcpdf_false);
        int c1 = netcpdf_fromFile("testinputs/logo.pdf", "");
        int c2 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_combinePages()");
        int c3 = netcpdf_combinePages(c1, c2);
        netcpdf_toFile(c3, "testoutputs/08c3after.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_stampAsXObject()"); 
        int undoc = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int ulogo = netcpdf_fromFile("testinputs/logo.pdf", "");
        string name = netcpdf_stampAsXObject(undoc, netcpdf_all(undoc), ulogo);

        /* CHAPTER 9. Multipage facilities */
        Console.WriteLine("***** CHAPTER 9. Multipage facilities");
        int mp = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_twoUp()");
        netcpdf_twoUp(mp);
        netcpdf_toFile(mp, "testoutputs/09mp.pdf", netcpdf_false, netcpdf_false);
        int mp2 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_twoUpStack()");
        netcpdf_twoUpStack(mp2);
        netcpdf_toFile(mp2, "testoutputs/09mp2.pdf", netcpdf_false, netcpdf_false);
        int mp25 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_impose()");
        netcpdf_impose(mp25, 5.0, 4.0, netcpdf_false, netcpdf_false, netcpdf_false, netcpdf_false, netcpdf_false, 40.0, 20.0, 2.0);
        netcpdf_toFile(mp25, "testoutputs/09mp25.pdf", netcpdf_false, netcpdf_false);
        int mp26 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        netcpdf_impose(mp26, 2000.0, 1000.0, netcpdf_true, netcpdf_false, netcpdf_false, netcpdf_false, netcpdf_false, 40.0, 20.0, 2.0);
        netcpdf_toFile(mp26, "testoutputs/09mp26.pdf", netcpdf_false, netcpdf_false);
        int mp3 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_padBefore()");
        netcpdf_padBefore(mp3, netcpdf_range(1, 10));
        netcpdf_toFile(mp3, "testoutputs/09mp3.pdf", netcpdf_false, netcpdf_false);
        int mp4 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_padAfter()");
        netcpdf_padAfter(mp4, netcpdf_range(1, 10));
        netcpdf_toFile(mp4, "testoutputs/09mp4.pdf", netcpdf_false, netcpdf_false);
        int mp5 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_padEvery()");
        netcpdf_padEvery(mp5, 5);
        netcpdf_toFile(mp5, "testoutputs/09mp5.pdf", netcpdf_false, netcpdf_false);
        int mp6 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_padMultiple()");
        netcpdf_padMultiple(mp6, 10);
        netcpdf_toFile(mp6, "testoutputs/09mp6.pdf", netcpdf_false, netcpdf_false);
        int mp7 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_padMultipleBefore()");
        netcpdf_padMultipleBefore(mp7, 23);
        netcpdf_toFile(mp7, "testoutputs/09mp7.pdf", netcpdf_false, netcpdf_false);

        /* CHAPTER 10. Annotations */
        /* Not in the library version */

        /* CHAPTER 11. Document Information and Metadata */
        Console.WriteLine("***** CHAPTER 11. Document Information and Metadata");
        int pdf30 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");

        Console.WriteLine("---cpdf_getVersion()");
        int v = netcpdf_getVersion(pdf30);
        Console.WriteLine($"minor version:{v}");
        Console.WriteLine("---cpdf_getMajorVersion()");
        int v2 = netcpdf_getMajorVersion(pdf30);
        Console.WriteLine($"major version:{v2}");
        Console.WriteLine("---cpdf_getTitle()");
        string title = netcpdf_getTitle(pdf30);
        Console.WriteLine($"title: {title}");
        Console.WriteLine("---cpdf_getAuthor()");
        string author = netcpdf_getAuthor(pdf30);
        Console.WriteLine($"author: {author}");
        Console.WriteLine("---cpdf_getSubject()");
        string subject = netcpdf_getSubject(pdf30);
        Console.WriteLine($"subject: {subject}");
        Console.WriteLine("---cpdf_getKeywords()");
        string keywords = netcpdf_getKeywords(pdf30);
        Console.WriteLine($"keywords: {keywords}");
        Console.WriteLine("---cpdf_getCreator()");
        string creator = netcpdf_getCreator(pdf30);
        Console.WriteLine($"creator: {creator}");
        Console.WriteLine("---cpdf_getProducer()");
        string producer = netcpdf_getProducer(pdf30);
        Console.WriteLine($"producer: {producer}");
        Console.WriteLine("---cpdf_getCreationDate()");
        string creationdate = netcpdf_getCreationDate(pdf30);
        Console.WriteLine($"creationdate: {creationdate}");
        Console.WriteLine("---cpdf_getModificationDate()");
        string modificationdate = netcpdf_getModificationDate(pdf30);
        Console.WriteLine($"modificationdate: {modificationdate}");
        Console.WriteLine("---cpdf_getTitleXMP()");
        string titlexmp = netcpdf_getTitleXMP(pdf30);
        Console.WriteLine($"titleXMP: {titlexmp}");
        Console.WriteLine("---cpdf_getAuthorXMP()");
        string authorxmp = netcpdf_getAuthorXMP(pdf30);
        Console.WriteLine($"authorXMP: {authorxmp}");
        Console.WriteLine("---cpdf_getSubjectXMP()");
        string subjectxmp = netcpdf_getSubjectXMP(pdf30);
        Console.WriteLine($"subjectXMP: {subjectxmp}");
        Console.WriteLine("---cpdf_getKeywordsXMP()");
        string keywordsxmp = netcpdf_getKeywordsXMP(pdf30);
        Console.WriteLine($"keywordsXMP: {keywordsxmp}");
        Console.WriteLine("---cpdf_getCreatorXMP()");
        string creatorxmp = netcpdf_getCreatorXMP(pdf30);
        Console.WriteLine($"creatorXMP: {creatorxmp}");
        Console.WriteLine("---cpdf_getProducerXMP()");
        string producerxmp = netcpdf_getProducerXMP(pdf30);
        Console.WriteLine($"producerXMP: {producerxmp}");
        Console.WriteLine("---cpdf_getCreationDateXMP()");
        string creationdatexmp = netcpdf_getCreationDateXMP(pdf30);
        Console.WriteLine($"creationdateXMP: {creationdatexmp}");
        Console.WriteLine("---cpdf_getModificationDateXMP()");
        string modificationdatexmp = netcpdf_getModificationDateXMP(pdf30);
        Console.WriteLine($"modificationdateXMP: {modificationdatexmp}");
        Console.WriteLine("---cpdf_setTitle()");
        netcpdf_setTitle(pdf30, "title");
        Console.WriteLine("---cpdf_setAuthor()");
        netcpdf_setAuthor(pdf30, "author");
        Console.WriteLine("---cpdf_setSubject()");
        netcpdf_setSubject(pdf30, "subject");
        Console.WriteLine("---cpdf_setKeywords()");
        netcpdf_setKeywords(pdf30, "keywords");
        Console.WriteLine("---cpdf_setCreator()");
        netcpdf_setCreator(pdf30, "creator");
        Console.WriteLine("---cpdf_setProducer()");
        netcpdf_setProducer(pdf30, "producer");
        Console.WriteLine("---cpdf_setCreationDate()");
        netcpdf_setCreationDate(pdf30, "now");
        Console.WriteLine("---cpdf_setModificationDate()");
        netcpdf_setModificationDate(pdf30, "now");
        Console.WriteLine("---cpdf_setTitleXMP()");
        netcpdf_setTitleXMP(pdf30, "title");
        Console.WriteLine("---cpdf_setAuthorXMP()");
        netcpdf_setAuthorXMP(pdf30, "author");
        Console.WriteLine("---cpdf_setSubjectXMP()");
        netcpdf_setSubjectXMP(pdf30, "subject");
        Console.WriteLine("---cpdf_setKeywordsXMP()");
        netcpdf_setKeywordsXMP(pdf30, "keywords");
        Console.WriteLine("---cpdf_setCreatorXMP()");
        netcpdf_setCreatorXMP(pdf30, "creator");
        Console.WriteLine("---cpdf_setProducerXMP()");
        netcpdf_setProducerXMP(pdf30, "producer");
        Console.WriteLine("---cpdf_setCreationDateXMP()");
        netcpdf_setCreationDateXMP(pdf30, "now");
        Console.WriteLine("---cpdf_setModificationDateXMP()");
        netcpdf_setModificationDateXMP(pdf30, "now");
        netcpdf_toFile(pdf30, "testoutputs/11setinfo.pdf", netcpdf_false, netcpdf_false);
        int year = 0;
        int month = 0;
        int day = 0;
        int hour = 0;
        int minute = 0;
        int second = 0;
        int hour_offset = 0;
        int minute_offset = 0;
        Console.WriteLine("---cpdf_getDateComponents()");
        netcpdf_getDateComponents("D:20061108125017Z", ref year, ref month, ref day, ref hour, ref minute, ref second, ref hour_offset, ref minute_offset);
        Console.WriteLine($"D:20061108125017Z = {year}, {month}, {day}, {hour}, {minute}, {second}, {hour_offset}, {minute_offset}");
        Console.WriteLine("---cpdf_dateStringOfComponents()");
        string datestr = netcpdf_dateStringOfComponents(year, month, day, hour, minute, second, hour_offset, minute_offset);
        Console.WriteLine(datestr);
        Console.WriteLine("---cpdf_getPageRotation()");
        int rot = netcpdf_getPageRotation(pdf30, 1);
        Console.WriteLine($"/Rotate on page 1 = {rot}");
        Console.WriteLine("---cpdf_hasBox()");
        int hasbox = netcpdf_hasBox(pdf30, 1, "/CropBox");
        Console.WriteLine($"hasbox: {hasbox}");
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
        netcpdf_getMediaBox(pdf30, 1, ref mb_minx, ref mb_maxx, ref mb_miny, ref mb_maxy);
        Console.WriteLine($"Media: {mb_minx:0.000000} {mb_maxx:0.000000} {mb_miny:0.000000} {mb_maxy:0.000000}");
        Console.WriteLine("---cpdf_getCropBox()");
        netcpdf_getCropBox(pdf30, 1, ref cb_minx, ref cb_maxx, ref cb_miny, ref cb_maxy);
        Console.WriteLine($"Crop: {cb_minx:0.000000} {cb_maxx:0.000000} {cb_miny:0.000000} {cb_maxy:0.000000}");
        Console.WriteLine("---cpdf_getBleedBox()");
        netcpdf_getBleedBox(pdf30, 1, ref bb_minx, ref bb_maxx, ref bb_miny, ref bb_maxy);
        Console.WriteLine($"Bleed: {bb_minx:0.000000} {bb_maxx:0.000000} {bb_miny:0.000000} {bb_maxy:0.000000}");
        Console.WriteLine("---cpdf_getArtBox()");
        netcpdf_getArtBox(pdf30, 1, ref ab_minx, ref ab_maxx, ref ab_miny, ref ab_maxy);
        Console.WriteLine($"Art: {ab_minx:0.000000} {ab_maxx:0.000000} {ab_miny:0.000000} {ab_maxy:0.000000}");
        Console.WriteLine("---cpdf_getTrimBox()");
        netcpdf_getTrimBox(pdf30, 1, ref tb_minx, ref tb_maxx, ref tb_miny, ref tb_maxy);
        Console.WriteLine($"Trim: {tb_minx:0.000000} {tb_maxx:0.000000} {tb_miny:0.000000} {tb_maxy:0.000000}");
        Console.WriteLine("---cpdf_setMediaBox()");
        netcpdf_setMediabox(pdf30, netcpdf_all(pdf30), 100, 500, 150, 550);
        Console.WriteLine("---cpdf_setCropBox()");
        netcpdf_setCropBox(pdf30, netcpdf_all(pdf30), 100, 500, 150, 550);
        Console.WriteLine("---cpdf_setTrimBox()");
        netcpdf_setTrimBox(pdf30, netcpdf_all(pdf30), 100, 500, 150, 550);
        Console.WriteLine("---cpdf_setArtBox()");
        netcpdf_setArtBox(pdf30, netcpdf_all(pdf30), 100, 500, 150, 550);
        Console.WriteLine("---cpdf_setBleedBox()");
        netcpdf_setBleedBox(pdf30, netcpdf_all(pdf30), 100, 500, 150, 550);
        netcpdf_toFile(pdf30, "testoutputs/11setboxes.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_markTrapped()");
        netcpdf_markTrapped(pdf30);
        Console.WriteLine("---cpdf_markTrappedXMP()");
        netcpdf_markTrappedXMP(pdf30);
        netcpdf_toFile(pdf30, "testoutputs/11trapped.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_markUntrapped()");
        netcpdf_markUntrapped(pdf30);
        Console.WriteLine("---cpdf_markUntrappedXMP()");
        netcpdf_markUntrappedXMP(pdf30);
        netcpdf_toFile(pdf30, "testoutputs/11untrapped.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_setPageLayout()");
        netcpdf_setPageLayout(pdf30, netcpdf_twoColumnLeft);
        Console.WriteLine("---cpdf_setPageMode()");
        netcpdf_setPageMode(pdf30, netcpdf_useOutlines);
        Console.WriteLine("---cpdf_hideToolbar()");
        netcpdf_hideToolbar(pdf30, netcpdf_true);
        Console.WriteLine("---cpdf_hideMenubar()");
        netcpdf_hideMenubar(pdf30, netcpdf_true);
        Console.WriteLine("---cpdf_hideWindowUi()");
        netcpdf_hideWindowUi(pdf30, netcpdf_true);
        Console.WriteLine("---cpdf_fitWindow()");
        netcpdf_fitWindow(pdf30, netcpdf_true);
        Console.WriteLine("---cpdf_centerWindow()");
        netcpdf_centerWindow(pdf30, netcpdf_true);
        Console.WriteLine("---cpdf_displayDocTitle()");
        netcpdf_displayDocTitle(pdf30, netcpdf_true);
        Console.WriteLine("---cpdf_openAtPage()");
        netcpdf_openAtPage(pdf30, netcpdf_true, 4);
        netcpdf_toFile(pdf30, "testoutputs/11open.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_setMetadataFromFile()");
        netcpdf_setMetadataFromFile(pdf30, "testinputs/cpdflibmanual.pdf");
        netcpdf_toFile(pdf30, "testoutputs/11metadata1.pdf", netcpdf_false, netcpdf_false);
        netcpdf_toFile(pdf30, "testoutputs/11metadata2.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_removeMetadata()");
        netcpdf_removeMetadata(pdf30);
        Console.WriteLine("---cpdf_createMetadata()");
        netcpdf_createMetadata(pdf30);
        netcpdf_toFile(pdf30, "testoutputs/11metadata3.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_setMetadataDate()");
        netcpdf_setMetadataDate(pdf30, "now");
        netcpdf_toFile(pdf30, "testoutputs/11metadata4.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_addPageLabels()");
        netcpdf_addPageLabels(pdf30, netcpdf_uppercaseRoman, "PREFIX-", 1, netcpdf_all(pdf30), netcpdf_false);
        Console.WriteLine("---cpdf: get page labels");
        int pls = netcpdf_startGetPageLabels(pdf30);
        Console.WriteLine($"There are {pls} labels");
        for (int plsc = 0; plsc < pls; plsc++)
        {
            int style = netcpdf_getPageLabelStyle(plsc);
            string prefix = netcpdf_getPageLabelPrefix(plsc);
            int offset = netcpdf_getPageLabelOffset(plsc);
            int lab_range = netcpdf_getPageLabelRange(plsc);
            Console.WriteLine($"Page label: {style}, {prefix}, {offset}, {lab_range}");
        }
        netcpdf_endGetPageLabels();
        Console.WriteLine("---cpdf_removePageLabels()");
        netcpdf_removePageLabels(pdf30);
        netcpdf_toFile(pdf30, "testoutputs/11pagelabels.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_getPageLabelStringForPage()");
        string pl = netcpdf_getPageLabelStringForPage(pdf30, 1);
        Console.WriteLine($"Label string is {pl}");

        /* CHAPTER 12. File Attachments */
        Console.WriteLine("***** CHAPTER 12. File Attachments");
        int attachments = netcpdf_fromFile("testinputs/has_attachments.pdf", "");
        Console.WriteLine("---cpdf_attachFile()");
        netcpdf_attachFile("testinputs/image.pdf", attachments);
        Console.WriteLine("---cpdf_attachFileToPage()");
        netcpdf_attachFileToPage("testinputs/cpdflibmanual.pdf", attachments, 1);
        netcpdf_toFile(attachments, "testoutputs/12with_attachments.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf: get attachments");
        netcpdf_startGetAttachments(attachments);
        int n_a = netcpdf_numberGetAttachments();
        Console.WriteLine($"There are {n_a} attachments to get");
        for (int aa = 0; aa < n_a; aa++)
        {
            string a_n = netcpdf_getAttachmentName(aa);
            Console.WriteLine($"Attachment {aa} is named {a_n}");
            int a_page = netcpdf_getAttachmentPage(aa);
            Console.WriteLine($"It is on page {a_page}");
            byte[] a_data = netcpdf_getAttachmentData(aa);
            Console.WriteLine($"Contains {a_data.Length} bytes of data");
        }
        netcpdf_endGetAttachments();
        Console.WriteLine("---cpdf_removeAttachedFiles()");
        netcpdf_removeAttachedFiles(attachments);
        netcpdf_toFile(attachments, "testoutputs/12removed_attachments.pdf", netcpdf_false, netcpdf_false);

        /* CHAPTER 13. Images. */
        Console.WriteLine("***** CHAPTER 13. Images");
        Console.WriteLine("---cpdf: get image resolution");
        int image_pdf = netcpdf_fromFile("testinputs/image.pdf", "");
        int im_n = netcpdf_startGetImageResolution(image_pdf, 2.0);
        for (int im = 0; im < im_n; im++)
        {
            int im_p = netcpdf_getImageResolutionPageNumber(im);
            string im_name = netcpdf_getImageResolutionImageName(im);
            int im_xp = netcpdf_getImageResolutionXPixels(im);
            int im_yp = netcpdf_getImageResolutionYPixels(im);
            double im_xres = netcpdf_getImageResolutionXRes(im);
            double im_yres = netcpdf_getImageResolutionYRes(im);
            Console.WriteLine($"IMAGE: {im_p}, {im_name}, {im_xp}, {im_yp}, {im_xres:00.000000}, {im_yres:00.000000}");
        }
        netcpdf_endGetImageResolution();

        /* CHAPTER 14. Fonts. */
        Console.WriteLine("***** CHAPTER 14. Fonts");
        Console.WriteLine("---cpdf: Get Fonts");
        int fonts = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int fonts2 = netcpdf_fromFile("testinputs/frontmatter.pdf", "");
        netcpdf_startGetFontInfo(fonts);
        int n_fonts = netcpdf_numberFonts();
        for (int ff = 0; ff < n_fonts; ff++)
        {
            int page = netcpdf_getFontPage(ff);
            string f_name = netcpdf_getFontName(ff);
            string type = netcpdf_getFontType(ff);
            string encoding = netcpdf_getFontEncoding(ff);
            Console.WriteLine("Page {0}, font {1} has type {2} and encoding {3}", page, f_name, type, encoding);
        }
        netcpdf_endGetFontInfo();
        Console.WriteLine("---cpdf_removeFonts()");
        netcpdf_removeFonts(fonts);
        netcpdf_toFile(fonts, "testoutputs/14remove_fonts.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_copyFont()");
        netcpdf_copyFont(fonts, fonts2, netcpdf_all(fonts), 1, "/Font");

        /* CHAPTER 15. PDF and JSON */
        Console.WriteLine("***** CHAPTER 15. PDF and JSON");
        int jsonpdf = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_outputJSON()");
        netcpdf_outputJSON("testoutputs/15json.json", netcpdf_false, netcpdf_false, netcpdf_false, jsonpdf);
        netcpdf_outputJSON("testoutputs/15jsonnostream.json", netcpdf_false, netcpdf_true, netcpdf_false, jsonpdf);
        netcpdf_outputJSON("testoutputs/15jsonparsed.json", netcpdf_true, netcpdf_false, netcpdf_false, jsonpdf);
        netcpdf_outputJSON("testoutputs/15jsondecomp.json", netcpdf_false, netcpdf_false, netcpdf_true, jsonpdf);
        Console.WriteLine("---cpdf_fromJSON()");
        int fromjsonpdf = netcpdf_fromJSON("testoutputs/15jsonparsed.json");
        netcpdf_toFile(fromjsonpdf, "testoutputs/15fromjson.pdf", netcpdf_false, netcpdf_false);


        /* CHAPTER 16. Optional Content Groups */
        Console.WriteLine("***** CHAPTER 16. Optional Content Groups");
        int ocg = netcpdf_fromFile("testinputs/has_ocgs.pdf", "");
        Console.WriteLine("---cpdf: Get OCG List");
        int n2 = netcpdf_startGetOCGList(ocg);
        for(int x = 0; x < n2; x++)
        {
            Console.WriteLine(netcpdf_OCGListEntry(x));
        }
        netcpdf_endGetOCGList();
        Console.WriteLine("---cpdf_OCGCoalesce()");
        netcpdf_OCGCoalesce(ocg);
        Console.WriteLine("---cpdf_OCGRename()");
        netcpdf_OCGRename(ocg, "From", "To");
        Console.WriteLine("---cpdf_OCGOrderAll()");
        netcpdf_OCGOrderAll(ocg);


        /* CHAPTER 17. Creating New PDFs */
        Console.WriteLine("***** CHAPTER 17. Creating New PDFs");
        Console.WriteLine("---cpdf_blankDocument()");
        int new1 = netcpdf_blankDocument(100.0, 200.0, 20);
        Console.WriteLine("---cpdf_blankDocumentPaper()");
        int new2 = netcpdf_blankDocumentPaper(netcpdf_a4portrait, 10);
        netcpdf_toFile(new1, "testoutputs/01blank.pdf", netcpdf_false, netcpdf_false);
        netcpdf_toFile(new2, "testoutputs/01blanka4.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_textToPDF()");
        int ttpdf = netcpdf_textToPDF(500.0, 600.0, netcpdf_timesItalic, 8.0, "../cpdflib-source/cpdflibtest.c");
        netcpdf_toFile(ttpdf, "testoutputs/01ttpdf.pdf", netcpdf_false, netcpdf_false);
        int ttpdfpaper = netcpdf_textToPDFPaper(netcpdf_a4portrait, netcpdf_timesBoldItalic, 10.0, "../cpdflib-source/cpdflibtest.c");
        Console.WriteLine("---cpdf_textToPDFPaper()");
        netcpdf_toFile(ttpdfpaper, "testoutputs/01ttpdfpaper.pdf", netcpdf_false, netcpdf_false);


        /* CHAPTER 18. Miscellaneous */
        Console.WriteLine("***** CHAPTER 18. Miscellaneous");
        int misc = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int misc2 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int misc3 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int misc4 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int misc5 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int misc6 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int misc7 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int misc8 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int misc9 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int misc10 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int misc11 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int misc12 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int misc13 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int misc14 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int misc15 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int misc16 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int misclogo = netcpdf_fromFile("testinputs/logo.pdf", "");
        Console.WriteLine("---cpdf_draft()");
        netcpdf_draft(misc, netcpdf_all(misc), netcpdf_true);
        netcpdf_toFile(misc, "testoutputs/17draft.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_removeAllText()");
        netcpdf_removeAllText(misc2, netcpdf_all(misc2));
        netcpdf_toFile(misc2, "testoutputs/17removealltext.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_blackText()");
        netcpdf_blackText(misc3, netcpdf_all(misc3));
        netcpdf_toFile(misc3, "testoutputs/17blacktext.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_blackLines()");
        netcpdf_blackLines(misc4, netcpdf_all(misc4));
        netcpdf_toFile(misc4, "testoutputs/17blacklines.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_blackFills()");
        netcpdf_blackFills(misc5, netcpdf_all(misc5));
        netcpdf_toFile(misc5, "testoutputs/17blackfills.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_thinLines()");
        netcpdf_thinLines(misc6, netcpdf_all(misc6), 2.0);
        netcpdf_toFile(misc6, "testoutputs/17thinlines.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_copyId()");
        netcpdf_copyId(misclogo, misc7);
        netcpdf_toFile(misc7, "testoutputs/17copyid.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_removeId()");
        netcpdf_removeId(misc8);
        netcpdf_toFile(misc8, "testoutputs/17removeid.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_setVersion()");
        netcpdf_setVersion(misc9, 1);
        netcpdf_toFile(misc9, "testoutputs/17setversion.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_setFullVersion()");
        netcpdf_setFullVersion(misc10, 2, 0);
        netcpdf_toFile(misc10, "testoutputs/17setfullversion.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_removeDictEntry()");
        netcpdf_removeDictEntry(misc11, "/Producer");
        netcpdf_toFile(misc11, "testoutputs/17removedictentry.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_removeDictEntrySearch()");
        netcpdf_removeDictEntrySearch(misc13, "/Producer", "1");
        netcpdf_toFile(misc13, "testoutputs/17removedictentrysearch.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_replaceDictEntry()");
        netcpdf_replaceDictEntry(misc14, "/Producer", "{\"I\" : 1}");
        netcpdf_toFile(misc14, "testoutputs/17replacedictentry.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_replaceDictEntrySearch()");
        netcpdf_replaceDictEntrySearch(misc15, "/Producer", "1", "2");
        netcpdf_toFile(misc15, "testoutputs/17replacedictentrysearch.pdf", netcpdf_false, netcpdf_false);
        Console.WriteLine("---cpdf_getDictEntries()");
        byte[] entries = netcpdf_getDictEntries(misc16, "/Producer");
        Console.WriteLine($"length of entries data = {entries.Length}");
        Console.WriteLine("---cpdf_removeClipping()");
        netcpdf_removeClipping(misc12, netcpdf_all(misc12));
        netcpdf_toFile(misc12, "testoutputs/17removeclipping.pdf", netcpdf_false, netcpdf_false);
    }
}
}
