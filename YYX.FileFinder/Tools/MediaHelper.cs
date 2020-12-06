using System.Collections.Generic;

namespace YYX.FileFinder.Tools
{
    public class MediaHelper
    {
        #region Known video extensions
        public static List<string> VideoExtensions = new List<string> {
            {".vob"},
            {".bup"},
            {".ifo"},
            {".rmvb"},
            {".mov"},
            //{".avi"},
            {".mpg"},
            {".mpeg"},
            {".wmv"},
            {".mp4"},
            {".mkv"},
            {".divx"},
            {".dvr-ms"},
            {".ogm"}
        };
        #endregion 

        public static bool IsVideo(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            string extension = System.IO.Path.GetExtension(path)?.ToLower();
            bool isVideo = VideoExtensions.Contains(extension);
            return isVideo;
        }
    }
}
