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
        public static double sum = 0;
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

            DataTable dt2 = new DataTable();
            int columns2 = 1;
            int lines2 = lines;
            for (int i = 0; i < columns2; i++)
                dt2.Columns.Add();
            for (int j = 0; j < lines2; j++)
                dt2.Rows.Add();
            dataGridView2.DataSource = dt2;
            for (int i = 0; i < columns2; i++)
                dataGridView2.Columns[i].Width = 50;
            for (int i = 0; i < lines2; i++)
                dataGridView2.Rows[i].Height = 50;
            dataGridView2.Width = 50 * columns2 + 5;
            dataGridView2.Height = 50 * lines2 + 5;
            if(columns!=lines2)
            MessageBox.Show("Кількість стовпців першої матриці не дорівнює кількості рядків другої! Множення матриць неможливе!", "Помилка!!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int columns = Convert.ToInt32(textBox2.Text);
            int lines = Convert.ToInt32(textBox1.Text);
            int columns2 = 1;
            int lines2 = columns;
            double[,] matrix1 = new double[lines, columns];
            double[,] matrix2 = new double[lines2, columns2];
            double[,] result_Matrix = new double[lines, columns2];
            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix1[i, j] = Convert.ToDouble(dataGridView1[j, i].Value);
                }
            }
            matrix1 = Obern(matrix1,columns);
            if (matrix1.Length == 0)
                MessageBox.Show("oops");
            else {
                for (int i = 0; i < lines2; i++) {
                    for (int j = 0; j < columns2; j++) {
                        matrix2[i, j] = Convert.ToDouble(dataGridView2[j, i].Value);
                    }
                }
                for (int i = 0; i < lines; i++) {
                    for (int j = 0; j < columns2; j++) {
                        double n = 0;
                        for (int k = 0; k < columns; k++)
                            n = n + matrix1[i, k] * matrix2[k, j];
                        result_Matrix[i, j] = n;
                    }
                }
                DataTable dt = new DataTable();
                for (int i = 0; i < columns2; i++)
                    dt.Columns.Add();
                for (int j = 0; j < lines; j++)
                    dt.Rows.Add();
                dataGridView3.DataSource = dt;
                for (int i = 0; i < columns2; i++)
                    dataGridView3.Columns[i].Width = 50;
                for (int i = 0; i < lines; i++)
                    dataGridView3.Rows[i].Height = 50;
                dataGridView3.Width = 50 * columns2 + 5;
                dataGridView3.Height = 50 * lines + 5;
                for (int i = 0; i < lines; i++)
                    for (int j = 0; j < columns2; j++) {
                        dataGridView3[j, i].Value = result_Matrix[i, j];
                    }
            }
        }
        public static double[,] minor(double[,] mas, int ryad, int stolb, int kin) {
            double[,] massiv = new double[kin-1, kin-1];
            for(int i = 0; i < kin; i++) {
                for(int j = 0; j < kin; j++) {
                    if(i != ryad && j != stolb) {
                        int line = i, column = j;
                        if (i > ryad)
                            line = i - 1;
                        if (j > stolb)
                            column = j - 1;
                        massiv[line, column] = mas[i, j];
                    }
                }
            }
            return massiv;
        }

        static private void Determinant(double[,] matrix, bool[,] matrixBool, int minBound, int maxBound, double tempMult, List<int> indexes) {
            if (minBound == maxBound) {
                int i = 0;
                for(int j = 0; j <= maxBound; j++) {
                    if (!indexes.Contains(j))
                        i = j;
                }
                indexes.Add(i);
                tempMult *= matrix[minBound, i]* Math.Pow(-1, Inverses(indexes));
                sum += tempMult;
                indexes.RemoveAt(maxBound);
                for (int i2 = 0; i2 <= maxBound; i2++)
                    matrixBool[maxBound, i2] = false;

            }
            else {
                for(int i = 0; i <= maxBound; i++) {
                    if (!matrixBool[minBound,i] && !indexes.Contains(i)) {
                        matrixBool[minBound,i] = true;
                        indexes.Add(i);
                        Determinant(matrix, matrixBool, minBound+1,maxBound,tempMult*matrix[minBound,i],indexes);
                        indexes.RemoveAt(minBound);
                        for (int i2 = 0; i2 <= maxBound; i2++)
                                matrixBool[minBound, i2] = false;
                    }
                }
            }
        }

        static private int Inverses(List<int> indexes) { 
            int inv = 0;
            for(int i = 0; i < indexes.Count; i++) {
                for(int j = i+1; j < indexes.Count; j++) {
                    if (indexes[i]>indexes[j])
                        inv++;
                }
            }
            return inv;
        }
        private static double[,] Obern(double[,] mas1, int kin) {
            double[,] massiv2 = new double[kin, kin];
            for (int i = 0; i < kin; i++)
                for (int j = 0; j < kin; j++)
                    massiv2[i, j] = mas1[i, j];
            double[,] massiv1 = new double[kin, kin];
            Determinant(mas1, new bool[kin, kin], 0, kin-1, 1, new List<int>());
            double determ = sum;
            sum = 0;
            for (int i = 0; i < kin; i++) {
                for (int j = 0; j < kin; j++) {
                    for (int i1 = 0; i1 < kin; i1++)
                        for (int j1 = 0; j1 < kin; j1++)
                            mas1[i1, j1] = massiv2[i1, j1];
                    Determinant(minor(mas1,j,i,kin),new bool[kin,kin], 0, kin-2, 1, new List<int>());
                    massiv1[i, j] = Math.Round(Math.Pow(-1, i + j) * sum / determ, 4);
                    sum = 0;
                }
            }
            return massiv1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void label3_Click(object sender, EventArgs e) {

        }
    }
}
