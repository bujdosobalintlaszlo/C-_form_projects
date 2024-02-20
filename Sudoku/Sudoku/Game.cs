using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Diagnostics.Eventing.Reader;
using System.Threading;

namespace Sudoku
{
    class Game
    {
        char[][] sudokuMap;
        string filePath;
        SudokuValidation sv;
        Clock clock;
        int mapSize;
        Form1 form;
        DataGridView dgv = new DataGridView();
        List<Label> labelLista;
        Label LIdo;
        FlowLayoutPanel FTimer;
        FlowLayoutPanel FGame;
        DataGridView Dmap;
        DataGridView Doption;
        private void LabelLoad()
        {
            labelLista = new List<Label>();
            for (int i = 0; i < 9; i++)
            {
                Label l = new Label();
                l.Location = new Point(500, 27 + i * 50);
                l.Size = new Size(50, 50);
                l.Visible = false;
                l.Text = (i + 1).ToString();
                form.Controls.Add(l);
                labelLista.Add(l);

            }
        }
        public Game(string filepath, Form1 form)
        {
            this.filePath = filepath;
            this.form = form;
        }

        public void Run()
        {

            this.form.Controls.Clear();
            FileBeolv();
            SetupValues();
            if (sv.IsValidSudoku(sudokuMap))
            {
                this.form.Controls.Add(LIdo);
                mapSize = sudokuMap.Length;
                DisplayMap();
                clock.StartTimer();
                OptionGen();
            }
            else
            {
                MessageBox.Show("The choosen file is not proper or valid, please choose another one!");
            }
        }
        void SetupValues()
        {
            FTimer = new FlowLayoutPanel();
            FTimer.Width = form.Width;
            FTimer.Height = 20;
            FGame = new FlowLayoutPanel();
            FGame.Height = form.Height;
            FGame.Width = form.Width;
            LIdo = new Label();
            LIdo.Width = form.Width;
            LIdo.TextAlign = ContentAlignment.MiddleCenter;
            FTimer.Controls.Add(LIdo);
            clock = new Clock(form, LIdo);
            sv = new SudokuValidation();
        }
        private void FileBeolv()
        {
            StreamReader r = new StreamReader(filePath);
            sudokuMap = new char[9][];
            List<string> split = r.ReadLine().Split(';').ToList();
            for (int i = 0; i < sudokuMap.Length; i++)
            {
                sudokuMap[i] = new char[9];
                string[] temp = split[i].Split(',');
                for (int j = 0; j < sudokuMap[i].Length; j++)
                {
                    sudokuMap[i][j] = Convert.ToChar(temp[j].Trim('\"'));
                }
            }
            r.Close();
        }


        private void DisplayMap()
        {
            Dmap = DataGridViewGen(mapSize, mapSize, 60, 60, 543, 543, Color.White);
            DmapDesignSet();


            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (sudokuMap[i][j] == '.')
                    {
                        Dmap.Rows[i].Cells[j].Value = "";
                        Dmap.Rows[i].Resizable = DataGridViewTriState.False;
                        Dmap.Rows[i].Cells[j].Style.BackColor = Color.LemonChiffon;
                        Dmap.Rows[i].Cells[j].Tag = i + j;
                    }
                    else
                    {
                        Dmap.Rows[i].Cells[j].Value = sudokuMap[i][j].ToString();
                        Dmap.Rows[i].Resizable = DataGridViewTriState.False;
                        Dmap.Rows[i].Cells[j].Style.BackColor = Color.Wheat;
                        Dmap.Rows[i].Cells[j].Style.ForeColor = Color.White;
                        Dmap.Rows[i].Cells[j].Style.Font = new Font("Arial", 30);
                        Dmap.Rows[i].Cells[j].Tag = "x";
                    }
                    Dmap.Rows[i].Cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            TableLayoutSetup();
        }
        void DmapDesignSet()
        {
            Dmap.CellClick += new DataGridViewCellEventHandler(datagridviewCellClick);
            Dmap.RowHeadersVisible = false;
            Dmap.ColumnHeadersVisible = false;
            Dmap.ColumnCount = mapSize;
            Dmap.RowCount = mapSize;
            Dmap.ReadOnly = true;
            Dmap.AllowUserToResizeRows = false;
            Dmap.AllowUserToResizeColumns = false;
        }
        void TableLayoutSetup()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Controls.Add(FTimer, 0, 0);
            tableLayoutPanel.Controls.Add(FGame, 0, 1);
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            form.Controls.Add(tableLayoutPanel);
            FGame.Controls.Add(Dmap);
        }
        DataGridView DataGridViewGen(int rowAmount, int columnAmount, int cellHeight, int cellWidth, int height, int width, Color backgroundColor)
        {
            dgv = new DataGridView();
            dgv.RowCount = rowAmount;
            for (int i = 0; i < columnAmount; i++)
            {
                dgv.Columns.Add($"Column{i + 1}", $"Column{i + 1}");
                dgv.Columns[i].Width = cellWidth;
                dgv.Rows[i].Height = cellHeight;
            }
            dgv.Height = height;
            dgv.Width = width;
            dgv.BackgroundColor = backgroundColor;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = false;
            return dgv;
        }

