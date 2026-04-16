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
            public string RelativePath { get; set; }
            public bool IsDirectory { get; set; }
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
            CopySelectedEntry(lvwLeftDir, txtLeftDir.Text, txtRightDir.Text);
        }

        private void btnCopyFromRight_Click(object sender, EventArgs e)
        {
            CopySelectedEntry(lvwRightDir, txtRightDir.Text, txtLeftDir.Text);
        }

        private void CopySelectedEntry(ListView sourceListView, string sourceRoot, string destinationRoot)
        {
            if (string.IsNullOrWhiteSpace(sourceRoot) || !Directory.Exists(sourceRoot))
            {
                MessageBox.Show("원본 폴더를 먼저 선택하세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(destinationRoot) || !Directory.Exists(destinationRoot))
            {
                MessageBox.Show("대상 폴더를 먼저 선택하세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (sourceListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("복사할 항목을 선택하세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var entry = sourceListView.SelectedItems[0].Tag as FileEntry;
            if (entry == null)
            {
                MessageBox.Show("선택한 항목 정보를 읽을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var sourcePath = Path.Combine(sourceRoot, entry.RelativePath);
            var destinationPath = Path.Combine(destinationRoot, entry.RelativePath);

            try
            {
                if (entry.IsDirectory)
                {
                    CopyDirectoryEntry(sourcePath, destinationPath, entry);
                }
                else
                {
                    CopyFileEntry(sourcePath, destinationPath, entry);
                }

                PopulateListViews();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CopyFileEntry(string sourcePath, string destinationPath, FileEntry entry)
        {
            if (!File.Exists(sourcePath))
            {
                MessageBox.Show("원본 파일을 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Directory.Exists(destinationPath))
            {
                var destinationDirectoryWriteTime = Directory.GetLastWriteTime(destinationPath);
                if (entry.LastWriteTime < destinationDirectoryWriteTime &&
                    !ConfirmOlderSource(sourcePath, destinationPath, "파일"))
                {
                    return;
                }

                Directory.Delete(destinationPath, true);
            }
            else if (File.Exists(destinationPath))
            {
                var destinationFileWriteTime = File.GetLastWriteTime(destinationPath);
                if (entry.LastWriteTime < destinationFileWriteTime &&
                    !ConfirmOlderSource(sourcePath, destinationPath, "파일"))
                {
                    return;
                }
            }

            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
            File.Copy(sourcePath, destinationPath, true);
            File.SetLastWriteTime(destinationPath, entry.LastWriteTime);
        }

        private void CopyDirectoryEntry(string sourcePath, string destinationPath, FileEntry entry)
        {
            if (!Directory.Exists(sourcePath))
            {
                MessageBox.Show("원본 하위폴더를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (File.Exists(destinationPath))
            {
                var destinationFileWriteTime = File.GetLastWriteTime(destinationPath);
                if (entry.LastWriteTime < destinationFileWriteTime &&
                    !ConfirmOlderSource(sourcePath, destinationPath, "하위폴더"))
                {
                    return;
                }

                File.Delete(destinationPath);
            }
            else if (Directory.Exists(destinationPath))
            {
                var destinationDirectoryWriteTime = Directory.GetLastWriteTime(destinationPath);
                if (entry.LastWriteTime < destinationDirectoryWriteTime &&
                    !ConfirmOlderSource(sourcePath, destinationPath, "하위폴더"))
                {
                    return;
                }
            }

            CopyDirectoryRecursive(sourcePath, destinationPath);
        }

        private bool ConfirmOlderSource(string sourcePath, string destinationPath, string kind)
        {
            var result = MessageBox.Show(
                string.Format(
                    "대상에 더 새로운 {0}이(가) 있습니다.\r\n그래도 덮어쓰시겠습니까?\r\n\r\n원본: {1}\r\n대상: {2}",
                    kind,
                    sourcePath,
                    destinationPath),
                "확인",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            return result == DialogResult.Yes;
        }

        private void CopyDirectoryRecursive(string sourceDirectory, string destinationDirectory)
        {
            Directory.CreateDirectory(destinationDirectory);

            foreach (var filePath in Directory.GetFiles(sourceDirectory))
            {
                var fileName = Path.GetFileName(filePath);
                var targetFilePath = Path.Combine(destinationDirectory, fileName);
                File.Copy(filePath, targetFilePath, true);
                File.SetLastWriteTime(targetFilePath, File.GetLastWriteTime(filePath));
            }

            foreach (var directoryPath in Directory.GetDirectories(sourceDirectory))
            {
                var directoryName = Path.GetFileName(directoryPath);
                var targetDirectoryPath = Path.Combine(destinationDirectory, directoryName);
                CopyDirectoryRecursive(directoryPath, targetDirectoryPath);
            }

            Directory.SetLastWriteTime(destinationDirectory, Directory.GetLastWriteTime(sourceDirectory));
        }

        private void PopulateListViews()
        {
            var leftEntries = LoadEntries(txtLeftDir.Text);
            var rightEntries = LoadEntries(txtRightDir.Text);

            PopulateListView(lvwLeftDir, leftEntries, rightEntries);
            PopulateListView(lvwRightDir, rightEntries, leftEntries);
        }

        private Dictionary<string, FileEntry> LoadEntries(string folderPath)
        {
            var entries = new Dictionary<string, FileEntry>(StringComparer.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
            {
                return entries;
            }

            try
            {
                LoadEntriesRecursive(folderPath, folderPath, entries);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return entries;
        }

        private void LoadEntriesRecursive(string rootPath, string currentPath, Dictionary<string, FileEntry> entries)
        {
            foreach (var directoryPath in Directory.GetDirectories(currentPath))
            {
                var directoryInfo = new DirectoryInfo(directoryPath);
                var relativePath = GetRelativePath(rootPath, directoryPath);

                entries[relativePath] = new FileEntry
                {
                    Name = directoryInfo.Name,
                    RelativePath = relativePath,
                    IsDirectory = true,
                    Length = 0,
                    LastWriteTime = directoryInfo.LastWriteTime
                };

                LoadEntriesRecursive(rootPath, directoryPath, entries);
            }

            foreach (var filePath in Directory.GetFiles(currentPath))
            {
                var fileInfo = new FileInfo(filePath);
                var relativePath = GetRelativePath(rootPath, filePath);

                entries[relativePath] = new FileEntry
                {
                    Name = fileInfo.Name,
                    RelativePath = relativePath,
                    IsDirectory = false,
                    Length = fileInfo.Length,
                    LastWriteTime = fileInfo.LastWriteTime
                };
            }
        }

        private void PopulateListView(ListView listView, Dictionary<string, FileEntry> currentEntries, Dictionary<string, FileEntry> otherEntries)
        {
            try
            {
                listView.BeginUpdate();
                listView.Items.Clear();

                foreach (var entry in currentEntries.Values.OrderBy(e => e.RelativePath))
                {
                    var item = new ListViewItem(GetDisplayName(entry));
                    item.SubItems.Add(GetSizeText(entry));
                    item.SubItems.Add(entry.LastWriteTime.ToString("yyyy-MM-dd tt h:mm"));
                    item.ForeColor = GetStatusColor(GetFileStatus(entry, otherEntries));
                    item.Tag = entry;
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

        private FileStatus GetFileStatus(FileEntry entry, Dictionary<string, FileEntry> otherEntries)
        {
            FileEntry otherEntry;

            if (!otherEntries.TryGetValue(entry.RelativePath, out otherEntry))
            {
                return FileStatus.Single;
            }

            if (entry.LastWriteTime == otherEntry.LastWriteTime)
            {
                return FileStatus.Same;
            }

            if (entry.LastWriteTime > otherEntry.LastWriteTime)
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

        private string GetDisplayName(FileEntry entry)
        {
            return entry.IsDirectory ? entry.RelativePath + Path.DirectorySeparatorChar : entry.RelativePath;
        }

        private string GetSizeText(FileEntry entry)
        {
            if (entry.IsDirectory)
            {
                return "<DIR>";
            }

            return string.Format("{0:#,##0} KB", entry.Length / 1024d);
        }

        private string GetRelativePath(string rootPath, string fullPath)
        {
            var rootFullPath = EnsureTrailingSeparator(Path.GetFullPath(rootPath));
            var fullPathNormalized = Path.GetFullPath(fullPath);

            if (fullPathNormalized.StartsWith(rootFullPath, StringComparison.OrdinalIgnoreCase))
            {
                return fullPathNormalized.Substring(rootFullPath.Length);
            }

            return Path.GetFileName(fullPathNormalized);
        }

        private string EnsureTrailingSeparator(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                return path + Path.DirectorySeparatorChar;
            }

            return path;
        }
    }
}
