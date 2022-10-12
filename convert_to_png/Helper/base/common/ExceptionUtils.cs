using System;
using System.IO;
using System.Security;

namespace convert_to_png
{
    public static class ExceptionUtils
    {
        public static bool IsFileSystemException(this Exception ex) =>
            ex is PathTooLongException || ex is UnauthorizedAccessException || ex is SecurityException || ex is IOException;
    }
}

