using System;
using System.Drawing;
using System.Windows.Forms;

namespace Stooper_effect
{
    /// <summary>
    /// Alap form, menu
    /// </summary>
    public partial class Form1 : Form
    {
        //Deklarálom az osztalyt amit itt is hasznalok
        private Menu MenuGen;
        //--

        //private Jatek JatekIndit;

        /// <summary>
        /// Maximalizalom az ablakot alapertelmezettent,beallitom a designelemeket
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            MenuGen = new Menu(this);
            //JatekIndit = new Jatek(this);
            this.Text = "Stroop hatás";
            Bitmap bitmap = new Bitmap("brain.png");
            Icon icon = ConvertImageToIcon(bitmap);
            this.Icon = icon;
            this.BackColor = Color.DimGray;
            this.BackgroundImage = new Bitmap("hatter.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        /// <summary>
        /// Beallitom bitmap segitsegevel az ikon-t.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        static Icon ConvertImageToIcon(Bitmap bitmap)
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

        /// <summary>
        /// Kigeneralom a menut a Menu osztaly egyik fuggvenyevel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Form1_Load(object sender, EventArgs e)
        {
            MenuGen.MenuGen();
        }

        /// <summary>
        /// egyszeruen bezarja az ablakot
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        public void Bezar(object o, EventArgs e)
        {
            this.Close();
        }
    }
}
