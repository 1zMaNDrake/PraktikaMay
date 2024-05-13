﻿using System;
using System.Collections.Generic;
using System.IO;
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

namespace PraktikaActivity
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ActivityEntities db = new ActivityEntities();
        public class Events_Direction
        {
            public int id { get; set; }
            public string DirectionName { get; set; }
            public DateTime StartDate { get; set; }
            public string EventName { get; set; }
            public string GetPhoto
            {
                get
                {
                    string imagePath = Directory.GetCurrentDirectory() + @"\Images\" + id + ".jpeg";
                    if (File.Exists(imagePath))
                    {
                        return imagePath;
                    }
                    else
                    {
                        string[] extensions = { ".png", ".jpg", ".gif" };
                        foreach (var ext in extensions)
                        {
                            imagePath = Directory.GetCurrentDirectory() + @"\Images\" + id + ext;
                            if (File.Exists(imagePath))
                            {
                                return imagePath;
                            }
                        }
                    }
                    // Если изображение не найдено ни в одном из форматов, можно вернуть значение по умолчанию или бросить исключение
                    return string.Empty;
                }
            }
        }

        private void VisibleButton()
        {
            if (CurrentUser.currentUserId == 0)
            {
                authorizationBtn.Visibility = Visibility.Visible;
                profile.Visibility = Visibility.Hidden;
                back.Visibility = Visibility.Hidden;
            }
            if (CurrentUser.currentUserId != 0)
            {
                authorizationBtn.Visibility = Visibility.Hidden;
                profile.Visibility = Visibility.Visible;
                back.Visibility = Visibility.Visible;
            }
        } 
        public MainWindow()
        {
            InitializeComponent();
            VisibleButton();
            CmbDirection.Items.Clear();
            foreach (var d in db.Directions)
            {
                CmbDirection.Items.Add(d);

            }
            foreach (var d in db.Events)
            {
                CmbData.Items.Add(d);
            }
            var productsWithCounts = db.Events
    .Join(
        db.Directions,
        product => product.DirectionId,
        order => order.Id,
        (product, order) => new Events_Direction
        {
            id = product.Id,
            DirectionName = product.Directions.DirectionName,
            StartDate = product.StartDate,
            EventName = product.EventName,
        }).ToList();
            Lucas.ItemsSource = productsWithCounts;
        }

        private void authorization_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            this.Close();
            authorization.Show();
        }

        private void CmbDirection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbDirection.SelectedItem != null)
            {
                string selectedDirectionName = (CmbDirection.SelectedItem as Directions).DirectionName;

                var productsWithCounts = db.Events
                    .Join(
                        db.Directions,
                        product => product.DirectionId,
                        order => order.Id,
                        (product, order) => new Events_Direction
                        {
                            id = product.Id,
                            DirectionName = product.Directions.DirectionName,
                            StartDate = product.StartDate,
                            EventName = product.EventName,
                        })
                    .Where(x => x.DirectionName == selectedDirectionName)
                    .ToList();

                Lucas.ItemsSource = productsWithCounts;
            }
            else
            {
                var productsWithCounts = db.Events
    .Join(
        db.Directions,
        product => product.DirectionId,
        order => order.Id,
        (product, order) => new Events_Direction
        {
            id = product.Id,
            DirectionName = product.Directions.DirectionName,
            StartDate = product.StartDate,
            EventName = product.EventName,
        }).ToList();
                Lucas.ItemsSource = productsWithCounts;
            }
        }

        private void CmbData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbData.SelectedItem != null)
            {
                var selectedDirectionName = (CmbData.SelectedItem as Events).StartDate;

                var productsWithCounts = db.Events
                    .Join(
                        db.Directions,
                        product => product.DirectionId,
                        order => order.Id,
                        (product, order) => new Events_Direction
                        {
                            id = product.Id,
                            DirectionName = product.Directions.DirectionName,
                            StartDate = product.StartDate,
                            EventName = product.EventName,
                        })
                    .Where(x => x.StartDate == selectedDirectionName)
                    .ToList();

                Lucas.ItemsSource = productsWithCounts;
            }
            else
            {
                var productsWithCounts = db.Events
    .Join(
        db.Directions,
        product => product.DirectionId,
        order => order.Id,
        (product, order) => new Events_Direction
        {
            id = product.Id,
            DirectionName = product.Directions.DirectionName,
            StartDate = product.StartDate,
            EventName = product.EventName,
        }).ToList();
                Lucas.ItemsSource = productsWithCounts;
            }
        }

        private void leave_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.currentUserId = 0;
            VisibleButton();
        }

        private void profile_Click(object sender, RoutedEventArgs e)
        {
            Users users = db.Users.Where(x => x.Id == CurrentUser.currentUserId).FirstOrDefault();
            if (users.RoleId == 1)
            {
                Participant participant = new Participant();
                this.Close();
                participant.Show();
            }
            if (users.RoleId == 2)
            {
                Moderator moderator = new Moderator();
                this.Close();
                moderator.Show();
            }
            if (users.RoleId == 3)
            {
                Jury jury = new Jury();
                this.Close();
                jury.Show();
            }
            if (users.RoleId == 4)
            {
                Organaizer organaizer = new Organaizer();
                this.Close();
                organaizer.Show();
            }
        }
    }
}