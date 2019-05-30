using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursach
{
    public partial class Form1 : Form
    {


        OpenFileDialog openFileDialog;
        SaveFileDialog saveFileDialog;

        List<Parking> parking = new List<Parking>();

        public Form1()
        {
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            InitializeComponent();
        }
        bool InputIsPositiveDigit(string st)
        {
            bool check = false;
            int temp;
            if (int.TryParse(st, out temp) == true && temp > 0)
                check = true;

            return check;
        }
        void UpdateT()
        {

            dataGridView1.Rows.Clear();
            foreach (var auto in parking)
            {
                dataGridView1.Rows.Add(auto.ParkingSpaceNum, auto.CarOwner, auto.CarModel, auto.ParkingMark, auto.PaymentMark);
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (InputIsPositiveDigit(textBox2.Text))
            {
                parking.Add(new Parking()
                {
                    ParkingSpaceNum = Convert.ToInt32(textBox2.Text),
                    CarOwner = textBox1.Text,
                    CarModel = textBox3.Text,
                    ParkingMark = comboBox1.Text,
                    PaymentMark = comboBox2.Text
                });
            }
            else
            {
                MessageBox.Show("Номер парко-места должен быть положительным числом!");
            }
            

            UpdateT();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0 && this.dataGridView1.SelectedRows[0].Index != this.dataGridView1.Rows.Count - 1)
            {
                int row = this.dataGridView1.SelectedRows[0].Index;
                this.dataGridView1.Rows.RemoveAt(row);
                parking.RemoveAt(row);
            }
            UpdateT();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            if (this.dataGridView1.SelectedRows.Count > 0 && this.dataGridView1.SelectedRows[0].Index != this.dataGridView1.Rows.Count - 1)
            {

                int index = this.dataGridView1.SelectedRows[0].Index;
                if (InputIsPositiveDigit(textBox2.Text))
                {
                    parking[index].ParkingSpaceNum = Convert.ToInt32(textBox2.Text);
                    parking[index].CarOwner = textBox1.Text;
                    parking[index].CarModel = textBox3.Text;
                    parking[index].ParkingMark = comboBox1.Text;
                    parking[index].PaymentMark = comboBox2.Text;
                }
                else
                {
                    MessageBox.Show("Номер парко-места должен быть положительным числом!");
                }
               
            }
            UpdateT();
        }



        private void ОчиститьВсеЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            parking.Clear();
            UpdateT();
        }

        private void СведеньяОПустыхАвToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flag = false;
            dataGridView1.Rows.Clear();
            foreach(var auto in parking)
            {
                if(auto.ParkingMark == "Нет")
                {
                    dataGridView1.Rows.Add(auto.ParkingSpaceNum, auto.CarOwner, auto.CarModel, auto.ParkingMark, auto.PaymentMark);
                    flag = true;
                }
            }
            if(flag == false)
            {
                MessageBox.Show("Не удалось найти записи!");
            }
        }

        private void СведеньяОНеОплаченыхМесятахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flag = false;
            dataGridView1.Rows.Clear();
            foreach (var auto in parking)
            {
                if (auto.PaymentMark == "Нет")
                {
                    dataGridView1.Rows.Add(auto.ParkingSpaceNum, auto.CarOwner, auto.CarModel, auto.ParkingMark, auto.PaymentMark);
                    flag = true;
                }
            }
            if (flag == false)
            {
                MessageBox.Show("Не удалось найти записи!");
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            string name = textBox4.Text;
          

            parking = parking.Where(x => x.CarOwner != name).ToList();
            UpdateT();
            MessageBox.Show("Записи удалены!");
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0 && this.dataGridView1.SelectedRows[0].Index != this.dataGridView1.Rows.Count - 1)
            {
                int row = this.dataGridView1.SelectedRows[0].Index;
                if (InputIsPositiveDigit(textBox2.Text))
                {
                    parking.Insert(row + 1, new Parking()
                    {
                        ParkingSpaceNum = Convert.ToInt32(textBox2.Text),
                        CarOwner = textBox1.Text,
                        CarModel = textBox3.Text,
                        ParkingMark = comboBox1.Text,
                        PaymentMark = comboBox2.Text
                    });
                }
                else
                {
                    MessageBox.Show("Номер парко-места должен быть положительным числом!");
                }
              
            }
            UpdateT();
            
        }

        private void ОткрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = openFileDialog.FileName;

            FileStream fsR = new FileStream(filename, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fsR, Encoding.UTF8);


            int length = br.ReadInt32();


            for (int i = 0; i < length; i++)
            {
                parking.Add(Parking.Read(br));
            }

            dataGridView1.Rows.Clear();

            UpdateT();

            // Закрываем потоки
            br.Close();
            fsR.Close();
        }

        private void СохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog.FileName;



            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8);

            int length = parking.Count;
            bw.Write(length);

            foreach (var auto in parking)
                auto.Write(bw);


            
            bw.Close();
            fs.Close();
            MessageBox.Show("Файл сохранен");
        }

        private void ПоказатьВсеЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateT();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
