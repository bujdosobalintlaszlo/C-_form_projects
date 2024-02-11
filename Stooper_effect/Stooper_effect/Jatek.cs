using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Xml;

namespace Stooper_effect
{
    /// <summary>
    /// Osszes jatekfolyamtot tartalmazza. Ora, navigacio, gombok, szovegmegjelenites
    /// </summary>
    public class Jatek
    {
        Random r = new Random();

        //meglevo hasznalt osztalyok
        private Menu menu;
        private Form1 form;
        //--

        //navigacio es ora
        private Timer idozito;
        private int mp;
        private int p;
        private Label LIdo = new Label();
        private Button BMegallit = new Button();
        private Button BFolytat = new Button();
        private Button BVissza = new Button();
        private Button BKIlep = new Button();
        //--

        //Magahoz a jatekhoz kell(gomb,feliratok)
        private FlowLayoutPanel Fmain = new FlowLayoutPanel();
        private Label kellSzoveg = new Label();
        private Label  szinSzoveg= new Label();
        private List<Color> szinek = new List<Color>() {Color.Red,Color.Green,Color.Blue,Color.Black,Color.Yellow };
        private List<string> kellLista = new List<string>() {"Szöveg","Szín"};
        private List<string> irtSzin = new List<string>() { "Piros", "Zöld", "Kék", "Fekete", "Sárga" };
        private List<Action<object, EventArgs>> szinEsemenyek = new List<Action<object, EventArgs>>();
        private string keresett;
        private int jok;
        private int rossz;
        //---

        /// <summary>
        /// kostruktor
        /// </summary>
        /// <param name="form">Az eredeti form</param>
        public Jatek(Form1 form)
        {
            this.form = form;
        }

        /// <summary>
        /// feltolti a listat amibe az actionokat tárolom
        /// </summary>
        private void SzinEsFelt()
        {
            szinEsemenyek.Add((sender, e) => Piros(sender, e));
            szinEsemenyek.Add((sender, e) => Zold(sender, e));
            szinEsemenyek.Add((sender, e) => Kek(sender, e));
            szinEsemenyek.Add((sender, e) => Fekete(sender, e));
            szinEsemenyek.Add((sender, e) => Sarga(sender, e));
        }

        /// <summary>
        /// Egesz jatek megjelenitese. stropper,menugomb,maga a jatek
        /// </summary>
        public void Megjelenit()
        {
            SzinEsFelt();
            menu = new Menu(this.form);
            Fmain = new FlowLayoutPanel();
            this.form.Controls.Clear();
            FrissitIdoLabel();
            Ora();
            NavGen();

            LIdo.Anchor = AnchorStyles.None;
            LIdo.BackColor = Color.Transparent;
            LIdo.Dock = DockStyle.None;
            LIdo.TextAlign = ContentAlignment.MiddleCenter;
            LIdo.Location = new Point((form.ClientSize.Width - LIdo.Width) / 2, 0);
            LIdo.ForeColor = Color.Red;
            LIdo.Font = new Font("Arial", 15);

            BMegallit.Height = 35;
            BMegallit.Width = 125;
            BMegallit.Anchor = AnchorStyles.None;
            BMegallit.Dock = DockStyle.None;
            BMegallit.Location = new Point((form.ClientSize.Width - BMegallit.Width) / 2, LIdo.Bottom);

            Fmain.FlowDirection = FlowDirection.TopDown;
            Fmain.Dock = DockStyle.None;
            Fmain.AutoSize = false;
            Fmain.WrapContents = false;
            Fmain.Width = 600;

            Fmain.Anchor = AnchorStyles.None;
            Fmain.Location = new Point((form.ClientSize.Width - Fmain.Width) / 2 + 100, (form.ClientSize.Height - Fmain.Height) / 2);

            kellSzoveg.TextAlign = ContentAlignment.MiddleCenter;
            szinSzoveg.TextAlign = ContentAlignment.MiddleCenter;

            this.form.Controls.Add(Fmain);
            this.form.Controls.Add(LIdo);
            this.form.Controls.Add(BMegallit);

            Szoveg();
            Gombok();
        }
        /// <summary>
        /// Gombok generalasa
        /// </summary>
        private void Gombok()
        {
            FlowLayoutPanel gombPanel = new FlowLayoutPanel();
            gombPanel.FlowDirection = FlowDirection.LeftToRight;
            gombPanel.Dock = DockStyle.None;
            gombPanel.AutoSize = true;
            gombPanel.WrapContents = false;

            for (int i = 0; i < 5; i++)
            {
                Button b = new Button();
                b.BackColor = szinek[i];

                int currentI = i;

                b.Click += (object o, EventArgs e) => szinEsemenyek[currentI].Invoke(o, e);

                gombPanel.Controls.Add(b);
            }

            Fmain.Controls.Add(gombPanel);
        }
        /// <summary>
        /// piros func
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void Piros(object o,EventArgs e)
        {
            if (keresett == "Piros")
            {
                jok++;
            }
            else
            {
                rossz++;
            }
            Fmain.Controls.Clear();
            Szoveg();
            Gombok();
        }
        /// <summary>
        /// zold func
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void Zold(object o, EventArgs e)
        {
            if (keresett == "Zold")
            {
                jok++;
            }
            else
            {
                rossz++;
            }
            Fmain.Controls.Clear();
            Szoveg();
            Gombok();
        }
        /// <summary>
        /// kek func
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void Kek(object o, EventArgs e)
        {
            if (keresett == "Kek")
            {
                jok++;
            }
            else
            {
                rossz++;
            }
            Fmain.Controls.Clear();
            Szoveg();
            Gombok();
        }
        /// <summary>
        /// fekete func
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void Fekete(object o, EventArgs e)
        {
            if (keresett == "Fekete")
            {
                jok++;
            }
            else
            {
                rossz++;
            }
            Fmain.Controls.Clear();
            Szoveg();
            Gombok();
        }
        /// <summary>
        /// sarga func
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void Sarga(object o, EventArgs e)
        {
            if (keresett == "Sarga")
            {
                jok++;
            }
            else
            {
                rossz++;
            }
            Fmain.Controls.Clear();
            Szoveg();
            Gombok();
        }

