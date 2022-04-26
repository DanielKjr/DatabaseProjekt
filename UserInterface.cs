using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System.Data.SQLite;
using System;

namespace DatabaseProjekt
{
    public class UserInterface
    {
        SpriteFont spriteFont;
        Texture2D[] sprites = new Texture2D[4];
        Rectangle titleOption1 = new Rectangle();        
        Texture2D sprite;
        MouseState mState;
        
        private static UserInterface instance;
        public static UserInterface Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserInterface();
                }
                return instance;
            }
                       
        }
        private UserInterface()
        {

        }
        public void Start()
        {
            sprites[0] = GameWorld.Instance.Content.Load<Texture2D>("saveSelect");
            sprites[1] = GameWorld.Instance.Content.Load<Texture2D>("titleScreenBackground");
            //sprites[2] = GameWorld.Instance.Content.Load<Texture2D>("");
            //sprites[3] = GameWorld.Instance.Content.Load<Texture2D>("");
            //sprites[4] = GameWorld.Instance.Content.Load<Texture2D>("");



            spriteFont = GameWorld.Instance.Content.Load<SpriteFont>("spriteFont");
        }
        public void Update(GameTime gameTime)
        {
            mState = Mouse.GetState();
            switch (GameWorld.Instance.GameState)
            {
                case GameState.SaveSelect:
                    SaveSelect();
                    break;
                case GameState.TitleScreen:
                    TitleScreen();
                    break;
                case GameState.ViewScores:

                    break;
                case GameState.Playing:

                    break;
                case GameState.End:

                    break;
            }
        }
        public void SaveSelect()
        {
            sprite = sprites[0];

            if (mState.LeftButton == ButtonState.Pressed && mState.LeftButton != ButtonState.Released)
            {
                if (mState.Position.X < 375 && mState.Position.X > 64 && mState.Position.Y < 295 && mState.Position.Y > 50)
                {
                    GameWorld.Instance.GameState = GameState.TitleScreen;
                    
                }
                if (mState.Position.X < 1000 && mState.Position.X > 666 && mState.Position.Y < 295 && mState.Position.Y > 50)
                {
                    GameWorld.Instance.GameState = GameState.TitleScreen;
                }
                if (mState.Position.X < 375 && mState.Position.X > 64 && mState.Position.Y < 746 && mState.Position.Y > 516)
                {
                    GameWorld.Instance.GameState = GameState.TitleScreen;
                }
                if (mState.Position.X < 1000 && mState.Position.X > 666 && mState.Position.Y < 746 && mState.Position.Y > 516)
                {
                    GameWorld.Instance.GameState = GameState.TitleScreen;

                }
                
            }
        }
        public void TitleScreen()
        {
            sprite = sprites[1];
            

        }
        public void ViewScores()
        {
            sprite = sprites[2];
        }
        public void Playing()
        {
            sprite = sprites[3];
        }
        public void End()
        {
            sprite = sprites[4];
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Vector2(0, 0), Color.White);
        }
    }
}
