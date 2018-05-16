using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace CSharpAsyncDemo
{
    public partial class Form1 : Form
    {
        //public delegate 
        Random r;
        public Form1()
        {
            InitializeComponent();
            r = new Random();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(r.Next(300));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThreadStart ts = new ThreadStart(DoTimeConsumingWork);
            ts += delegate { MessageBox.Show("Busy work done! ;) Message by a callback function :O"); };
            Thread t = new Thread(ts);
            t.Start();
        }

        private void DoTimeConsumingWork()
        {
            progressBar1.Invoke(new Action(delegate { progressBar1.Value = 0; }));
            for (int i = 1; i <= 5; i++)
            {
                progressBar1.Invoke(new Action(delegate { progressBar1.Value += 20; }));
                Thread.Sleep(1000);
            }
            listBox2.Invoke(new Action(delegate { listBox2.Items.Add("Busy work done ;)"); }));
        }
    }
}
