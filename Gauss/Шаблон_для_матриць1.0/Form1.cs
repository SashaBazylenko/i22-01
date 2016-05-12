using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Шаблон_для_матриць1._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            int columns = Convert.ToInt32(textBox2.Text);
            int lines  = Convert.ToInt32(textBox1.Text);
            for (int i = 0; i < columns; i++)
                dt.Columns.Add();
            for (int j = 0; j < lines; j++)
                    dt.Rows.Add();
            dataGridView1.DataSource = dt;
            for (int i = 0; i < columns; i++)
                dataGridView1.Columns[i].Width = 50;
            for (int i = 0; i < lines; i++)
                dataGridView1.Rows[i].Height = 50;
            dataGridView1.Width = 50 * columns + 5;
            dataGridView1.Height = 50 * lines + 5;
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int columns = Convert.ToInt32(textBox2.Text);
            int lines = Convert.ToInt32(textBox1.Text);
            double[,] matrix = new double[lines, columns];
            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = Convert.ToDouble(dataGridView1[j, i].Value);
                }
            }
            int ki = 0, kj = 0, w = 0, rjadok = 0;
            for (int i = 0; i < lines - 1; i++)
            {
                double n = matrix[ki, kj];
                if (i == 0 && n == 0)
                {
                    double[] qwer = new double[columns];
                    int f = 0;
                    for (int r = 0; r < lines; r++)
                    {
                        if (matrix[r, 0] != 0)
                        {
                            f = r;
                            break;
                        }
                    }
                    for (int r = 0; r < columns; r++)
                    {
                        qwer[r] = matrix[0, r];
                        matrix[0, r] = matrix[f, r];
                        matrix[f, r] = qwer[r];
                    }
                    n = matrix[ki, kj];
                }
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = matrix[i, j] / n;
                }
                int counter = 0;
                for (int q = w; q < lines - 1; q++)
                {
                    double n1 = matrix[++ki, kj] * (-1);
                    counter++;
                    for (int j = 0; j < columns; j++)
                    {
                        matrix[q + 1, j] = matrix[q + 1, j] + (matrix[rjadok, j] * n1);
                    }
                }
                rjadok++;
                w++;
                ki -= counter;
                ki++;
                kj++;
            }
            for (int i = lines - 1; i > 0; i--)
            {
                double n = matrix[ki, kj];
                if (n != 0)
                    for (int j = 0; j < columns; j++)
                    {
                        matrix[i, j] = matrix[i, j] / n;
                    }
                int counter = 0;
                for (int q = w; q > 0; q--)
                {
                    double n1 = matrix[--ki, kj] * (-1);
                    counter++;
                    for (int j = 0; j < columns; j++)
                    {
                        matrix[q - 1, j] = matrix[q - 1, j] + (matrix[rjadok, j] * n1);
                    }
                }
                rjadok--;
                w--;
                ki += counter;
                ki--;
                kj--;
            }
            double[] result = new double[lines];
            for (int i = 0; i < lines; i++)
                result[i] = matrix[i, columns - 1];
            DataTable dt = new DataTable();
            for (int i = 0; i < 2; i++)
                dt.Columns.Add();
            for (int j = 0; j < lines; j++)
                dt.Rows.Add();
            dataGridView2.DataSource = dt;
            for (int i = 0; i < 2; i++)
                dataGridView2.Columns[i].Width = 50;
            for (int i = 0; i < lines; i++)
                dataGridView2.Rows[i].Height = 50;
            dataGridView2.Width = 50 * 2 + 5;
            dataGridView2.Height = 50 * lines + 5;
            for (int i = 0; i < lines; i++)
            {
                dataGridView2[0, i].Value = "x[" + (i + 1) + "]";
                dataGridView2[1, i].Value = result[i];
            }
        }
    }
}
