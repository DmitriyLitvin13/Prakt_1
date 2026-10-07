using System;

namespace Lib_8
{

    public static class Variant7
    {

        public static int[] ProcessMas(int[] mas)
        {
            if (mas == null)
                throw new ArgumentNullException(nameof(mas));

            int[] result = new int[mas.Length];

            for (int i = 0; i < mas.Length; i++)
            {
                if (mas[i] < 0)
                    result[i] = mas[i] * mas[i];   // x в квадрате
                else
                    result[i] = mas[i];            // без изменений
            }

            return result;
        }


        public static int[] GetOnlySquaresOfNegatives(int[] mas)
        {
            if (mas == null) return new int[0];

            int count = 0;
            foreach (int x in mas)
                if (x < 0) count++;

            int[] result = new int[count];
            int j = 0;
            for (int i = 0; i < mas.Length; i++)
            {
                if (mas[i] < 0)
                    result[j++] = mas[i] * mas[i];
            }
            return result;
        }
    }
}