using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace GAMES
{
    public partial class Form3 : Form
    {
        Point EmptyPoint;
        ArrayList images = new ArrayList();
        public Form3()

        {   //задаем начальную позицию пустой ячейки
            EmptyPoint.X = 180;
            EmptyPoint.Y = 180;
            InitializeComponent();/*метод, генерируемый автоматически для инициализации элементов формы(панели, кнопок и т.д.).*/
        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (Button b in panel1.Controls) /*активируем все кнопки на панели(делаем их доступными для нажатия).*/
                b.Enabled = true;

            Image orginal = Image.FromFile(@"img\img.jpg"); /*загружаем исходное изображение из файла.*/

            cropImageTomages(orginal, 270, 270); /*разрезаем изображение на 8 кусочков(270x270 — размер изображения).*/

            AddImagesToButtons(images);/*добавляем разрезанные кусочки изображения на кнопки.*/
        }

        //        Добавление изображений на кнопки
        private void AddImagesToButtons(ArrayList images)
        {
            int i = 0;
            int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7 };/* массив индексов кусочков изображения*/

            arr = suffle(arr); /*перемешиваем индексы, чтобы части изображения оказались в случайном порядке.*/
            //Присваиваем кнопкам кусочки изображения в соответствии с перемешанным массивом.
            foreach (Button b in panel1.Controls)
            {
                if (i < arr.Length)
                {
                    b.Image = (Image)images[arr[i]];
                    i++;
                }
            }
        }

        //Перемешивание индексов
        private int[] suffle(int[] arr)
        {
            Random rand = new Random();
            arr = arr.OrderBy(x => rand.Next()).ToArray(); /*сортируем массив в случайном порядке с помощью генератора случайных чисел.*/
            return arr;
        }
        //        Разрезание изображения

        private void cropImageTomages(Image orginal, int w, int h)
        {
            Bitmap bmp = new Bitmap(w, h); /*создаем пустое изображение */

            Graphics graphic = Graphics.FromImage(bmp);

            graphic.DrawImage(orginal, 0, 0, w, h); /*копируем исходное изображение на созданный холст.*/

            graphic.Dispose();

            int movr = 0, movd = 0; /*сдвиги по горизонтали и вертикали для вырезания следующего кусочка.*/

            for (int x = 0; x < 8; x++)//Разрезаем изображение на кусочки 90x90 пикселей.
            {
                Bitmap piece = new Bitmap(90, 90);

                for (int i = 0; i < 90; i++)
                    for (int j = 0; j < 90; j++)
                        piece.SetPixel(i, j,
                            bmp.GetPixel(i + movr, j + movd));

                images.Add(piece); /*добавляем кусочек в список.*/

                movr += 90;

                if (movr == 270)
                {
                    movr = 0;
                    movd += 90;
                }
            }

        }
    }
}
