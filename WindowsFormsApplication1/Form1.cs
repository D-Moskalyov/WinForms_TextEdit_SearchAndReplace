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
    public partial class Form1 : Form
    {
        MDIParent1 mDIP;
        public Form1()
        {
            InitializeComponent();
        }

        int startMain = 0;
        int cntReplaced = 0;

        bool Replace(RichTextBox rTB)
        {

            int start = rTB.SelectionStart;
            int resFind = rTB.Find(textBox1.Text, start, RichTextBoxFinds.MatchCase);
            if (resFind != -1)
            {
                rTB.Select(resFind, textBox1.Text.Length);
                rTB.SelectedText = textBox2.Text;
                rTB.Select(resFind, textBox2.Text.Length);
                rTB.Parent.Select();
                return true;
            }
            else if(start != 0)
            {
                DialogResult result = MessageBox.Show("Продолжить с начала?", "No Results", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    int start1 = 0;
                    resFind = rTB.Find(textBox1.Text, start1, start, RichTextBoxFinds.MatchCase);
                    if (resFind != -1)
                    {
                        rTB.Select(resFind, textBox1.Text.Length);
                        rTB.SelectedText = textBox2.Text;
                        rTB.Select(resFind, textBox2.Text.Length);
                        rTB.Parent.Select();
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        bool ReplaceAll(RichTextBox rTB)
        {
            int start = rTB.SelectionStart;
            int resFind = rTB.Find(textBox1.Text, start, RichTextBoxFinds.MatchCase);
            while (resFind != -1)
            {
                cntReplaced++;
                rTB.Select(resFind, textBox1.Text.Length);
                rTB.SelectedText = textBox2.Text;
                rTB.Select(resFind, textBox2.Text.Length);
                start = resFind + textBox2.Text.Length;
                resFind = rTB.Find(textBox1.Text, start, RichTextBoxFinds.MatchCase);
            }
            if(cntReplaced != 0)
                rTB.Parent.Select();
            if (startMain != 0)
            {
                DialogResult result = MessageBox.Show("Продолжить с начала?", "", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    start = 0;
                    resFind = rTB.Find(textBox1.Text, start, startMain, RichTextBoxFinds.MatchCase);
                    while (resFind != -1)
                    {
                        cntReplaced++;
                        rTB.Select(resFind, textBox1.Text.Length);
                        rTB.SelectedText = textBox2.Text;
                        rTB.Select(resFind, textBox2.Text.Length);
                        start = resFind + textBox2.Text.Length;
                        resFind = rTB.Find(textBox1.Text, start, RichTextBoxFinds.MatchCase);
                    }
                    if (cntReplaced != 0)
                    {
                        rTB.Parent.Select();
                        return true;
                    }
                    else
                        return false;
                }
                else if (cntReplaced == 0)
                    return false;
                else
                    return true;
            }
            else if (cntReplaced == 0)
                return false;
            else
                return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                MDIParent1 mDIP = (MDIParent1)this.MdiParent;
                if (mDIP.activeForm != null)
                {
                    if (Replace(mDIP.activeForm.RichTextBox1))
                    {
                        MessageBox.Show("Заменено");
                    }
                    else
                    {
                        MessageBox.Show("Не найдено");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                mDIP = (MDIParent1)this.MdiParent;
                if (mDIP.activeForm != null)
                {
                    startMain = mDIP.activeForm.RichTextBox1.SelectionStart;
                    if (ReplaceAll(mDIP.activeForm.RichTextBox1))
                    {
                        MessageBox.Show(string.Format("Заменено {0} элементов", cntReplaced));
                        cntReplaced = 0;
                    }
                    else
                    {
                        MessageBox.Show("Не найдено");
                    }
                }
            }
        }
    }
}
