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
                return  RegexMethod(contentHtml); ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static string RegexMethod(string contentHtml)
        {
            if (string.IsNullOrWhiteSpace(contentHtml))
            {
                return null;
            }
            return  Regex.Replace(contentHtml, @"<[^>]+>|&nbsp;|<span>|&mdash|&raquo", string.Empty);
        }

        public static string GroupString(string content)
        {
            var masssivCar = new char[] { ' ', '\r', '\n', ',', '.', ';',':', '(', ')' }; // todo качество поиск
            List<string> contentList = content.Split(masssivCar).ToList();
            var newcontentList = new List<string>();
            foreach (var item in contentList)
            {
                if (!string.IsNullOrEmpty(item))
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
                if (item.Name != null)
                {
                    rezList += $"{item.Name} - {item.Count} \n";
                }
              
            }

            return rezList;
        }
    }

}