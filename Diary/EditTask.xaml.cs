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
using Microsoft.EntityFrameworkCore;

namespace Diary
{
    /// <summary>
    /// Логика взаимодействия для EditTask.xaml
    /// </summary>
    public partial class EditTask : Window
    {
        Task task { get; set; }
        Day selectedDay { get; set; }
        public EditTask(Task current)
        {
            InitializeComponent();
            task = current;
            selectedDay = Data.Context().Days.First(x => x.Id == current.DayId);
            Load();
        }

        void Load()
        {
            nameTB.Text += task.Name;
            descriptionTB.Text += task.Description;
            dateTB.Text += Data.Context().Days.First(x => x.Id == task.DayId).Date.ToString();

            nameTBox.Text = task.Name;
            descriptionTBox.Text = task.Description;
            datePicker.SelectedDate = Data.Context().Days.First(x => x.Id == task.DayId).Date;

        }

        private void ClickBack(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите отменить редактирование задачи?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.DialogResult = false;
            }
        }

        private void ClickDone(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(nameTBox.Text) && !string.IsNullOrEmpty(descriptionTBox.Text))
            {
                Day? day = Data.Context().Days.FirstOrDefault(x => x.Date == datePicker.SelectedDate);
                if (day == null)
                {
                    Data.Context().Days.Add(new Day { Date = datePicker.SelectedDate });
                    Data.Context().SaveChanges();
                }
                Task editTask = Data.Context().Tasks.First(x => x.Id == task.Id);
                editTask.Name = nameTBox.Text;
                editTask.Description = descriptionTBox.Text;
                editTask.DayId = Data.Context().Days.First(x => x.Date == datePicker.SelectedDate).Id;
                Data.Context().SaveChanges();
                Day dayCheck = Data.Context().Days.Include(x => x.Tasks).First(x => x.Id == selectedDay.Id);
                if (dayCheck.Tasks.Count == 0)
                {
                    Data.Context().Days.Remove(dayCheck);
                    Data.Context().SaveChanges();
                }
                this.DialogResult = true;
            }
            else
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
