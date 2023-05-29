using System;
using System.Windows.Forms;
using System.Drawing;

namespace Maze
{
    class Labirint
    {
        public int height;
        public int width;

        public int characterX; // текущая позиция персонажа по оси X
        public int characterY; // текущая позиция персонажа по оси Y
        private int collectedMedals = 0;
        public int health = 100;

        public MazeObject[,] maze;
        public PictureBox[,] images;

        public static Random r = new Random();
        public Form parent;

        public Labirint(Form parent, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.parent = parent;

            maze = new MazeObject[height, width];
            images = new PictureBox[height, width];

            this.characterX = 40;
            this.characterY = 30;

            Generate();
        }

        private void Generate()
        {
            int smileX = 0;
            int smileY = 2;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    MazeObject.MazeObjectType current = MazeObject.MazeObjectType.HALL;

                    if (r.Next(5) == 0)
                    {
                        current = MazeObject.MazeObjectType.WALL;
                    }

                    if (r.Next(250) == 0)
                    {
                        current = MazeObject.MazeObjectType.MEDAL;
                        collectedMedals++;
                    }

                    if (r.Next(250) == 0)
                    {
                        current = MazeObject.MazeObjectType.ENEMY;
                    }

                    if (r.Next(250) == 0)
                    {
                        current = MazeObject.MazeObjectType.MEDICINE;
                    }

                    if (y == 0 || x == 0 || y == height - 1 | x == width - 1)
                    {
                        current = MazeObject.MazeObjectType.WALL;
                    }

                    if (x == smileX && y == smileY)
                    {
                        current = MazeObject.MazeObjectType.CHAR;
                    }

                    if (x == smileX + 1 && y == smileY || x == width - 1 && y == height - 3)
                    {
                        current = MazeObject.MazeObjectType.HALL;
                    }
                    
                    maze[y, x] = new MazeObject(current);
                    images[y, x] = new PictureBox();
                    images[y, x].Location = new Point(x * maze[y, x].width, y * maze[y, x].height);
                    images[y, x].Parent = parent;
                    images[y, x].Width = maze[y, x].width;
                    images[y, x].Height = maze[y, x].height;
                    images[y, x].BackgroundImage = maze[y, x].texture;
                    images[y, x].Visible = false;

                    if (x == smileX && y == smileY)
                    {
                        current = MazeObject.MazeObjectType.CHAR;
                    }

                    if ((x == smileX + 1 && y == smileY && x < width - 1) || (x == width - 1 && y == height - 3 && y < height - 1))
                    {
                        current = MazeObject.MazeObjectType.HALL;
                    }
                }

            }
        }

        public void Show()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    images[y, x].Visible = true;
                }
            }
        }
        private bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }
        private int TotalMedals()
        {
            int totalMedals = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (maze[y, x].type == MazeObject.MazeObjectType.MEDAL || maze[y, x].type == MazeObject.MazeObjectType.MEDICINE)
                    {
                        totalMedals++;
                    }
                }
            }

            return totalMedals;
        }
        public void MoveCharacter(int deltaX, int deltaY)
        {
            int newCharacterX = characterX + deltaX;
            int newCharacterY = characterY + deltaY;

            if (IsWithinBounds(newCharacterX, newCharacterY) && maze[newCharacterY, newCharacterX].type != MazeObject.MazeObjectType.WALL)
            {
                images[characterY, characterX].Visible = false;

                characterX = newCharacterX;
                characterY = newCharacterY;

                images[characterY, characterX].Visible = true;

                if (characterX == width - 1 && characterY == height - 1)
                {
                    MessageBox.Show("Победа - Найден выход");
                }
            }
            if (maze[newCharacterY, newCharacterX].type == MazeObject.MazeObjectType.MEDAL)
            {
                collectedMedals++;
                maze[newCharacterY, newCharacterX] = new MazeObject(MazeObject.MazeObjectType.HALL);
                images[newCharacterY, newCharacterX].BackgroundImage = maze[newCharacterY, newCharacterX].texture;

                if (collectedMedals == TotalMedals())
                {
                    MessageBox.Show("Победа - Медали собраны");
                }
            }

            parent.Text = "Maze - Собрано медалей: " + collectedMedals;

            if (maze[newCharacterY, newCharacterX].type == MazeObject.MazeObjectType.ENEMY)
            {
                health -= 20; // Уменьшение здоровья на 20%

                if (health <= 0) // Проверка на поражение
                {
                    MessageBox.Show("Поражение - Вы погибли");
                    return;
                }

                maze[newCharacterY, newCharacterX] = new MazeObject(MazeObject.MazeObjectType.HALL); // Удаление врага из лабиринта
                images[newCharacterY, newCharacterX].BackgroundImage = maze[newCharacterY, newCharacterX].texture; // Обновление текстуры новой позиции
            }

            if (maze[newCharacterY, newCharacterX].type == MazeObject.MazeObjectType.MEDICINE)
            {
                health += 5; // Увеличение здоровья на 5%

                if (health > 100) // Проверка, чтобы здоровье не превышало 100%
                {
                    health = 100;
                }

                maze[newCharacterY, newCharacterX] = new MazeObject(MazeObject.MazeObjectType.HALL); // Удаление лекарства из лабиринта
                images[newCharacterY, newCharacterX].BackgroundImage = maze[newCharacterY, newCharacterX].texture; // Обновление текстуры новой позиции
            }
        }

    }
}
