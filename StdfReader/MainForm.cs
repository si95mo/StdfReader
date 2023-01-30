using LinqToStdf;
using LinqToStdf.Records;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel.Application;

namespace StdfReader
{
    public partial class MainForm : Form
    {
        private const string BaseText = "Stdf reader";
        private const string NullAsString = "null";

        private Dictionary<string, Color> colors;
        private Dictionary<string, int> recordCounter;

        private Excel excel;
        private Workbook workbook;
        private Worksheet worksheet;
        private Worksheet sheet;

        private List<StdfRecord> entries;

        private TreeNode highlightedNode;

        /// <summary>
        /// Create a new instance of <see cref="MainForm"/>
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            CenterToScreen();

            bgReaderWorker.DoWork += BgReaderWorker_DoWork;
            bgExcelWorker.DoWork += BgExcelWorker_DoWork;

            colors = new Dictionary<string, Color>();
            recordCounter = new Dictionary<string, int>();
            recordCounter.Add("Total", 0);

            excel = new Excel
            {
                DisplayAlerts = false,
                Visible = false,
                UserControl = false
            };

            entries = new List<StdfRecord>();
        }

        /// <summary>
        /// Create a new instance of <see cref="MainForm"/>
        /// </summary>
        /// <param name="path">The stdf file path</param>
        public MainForm(string path) : this()
        {
            if (path.CompareTo(string.Empty) != 0)
            {
                lblStatus.Text = path;
                Text = BaseText + $" - {Path.GetFileName(lblStatus.Text)}";

                PopulateControl(path, trvRecords);
            }
        }

        #region Helper methods

        /// <summary>
        /// Read an stdf file
        /// </summary>
        /// <param name="path">The path</param>
        /// <returns>The <see cref="List{T}"/> of <see cref="StdfRecord"/></returns>
        private List<StdfRecord> ReadFile(string path)
        {
            StdfFile file = new StdfFile(path);

            List<StdfRecord> records = file.GetRecords().ToList();

            foreach (StdfRecord record in records)
            {
                string type = record.GetType().Name;

                if (!colors.ContainsKey(type))
                    colors.Add(type, GenerateColor(type));

                if (type != "StartOfStreamRecord" && type != "EndOfStreamRecord") // Non stdf-related records
                {
                    recordCounter["Total"]++;

                    if (!recordCounter.ContainsKey(type)) // If the counter dictionary does not contain the record type
                        recordCounter.Add(type, 1); // Add the key and set the counter to 1
                    else
                        recordCounter[type]++; // Else (key contained), increment the relative counter
                }
            }

            return records;
        }

        /// <summary>
        /// Let the user choose an stdf file
        /// </summary>
        /// <returns>The chosen path (or <see cref="string.Empty"/> if invalid file is selected)</returns>
        private string ChooseFile()
        {
            string path = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "stdf files (*.stdf)|*.stdf|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                //Get the path of specified file
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (Path.GetExtension(openFileDialog.FileName).CompareTo(".stdf") != 0)
                        MessageBox.Show("Invalid file selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        path = openFileDialog.FileName;
                }
            }

