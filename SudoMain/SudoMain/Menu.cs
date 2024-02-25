using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudoMain
{
    internal class Menu
    {
        Form1 form1;
        public Menu(Form1 form1)
        {
            this.form1 = form1;
        }

        public void MenuGen()
        {
            FlowLayoutPanel p = new FlowLayoutPanel();
            p.Height = 600;
            p.Width = 600;

            Button bStart = ButtonGen(35, 160, "Start", new Font("Arial", 15), Color.Black, Color.White,Start);
            Button bThemes = ButtonGen(35, 160, "Themes", new Font("Arial", 15), Color.Black, Color.White, Themes);
            Button bMap = ButtonGen(35, 160, "Map", new Font("Arial", 15), Color.Black, Color.White,OwnMap);
            Button bExit = ButtonGen(35, 160, "Exit", new Font("Arial", 15), Color.Black, Color.White,Exit);

            p.Controls.Add(bStart);
            p.Controls.Add(bThemes);
            p.Controls.Add(bMap);
            p.Controls.Add(bExit);
            p.FlowDirection = FlowDirection.TopDown;
            p.Location = new Point((form1.ClientSize.Width - p.Width) / 2+225, (form1.ClientSize.Height - p.Height) / 2+225);

            form1.Controls.Add(p);
        }



        Button ButtonGen(int height,int width,string text,Font f,Color fc,Color bc,Action a)
        {
            Button b = new Button();
            b.Height = height;
            b.Width = width;
            b.Text = text;
            b.Font = f; 
            b.ForeColor = fc;
            b.BackColor = bc;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderColor = Color.Black;
            b.FlatAppearance.BorderSize = 1;
            b.TextAlign = ContentAlignment.MiddleCenter;
            b.Click += (object o, EventArgs e ) => a.Invoke();
            return b;
        }

        void Start()
        {
            Game g = new Game(form1);
            g.Run();
        }

        void Themes()
        {
            ThemesMenu t = new ThemesMenu(form1);
        }

        void OwnMap()
        { 
        }

        void Exit()
        { 
            form1.Close();
        }
    }
}
