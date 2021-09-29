using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Volga_IT_21.Core;
using Volga_IT_21.WPF.Servis;

namespace Volga_IT_21.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IMessageExeption _messageExeption = Dependence.GetMessageExeption();
        IReadHTML _readHTML = Dependence.GetReadHTML();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// найти исходный файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async  void buttonOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "HTMLfiles (*.HTML)|*.HTML|All files (*.*)|*.*" };
            if (openFileDialog.ShowDialog() == true)
            {
                textBlocContent.Content = string.Empty;
                textBlocContent.Content = "подождите идет загрузка";
                var content = await Task.Run(() => RunAsync(openFileDialog.FileName));

                if ( content != null)
                {
                    textBlocContent.Content = content;
                }
            }
        }

        /// <summary>
        /// получаем контент  из БЛ
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private  async Task<string> RunAsync(string name)
        {
            try
            {
                return await Task.Run(() => _readHTML.GetStringContent(name));
            }
            catch (Exception ex)
            {
                _messageExeption.Error($"Ошибка открытия файла \n {ex.Message}");
                return null;
            }
        }

        private void buttonOpenConsole_Click(object sender, RoutedEventArgs e)
        {
            Assembly asm = Assembly.LoadFrom(@"Volga_IT_21.ConsoleApp.dll");
            List<Type> types = asm.GetTypes().ToList();

            Type pr = types[0];

            // создаем экземпляр класса Program
            object programObj = Activator.CreateInstance(pr);

            ConsoleHelper.AllocConsole();
            MethodInfo method = pr.GetMethod("StartMetod", BindingFlags.DeclaredOnly |
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
            method.Invoke(programObj, new object[] { textBlocContent.Content });

        }
    }
}