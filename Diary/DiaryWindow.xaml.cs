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
using Microsoft.EntityFrameworkCore;
using Task = Diary.Database.Task;

namespace Diary
{
    /// <summary>
    /// Логика взаимодействия для DiaryWindow.xaml
    /// </summary>
    public partial class DiaryWindow : Window
    {
        public User user { get; set; }
        public DiaryWindow(User auth)
        {
            InitializeComponent();
            user = auth;
            fioTB.Text += auth.Fio;
            Load();
        }
        void Load()
        {
            DateTime now = DateTime.Now;
            DateTime tomorrow = DateTime.Now.AddDays(1);

            var allTasks = Data.Context().Tasks.Where(x=>x.UserId == user.Id).ToList();
            var todayTasks = Data.Context().Tasks.Where(x => x.UserId == user.Id).Include(x => x.Day).ToList();
            var tobays = todayTasks.Where(x => x.Day.Date.Value.Day == now.Day && x.Day.Date.Value.Month == now.Month && x.Day.Date.Value.Year == now.Year).ToList();
            var tomorrowTasks = Data.Context().Tasks.Where(x => x.UserId == user.Id).Include(x => x.Day).ToList();
            var tomorrows = tomorrowTasks.Where(x => x.Day.Date.Value.Day == tomorrow.Day && x.Day.Date.Value.Month == tomorrow.Month && x.Day.Date.Value.Year == tomorrow.Year).ToList();

            allLB.ItemsSource = allTasks;
            todayLB.ItemsSource = tobays;
            tomorrowLB.ItemsSource = tomorrows;

            {
                double count1 = 0, count2 = 0, count3 = 0;
                double progress1, progress2, progress3;
                foreach (var s in tobays)
                {
                    if (s.Status == true)
                        count1++;
                }
                foreach (var s in tomorrows)
                {
                    if (s.Status == true)
                        count2++;
                }
                foreach (var s in allTasks)
                {
                    if (s.Status == true)
                        count3++;
                }
                if (tobays.Count == 0)
                    progress1 = 0;
                else
                    progress1 = (count1 / tobays.Count) * 100;
                if (tomorrows.Count == 0)
                    progress2 = 0;
                else
                    progress2 = (count2 / tomorrows.Count) * 100;
                if (allTasks.Count == 0)
                    progress3 = 0;
                else
                    progress3 = (count3 / allTasks.Count) * 100;
                todayprogressTB.Text = $"{progress1:f0} %";
                tomorrowprogressTB.Text = $"{progress2:f0} %";
                allprogressTB.Text = $"{progress3:f0} %";
            }
        }

        private void ClickEdit1(object sender, RoutedEventArgs e)
        {
            if (todayLB.SelectedItem is Task t)
            {
                if (MessageBox.Show($"Подтвердите редактирование задачи\n\"{t.Name}\"","Подтверждение",MessageBoxButton.OKCancel,MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    EditTask editTask = new EditTask(t);
                    if (editTask.ShowDialog() == true) 
                    {
                        MessageBox.Show("Вы успешно отредактировали задачу!","Информация",MessageBoxButton.OK,MessageBoxImage.Information);
                        Load();
                    }
                }
            }
        }
        private void ClickEdit2(object sender, RoutedEventArgs e)
        {
            if (tomorrowLB.SelectedItem is Task t)
            {
                if (MessageBox.Show($"Подтвердите редактирование задачи\n\"{t.Name}\"", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    EditTask editTask = new EditTask(t);
                    if (editTask.ShowDialog() == true)
                    {
                        MessageBox.Show("Вы успешно отредактировали задачу!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        Load();
                    }
                }
            }
        }
        private void ClickEdit3(object sender, RoutedEventArgs e)
        {
            if (allLB.SelectedItem is Task t)
            {
                if (MessageBox.Show($"Подтвердите редактирование задачи\n\"{t.Name}\"", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    EditTask editTask = new EditTask(t);
                    if (editTask.ShowDialog() == true)
                    {
                        MessageBox.Show("Вы успешно отредактировали задачу!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        Load();
                    }
                }
            }
        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {
            if (todayLB.SelectedItem is Task t)
            {
                Task task = Data.Context().Tasks.First(x => x.Id == t.Id);
                task.Status = t.Status;
                Data.Context().SaveChanges();
                MessageBox.Show($"{task.Status}    {t.Status}");
                //if (Data.Context().SaveChanges() == 1)
                //{
                //    MessageBox.Show("Изменения успешно сохранены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                //    Load();
                //}
                Load();
            }
            else if (tomorrowLB.SelectedItem is Task t2)
            {
                Task task = Data.Context().Tasks.First(x => x.Id == t2.Id);
                task.Status = t2.Status;
                Data.Context().SaveChanges();
                MessageBox.Show($"{task.Status}    {t2.Status}");
            }
            else if (allLB.SelectedItem is Task t3)
            {
                Task task = Data.Context().Tasks.First(x => x.Id == t3.Id);
                task.Status = t3.Status;
                Data.Context().SaveChanges();
                MessageBox.Show($"{task.Status}    {t3.Status}");
            }
        }

        private void ClickExit(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из своего профиля?","Сообщение",MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }

        private void ClickDelete1(object sender, RoutedEventArgs e)
        {
            if (todayLB.SelectedItem is Task t)
            {
                Data.Context().Tasks.Remove(t);
                Data.Context().SaveChanges();
                Day day = Data.Context().Days.Include(x => x.Tasks).First(x => x.Id == t.DayId);
                if (day.Tasks.Count == 0)
                {
                    Data.Context().Days.Remove(day);
                    Data.Context().SaveChanges();
                }
                Load();
            }
        }
        private void ClickDelete2(object sender, RoutedEventArgs e)
        {
            if (tomorrowLB.SelectedItem is Task t)
            {
                Data.Context().Tasks.Remove(t);
                Data.Context().SaveChanges();
                Day day = Data.Context().Days.Include(x => x.Tasks).First(x => x.Id == t.DayId);
                if (day.Tasks.Count == 0)
                {
                    Data.Context().Days.Remove(day);
                    Data.Context().SaveChanges();
                }
                Load();
            }
        }
        private void ClickDelete3(object sender, RoutedEventArgs e)
        {
            if (allLB.SelectedItem is Task t)
            {
                Data.Context().Tasks.Remove(t);
                Data.Context().SaveChanges();
                Day day = Data.Context().Days.Include(x => x.Tasks).First(x => x.Id == t.DayId);
                if (day.Tasks.Count == 0)
                {
                    Data.Context().Days.Remove(day);
                    Data.Context().SaveChanges();
                }
                Load();
            }
        }

        private void ClickCreate(object sender, RoutedEventArgs e)
        {
            CreateTask createTask = new CreateTask(user);
            if (createTask.ShowDialog() == true)
            {
                Load();
            }

        }
    }
}
