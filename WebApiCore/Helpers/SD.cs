using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Helpers
{
    public static class SD
    {

        public const string Admin = "Admin";
        public const string Member = "Member";
        public const string AdminOnly = "RequiredAdminRole";
        public const int Retry = 3;
        public const int MaxFileSize = 209715200;
        public const long Large = 3221225472;
        public const long Light = 1073741824;
        public const long Meduim = 2147483648;
  

        static SD()
        {
            if (FileSizeStore == null)
            {
                FileSizeStore = new Dictionary<FileSize, long>();
            }
            FileSizeStore.Add(FileSize.Normal, 0);
            FileSizeStore.Add(FileSize.Low_Heavy, 2);
            FileSizeStore.Add(FileSize.Medium_Heavy, 3);
            FileSizeStore.Add(FileSize.Large, 5);
        }

        public static Dictionary<FileSize,long>  FileSizeStore { get; set; }

        public static FileSize MapFileSize(this long fileSize)
        {
            if (fileSize == Light)
            {
                return FileSize.Low_Heavy;
            }
            else if (fileSize > Light && fileSize <= Meduim)
            {
                return FileSize.Medium_Heavy;
            }
            else if (fileSize > Light && fileSize <= Large)
            {
                return FileSize.Medium_Heavy;
            }
            else if (fileSize > Large)
            {
                return FileSize.Medium_Heavy;
            }
            else
            {
                return FileSize.Normal;
            }
        }
    }
    public enum FileSize
    {
        Normal,
        Low_Heavy,
        Medium_Heavy,
        Large
    }
}
