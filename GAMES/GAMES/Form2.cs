using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAMES
{
    public partial class Form2 : Form
    {
        private bool player1_turn = true;
        private int[,] boardValue = new int[3, 3];
        const int X_VALUE = 1;
        const int O_VALUE = 2;

        public Form2()
        {
            InitializeComponent();
            restart_matrix();
            player1_turn = true;
        }


        //-------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            if (player1_turn == true)
            {
                player1_turn = false;
                ((Button)sender).Text = "X";
                boardValue[0, 0] = X_VALUE;
            }
            else
            {
                player1_turn = true;
                ((Button)sender).Text = "O";
                boardValue[0, 0] = O_VALUE;
            }

            check_win();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (player1_turn == true)
            {
                player1_turn = false;
                ((Button)sender).Text = "X";
                boardValue[0, 1] = X_VALUE;
            }
            else
            {
                player1_turn = true;
                ((Button)sender).Text = "O";
                boardValue[0, 1] = O_VALUE;
            }
            check_win();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (player1_turn == true)
            {
                player1_turn = false;
                ((Button)sender).Text = "X";
                boardValue[0, 2] = X_VALUE;
            }
            else
            {
                player1_turn = true;
                ((Button)sender).Text = "O";
                boardValue[0, 2] = O_VALUE;
            }
            check_win();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (player1_turn == true)
            {
                player1_turn = false;
                ((Button)sender).Text = "X";
                boardValue[1, 0] = X_VALUE;
            }
            else
            {
                player1_turn = true;
                ((Button)sender).Text = "O";
                boardValue[1, 0] = O_VALUE;
            }
            check_win();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (player1_turn == true)
            {
                player1_turn = false;
                ((Button)sender).Text = "X";
                boardValue[1, 1] = X_VALUE;
            }
            else
            {
                player1_turn = true;
                ((Button)sender).Text = "O";
                boardValue[1, 1] = O_VALUE;
            }
            check_win();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (player1_turn == true)
            {
                player1_turn = false;
                ((Button)sender).Text = "X";
                boardValue[1, 2] = X_VALUE;
            }
            else
            {
                player1_turn = true;
                ((Button)sender).Text = "O";
                boardValue[1, 2] = O_VALUE;
            }
            check_win();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (player1_turn == true)
            {
                player1_turn = false;
                ((Button)sender).Text = "X";
                boardValue[2, 0] = X_VALUE;
            }
            else
            {
                player1_turn = true;
                ((Button)sender).Text = "O";
                boardValue[2, 0] = O_VALUE;
            }
            check_win();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (player1_turn == true)
            {
                player1_turn = false;
                ((Button)sender).Text = "X";
                boardValue[2, 1] = X_VALUE;
            }
            else
            {
                player1_turn = true;
                ((Button)sender).Text = "O";
                boardValue[2, 1] = O_VALUE;
            }
            check_win();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (player1_turn == true)
            {
                player1_turn = false;
                ((Button)sender).Text = "X";
                boardValue[2, 2] = X_VALUE;
            }
            else
            {
                player1_turn = true;
                ((Button)sender).Text = "O";
                boardValue[2, 2] = O_VALUE;
            }
            check_win();
        }
        //-------------------------------

        private void check_win()
        {
            //Проверка строк
            for (int i = 0; i < 3; ++i)
            {
                if (Convert.ToInt32(boardValue[i, 0]) == Convert.ToInt32(boardValue[i, 1]) &&
                    Convert.ToInt32(boardValue[i, 1]) == Convert.ToInt32(boardValue[i, 2]) &&
                    Convert.ToInt32(boardValue[i, 1]) != 0)
                {
                    color_winning_buttons(find_button_by_value(i, 0),
                        find_button_by_value(i, 1),
                        find_button_by_value(i, 2));
                    display_win_screen(boardValue[i, 0]);

                    return;
                }
            }

            //Проверка столбцов
            for (int i = 0; i < 3; ++i)
            {
                if (Convert.ToInt32(boardValue[0, i]) == Convert.ToInt32(boardValue[1, i]) &&
                    Convert.ToInt32(boardValue[1, i]) == Convert.ToInt32(boardValue[2, i]) &&
                    Convert.ToInt32(boardValue[2, i]) != 0)
                {
                    color_winning_buttons(find_button_by_value(0, i),
                        find_button_by_value(1, i),
                        find_button_by_value(2, i));
                    display_win_screen(boardValue[1, i]);
                    return;
                }
            }

            //Проверка диагоналей
            if (Convert.ToInt32(boardValue[0, 0]) == Convert.ToInt32(boardValue[1, 1]) &&
                Convert.ToInt32(boardValue[1, 1]) == Convert.ToInt32(boardValue[2, 2]) &&
                Convert.ToInt32(boardValue[2, 2]) != 0)
            {
                color_winning_buttons(find_button_by_value(0, 0),
                    find_button_by_value(1, 1),
                    find_button_by_value(2, 2));
                display_win_screen(boardValue[1, 1]);
                return;
            }
            else if (Convert.ToInt32(boardValue[0, 2]) == Convert.ToInt32(boardValue[1, 1]) &&
              Convert.ToInt32(boardValue[1, 1]) == Convert.ToInt32(boardValue[2, 0]) &&
              Convert.ToInt32(boardValue[2, 0]) != 0)
            {
                color_winning_buttons(find_button_by_value(0, 2),
                    find_button_by_value(1, 1),
                    find_button_by_value(2, 0));
                display_win_screen(boardValue[1, 1]);
                return;
            }

            //Вывод ничьи
            bool is_completed = true;
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 3; j++)
                    if (boardValue[i, j] == 0)
                        is_completed = false;
            if (is_completed)
            {
                MessageBox.Show("Ничья :(");
                restart_matrix();
            }
        }

        // Вывод сообщения о выигрыше
        private void display_win_screen(int val)
        {
            if (val == X_VALUE)
            {
                MessageBox.Show("Игрок 1 выйграл!");
                int player1_score = Convert.ToInt32(label6.Text);
                ++player1_score;
                label6.Text = player1_score.ToString();
            }
            else
            {
                MessageBox.Show("Игрок 2 выйграл!");
                int player2_score = Convert.ToInt32(label7.Text);
                ++player2_score;
                label7.Text = player2_score.ToString();
            }

            restart_matrix();
        }

        //Перезапуск площадки
        private void restart_matrix()
        {
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 3; ++j)
                    boardValue[i, j] = 0;

            clear_buttons();
            player1_turn = true;
        }

    }

}
