using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DatabaseProjekt
{
    public class SpriteRenderer : Component
    {
        public Texture2D Sprite { get; set; }
        public Vector2 Origin { get; set; }

        public override void Start()
        {
            Origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
        }

        /// <summary>
        /// Sets the sprite of a component through the instance of a SpriteRenderer
        /// </summary>
        /// <param name="spriteName"></param>
        public void SetSprite(string spriteName)
        {
            Sprite = GameWorld.Instance.Content.Load<Texture2D>(spriteName);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, GameObject.Transform.Position, null, Color.White, 0, Origin, 1, SpriteEffects.None, 1);
        }
    }
}
