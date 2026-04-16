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
            btnCopyFromLeft.Click += btnCopyFromLeft_Click;
            btnCopyFromRight.Click += btnCopyFromRight_Click;
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

        private void btnCopyFromLeft_Click(object sender, EventArgs e)
        {
            CopySelectedFile(lvwLeftDir, txtLeftDir.Text, txtRightDir.Text);
        }

        private void btnCopyFromRight_Click(object sender, EventArgs e)
        {
            CopySelectedFile(lvwRightDir, txtRightDir.Text, txtLeftDir.Text);
        }

        private void CopySelectedFile(ListView sourceListView, string sourceFolder, string destinationFolder)
        {
            if (string.IsNullOrWhiteSpace(sourceFolder) || !Directory.Exists(sourceFolder))
            {
                MessageBox.Show("원본 폴더를 먼저 선택하세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(destinationFolder) || !Directory.Exists(destinationFolder))
            {
                MessageBox.Show("대상 폴더를 먼저 선택하세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (sourceListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("복사할 파일을 선택하세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var fileName = sourceListView.SelectedItems[0].Text;
            var sourcePath = Path.Combine(sourceFolder, fileName);
            var destinationPath = Path.Combine(destinationFolder, fileName);

            try
            {
                if (!File.Exists(sourcePath))
                {
                    MessageBox.Show("원본 파일을 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (File.Exists(destinationPath))
                {
                    var sourceInfo = new FileInfo(sourcePath);
                    var destinationInfo = new FileInfo(destinationPath);

                    var message = string.Format(
                        "대상에 동일한 이름의 파일이 이미 있습니다.\r\n대상 파일이 덮어쓰기 됩니다. 계속하시겠습니까?\r\n\r\n원본: {0}\r\n대상: {1}",
                        sourcePath,
                        destinationPath);

                    if (sourceInfo.LastWriteTime != destinationInfo.LastWriteTime)
                    {
                        message = string.Format(
                            "대상에 동일한 이름의 파일이 이미 있습니다.\r\n수정된 날짜가 다릅니다. 덮어쓰기 하시겠습니까?\r\n\r\n원본: {0}\r\n대상: {1}",
                            sourcePath,
                            destinationPath);
                    }

                    var result = MessageBox.Show(message, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result != DialogResult.Yes)
                    {
                        return;
                    }
                }

                File.Copy(sourcePath, destinationPath, true);
                var copiedInfo = new FileInfo(sourcePath);
                File.SetLastWriteTime(destinationPath, copiedInfo.LastWriteTime);
                PopulateListViews();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
