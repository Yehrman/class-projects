using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void NewThread()
        {
          
              HttpClient http = new HttpClient();
            var ret = http.GetStringAsync("http://slowwly.robertomurray.co.uk/delay/11000/url/http://google.co.uk").Result.Length;
          MethodInvoker method = delegate
              {
                    textBox1.Text = ret.ToString();
              };
            textBox1.BeginInvoke(method);
        }
       private void button1_Click(object sender, EventArgs e)
        {
            //HttpClient http = new HttpClient();
            //var ret = http.GetStringAsync("http://slowwly.robertomurray.co.uk/delay/11000/url/http://google.co.uk").Result.Length;
          //  textBox1.Text = ret.ToString();
             Thread t = new Thread(NewThread);
            t.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           pictureBox1.Image= Image.FromFile("C: /Users/Rivka/Pictures/rochels birthday/DSCN1931.JPG");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      
        }
    }
}
