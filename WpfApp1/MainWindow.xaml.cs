using System;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Win32;
using LibMas;
using Lib_8;

namespace Pract1_Var8
{
    public partial class MainWindow : Window
    {
        public class Cell
        {
            public int Index { get; set; }
            public int Value { get; set; }
        }

        private int[] mas = null;
        private ObservableCollection<Cell> inputCollection = new ObservableCollection<Cell>();
        private ObservableCollection<Cell> resultCollection = new ObservableCollection<Cell>();

        public MainWindow()
        {
            InitializeComponent();
            dgInput.ItemsSource = inputCollection;
            dgResult.ItemsSource = resultCollection;
        }

        private void CreateArray_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbN.Text, out int n) || n <= 0)
            {
                MessageBox.Show("Введите корректный размер массива (n > 0)", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            mas = new int[n];
            inputCollection.Clear();
            resultCollection.Clear();

            for (int i = 0; i < n; i++)
                inputCollection.Add(new Cell { Index = i, Value = 0 });
        }

        private void SyncFromGrid()
        {
            if (mas == null || inputCollection.Count != mas.Length) return;
            for (int i = 0; i < mas.Length; i++)
                mas[i] = inputCollection[i].Value;
        }

        private void FillRandom_Click(object sender, RoutedEventArgs e)
        {
            if (mas == null)
            {
                MessageBox.Show("Сначала создайте массив", "Внимание");
                return;
            }

            ArrayUtils.InitMas(mas, -30, 31);   

            inputCollection.Clear();
            for (int i = 0; i < mas.Length; i++)
                inputCollection.Add(new Cell { Index = i, Value = mas[i] });

            resultCollection.Clear();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            if (mas == null) return;
            ArrayUtils.ClearMas(mas);

            inputCollection.Clear();
            for (int i = 0; i < mas.Length; i++)
                inputCollection.Add(new Cell { Index = i, Value = 0 });

            resultCollection.Clear();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (mas == null)
            {
                MessageBox.Show("Сначала создайте и заполните массив", "Внимание");
                return;
            }

            SyncFromGrid();   

            int[] result = Variant7.ProcessMas(mas);

            resultCollection.Clear();
            for (int i = 0; i < result.Length; i++)
                resultCollection.Add(new Cell { Index = i, Value = result[i] });
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (mas == null)
            {
                MessageBox.Show("Нет данных для сохранения");
                return;
            }

            SyncFromGrid();

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                DefaultExt = "txt",
                FileName = "mas.txt"
            };

            if (sfd.ShowDialog() == true)
            {
                try
                {
                    ArrayUtils.SaveMas(mas, sfd.FileName);
                    MessageBox.Show("Массив успешно сохранён", "Сохранение");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка сохранения: " + ex.Message);
                }
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };

            if (ofd.ShowDialog() == true)
            {
                try
                {
                    mas = ArrayUtils.LoadMas(ofd.FileName);
                    tbN.Text = mas.Length.ToString();

                    inputCollection.Clear();
                    resultCollection.Clear();

                    for (int i = 0; i < mas.Length; i++)
                        inputCollection.Add(new Cell { Index = i, Value = mas[i] });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки: " + ex.Message);
                }
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Практическая работа №1\n" +
                "Вариант 7\n\n" +
                "Задание:\n" +
                "Ввести n целых чисел. Вычислить для чисел < 0 функцию x².\n" +
                "Результат обработки каждого числа вывести на экран.\n\n" +
                "Разработчик: Гришин Дмитрий\n" +
                "Группа: ИСП-31\n" ,
                "О программе",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}