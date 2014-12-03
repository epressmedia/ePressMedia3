using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPM.Core
{

    /// <summary>
    /// Summary description for FileHelper
    /// </summary>
    public static class FileHelper
    {


        public static Boolean FileExists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 안전한 파일 저장을 위해 중복되지 않은 파일명을 얻는다. 
        /// </summary>
        /// <param name="filePath">저장할 파일명</param>
        /// <returns>해당 파일이 존재하지 않으면 filePath 와 같은 내용의 문자열을 리턴한다. 
        /// 해당 파일이 존재하면존재한다면 '경로/파일명_숫자.확장자' 형식의 이름을 리턴한다.</returns>
        public static string GetSafeFileName(string filePath)
        {
            int i = 1;
            string newName = filePath;
            while (File.Exists(newName))
            {
                newName = Path.GetDirectoryName(filePath) + "\\" +
                           GetValidatFileName(Path.GetFileNameWithoutExtension(filePath)) +
                           "_" + i + Path.GetExtension(filePath);
                i++;
            }

            return newName;
        }

        private static string GetValidatFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }


        /// <summary>
        /// Confirm and get a new file name in case of duplicate file name
        /// </summary>
        /// <param name="directory">Target Directory Name</param>
        /// <param name="fileName">file name</param>
        /// <returns>Returns a unique file name in the specified directory.</returns>
        public static string GetSafeFileName(string directory, string fileName)
        {
            if (!directory.EndsWith("\\"))
                directory = directory + "\\";

            string newName = fileName;

            int i = 1;
            while (File.Exists(directory + newName))
            {
                newName = GetValidatFileName(Path.GetFileNameWithoutExtension(fileName)) +
                           "_" + i + Path.GetExtension(fileName);
                i++;
            }

            return newName;
        }


        public static string GetSafeFileName(System.Web.UI.Page page, string path, string fileName)
        {
            string directory = page.MapPath(path);
            if (!directory.EndsWith("\\"))
                directory = directory + "\\";

            string newName = fileName;

            int i = 1;
            while (File.Exists(directory + newName))
            {
                newName = GetValidatFileName(Path.GetFileNameWithoutExtension(fileName)) +
                           "_" + i + Path.GetExtension(fileName);
                i++;
            }

            return newName;
        }





        public static IDictionary<string, string> GetFiles(HttpServerUtility page, string path, string extension, bool include_sub)
        {
            IDictionary<string, string> FileList = new Dictionary<string, string>();

            string physical_path = page.MapPath(path);

            // This path is a file
            FileList = ProcessFile(page, FileList, path, extension);


            if (include_sub)
            {
                string[] dirs = Directory.GetDirectories(physical_path);
                foreach (string dir in dirs)
                {
                    FileList = ProcessDir(page, FileList, dir, extension);
                }

            }

            return FileList;
        }

        public static string[] GetDirectories(HttpServerUtility page, string parent_folder_path)
        {
            string physical_path = page.MapPath(parent_folder_path);
            return Directory.GetDirectories(physical_path);
        }

        public static string GetLastDirectoryInFullPath(string fullpath)
        {
            return fullpath.Substring(fullpath.LastIndexOf('\\') + 1, fullpath.Length - fullpath.LastIndexOf('\\') - 1);
        }



        private static IDictionary<string, string> ProcessFile(HttpServerUtility page, IDictionary<string, string> filelist, string path, string extension)
        {

            string[] filePaths = Directory.GetFiles(page.MapPath(path), "*." + extension);
            foreach (string filename in filePaths)
            {
                filelist.Add(Path.GetFileName(filename), filename.Replace(HttpRuntime.AppDomainAppPath, "/").Replace(Path.DirectorySeparatorChar, '/'));
            }
            return filelist;
        }

        private static IDictionary<string, string> ProcessDir(HttpServerUtility page, IDictionary<string, string> filelist, string path, string extension)
        {

            string[] filePaths = Directory.GetFiles(path, "*." + extension);
            foreach (string filename in filePaths)
            {
                filelist.Add(Path.GetFileName(filename), filename.Replace(HttpRuntime.AppDomainAppPath, "/").Replace(Path.DirectorySeparatorChar, '/'));
            }
            return filelist;

            //return ProcessFile(page, filelist, path, extension);
        }


        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static bool FolderExists(string path, bool CreateIfNot)
        {
            bool exists = false;
            DirectoryInfo dir = new DirectoryInfo(path);

            if (!dir.Exists)
            {
                if (CreateIfNot)
                {
                    dir.Create();
                    exists = true;
                }
            }
            else
            {
                exists = true;
            }

            return exists;
        }
    }
}