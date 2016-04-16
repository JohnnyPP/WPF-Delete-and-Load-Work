using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace DeleteCopyAndLoad
{
    public class Model : INotifyPropertyChanged
    {

        #region Properties

        private string[] _sourceFileNameAndPath;
        private bool _copyRange;

        private string _Source;

        public string Source
        {
            get { return _Source; }
            set
            {
                _Source = value;
                OnPropertyChanged("Source");
            }
        }

        private string _Destination;

        public string Destination
        {
            get { return _Destination; }
            set
            {
                _Destination = value;
                OnPropertyChanged("Destination");
            }
        }

        private string _DisplayLabel;

        public string DisplayLabel
        {
            get { return _DisplayLabel; }
            set
            {
                _DisplayLabel = value;
                OnPropertyChanged("DisplayLabel");
            }

        }

        #endregion

        public Model()
        {
            _Source = @"C:\Bilder\4";
            _Destination = @"C:\Bilder\";
        }

        public void DeleteClick()
        {
            var filesToDelete = new DirectoryInfo(_Destination);

            try
            {
                foreach (var file in filesToDelete.GetFiles())
                {
                    file.Delete();
                }
                DisplayLabel = "Images deleted";
            }
            catch (Exception e)
            {
                DisplayLabel = e.ToString();
            }    
        }

        public void CopyClick()
        {

            if (_copyRange)
            {
                string[] picList = Directory.GetFiles(_Source, "*.png");

                try
                {
                    foreach (string f in picList)
                    {
                        // Remove path from the file name.
                        string fName = f.Substring(_Source.Length + 1);

                        // Use the Path.Combine method to safely append the file name to the path.
                        // Will overwrite if the destination file already exists.
                        File.Copy(Path.Combine(_Source, fName), Path.Combine(_Destination, fName), true);
                    }

                    DisplayLabel = "Images copied";
                }
                catch (Exception e)
                {
                    DisplayLabel = e.ToString();
                }
            }
            //else
            //{
            //    try
            //    {
            //        foreach (string file in FileArray())
            //        {
            //            // Remove path from the file name.
            //            string fName = file.Substring(_sourceFileNameAndPath[0].Length + 1);

            //            // Use the Path.Combine method to safely append the file name to the path.
            //            // Will overwrite if the destination file already exists.
            //            File.Copy(Path.Combine(_sourceFileNameAndPath[0], fName), Path.Combine(_Destination, fName), true);
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        throw new Exception("Exception in CopyImages");
            //    }
            //}
        }

        private string[] FileArray(string fileName, string sourcePath)
        {
            var imageListIn = Directory.GetFiles(sourcePath, "*.png").ToList();
            string[] imageArrayOut = null;
            int MatchingIndex;

            //var imageListIn = imageArrayIn.ToList();
            //var imageList = new List<string>(imageArrayIn);

            MatchingIndex = imageListIn.IndexOf(fileName);

            imageListIn.RemoveRange(MatchingIndex, 2); //remove a range of items.

            //https://msdn.microsoft.com/en-us/library/y33yd2b5(v=vs.110).aspx
            // probaly need to invert the selectiuon
            //int MatchingIndex;

            //for (int i = 0; i < imageArrayIn.Length; i++)
            //{
            //    if (imageArrayIn[i] == fileName)
            //    {
            //        MatchingIndex = i;
            //    }

            //}

            return imageArrayOut;
        }

        public string[] CopyRangeClick()
        {
            _copyRange = true;
            //var sourceFileNameAndPath = new[] { (string)null, (string)null };
            //string sourcePath = null;
            //string sourceFileName = null;

            var dlg = new OpenFileDialog
            {
                DefaultExt = ".png",
                Filter =
                    "All image files (*.bmp;*.jpg;*.png;*.gif;*.tif)|*.bmp;*.jpg;*.png;*.gif;*.tif|Bitmap files (*.bmp)|*.bmp|JPEG files (*.jpg)|*.jpg|Portable network graphics files (*.png)|*.png|Gif files (*.gif)|*.gif|Tagged image format files (*.tif)|*.tif|All files (*.*)|*.*",
                RestoreDirectory = true
            };
            // Show open file dialog box
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                _sourceFileNameAndPath[0] = dlg.FileName;
                _sourceFileNameAndPath[1] = Path.GetDirectoryName(dlg.FileName);
            }

            return _sourceFileNameAndPath;
        }

        public void ChooseFolderClick()
        {
            _copyRange = false;

            var dlg = new OpenFileDialog
            {
               // FileName = "Document",
                DefaultExt = ".png",
                Filter =
                    "All image files (*.bmp;*.jpg;*.png;*.gif;*.tif)|*.bmp;*.jpg;*.png;*.gif;*.tif|Bitmap files (*.bmp)|*.bmp|JPEG files (*.jpg)|*.jpg|Portable network graphics files (*.png)|*.png|Gif files (*.gif)|*.gif|Tagged image format files (*.tif)|*.tif|All files (*.*)|*.*",
                RestoreDirectory = true
            };
            // Show open file dialog box
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                try
                {
                    string directoryPath = Path.GetDirectoryName(dlg.FileName);
                    //DisplayLabel = dlg.FileName;
                    DisplayLabel = directoryPath;
                    Source = directoryPath;
                }
                catch (Exception e)
                {
                    DisplayLabel = e.ToString();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
