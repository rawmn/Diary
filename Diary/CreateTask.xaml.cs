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
using Task = Diary.Database.Task;
using Diary.Database;

namespace Diary
{
    /// <summary>
    /// Логика взаимодействия для CreateTask.xaml
    /// </summary>
    public partial class CreateTask : Window
    {
        public User user { get; set; }
        public CreateTask(User auth)
        {
            InitializeComponent();
            user = auth;
        }

        private void ClickDone(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(nameTB.Text) && !string.IsNullOrEmpty(descriptionTB.Text) && datePicker.SelectedDate != null)
            {
                Day? day = Data.Context().Days.FirstOrDefault(x => x.Date == datePicker.SelectedDate);
                if (day == null)
                {
                    Data.Context().Days.Add(new Day { Date = datePicker.SelectedDate });
                    Data.Context().SaveChanges();
                }
                Task task = new Task()
                {
                    Name = nameTB.Text,
                    Description = descriptionTB.Text,
                    Status = false,
                    UserId = user.Id,
                    DayId = Data.Context().Days.First(x => x.Date == datePicker.SelectedDate).Id
                };
                Data.Context().Tasks.Add(task);
                if (Data.Context().SaveChanges() == 1)
                {
                    MessageBox.Show("Изменения успешно сохранены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                }
            }
            else
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ClickBack(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Отменить добавление задачи?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.DialogResult = false;
            }
        }
    }
}
