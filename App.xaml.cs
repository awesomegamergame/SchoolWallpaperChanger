﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolWallpaperChanger
{
    public partial class App : Application
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            var mutex = new Mutex(true, "SchoolWallpaperChanger", out bool aIsNewInstance);
            if (!aIsNewInstance)
            {
                Process current = Process.GetCurrentProcess();
                foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                {
                    Current.Shutdown();
                    if (process.Id != current.Id)
                    {
                        IntPtr pointer = FindWindow(null, "SchoolWallpaperChanger");
                        ShowWindow(pointer, 1);
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show(args[0]);
                MessageBox.Show(args[1]);
                MainWindow window = new MainWindow();
                window.Show();
            }
        }
    }
}
