﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using System.Windows;
using static SchoolWallpaperChanger.MainWindow;

namespace SchoolWallpaperChanger.Functions
{
    internal class SlideShowS
    {
        public int x;
        public int y;
        public string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static Timer aTimer = new Timer();
        public static List<string> PictureNameList = new List<string>();
        public static List<string> PictureList = new List<string>();
        public void SlideShow(int Time)
        {

            window.Picture.IsEnabled = false;
            window.Stop.Visibility = Visibility.Visible;
            window.Change.Visibility = Visibility.Collapsed;
            window.Select.IsEnabled = false;
            x = 0;
            y = 0;
            if (Directory.Exists($@"{AppDataPath}\Microsoft\Windows\Themes\SlideShow"))
                DeleteDirectory($@"{AppDataPath}\Microsoft\Windows\Themes\SlideShow");
            if (!Directory.Exists($@"{AppDataPath}\Microsoft\Windows\Themes\SlideShow"))
                Directory.CreateDirectory($@"{AppDataPath}\Microsoft\Windows\Themes\SlideShow");
            foreach (string PictureLocation in PictureList)
            {
                File.Copy(PictureLocation, $@"{AppDataPath}\Microsoft\Windows\Themes\SlideShow\{PictureNameList[x]}");
                x++;
            }
            string Picture = PictureNameList[y];
            File.Copy($@"{AppDataPath}\Microsoft\Windows\Themes\SlideShow\{Picture}", $@"{AppDataPath}\Microsoft\Windows\Themes\TranscodedWallpaper", true);
            y++;
            UIFunctions.SetWallpaper($@"{AppDataPath}\Microsoft\Windows\Themes\SlideShow\{Picture}");
            
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = Time;
            aTimer.Enabled = true;
        }
        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (y >= PictureNameList.Count)
                y = 0;
            string Picture = PictureNameList[y];
            File.Copy($@"{AppDataPath}\Microsoft\Windows\Themes\SlideShow\{Picture}", $@"{AppDataPath}\Microsoft\Windows\Themes\TranscodedWallpaper", true);
            UIFunctions.SetWallpaper($@"{AppDataPath}\Microsoft\Windows\Themes\SlideShow\{Picture}");
            y++;
        }

        public static void End()
        {
            aTimer.Close();
            Stopped = true;
            window.Time.IsEnabled = true;
            window.Change.Visibility = Visibility.Visible;
            window.Stop.Visibility = Visibility.Collapsed;
            window.Select.Visibility = Visibility.Visible;
            window.SlideShow.IsEnabled = false;
            window.Picture.IsEnabled = true;
            window.Change.IsEnabled = true;
            window.Select.IsEnabled = true;
        }

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }
    }
}
