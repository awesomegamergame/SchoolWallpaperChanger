﻿using System;
using System.IO;
using System.Windows;
using static SchoolWallpaperChanger.MainWindow;

namespace SchoolWallpaperChanger.Functions
{
    internal class ChangeButton
    {
        public static string FileLocation;
        public static string PicLocation;
        public static void Change(int Selected)
        {
            switch (Selected)
            {
                case 0:
                    UIFunctions.SetWallpaper(PicLocation);
                    MessageBox.Show("Wallpaper Changed");
                    MainWindow.Selected = 2;
                    break;
                case 1:
                    if(window.Time.Text.Equals(null) || window.Time.Text.Equals(""))
                    {
                        MessageBox.Show("Time cant be empty");
                        break;
                    }
                    int time = int.Parse(window.Time.Text);
                    if(time < 5)
                    {
                        MessageBox.Show("Time cant be less than 5 seconds");
                        break;
                    }
                    window.Change.IsEnabled = false;
                    time *= 1000;
                    window.Time.IsEnabled = false;
                    int CP = 0;
                    if (int.Parse(settings.Read("CurrentPicture")) != CP && int.Parse(settings.Read("CurrentPicture")) < int.Parse(settings.Read("PictureCount")))
                        CP = int.Parse(settings.Read("CurrentPicture"));
                    var SL = new SlideShowS();
                    SL.SlideShow(time, CP);
                    break;
                case 2:
                    File.Copy(PicLocation, $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Themes\TranscodedWallpaper", true);
                    UIFunctions.SetWallpaper(FileLocation);
                    MessageBox.Show("Wallpaper Reapplied");
                    break;
            }
        }
    }
}
