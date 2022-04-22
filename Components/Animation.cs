using Microsoft.Xna.Framework.Graphics;

namespace DatabaseProjekt
{
    public class Animation
    {
        public float FPS { get; private set; }
        public string Name { get; private set; }
        public Texture2D[] Sprites { get; private set; }

        public Animation(string name, Texture2D[] sprites, float fps)
        {
            Name = name;
            Sprites = sprites;
            FPS = fps;
        }
    }
}
