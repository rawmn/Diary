using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Diary.Database;

namespace Diary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            regTB.Foreground = Brushes.DarkRed;
            regTB.TextDecorations = TextDecorations.Underline;
            regTB.FontSize = 15;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            regTB.Foreground = Brushes.DarkBlue;
            regTB.TextDecorations = null;
            regTB.FontSize = 14;
        }

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            if (registration.ShowDialog() == true)
            {
                MessageBox.Show("Вы успешно зарегистрировались!");
            }
            else
                MessageBox.Show("Регистрация отменена!");
        }

        private void ClickLogin(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(loginTB.Text) && !string.IsNullOrEmpty(passPB.Password))
            {
                User? user = Data.Context().Users.FirstOrDefault(x=>x.Login == loginTB.Text && x.Password == passPB.Password);
                if (user != null)
                {
                    DiaryWindow diary = new DiaryWindow(user);
                    diary.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Заполните поля для логина и пароля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
