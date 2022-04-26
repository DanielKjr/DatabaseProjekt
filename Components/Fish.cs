using System.Data.SQLite;

public enum FishType
{
    river,
    seafish,
    fjordfish
}
namespace DatabaseProjekt
{
    public class Fish : Component, IDatabaseImporter
    {
        private SQLiteConnection connection = new SQLiteConnection("Data Source=FishingFrenzy.db");
        private int myId;
        private string spritePath;
        private FishType myFishType;
        private string[] myType = new string[]
        { 
            "riverfish", 
            "seafish", 
            "fjordfish" 
        };


        public FishType MyFishType { get => myFishType; set => myFishType = value; }

        public override void Start()
        {

            GetId(myType[(int)MyFishType]);

            GetTexture();

            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            sr.SetSprite(spritePath);

        }

        //<inheritdoc/>
        public void Open()
        {
            connection.Open();
        }

        //<inheritdoc/>
        public void Close()
        {
            connection.Close();
        }

        //<inheritdoc/>
        public void GetId(string type)
        {
            Open();
            var cmd = new SQLiteCommand($"SELECT Id FROM {type} WHERE Species='{GameObject.Tag}'", connection);
            var dataset = cmd.ExecuteReader();

            while (dataset.Read())
            {
                myId = dataset.GetInt32(0);
            }
            Close();
        }

        //<inheritdoc/>
        public void GetTexture()
        {
            Open();

            var cmd = new SQLiteCommand($"SELECT Texture FROM '{myType[(int)myFishType]}' WHERE Id={myId}", connection);
            var dataread = cmd.ExecuteReader();
            while (dataread.Read())
            {
                spritePath = dataread.GetString(0);
            }
            Close();

        }


    }
}
