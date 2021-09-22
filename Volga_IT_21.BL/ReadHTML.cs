using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string line = string.Empty;
            try
            {
                    line = StreamMethod(fileName);
                    return Parsing.HTML(line);
                
            }
            catch(OutOfMemoryException ex)
            {
                throw new Exception("Ошибка памяти " +  ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        private static string StreamMethod(string name)
        {
            using (StreamReader sr = new StreamReader(name))
            {
                return sr.ReadToEnd();
            }
               
        }
    }
}
