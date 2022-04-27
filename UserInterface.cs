using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System.Data.SQLite;
using System;

namespace DatabaseProjekt
{
    public class UserInterface : IDatabaseImporter
    {

        private FishType currentArea;
        private Texture2D[] sprites = new Texture2D[3];
        private Texture2D[] areaSprites = new Texture2D[3];
        private Texture2D sprite;
        private SpriteFont saveSelectFont;
        private SpriteFont titleScreenFont;

        private KeyboardState kState;
        private KeyboardState kStateOld;
        private MouseState mState;
        private bool mLeftReleased = true;
        private bool playerMade = false;
        private int saveID;
        private int[] userHighscore = new int[4];
        private bool scoreSet = false;
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
                    saveID = 1;
                    GameWorld.Instance.GameState = GameState.TitleScreen;
                }
                if (mState.Position.X < 1000 && mState.Position.X > 666 && mState.Position.Y < 295 && mState.Position.Y > 50)
                {
                    saveID = 2;
                    GameWorld.Instance.GameState = GameState.TitleScreen;
                }
                if (mState.Position.X < 375 && mState.Position.X > 64 && mState.Position.Y < 746 && mState.Position.Y > 516)
                {
                    saveID = 3;
                    GameWorld.Instance.GameState = GameState.TitleScreen;
                }
                if (mState.Position.X < 1000 && mState.Position.X > 666 && mState.Position.Y < 746 && mState.Position.Y > 516)
                {
                    saveID = 4;
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

            CreatePlayer();
            if (currentArea == FishType.river)
            {

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
                case GameState.SaveSelect:
                    GetAttributes();
                    spriteBatch.DrawString(titleScreenFont, $"{userHighscore[0]}", new Vector2(70, 300), Color.White);
                    spriteBatch.DrawString(titleScreenFont, $"{userHighscore[1]}", new Vector2(670, 300), Color.White);
                    spriteBatch.DrawString(titleScreenFont, $"{userHighscore[2]}", new Vector2(70, 425), Color.White);
                    spriteBatch.DrawString(titleScreenFont, $"{userHighscore[3]}", new Vector2(670, 425), Color.White);
                    break;
                case GameState.TitleScreen:
                    spriteBatch.DrawString(titleScreenFont, "Play", new Vector2(15, 630), Color.White);
                    spriteBatch.DrawString(titleScreenFont, "HighScore", new Vector2(15, 700), Color.White);
                    break;
                case GameState.Playing:
                    break;
            }
        }

        public void Open()
        {
            GameWorld.Instance.connection.Open();
        }

        public void Close()
        {
            GameWorld.Instance.connection.Close();
        }

        public void GetTexture()
        {
            throw new NotImplementedException();
        }

        public void GetId(string type)
        {
            throw new NotImplementedException();
        }

        public void GetAttributes()
        {
            Open();
            
            for (int i = 1; i < 4; i++)
            {
                
                var cmd = new SQLiteCommand($"SELECT Score FROM highscore WHERE Id={i}", GameWorld.Instance.connection);
                var dataread = cmd.ExecuteReader();

                while (dataread.Read())
                {
                   userHighscore[i-1]  = dataread.GetInt32(0);
                }
                
            }
            Close();
        }

        public void CreatePlayer()
        {
            if (playerMade == false)
            {
                GameObject player = PlayerFactory.Instance.CreateObject();
                Player p = player.GetComponent<Player>() as Player;
                p.UserID = saveID;
                p.SaveHighScore(200);

                GameWorld.Instance.Instantiate(player);

                playerMade = true;
            }


        }
    }
}
