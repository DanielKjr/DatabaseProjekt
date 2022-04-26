namespace DatabaseProjekt
{
    public class PlayerFactory : Factory
    {
        private static PlayerFactory instance;

        public static PlayerFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new PlayerFactory();

                return instance;
            }
        }
        public override GameObject CreateObject()
        {
            GameObject player = new GameObject();
            player.AddComponent(new Player());
            player.AddComponent(new SpriteRenderer());

            return player;
        }
    }
}
