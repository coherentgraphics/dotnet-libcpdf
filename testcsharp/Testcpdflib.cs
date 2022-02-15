using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Netcpdf;

namespace test_libcpdf
{
class Program
{
    public static void chapter0()
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
    }

    public static void chapter1()
    {
        /* CHAPTER 1. Basics */
        Console.WriteLine("***** CHAPTER 1. Basics");
        Console.WriteLine("---cpdf_fromFile()");
        int pdf = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_fromFileLazy()");
        int pdf2 = netcpdf_fromFileLazy("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_toMemory()");
        byte[] mempdf = netcpdf_toMemory(pdf, false, false);
        Console.WriteLine("---cpdf_fromMemory()");
        int frommem = netcpdf_fromMemory(mempdf, "");
        netcpdf_toFile(frommem, "testoutputs/01fromMemory.pdf", false, false);
        Console.WriteLine("---cpdf_fromMemoryLazy()");
        IntPtr ptr = Marshal.AllocHGlobal(mempdf.Length);
        Marshal.Copy(mempdf, 0, ptr, mempdf.Length);
        int frommemlazy = netcpdf_fromMemoryLazy(ptr, mempdf.Length, "");
        netcpdf_toFile(frommemlazy, "testoutputs/01fromMemoryLazy.pdf", false, false);
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
        List<int> range = netcpdf_range(1, 10);
        Console.WriteLine("---cpdf_all()");
        List<int> all = netcpdf_all(pdf3);
        Console.WriteLine("---cpdf_even()");
        List<int> even = netcpdf_even(all);
        Console.WriteLine("---cpdf_odd()");
        List<int> odd = netcpdf_odd(all);
        Console.WriteLine("---cpdf_rangeUnion()");
        List<int> union = netcpdf_rangeUnion(even, odd);
        Console.WriteLine("---cpdf_difference()");
        List<int> diff = netcpdf_difference(even, odd);
        Console.WriteLine("---cpdf_removeDuplicates()");
        List<int> revdup = netcpdf_removeDuplicates(even);
        Console.WriteLine("---cpdf_rangeLength()");
        int length = netcpdf_rangeLength(even);
        Console.WriteLine("---cpdf_rangeGet()");
        int rangeget = netcpdf_rangeGet(even, 1);
        Console.WriteLine("---cpdf_rangeAdd()");
        List<int> rangeadd = netcpdf_rangeAdd(even, 20);
        Console.WriteLine("---cpdf_isInRange()");
        bool isin = netcpdf_isInRange(even, 2);
        Console.WriteLine("---cpdf_parsePagespec()");
        List<int> r = netcpdf_parsePagespec(pdf3, "1-5");
        Console.WriteLine("---cpdf_validatePagespec()");
        bool valid = netcpdf_validatePagespec("1-4,5,6");
        Console.WriteLine($"Validating pagespec gives {(valid ? 1 : 0)}");
        Console.WriteLine("---cpdf_stringOfPagespec()");
        string ps = netcpdf_stringOfPagespec(pdf3, r);
        Console.WriteLine($"String of pagespec is {ps}");
        Console.WriteLine("---cpdf_blankRange()");
        List<int> b = netcpdf_blankRange();

        int pdf10 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_pages()");
        int pages = netcpdf_pages(pdf10);
        Console.WriteLine($"Pages = {pages}");
        Console.WriteLine("---cpdf_pagesFast()");
        int pagesfast = netcpdf_pagesFast("", "testinputs/cpdflibmanual.pdf");
        Console.WriteLine($"Pages = {pages}");
        Console.WriteLine("---cpdf_toFile()");
        netcpdf_toFile(pdf10, "testoutputs/01tofile.pdf", false, false);
        Console.WriteLine("---cpdf_toFileExt()");
        netcpdf_toFileExt(pdf10, "testoutputs/01tofileext.pdf", false, true, true, true, true);
        Console.WriteLine("---cpdf_isEncrypted()");
        bool isenc = netcpdf_isEncrypted(pdf10);
        Console.WriteLine($"isencrypted:{(isenc ? 1 : 0)}");
        Console.WriteLine("---cpdf_isLinearized()");
        bool lin = netcpdf_isLinearized("testinputs/cpdfmanual.pdf");
        Console.WriteLine($"islinearized:{(lin ? 1 : 0)}");

        int pdf400 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pdf401 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int[] permissions = new [] {netcpdf_noEdit};
        Console.WriteLine("---cpdf_toFileEncrypted()");
        netcpdf_toFileEncrypted(pdf400, netcpdf_pdf40bit, permissions, permissions.Length, "owner", "user", false, false, "testoutputs/01encrypted.pdf");
        Console.WriteLine("---cpdf_toFileEncryptedExt()");
        netcpdf_toFileEncryptedExt(pdf401, netcpdf_pdf40bit, permissions, permissions.Length, "owner", "user", false, false, true, true, true, "testoutputs/01encryptedext.pdf");
        Console.WriteLine("---cpdf_hasPermission()");
        int pdfenc = netcpdf_fromFile("testoutputs/01encrypted.pdf", "user");
        bool hasnoedit = netcpdf_hasPermission(pdfenc, netcpdf_noEdit);
        bool hasnocopy = netcpdf_hasPermission(pdfenc, netcpdf_noCopy);
        Console.WriteLine($"Haspermission {(hasnoedit ? 1 : 0)}, {(hasnocopy ? 1 : 0)}");
        Console.WriteLine("---cpdf_encryptionKind()");
        int enckind = netcpdf_encryptionKind(pdfenc);
        Console.WriteLine($"encryption kind is {enckind}");
        Console.WriteLine("---cpdf_decryptPdf()");
        netcpdf_decryptPdf(pdf10, "");
        Console.WriteLine("---cpdf_decryptPdfOwner()");
        netcpdf_decryptPdfOwner(pdf10, "");
    }

