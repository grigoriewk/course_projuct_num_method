using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace kursach_ch_m_2_kurs
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        
        public double a;
        public double b;
        public double h;
        public double y;
        List <double> x_List;
        List <double> y_List_Adams;
        List <double> y_List_RK;
        List <double> temp;

        private double func(double x, double y)
        {
            return 2 * Math.Pow(x, 2) + x * y + 3 * Math.Pow(y, 2);
            //return 5 * x + 3 * Math.Cos(y + 2.6);
        }

        private void Adams(double x, double y)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            int N = Convert.ToInt32(Math.Abs(b - a) / h);
            x_List = new List<double>()
            { x };
            y_List_Adams = new List<double>()
            { y };
            temp = new List<double>()
            { h*func(x,y) };

            for (int i = 1; i < 4; ++i)
            {
                double k1 = h * func(x, y);
                double k2 = h * func(x + h / 2, y + k1 / 2);
                double k3 = h * func(x + h / 2, y + k2 / 2);
                double k4 = h * func(x + h, y + k3);
                y += (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                x += h;
                x_List.Add(x);
                y_List_Adams.Add(y);
                temp.Add(h * func(x, y));
            }

            for (int i = 3; i < N; ++i)
            {
                y += differences(i);
                x += h;
                x_List.Add(x);
                y_List_Adams.Add(y);
                temp.Add(h * func(x, y));
            }
            sw.Stop();
            label9.Text += (sw.Elapsed.TotalMilliseconds).ToString();
        }

        double differences(int N)
        {
            double temp1 = temp[N] - temp[N - 1];
            double temp2 = temp[N] - 2 * temp[N - 1] + temp[N - 2];
            double temp3 = temp[N] - 3 * temp[N - 1] + 3 * temp[N-2] - temp[N - 3];
            return temp[N] + temp1 / 2 - temp2 / 12 - temp3 / 24;
        }


        private void Runge_Kutt(double x, double y)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            int N = Convert.ToInt32(Math.Abs(b - a) / h);
            x_List = new List<double>()
            { x };
            y_List_RK = new List<double>()
            { y };
            for (int i = 0; i < N; ++i)
            {
                double k1 = h * func(x, y);
                double k2 = h * func(x + h / 2, y + k1 / 2);
                double k3 = h * func(x + h / 2, y + k2 / 2);
                double k4 = h * func(x + h, y + k3);
                y += (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                x = x + h;
                y_List_RK.Add(y);
                x_List.Add(x);
            }
            sw.Stop(); label10.Text += (sw.Elapsed.TotalMilliseconds).ToString();
        }


        private void button1_Click(object sender, EventArgs e) 
        {
            dataGridView1.Rows.Clear();
            label9.Text = "Время работы алгоритма (мсек) :\r\n";
            try
            {
                a = Convert.ToDouble(textBox1.Text);
                b = Convert.ToDouble(textBox2.Text);
                h = Convert.ToDouble(textBox4.Text);
                y = Convert.ToDouble(textBox3.Text);
                Adams(a, y);
            } catch { MessageBox.Show("ОШИБКА! Проверьте правильность введённых данных!", "Внимание!"); }
            for (int i = 0; i < x_List.Count; ++i)
                dataGridView1.Rows.Add(Math.Round(x_List[i], 5), Math.Round(y_List_Adams[i], 5));
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            label10.Text = "Время работы алгоритма (мсек) :\r\n";
            try
            {
                a = Convert.ToDouble(textBox8.Text);
                b = Convert.ToDouble(textBox7.Text);
                h = Convert.ToDouble(textBox5.Text);
                y = Convert.ToDouble(textBox6.Text);
                Runge_Kutt(a, y);
                
            }
            catch { MessageBox.Show("ОШИБКА! Проверьте правильность введённых данных!", "Внимание!"); }
            for (int i = 0; i < x_List.Count; ++i)
                dataGridView2.Rows.Add(Math.Round(x_List[i], 5), Math.Round(y_List_RK[i], 5));
        }
    }
}