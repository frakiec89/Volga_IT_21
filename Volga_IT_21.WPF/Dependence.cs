using System;
using Volga_IT_21.BL;
using Volga_IT_21.Core;

namespace Volga_IT_21.WPF
{

    /// <summary>
    /// список всех зависимостей 
    /// </summary>
    internal class Dependence 
    {
        internal static IMessageExeption GetMessageExeption()
        {
            return new Servis.MessageExeption(); // инициализация класса MessageExeption
        }

        internal static IReadHTML GetReadHTML()
        {
            return new BL.ReadHTML();//  инициализация класса ReadHTML
        }
    }
}