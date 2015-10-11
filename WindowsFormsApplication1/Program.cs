using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;
using System.Drawing;

namespace WindowsFormsApplication1
{
    static class Program
    {
        
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        //public static string[] namesFontFamily;
        //public static System.Drawing.Text.InstalledFontCollection ifc = new System.Drawing.Text.InstalledFontCollection();
        [STAThread]
        static void Main()
        {
            InstalledFontCollection fonts = new InstalledFontCollection();
            //foreach (FontFamily f in fonts.Families)
            //    Debug.WriteLine(f.Name);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MDIParent1());
        }
    }
}