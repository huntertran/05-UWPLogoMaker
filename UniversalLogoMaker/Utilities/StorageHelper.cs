namespace UniversalLogoMaker.Utilities
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Windows.Storage;

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
            bool result = false;
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

                StorageFile file = await folder.GetFileAsync(fileName);
                using (Stream x = await file.OpenStreamForReadAsync())
                {
                    StreamReader reader = new StreamReader(x);
                    string json = reader.ReadToEnd();
                    JObject jObject = JObject.Parse(json);
                    T data = jObject.ToObject<T>();
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
                StorageFolder localFolder = ApplicationData.Current.RoamingFolder;
                file = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            }
            else
            {
                file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            }

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (Stream x = await file.OpenStreamForWriteAsync())
            {
                using (StreamWriter sw = new StreamWriter(x))
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
