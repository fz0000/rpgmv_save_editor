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

        // case 1
        private void textBoxSaveFileContent_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] content = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (content.Length != 1)
                {
                    MessageBox.Show("只能拖入一个文件");
                    return;
                }
                strSaveFile = content[0];
            }
            LoadSaveFile(strSaveFile);
        }

        private void LoadSaveFile(string strFile)
        {
            StreamReader sr = new StreamReader(strFile);
            string textFile = LZString.DecompressFromBase64(sr.ReadToEnd());
            textBoxSaveFileContent.Text = textFile;
            sr.Close();
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

        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            if (strSaveFile == "")
            {
                if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                strSaveFile = saveFileDialog1.FileName;
            }
            else
            {
                File.Copy(strSaveFile, strSaveFile + ".bak");
            }
            StreamWriter wr = new StreamWriter(strSaveFile);
            wr.Write(LZString.CompressToBase64(textBoxSaveFileContent.Text));
            wr.Close();
        }
    }
}
