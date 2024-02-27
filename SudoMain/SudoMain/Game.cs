using SudoMain.Sudoku;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SudoMain
{
    internal class Game
    {
        Form1 form1;
        int[,] map;

        Label LIdo;
        Clock clock;
        FlowLayoutPanel Fleft = new FlowLayoutPanel();
        DataGridView Dmap;

        FlowLayoutPanel Fright = new FlowLayoutPanel();

        FlowLayoutPanel FTimer = new FlowLayoutPanel();
        DataGridView Doption;
        DataGridView dgv;
        int ures;
        public Game(Form1 form1)
        {
            this.form1 = form1;
        }

        public void Run()
        {
            form1.Controls.Clear();
            ValueGen();
            PlayGroundGen();
            TimerGen();
            TableGen();
            OptionGen();
        }
        void ValueGen()
        {
            SudokuGen s = new SudokuGen(9, 49);
            map = s.GenerateSudoku();
        }
        void PlayGroundGen()
        {
            Fleft = new FlowLayoutPanel();
            Fright = new FlowLayoutPanel();
            FTimer = new FlowLayoutPanel();
            Dmap = new DataGridView();  
            
            Fleft.Dock = DockStyle.Left;
            Fleft.Width = form1.ClientSize.Width / 2; ;
            Fleft.BackColor = Color.White;

            Fright.Dock = DockStyle.Right;
            Fright.Width = form1.ClientSize.Width / 2;
            Fright.BackColor = Color.White;

            FTimer.Dock = DockStyle.Top;
            FTimer.Height = 80;
            FTimer.Width = form1.ClientSize.Width;
            FTimer.BackColor = Color.White;

            form1.Controls.Add(Fleft);
            form1.Controls.Add(Fright);
            form1.Controls.Add(FTimer);
            
        }

        void TableGen()
        {
            Dmap = DataGridViewGen(9, 9, 60, 60, 543, 543, Color.White);
            DmapDesignSet();
            Dmap = DataGridViewGen(9, 9, 60, 60, 543, 543, Color.White);
            DmapDesignSet();

            Fleft.Controls.Add(Dmap);
            Fleft.SetFlowBreak(Dmap, false);
            int horizontalMargin = (Fleft.ClientSize.Width - Dmap.Width) / 2;
            int verticalMargin = (Fleft.ClientSize.Height - Dmap.Height) / 2;
            Dmap.Margin = new Padding(horizontalMargin, verticalMargin, 0, 0);

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i,j] == 0)
                    {
                        Dmap.Rows[i].Cells[j].Value = "";
                        Dmap.Rows[i].Resizable = DataGridViewTriState.False;
                        Dmap.Rows[i].Cells[j].Style.BackColor = Color.LemonChiffon;
                        Dmap.Rows[i].Cells[j].Tag = i + j;
                        ures++;
                    }
                    else
                    {
                        Dmap.Rows[i].Cells[j].Value = map[i,j].ToString();
                        Dmap.Rows[i].Resizable = DataGridViewTriState.False;
                        Dmap.Rows[i].Cells[j].Style.BackColor = Color.Black;
                        Dmap.Rows[i].Cells[j].Style.ForeColor = Color.White;
                        Dmap.Rows[i].Cells[j].Style.Font = new Font("Arial", 30);
                        Dmap.Rows[i].Cells[j].Tag = "x";
                    }
                    Dmap.Rows[i].Cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        void OptionGen()
        {
            Doption = DataGridViewGen(3, 3, 100, 100, 300, 300, Color.Wheat);
            Doption.CellClick += new DataGridViewCellEventHandler(SelectedNum);
            Doption.RowHeadersVisible = false;
            Doption.ColumnHeadersVisible = false;
            Doption.ReadOnly = true;
            Doption.AllowUserToResizeRows = false;
            Doption.AllowUserToResizeColumns = false;
            Doption.ScrollBars = ScrollBars.None;
            Doption.MultiSelect = false;
            Fright.SetFlowBreak(Dmap, false);
            int horizontalMargin = (Fleft.ClientSize.Width - Dmap.Width) / 2 + 122;
            int verticalMargin = (Fleft.ClientSize.Height - Dmap.Height) / 2 + 100;
            Doption.Margin = new Padding(horizontalMargin, verticalMargin, 0, 0);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Doption.Rows[i].Cells[j].Value = (i * 3 + j + 1).ToString();
                    Doption.Rows[i].Cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Doption.Rows[i].Cells[j].Tag = (i * 3 + j + 1);
                    Doption.Rows[i].Cells[j].Style.BackColor = Color.LemonChiffon;
                    Doption.Rows[i].Cells[j].Style.ForeColor = Color.Brown;
                    Doption.Rows[i].Cells[j].Style.Font = new Font("Arial", 20);
                }
            }
            Doption.Columns.RemoveAt(3);
            Fright.Controls.Add(Doption);
            Actions();

        }
        Button bDelete;
        Button bUndo;
        Button bSolve;
        void Actions()
        {
            FlowLayoutPanel Fcontrols = new FlowLayoutPanel();
            Fcontrols.FlowDirection = FlowDirection.LeftToRight;
            Fcontrols.Width = Fright.Width;
            Fcontrols.Dock = DockStyle.Top;
            //Fcontrols.BackColor = Color.Red;
            Fcontrols.WrapContents = false; // Prevent wrapping to the next line

            bDelete = new Button();
            bDelete.Text = "Delete";
            bDelete.Size = new Size(160, 50);
            bDelete.Click += Delete;
            bDelete.BackColor = Color.LemonChiffon;
            bDelete.ForeColor = Color.Brown;
            bDelete.Margin = new Padding((Fcontrols.Width - bDelete.Width * 3) / 6, (Fcontrols.Height - bDelete.Height) / 2, (Fcontrols.Width - bDelete.Width * 3) / 6, 0); // Center horizontally and vertically

            bUndo = new Button();
            bUndo.Text = "Empty hand";
            bUndo.Size = new Size(160, 50);
            bUndo.Click += Undo;
            bUndo.BackColor = Color.LemonChiffon;
            bUndo.ForeColor = Color.Brown;
            bUndo.Margin = new Padding((Fcontrols.Width - bUndo.Width * 3) / 6, (Fcontrols.Height - bUndo.Height) / 2, (Fcontrols.Width - bUndo.Width * 3) / 6, 0); // Center horizontally and vertically

            bSolve = new Button();
            bSolve.Text = "Solve";
            bSolve.Size = new Size(160, 50);
            bSolve.Click += Solve;
            bSolve.BackColor = Color.LemonChiffon;
            bSolve.ForeColor = Color.Brown;
            bSolve.Margin = new Padding((Fcontrols.Width - bSolve.Width * 3) / 6, (Fcontrols.Height - bSolve.Height) / 2, (Fcontrols.Width - bSolve.Width * 3) / 6, 0); // Center horizontally and vertically

            Fcontrols.Controls.Add(bDelete);
            Fcontrols.Controls.Add(bUndo);
            Fcontrols.Controls.Add(bSolve);
            
            Fright.Controls.Add(Fcontrols);
        }

        void Solve(object o, EventArgs e)
        {
            SolveClass s = new SolveClass();
            int[,] solved = s.SolveSudoku(map);
            SolveTable(solved);
        }

        async void SolveTable(int[,] m)
        {
            clock.StopTimer();
            form1.Enabled = false;
            bDelete.Enabled = false;
            bUndo.Enabled = false;
            bSolve.Enabled = false;
            for (int i = 0; i < Dmap.Rows.Count; i++)
            {
                for (int x = 0; x < Dmap.Columns.Count; x++)
                {
                    if (Dmap.Rows[i].Cells[x].Tag != "x")
                    {
                        if (Dmap.Rows[i].Cells[x].Tag != "x" && Dmap.Rows[i].Cells[x].Value != " ")
                        {
                            Dmap.Rows[i].Cells[x].Value = " ";
                        }
                        Dmap.Rows[i].Cells[x].Value = m[i, x];
                        Dmap.Rows[i].Cells[x].Style.ForeColor = Color.Brown;
                        Dmap.Rows[i].Cells[x].Style.Font = new Font("Arial", 30);
                        await Task.Delay(100);
                    }
                }
            }
            await Task.Delay(5000);
            form1.Close();
        }

        void Undo(object o, EventArgs e)
        {
            selectedNum = new int();
        }

        void DmapDesignSet()
        {
            Dmap.SelectionChanged += new EventHandler(datagridviewCellClick);
            Dmap.RowHeadersVisible = false;
            Dmap.ColumnHeadersVisible = false;
            Dmap.ColumnCount = 9;
            Dmap.RowCount = 9;
            Dmap.ReadOnly = true;
            Dmap.AllowUserToResizeRows = false;
            Dmap.AllowUserToResizeColumns = false;
            Dmap.MultiSelect = false;
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

        void TimerGen()
        {
            LIdo = new Label();
            FTimer.Controls.Add(LIdo);
            clock = new Clock(LIdo);
            clock.StartTimer();
            TimerDesign();
        }

        void TimerDesign()
        { 
            LIdo.TextAlign = ContentAlignment.MiddleCenter;
            LIdo.Font = new Font("Arial", 30);
            LIdo.ForeColor = Color.Black;
            LIdo.Height = FTimer.Height;
            LIdo.Width = FTimer.Width;
        }

        string lastClickedRowIndex;
        string lastClickedColumnIndex;
        int selectedNum;
        void SelectedNum(object o, DataGridViewCellEventArgs e)
        {
            string temp = Convert.ToString(Doption.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag);
            if (CheckIfAllowed(temp, Convert.ToInt32(lastClickedRowIndex), Convert.ToInt32(lastClickedColumnIndex)))
            {
                selectedNum = int.Parse(Convert.ToString(temp));
            }
            else
            {
                //map[Convert.ToInt32(lastClickedRowIndex),Convert.ToInt32(lastClickedColumnIndex)] = ' ';
                //Dmap.Rows[Convert.ToInt32(lastClickedRowIndex)].Cells[Convert.ToInt32(lastClickedColumnIndex)].Style.ForeColor = Color.Red;
                selectedNum = int.Parse(Convert.ToString(temp));
            }
        }

        bool CheckIfAllowed(string num, int row, int col)
        {
            for (int i = 0; i < Dmap.Rows[row].Cells.Count; i++)
            {
                if (i != col && map[row, i] == Convert.ToInt32(num))
                {
                    return false;
                }
            }

            for (int i = 0; i < 9; i++)
            {
                if (i != row && map[i, col] == Convert.ToInt32(num))
                {
                    return false;
                }
            }

            int boxStartRow = row - row % 3;
            int boxStartCol = col - col % 3;
            for (int i = boxStartRow; i < boxStartRow + 3; i++)
            {
                for (int j = boxStartCol; j < boxStartCol + 3; j++)
                {
                    if (i != row || j != col && map[i, j] == Convert.ToInt32(num))
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        void Delete(object o, EventArgs e)
        {
            Dmap.Rows[Convert.ToInt32(lastClickedRowIndex)].Cells[Convert.ToInt32(lastClickedColumnIndex)].Value = " ";
            Cellaures();
        }

        private void datagridviewCellClick(object sender, EventArgs e)
        {
            if (Dmap.SelectedCells[0].RowIndex >= 0 && Dmap.SelectedCells[0].ColumnIndex >= 0 && selectedNum != 0)
            {
                int x = Dmap.CurrentCell.RowIndex;
                int y = Dmap.CurrentCell.ColumnIndex;
                DataGridViewCell clickedCell = Dmap.Rows[x].Cells[y];
                lastClickedRowIndex = Convert.ToString(x);
                lastClickedColumnIndex = Convert.ToString(y);
                if (clickedCell.Tag != "x")
                {
                    Fright.Enabled = true;
                    if (Dmap.CurrentCell.Value == null)
                    {
                        ures--;
                    }

                    string ertek = selectedNum.ToString();

                    if (Dmap.CurrentCell.Style.BackColor == Color.IndianRed)
                    {
                        //visszaszinez
                        beszinezSor(x, Dmap.CurrentCell.Value.ToString(), Color.LemonChiffon);
                        beszinezOszlop(y, Dmap.CurrentCell.Value.ToString(), Color.LemonChiffon);
                        beszinezNegyzet(x, y, Dmap.CurrentCell.Value.ToString(), Color.LemonChiffon);
                    }
                    bool voltRossz = false;
                    if (vanASorban(x, ertek))
                    {
                        Dmap.CurrentCell.Value = ertek;
                        beszinezSor(x, ertek, Color.IndianRed);
                        voltRossz = true;
                    }
                    if (vanAzOszlopban(y, ertek))
                    {
                        Dmap.CurrentCell.Value = ertek;
                        beszinezOszlop(y, ertek, Color.IndianRed);
                        voltRossz = true;
                    }
                    if (vanANegyzetben(x, y, ertek))
                    {
                        Dmap.CurrentCell.Value = ertek;
                        beszinezNegyzet(x, y, ertek, Color.IndianRed);
                        voltRossz = true;
                    }
                    if (!voltRossz)
                    {
                        if (Dmap.CurrentCell.Style.BackColor == Color.IndianRed)
                        {
                            beszinezSor(x, Dmap.CurrentCell.Value.ToString(), Color.LemonChiffon);
                            beszinezOszlop(y, Dmap.CurrentCell.Value.ToString(), Color.LemonChiffon);
                            beszinezNegyzet(x, y, Dmap.CurrentCell.Value.ToString(), Color.LemonChiffon);
                        }
                        Dmap.CurrentCell.Value = ertek;

                    }
                    
                    if (ures == 0 && NincsPiros())
                    {
                        MessageBox.Show("nyertél");
                    }
                }
                else
                { 
                    Fright.Enabled = false;
                }

            }

        }
        void Cellaures()
        {
            for (int i = 0; i < Dmap.RowCount; ++i)
            {
                for (int j = 0; j < Dmap.ColumnCount; ++j)
                {
                    if (Dmap.Rows[i].Cells[j].Value == null)
                    {
                        if (Dmap.Rows[i].Cells[j].Tag.ToString() == "x")
                        {
                            Dmap.Rows[i].Cells[j].Style.BackColor = Color.Black;
                        }
                        else
                        {
                            Dmap.Rows[i].Cells[j].Style.BackColor = Color.LemonChiffon;
                        }
                    }
                    else
                    {
                        if (Dmap.Rows[i].Cells[j].Tag.ToString() == "x")
                        {
                            Dmap.Rows[i].Cells[j].Style.BackColor = Color.Black;
                        }
                        else
                        {
                            Dmap.Rows[i].Cells[j].Style.BackColor = Color.LemonChiffon;
                        }
                    }
                }
            }
        }



        bool NincsPiros()
        {
            for (int i = 0; i < Dmap.RowCount; ++i)
            {
                for (int j = 0; j < Dmap.ColumnCount; ++j)
                {
                    if (Dmap.Rows[i].Cells[j].Style.BackColor == Color.IndianRed)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool vanASorban(int sor, string ertek)
        {
            for (int i = 0; i < Dmap.Rows[sor].Cells.Count; i++)
            {
                if (Dmap.Rows[sor].Cells[i].Value != null &&
                    Dmap.Rows[sor].Cells[i].Value.ToString() == ertek)
                {
                    return true;
                }
            }
            return false;
        }



        private void beszinezSor(int sor, string ertek, Color szin)
        {
            for (int i = 0; i < Dmap.Rows[sor].Cells.Count; i++)
            {
                if (Dmap.Rows[sor].Cells[i].Value != null &&
                    Dmap.Rows[sor].Cells[i].Value.ToString() == ertek)
                {
                    if (Dmap.Rows[sor].Cells[i].Tag == "x" && szin == Color.LemonChiffon)
                    {
                        Dmap.Rows[sor].Cells[i].Style.BackColor = Color.Wheat;
                    }
                    else
                    {
                        Dmap.Rows[sor].Cells[i].Style.BackColor = szin;
                    }
                }
            }
        }

        private bool vanAzOszlopban(int oszlop, string ertek)
        {
            for (int i = 0; i < Dmap.RowCount; i++)
            {
                if (Dmap.Rows[i].Cells[oszlop].Value != null &&
                    Dmap.Rows[i].Cells[oszlop].Value.ToString() == ertek)
                {
                    return true;
                }
            }
            return false;
        }

        private void beszinezOszlop(int oszlop, string ertek, Color szin)
        {
            for (int i = 0; i < Dmap.RowCount; i++)
            {
                if (Dmap.Rows[i].Cells[oszlop].Value != null &&
                    Dmap.Rows[i].Cells[oszlop].Value.ToString() == ertek)
                {
                    if (Dmap.Rows[i].Cells[oszlop].Tag == "x" && szin == Color.LemonChiffon)
                    {
                        Dmap.Rows[i].Cells[oszlop].Style.BackColor = Color.Wheat;
                    }
                    else
                    {
                        Dmap.Rows[i].Cells[oszlop].Style.BackColor = szin;
                    }
                }
            }
        }

        private bool vanANegyzetben(int sor, int oszlop, string ertek)
        {
            int balFelsoSor = sor - sor % 3;
            int balFelsoOszlop = oszlop - oszlop % 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Dmap.Rows[balFelsoSor + i].Cells[balFelsoOszlop + j].Value != null &&
                        Dmap.Rows[balFelsoSor + i].Cells[balFelsoOszlop + j].Value.ToString() == ertek)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        private void beszinezNegyzet(int sor, int oszlop, string ertek, Color szin)
        {
            int bfs = sor - sor % 3;
            int bfo = oszlop - oszlop % 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Dmap.Rows[bfs + i].Cells[bfo + j].Value != null &&
                        Dmap.Rows[bfs + i].Cells[bfo + j].Value.ToString() == ertek)
                    {

                        if (Dmap.Rows[bfs + i].Cells[bfo + j].Tag == "x" && szin == Color.LemonChiffon)
                        {
                            Dmap.Rows[bfs + i].Cells[bfo + j].Style.BackColor = Color.Wheat;
                        }
                        else
                        {
                            Dmap.Rows[bfs + i].Cells[bfo + j].Style.BackColor = szin;
                        }
                    }

                }
            }
        }
    }
}
