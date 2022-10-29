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
using System.Windows.Shapes;
using Diary.Database;

namespace Diary
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void ClickBack(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void ClickDone(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(loginTB.Text) && !string.IsNullOrEmpty(passTB.Text) && !string.IsNullOrEmpty(secondnameTB.Text) && !string.IsNullOrEmpty(firstnameTB.Text) && !string.IsNullOrEmpty(middlenameTB.Text))
            {
                User user = new User();
                user.Login = loginTB.Text;
                user.Password = passTB.Text;
                user.Fio = $"{secondnameTB.Text} {firstnameTB.Text} {middlenameTB.Text}";
                Data.Context().Users.Add(user);
                Data.Context().SaveChanges();
                this.DialogResult = true;
            }
            else
                MessageBox.Show("Заполните все поля!","",MessageBoxButton.OK,MessageBoxImage.Warning);
        }
    }
}