        /// <summary>
        /// Itt van az a resz amikor ket szot jelenitunk meg. Egyik az hogy a szoveget vagy a szint nezzuk. A masik egy szin annak szinten adunk egy szint, amit az elso szo alapjan kell figyelembevenni.
        /// </summary>
        private void Szoveg()
        {
            kellSzoveg = new Label();
            szinSzoveg = new Label();
            kellSzoveg.Font = new Font("Arial", 20);
            kellSzoveg.Anchor = AnchorStyles.None;
            kellSzoveg.AutoSize = true;

            szinSzoveg.Font = new Font("Arial", 20);
            szinSzoveg.Anchor = AnchorStyles.None;
            szinSzoveg.AutoSize = true;

            int dont = r.Next(0, 2);

            if (dont == 0)
            {
                kellSzoveg.Text = "Szöveg";
                int kerSzinIndex = r.Next(0, 5);
                keresett = irtSzin[kerSzinIndex];
                szinSzoveg.ForeColor = szinek[r.Next(0, 5)];
                szinSzoveg.Text = irtSzin[kerSzinIndex];
            }
            else
            {
                kellSzoveg.Text = "Szín";
                int kerSzinIndex = r.Next(0, 5);
                keresett = irtSzin[kerSzinIndex];
                szinSzoveg.ForeColor = szinek[kerSzinIndex];
                szinSzoveg.Text = irtSzin[r.Next(0, 5)];
            }

            Fmain.Controls.Add(kellSzoveg);
            Fmain.Controls.Add(szinSzoveg);
        }

        /// <summary>
        /// Stopperora ha, 2p eleri akk. clearel es megmutatja az eredmenyt
        /// </summary>
        private void Ora()
        {
            idozito = new Timer();
            idozito.Interval = 1000;
            idozito.Tick += (object o, EventArgs e) =>
            {
                mp++;
                if (mp >= 60)
                {
                    mp = 0;
                    PercNoveles();
                }
                if (p == 2)
                {
                    idozito.Stop();
                    this.form.Controls.Clear();
                    VegeDisplay();
                }
                FrissitIdoLabel();
            };
            idozito.Start();
        }

