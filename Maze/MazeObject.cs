using System;
using System.Drawing;

namespace Maze
{
    class MazeObject
    {
        public enum MazeObjectType { HALL, WALL, MEDAL, ENEMY, CHAR,
            MEDICINE
        }
        public Bitmap[] images = {new Bitmap(@"D:\Visual Studio ШАГ\C#\Maze - Copy\pics\hall.png"),
            new Bitmap(@"D:\Visual Studio ШАГ\C#\Maze - Copy\pics\wall.png"),
            new Bitmap(@"D:\Visual Studio ШАГ\C#\Maze - Copy\pics\medal.png"),
            new Bitmap(@"D:\Visual Studio ШАГ\C#\Maze - Copy\pics\enemy.png"),
            new Bitmap(@"D:\Visual Studio ШАГ\C#\Maze - Copy\pics\player.png"),
            new Bitmap(@"D:\Visual Studio ШАГ\C#\Maze - Copy\pics\medicine.png")};

        public MazeObjectType type;
        public int width;
        public int height;
        public Image texture;

        public MazeObject(MazeObjectType type)
        {
            this.type = type;
            width = 16;
            height = 16;
            texture = images[(int)type];
        }

    }
}
