using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GAMES
{
    public partial class Form4 : Form
    {
        private const int CELL_SIZE_PIXELS = 30;                    // Размер клетки игрового поля, в пикселях
        private const int ROWS_NUMBER = 15;                         // Количество рядов в игровом поле
        private const int COLS_NUMBER = 15;                         // Количество столбцов в игровом поле
        private const int FIELD_LEFT_OFFSET_PIXELS = 40;            // Отступ в пикселях от левого края формы
        private const int FIELD_TOP_OFFSET_PIXELS = 15;             // Отступ в пикселях от правого края формы
        private const int INITIAL_SNAKE_SPEED_INTERVAL = 300;       // Задержка (свойство "Interval") для основного игрового таймера TimerGameLoop
        private const int SPEED_INCREMENT_BY = 5;                   // На сколько миллисекунд увеличить скорость "Змейки" при очередном поглощении змейкой "Еды"

        private enum SnakeDirection
        {
            Left,
            Right,
            Up,
            Down
        }

        private SnakeDirection snakeDirection = SnakeDirection.Up;  // текущее направление движения "Змейки"
        private LinkedList<Point> snake = new LinkedList<Point>();  // Список точек, содержащих координаты всего "тела Змейки"
        private Point food;                                         // Точка, содержащая координаты "Еды" для "Змейки"
        private Random rand = new Random();                         // генератор псевдослучайных чисел. нужен для генерации очередной "Еды" в произвольном месте игрового поля
        private bool isGameEnded;                                   // признак: игра завершена?

        public Form4()
        {
            InitializeComponent();
        }

        private void InitializeSnake()
        {
            snakeDirection = SnakeDirection.Up;
            snake.Clear();
            snake.AddFirst(new Point(ROWS_NUMBER - 1, COLS_NUMBER / 2 - 1));
        }

        private void DrawGrid(Graphics g)
        {
            for (int row = 0; row <= ROWS_NUMBER; row++)
            {
                g.DrawLine(Pens.Cyan,
                    new Point(FIELD_LEFT_OFFSET_PIXELS, FIELD_TOP_OFFSET_PIXELS + row * CELL_SIZE_PIXELS),
                    new Point(FIELD_LEFT_OFFSET_PIXELS + CELL_SIZE_PIXELS * ROWS_NUMBER, FIELD_TOP_OFFSET_PIXELS + row * CELL_SIZE_PIXELS)
                );

                for (int col = 0; col <= COLS_NUMBER; col++)
                {
                    g.DrawLine(Pens.Cyan,
                        new Point(FIELD_LEFT_OFFSET_PIXELS + col * CELL_SIZE_PIXELS, FIELD_TOP_OFFSET_PIXELS),
                        new Point(FIELD_LEFT_OFFSET_PIXELS + col * CELL_SIZE_PIXELS, FIELD_TOP_OFFSET_PIXELS + CELL_SIZE_PIXELS * COLS_NUMBER)
                    );
                }
            }
        }

        private void GenerateFood()
        {
            bool isFoodClashWithSnake;
            do
            {
                food = new Point(rand.Next(0, ROWS_NUMBER), rand.Next(0, COLS_NUMBER));
                isFoodClashWithSnake = false;
                foreach (Point p in snake)
                {
                    if (p.X == food.X && p.Y == food.Y)
                    {
                        isFoodClashWithSnake = true;
                        break;
                    }
                }
            } while (isFoodClashWithSnake);

            TimerGameLoop.Interval -= SPEED_INCREMENT_BY;
        }

        private void DrawSnake(Graphics g)
        {
            foreach (Point p in snake)
            {
                g.FillRectangle(Brushes.Lime, new Rectangle(
                    FIELD_LEFT_OFFSET_PIXELS + p.Y * CELL_SIZE_PIXELS + 1,
                    FIELD_TOP_OFFSET_PIXELS + p.X * CELL_SIZE_PIXELS + 1,
                    CELL_SIZE_PIXELS - 1,
                    CELL_SIZE_PIXELS - 1));
            }
        }

        private void DrawFood(Graphics g)
        {
            g.FillRectangle(Brushes.Red, new Rectangle(
                FIELD_LEFT_OFFSET_PIXELS + food.Y * CELL_SIZE_PIXELS + 1,
                FIELD_TOP_OFFSET_PIXELS + food.X * CELL_SIZE_PIXELS + 1,
                CELL_SIZE_PIXELS - 1,
                CELL_SIZE_PIXELS - 1));
        }

        private void MoveSnake()
        {
            LinkedListNode<Point> head = snake.First;
            Point newHead = new Point(0, 0);
            switch (snakeDirection)
            {
                case SnakeDirection.Left:
                    newHead = new Point(head.Value.X, head.Value.Y - 1);
                    break;
                case SnakeDirection.Right:
                    newHead = new Point(head.Value.X, head.Value.Y + 1);
                    break;
                case SnakeDirection.Down:
                    newHead = new Point(head.Value.X + 1, head.Value.Y);
                    break;
                case SnakeDirection.Up:
                    newHead = new Point(head.Value.X - 1, head.Value.Y);
                    break;
            }

            if (snake.Any(point => point.X == newHead.X && point.Y == newHead.Y))
            {
                // "Змейка" съела саму себя! Конец игры!
                Invalidate();
                GameOver();
                return;
            }

            snake.AddFirst(newHead);

            if (newHead.X == food.X && newHead.Y == food.Y)
            {
                GenerateFood();
            }
            else
            {
                snake.RemoveLast();
            }
        }

        private void Form4_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawGrid(g);
            DrawFood(g);
            DrawSnake(g);
        }


        private void Form4_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            BackColor = Color.Black;
            StartGame();
        }

        private void StartGame()
        {
            GenerateFood();
            InitializeSnake();
            isGameEnded = false;
            TimerGameLoop.Start();
            TimerGameLoop.Interval = INITIAL_SNAKE_SPEED_INTERVAL;
        }

        private bool IsGameOver()
        {
            LinkedListNode<Point> head = snake.First;
            switch (snakeDirection)
            {
                case SnakeDirection.Left:
                    return head.Value.Y - 1 < 0;
                case SnakeDirection.Right:
                    return head.Value.Y + 1 >= COLS_NUMBER;
                case SnakeDirection.Down:
                    return head.Value.X + 1 >= ROWS_NUMBER;
                case SnakeDirection.Up:
                    return head.Value.X - 1 < 0;
            }
            return false;
        }

        private void GameOver()
        {
            isGameEnded = true;
            TimerGameLoop.Stop();
            if (MessageBox.Show("Конец игры! Начать заново?", "Конец игры", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                StartGame();
            }
        }
                
        private void ChangeSnakeDirection(SnakeDirection restrictedDirection, SnakeDirection newDirection)
        {
            if (snakeDirection != restrictedDirection)
            {
                snakeDirection = newDirection;
            }
        }

        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                    ChangeSnakeDirection(SnakeDirection.Right, SnakeDirection.Left);
                    break;
                case Keys.Right:
                case Keys.D:
                    ChangeSnakeDirection(SnakeDirection.Left, SnakeDirection.Right);
                    break;
                case Keys.Down:
                case Keys.S:
                    ChangeSnakeDirection(SnakeDirection.Up, SnakeDirection.Down);
                    break;
                case Keys.Up:
                case Keys.W:
                    ChangeSnakeDirection(SnakeDirection.Down, SnakeDirection.Up);
                    break;
                case Keys.Escape:
                    TimerGameLoop.Stop();
                    Close();
                    break;
                case Keys.Space:
                    if (isGameEnded && !TimerGameLoop.Enabled)
                    {
                        StartGame();
                    }
                    break;
            }
        }

        private void TimerGameLoop_Tick_1(object sender, EventArgs e)
        {
            if (IsGameOver())
            {
                GameOver();
            }
            else
            {
                MoveSnake();
                Invalidate();
            }
        }
    }
}
