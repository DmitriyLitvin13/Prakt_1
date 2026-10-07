using System;
using System.IO;

namespace LibMas
{

    public static class ArrayUtils
    {

        public static void InitMas(int[] mas, int minValue = -50, int maxValue = 51)
        {
            if (mas == null || mas.Length == 0) return;
            Random rnd = new Random();
            for (int i = 0; i < mas.Length; i++)
                mas[i] = rnd.Next(minValue, maxValue);
        }

        public static void ClearMas(int[] mas)
        {
            if (mas == null) return;
            for (int i = 0; i < mas.Length; i++)
                mas[i] = 0;
        }


        public static void SaveMas(int[] mas, string fileName)
        {
            if (mas == null) throw new ArgumentNullException(nameof(mas));
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine(mas.Length);
                foreach (int x in mas)
                    sw.WriteLine(x);
            }
        }

        public static int[] LoadMas(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                int n = int.Parse(sr.ReadLine());
                int[] mas = new int[n];
                for (int i = 0; i < n; i++)
                    mas[i] = int.Parse(sr.ReadLine());
                return mas;
            }
        }
    }
}