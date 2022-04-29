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
        private Random rand = new Random();
        public FishType currentArea;
        private Texture2D[] sprites = new Texture2D[4];
        private Texture2D[] areaSprites = new Texture2D[3];
        private Texture2D sprite;
        private SpriteFont saveSelectFont;
        private SpriteFont titleScreenFont;

        private KeyboardState kState;
        private KeyboardState kStateOld;
        private MouseState mState;
        private bool mLeftReleased = true;
        private string species;
        private double weight;
        private int depth;
        private int endScore;
        private bool instructions = false;

        private bool playerMade = false;
        private int saveID;
        private int[] userHighscore = new int[4];
        private bool endTimerBegin = false;
        private float endTimer = 2;
        private bool riverSpawned = false;
        private bool seaSpawned = false;
        private bool fjordSpawned = false;
        private float playingTimer;
        private bool endBegin = false;
        private int[] highscoreScore = new int[5];
        private int[] highscoreUserId = new int[5];
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


            for (int i = 1; i < 4; i++)
            {
                var cmd = new SQLiteCommand($"SELECT Id,Species, Depth, Weight FROM '{myType[(int)currentArea]}' WHERE Id={i}", GameWorld.Instance.connection);
                var dataread = cmd.ExecuteReader();

                while (dataread.Read())
                {
                    species = dataread.GetString(1);
                    depth = dataread.GetInt32(2);
                    weight = dataread.GetDouble(3);

                }
                for (int x = 0; x < amount; x++)
                {
                    GameWorld.Instance.Instantiate(SpawnFish(currentArea, species, depth));
                }
            }

            GameWorld.Instance.connection.Close();

        }

        public GameObject SpawnFish(FishType type, string species, int depth)
        {
            GameObject fish = FishFactory.Instance.CreateObject();
            Fish f = fish.GetComponent<Fish>() as Fish;
            f.MyFishType = type;
            f.GameObject.Tag = species;

            f.GameObject.Transform.Position = new Vector2(rand.Next(150, 950), depth);



            return fish;
        }

        public void Start()
        {
            sprites[0] = GameWorld.Instance.Content.Load<Texture2D>("saveSelect");
            sprites[1] = GameWorld.Instance.Content.Load<Texture2D>("titleScreenBackground");
            sprites[2] = GameWorld.Instance.Content.Load<Texture2D>("riverBackground");
            sprites[3] = GameWorld.Instance.Content.Load<Texture2D>("fishingFrenzyEndState");

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
                    End();
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
                
                if (mState.Position.X < 645 && mState.Position.X > 385 && mState.Position.Y < 705 && mState.Position.Y > 600)
                {

                    Open();
                    var cmd = new SQLiteCommand($"DELETE FROM highscore", GameWorld.Instance.connection);
                    cmd.ExecuteNonQuery();
                    Close();
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
                if (mState.Position.X < 375 && mState.Position.X > 5 && mState.Position.Y < 685 && mState.Position.Y > 630)
                {
                    playingTimer = 15;
                    GameWorld.Instance.GameState = GameState.Playing;

                }
                if (mState.Position.X < 200 && mState.Position.X > 10 && mState.Position.Y < 790 && mState.Position.Y > 700)
                {
                    endBegin = true;
                    GameWorld.Instance.GameState = GameState.End;
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
            if (instructions == false)
            {
                if (kState.IsKeyDown(Keys.Space) && kState != kStateOld)
                {
                    instructions = true;
                }
            }
            CreatePlayer();
            if (currentArea == FishType.river)
            {

                if (!riverSpawned)
                {
                    FishSpawner(currentArea, 2);
                    riverSpawned = true;
                }

                if (kState.IsKeyDown(Keys.Left) && kState != kStateOld)
                {
                    GameWorld.Instance.CleanUpFish();
                    currentArea = FishType.sea;
                    riverSpawned = false;
                }
                kStateOld = kState;
            }

            if (currentArea == FishType.sea)
            {

                if (!seaSpawned)
                {
                    FishSpawner(currentArea, 2);
                    seaSpawned = true;
                }
                if (kState.IsKeyDown(Keys.Left) && kState != kStateOld)
                {
                    GameWorld.Instance.CleanUpFish();
                    currentArea = FishType.fjord;
                    seaSpawned = false;
                }
                if (kState.IsKeyDown(Keys.Right) && kState != kStateOld)
                {
                    GameWorld.Instance.CleanUpFish();
                    currentArea = FishType.river;
                    seaSpawned = false;
                }
                kStateOld = kState;
            }

            if (currentArea == FishType.fjord)
            {

                if (!fjordSpawned)
                {
                    FishSpawner(currentArea, 2);
                    fjordSpawned = true;
                }
                if (kState.IsKeyDown(Keys.Right) && kState != kStateOld)
                {
                    GameWorld.Instance.CleanUpFish();
                    currentArea = FishType.sea;
                    fjordSpawned = false;
                }
                kStateOld = kState;
            }

            playingTimer -= GameWorld.DeltaTime;

            if (playingTimer <= 0)
            {
                endBegin = true;
                GameWorld.Instance.GameState = GameState.End;
            }

        }
        public void End()
        {
            sprite = sprites[3];
            if (endBegin)
            {
                Player player = (Player)GameWorld.Instance.FindObjectOfType<Player>();
                if (player != null)
                {
                    player.SaveHighScore(player.Score);
                    endScore = player.Score;
                    GameWorld.Instance.Destroy(player.GameObject);
                    playerMade = false;
                    GameWorld.Instance.CleanUpFish();
                }

                fjordSpawned = false;
                riverSpawned = false;
                seaSpawned = false;
                endBegin = false;
                List<highscore> highscores = ReadHighscores();
                int i = 0;

                foreach (highscore h in highscores)
                {

                    highscoreScore[i] = h.Score;
                    highscoreUserId[i] = h.UserId;
                    i++;

                }
                endTimerBegin = true;
                endBegin = false;
            }
            if (endTimerBegin)
            {
                endTimer -= GameWorld.DeltaTime;
                if (endTimer <= 0 && kState.IsKeyDown(Keys.Space) && kState != kStateOld)
                {
                    GameWorld.Instance.GameState = GameState.SaveSelect;
                    kState = kStateOld;
                    endTimer = 2;
                }
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(sprite, new Vector2(0, 0), Color.White);
            switch (GameWorld.Instance.GameState)
            {
                case GameState.SaveSelect:
                    GetAttributes();
                    spriteBatch.DrawString(saveSelectFont, "click this text to\n" +
                        " delete current\n" +
                        "   Highscores"
                        , new Vector2(385, 600), Color.White);
                    spriteBatch.DrawString(saveSelectFont, "Click the picture of\n" +
                        $"your preffered profile\n" +
                        $"to login and play", new Vector2(385, 0), Color.White);
                    spriteBatch.DrawString(titleScreenFont, $"last run: {userHighscore[0]}", new Vector2(70, 300), Color.White);
                    spriteBatch.DrawString(titleScreenFont, $"last run: {userHighscore[1]}", new Vector2(670, 300), Color.White);
                    spriteBatch.DrawString(titleScreenFont, $"last run: {userHighscore[2]}", new Vector2(70, 425), Color.White);
                    spriteBatch.DrawString(titleScreenFont, $"last run: {userHighscore[3]}", new Vector2(670, 425), Color.White);
                    break;
                case GameState.TitleScreen:
                    spriteBatch.DrawString(titleScreenFont, "Play", new Vector2(15, 630), Color.White);
                    spriteBatch.DrawString(titleScreenFont, "HighScore", new Vector2(15, 700), Color.White);
                    break;
                case GameState.Playing:
                    spriteBatch.DrawString(saveSelectFont, $"{(int)playingTimer}", new Vector2(5, 10), Color.White);
                    if (instructions == false)
                    {
                        spriteBatch.DrawString(saveSelectFont, "left and right arrow keys to change location\n" +
                        "press and hold spacebar to charge your throw", new Vector2(50, 10), Color.White);
                    }

                    break;
                case GameState.End:
                    if (endTimer <= 0)
                    {
                        spriteBatch.DrawString(saveSelectFont, "Press space to \n" +
                            "return to UserSelection", new Vector2(400, 20), Color.White);
                    }
                    spriteBatch.DrawString(titleScreenFont, $"score for this run: {endScore}", new Vector2(GameWorld.Instance.Graphics.PreferredBackBufferWidth / 2 - 200, 650), Color.White);
                    spriteBatch.DrawString(saveSelectFont, $"User:{highscoreUserId[0]} with {highscoreScore[0]} Points \n" +
                                                $"User:{highscoreUserId[1]} With {highscoreScore[1]} Points\n" +
                                                $"User:{highscoreUserId[2]} With {highscoreScore[2]} Points\n" +
                                                $"User:{highscoreUserId[3]} With {highscoreScore[3]} Points\n" +
                                                $"User:{highscoreUserId[4]} With {highscoreScore[4]} Points\n", new Vector2(GameWorld.Instance.Graphics.PreferredBackBufferWidth / 2 - 300, 150), Color.White);



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

                var cmd = new SQLiteCommand($"SELECT Score FROM highscore WHERE UserId={i}", GameWorld.Instance.connection);
                var dataread = cmd.ExecuteReader();

                while (dataread.Read())
                {
                    userHighscore[i - 1] = dataread.GetInt32(0);
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

                GameWorld.Instance.Instantiate(player);

                playerMade = true;
            }


        }


        public List<highscore> ReadHighscores()
        {
            Open();

            List<highscore> highscores = new List<highscore>();


            var cmd = new SQLiteCommand($"SELECT * FROM highscore ORDER BY Score DESC LIMIT 5;", GameWorld.Instance.connection);
            cmd.ExecuteNonQuery();
            var dataread = cmd.ExecuteReader();
            while (dataread.Read())
            {

                highscore highscore = new highscore();
                highscore.id = dataread.GetInt32(0);
                highscore.UserId = dataread.GetInt32(1);
                highscore.Score = dataread.GetInt32(2);

                highscores.Add(highscore);
            }

            Close();
            return highscores;
        }

    }
    public class highscore
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
    }
}
