using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using DtxCS;

namespace RBVR_Enhanced_Launcher
{
  public partial class RBVRELauncherApp : Form
  {
    public RBVRELauncherApp()
    {
      InitializeComponent();
    }

    void ClearState()
    {
      listBox1.Items.Clear();
      UpdateState();
    }
    void UpdateState()
    {
      if (listBox1.Items.Count == 0)
      {
        groupBox2.Enabled = false;
        groupBox3.Enabled = false;
        tspeedMult.Text = "";
        return;
      }
      else
      {
        groupBox2.Enabled = true;
      }
    }

    public static void CopyPatches(DirectoryInfo source, DirectoryInfo target)
    {
      Directory.CreateDirectory(target.FullName);

      // Copy each file into the new directory.
      foreach (FileInfo fi in source.GetFiles())
      {
        fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
      }

      // Copy each subdirectory using recursion.
      foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
      {
        DirectoryInfo nextTargetSubDir =
            target.CreateSubdirectory(diSourceSubDir.Name);
        CopyPatches(diSourceSubDir, nextTargetSubDir);
      }
    }

    private void pickFileButton_Click(object sender, EventArgs e)
    {
      using (var ofd = new FolderBrowserDialog())
      {
        if(ofd.ShowDialog() == DialogResult.OK && Directory.Exists(ofd.SelectedPath + "/rawfiles"))
        {
          listBox1.Items.Add(ofd.SelectedPath);
        }
        else
        {
          logBox.AppendText($"ERROR: {ofd.SelectedPath}\\rawfiles does not exist!");
        }
        UpdateState();
      }
    }

    void RemoveCon(string filename)
    {
      listBox1.Items.Remove(filename);
    }

    private void idBox_TextChanged(object sender, EventArgs e)
    {
      
    }

    private void euCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      
    }

    private void buildButton_Click(object sender, EventArgs e)
    {
      Action<string> log = x => logBox.AppendText(x + Environment.NewLine);
      var rbvrExec = folderName.Text + "/rbvr.exe";
      var rbvrPatchDir = folderName.Text + "/rawfiles";
      Process rbvr = new Process();
      rbvr.StartInfo.WorkingDirectory = folderName.Text;
      rbvr.StartInfo.FileName = rbvrExec;
      DirectoryInfo rbvrDir = new DirectoryInfo(folderName.Text);
      if (Directory.Exists(rbvrPatchDir))
      {
        Directory.Delete(rbvrPatchDir, true);
        log("Clearing Old Patches...");
      }
      for (int i = 0; i < listBox1.Items.Count; i++)
      {
        var patchDir = (string)listBox1.Items[i];
        DirectoryInfo patch = new DirectoryInfo(patchDir);
        CopyPatches(patch, rbvrDir);
        log($"Copying Patch {listBox1.Items[i]} to {folderName.Text}...");
      }
      log("Starting RBVR!");
      rbvr.Start();
    }

    private void listBox1_DragEnter(object sender, DragEventArgs e)
    {
      e.Effect = DragDropEffects.Copy;
    }

    private void listBox1_DragDrop(object sender, DragEventArgs e)
    {
      string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
      foreach (string file in files)
        if (Directory.Exists(file + "/rawfiles"))
          listBox1.Items.Add(file);
        else
          logBox.AppendText($"ERROR: {file}\\rawfiles does not exist!");
      UpdateState();
    }

    private void clearButton_Click(object sender, EventArgs e)
    {
      ClearState();
    }

    private void listBox1_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
      {
        RemoveCon(listBox1.SelectedItem as string);
        UpdateState();
      }
    }

    private void Outputbtn_Click(object sender, EventArgs e)
    {
      using (var sfd = new FolderBrowserDialog())
      {
        if (sfd.ShowDialog() == DialogResult.OK)
        {
          folderName.Text = sfd.SelectedPath;
          groupBox3.Enabled = true;
        }
      }
    }
  }
}
