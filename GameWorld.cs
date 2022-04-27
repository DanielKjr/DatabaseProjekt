using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System.Data.SQLite;
using System;
using System.Diagnostics;

namespace DatabaseProjekt
{
    public enum GameState
    {
        SaveSelect,
        TitleScreen,
        ViewScores,
        Playing,
        End
    }
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public SQLiteConnection connection = new SQLiteConnection("Data Source=FishingFrenzy.db");
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<GameObject> newGameObjects = new List<GameObject>();
        private List<GameObject> destroyedGameObjects = new List<GameObject>();
        private GameState _gameState;
        public GameState GameState { get => _gameState; set => _gameState = value; }
        public GraphicsDeviceManager Graphics { get => _graphics; }

        public static float DeltaTime;
        private static GameWorld instance;
        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }

        }



        private GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            CreateDb();

            GameState = GameState.SaveSelect;
            UserInterface.Instance.Start();
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.PreferredBackBufferWidth = 1080;
            _graphics.ApplyChanges();

            //skal flyttes ind i userinterface
            GameObject player = PlayerFactory.Instance.CreateObject();
            Player p = player.GetComponent<Player>() as Player;
            p.UserID = 1;
           
            gameObjects.Add(player);

            Instantiate(SpawnFish(FishType.fjord, "Salmon", 500));


            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Awake();
            }

            base.Initialize();
        }

        public GameObject SpawnFish(FishType type, string species, int depth)
        {
            GameObject fish = FishFactory.Instance.CreateObject();
            Fish f = fish.GetComponent<Fish>() as Fish;
            f.MyFishType = type;
            f.GameObject.Tag = species;
            f.GameObject.Transform.Position =new Vector2(0, depth);


            return fish;
        }

        public void CreateDb()
        {

            if (File.Exists("FishingFrenzy.db") == false)
            {
                string sqlConnectionString = "Data Source=FishingFrenzy.db; new=True";
                var sqlconnection = new SQLiteConnection(sqlConnectionString);
                sqlconnection.Open();
                string cmd = File.ReadAllText("CreateFishingFrenzyDb.sql");
                var SqliteDb = new SQLiteCommand(cmd, sqlconnection);
                SqliteDb.ExecuteNonQuery();
                sqlconnection.Close();

            }



        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Start();
            }


        }

        protected override void Update(GameTime gameTime)
        {
            UserInterface.Instance.Update(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);


            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update(gameTime);
            }


            //adds and removes new objects
            CleanUp();
        }

        //draws all gameobjects
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            UserInterface.Instance.Draw(_spriteBatch);
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw(_spriteBatch);
            
            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Adds GameObjects to the newGameObjects list
        /// </summary>
        /// <param name="go"></param>
        public void Instantiate(GameObject go)
        {
            newGameObjects.Add(go);
        }

        /// <summary>
        /// Adds GameObjects to the destroyedGameObjects list 
        /// </summary>
        /// <param name="go"></param>
        public void Destroy(GameObject go)
        {
            destroyedGameObjects.Add(go);
        }

        /// <summary>
        /// Adds new objects to gameObjects list, runs Awake and Start.
        /// while also removing all the objects that are destroyed before clearing both lists
        /// </summary>
        public void CleanUp()
        {
            for (int i = 0; i < newGameObjects.Count; i++)
            {
                gameObjects.Add(newGameObjects[i]);
                newGameObjects[i].Awake();
                newGameObjects[i].Start();

            }

            for (int i = 0; i < destroyedGameObjects.Count; i++)
            {
                gameObjects.Remove(destroyedGameObjects[i]);
            }

            destroyedGameObjects.Clear();
            newGameObjects.Clear();

        }

        /// <summary>
        /// Returns the specified object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Component FindObjectOfType<T>() where T : Component
        {
            foreach (GameObject gameObject in gameObjects)
            {
                Component c = gameObject.GetComponent<T>();

                if (c != null)
                {
                    return c;
                }
            }

            return null;


        }
    }
}
