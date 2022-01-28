using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Mead
{
    /// <summary>
    /// Static class used to handle reading in file bytes.
    /// </summary>
    static class FileHandler
    {
        public static byte[] GetFileBytes(string path)
        {
            byte[] fileBytes = null;
            try
            {
                fileBytes = File.ReadAllBytes(Path.GetFullPath(path));
            }
            catch (Exception e)
            {
              Console.WriteLine("[MEAD][ERROR] Unable to open specified file.");
            }

          return fileBytes;
        }

        public static byte[] GetLocalFileBytes(string path)
        {
            byte[] fileBytes = null;
            try
            {
                fileBytes = File.ReadAllBytes(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("[MEAD][ERROR] Unable to open specified file.");
            }

            return fileBytes;
        }
    }
}
