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
    public partial class Form3 : Form
    {
        //public Dictionary<string, Dictionary<string, Color>> Samples;

        void SamplesToCombo(ComboBox cBox)
        {
            MDIParent1 mDIP = (MDIParent1)this.MdiParent;
            Dictionary<string, Dictionary<string, Color>>.KeyCollection keyCol = mDIP.Samples.Keys;
            foreach (string str in keyCol)
            {
                if (!cBox.Items.Contains(str))
                {
                    cBox.Items.Add(str);
                }
            }
        }

        public Form3()
        {
            //Samples = new Dictionary<string, Dictionary<string, Color>>();
            InitializeComponent();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                MDIParent1 mDIP = (MDIParent1)this.MdiParent;
                if (!mDIP.Samples.ContainsKey(textBox1.Text))
                {
                    mDIP.Samples.Add(textBox1.Text, new Dictionary<string, Color>());
                }
                else
                {
                    MessageBox.Show("Повтор");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MDIParent1 mDIP = (MDIParent1)this.MdiParent;
            mDIP.Samples.Remove(comboBox3.Text);
            if (comboBox1.Text == comboBox3.Text)
            {
                MessageBox.Show("sd");
                comboBox2.Items.Clear();
            }
            mDIP.ComboBox3.Items.Remove(comboBox3.Text);

            comboBox1.Items.Remove(comboBox3.Text);
            comboBox3.Items.Remove(comboBox3.Text);
            
            //comboBox3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.BackColor = colorDialog1.Color;
            }
        }

        private void comboBox3_DropDown(object sender, EventArgs e)
        {
            SamplesToCombo(this.comboBox3);
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            SamplesToCombo(this.comboBox1);
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {

        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            MDIParent1 mDIP = (MDIParent1)this.MdiParent;
            comboBox2.Text = "";
            comboBox2.ForeColor = Color.Black;
            if (comboBox1.Text != "")
            {
                comboBox2.Items.Clear();
                Dictionary<string, Color> dict = mDIP.Samples[comboBox1.Text];
                Dictionary<string, Color>.KeyCollection keyCol = dict.Keys;
                foreach (string str in keyCol)
                {
                    if (!comboBox2.Items.Contains(str))
                    {
                        comboBox2.Items.Add(str);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox2.Text != "Hов слов или список")
            {
                MDIParent1 mDIP = (MDIParent1)this.MdiParent;
                Dictionary<string, Color> dict = mDIP.Samples[comboBox1.Text];
                if (dict.ContainsKey(comboBox2.Text))
                {
                    if (label1.BackColor == Color.Black)
                    {
                        if (MessageBox.Show("Установить цвет по-умолчанию", "default color", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            dict.Remove(comboBox2.Text);
                        }
                    }
                    else
                    {
                        dict[comboBox2.Text] = label1.BackColor;
                    }
                }
                else
                {
                    if (label1.BackColor != Color.Black)
                    {
                        dict.Add(comboBox2.Text, label1.BackColor);
                    }
                    else
                    {
                        MessageBox.Show("Выберите цвет");
                    }
                }
            }
            else
            {
                MessageBox.Show("Не достаточно данных");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MDIParent1 mDIP = (MDIParent1)this.MdiParent;
            label1.BackColor = mDIP.Samples[comboBox1.Text][comboBox2.Text];
        }
    }
}
