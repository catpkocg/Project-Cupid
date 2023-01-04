using System;
using System.IO;

namespace Wayway.Engine.Editor
{
    public static class FileUtility
    {
        public static bool FindFile(string directory, string fileName)
        {
            return File.Exists($"{directory}/{fileName}");
        }
        
        public static bool CopyFolder(string sourcePath, string targetPath)
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
       
            if(!Directory.Exists(sourcePath))
                return false;
       
            var files = Directory.GetFiles(sourcePath);
            foreach(var i in files)
            {
                var extension = Path.GetExtension(i);
                if(string.Compare(extension, ".txt", StringComparison.Ordinal) != 0)// SVN파일이 같이 와서 필터기능이 필요로 하다.
                    continue;
           
                var fileName = Path.GetFileName(i);
                var destFile = Path.Combine(targetPath, fileName);
                File.Copy(i, destFile, true);
            }
            return true;
        }
    }
}