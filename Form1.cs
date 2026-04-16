using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileCompare
{
    public partial class Form1 : Form
    {
        private enum FileStatus
        {
            Same,
            New,
            Old,
            Single
        }

        private sealed class FileEntry
        {
            public string Name { get; set; }
            public long Length { get; set; }
            public DateTime LastWriteTime { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLeftDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";

                if (!string.IsNullOrWhiteSpace(txtLeftDir.Text) && Directory.Exists(txtLeftDir.Text))
                {
                    dlg.SelectedPath = txtLeftDir.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtLeftDir.Text = dlg.SelectedPath;
                    PopulateListViews();
                }
            }
        }

        private void btnRightDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";

                if (!string.IsNullOrWhiteSpace(txtRightDir.Text) && Directory.Exists(txtRightDir.Text))
                {
                    dlg.SelectedPath = txtRightDir.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtRightDir.Text = dlg.SelectedPath;
                    PopulateListViews();
                }
            }
        }

        private void PopulateListViews()
        {
            var leftFiles = LoadFiles(txtLeftDir.Text);
            var rightFiles = LoadFiles(txtRightDir.Text);

            PopulateListView(lvwLeftDir, leftFiles, rightFiles);
            PopulateListView(lvwRightDir, rightFiles, leftFiles);
        }

        private Dictionary<string, FileEntry> LoadFiles(string folderPath)
        {
            var files = new Dictionary<string, FileEntry>(StringComparer.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
            {
                return files;
            }

            try
            {
                foreach (var filePath in Directory.GetFiles(folderPath))
                {
                    var info = new FileInfo(filePath);
                    files[info.Name] = new FileEntry
                    {
                        Name = info.Name,
                        Length = info.Length,
                        LastWriteTime = info.LastWriteTime
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return files;
        }

        private void PopulateListView(ListView listView, Dictionary<string, FileEntry> currentFiles, Dictionary<string, FileEntry> otherFiles)
        {
            try
            {
                listView.BeginUpdate();
                listView.Items.Clear();

                foreach (var file in currentFiles.Values.OrderBy(f => f.Name))
                {
                    var item = new ListViewItem(file.Name);
                    item.SubItems.Add(FormatSize(file.Length));
                    item.SubItems.Add(file.LastWriteTime.ToString("yyyy-MM-dd tt h:mm"));
                    item.ForeColor = GetStatusColor(GetFileStatus(file, otherFiles));
                    listView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                listView.EndUpdate();
            }
        }

        private FileStatus GetFileStatus(FileEntry file, Dictionary<string, FileEntry> otherFiles)
        {
            FileEntry otherFile;

            if (!otherFiles.TryGetValue(file.Name, out otherFile))
            {
                return FileStatus.Single;
            }

            if (file.LastWriteTime == otherFile.LastWriteTime)
            {
                return FileStatus.Same;
            }

            if (file.LastWriteTime > otherFile.LastWriteTime)
            {
                return FileStatus.New;
            }

            return FileStatus.Old;
        }

        private Color GetStatusColor(FileStatus status)
        {
            switch (status)
            {
                case FileStatus.Same:
                    return Color.Black;
                case FileStatus.New:
                    return Color.Red;
                case FileStatus.Old:
                    return Color.Gray;
                case FileStatus.Single:
                    return Color.Purple;
                default:
                    return Color.Black;
            }
        }

        private string FormatSize(long length)
        {
            return string.Format("{0:#,##0} KB", length / 1024d);
        }
    }
}
