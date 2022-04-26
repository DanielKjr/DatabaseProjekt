using Microsoft.Xna.Framework;

namespace DatabaseProjekt
{
    public class FishFactory : Factory
    {
        private static FishFactory instance;

        public static FishFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new FishFactory();

                return instance;
            }
        }


        public override GameObject CreateObject()
        {
            GameObject go = new GameObject();
            go.AddComponent(new Fish());
            go.AddComponent(new SpriteRenderer());
            go.Transform.Position = new Vector2(50, 50);

            

            return go;
        }
    }
}
