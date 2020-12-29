using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Windows.Forms;
using CheckDotNet.Properties;

namespace CheckDotNet
{
    public partial class MainWnd : Form
    {
        private AxWorker _oWorker;
        private List<String> _listFiles;
        private string _strSelectedPath;

        public MainWnd()
        {
            InitializeComponent();

            _oWorker = new AxWorker();
            _listFiles = new List<string>();

            _strSelectedPath = "";

            List<String> list = _oWorker.GetFoundedDotNet();
            lstBoxFound.Columns.Add(new ColumnHeader());
            lstBoxFound.Columns[0].Width = lstBoxFound.Width - 2;
            
            foreach (var oItem in list)
                lstBoxFound.Items.Add(oItem);

            lstView.Columns.Add(new ColumnHeader());
            lstView.Columns[0].Text = Resources.MainWnd_MainWnd_Dateiname;
            lstView.Columns[0].Width = 175;

            lstView.Columns.Add(new ColumnHeader());
            lstView.Columns[1].Text = Resources.MainWnd_MainWnd__NET_benötigt;
            lstView.Columns[1].Width = 150;

            lstView.Columns.Add(new ColumnHeader());
            lstView.Columns[2].Text = Resources.MainWnd_MainWnd__NET_Vorhanden;
            lstView.Columns[2].Width = 150;

            var toolTip = new ToolTip();
            
            toolTip.ToolTipTitle = "Select Directory";
            toolTip.IsBalloon = true;
            toolTip.Show("Please select the target directory!", butGetDir);
            //toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            toolTip.SetToolTip(butGetDir, "Please select the target directory!");
        }

