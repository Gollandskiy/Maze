using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Maze
{
    public partial class Form1 : Form
    {
        public int characterX; // текущая позиция персонажа по оси X
        public int characterY; // текущая позиция персонажа по оси Y
        private Labirint labirint;
        public Form1()
        {
            InitializeComponent();
            Options();
            StartGame();
            this.KeyDown += Form1_KeyDown;
        }

        public void Options()
        {
            Text = "Maze";

            BackColor = Color.FromArgb(255, 92, 118, 137);

            int sizeX = 40;
            int sizeY = 20;

            Width = sizeX * 16 + 16;
            Height = sizeY * 16 + 40;
            StartPosition = FormStartPosition.CenterScreen;
        }

        public void StartGame() {
            labirint = new Labirint(this, 40, 20);
            labirint.Show();

            UpdateHealthLabel();
        }
        private void UpdateHealthLabel()
        {
            Text = "Maze - Здоровье: " + labirint.health + "%";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int deltaX = 0;
            int deltaY = 0;

            switch (e.KeyCode)
            {
                case Keys.Left:
                    deltaX = -1;
                    break;
                case Keys.Right:
                    deltaX = 1;
                    break;
                case Keys.Up:
                    deltaY = -1;
                    break;
                case Keys.Down:
                    deltaY = 1;
                    break;
            }

            labirint.MoveCharacter(deltaX, deltaY);
        }
    }
}
