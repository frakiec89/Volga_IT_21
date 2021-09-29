using System;
using System.IO;
namespace Volga_IT_21.BL
{
    public class ReadHTML : Core.IReadHTML
    {
        /// <summary>
        /// чтение  файла HTML
        /// </summary>
        /// <param name="fileName">Путь к файлу</param>
        /// <returns>Контент в  виде необработанной  строки</returns>
        public   string  GetStringContent(string fileName)
        {
            try
            {
                return StreamMethod(fileName);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private static string StreamMethod(string name)
        {
            string content = string.Empty;
            try
            {
                using (FileStream fs = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (BufferedStream bs = new BufferedStream(fs))
                using (StreamReader sr = new StreamReader(bs))
                {
                    string line;
                    string bufer = string.Empty;
                    while ((line = sr.ReadLine()) != null)
                    {
                        bufer += line;
                        if (line.EndsWith(">"))
                        {
                            content += Parsing.HTML(bufer) + " ";
                            bufer = string.Empty;
                            continue;
                        }

                        content += Parsing.HTML(line) +" ";
                    }
                    return Parsing.GroupString(content);
                }
            }
            catch (OutOfMemoryException ex)
            {
                throw new Exception("Ошибка памяти " + ex.Message);
            }
        }
    }
}