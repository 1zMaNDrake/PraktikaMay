﻿using System;
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

namespace PraktikaActivity
{
    /// <summary>
    /// Логика взаимодействия для Organaizer.xaml
    /// </summary>
    public partial class Organaizer : Window
    {
        ActivityEntities db = new ActivityEntities();
        public Organaizer()
        {
            InitializeComponent();
            Users users = db.Users.Where(x => x.Id == CurrentUser.currentUserId).FirstOrDefault();
            DateTime currentTime = DateTime.Now;
            int currentHour = currentTime.Hour;

            // Определяем время суток
            string timeOfDay;
            if (currentHour >= 9 && currentHour < 11)
            {
                timeOfDay = "Доброе утро";
            }
            else if (currentHour >= 11 && currentHour < 18)
            {
                timeOfDay = "Добрый день";
            }
            else
            {
                timeOfDay = "Добрый вечер";
            }
            time.Text = timeOfDay;
            name.Text = users.FullName;
        }

        private void Jury_Click(object sender, RoutedEventArgs e)
        {
            RgistrationOfJuryModerator rgistrationOfJuryModerator = new RgistrationOfJuryModerator();
            rgistrationOfJuryModerator.Show();
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
