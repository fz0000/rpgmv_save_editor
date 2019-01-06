using LZStringCSharp;
using System;
using System.IO;
using System.Windows.Forms;

namespace rpgmv_save_editor
{
    public partial class Main : Form
    {
        public string strSaveFile = "";
        public Main()
        {
            InitializeComponent();
        }

        private void 打开OToolStripButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr != DialogResult.OK) return;
            strSaveFile = openFileDialog1.FileName;
            LoadSaveFile(strSaveFile);
        }

        private void textBoxSaveFileContent_DragDrop(object sender, DragEventArgs e)
        {
            string strFile = "";
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] content = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (content.Length != 1)
                {
                    MessageBox.Show("只能拖入一个文件");
                    return;
                }
                strFile = content[0];
            }
            LoadSaveFile(strFile);
        }

        private void LoadSaveFile(string strFile)
        {
            StreamReader sr = new StreamReader(strFile);
            string textFile = LZString.DecompressFromBase64(sr.ReadToEnd());
            textBoxSaveFileContent.Text = textFile;
        }

        private void textBoxSaveFileContent_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
