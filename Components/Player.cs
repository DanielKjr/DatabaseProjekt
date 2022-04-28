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

        private Vector2 castVector;
        private bool hasCast = false;

        public double Power { get => power; }

        public int UserID { get => userID; set => userID = value; }
        public int Score { get => score; set => score = value; }
        public Vector2 CastVector { get => castVector; }

        public Rectangle PowerBar
        {
            get
            {
                return new Rectangle(
                    (int)GameObject.Transform.Position.X+20,
                    (int)GameObject.Transform.Position.Y,
                    20,
                    (int)Power
                    );
            }

        }

        public void CastOut()
        {
            
            castVector.X += (int)power * 9;
            power = 0;
            hasCast = true;

        }

        public void CastOutMeter(double power)
        {
            castVector.X = GameObject.Transform.Position.X;
            hasCast = false;
            if (Power <= 100)
            {
                power *= -10;
            }

            this.power += power;
        }


        public override void Awake()
        {
            rectangleTexture = GameWorld.Instance.Content.Load<Texture2D>("Pixel");
            castVector = new Vector2(GameObject.Transform.Position.X, GameObject.Transform.Position.Y);
        }

        public void DrawLine(SpriteBatch spriteBatch)
        {
            if (hasCast)
            {
                //rod depth 
                for (int i = 0; i < 300; i++)
                {
                   
                    spriteBatch.Draw(rectangleTexture, new Vector2(castVector.X, castVector.Y + i), Color.Black);

                }
            }


        }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            // sr.SetSprite("Insert sprite path here");
            sr.SetSprite("MinerTest");

            GameObject.Transform.Position = new Vector2(50, 50);
            animator = (Animator)GameObject.GetComponent<Animator>();


        }

        public override void Update(GameTime gameTime)
        {

            InputHandler.Instance.Execute(this);

        }

        public void DrawRectangle(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(rectangleTexture, PowerBar, Color.Green);
            if (hasCast)
                spriteBatch.Draw(rectangleTexture, castVector, Color.Black);
            DrawLine(spriteBatch);
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
            var cmd = new SQLiteCommand($"SELECT Score FROM highscore WHERE Id={userID}", GameWorld.Instance.connection);
            var dataread = cmd.ExecuteReader();

            while (dataread.Read())
            {

                score = dataread.GetInt32(0);

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

        public void SaveUserScore(int score)
        {
            Open();

            var cmd = new SQLiteCommand($"INSERT INTO userslots (Id, score) VALUES ({UserID}, { score})", GameWorld.Instance.connection);
            cmd.ExecuteNonQuery();

            Close();
        }


        #endregion

    }
}
