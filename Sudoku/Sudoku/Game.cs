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

namespace Sudoku
{
    class Game
    {
        private char[][] sudokuMap;
        private string filePath;
        private SudokuValidation sv;
        private Clock clock;
        private int mapSize;
        private Form1 form;
        DataGridView dgv = new DataGridView();
        List<Label> labelLista;
        Label LIdo;
        private FlowLayoutPanel FTimer;
        private FlowLayoutPanel FGame;
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
        public Game(string filepath,Form1 form)
        {
            this.filePath = filepath;
            this.form = form;
        }

        public void Run()
        {
            
            this.form.Controls.Clear();
            LabelLoad();
            FileBeolv();
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
            clock = new Clock(form,LIdo);
            sv = new SudokuValidation();
            if (sv.IsValidSudoku(sudokuMap))
            {
                this.form.Controls.Add(LIdo);
                mapSize = sudokuMap.Length;
                DisplayMap();
                clock.StartTimer();
            }
            else
            {
                MessageBox.Show("The choosen file is not proper or valid, please choose another one!");
            }
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
            DataGridView Dmap = DataGridViewGen(mapSize, mapSize, 50, 50, 600, 600, Color.White);
            Dmap.CellClick += new DataGridViewCellEventHandler(datagridviewCellClick);
            Dmap.RowHeadersVisible = false;
            Dmap.ColumnHeadersVisible = false;
            Dmap.ColumnCount = mapSize;
            Dmap.RowCount = mapSize;

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (sudokuMap[i][j] == '.')
                    {
                        Dmap.Rows[i].Cells[j].Value = "";
                    }
                    else
                    {
                        Dmap.Rows[i].Cells[j].Value = sudokuMap[i][j].ToString();
                    }
                }
            }

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Controls.Add(FTimer, 0, 0);
            tableLayoutPanel.Controls.Add(FGame, 0, 1);
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            form.Controls.Add(tableLayoutPanel);
            FGame.Controls.Add(Dmap);
        }


        private DataGridView DataGridViewGen(int rowAmount, int columnAmount, int cellHeight, int cellWidth, int height, int width, Color backgroundColor)
        {
            DataGridView dgv = new DataGridView();
            dgv.RowCount = rowAmount;
            for (int i = 0; i < columnAmount; i++)
            {
                dgv.Columns.Add($"Column{i + 1}", $"Column{i + 1}");
            }
            dgv.RowTemplate.Height = cellHeight;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.Width = cellWidth;
            }
            dgv.Height = height;
            dgv.Width = width;
            dgv.BackgroundColor = backgroundColor;
            return dgv;
        }

        private void datagridviewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            labelLista.ForEach(x =>{
                if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == " "){
                    x.Visible = true;
                }
            });
            */
        }
    }
}
