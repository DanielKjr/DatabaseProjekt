using System.Data.SQLite;
/// <summary>
/// FishType is used to specify which data table the fish is found.
/// The index of the enum is used to match the myType string array in the Fish.cs
/// </summary>
public enum FishType
{
    river,
    sea,
    fjord
}
namespace DatabaseProjekt
{
    public class Fish : Component, IDatabaseImporter
    {
        private SQLiteConnection connection = new SQLiteConnection("Data Source=FishingFrenzy.db");
        private FishType myFishType;

        private int myId;
        private float weight;
        private int depth;
        private string spritePath;
        private string[] myType = new string[]
        {
            "riverfish",
            "seafish",
            "fjordfish"
        };


        public FishType MyFishType { get => myFishType; set => myFishType = value; }
        public float Weight { get => weight; set => weight = value; }
        public int Depth { get => depth; set => depth = value; }


        public override void Start()
        {
            //open db connection
            Open();

            //get Id, Texture and Attributes from db
            GetId(myType[(int)MyFishType]);
            GetTexture();
            GetAttributes();

            //set sprite in SpriteRenderer component
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            sr.SetSprite(spritePath);

            //close db connection
            Close();
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

            var cmd = new SQLiteCommand($"SELECT Id FROM {type} WHERE Species='{GameObject.Tag}'", connection);
            var dataset = cmd.ExecuteReader();

            while (dataset.Read())
            {
                myId = dataset.GetInt32(0);
            }

        }

        //<inheritdoc/>
        public void GetTexture()
        {

            var cmd = new SQLiteCommand($"SELECT Texture FROM '{myType[(int)myFishType]}' WHERE Id={myId}", connection);
            var dataread = cmd.ExecuteReader();
            while (dataread.Read())
            {
                spritePath = dataread.GetString(0);
            }

        }

        //<inheritdoc/>
        public void GetAttributes()
        {
            var cmd = new SQLiteCommand($"SELECT Weight, Depth FROM '{myType[(int)myFishType]}' WHERE Id={myId}", connection);
            var dataread = cmd.ExecuteReader();

            while (dataread.Read())
            {
                weight = dataread.GetFloat(0);
                depth = dataread.GetInt32(1);
            }
        }


    }
}
