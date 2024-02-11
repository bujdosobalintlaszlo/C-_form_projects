using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stooper_effect
{
    /// <summary>
    /// Menu elemeinek generalasa
    /// </summary>
    public class Menu
    {
        //form1 osztaly meghivasa
        private Form1 form;
        
        //konstruktor
        public Menu(Form1 form)
        {
            this.form = form;
        }

        /// <summary>
        /// kigeneralja az egesz menut
        /// </summary>
        public void MenuGen()
        {
            FlowLayoutPanel FgombokHelye = FlowPanelGen();
            FgombokHelye.FlowDirection = FlowDirection.TopDown;
            FgombokHelye.WrapContents = false;
            FgombokHelye.BackColor = Color.Transparent;
            FgombokHelye.Anchor = AnchorStyles.None;

            Label LFelirat = new Label();
            LFelirat.Text = "Stroop Hatás";
            LFelirat.Font = new Font("Arial", 36);
            LFelirat.ForeColor = Color.Red;
            LFelirat.AutoSize = true;
            LFelirat.Dock = DockStyle.None;

            Button Bstart = GombGen("Indítás", 50, 300, "inditas_gomb");
            Bstart.Click += Start;
            Bstart.BackColor = Color.DarkGray;
            Bstart.ForeColor = Color.Red;
            Bstart.Font = new Font("Arial", 12);
            Button Binfo = GombGen("Információk", 50, 300, "info");
            Binfo.Click += Info;
            Binfo.BackColor = Color.DarkGray;
            Binfo.ForeColor = Color.Red;
            Binfo.Font = new Font("Arial", 12);
            Button Bezaras = GombGen("Kilépés", 50, 300, "kilep");
            Bezaras.Click += form.Bezar;
            Bezaras.BackColor = Color.DarkGray;
            Bezaras.ForeColor = Color.Red;
            Bezaras.Font = new Font("Arial", 12);
            FgombokHelye.Controls.Add(LFelirat);
            FgombokHelye.Controls.Add(Bstart);
            FgombokHelye.Controls.Add(Binfo);
            FgombokHelye.Controls.Add(Bezaras);

            FgombokHelye.Size = new Size(
                Math.Max(LFelirat.Width, Math.Max(Bstart.Width, Math.Max(Binfo.Width, Bezaras.Width))),
                LFelirat.Height + Bstart.Height + Binfo.Height + Bezaras.Height
            );

            LFelirat.Location = new Point((FgombokHelye.Width - LFelirat.Width) / 2, 0);
            FgombokHelye.Location = new Point((form.ClientSize.Width - FgombokHelye.Width) / 2, (form.ClientSize.Height - FgombokHelye.Height) / 2);

            form.Controls.Add(FgombokHelye);
        }

        /// <summary>
        /// az info resz kigeneralasa + visszagomb
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        public void Info(object o, EventArgs e)
        {
            this.form.BackgroundImage = null;

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.AutoSize = true;
            Label Lcim = new Label();
            Lcim.Text = "\nMi az a Stroop hatás?";
            Lcim.Font = new Font("Arial", 25);
            Lcim.ForeColor = Color.Red;
            Lcim.AutoSize = true;
            Label LdialoguElso = new Label();
            LdialoguElso.Text = "Ezt a jelenséget Stroop-hatásnak nevezi a pszichológia, és az erre épülő teszteket mindennaposan használják pszichológiai és pszichiátriai vizsgálatokban, kutatásokban. Névadója az amerikai John Ridley Stroop, aki 1935-ben publikált PhD-dolgozatában fejtett ki a fentiekhez hasonló kísérleteket. Nem ő volt az első, aki felismerte a jelenséget: a német Erich Rudolf Jaensch már 1929-ben publikált a témában németül, és a modern pszichológia egyik atyja, a szintén német Wilhelm Wundt is érintette a kérdést a 19. században. Stroop viszont olyan egyszerűen és hatásosan foglalta össze tapasztalatait, hogy munkája a tudományterület egyik legtöbbet idézett cikke lett, a hatás pedig összeforrt a nevével (annak ellenére, hogy pár év múlva elhagyta a kutatói pályát, és teljesen a tanításra váltott, egyre jobban elmélyülve bibliai tanulmányokban).\n\n" +
                "A Stroop-hatás általánosságban azt jelenti, hogy inkongruens (össze nem illő, meg nem egyező) ingerek esetén a reakcióidő megnő, nagyobb lesz, mint semleges vagy kongruens ingerek esetén. A cikk elején leírt három színes sorból az első sor kongruens ingereket példáz (a színek nevei jelentésükkel megegyező színűek), a harmadik inkongruens ingereket (a színek nevei mindig más színnel szerepelnek, mint a szavak jelentése), a második pedig semleges ingereket (a szavak jelentése és a szavak színe között nincs kapcsolat). A reakcióidő pedig ez esetben egy szín érzékelése és a nevének kimondása közötti időt jelenti. Ez az idő a harmadik, inkongruens esetben érezhetően nagyobb, mint az első, kongruens sornál. Ha belezavarodunk a színek kimondásába, az annak a jele, hogy a kétféle inger feldolgozása erős konfliktusban van egymással – ezt Stroop-interferenciának szokták nevezni.";
            LdialoguElso.Font = new Font("Arial", 16);
            LdialoguElso.ForeColor = Color.White;
            LdialoguElso.AutoSize = true;


            Label LcimMasodik = new Label();
            LcimMasodik.Text = "\n\n    Játékleírás:\n";
            LcimMasodik.Font = new Font("Arial", 25);
            LcimMasodik.ForeColor = Color.Red;
            LcimMasodik.AutoSize = true;
            Label LdialoguMasodik = new Label();
            LdialoguMasodik.Text = "A játékban 5 szín van: fekete, kék, zöld, piros, sárga. A képernyőn 5 gomb van ezekben a színekben, illetve a képernyő közepén megjelenik egy szó ami a színekneve lehet. Ez a kiírás egy színt is kap. Fölötte megjelenik az is, hogy a szöveg színét vagy pedig magát a leírt színt kell néznie a felhasználónak. Ha az van írva hogy szöveg akkor azt a színt kell figyelembevenni amit a szó ír le, viszont ha szín van akkor a szöveg színét kell figyelembevennije a játékosnak.\n\n";
            LdialoguMasodik.Font = new Font("Arial", 16);
            LdialoguMasodik.ForeColor = Color.White;
            LdialoguMasodik.AutoSize= true;

            Button backBtn = GombGen("Vissza", 30, 60, "vissza");
            backBtn.Font = new Font("Arial", 8);
            backBtn.Click += VisszaMenu;
            backBtn.BackColor = Color.DarkGray;
            backBtn.ForeColor = Color.Red;

            flowLayoutPanel.Controls.Add(backBtn);
            flowLayoutPanel.Controls.Add(Lcim);
            flowLayoutPanel.Controls.Add(LdialoguElso);
            flowLayoutPanel.Controls.Add(LcimMasodik);
            flowLayoutPanel.Controls.Add(LdialoguMasodik);

            form.Controls.Clear();
            form.Controls.Add(flowLayoutPanel);
        }

        /// <summary>
        /// Visszagom az infohoz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VisszaMenu(object sender, EventArgs e)
        {
            this.form.BackgroundImage = new Bitmap("hatter.jpg");
            this.form.BackgroundImageLayout = ImageLayout.Stretch;
            form.Controls.Clear();
            MenuGen();
        }

        /// <summary>
        /// flowpanel generalas
        /// </summary>
        /// <returns>flowpanel</returns>
        private FlowLayoutPanel FlowPanelGen()
        {
            FlowLayoutPanel F = new FlowLayoutPanel();
            F.AutoSize = true;
            return F;
        }

        /// <summary>
        /// gombgeneralas
        /// </summary>
        /// <param name="text">gombba irt szoveg</param>
        /// <param name="height">magassag</param>
        /// <param name="width">szelesseg</param>
        /// <param name="tag">tag-je</param>
        /// <returns>gomb</returns>
        private Button GombGen(string text, int height, int width, string tag)
        {
            Button b = new Button();
            b.Tag = tag;
            b.Height = height;
            b.Width = width;
            b.Text = text;
            return b;
        }

        /// <summary>
        /// startolja a jatekot es peldanyositja a jatek osztalyt.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void Start(object o, EventArgs e)
        {
            this.form.BackgroundImage = null;
            Jatek j = new Jatek(this.form);
            j.Megjelenit();
        }
    }
}
