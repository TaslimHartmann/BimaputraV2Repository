using System.Collections;
using System.Collections.Generic;
using WpfControls;

namespace BPDMH.Tools
{
    class FilesystemSuggestionProvider : ISuggestionProvider
    {
        public IEnumerable GetSuggestions(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return null;
            }
            if (filter.Length < 3)
            {
                return null;
            }

            if (filter[1] != ':')
            {
                return null;
            }

            var lst = new List<System.IO.FileSystemInfo>();
            var dirFilter = "*";
            var dirPath = filter;
            if (!filter.EndsWith("\\"))
            {
                var index = filter.LastIndexOf("\\", System.StringComparison.Ordinal);
                dirPath = filter.Substring(0, index + 1);
                dirFilter = filter.Substring(index + 1) + "*";
            }
            var dirInfo = new System.IO.DirectoryInfo(dirPath);
            lst.AddRange(dirInfo.GetFileSystemInfos(dirFilter));
            System.Threading.Thread.Sleep(2000);
            return lst;
        }
    }
}
