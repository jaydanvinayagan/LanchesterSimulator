using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LanchesterSimulator
{
    public partial class Form1 : Form
    {
        public List<Double> force_a_series = new List<Double>();
        public List<Double> force_b_series = new List<Double>();
        public List<Double> total_losses = new List<Double>();

        public double force_asize;
        public double force_bsize;

        public Form1()
        {
            InitializeComponent();
           
        }

        private void runProgramBtn_Click(object sender, EventArgs e)
        {
            
           
             double a_kill_rate;
             double b_kill_rate;
            outputTextBox.Text = "";

            force_asize = Convert.ToDouble(forceABox1.Text);
            force_bsize = Convert.ToDouble(forceBBox1.Text);

            a_kill_rate = Convert.ToDouble(forceaBox2.Text);
            b_kill_rate = Convert.ToDouble(forcebkBox2.Text);

            force_a_series.Add(force_asize);
            force_b_series.Add(force_bsize);
            total_losses.Add(0);

            simulate(force_asize,a_kill_rate,force_bsize,b_kill_rate,0,0);

            outputChart.Series["Force A"].Points.DataBindY(force_a_series);
            outputChart.Series["Force B"].Points.DataBindY(force_b_series);
            outputChart.Series["Total Casualties"].Points.DataBindY(total_losses);

            for(int i = 0; i < force_a_series.Count(); i++)
            {

                outputTextBox.Text += string.Format("{0,5:###.00} {1,5:###.00} \n", force_a_series[i].ToString(), force_b_series[i].ToString());
            }

            force_a_series.Clear();
            force_b_series.Clear();
            total_losses.Clear();
        }
       
        public void simulate(double f_sizea, double a_kill, double f_sizeb, double b_kill, double enda, double endb)
        {

            double z;
            

            if (!(f_sizea < 1.0 || f_sizeb < 1.0))
            {
                enda = f_sizea - (a_kill * f_sizeb);
                endb = f_sizeb - (b_kill * f_sizea);

                enda = Math.Round(enda, 4);
                endb = Math.Round(endb, 4);

                z = ((force_asize - enda) + (force_bsize - endb));

                force_a_series.Add(enda);
                force_b_series.Add(endb);

                total_losses.Add(z);
                
       
                simulate(enda, a_kill, endb, b_kill, enda, endb);
            }
           



        }
    }
}
