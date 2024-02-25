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
    class Clock
    {
        private Timer idozito;
        private int mp;
        private int p;
        private Label LIdo;
        public Clock(Label LIdo)
        {
            this.LIdo = LIdo;
        }
        public void StartTimer()
        {
            idozito = new Timer();
            idozito.Interval = 1000;
            idozito.Tick += (object o, EventArgs e) => {
                mp++;
                if (mp >= 60)
                {
                    mp = 0;
                    PercNoveles();
                }
                if (p >= 59)
                {
                    StopTimer();
                }
                FrissitIdoLabel();
            };
            idozito.Start();
        }
        private void FrissitIdoLabel() => LIdo.Text = $"{PercFormat(p)}:{mp:D2}";
        private void PercNoveles() => p++;
        private string PercFormat(int perc) => perc.ToString("D2");
        public void StopTimer() => idozito.Stop();
    }
}
