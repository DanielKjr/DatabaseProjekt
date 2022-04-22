using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DatabaseProjekt
{
    public abstract class Component
    {
        public bool IsEnabled { get; set; }
        public GameObject GameObject { get; set; }

        public virtual void Awake()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual Object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
