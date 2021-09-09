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
        //FIXME fromMemory
        //FIXME fromMemoryLazy
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
        [DllImport("libcpdf.so")] static extern void cpdf_toFile(int pdf, string filename, int linearize, int make_id);
        [DllImport("libcpdf.so")] static extern void cpdf_toFileExt(int pdf, string filename, int linearize, int make_id, int preserve_objstm, int generate_objstm, int compress_objstm);
        //FIXME toMemory
        [DllImport("libcpdf.so")] static extern int cpdf_isEncrypted(int pdf);
        [DllImport("libcpdf.so")] static extern void cpdf_decryptPdf(int pdf, string userpw);
        [DllImport("libcpdf.so")] static extern void cpdf_decryptPdfOwner(int pdf, string ownerpw);
        //FIXME [DllImport("libcpdf.so")] static extern void cpdf_toFileEncrypted(int pdf, int encryption_method, int *permissions, int permission_length, string ownerpw, string userpw, int linearize, int makeid, string filename);
        //FIXME [DllImport("libcpdf.so")] static extern void cpdf_toFileEncryptedExt(int pdf, int encryption_method, int *permissions, int permission_length, string ownerpw, string userpw, int linearize, int makeid, int preserve_objstm, int generate_objstm, int compress_objstm, string filename);
        [DllImport("libcpdf.so")] static extern int cpdf_hasPermission(int pdf, int permission);
        [DllImport("libcpdf.so")] static extern int cpdf_encryptionKind(int pdf);

        /* CHAPTER 2. Merging and Splitting */
        //FIXME mergeSimple
        //FIXME merge
        //FIXME mergeSame
        [DllImport("libcpdf.so")] static extern int cpdf_selectPages(int pdf, int r);

        /* CHAPTER 3. Pages */
        [DllImport("libcpdf.so")] static extern void cpdf_scalePages(int pdf, int range, double sx, double sy);
        [DllImport("libcpdf.so")] static extern void cpdf_scaleToFit(int pdf, int range, double sx, double sy, double scale);
        [DllImport("libcpdf.so")] static extern void cpdf_scaleToFitPaper(int pdf, int range, int pagesize, double scale);
        //FIXME scaleContents (position)
        [DllImport("libcpdf.so")] static extern void cpdf_shiftContents(int pdf, int range, double dx, double dy);
        [DllImport("libcpdf.so")] static extern void cpdf_rotate(int pdf, int range, int rotation);
        [DllImport("libcpdf.so")] static extern void cpdf_rotateBy(int pdf, int range, int rotation);
        [DllImport("libcpdf.so")] static extern void cpdf_rotateContents(int pdf, int range, double angle);
        [DllImport("libcpdf.so")] static extern void cpdf_upright(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_hFlip(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_vFlip(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_crop(int pdf, int range, double x, double y, double w, double h);
        [DllImport("libcpdf.so")] static extern void cpdf_removeCrop(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_removeTrim(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_removeArt(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_removeBleed(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_trimMarks(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_showBoxes(int pdf, int range);
        [DllImport("libcpdf.so")] static extern void cpdf_hardBox(int pdf, int range, string boxname);
        
        /* CHAPTER 4. Encryption */

        /* Encryption covered under Chapter 1 in cpdflib. */


        /* CHAPTER 5. Compression */
        [DllImport("libcpdf.so")] static extern void cpdf_compress(int pdf);
        [DllImport("libcpdf.so")] static extern void cpdf_uncompress(int pdf);
        [DllImport("libcpdf.so")] static extern void cpdf_squeezeInMemory(int pdf);

        /* CHAPTER 6. Bookmarks */
        [DllImport("libcpdf.so")] static extern void cpdf_startGetBookmarkInfo(int pdf);
        [DllImport("libcpdf.so")] static extern int cpdf_numberBookmarks();
        [DllImport("libcpdf.so")] static extern int cpdf_getBookmarkLevel(int n);
        [DllImport("libcpdf.so")] static extern int cpdf_getBookmarkPage(int pdf, int n);
        [DllImport("libcpdf.so")] static extern IntPtr cpdf_getBookmarkText(int n);
        [DllImport("libcpdf.so")] static extern int cpdf_getBookmarkOpenStatus(int n);
        [DllImport("libcpdf.so")] static extern void cpdf_endGetBookmarkInfo();



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

            int cpdf_noEdit = 0;
            int cpdf_noPrint = 1;
            int cpdf_noCopy = 2;
            int cpdf_noAnnot = 3;
            int cpdf_noForms = 4;
            int cpdf_noExtract = 5;
            int cpdf_noAssemble = 6;
            int cpdf_noHqPrint = 7;

            int cpdf_pdf40bit = 0;
            int cpdf_pdf128bit = 1;
            int cpdf_aes128bitfalse = 2;
            int cpdf_aes128bittrue = 3;
            int cpdf_aes256bitfalse = 4;
            int cpdf_aes256bittrue = 5;
            int cpdf_aes256bitisofalse = 6;
            int cpdf_aes256bitisotrue = 7;

            int cpdf_posCentre = 0;
            int cpdf_posLeft = 1;
            int cpdf_posRight = 2;
            int cpdf_top = 3;
            int cpdf_topLeft = 4;
            int cpdf_topRight = 5;
            int cpdf_left = 6;
            int cpdf_bottomLeft = 7;
            int cpdf_bottom = 8;
            int cpdf_bottomRight = 9;
            int cpdf_right = 10;
            int cpdf_diagonal = 11;
            int cpdf_reverseDiagonal = 12;

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
            int pdf10 = cpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
            int pages = cpdf_pages(pdf10);
            int pagesfast = cpdf_pagesFast("", "testinputs/cpdflibmanual.pdf");
            cpdf_toFile(pdf10, "testoutputs/even.pdf", cpdf_false, cpdf_true);
            cpdf_toFileExt(pdf10, "testoutputs/evenext.pdf", cpdf_false, cpdf_true, cpdf_true, cpdf_true, cpdf_true);
            int isenc = cpdf_isEncrypted(pdf10);
            cpdf_decryptPdf(pdf10, "");
            cpdf_decryptPdfOwner(pdf10, "");
            int hasnoedit = cpdf_hasPermission(pdf10, cpdf_noEdit);
            int enckind = cpdf_encryptionKind(pdf10);

            /* CHAPTER 2. Merging and Splitting */
            int pdf11 = cpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
            int pdf12 = cpdf_selectPages(pdf11, even);
            cpdf_toFile(pdf12, "testoutputs/selectedpages.pdf", cpdf_false, cpdf_true);

            /* CHAPTER 3. Pages */
        }
    }
}
