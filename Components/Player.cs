using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Data.SQLite;

namespace DatabaseProjekt
{

    public class Player : Component, IDatabaseImporter
    {
        private double power = 0;
        private int userID;
        private Animator animator;
        private Texture2D rectangleTexture;
        private int score;


        public double Power { get => power; }
        public int UserID { get => userID; set => userID = value; }

        public Rectangle PowerBar
        {
            get
            {
                return new Rectangle(
                    (int)GameObject.Transform.Position.X,
                    (int)GameObject.Transform.Position.Y,
                    50,
                    (int)RodCommand.Power
                    );
            }
            
        }
       
        public void CastOut(double power)
        {
           this.power *= power;

        }


        public override void Awake()
        {
            rectangleTexture = GameWorld.Instance.Content.Load<Texture2D>("Pixel");
        }


        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            // sr.SetSprite("Insert sprite path here");
            sr.SetSprite("MinerTest");
            GameObject.Transform.Position = new Vector2(GameWorld.Instance.Graphics.PreferredBackBufferWidth / 2, GameWorld.Instance.Graphics.PreferredBackBufferHeight - sr.Sprite.Height * 2);
            animator = (Animator)GameObject.GetComponent<Animator>();
            
            
        }

        public override void Update(GameTime gameTime)
        {
            
            InputHandler.Instance.Execute(this);
            
        }

        public void DrawRectangle(SpriteBatch spriteBatch)
        {
           
            spriteBatch.Draw(rectangleTexture, PowerBar, Color.Red);
        }

        #region dbstuff
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

        #endregion
    }
}
