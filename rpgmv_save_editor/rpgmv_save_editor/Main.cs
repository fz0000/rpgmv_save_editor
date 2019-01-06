using LZStringCSharp;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace rpgmv_save_editor
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void 打开OToolStripButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr != DialogResult.OK) return;
            string strFile = openFileDialog1.FileName;
            StreamReader sr = new StreamReader(strFile);
            string textFile = LZString.DecompressFromBase64(sr.ReadToEnd());
            textBoxSaveFileContent.Text = textFile;
        }
    }
}
