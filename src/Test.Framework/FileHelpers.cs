using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Test.Framework
{
    public static class FileHelpers
    {
        public static bool IsOfFormat(this FileInfo fileInfo, IEnumerable<string> formats)
        {
            return formats.Any(format => format.Equals(fileInfo.Extension, StringComparison.OrdinalIgnoreCase));
        }
    }
}
