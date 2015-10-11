using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_Activated(object sender, EventArgs e)
        {
            MDIParent1 mDI = (MDIParent1)this.MdiParent;
            mDI.activeForm = this;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            MDIParent1 mDI = (MDIParent1)this.MdiParent;
            if (this == mDI.activeForm)
                mDI.activeForm = null;
        }
    }
}
