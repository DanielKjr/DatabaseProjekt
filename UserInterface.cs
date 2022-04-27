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
        private FishType currentArea;
        private Texture2D[] sprites = new Texture2D[3];
        private Texture2D[] areaSprites = new Texture2D[3];
        private Texture2D sprite;
        private SpriteFont saveSelectFont;
        private SpriteFont titleScreenFont;
        string species;
        private KeyboardState kState;
        private KeyboardState kStateOld;
        private MouseState mState;
        private bool mLeftReleased = true;
        private string[] myType = new string[]
       {
            "riverfish",
            "seafish",
            "fjordfish"
       };

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
        public void FishSpawner(FishType area, int amount)
        {
            GameWorld.Instance.connection.Open();

           
            for (int i = 0; i < 3; i++)
            {
               var cmd = new SQLiteCommand($"SELECT Id,Species, Depth, Weight FROM '{myType[(int)currentArea]}' WHERE Id={i}", GameWorld.Instance.connection);
              var  dataread = cmd.ExecuteReader();
               
                while (dataread.Read())
                {
                     species = dataread.GetString(1);
                    double depth = dataread.GetInt32(2);
                    double weight = dataread.GetDouble(3);
                   
                }
                for (int x = 0; x < amount; x++)
                {
                    GameWorld.Instance.Instantiate(
                    GameWorld.Instance.SpawnFish(currentArea, species));
                }
            }
            

            GameWorld.Instance.connection.Close();
        }


        public void Start()
        {
            sprites[0] = GameWorld.Instance.Content.Load<Texture2D>("saveSelect");
            sprites[1] = GameWorld.Instance.Content.Load<Texture2D>("titleScreenBackground");
            sprites[2] = GameWorld.Instance.Content.Load<Texture2D>("riverBackground");
            //sprites[3] = GameWorld.Instance.Content.Load<Texture2D>("");
            //sprites[4] = GameWorld.Instance.Content.Load<Texture2D>("");
            areaSprites[0] = GameWorld.Instance.Content.Load<Texture2D>("riverBackground");
            areaSprites[1] = GameWorld.Instance.Content.Load<Texture2D>("seaBackGround");
            areaSprites[2] = GameWorld.Instance.Content.Load<Texture2D>("fjordBackground");



            saveSelectFont = GameWorld.Instance.Content.Load<SpriteFont>("saveSelectFont");
            titleScreenFont = GameWorld.Instance.Content.Load<SpriteFont>("titleScreenFont");




        }
        public void Update(GameTime gameTime)
        {
            mState = Mouse.GetState();
            kState = Keyboard.GetState();
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
                    Playing();
                    break;
                case GameState.End:

                    break;
            }
        }
        public void SaveSelect()
        {
            sprite = sprites[0];

            if (mState.LeftButton == ButtonState.Pressed && mLeftReleased == true)
            {
                mLeftReleased = false;
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
            if (mState.LeftButton == ButtonState.Released)
            {
                mLeftReleased = true;
            }
        }
        public void TitleScreen()
        {
            sprite = sprites[1];
            if (mState.LeftButton == ButtonState.Pressed && mLeftReleased == true)
            {
                mLeftReleased = false;
                if (mState.Position.X < 375 && mState.Position.X > 64 && mState.Position.Y < 690 && mState.Position.Y > 630)
                {
                    GameWorld.Instance.GameState = GameState.Playing;

                }
                if (mState.Position.X < 200 && mState.Position.X > 10 && mState.Position.Y < 120 && mState.Position.Y > 50)
                {
                    GameWorld.Instance.GameState = GameState.Playing;
                }


            }
            if (mState.LeftButton == ButtonState.Released)
            {
                mLeftReleased = true;
            }

        }
        public void ViewScores()
        {
            sprite = sprites[2];
        }
        public void Playing()
        {
            sprite = sprites[2];
            sprites[2] = areaSprites[(int)currentArea];
            if (currentArea == FishType.river)
            {
                FishSpawner(currentArea, 2);
                if (kState.IsKeyDown(Keys.Left) && kState != kStateOld)
                {
                    currentArea = FishType.sea;
                    
                }
                kStateOld = kState;
            }
            
            if (currentArea == FishType.sea)
            {

                if (kState.IsKeyDown(Keys.Left) && kState != kStateOld)
                {
                    currentArea = FishType.fjord;
                }
                if (kState.IsKeyDown(Keys.Right) && kState != kStateOld)
                {
                    currentArea = FishType.river;
                }
                kStateOld = kState;
            }
            
            if (currentArea == FishType.fjord)
            {
                if (kState.IsKeyDown(Keys.Right) && kState != kStateOld)
                {
                    currentArea = FishType.sea;
                }
                kStateOld = kState;
            }
            
            
        }
        public void End()
        {
            sprite = sprites[4];
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(sprite, new Vector2(0, 0), Color.White);
            switch (GameWorld.Instance.GameState)
            {
                case GameState.TitleScreen:
                    spriteBatch.DrawString(titleScreenFont, "Play", new Vector2(15, 630), Color.White);
                    spriteBatch.DrawString(titleScreenFont, "HighScore", new Vector2(15, 700), Color.White);
                    break;
            }
        }
    }
}