            return path;
        }

        /// <summary>
        /// Get all the property values of an <see cref="StdfRecord"/>
        /// </summary>
        /// <param name="record">The <see cref="StdfRecord"/></param>
        /// <param name="treeView">The <see cref="CustomTreeView"/></param>
        /// <param name="index">The index to in the <see cref="ToolTip"/>. Leave -1 if should not be prepended</param>
        private void Populate(StdfRecord record, TreeView treeView, int index = -1)
        {
            entries.Add(record);

            List<object> values = new List<object>();

            Type type = record.GetType();
            PropertyInfo[] propertiesToExclude = typeof(StdfRecord).GetProperties();
            if (type != typeof(StartOfStreamRecord) && type != typeof(EndOfStreamRecord)) // Remove non-stdf related records (file stream)
            {
                // Get all properties aside from the ones of StdfRecord. This is done for records like HBR and SBR that inherits from an intermediate class
                PropertyInfo[] allProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo[] properties = allProperties.Where((x) => !propertiesToExclude.Select((y) => y.Name).Contains(x.Name)).ToArray();
                TreeNode root = new TreeNode(record.GetType().Name); // Root, first level node

                if (index != -1)
                    root.ToolTipText = $"Record number {index}";

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(record, null);
                    if ((value != value?.GetType().GetDefaultValue() || value == null) && property.Name.CompareTo("RecordType") != 0) // Remove empty record fields
                    {
                        if (value == null || !value.GetType().IsArray) // Second level node
                        {
                            string valueAsString = value?.ToString() ?? NullAsString;
                            if (value is byte)
                            {
                                if (value != null && property.Name.Contains("Flag") || property.Name.Contains("flag"))
                                    valueAsString = $"0b{Convert.ToString((byte)value, toBase: 2).PadLeft(8, '0')}";
                                else if (value == null)
                                    valueAsString = NullAsString;
                            }

                            TreeNode leaf = new TreeNode($"{property.Name.ToSentenceCase()}: {valueAsString}")
                            {
                                ForeColor = SystemColors.Info
                            };
                            root.Nodes.Add(leaf);
                        }
                        else if(value.GetType().IsArray) // Third level node
                        {
                            object[] array = value as object[];
                            TreeNode node = new TreeNode(property.Name.ToSentenceCase())
                            {
                                ForeColor = SystemColors.MenuHighlight
                            };

                            if (array != null) // Third level node values (object array)
                            {
                                List<object> objects = array.ToList();
                                objects.ForEach((x) =>
                                    {
                                        TreeNode leaf;
                                        if (!(x is byte))
                                        {
                                            leaf = new TreeNode(x.ToString())
                                            {
                                                ForeColor = SystemColors.Info
                                            };
                                            node.Nodes.Add(leaf);
                                        }
                                        else
                                        {
                                            leaf = new TreeNode($"0b{Convert.ToString((byte)x, toBase: 2).PadLeft(8, '0')}")
                                            {
                                                ForeColor = SystemColors.Info
                                            };
                                            node.Nodes.Add(leaf);
                                        }
                                    }
                                );
                            }
                            else // Third level node values (byte array)
                            {
                                byte[] bytes = value as byte[];
                                bytes.ToList().ForEach((x) =>
                                    {
                                        TreeNode leaf = new TreeNode($"0b{Convert.ToString(x, toBase: 2).PadLeft(8, '0')}")
                                        {
                                            ForeColor = SystemColors.Info
                                        };
                                        node.Nodes.Add(leaf);
                                    }
                                );
                            }

                            root.Nodes.Add(node);
                        }
                    }
                }

                treeView.Nodes.Add(root.Clone() as TreeNode);
                ColorNode(treeView.Nodes);
            }
        }

        /// <summary>
        /// Color the node of a <see cref="TreeNodeCollection"/>
        /// </summary>
        /// <param name="nodes">The nodes to color</param>
        /// <param name="text">The text based on which choose the <see cref="Color"/></param>
        void ColorNode(TreeNodeCollection nodes)
        {
            string text;
            foreach (TreeNode child in nodes)
            {
                text = child.Text;

                colors.TryGetValue(text, out Color color);
                child.BackColor = color;

                if (child.Nodes != null && child.Nodes.Count > 0)
                    ColorNode(child.Nodes);
            }
        }

        /// <summary>
        /// Read an stdf file and then populate the <see cref="TreeView"/>
        /// </summary>
        /// <param name="path">The stdf file path</param>
        /// <param name="treeView">The <see cref="CustomTreeView"/></param>
        /// <returns>The (async) <see cref="Task"/></returns>
        private void PopulateControl(string path, TreeView treeView)
        {
            List<StdfRecord> records = ReadFile(path);

            int index = 0;
            foreach (StdfRecord record in records)
                Populate(record, treeView, index++);

            treeView.ShowNodeToolTips = true;

            string entry = string.Empty;
            foreach (KeyValuePair<string, int> pair in recordCounter)
            {
                if (pair.Key != "Total")
                    entry += $"{pair.Key}: {pair.Value}, ";
            }

            entry = entry.TrimEnd(new char[] { ',', ' ' }); // Remove trailing blank space
            entry += $". Total: {recordCounter["Total"]}";

            List<string> dataSource = new List<string>();
            dataSource.Add("None");
            dataSource.AddRange(recordCounter.Keys.Where((x) => x != "Total"));

            cbxFilter.DataSource = dataSource;
            cbxFilter.SelectedIndex = 0;

            lblRecordCounter.Text = entry;
        }

        /// <summary>
        /// Generate a random color from a seed
        /// </summary>
        /// <param name="seed">The seed <see cref="string"/></param>
        /// <returns>The generated <see cref="Color"/></returns>
        private Color GenerateColor(string seed)
        {
            Color color = Color.DimGray;

            switch (seed)
            {
                case "Far":
                    color = Color.Red;
                    break;
                case "Mir":
                    color = Color.Blue;
                    break;
                case "Ptr":
                    color = Color.Yellow;
                    break;
                case "Pir":
                    color = Color.Beige;
                    break;
                case "Prr":
                    color = Color.Coral;
                    break;
                case "Gdr":
                    color = Color.Green;
                    break;
                case "Pcr":
                    color = Color.BlanchedAlmond;
                    break;
                case "Sdr":
                    color = Color.BlueViolet;
                    break;
                case "Tsr":
                    color = Color.Orange;
                    break;
                case "Mrr":
                    color = Color.MediumVioletRed;
                    break;
            }

            color = ControlPaint.LightLight(color);
            return color;
        }

        /// <summary>
        /// Export the stdf file to Excel
        /// </summary>
        private void ExportToExcel()
        {
            string xlsxPath = Path.ChangeExtension(lblStatus.Text, "xlsx");
            bool doesFileExists = File.Exists(xlsxPath);
            if (doesFileExists)
                File.Delete(xlsxPath);

            workbook = doesFileExists ? excel.Workbooks.Open(xlsxPath, ReadOnly: false) : excel.Workbooks.Add();
            worksheet = (Worksheet)workbook.Sheets[1];
            sheet = (Worksheet)workbook.ActiveSheet;

            int index = 1;

            index++;

            // Data
            for (int i = 0; i < entries.Count; i++)
            {
                StdfRecord record = entries[i];
                Type type = record.GetType();

                if (type != typeof(StartOfStreamRecord) && type != typeof(EndOfStreamRecord)) // Remove non-stdf related records (file stream)
                {
                    PropertyInfo[] properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

                    sheet.Cells[index, 1].Value = record.GetType().Name; // Record name

                    int counter = 3;
                    foreach (PropertyInfo property in properties)
                    {
                        object value = property.GetValue(record, null);
                        if (value != null && value != value.GetType().GetDefaultValue() && property.Name.CompareTo("RecordType") != 0) // Remove empty record fields
                        {
                            if (!value.GetType().IsArray) // Second level node
                            {
                                string valueAsString = value.ToString();
                                if (value is byte)
                                {
                                    if (property.Name.Contains("Flag") || property.Name.Contains("flag"))
                                        valueAsString = Convert.ToString((byte)value, toBase: 2).PadLeft(8, '0');
                                }

                                sheet.Cells[index, counter] = property.Name.ToSentenceCase();
                                sheet.Cells[index, counter + 1] = valueAsString;
                            }
                            else // Third level node
                            {
                                object[] array = value as object[];
                                string valueAsString = string.Empty;

                                if (array != null) // Third level node values (object array)
                                {
                                    List<object> objects = array.ToList();
                                    objects.ForEach((x) =>
                                        {
                                            if (!(x is byte))
                                                valueAsString = x.ToString();
                                            else
                                                valueAsString = Convert.ToString((byte)x, toBase: 2).PadLeft(8, '0');
                                        }
                                    );

                                    sheet.Cells[index, counter] = property.Name.ToSentenceCase();
                                    sheet.Cells[index, counter + 1] = valueAsString;
                                }
                                else // Third level node values (byte array)
                                {
                                    valueAsString = string.Empty;

                                    byte[] bytes = value as byte[];
                                    bytes.ToList().ForEach(x => valueAsString += Convert.ToString(x, toBase: 2).PadLeft(8, '0'));

                                    sheet.Cells[index, counter] = property.Name.ToSentenceCase();
                                    sheet.Cells[index, counter + 1] = valueAsString;
                                }
                            }
                        }
                    }

                    index += 2;
                }
            }

            workbook.SaveAs(
                xlsxPath,
                XlFileFormat.xlWorkbookDefault,
                Type.Missing,
                Type.Missing,
                false,
                false,
                XlSaveAsAccessMode.xlNoChange,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing,
                Type.Missing
            );
            excel.Quit();

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = xlsxPath
            };
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
        }

        #endregion Helper methods

        #region Event handlers

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Waiting for I/O operation...";
            lblRecordCounter.Text = "--";
            Text = BaseText + $" - {lblStatus.Text}";

            string path = ChooseFile();
            if (path.CompareTo(string.Empty) != 0)
            {
                lblStatus.Text = path;
                bgReaderWorker.RunWorkerAsync();
            }
        }

        private void BgReaderWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (!InvokeRequired)
                PopulateControl(lblStatus.Text, trvRecords);
            else
                BeginInvoke(new System.Action(() => PopulateControl(lblStatus.Text, trvRecords)));
        }

        private void BgExcelWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (!InvokeRequired)
                ExportToExcel();
            else
                BeginInvoke(new System.Action(() => ExportToExcel()));
        }

        private void BtnExpandAll(object sender, EventArgs e)
            => trvRecords.ExpandAll();

        private void BtnCollapseAll_Click(object sender, EventArgs e)
            => trvRecords.CollapseAll();

        private void BtnExport_Click(object sender, EventArgs e)
            => bgExcelWorker.RunWorkerAsync();

        private void CbxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nodeToShow = cbxFilter.SelectedItem.ToString();

            if (nodeToShow != "None")
            {
                foreach (TreeNode node in trvRecords.Nodes)
                {
                    if (node.Text == nodeToShow)
                        node.Expand();
                    else
                        node.Collapse();
                }
            }
        }

        private void TxbFieldName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnSearch_Click(this, new EventArgs());
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = trvRecords.SelectedNode;
            if(selectedNode != null)
            {
                if(!string.IsNullOrEmpty(txbFieldName.Text))
                {
                    string text = txbFieldName.Text.FirstCharToUpper(); // All field name have the first char in uppercase
                    
                    TreeNodeCollection nodes = selectedNode.Nodes;
                    bool flag = false;
                    for (int i = 0; i < nodes.Count && !flag; i++)
                    {
                        highlightedNode = nodes[i];
                        if (highlightedNode.Text.Contains(text))
                        {
                            flag = true;

                            highlightedNode.BackColor = ControlPaint.LightLight(Color.IndianRed);
                            highlightedNode.ForeColor = SystemColors.ControlText;
                        }
                    }

                    if(!flag)
                        MessageBox.Show($"Field named '{text}' not found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Enter a record field name first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txbFieldName.Focus();
                }
            }
            else
                MessageBox.Show("No node selected, select one node first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void TrvRecords_Click(object sender, EventArgs e)
        {
            if (highlightedNode != null)
            {
                highlightedNode.BackColor = BackColor;
                highlightedNode.ForeColor = SystemColors.Info;

                highlightedNode = null;
            }
        }

        #endregion Event handlers
    }
}
