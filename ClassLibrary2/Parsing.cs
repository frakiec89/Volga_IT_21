using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Volga_IT_21.BL
{
    internal class Parsing
    {
        /// <summary>
        /// метод  обработки документа
        /// </summary>
        /// <param name="contentHtml"></param>
        /// <returns>Возвращает  строку с контентом в порядке убывания</returns>
        internal static string HTML(string contentHtml)
        {
            try
            {
                List<string> contentList = RegexMethod(contentHtml);
                return GroupString(contentList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        private static List<string> RegexMethod(string contentHtml)
        {
            var masssivCar = new char[] { ' ', '\r', '\n', ',', '.', ':' , '(' , ')' }; // todo качество поиска

            string content = Regex.Replace(contentHtml, @"<[^>]+>|&nbsp;", "");
            var contentList = content.Split(masssivCar).ToList();
            return contentList;
        }

        private static string GroupString(List<string> contentList)
        {
            var newcontentList = new List<string>();

            foreach (var item in contentList)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    newcontentList.Add(item);
                }
            }

             var rez = from r in newcontentList
                      group r by r.ToUpper() into g
                      select (Name: g.Key, Count: g.Count());

            string rezList = string.Empty;

            foreach (var item in rez.OrderByDescending(x=>x.Count).ThenBy(x=>x.Name))
            {
                rezList += $"{item.Name} - {item.Count} \n";
            }
            return rezList;
        }
    }

}