    public static void chapter2()
    {
        /* CHAPTER 2. Merging and Splitting */
        Console.WriteLine("***** CHAPTER 2. Merging and Splitting");
        int pdf11 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        List<int> selectrange = netcpdf_range(1, 3);
        Console.WriteLine("---cpdf_mergeSimple()");
        int[] arr = new [] {pdf11, pdf11, pdf11};
        List<int> arr_list = new List<int> {};
        arr_list.AddRange(arr);
        int merged = netcpdf_mergeSimple(arr, arr.Length);
        netcpdf_toFile(merged, "testoutputs/02merged.pdf", false, true);
        Console.WriteLine("---cpdf_merge()");
        int merged2 = netcpdf_merge(arr, arr.Length, false, false);
        netcpdf_toFile(merged2, "testoutputs/02merged2.pdf", false, true);
        Console.WriteLine("---cpdf_mergeSame()");
        List<List<int>> ranges = new List<List<int>> {netcpdf_all(pdf11), netcpdf_all(pdf11), netcpdf_all(pdf11)};
        int merged3 = netcpdf_mergeSame(arr_list, arr.Length, false, false, ranges);
        netcpdf_toFile(merged3, "testoutputs/02merged3.pdf", false, false);
        Console.WriteLine("---cpdf_selectPages()");
        int pdf12 = netcpdf_selectPages(pdf11, selectrange);
        netcpdf_toFile(pdf12, "testoutputs/02selected.pdf", false, false);
    }

