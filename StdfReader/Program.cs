using System;
using System.Windows.Forms;

namespace StdfReader
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The arguments (the file path)</param>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm form;
            if (args != null && args.Length > 0)
                form = new MainForm(args[0]);
            else
                form = new MainForm();

            Application.Run(form);
        }
    }
}
