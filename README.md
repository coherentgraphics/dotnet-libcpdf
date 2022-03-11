# Coherent PDF Library for .NET

CoherentPDF is a .NET interface to the cpdf pdf tools. More information,
including commercial licenses can be found at <https://www.coherentpdf.com/>

The sofware is free for non-commercial use. The non-commercial home of this
software is <http://community.coherentpdf.com/>


Functionality
-------------

* Quality Split and Merge, keeping bookmarks. Extract pages. Split on Bookmarks.
* Encrypt and Decrypt (including AES 128 and AES 256 encryption)
* Scale, rotate, crop and flip pages. Scale pages to fit
* Copy, Remove and Add bookmarks
* Stamp logos, watermarks, page numbers and multiline text. Transparency.
* Supports Unicode UTF8 text input and output
* Put multiple pages on a single page
* List, copy, or remove annotations
* Read and set document information and metadata
* Add and remove file attachments to document or page.
* Thicken hairlines, blacken text, make draft documents
* Reconstruct malformed files
* Detect missing fonts, low resolution images
* Read and write PDF as JSON

Obtaining and installing the DLLs
---------------------------------

The DLL `libcpdf` is required. This is free for non-commercial use. Commercial
use requires a license. The DLL can be obtained here for all platforms here:

<https://github.com/coherentgraphics/cpdflib-binary>

Before using the library, you must make sure your project or build environment
has access to the cpdf DLL, which is not part of the .NET assembly obtained
from NuGet. You can add it to a Visual Studio project as a file, set to
copy-to-output-folder. Or, you can install it in a standard location.

The DLL must be named as follows, for .NET to be able to find it:

Windows: cpdf.dll
MacOS: libcpdf.dylib
Linux: libcpdf.so

So you should rename the DLL you download from the URL above before use.

Documentation
-------------

Full manual (required reading): <https://coherentpdf.com/dotnetcpdflibmanual.pdf>

Follow the instructions at the end of Chapter 1 to write your first program.

In addition, the NuGet package provides Intellisense documentation with each function.

Contact
-------

<mailto:contact@coherentgraphics.co.uk>

Bug reports: <https://github.com/coherentgraphics/dotnet-libcpdf>
