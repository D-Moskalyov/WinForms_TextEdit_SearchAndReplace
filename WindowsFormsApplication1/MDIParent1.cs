using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;

        StreamReader sr;
        StreamWriter sw;
        string path;
        int sz = 8;
        public string[] namesFontFamily;
        public Form2 activeForm;
        public Dictionary<string, Dictionary<string, Color>> Samples;

        void DefaultWord()
        {

        }

        int ApplayWord(KeyValuePair<string, Color> kvp)
        {
            int cnt = 0;
            int start = 0;
            int resFind = activeForm.RichTextBox1.Find(kvp.Key, start, RichTextBoxFinds.None);
            while (resFind != -1)
            {
                cnt++;
                activeForm.RichTextBox1.Select(resFind, kvp.Key.Length);
                activeForm.RichTextBox1.SelectionColor = kvp.Value;
                start = resFind + kvp.Key.Length;
                resFind = activeForm.RichTextBox1.Find(kvp.Key, start, RichTextBoxFinds.None);
            }
            return cnt;
        }

        int ApplaySample(RichTextBox rTB)
        {
            int cnt = 0;

            if (comboBox3.Text == "Default")
            {
                activeForm.RichTextBox1.Select(0, activeForm.RichTextBox1.Text.Length - 1);
                activeForm.RichTextBox1.SelectionColor = Color.Black;
                activeForm.RichTextBox1.Select(activeForm.RichTextBox1.Text.Length, activeForm.RichTextBox1.Text.Length);
                DefaultWord();
                return -1;
            }
            else
            {
                activeForm.RichTextBox1.Select(0, activeForm.RichTextBox1.Text.Length - 1);
                activeForm.RichTextBox1.SelectionColor = Color.Black;
                activeForm.RichTextBox1.Select(activeForm.RichTextBox1.Text.Length, activeForm.RichTextBox1.Text.Length);

                Dictionary<string, Color> dict = Samples[comboBox3.Text];
                foreach (KeyValuePair<string, Color> kvp in dict)
                {
                    cnt += ApplayWord(kvp);
                }

                DefaultWord();
                return cnt;
            }
        }

        void SamplesToCombo(ComboBox cBox)
        {
            foreach (KeyValuePair<string, Dictionary<string, Color>> kvp in Samples)
            {
                if (!cBox.Items.Contains(kvp.Key))
                {
                    cBox.Items.Add(kvp.Key);
                }
            }
        }

        public MDIParent1()
        {
            System.Drawing.Text.InstalledFontCollection ifc = new System.Drawing.Text.InstalledFontCollection();
            FontFamily[] families = ifc.Families;
            namesFontFamily = new string[(families.GetLength(0))];
            int indx = 0;
            foreach (FontFamily fontFamily in families)
            {
                namesFontFamily.SetValue(fontFamily.Name, indx);
                indx++;
            }
            Samples = new Dictionary<string, Dictionary<string, Color>>();
            InitializeComponent();
            comboBox3.Items.Add("Default");
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Окно " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                sr = new StreamReader(openFileDialog.FileName);
                path = openFileDialog.FileName;
                Form2 f2 = new Form2();
                f2.Text = path;
                f2.MdiParent = this;
                f2.RichTextBox1.Text = sr.ReadToEnd();
                f2.Show();
                f2.RichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
                sr.Close();
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    Form tmpForm = this.ActiveMdiChild;
                    if (tmpForm is Form2)
                    {
                        Form2 f2 = (Form2)tmpForm;
                        path = saveFileDialog.FileName;
                        sw = new StreamWriter(path, false);
                        string[] stroka = f2.RichTextBox1.Lines;
                        int cnt = stroka.Count();
                        for (int i = 0; i < cnt - 1; i++)
                        {
                            sw.WriteLine(stroka[i]);
                        }
                        sw.Write(stroka[cnt - 1]);
                        sw.Close();
                    }
                }
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 FandRForm = new Form2();
            FandRForm.MdiParent = this;
            FandRForm.Text = "FandRForm";
            FandRForm.TopMost = true;
            FandRForm.Show();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (path != null)
            {
                sw = new StreamWriter(path, false);

                Form tmpForm = this.ActiveMdiChild;
                if (tmpForm is Form2)
                {
                    Form2 f2 = (Form2)tmpForm;
                    string[] stroka = f2.RichTextBox1.Lines;
                    int cnt = stroka.Count();
                    for (int i = 0; i < cnt - 1; i++)
                    {
                        sw.WriteLine(stroka[i]);
                    }
                    sw.Write(stroka[cnt - 1]);
                    
                }
                sw.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (path != null)
            {
                sw = new StreamWriter(path, false);

                Form tmpForm = this.ActiveMdiChild;
                if (tmpForm is Form2)
                {
                    Form2 f2 = (Form2)tmpForm;
                    string[] stroka = f2.RichTextBox1.Lines;
                    int cnt = stroka.Count();
                    for (int i = 0; i < cnt - 1; i++)
                    {
                        sw.WriteLine(stroka[i]);
                    }
                    sw.Write(stroka[cnt - 1]);

                }
                sw.Close();
            }
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            if (File.Exists("samples.txt"))
            {
                string smpl = "";
                string key = "";
                Color color;
                sr = new StreamReader("samples.txt");
                //Samples = new Dictionary<string, Dictionary<string, Color>>();

                smpl = sr.ReadLine();

                while (smpl != "" && smpl !=null)
                {
                    Samples.Add(smpl, new Dictionary<string, Color>());
                    key = sr.ReadLine();
                    while (key != "" && key != null)
                    {
                        color = Color.FromArgb(int.Parse(sr.ReadLine()));
                        Samples[smpl].Add(key, color);
                        key = sr.ReadLine();
                    }
                    smpl = sr.ReadLine();
                }

                sr.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form tmpForm = this.ActiveMdiChild;
            if (tmpForm is Form2)
            {
                Font font = new Font(comboBox1.SelectedItem.ToString(), sz);
                Form2 f2 = (Form2)tmpForm;
                f2.RichTextBox1.Font = font;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form tmpForm = this.ActiveMdiChild;
            if (tmpForm is Form2)
            {
                sz = int.Parse(comboBox2.SelectedItem.ToString());
                Form2 f2 = (Form2)tmpForm;
                Font font = new Font(f2.RichTextBox1.Font.FontFamily, sz);
                f2.RichTextBox1.Font = font;
            }
        }

        private void toolTip_Popup(object sender, PopupEventArgs e)
        {

        }

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void найтиИЗаменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form tmpForm = this.ActiveMdiChild;
            //if (tmpForm is Form2)
            //{
            //    activeForm = (Form2)tmpForm;
                Form1 f1 = new Form1();
                f1.MdiParent = this;
                f1.Show();
            //}
        }

        private void синтаксисToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.MdiParent = this;
            f3.Show();
        }

        private void comboBox3_DropDown(object sender, EventArgs e)
        {
            SamplesToCombo(comboBox3);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                int cnt = ApplaySample(activeForm.RichTextBox1);
                if (cnt == 0)
                {
                    activeForm.Select();
                    MessageBox.Show("Не найдено элементов");
                }
                if (cnt == -1)
                {
                    activeForm.Select();
                    MessageBox.Show("Установлен цвет по-умолчанию");
                }
                else
                {
                    activeForm.Select();
                    MessageBox.Show(string.Format("Отредактировано {0} элементов", cnt));
                }
            }
        }

        private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sw = new StreamWriter("samples.txt", false);
            Dictionary<string, Dictionary<string, Color>>.KeyCollection keyColMain = Samples.Keys;
            foreach (string str in keyColMain)
            {
                sw.WriteLine(str);
                Dictionary<string, Color> dict = Samples[str];
                Dictionary<string, Color>.KeyCollection keyCol = dict.Keys;
                Dictionary<string, Color>.ValueCollection valCol = dict.Values;
                for (int i = 0; i < keyCol.Count; i++)
                {
                    sw.WriteLine(keyCol.ElementAt(i));
                    sw.WriteLine(valCol.ElementAt(i).ToArgb());
                    //sw.WriteLine(string.Format("Color.{0}", valCol.ElementAt(i).Name));
                }
                sw.WriteLine();
            }
            sw.Close();
        }
    }
}

//this.comboBox1.Items.AddRange(namesFontFamily);