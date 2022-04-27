using Microsoft.Xna.Framework;
using System.Data.SQLite;

namespace DatabaseProjekt
{

    public class Player : Component, IDatabaseImporter
    {
        private float speed;
        private int userID;
        private Animator animator;
        private int score;


        public int UserID { get => userID; set => userID = value; }

        public void Move(Vector2 _velocity)
        {
            if (_velocity != Vector2.Zero)
            {
                _velocity.Normalize();
            }

            _velocity *= speed;

            GameObject.Transform.Translate(_velocity * GameWorld.DeltaTime);
        }

        public override void Awake()
        {
            speed = 200;
        }


        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            // sr.SetSprite("Insert sprite path here");
            sr.SetSprite("MinerTest");
            GameObject.Transform.Position = new Vector2(GameWorld.Instance.Graphics.PreferredBackBufferWidth / 2, GameWorld.Instance.Graphics.PreferredBackBufferHeight - sr.Sprite.Height / 2);
            animator = (Animator)GameObject.GetComponent<Animator>();
        }

        public override void Update(GameTime gameTime)
        {
            InputHandler.Instance.Execute(this);
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
            throw new System.NotImplementedException();
        }

        public void GetId(string type)
        {
            throw new System.NotImplementedException();
        }

        public void GetAttributes()
        {
            Open();
            //indsæt værdier før det køres
            var cmd = new SQLiteCommand("SELECT attributes FROM table", GameWorld.Instance.connection);
            var dataread = cmd.ExecuteReader();

            while (dataread.Read())
            {
               
            }

            Close();
        }

        /// <summary>
        /// Adds a highscore entry with UserId and their Score.
        /// </summary>
        /// <param name="score"></param>
        public void SaveHighScore(int score)
        {
            Open();

            var cmd = new SQLiteCommand($"INSERT INTO highscore (Id, Score) VALUES ({userID}, {score})", GameWorld.Instance.connection);
            cmd.ExecuteNonQuery();

            Close();
        }
    }
}
