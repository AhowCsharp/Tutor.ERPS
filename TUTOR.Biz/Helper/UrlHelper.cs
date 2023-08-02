using Flurl;
using System.Reflection;

namespace TUTOR.Biz.Helpers
{
    public static class UrlHelper
    {
        /// <summary>
        /// 圖片路徑
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToAbsoluteUri(string path)
        {
            var url = string.Empty;
            if (!string.IsNullOrEmpty(path))
            {
                url = Url.Combine("https://eip.interasia.cc", path);
            }

            return url;
        }

        /// <summary>
        /// 檔案位置絕對路徑
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToFilePath(string path)
        {
            var url = "";
            url = "/var/www/Web/WebApi/updates/";
#if (DEBUG)
            url = "D:\\EC\\ialw\\build\\updates\\";
#endif

          
            if (!string.IsNullOrEmpty(path))
            {
               
#if (DEBUG)
                url += path + "\\";
#else
                url += path + "/";
#endif

            }

            return url;
        }

        /// <summary>
        /// 檔案下載路徑
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToFileDownloadPath(string path)
        {
            var url = string.Empty;
            if (!string.IsNullOrEmpty(path))
            {
                if (path.Contains("/Updates"))
                {
                    url = Url.Combine("http://10.11.64.131/download/", path);
                }
                else
                {
                    url = Url.Combine("http://10.11.64.131/download/updates/", path);
                }
              
            }

            return url;
        }

    }
}
