using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace photo_organizer {
    public partial class PhotoOrganizer : Form {
        public PhotoOrganizer() {
            InitializeComponent();
        }

        private void directoryButton_Click(object sender,EventArgs e) {
            FolderBrowserDialog folderPicker = new FolderBrowserDialog();
            if(folderPicker.ShowDialog()==DialogResult.OK) {

                listView1.Items.Clear();

                string[] files = Directory.GetFiles(folderPicker.SelectedPath);
                foreach(string file in files) {

                    string fileName = Path.GetFileName(file);
                    string parentFolder = Path.GetDirectoryName(file);
                    string path = parentFolder;
                    parentFolder=parentFolder.Substring(parentFolder.LastIndexOf('\\')+1);
                    string[] arr = new string[3];
                    arr[0]=fileName;
                    arr[1]=parentFolder;
                    arr[2]=path;
                    ListViewItem item = new ListViewItem(arr);
                    item.Tag=file;

                    listView1.Items.Add(item);
                }
            }
        }

        private void catagory1Button_Click(object sender,EventArgs e) {
            string catagory1 = catagory1TextBox.Text;
            try {
                catagorize(catagory1,listView1.SelectedItems[0],listView1);
                listView1.Refresh();
            } catch { }
        }

        private void catagory2Button_Click(object sender,EventArgs e) {
            string catagory2 = catagory2TextBox.Text;
            try {
                catagorize(catagory2,listView1.SelectedItems[0],listView1);
            }
            catch { }
        }

        private void catagory3Button_Click(object sender,EventArgs e) {
            string catagory3 = catagory3TextBox.Text;
            try {
                catagorize(catagory3,listView1.SelectedItems[0],listView1);
            } catch { }
        }

        public void DisplayFolder(string folderPath) {
            string[] files = System.IO.Directory.GetFiles(folderPath);

            for(int x=0;x<files.Length;x++) {
                listView1.Items.Add(files[x]);
            }
        }

        private void listView1_SelectedIndexChanged(object sender,EventArgs e) {
            try {
                Image image = GetCopyImage(listView1.SelectedItems[0].SubItems[2].Text+"\\"+listView1.SelectedItems[0].SubItems[0].Text);
                pictureBox1.Image=image;
            }
            catch { }
        }

        private void catagorize(string catagoryName, ListViewItem item, ListView listView) {
            if(catagoryName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars())!=-1) {
                MessageBox.Show("Invalid file name");
                return;
            }
            ListViewItem temp = item;
            string sourceFile = System.IO.Path.Combine(item.SubItems[2].Text, item.SubItems[0].Text);
            string destFolder = System.IO.Path.Combine(item.SubItems[2].Text, catagoryName);
            string destFile = System.IO.Path.Combine(destFolder, item.SubItems[0].Text);
            System.IO.Directory.CreateDirectory(destFolder);
            if(!File.Exists(destFile)) {
                System.IO.File.Copy(sourceFile,destFile);
            }
            if(File.Exists(destFile)) {
                System.IO.File.Delete(sourceFile);
            }
            listView.SelectedItems[0].Remove();
            listView.Refresh();
            if(listView.Items.Count>0) {
                listView.Items[0].Selected=true;
                listView.Select();
                Image image = GetCopyImage(listView1.SelectedItems[0].SubItems[2].Text+"\\"+listView1.SelectedItems[0].SubItems[0].Text);
                pictureBox1.Image=image;
            }
            else {
                pictureBox1.Image=null;
            }
        }

        private Image GetCopyImage(string path) {

            using(Image im = Image.FromFile(path)) {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }
    }
}
