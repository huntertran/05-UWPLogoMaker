namespace UWPLogoMaker.Utilities
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    public class StorageHelper
    {
        #region Check exist

        /// <summary>
        /// Check if a folder is existed with its path
        /// </summary>
        /// <param name="path">Path to folder</param>
        /// <returns></returns>
        public static bool IsFolderExisted(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// Check if a file is existed with its path
        /// </summary>
        /// <param name="fileName">path to file, not include Local folder root</param>
        /// <param name="folder">Folder to check, if null, get Local Folder</param>
        /// <param name="isCheckFileContent"></param>
        /// <returns></returns>
        public static bool IsFileExisted(string fileName, StorageFolder folder = null, bool isCheckFileContent = false)
        {
            var result = false;
            if (folder == null)
            {
                folder = ApplicationData.Current.LocalFolder;
            }

            if (!string.IsNullOrEmpty(fileName) && File.Exists(string.Concat(folder.Path, "\\", fileName)))
            {
                if (isCheckFileContent)
                {
                    var info = new FileInfo(folder.Path + "\\" + fileName);
                    if (info.Length > 0)
                    {
                        result = true;
                    }
                }
                else
                {
                    result = true;
                }
            }

            return result;
        }

        #endregion

        public static async Task<T> Json2Object<T>(string fileName, StorageFolder folder = null)
        {
            if (folder == null)
            {
                folder = ApplicationData.Current.RoamingFolder;
            }

            if (IsFileExisted(fileName, folder))
            {
                var file = await folder.GetFileAsync(fileName);
                using (var x = await file.OpenStreamForReadAsync())
                {
                    var reader = new StreamReader(x);
                    var json = reader.ReadToEnd();
                    var jObject = JObject.Parse(json);
                    var data = jObject.ToObject<T>();
                    return data;
                }
            }

            return default(T);
        }

        public static async Task Object2Json<T>(T data, string fileName, StorageFolder folder = null)
        {
            StorageFile file;
            if (folder == null)
            {
                var localFolder = ApplicationData.Current.RoamingFolder;
                file = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            }
            else
            {
                file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            }

            var serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (var x = await file.OpenStreamForWriteAsync())
            {
                using (var sw = new StreamWriter(x))
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, data);
                    }
                }
            }
        }
    }
}