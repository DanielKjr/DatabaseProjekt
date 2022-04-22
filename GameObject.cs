using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DatabaseProjekt
{
    public class GameObject
    {
        public Transform Transform { get; private set; } = new Transform();
        private List<Component> components = new List<Component>();
        public string Tag { get; set; }

        /// <summary>
        /// Adds a Component to the GameObject and the components list
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public Component AddComponent(Component component)
        {
            component.GameObject = this;
            components.Add(component);

            return component;
        }

        /// <summary>
        /// Returns the specified Component
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Component GetComponent<T>() where T : Component
        {
            return components.Find(x => x.GetType() == typeof(T));
        }

        public void Awake()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Awake();
            }
        }

        public void Start()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Start();
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Adds a clone of a component and returns GameObject
        /// </summary>
        /// <returns></returns>
        public Object Clone()
        {
            GameObject go = new GameObject();

            foreach (Component c in components)
            {
                go.AddComponent(c.Clone() as Component);
            }

            return go;
        }
    }

}
