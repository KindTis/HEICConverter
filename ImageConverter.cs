using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;

namespace HEICConverter
{
    public class ImageConverter
    {
        public void Excution(string workDir, bool delOriginal)
        {
            string[] fileEntries = Directory.GetFiles(workDir);
            if (fileEntries.Length > 0)
            {
                Console.WriteLine("Working Directory :" + workDir);
            }
            foreach (string fileName in fileEntries)
            {
                if (Path.GetExtension(fileName).ToLower() == ".heic")
                    _ConvertImage(fileName, delOriginal);
            }

            string[] subdirectoryEntries = Directory.GetDirectories(workDir);
            foreach (string subdirectory in subdirectoryEntries)
                Excution(subdirectory, delOriginal);
        }

        private void _ConvertImage(string fileName, bool delOriginal)
        {
            using (var image = new MagickImage(fileName))
            {
                string newFileName = fileName.Replace(Path.GetExtension(fileName), ".jpg");
                image.Write(newFileName);
                Console.WriteLine(Path.GetFileName(fileName) + " -> " + Path.GetFileName(newFileName));
                if (delOriginal)
                    DeleteFile.MoveToRecycleBin(fileName);
            }
        }
    }
}