        void OptionGen()
        {
            Doption = DataGridViewGen(3, 3, 60, 60, 182, 180, Color.Wheat);
            Doption.CellClick += new DataGridViewCellEventHandler(SelectedNum);
            Doption.RowHeadersVisible = false;
            Doption.ColumnHeadersVisible = false;
            Doption.ReadOnly = true;
            Doption.AllowUserToResizeRows = false;
            Doption.AllowUserToResizeColumns = false;
            Doption.ScrollBars = ScrollBars.None;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Doption.Rows[i].Cells[j].Value = (i * 3 + j + 1).ToString();
                    Doption.Rows[i].Cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Doption.Rows[i].Cells[j].Tag = (i * 3 + j + 1);
                }
            }
            Doption.Columns.RemoveAt(3);
            //Doption.BackgroundColor = Color.Wheat;
            
            Button bDelete = new Button();
            bDelete.Text = "Delete";
            bDelete.Size = new Size(100, 30);
            bDelete.Location = new Point(10, Doption.Bottom + 10 - FGame.AutoScrollPosition.Y);
            FGame.Controls.Add(bDelete);
            FGame.Controls.Add(Doption);
            //kell egy negyedik sor amiben egyesítve vannak a cellak
        }
        string lastClickedRowIndex;
        string lastClickedColumnIndex;
        int selectedNum;
        void datagridviewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && selectedNum != 0) // Ellenőrizd, hogy a kattintás egy cellán belül történt-e
            {
                DataGridViewCell clickedCell = Dmap.Rows[e.RowIndex].Cells[e.ColumnIndex];
                lastClickedRowIndex = Convert.ToString(e.RowIndex);
                lastClickedColumnIndex = Convert.ToString(e.ColumnIndex);
                if (clickedCell.Tag != "x")
                {
                    Dmap.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = selectedNum;
                    Dmap.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.ForestGreen;
                    Dmap.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = new Font("Arial", 30);
                }
                else
                {
                    Dmap.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                    Thread.Sleep(1000);
                    Dmap.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Wheat;
                }

                // Most használd a cellTag változót az adott cella tag-jének felhasználásához
                // Példa: Console.WriteLine("A kiválasztott cella Tag-je: " + cellTag.ToString());
            }
        }


        void SelectedNum(object o, DataGridViewCellEventArgs e)
        {
            string temp = Convert.ToString(Doption.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag);
            //sudokuMap[e.RowIndex][e.ColumnIndex] = temp;
            if (CheckIfAllowed(temp, Convert.ToInt32(lastClickedRowIndex), Convert.ToInt32(lastClickedColumnIndex)))
            {
                selectedNum = int.Parse(Convert.ToString(temp));
            }
            else
            {
                sudokuMap[Convert.ToInt32(lastClickedRowIndex)][Convert.ToInt32(lastClickedColumnIndex)] = ' ';
                Dmap.Rows[Convert.ToInt32(lastClickedRowIndex)].Cells[Convert.ToInt32(lastClickedColumnIndex)].Style.ForeColor = Color.Red;
            }
        }

        bool CheckIfAllowed(string num, int row, int col)
        {
            // Ellenőrizzük a sorban
            for (int i = 0; i < 9; i++)
            {
                if (i != col && sudokuMap[row][i] == Convert.ToChar(num))
                {
                    return false; // Az adott szám már szerepel a sorban
                }
            }

            // Ellenőrizzük az oszlopban
            for (int i = 0; i < 9; i++)
            {
                if (i != row && sudokuMap[i][col] == Convert.ToChar(num))
                {
                    return false; // Az adott szám már szerepel az oszlopban
                }
            }

            // Ellenőrizzük a 3x3-as négyzetben
            int boxStartRow = row - row % 3;
            int boxStartCol = col - col % 3;
            for (int i = boxStartRow; i < boxStartRow + 3; i++)
            {
                for (int j = boxStartCol; j < boxStartCol + 3; j++)
                {
                    if (i != row && j != col && sudokuMap[i][j] == Convert.ToChar(num))
                    {
                        return false; // Az adott szám már szerepel a 3x3-as négyzetben
                    }
                }
            }

            return true; // Az adott szám engedélyezett
        }



    }
}
