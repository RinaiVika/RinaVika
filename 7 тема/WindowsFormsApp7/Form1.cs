using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ClassLibrary7;

namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        private List<Participant> participants = new List<Participant>();

        // Конструктор формы
        public Form1()
        {
            InitializeComponent();

            MaximumSize = new Size(571, 462);
            MinimumSize = new Size(571, 462);
        }

        // Кнопка Добавить
        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                string name = surnameTextBox.Text;
                double distance = double.Parse(distanceTextBox.Text);
                double time = double.Parse(timeTextBox.Text);

                // создаем нового участника и добавляем его в список
                var participant = new Participant(name, distance, time);
                participants.Add(participant);

                UpdateTable();
                UpdateChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка ввода данных: " + ex.Message);
            }
            surnameTextBox.Text = "";
            distanceTextBox.Text = "";
            timeTextBox.Text = "";
        }

        // Кнопка Сохранить
        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Text Files (*.txt)|*.txt",
                    DefaultExt = "txt",
                    AddExtension = true
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Participant.SaveToFile(participants, saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Кнопка Загрузить
        private void loadButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Text Files (*.txt)|*.txt",
                    DefaultExt = "txt",
                    AddExtension = true
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    participants = Participant.LoadFromFile(openFileDialog.FileName);
                    UpdateTable();
                    UpdateChart();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Метод для обновления данных в таблице
        private void UpdateTable()
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add("Name", "Фамилия");
            dataGridView.Columns.Add("Distance", "Дистанция, м");
            dataGridView.Columns.Add("Time", "Время, с");
            dataGridView.Columns.Add("Speed", "Скорость, м/с");

            // заполняем таблицу данными участников
            foreach (var participant in participants)
            {
                dataGridView.Rows.Add(participant.Name, participant.Distance, participant.Time, participant.Speed.ToString("F2"));
            }
        }

        // Метод для обновления данных на диаграмме
        private void UpdateChart()
        {
            // очищаем существующие серии данных
            chart1.Series.Clear();

            // создаем новую серию данных для диаграммы
            var series = new Series("Скорость, м/с")
            {
                ChartType = SeriesChartType.Column
            };

            // заполняем серию данными участников
            foreach (var participant in participants)
            {
                series.Points.AddXY(participant.Name, participant.Speed);
            }

            // добавляем серию к диаграмме
            chart1.Series.Add(series);
        }

        private void surnameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void distanceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != ',' || distanceTextBox.Text.Contains(",")))
            {
                e.Handled = true;
            }
        }

        private void timeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != ',' || timeTextBox.Text.Contains(",")))
            {
                e.Handled = true;
            }
        }




        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {

        }
    }
}