        /// <summary>
        /// frissiti az idot
        /// </summary>
        private void FrissitIdoLabel()
        {
            LIdo.Text = $"{PercFormat(p)}:{mp:D2}";
        }

        /// <summary>
        /// percnoveles
        /// </summary>
        private void PercNoveles()
        {
            p++;
        }

        /// <summary>
        /// percformazas
        /// </summary>
        /// <param name="perc">adott perc(1v2 itt)</param>
        /// <returns></returns>
        private string PercFormat(int perc)
        {
            return perc.ToString("D2");
        }


        /// <summary>
        /// Megallitogomb
        /// </summary>
        private void NavGen()
        {
            BMegallit = new Button();
            BMegallit.Tag = "megallit";
            BMegallit.Text = "Megállít";
            BMegallit.Click += MegallitMenu;
            BMegallit.BackColor = Color.DarkGray;
            BMegallit.ForeColor = Color.Red;
        }

        /// <summary>
        /// Megallitas menuje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MegallitMenu(object sender, EventArgs e)
        {
            LIdo.Hide();
            BMegallit.Hide();
            idozito.Stop();
            Fmain.Hide();

            BFolytat = new Button();
            BVissza = new Button();
            BKIlep = new Button();

            BFolytat.Text = "Folytatás";
            BFolytat.Click += Folytatas;
            BFolytat.Height = 50;
            BFolytat.Width = 300;
            BFolytat.BackColor = Color.DarkGray;
            BFolytat.ForeColor = Color.Red;

            BVissza.Text = "Vissza";
            BVissza.Click += VisszaMenu;
            BVissza.Height = 50;
            BVissza.Width = 300;
            BVissza.BackColor = Color.Cyan;
            BVissza.BackColor = Color.DarkGray;
            BVissza.ForeColor = Color.Red;

            BKIlep.Text = "Játék bezésása";
            BKIlep.Height = 50;
            BKIlep.Width = 300;
            BKIlep.Click += Kilep;
            BKIlep.BackColor = Color.Cyan;
            BKIlep.BackColor = Color.DarkGray;
            BKIlep.ForeColor = Color.Red;

            int hely = 10;
            int felosMargin = this.form.Height / 2 - 100;

            BFolytat.Top = felosMargin;
            BVissza.Top = BFolytat.Bottom + hely;
            BKIlep.Top = BVissza.Bottom + hely;

            int kozep = (this.form.ClientSize.Width - BFolytat.Width) / 2;

            BFolytat.Left = kozep;
            BVissza.Left = kozep;
            BKIlep.Left = kozep;

            this.form.Controls.Add(BFolytat);
            this.form.Controls.Add(BVissza);
            this.form.Controls.Add(BKIlep);
        }

        /// <summary>
        /// folytatasgomb hatasai az alalmazasra
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void Folytatas(object o, EventArgs e)
        {
            BFolytat.Hide();
            BKIlep.Hide();
            BVissza.Hide();
            LIdo.Show();
            BMegallit.Show();
            idozito.Start();
            Fmain.Show();
        }


        /// <summary>
        /// Vissza gomb hatasa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VisszaMenu(object sender, EventArgs e)
        {
            this.form.BackgroundImage = new Bitmap("hatter.jpg");
            this.form.BackgroundImageLayout = ImageLayout.Stretch;
            form.Controls.Clear();
            form.Form1_Load(sender, e);
        }


        /// <summary>
        /// bezearja a programot
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void Kilep(object o, EventArgs e)
        {
            this.form.Close();
        }

        /// <summary>
        /// nagy gomb kiirja az eltalalt,elrontott es osszes klikket
        /// </summary>
        private void VegeDisplay()
        { 
            this.form.Controls.Clear();
            Button b = new Button();
            b.Height = this.form.Height;
            b.Width = this.form.Width;
            b.Text = $"Jók száma: {jok}\nHibák száma: {rossz}\n Összes kattintás: {jok+rossz}";
            b.Font = new Font("Arial", 30);
            b.Click += MenuGenVegeGombhoz;
            this.form.Controls.Add(b);
        }

        /// <summary>
        /// Menugenhez van mert lusta vok. atirni az eredetit
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void MenuGenVegeGombhoz(object o ,EventArgs e)
        {
            this.form.Controls.Clear();
            menu.MenuGen();
        }
    }
}
