using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CouchPotato.Helper
{
    public class DirectoryHelper
    {
        // Scans the directories and returns MovieName and its Path
        public static List<KeyValuePair<string, string>> GetMoviePath(String path)
        {
            List<string> fileList = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).ToList();
            List<KeyValuePair<string, string>> kvpList = new List<KeyValuePair<string, string>>();
            foreach (var file in fileList)
            {
                if (Path.GetExtension(file) == ".avi" || Path.GetExtension(file) == ".mp4" || Path.GetExtension(file) == ".mkv" || Path.GetExtension(file) == ".wmv")
                {
                    kvpList.Insert(0, new KeyValuePair<string, string>(Path.GetFileNameWithoutExtension(file), (Path.GetDirectoryName(file)).Substring(2)));
                }
            }
            return kvpList;
        }
    }
}