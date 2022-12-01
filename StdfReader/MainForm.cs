using LinqToStdf;
using LinqToStdf.Records;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
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

        private Dictionary<string, Color> colors;

        private Excel excel;
        private Workbook workbook;
        private Worksheet worksheet;
        private Worksheet sheet;

        private List<StdfRecord> entries;

        /// <summary>
        /// Create a new instance of <see cref="MainForm"/>
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            CenterToScreen();

            bgWorker.DoWork += BgWorker_DoWork;

            colors = new Dictionary<string, Color>();

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
            if (type != typeof(StartOfStreamRecord) && type != typeof(EndOfStreamRecord)) // Remove non-stdf related records (file stream)
            {
                PropertyInfo[] properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
                TreeNode root = new TreeNode(record.GetType().Name); // Root, first level node

                if (index != -1)
                    root.ToolTipText = $"Record number {index}";

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

                            TreeNode leaf = new TreeNode($"{property.Name.ToSentenceCase()}: {valueAsString}");
                            root.Nodes.Add(leaf);
                        }
                        else // Third level node
                        {
                            object[] array = value as object[];
                            TreeNode node = new TreeNode(property.Name.ToSentenceCase());

                            if (array != null) // Third level node values (object array)
                            {
                                List<object> objects = array.ToList();
                                objects.ForEach((x) =>
                                    {
                                        if (!(x is byte))
                                            node.Nodes.Add(new TreeNode(x.ToString()));
                                        else
                                            node.Nodes.Add(new TreeNode(Convert.ToString((byte)x, toBase: 2).PadLeft(8, '0')));
                                    }
                                );
                            }
                            else // Third level node values (byte array)
                            {
                                byte[] bytes = value as byte[];
                                bytes.ToList().ForEach(x => node.Nodes.Add(new TreeNode(Convert.ToString(x, toBase: 2).PadLeft(8, '0'))));
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
            string text = string.Empty;

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
        }

        /// <summary>
        /// Generate a random color from a seed
        /// </summary>
        /// <param name="seed">The seed <see cref="string"/></param>
        /// <returns>The generated <see cref="Color"/></returns>
        private Color GenerateColor(string seed)
        {
            Color color = Color.Green;

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
                case "Pcr":
                    color = Color.BlanchedAlmond;
                    break;
                case "Sdr":
                    color = Color.BlueViolet;
                    break;
                case "Tsr":
                    color = Color.Orange;
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

                    sheet.Cells[2, index].Value = record.GetType().Name; // Record name

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

                                sheet.Cells[counter, index] = property.Name.ToSentenceCase();
                                sheet.Cells[counter, index + 1] = valueAsString;
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

                                    sheet.Cells[counter, index] = property.Name.ToSentenceCase();
                                    sheet.Cells[counter, index + 1] = valueAsString;
                                }
                                else // Third level node values (byte array)
                                {
                                    valueAsString = string.Empty;

                                    byte[] bytes = value as byte[];
                                    bytes.ToList().ForEach(x => valueAsString += Convert.ToString(x, toBase: 2).PadLeft(8, '0'));

                                    sheet.Cells[counter, index] = property.Name.ToSentenceCase();
                                    sheet.Cells[counter, index + 1] = valueAsString;
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
        }

        #endregion Helper methods

        #region Event handlers

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Waiting for I/O operation...";
            Text = BaseText + $" - {lblStatus.Text}";

            string path = ChooseFile();
            if (path.CompareTo(string.Empty) != 0)
            {
                lblStatus.Text = path;
                bgWorker.RunWorkerAsync();
            }
        }

        private void BgWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (!InvokeRequired)
                PopulateControl(lblStatus.Text, trvRecords);
            else
                BeginInvoke(new System.Action(() => PopulateControl(lblStatus.Text, trvRecords)));
        }

        private void BtnExpandAll(object sender, EventArgs e)
            => trvRecords.ExpandAll();

        private void BtnCollapseAll_Click(object sender, EventArgs e)
            => trvRecords.CollapseAll();

        private void BtnExport_Click(object sender, EventArgs e)
            => ExportToExcel();

        #endregion Event handlers
    }
}
