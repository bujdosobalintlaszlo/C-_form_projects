using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        Game g;
        string selectedFile;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Sudoku";
            Bitmap bitmap = new Bitmap("sudoku.png");
            Icon icon = ImgToIcon(bitmap);
            this.Icon = icon;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MenuGen();
        }

        private void MenuGen()
        {
            MenuStrip Mmain = MenuStripGen(50, this.ClientSize.Width);
            ToolStripMenuItem Main = new ToolStripMenuItem("Files");
            ToolStripMenuItem newMenuPoint = ToolStripMenuItemGen("New file", OpenFile);
            ToolStripMenuItem generateMenuItem = ToolStripMenuItemGen("Generate", GenerateSudoku);
            ToolStripMenuItem close = ToolStripMenuItemGen("Close the app", CloseApp);

            Main.DropDownItems.Add(newMenuPoint);
            Main.DropDownItems.Add(generateMenuItem);
            Main.DropDownItems.Add(close);

            Mmain.Items.Add(Main);
            this.Controls.Add(Mmain);
        }

        static Icon ImgToIcon(Bitmap bitmap)
        {
            Icon icon = null;
            try
            {
                IntPtr hIcon = bitmap.GetHicon();
                icon = Icon.FromHandle(hIcon);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba: {ex.Message}");
            }
            return icon;
        }

        public MenuStrip MenuStripGen(int height, int width)
        {
            MenuStrip ms = new MenuStrip();
            ms.Height = height;
            ms.Width = width;
            return ms;
        }

        public Button ButtonGen(int height, int width, string text, string name)
        {
            Button btn = new Button();
            btn.Height = height;
            btn.Width = width;
            btn.Text = text;
            btn.Name = name;
            return btn;
        }

        public ToolStripMenuItem ToolStripMenuItemGen(string text, EventHandler handler)
        {
            ToolStripMenuItem mi = new ToolStripMenuItem();
            mi.Text = text;
            mi.Click += handler;
            return mi;
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files|*.txt|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFile = openFileDialog.FileName;
                g = new Game(selectedFile,this);
                g.Run();
            }
        }

        private void GenerateSudoku(object sender, EventArgs e)
        {
            SudokuGen s = new SudokuGen(9,9);
            s.fillValues();
            selectedFile = "generated.txt";
            g = new Game(selectedFile, this);
            g.Run();
        }

        public void CloseApp(object sender, EventArgs e) => this.Close();
    }
}
