using Assessment.Repository.Interfaces;
using log4net;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Assessment.Repository.Data
{
    public class OutputWritter : IOutputWritter
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
       
        private string folder = Environment.CurrentDirectory + @"\Output";

        public OutputWritter()
        {
            CleanupFolder();
        }

        private void  CleanupFolder()
        {
            DirectoryInfo di = new DirectoryInfo(folder);
            FileInfo[] files = di.GetFiles("Output*.csv")
                                 .Where(p => p.Extension == ".csv").ToArray();
            foreach (FileInfo file in files)
                try
                {
                    file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
                catch (Exception ex)
                {
                    _logger.Error("Failed to delete output files");
                    _logger.Error(ex.Message);
                    _logger.Error(ex.StackTrace);
                }
        }

        /// <summary>
        /// Opens the folder onto which the output files have been saved.
        /// </summary>
        public void OpenLogDirectory()
        {
            try
            {
                System.Diagnostics.Process.Start(folder);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
            }
        }


        /// <summary>
        /// Writes to output file
        /// </summary>
        /// <param name="message">the message to be written</param>
        public void WriteToOutPutFile(StringBuilder message)
        {
            string  _filename = Path.Combine(folder, String.Format("{0}{1}{2}", "OutPutFile_", Guid.NewGuid(), ".csv"));
            if (File.Exists(_filename))
            {
                try
                {
                    File.AppendAllText(_filename, message.ToString());
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    _logger.Error(ex.StackTrace);
                }
            }

            else
            {
                try
                {
                    File.WriteAllText(_filename, message.ToString());
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    _logger.Error(ex.StackTrace);
                }
            }
        }       
    }
}