    public static void chapter3()
    {
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
        netcpdf_toFile(pagespdf1, "testoutputs/03scalepages.pdf", false, false);
        Console.WriteLine("---cpdf_scaleToFit()");
        netcpdf_scaleToFit(pagespdf2, netcpdf_all(pagespdf2), 1.5, 1.8, 0.9);
        netcpdf_toFile(pagespdf2, "testoutputs/03scaletofit.pdf", false, false);
        Console.WriteLine("---cpdf_scaleToFitPaper()");
        netcpdf_scaleToFitPaper(pagespdf3, netcpdf_all(pagespdf3), netcpdf_a4portrait, 0.8);
        netcpdf_toFile(pagespdf3, "testoutputs/03scaletofitpaper.pdf", false, false);
        Console.WriteLine("---cpdf_scaleContents()");
        netcpdf_position position = new netcpdf_position (netcpdf_topLeft, 20.0, 20.0);
        netcpdf_scaleContents(pagespdf4, netcpdf_all(pagespdf4), position, 2.0);
        netcpdf_toFile(pagespdf4, "testoutputs/03scalecontents.pdf", false, false);
        Console.WriteLine("---cpdf_shiftContents()");
        netcpdf_shiftContents(pagespdf5, netcpdf_all(pagespdf5), 1.5, 1.25);
        netcpdf_toFile(pagespdf5, "testoutputs/03shiftcontents.pdf", false, false);
        Console.WriteLine("---cpdf_rotate()");
        netcpdf_rotate(pagespdf6, netcpdf_all(pagespdf6), 90);
        netcpdf_toFile(pagespdf6, "testoutputs/03rotate.pdf", false, false);
        Console.WriteLine("---cpdf_rotateBy()");
        netcpdf_rotateBy(pagespdf7, netcpdf_all(pagespdf7), 90);
        netcpdf_toFile(pagespdf7, "testoutputs/03rotateby.pdf", false, false);
        Console.WriteLine("---cpdf_rotateContents()");
        netcpdf_rotateContents(pagespdf8, netcpdf_all(pagespdf8), 35.0);
        netcpdf_toFile(pagespdf8, "testoutputs/03rotatecontents.pdf", false, false);
        Console.WriteLine("---cpdf_upright()");
        netcpdf_upright(pagespdf9, netcpdf_all(pagespdf9));
        netcpdf_toFile(pagespdf9, "testoutputs/03upright.pdf", false, false);
        Console.WriteLine("---cpdf_hFlip()");
        netcpdf_hFlip(pagespdf10, netcpdf_all(pagespdf10));
        netcpdf_toFile(pagespdf10, "testoutputs/03hflip.pdf", false, false);
        Console.WriteLine("---cpdf_vFlip()");
        netcpdf_vFlip(pagespdf11, netcpdf_all(pagespdf11));
        netcpdf_toFile(pagespdf11, "testoutputs/03vflip.pdf", false, false);
        Console.WriteLine("---cpdf_crop()");
        netcpdf_crop(pagespdf12, netcpdf_all(pagespdf12), 0.0, 0.0, 400.0, 500.0);
        netcpdf_toFile(pagespdf12, "testoutputs/03crop.pdf", false, false);
        Console.WriteLine("---cpdf_trimMarks()");
        netcpdf_trimMarks(pagespdf13, netcpdf_all(pagespdf13));
        netcpdf_toFile(pagespdf13, "testoutputs/03trim_marks.pdf", false, false);
        Console.WriteLine("---cpdf_showBoxes()");
        netcpdf_showBoxes(pagespdf14, netcpdf_all(pagespdf14));
        netcpdf_toFile(pagespdf14, "testoutputs/03show_boxes.pdf", false, false);
        Console.WriteLine("---cpdf_hardBox()");
        netcpdf_hardBox(pagespdf15, netcpdf_all(pagespdf15), "/MediaBox");
        netcpdf_toFile(pagespdf15, "testoutputs/03hard_box.pdf", false, false);
        Console.WriteLine("---cpdf_removeCrop()");
        netcpdf_removeCrop(pagespdf16, netcpdf_all(pagespdf16));
        netcpdf_toFile(pagespdf16, "testoutputs/03remove_crop.pdf", false, false);
        Console.WriteLine("---cpdf_removeTrim()");
        netcpdf_removeTrim(pagespdf17, netcpdf_all(pagespdf17));
        netcpdf_toFile(pagespdf17, "testoutputs/03remove_trim.pdf", false, false);
        Console.WriteLine("---cpdf_removeArt()");
        netcpdf_removeArt(pagespdf18, netcpdf_all(pagespdf18));
        netcpdf_toFile(pagespdf18, "testoutputs/03remove_art.pdf", false, false);
        Console.WriteLine("---cpdf_removeBleed()");
        netcpdf_removeBleed(pagespdf19, netcpdf_all(pagespdf19));
        netcpdf_toFile(pagespdf19, "testoutputs/03remove_bleed.pdf", false, false);
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
        int pdf16 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_compress()");
        netcpdf_compress(pdf16);
        netcpdf_toFile(pdf16, "testoutputs/05compressed.pdf", false, false);
        Console.WriteLine("---cpdf_decompress()");
        netcpdf_decompress(pdf16);
        netcpdf_toFile(pdf16, "testoutputs/05decompressed.pdf", false, false);
        Console.WriteLine("---cpdf_squeezeInMemory()");
        netcpdf_squeezeInMemory(pdf16);
        netcpdf_toFile(pdf16, "testoutputs/05squeezedinmemory.pdf", false, false);
    }

