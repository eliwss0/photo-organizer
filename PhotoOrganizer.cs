using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string parentFolder = Path.GetDirectoryName(file);
                    parentFolder=parentFolder.Substring(parentFolder.LastIndexOf('\\')+1);
                    string[] arr = new string[2];
                    arr[0]=fileName;
                    arr[1]=parentFolder;
                    ListViewItem item = new ListViewItem(arr);
                    item.Tag=file;

                    listView1.Items.Add(item);
                }
            }
        }

        private void directoryTextBox_TextChanged(object sender,EventArgs e) {

        }

        private void catagory1Button_Click(object sender,EventArgs e) {

        }

        private void catagory2Button_Click(object sender,EventArgs e) {

        }

        private void catagory3Button_Click(object sender,EventArgs e) {

        }

        public void DisplayFolder(string folderPath) {
            string[] files = System.IO.Directory.GetFiles(folderPath);

            for(int x = 0;x<files.Length;x++) {
                listView1.Items.Add(files[x]);
            }
        }
    }
}
