﻿using QLCongTy.QLDuAn;
using System;
using System.Windows.Forms;

namespace QLCongTy
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new fQLDuAn());
        }
    }
}