    public static void chapter6()
    {
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
            bool open = netcpdf_getBookmarkOpenStatus(b2);
            Console.WriteLine($"Bookmark at level {level} points to page {page} and has text \"{text}\" and open {(open ? 1 : 0)}");
        }
        netcpdf_endGetBookmarkInfo();
        Console.WriteLine("---cpdf: set bookmarks");
        netcpdf_startSetBookmarkInfo(1);
        netcpdf_setBookmarkLevel(0, 0);
        netcpdf_setBookmarkPage(pdf17, 0, 20);
        netcpdf_setBookmarkOpenStatus(0, true);
        netcpdf_setBookmarkText(0, "New bookmark!");
        netcpdf_endSetBookmarkInfo(pdf17);
        netcpdf_toFile(pdf17, "testoutputs/06newmarks.pdf", false, false);
        Console.WriteLine("---cpdf_getBookmarksJSON()");
        int marksjson = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        byte[] marksdata = netcpdf_getBookmarksJSON(marksjson);
        Console.WriteLine($"Contains {marksdata.Length} bytes of data");
        Console.WriteLine("---cpdf_setBookmarksJSON()");
        netcpdf_setBookmarksJSON(marksjson, marksdata);
        netcpdf_toFile(marksjson, "testoutputs/06jsonmarks.pdf", false, false);
        Console.WriteLine("---cpdf_tableOfContents()");
        int tocpdf = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        netcpdf_tableOfContents(tocpdf, netcpdf_timesRoman, 12.0, "Table of Contents", false);
        netcpdf_toFile(tocpdf, "testoutputs/06toc.pdf", false, false);
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
        int textfile = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_addText()");
        netcpdf_position pos = new netcpdf_position (netcpdf_topLeft, 20.0, 20.0);
        netcpdf_addText(false,
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
                        false,
                        false,
                        true,
                        0.5,
                        netcpdf_leftJustify,
                        false,
                        false,
                        "",
                        1.0,
                        false);
        Console.WriteLine("---cpdf_addTextSimple()");
        netcpdf_addTextSimple(textfile, netcpdf_all(textfile), "The text!", pos, netcpdf_timesRoman, 50.0);
        netcpdf_toFile(textfile, "testoutputs/08added_text.pdf", false, false);
        Console.WriteLine("---cpdf_removeText()");
        netcpdf_removeText(textfile, netcpdf_all(textfile));
        netcpdf_toFile(textfile, "testoutputs/08removed_text.pdf", false, false);
        Console.WriteLine("---cpdf_textWidth()");
        int w = netcpdf_textWidth(netcpdf_timesRoman, "What is the width of this?");
        int stamp = netcpdf_fromFile("testinputs/logo.pdf", "");
        int stampee = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        List<int> stamp_range = netcpdf_all(stamp);
        Console.WriteLine("---cpdf_stampOn()");
        netcpdf_stampOn(stamp, stampee, stamp_range);
        Console.WriteLine("---cpdf_stampUnder()");
        netcpdf_stampUnder(stamp, stampee, stamp_range);
        netcpdf_position spos = new netcpdf_position (netcpdf_topLeft, 20.0, 20.0);
        Console.WriteLine("---cpdf_stampExtended()");
        netcpdf_stampExtended(stamp, stampee, stamp_range, true, true, spos, true);
        netcpdf_toFile(stamp, "testoutputs/08stamp_after.pdf", false, false);
        netcpdf_toFile(stampee, "testoutputs/08stampee_after.pdf", false, false);
        int c1 = netcpdf_fromFile("testinputs/logo.pdf", "");
        int c2 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_combinePages()");
        int c3 = netcpdf_combinePages(c1, c2);
        netcpdf_toFile(c3, "testoutputs/08c3after.pdf", false, false);
        Console.WriteLine("---cpdf_stampAsXObject()");
        int undoc = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int ulogo = netcpdf_fromFile("testinputs/logo.pdf", "");
        string name = netcpdf_stampAsXObject(undoc, netcpdf_all(undoc), ulogo);
        string content = $"q 1 0 0 1 100 100 cm {name} Do Q q 1 0 0 1 300 300 cm {name} Do Q q 1 0 0 1 500 500 cm {name} Do Q";
        Console.WriteLine("---cpdf_addContent()");
        netcpdf_addContent(content, true, undoc, netcpdf_all(undoc));
        netcpdf_toFile(undoc, "testoutputs/08demo.pdf", false, false);
    }

    public static void chapter9()
    {
        /* CHAPTER 9. Multipage facilities */
        Console.WriteLine("***** CHAPTER 9. Multipage facilities");
        int mp = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_twoUp()");
        netcpdf_twoUp(mp);
        netcpdf_toFile(mp, "testoutputs/09mp.pdf", false, false);
        int mp2 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_twoUpStack()");
        netcpdf_twoUpStack(mp2);
        netcpdf_toFile(mp2, "testoutputs/09mp2.pdf", false, false);
        int mp25 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_impose()");
        netcpdf_impose(mp25, 5.0, 4.0, false, false, false, false, false, 40.0, 20.0, 2.0);
        netcpdf_toFile(mp25, "testoutputs/09mp25.pdf", false, false);
        int mp26 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        netcpdf_impose(mp26, 2000.0, 1000.0, true, false, false, false, false, 40.0, 20.0, 2.0);
        netcpdf_toFile(mp26, "testoutputs/09mp26.pdf", false, false);
        int mp3 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_padBefore()");
        netcpdf_padBefore(mp3, netcpdf_range(1, 10));
        netcpdf_toFile(mp3, "testoutputs/09mp3.pdf", false, false);
        int mp4 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_padAfter()");
        netcpdf_padAfter(mp4, netcpdf_range(1, 10));
        netcpdf_toFile(mp4, "testoutputs/09mp4.pdf", false, false);
        int mp5 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_padEvery()");
        netcpdf_padEvery(mp5, 5);
        netcpdf_toFile(mp5, "testoutputs/09mp5.pdf", false, false);
        int mp6 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_padMultiple()");
        netcpdf_padMultiple(mp6, 10);
        netcpdf_toFile(mp6, "testoutputs/09mp6.pdf", false, false);
        int mp7 = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_padMultipleBefore()");
        netcpdf_padMultipleBefore(mp7, 23);
        netcpdf_toFile(mp7, "testoutputs/09mp7.pdf", false, false);
    }

    public static void chapter10()
    {
        /* CHAPTER 10. Annotations */
        Console.WriteLine("***** CHAPTER 10. Annotations");
        Console.WriteLine("---cpdf_annotationsJSON()");
        int annot = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        byte[] annotjson = netcpdf_annotationsJSON(annot);
        Console.WriteLine($"Contains {annotjson.Length} bytes of data");
    }

    public static void chapter11()
    {
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
        netcpdf_toFile(pdf30, "testoutputs/11setinfo.pdf", false, false);
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
        bool hasbox = netcpdf_hasBox(pdf30, 1, "/CropBox");
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
        netcpdf_toFile(pdf30, "testoutputs/11setboxes.pdf", false, false);
        Console.WriteLine("---cpdf_markTrapped()");
        netcpdf_markTrapped(pdf30);
        Console.WriteLine("---cpdf_markTrappedXMP()");
        netcpdf_markTrappedXMP(pdf30);
        netcpdf_toFile(pdf30, "testoutputs/11trapped.pdf", false, false);
        Console.WriteLine("---cpdf_markUntrapped()");
        netcpdf_markUntrapped(pdf30);
        Console.WriteLine("---cpdf_markUntrappedXMP()");
        netcpdf_markUntrappedXMP(pdf30);
        netcpdf_toFile(pdf30, "testoutputs/11untrapped.pdf", false, false);
        Console.WriteLine("---cpdf_setPageLayout()");
        netcpdf_setPageLayout(pdf30, netcpdf_twoColumnLeft);
        Console.WriteLine("---cpdf_setPageMode()");
        netcpdf_setPageMode(pdf30, netcpdf_useOutlines);
        Console.WriteLine("---cpdf_hideToolbar()");
        netcpdf_hideToolbar(pdf30, true);
        Console.WriteLine("---cpdf_hideMenubar()");
        netcpdf_hideMenubar(pdf30, true);
        Console.WriteLine("---cpdf_hideWindowUi()");
        netcpdf_hideWindowUi(pdf30, true);
        Console.WriteLine("---cpdf_fitWindow()");
        netcpdf_fitWindow(pdf30, true);
        Console.WriteLine("---cpdf_centerWindow()");
        netcpdf_centerWindow(pdf30, true);
        Console.WriteLine("---cpdf_displayDocTitle()");
        netcpdf_displayDocTitle(pdf30, true);
        Console.WriteLine("---cpdf_openAtPage()");
        netcpdf_openAtPage(pdf30, true, 4);
        netcpdf_toFile(pdf30, "testoutputs/11open.pdf", false, false);
        Console.WriteLine("---cpdf_setMetadataFromFile()");
        netcpdf_setMetadataFromFile(pdf30, "testinputs/cpdflibmanual.pdf");
        netcpdf_toFile(pdf30, "testoutputs/11metadata1.pdf", false, false);
        Console.WriteLine("---cpdf_setMetadataFromByteArray()");
        byte[] md = Encoding.ASCII.GetBytes("BYTEARRAY");
        netcpdf_setMetadataFromByteArray(pdf30, md);
        netcpdf_toFile(pdf30, "testoutputs/11metadata2.pdf", false, false);
        Console.WriteLine("---cpdf_getMetadata()");
        byte[] metadata = netcpdf_getMetadata(pdf30);
        Console.WriteLine("---cpdf_removeMetadata()");
        netcpdf_removeMetadata(pdf30);
        Console.WriteLine("---cpdf_createMetadata()");
        netcpdf_createMetadata(pdf30);
        netcpdf_toFile(pdf30, "testoutputs/11metadata3.pdf", false, false);
        Console.WriteLine("---cpdf_setMetadataDate()");
        netcpdf_setMetadataDate(pdf30, "now");
        netcpdf_toFile(pdf30, "testoutputs/11metadata4.pdf", false, false);
        Console.WriteLine("---cpdf_addPageLabels()");
        netcpdf_addPageLabels(pdf30, netcpdf_uppercaseRoman, "PREFIX-", 1, netcpdf_all(pdf30), false);
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
        netcpdf_toFile(pdf30, "testoutputs/11pagelabels.pdf", false, false);
        Console.WriteLine("---cpdf_getPageLabelStringForPage()");
        string pl = netcpdf_getPageLabelStringForPage(pdf30, 1);
        Console.WriteLine($"Label string is {pl}");
    }

    public static void chapter12()
    {
        /* CHAPTER 12. File Attachments */
        Console.WriteLine("***** CHAPTER 12. File Attachments");
        int attachments = netcpdf_fromFile("testinputs/has_attachments.pdf", "");
        Console.WriteLine("---cpdf_attachFile()");
        netcpdf_attachFile("testinputs/image.pdf", attachments);
        Console.WriteLine("---cpdf_attachFileToPage()");
        netcpdf_attachFileToPage("testinputs/image.pdf", attachments, 1);
        Console.WriteLine("---cpdf_attachFileFromMemory()");
        byte[] empty = {};
        netcpdf_attachFileFromMemory(empty, "metadata.txt", attachments);
        Console.WriteLine("---cpdf_attachFileToPageFromMemory()");
        netcpdf_attachFileToPageFromMemory(empty, "metadata.txt", attachments, 1);
        netcpdf_toFile(attachments, "testoutputs/12with_attachments.pdf", false, false);
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
        netcpdf_toFile(attachments, "testoutputs/12removed_attachments.pdf", false, false);
    }

    public static void chapter13()
    {
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
    }

    public static void chapter14()
    {
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
        netcpdf_toFile(fonts, "testoutputs/14remove_fonts.pdf", false, false);
        Console.WriteLine("---cpdf_copyFont()");
        netcpdf_copyFont(fonts, fonts2, netcpdf_all(fonts), 1, "/Font");
    }

    public static void chapter15()
    {
        /* CHAPTER 15. PDF and JSON */
        Console.WriteLine("***** CHAPTER 15. PDF and JSON");
        int jsonpdf = netcpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        Console.WriteLine("---cpdf_outputJSON()");
        netcpdf_outputJSON("testoutputs/15json.json", false, false, false, jsonpdf);
        netcpdf_outputJSON("testoutputs/15jsonnostream.json", false, true, false, jsonpdf);
        netcpdf_outputJSON("testoutputs/15jsonparsed.json", true, false, false, jsonpdf);
        netcpdf_outputJSON("testoutputs/15jsondecomp.json", false, false, true, jsonpdf);
        Console.WriteLine("---cpdf_fromJSON()");
        int fromjsonpdf = netcpdf_fromJSON("testoutputs/15jsonparsed.json");
        netcpdf_toFile(fromjsonpdf, "testoutputs/15fromjson.pdf", false, false);
        Console.WriteLine("---cpdf_outputJSONMemory()");
        byte[] jbuf = netcpdf_outputJSONMemory(fromjsonpdf, false, false, false);
        Console.WriteLine("---cpdf_fromJSONMemory()");
        int jfrommem = netcpdf_fromJSONMemory(jbuf);
        netcpdf_toFile(jfrommem, "testoutputs/15fromJSONMemory.pdf", false, false);
    }

    public static void chapter16()
    {
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
    }

    public static void chapter17()
    {
        /* CHAPTER 17. Creating New PDFs */
        Console.WriteLine("***** CHAPTER 17. Creating New PDFs");
        Console.WriteLine("---cpdf_blankDocument()");
        int new1 = netcpdf_blankDocument(100.0, 200.0, 20);
        Console.WriteLine("---cpdf_blankDocumentPaper()");
        int new2 = netcpdf_blankDocumentPaper(netcpdf_a4portrait, 10);
        netcpdf_toFile(new1, "testoutputs/01blank.pdf", false, false);
        netcpdf_toFile(new2, "testoutputs/01blanka4.pdf", false, false);
        Console.WriteLine("---cpdf_textToPDF()");
        int ttpdf = netcpdf_textToPDF(500.0, 600.0, netcpdf_timesItalic, 8.0, "../cpdflib-source/cpdflibtest.c");
        netcpdf_toFile(ttpdf, "testoutputs/01ttpdf.pdf", false, false);
        int ttpdfpaper = netcpdf_textToPDFPaper(netcpdf_a4portrait, netcpdf_timesBoldItalic, 10.0, "../cpdflib-source/cpdflibtest.c");
        Console.WriteLine("---cpdf_textToPDFPaper()");
        netcpdf_toFile(ttpdfpaper, "testoutputs/01ttpdfpaper.pdf", false, false);
    }

    public static void chapter18()
    {
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
        netcpdf_draft(misc, netcpdf_all(misc), true);
        netcpdf_toFile(misc, "testoutputs/17draft.pdf", false, false);
        Console.WriteLine("---cpdf_removeAllText()");
        netcpdf_removeAllText(misc2, netcpdf_all(misc2));
        netcpdf_toFile(misc2, "testoutputs/17removealltext.pdf", false, false);
        Console.WriteLine("---cpdf_blackText()");
        netcpdf_blackText(misc3, netcpdf_all(misc3));
        netcpdf_toFile(misc3, "testoutputs/17blacktext.pdf", false, false);
        Console.WriteLine("---cpdf_blackLines()");
        netcpdf_blackLines(misc4, netcpdf_all(misc4));
        netcpdf_toFile(misc4, "testoutputs/17blacklines.pdf", false, false);
        Console.WriteLine("---cpdf_blackFills()");
        netcpdf_blackFills(misc5, netcpdf_all(misc5));
        netcpdf_toFile(misc5, "testoutputs/17blackfills.pdf", false, false);
        Console.WriteLine("---cpdf_thinLines()");
        netcpdf_thinLines(misc6, netcpdf_all(misc6), 2.0);
        netcpdf_toFile(misc6, "testoutputs/17thinlines.pdf", false, false);
        Console.WriteLine("---cpdf_copyId()");
        netcpdf_copyId(misclogo, misc7);
        netcpdf_toFile(misc7, "testoutputs/17copyid.pdf", false, false);
        Console.WriteLine("---cpdf_removeId()");
        netcpdf_removeId(misc8);
        netcpdf_toFile(misc8, "testoutputs/17removeid.pdf", false, false);
        Console.WriteLine("---cpdf_setVersion()");
        netcpdf_setVersion(misc9, 1);
        netcpdf_toFile(misc9, "testoutputs/17setversion.pdf", false, false);
        Console.WriteLine("---cpdf_setFullVersion()");
        netcpdf_setFullVersion(misc10, 2, 0);
        netcpdf_toFile(misc10, "testoutputs/17setfullversion.pdf", false, false);
        Console.WriteLine("---cpdf_removeDictEntry()");
        netcpdf_removeDictEntry(misc11, "/Producer");
        netcpdf_toFile(misc11, "testoutputs/17removedictentry.pdf", false, false);
        Console.WriteLine("---cpdf_removeDictEntrySearch()");
        netcpdf_removeDictEntrySearch(misc13, "/Producer", "1");
        netcpdf_toFile(misc13, "testoutputs/17removedictentrysearch.pdf", false, false);
        Console.WriteLine("---cpdf_replaceDictEntry()");
        netcpdf_replaceDictEntry(misc14, "/Producer", "{\"I\" : 1}");
        netcpdf_toFile(misc14, "testoutputs/17replacedictentry.pdf", false, false);
        Console.WriteLine("---cpdf_replaceDictEntrySearch()");
        netcpdf_replaceDictEntrySearch(misc15, "/Producer", "1", "2");
        netcpdf_toFile(misc15, "testoutputs/17replacedictentrysearch.pdf", false, false);
        Console.WriteLine("---cpdf_getDictEntries()");
        byte[] entries = netcpdf_getDictEntries(misc16, "/Producer");
        Console.WriteLine($"length of entries data = {entries.Length}");
        Console.WriteLine("---cpdf_removeClipping()");
        netcpdf_removeClipping(misc12, netcpdf_all(misc12));
        netcpdf_toFile(misc12, "testoutputs/17removeclipping.pdf", false, false);
    }

    static void Main(string[] args)
    {
        chapter0();
        chapter1();
        chapter2();
        chapter3();
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
