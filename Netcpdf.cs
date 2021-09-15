using System;
using System.Runtime.InteropServices;

namespace dotnet_libcpdf
{
class Program
{
    /* Our imports. We must wrap them up for string conversions, error handling
     * and conversions between ranges and lists */

    /* CHAPTER 0. Preliminaries */

    public static void netcpdf_startup(string[] argv)
    {
      //FIXME Actually convert and pass the args
      [DllImport("libcpdf.so")] static extern void cpdf_startup(IntPtr[] ptr);
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
      [DllImport("libcpdf.so")] static extern int cpdf_lastError();
      return cpdf_lastError();
    }

    public static string netcpdf_lastErrorString()
    {
      [DllImport("libcpdf.so")] static extern IntPtr cpdf_lastErrorString();
      return Marshal.PtrToStringAuto(cpdf_lastErrorString());
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
    [DllImport("libcpdf.so")] static extern void cpdf_decompress(int pdf);
    [DllImport("libcpdf.so")] static extern void cpdf_squeezeInMemory(int pdf);

    /* CHAPTER 6. Bookmarks */
    [DllImport("libcpdf.so")] static extern void cpdf_startGetBookmarkInfo(int pdf);
    [DllImport("libcpdf.so")] static extern int cpdf_numberBookmarks();
    [DllImport("libcpdf.so")] static extern int cpdf_getBookmarkLevel(int n);
    [DllImport("libcpdf.so")] static extern int cpdf_getBookmarkPage(int pdf, int n);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getBookmarkText(int n);
    [DllImport("libcpdf.so")] static extern int cpdf_getBookmarkOpenStatus(int n);
    [DllImport("libcpdf.so")] static extern void cpdf_endGetBookmarkInfo();
    [DllImport("libcpdf.so")] static extern void cpdf_startSetBookmarkInfo(int nummarks);
    [DllImport("libcpdf.so")] static extern void cpdf_setBookmarkLevel(int n, int level);
    [DllImport("libcpdf.so")] static extern void cpdf_setBookmarkPage(int pdf, int n, int targetpage);
    [DllImport("libcpdf.so")] static extern void cpdf_setBookmarkOpenStatus(int n, int status);
    [DllImport("libcpdf.so")] static extern void cpdf_setBookmarkText(int n, string text);
    [DllImport("libcpdf.so")] static extern void cpdf_endSetBookmarkInfo(int pdf);

    /* CHAPTER 7. Presentations */
    /* Not included in the library version. */

    /* CHAPTER 8. Logos, Watermarks and Stamps */
    [DllImport("libcpdf.so")] static extern void cpdf_stampOn(int stamp_pdf, int pdf, int range);
    [DllImport("libcpdf.so")] static extern void cpdf_stampUnder(int stamp_pdf, int pdf, int range);
    //FIXME cpdf_stampExtended needs position struct
    [DllImport("libcpdf.so")] static extern int cpdf_combinePages(int under, int over);
    //FIXME addtext position
    //FIXME addtextSimple position
    [DllImport("libcpdf.so")] static extern void cpdf_removeText(int pdf, int range);
    [DllImport("libcpdf.so")] static extern int cpdf_textWidth(int font, string text);
    [DllImport("libcpdf.so")] static extern void cpdf_addContent(string content, int before, int range, int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_stampAsXObject(int pdf, int range, int stamp_pdf);

    /* CHAPTER 9. Multipage facilities */
    [DllImport("libcpdf.so")] static extern void cpdf_twoUp(int pdf);
    [DllImport("libcpdf.so")] static extern void cpdf_twoUpStack(int pdf);
    [DllImport("libcpdf.so")] static extern void cpdf_padBefore(int pdf, int range);
    [DllImport("libcpdf.so")] static extern void cpdf_padAfter(int pdf, int range);
    [DllImport("libcpdf.so")] static extern void cpdf_padEvery(int pdf, int n);
    [DllImport("libcpdf.so")] static extern void cpdf_padMultiple(int pdf, int n);
    [DllImport("libcpdf.so")] static extern void cpdf_padMultipleBefore(int pdf, int n);

    /* CHAPTER 10. Annotations */
    /* Not in the library version */

    /* CHAPTER 11. Document Information and Metadata */
    [DllImport("libcpdf.so")] static extern int cpdf_isLinearized(string filename);
    [DllImport("libcpdf.so")] static extern int cpdf_getVersion(int pdf);
    [DllImport("libcpdf.so")] static extern int cpdf_getMajorVersion(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getTitle(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getAuthor(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getSubject(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getKeywords(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getCreator(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getProducer(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getCreationDate(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getModificationDate(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getTitleXMP(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getAuthorXMP(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getSubjectXMP(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getKeywordsXMP(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getCreatorXMP(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getProducerXMP(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getCreationDateXMP(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getModificationDateXMP(int pdf);
    [DllImport("libcpdf.so")] static extern void cpdf_setTitle(int pdf, string s);
    [DllImport("libcpdf.so")] static extern void cpdf_setAuthor(int pdf, string s);
    [DllImport("libcpdf.so")] static extern void cpdf_setSubject(int pdf, string s);
    [DllImport("libcpdf.so")] static extern void cpdf_setKeywords(int pdf, string s);
    [DllImport("libcpdf.so")] static extern void cpdf_setCreator(int pdf, string s);
    [DllImport("libcpdf.so")] static extern void cpdf_setProducer(int pdf, string s);
    [DllImport("libcpdf.so")] static extern void cpdf_setCreationDate(int pdf, string s);
    [DllImport("libcpdf.so")] static extern void cpdf_setModificationDate(int pdf, string s);
    [DllImport("libcpdf.so")] static extern void cpdf_setTitleXMP(int pdf, string s);
    [DllImport("libcpdf.so")] static extern void cpdf_setAuthorXMP(int pdf, string s);
    [DllImport("libcpdf.so")] static extern void cpdf_setSubjectXMP(int pdf, string s);
    [DllImport("libcpdf.so")] static extern void cpdf_setKeywordsXMP(int pdf, string s);
    [DllImport("libcpdf.so")] static extern void cpdf_setCreatorXMP(int pdf, string s);
    [DllImport("libcpdf.so")] static extern void cpdf_setProducerXMP(int pdf, string s);
    [DllImport("libcpdf.so")] static extern void cpdf_setCreationDateXMP(int pdf, string s);
    [DllImport("libcpdf.so")] static extern void cpdf_setModificationDateXMP(int pdf, string s);
    //FIXME getDateComponents
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_dateStringOfComponents(int y, int m, int d, int h, int min, int sec);
    [DllImport("libcpdf.so")] static extern int cpdf_getPageRotation(int pdf, int pagenumber);
    [DllImport("libcpdf.so")] static extern int cpdf_hasBox(int pdf, int pagenumber, string boxname);
    //FIXME getMediaBox etc. pass by ref
    [DllImport("libcpdf.so")] static extern void cpdf_setMediabox(int pdf, int r, double minx, double maxx, double miny, double maxy);
    [DllImport("libcpdf.so")] static extern void cpdf_setCropBox(int pdf, int r, double minx, double maxx, double miny, double maxy);
    [DllImport("libcpdf.so")] static extern void cpdf_setTrimBox(int pdf, int r, double minx, double maxx, double miny, double maxy);
    [DllImport("libcpdf.so")] static extern void cpdf_setArtBox(int pdf, int r, double minx, double maxx, double miny, double maxy);
    [DllImport("libcpdf.so")] static extern void cpdf_setBleedBox(int pdf, int r, double minx, double maxx, double miny, double maxy);
    [DllImport("libcpdf.so")] static extern void cpdf_markTrapped(int pdf);
    [DllImport("libcpdf.so")] static extern void cpdf_markUntrapped(int pdf);
    [DllImport("libcpdf.so")] static extern void cpdf_markTrappedXMP(int pdf);
    [DllImport("libcpdf.so")] static extern void cpdf_markUntrappedXMP(int pdf);
    [DllImport("libcpdf.so")] static extern void cpdf_setPageLayout(int pdf, int layout);
    [DllImport("libcpdf.so")] static extern void cpdf_setPageMode(int pdf, int mode);
    [DllImport("libcpdf.so")] static extern void cpdf_hideToolbar(int pdf, int flag);
    [DllImport("libcpdf.so")] static extern void cpdf_hideMenubar(int pdf, int flag);
    [DllImport("libcpdf.so")] static extern void cpdf_hideWindowUi(int pdf, int flag);
    [DllImport("libcpdf.so")] static extern void cpdf_fitWindow(int pdf, int flag);
    [DllImport("libcpdf.so")] static extern void cpdf_centerWindow(int pdf, int flag);
    [DllImport("libcpdf.so")] static extern void cpdf_displayDocTitle(int pdf, int flag);
    [DllImport("libcpdf.so")] static extern void cpdf_openAtPage(int pdf, int fit, int pagenumber);
    [DllImport("libcpdf.so")] static extern void cpdf_setMetadataFromFile(int pdf, string filename);
    //FIXME setMetadataFromByteArray
    //FIXME getMetadata
    [DllImport("libcpdf.so")] static extern void cpdf_removeMetadata(int pdf);
    [DllImport("libcpdf.so")] static extern void cpdf_createMetadata(int pdf);
    [DllImport("libcpdf.so")] static extern void cpdf_setMetadataDate(int pdf, string date);
    [DllImport("libcpdf.so")] static extern void cpdf_addPageLabels(int pdf, int style, string prefix, int range, int progress);
    [DllImport("libcpdf.so")] static extern void cpdf_removePageLabels(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getPageLabelStringForPage(int pdf, int pagenumber);
    [DllImport("libcpdf.so")] static extern int cpdf_startGetPageLabels(int pdf);
    [DllImport("libcpdf.so")] static extern int cpdf_getPageLabelStyle(int n);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getPageLabelPrefix(int n);
    [DllImport("libcpdf.so")] static extern int cpdf_getPageLabelOffset(int n);
    [DllImport("libcpdf.so")] static extern int cpdf_getPageLabelRange(int n);
    [DllImport("libcpdf.so")] static extern void cpdf_endGetPageLabels();

    /* CHAPTER 12. File Attachments */
    [DllImport("libcpdf.so")] static extern void cpdf_attachFile(string filename, int pdf);
    [DllImport("libcpdf.so")] static extern void cpdf_attachFileToPage(string filename, int pdf, int pagenumber);
    //FIXME cpdf_attachFileFromMemory / cpdf_attachFileToPageFromMemory
    [DllImport("libcpdf.so")] static extern void cpdf_removeAttachedFiles(int pdf);
    [DllImport("libcpdf.so")] static extern void cpdf_startGetAttachments(int pdf);
    [DllImport("libcpdf.so")] static extern int cpdf_numberGetAttachments();
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getAttachmentName(int n);
    [DllImport("libcpdf.so")] static extern int cpdf_getAttachmentPage(int n);
    //FIXME cpdf_getAttachmentData
    [DllImport("libcpdf.so")] static extern void cpdf_endGetAttachments();

    /* CHAPTER 13. Images. */
    [DllImport("libcpdf.so")] static extern int cpdf_startGetImageResolution(int pdf, double min_required_resolution);
    [DllImport("libcpdf.so")] static extern int cpdf_getImageResolutionPageNumber(int n);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getImageResolutionImageName(int n);
    [DllImport("libcpdf.so")] static extern int cpdf_getImageResolutionXPixels(int n);
    [DllImport("libcpdf.so")] static extern int cpdf_getImageResolutionYPixels(int n);
    [DllImport("libcpdf.so")] static extern double cpdf_getImageResolutionXRes(int n);
    [DllImport("libcpdf.so")] static extern double cpdf_getImageResolutionYRes(int n);
    [DllImport("libcpdf.so")] static extern void cpdf_endGetImageResolution();

    /* CHAPTER 14. Fonts. */
    [DllImport("libcpdf.so")] static extern void cpdf_startGetFontInfo(int pdf);
    [DllImport("libcpdf.so")] static extern int cpdf_numberFonts();
    [DllImport("libcpdf.so")] static extern int cpdf_getFontPage(int n);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getFontName(int n);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getFontType(int n);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_getFontEncoding(int n);
    [DllImport("libcpdf.so")] static extern void cpdf_endGetFontInfo();
    [DllImport("libcpdf.so")] static extern void cpdf_removeFonts(int pdf);
    [DllImport("libcpdf.so")] static extern void cpdf_copyFont(int docfrom, int docto, int range, int pagenumber, string fontname);


    /* CHAPTER 15. PDF and JSON */
    [DllImport("libcpdf.so")] static extern void cpdf_outputJSON(string filename, int parse_content, int no_stream_data, int pdf);


    /* CHAPTER 16. Optional Content Groups */
    [DllImport("libcpdf.so")] static extern int cpdf_startGetOCGList(int pdf);
    [DllImport("libcpdf.so")] static extern IntPtr cpdf_OCGListEntry(int n);
    [DllImport("libcpdf.so")] static extern void cpdf_endGetOCGList();
    [DllImport("libcpdf.so")] static extern void cpdf_OCGRename(int pdf, string name_from, string name_to);
    [DllImport("libcpdf.so")] static extern void cpdf_OCGOrderAll(int pdf);
    [DllImport("libcpdf.so")] static extern void cpdf_OCGCoalesce(int pdf);


    /* CHAPTER 17. Miscellaneous */
    [DllImport("libcpdf.so")] static extern void cpdf_draft(int pdf, int range, int boxes);
    [DllImport("libcpdf.so")] static extern void cpdf_removeAllText(int pdf, int range);
    [DllImport("libcpdf.so")] static extern void cpdf_blackText(int pdf, int range);
    [DllImport("libcpdf.so")] static extern void cpdf_blackLines(int pdf, int range);
    [DllImport("libcpdf.so")] static extern void cpdf_blackFills(int pdf, int range);
    [DllImport("libcpdf.so")] static extern void cpdf_thinLines(int pdf, int range, double min_thickness);
    [DllImport("libcpdf.so")] static extern void cpdf_copyId(int pdf_from, int pdf_to);
    [DllImport("libcpdf.so")] static extern void cpdf_removeId(int pdf);
    [DllImport("libcpdf.so")] static extern void cpdf_setVersion(int pdf, int version);
    [DllImport("libcpdf.so")] static extern void cpdf_setFullVersion(int pdf, int major, int minor);
    [DllImport("libcpdf.so")] static extern void cpdf_removeDictEntry(int pdf, string key);
    [DllImport("libcpdf.so")] static extern void cpdf_removeClipping(int pdf, int range);


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

        int cpdf_timesRoman = 0;
        int cpdf_timesBold = 1;
        int cpdf_timesItalic = 2;
        int cpdf_timesBoldItalic = 3;
        int cpdf_helvetica = 4;
        int cpdf_helveticaBold = 5;
        int cpdf_helveticaOblique = 6;
        int cpdf_helveticaBoldOblique = 7;
        int cpdf_courier = 8;
        int cpdf_courierBold = 9;
        int cpdf_courierOblique = 10;
        int cpdf_courierBoldOblique = 11;

        int cpdf_leftJustify = 0;
        int cpdf_CentreJustify = 1;
        int cpdf_RightJustify = 2;

        int cpdf_singlePage = 0;
        int cpdf_oneColumn = 1;
        int cpdf_twoColumnLeft = 2;
        int cpdf_twoColumnRight = 3;
        int cpdf_twoPageLeft = 4;
        int cpdf_twoPageRight = 5;

        int cpdf_useNone = 0;
        int cpdf_useOutlines = 1;
        int cpdf_useThumbs = 2;
        int cpdf_useOC = 3;
        int cpdf_useAttachments = 4;

        int cpdf_decimalArabic = 0;
        int cpdf_uppercaseRoman = 1;
        int cpdf_lowercaseRoman = 2;
        int cpdf_uppercaseLetters = 4;
        int cpdf_lowercaseLetters = 5;

        /* CHAPTER 0. Preliminaries */
        string[] argv = new string[] { };
        netcpdf_startup(argv); //FIXME real argv
        netcpdf_version();
        netcpdf_setSlow();
        netcpdf_setFast();
        //Console.WriteLine("lastError = %i\n", cpdf_lastError());
        //Console.WriteLine("lastErrorString = %s\n", Marshal.PtrToStringAuto(cpdf_lastErrorString()));
        netcpdf_onExit();

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
        int pdf15 = cpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        cpdf_scalePages(pdf15, cpdf_all(pdf15), 1.3, 1.5);
        cpdf_scaleToFit(pdf15, cpdf_all(pdf15), 200.0, 300.0, 0.9);
        cpdf_scaleToFitPaper(pdf15, cpdf_all(pdf15), cpdf_a4landscape, 0.9);
        cpdf_shiftContents(pdf15, cpdf_all(pdf15), 1.5, 0.9);
        cpdf_rotate(pdf15, cpdf_all(pdf15), 90);
        cpdf_rotateBy(pdf15, cpdf_all(pdf15), 90);
        cpdf_rotateContents(pdf15, cpdf_all(pdf15), 45.0);
        cpdf_upright(pdf15, cpdf_all(pdf15));
        cpdf_hFlip(pdf15, cpdf_all(pdf15));
        cpdf_vFlip(pdf15, cpdf_all(pdf15));
        cpdf_crop(pdf15, cpdf_all(pdf15), 100.0, 100.0, 200.0, 200.0);
        cpdf_removeCrop(pdf15, cpdf_all(pdf15));
        cpdf_removeTrim(pdf15, cpdf_all(pdf15));
        cpdf_removeArt(pdf15, cpdf_all(pdf15));
        cpdf_removeBleed(pdf15, cpdf_all(pdf15));
        cpdf_trimMarks(pdf15, cpdf_all(pdf15));
        cpdf_showBoxes(pdf15, cpdf_all(pdf15));
        cpdf_hardBox(pdf15, cpdf_all(pdf15), "/MediaBox");

        /* CHAPTER 4. Encryption */
        /* Encryption covered under Chapter 1 in cpdflib. */

        /* CHAPTER 5. Compression */
        int pdf16 = cpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        cpdf_compress(pdf16);
        cpdf_decompress(pdf16);
        cpdf_squeezeInMemory(pdf16);

        /* CHAPTER 6. Bookmarks */
        int pdf17 = cpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        cpdf_startGetBookmarkInfo(pdf17);
        int nb = cpdf_numberBookmarks();
        for (int b2 = 0; b2 < nb; b2++)
        {
            int level = cpdf_getBookmarkLevel(b2);
            int page = cpdf_getBookmarkPage(pdf17, b2);
            string text = Marshal.PtrToStringAuto(cpdf_getBookmarkText(b2));
            int open = cpdf_getBookmarkOpenStatus(b2);
            Console.WriteLine($"{level} {page} {text} {open}", level, page, text, open);
        }
        cpdf_endGetBookmarkInfo();

        cpdf_startSetBookmarkInfo(1);
        cpdf_setBookmarkLevel(0, 0);
        cpdf_setBookmarkPage(pdf17, 0, 1);
        cpdf_setBookmarkOpenStatus(0, 0);
        cpdf_setBookmarkText(0, "The text");
        cpdf_endSetBookmarkInfo(pdf17);


        /* CHAPTER 8. Logos, Watermarks and Stamps */
        int pdf20 = cpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int pdf21 = cpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        cpdf_stampOn(pdf20, pdf21, cpdf_all(pdf20));
        cpdf_stampUnder(pdf20, pdf21, cpdf_all(pdf20));
        cpdf_combinePages(pdf20, pdf21);
        cpdf_removeText(pdf20, cpdf_all(pdf20));
        int w = cpdf_textWidth(cpdf_timesBoldItalic, "foo");
        string name = Marshal.PtrToStringAuto(cpdf_stampAsXObject(pdf20, cpdf_all(pdf20), pdf20));

        /* CHAPTER 9. Multipage facilities */
        int pdf19 = cpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        cpdf_twoUp(pdf19);
        cpdf_twoUpStack(pdf19);
        cpdf_padBefore(pdf19, cpdf_all(pdf19));
        cpdf_padAfter(pdf19, cpdf_all(pdf19));
        cpdf_padEvery(pdf19, 6);
        cpdf_padMultiple(pdf19, 6);
        cpdf_padMultipleBefore(pdf19, 7);

        /* CHAPTER 10. Annotations */
        /* Not in the library version */

        /* CHAPTER 11. Document Information and Metadata */
        int pdf30 = cpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int lin = cpdf_isLinearized("testinputs/cpdfmanual.pdf");
        int v = cpdf_getVersion(pdf30);
        int v2 = cpdf_getMajorVersion(pdf30);
        string title = Marshal.PtrToStringAuto(cpdf_getTitle(pdf30));
        string author = Marshal.PtrToStringAuto(cpdf_getAuthor(pdf30));
        string subject = Marshal.PtrToStringAuto(cpdf_getSubject(pdf30));
        string keywords = Marshal.PtrToStringAuto(cpdf_getKeywords(pdf30));
        string creator = Marshal.PtrToStringAuto(cpdf_getCreator(pdf30));
        string producer = Marshal.PtrToStringAuto(cpdf_getProducer(pdf30));
        string creationdate = Marshal.PtrToStringAuto(cpdf_getCreationDate(pdf30));
        string modificationdate = Marshal.PtrToStringAuto(cpdf_getModificationDate(pdf30));
        string titlexmp = Marshal.PtrToStringAuto(cpdf_getTitleXMP(pdf30));
        string authorxmp = Marshal.PtrToStringAuto(cpdf_getAuthorXMP(pdf30));
        string subjectxmp = Marshal.PtrToStringAuto(cpdf_getSubjectXMP(pdf30));
        string keywordsxmp = Marshal.PtrToStringAuto(cpdf_getKeywordsXMP(pdf30));
        string creatorxmp = Marshal.PtrToStringAuto(cpdf_getCreatorXMP(pdf30));
        string producerxmp = Marshal.PtrToStringAuto(cpdf_getProducerXMP(pdf30));
        string creationdatexmp = Marshal.PtrToStringAuto(cpdf_getCreationDateXMP(pdf30));
        string modificationdatexmp = Marshal.PtrToStringAuto(cpdf_getModificationDateXMP(pdf30));
        cpdf_setTitle(pdf30, "title");
        cpdf_setAuthor(pdf30, "title");
        cpdf_setSubject(pdf30, "title");
        cpdf_setKeywords(pdf30, "title");
        cpdf_setCreator(pdf30, "title");
        cpdf_setProducer(pdf30, "title");
        cpdf_setCreationDate(pdf30, "title");
        cpdf_setModificationDate(pdf30, "title");
        cpdf_setTitleXMP(pdf30, "title");
        cpdf_setAuthorXMP(pdf30, "title");
        cpdf_setSubjectXMP(pdf30, "title");
        cpdf_setKeywordsXMP(pdf30, "title");
        cpdf_setCreatorXMP(pdf30, "title");
        cpdf_setProducerXMP(pdf30, "title");
        cpdf_setCreationDateXMP(pdf30, "title");
        cpdf_setModificationDateXMP(pdf30, "title");
        string datestr = Marshal.PtrToStringAuto(cpdf_dateStringOfComponents(1, 2, 3, 4, 5, 6));
        int rot = cpdf_getPageRotation(pdf30, 1);
        int hasbox = cpdf_hasBox(pdf30, 1, "/CropBox");
        cpdf_setMediabox(pdf30, cpdf_all(pdf30), 1.0, 2.0, 3.0, 4.0);
        cpdf_setCropBox(pdf30, cpdf_all(pdf30), 1.0, 2.0, 3.0, 4.0);
        cpdf_setTrimBox(pdf30, cpdf_all(pdf30), 1.0, 2.0, 3.0, 4.0);
        cpdf_setArtBox(pdf30, cpdf_all(pdf30), 1.0, 2.0, 3.0, 4.0);
        cpdf_setBleedBox(pdf30, cpdf_all(pdf30), 1.0, 2.0, 3.0, 4.0);
        cpdf_markTrapped(pdf30);
        cpdf_markUntrapped(pdf30);
        cpdf_markTrappedXMP(pdf30);
        cpdf_markUntrappedXMP(pdf30);
        cpdf_setPageLayout(pdf30, cpdf_singlePage);
        cpdf_setPageMode(pdf30, cpdf_useNone);
        cpdf_hideToolbar(pdf30, cpdf_true);
        cpdf_hideMenubar(pdf30, cpdf_true);
        cpdf_hideWindowUi(pdf30, cpdf_true);
        cpdf_fitWindow(pdf30, cpdf_true);
        cpdf_centerWindow(pdf30, cpdf_true);
        cpdf_displayDocTitle(pdf30, cpdf_true);
        cpdf_openAtPage(pdf30, cpdf_true, 1);
        cpdf_setMetadataFromFile(pdf30, "testinputs/cpdflibmanual.pdf");
        cpdf_removeMetadata(pdf30);
        cpdf_createMetadata(pdf30);
        cpdf_setMetadataDate(pdf30, "2000");
        cpdf_addPageLabels(pdf30, cpdf_decimalArabic, "PRE-", cpdf_all(pdf), cpdf_false);
        cpdf_removePageLabels(pdf30);
        string pl = Marshal.PtrToStringAuto(cpdf_getPageLabelStringForPage(pdf30, 1));
        int pls = cpdf_startGetPageLabels(pdf30);
        for (int plsc = 0; plsc < pls; plsc++)
        {
          int style = cpdf_getPageLabelStyle(plsc);
          string prefix = Marshal.PtrToStringAuto(cpdf_getPageLabelPrefix(plsc));
          int offset = cpdf_getPageLabelOffset(plsc);
          int lab_range = cpdf_getPageLabelRange(plsc);
        }
        cpdf_endGetPageLabels();

        /* CHAPTER 12. File Attachments */
        cpdf_attachFile("testinputs/cpdflibmanual.pdf", pdf30);
        cpdf_attachFileToPage("testinputs/cpdflibmanual.pdf", pdf30, 1);
        cpdf_removeAttachedFiles(pdf30);
        cpdf_startGetAttachments(pdf30);
        int n_a = cpdf_numberGetAttachments();
        for (int aa = 0; aa < n_a; aa++)
        {
          string a_n = Marshal.PtrToStringAuto(cpdf_getAttachmentName(aa));
          int a_page = cpdf_getAttachmentPage(aa);
        }
        cpdf_endGetAttachments();

        /* CHAPTER 13. Images. */
        int im_n = cpdf_startGetImageResolution(pdf30, 2.0);
        for (int im = 0; im < im_n; im++)
        {
          int im_p = cpdf_getImageResolutionPageNumber(im);
          string im_name = Marshal.PtrToStringAuto(cpdf_getImageResolutionImageName(im));
          int im_xp = cpdf_getImageResolutionXPixels(im);
          int im_yp = cpdf_getImageResolutionYPixels(im);
          double im_xres = cpdf_getImageResolutionXRes(im);
          double im_yres = cpdf_getImageResolutionYRes(im);
        }
        cpdf_endGetImageResolution();

        /* CHAPTER 14. Fonts. */
        cpdf_startGetFontInfo(pdf30);
        int fonts = cpdf_numberFonts();
        for (int ff = 0; ff < fonts; ff++)
        {
          int page = cpdf_getFontPage(ff);
          string f_name = Marshal.PtrToStringAuto(cpdf_getFontName(ff));
          string type = Marshal.PtrToStringAuto(cpdf_getFontType(ff));
          string encoding = Marshal.PtrToStringAuto(cpdf_getFontEncoding(ff));
        }
        cpdf_endGetFontInfo();
        cpdf_removeFonts(pdf30);
        cpdf_copyFont(pdf30, pdf30, cpdf_all(pdf30), 1, "/Font");

        /* CHAPTER 15. PDF and JSON */
        int pdf14 = cpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        cpdf_outputJSON("testoutputs/foo.json", cpdf_false, cpdf_true, pdf14);


        /* CHAPTER 16. Optional Content Groups */
        int pdf13 = cpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        int n2 = cpdf_startGetOCGList(pdf13);
        for(int x = 0; x < n2; x++)
        {
            Console.WriteLine(Marshal.PtrToStringAuto(cpdf_OCGListEntry(x)));
        }
        cpdf_endGetOCGList();
        cpdf_OCGRename(pdf13, "From", "To");
        cpdf_OCGOrderAll(pdf13);
        cpdf_OCGCoalesce(pdf13);

        /* CHAPTER 17. Miscellaneous */
        int pdf22 = cpdf_fromFile("testinputs/cpdflibmanual.pdf", "");
        cpdf_draft(pdf22, cpdf_all(pdf22), cpdf_false);
        cpdf_removeAllText(pdf22, cpdf_all(pdf22));
        cpdf_blackText(pdf22, cpdf_all(pdf22));
        cpdf_blackLines(pdf22, cpdf_all(pdf22));
        cpdf_blackFills(pdf22, cpdf_all(pdf22));
        cpdf_thinLines(pdf22, cpdf_all(pdf22), 1.0);
        cpdf_copyId(pdf22, pdf22);
        cpdf_removeId(pdf22);
        cpdf_setVersion(pdf22, 2);
        cpdf_setFullVersion(pdf22, 2, 0);
        cpdf_removeDictEntry(pdf22, "/Foo");
        cpdf_removeClipping(pdf22, cpdf_all(pdf22));
    }
}
}