        private void butGetDir_Click(object sender, EventArgs e)
        {
            lstView.Items.Clear();
            _listFiles.Clear();
            txtDir.Text = "";
            ToolTip toolTip = new ToolTip();
            toolTip.IsBalloon = true;
            toolTip.ToolTipTitle = "Directory:";
            toolTip.SetToolTip(txtDir, "");
            mnuItem_Report.Enabled = false;
            
            try
            {
                var fbd = new FolderBrowserDialog();

                DialogResult result = fbd.ShowDialog();

                if (result != DialogResult.OK)
                    return;

                if (string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    return;

                Cursor.Current = Cursors.WaitCursor;

                _strSelectedPath = fbd.SelectedPath;
                txtDir.Text = fbd.SelectedPath;
                
                toolTip.SetToolTip(txtDir, _strSelectedPath);

                _listFiles.AddRange(Directory.GetFiles(fbd.SelectedPath, "*.exe"));
                _listFiles.AddRange(Directory.GetFiles(fbd.SelectedPath, "*.dll"));
                _listFiles.AddRange(Directory.GetFiles(fbd.SelectedPath, "*.ocr"));

                if (_listFiles.Count < 1)
                    return;

                // Check the files
                Assembly ass;
                string strVer, strOk;
                string[] arVer;
                ListViewItem lvItem;
                
                foreach (var file in _listFiles)
                {
                    strOk = "";

                    try
                    {
                        ass = Assembly.LoadFrom(file);
                    }
                    catch (Exception)
                    {
                        ass = null;
                        // Fallback, don't need any .NET!
                    }
                    
                    if (ass != null)
                    {
                       
                        strVer = ass.ImageRuntimeVersion;
                        arVer = strVer.Split('.');

                        if (arVer[0] == "v1")
                        {
                            if (_oWorker.IsNetfx10Installed())
                                strOk = " .NET 1";

                            if (_oWorker.IsNetfx11Installed())
                                strOk += " .NET1.1";

                            if (_oWorker.IsNetfx20Installed())
                                strOk = " .NET 2";

                            if (_oWorker.IsNetfx30Installed())
                                strOk += " .NET3";

                            if (_oWorker.IsNetfx35Installed())
                                strOk += " .NET3.5";

                            if (String.IsNullOrWhiteSpace(strOk))
                                strOk = "nicht vorhanden";
                        }
                        else if (arVer[0] == "v2")
                        {
                            if (_oWorker.IsNetfx20Installed())
                                strOk = " .NET 2";

                            if (_oWorker.IsNetfx30Installed())
                                strOk += " .NET3";

                            if (_oWorker.IsNetfx35Installed())
                                strOk += " .NET3.5";

                            if (String.IsNullOrWhiteSpace(strOk))
                                strOk = "not found";
                        }
                        else if (arVer[0] == "v4")
                        {
                            object[] targetFrameworkAttributes = null;

                            if (_oWorker.IsNetAfter40Installed())
                                targetFrameworkAttributes = ass.GetCustomAttributes(typeof(TargetFrameworkAttribute), true);

                            if (targetFrameworkAttributes != null && targetFrameworkAttributes.Length > 0)
                            {
                                var targetFrameworkAttribute =
                                    (TargetFrameworkAttribute) targetFrameworkAttributes.First();
                                strVer = targetFrameworkAttribute.FrameworkDisplayName;

                                if (strVer == ".NET Framework 4")
                                {
                                    if (_oWorker.IsNetfx40ClientInstalled() || _oWorker.IsNetfx40FullInstalled())
                                        strOk = " .NET 4";

                                    if (_oWorker.IsNetfx45Installed() || _oWorker.IsNetfx451Installed() || _oWorker.IsNetfx452Installed() ||
                                        _oWorker.IsNetfx46Installed() || _oWorker.IsNetfx461Installed())
                                        strOk += " .NET4.5.x";

                                    if (String.IsNullOrWhiteSpace(strOk))
                                        strOk = "not found";
                                }
                                else if (strVer == ".NET Framework 4.5")
                                {
                                    if (_oWorker.IsNetfx45Installed() || _oWorker.IsNetfx451Installed() || _oWorker.IsNetfx452Installed() ||
                                        _oWorker.IsNetfx46Installed() || _oWorker.IsNetfx461Installed())
                                        strOk += " .NET4.5.x";
                                    else
                                        strOk = "not found";
                                }
                                else if (strVer == ".NET Framework 4.5.1")
                                {
                                    if (_oWorker.IsNetfx451Installed() || _oWorker.IsNetfx452Installed() ||
                                        _oWorker.IsNetfx46Installed() || _oWorker.IsNetfx461Installed())
                                        strOk += " .NET4.5.x";
                                    else
                                        strOk = "not found";
                                }
                                else if (strVer == ".NET Framework 4.5.2")
                                {
                                    if (_oWorker.IsNetfx452Installed() || _oWorker.IsNetfx46Installed() || _oWorker.IsNetfx461Installed())
                                        strOk += " .NET4.5.2";
                                    else
                                        strOk = "not found";
                                }
                                else if (strVer == ".NET Framework 4.6")
                                {
                                    if ( _oWorker.IsNetfx46Installed() || _oWorker.IsNetfx461Installed())
                                        strOk += " .NET4.6";
                                    else
                                        strOk = "not found";
                                }
                                else if (strVer == ".NET Framework 4.6.1")
                                {
                                    if (_oWorker.IsNetfx461Installed())
                                        strOk += " .NET4.6.1";
                                    else
                                        strOk = "not found";
                                }
                                else if (strVer == ".NET Framework 4.6.2")
                                {
                                    if (_oWorker.IsNetfx462Installed())
                                        strOk += " .NET4.6.2";
                                    else
                                        strOk = "not found";
                                }
                                else if (strVer == ".NET Framework 4.7")
                                {
                                    if (_oWorker.IsNetfx47Installed())
                                        strOk += " .NET4.7";
                                    else
                                        strOk = "not found";
                                }
                                else if (strVer == ".NET Framework 4.7.1")
                                {
                                    if (_oWorker.IsNetfx471Installed())
                                        strOk += " .NET4.7.1";
                                    else
                                        strOk = "not found";
                                }
                                else if (strVer == ".NET Framework 4.7.2")
                                {
                                    if (_oWorker.IsNetfx472Installed())
                                        strOk += " .NET4.7.2";
                                    else
                                        strOk = "not found";
                                }
                            }
                            else
                            {
                                if (_oWorker.IsNetfx40ClientInstalled() || _oWorker.IsNetfx40FullInstalled())
                                    strOk = " .NET 4";

                                if (_oWorker.IsNetfx451Installed() || _oWorker.IsNetfx452Installed() || _oWorker.IsNetfx45Installed())
                                    strOk += " .NET4.5";

                                if (String.IsNullOrWhiteSpace(strOk))
                                    strOk = "not found";
                            }
                            
                        }
                        else
                        {
                            strOk = "not found";
                        }

                        // Cut the path from the "file", only file name need!
                        lvItem = new ListViewItem(new[] { Path.GetFileName(file), strVer, strOk });
                        
                        if (strOk == "not found")
                            lvItem.BackColor = Color.Red;
                        lstView.Items.Add(lvItem);
                    }
                }

                mnuItem_Report.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                _oWorker.GetLogger().Write(ex);
            }
        }

        private void OnReport()
        {
            try
            {
                string strPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), string.Format("Report_{0}.log", DateTime.Now.ToShortDateString()));

                if (File.Exists(strPath))
                    File.Delete(strPath);

                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(strPath))
                {
                    sw.WriteLine("Found Components:");
                    sw.WriteLine("************************");

                    List<String> list = _oWorker.GetFoundedDotNet();
                    foreach (var line in list)
                        sw.WriteLine(line);

                    sw.WriteLine("");
                    sw.WriteLine("");

                    sw.WriteLine("In Directory {0} have found EXE/DLL/OCX:", _strSelectedPath);
                    sw.WriteLine("**************************************************************************");

                    string str = "";

                    foreach (ListViewItem item in lstView.Items)
                    {
                        //str = item.SubItems.Cast<ListViewItem.ListViewSubItem>().Aggregate(str, (current, sub) => current + (sub.Text));

                        foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                        {
                            str += subItem.Text;

                            int nModLen = subItem.Text.Length % 8;

                            for (int i = 0; i < 8 - nModLen; i++)
                                str += " ";

                            if (subItem.Text.Length < 32)
                                str += "\t";

                            if (subItem.Text.Length < 24)
                                str += "\t";

                            if (subItem.Text.Length < 16)
                                str += "\t";
                        }
                        
                        sw.WriteLine(str);
                        str = "";
                    }

                    System.Diagnostics.Process.Start(strPath);
                }


                
            }
            catch (Exception ex)
            {
                _oWorker.GetLogger().Write(ex);
            }
        }

        private void OnClose()
        {
            Close();
        }

        private void OnMenuGetDir(object sender, EventArgs e)
        {
            butGetDir_Click(sender, e);
        }

        private void OnMenuReport(object sender, EventArgs e)
        {
            OnReport();
        }

        private void OnMenuClose(object sender, EventArgs e)
        {
            OnClose();
        }

        private void OnMenuInfo(object sender, EventArgs e)
        {
            InfoWnd infoWnd = new InfoWnd();
            infoWnd.StartPosition = FormStartPosition.CenterParent;
            infoWnd.ShowDialog(this);
        }
    }
}
