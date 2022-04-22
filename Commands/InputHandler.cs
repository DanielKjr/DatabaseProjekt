using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace DatabaseProjekt
{
    public class InputHandler
    {
        private static InputHandler instance;

        public static InputHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputHandler();
                }
                return instance;
            }
        }

        private Dictionary<KeyInfo, ICommand> keybinds = new Dictionary<KeyInfo, ICommand>();

        public InputHandler()
        {
            Player player = (Player)GameWorld.Instance.FindObjectOfType<Player>();

            //example of keybinds
            keybinds.Add(new KeyInfo(Keys.D), new MoveCommand(new Vector2(1, 0)));
            keybinds.Add(new KeyInfo(Keys.W), new MoveCommand(new Vector2(0, -1)));
            keybinds.Add(new KeyInfo(Keys.A), new MoveCommand(new Vector2(-1, 0)));
            keybinds.Add(new KeyInfo(Keys.S), new MoveCommand(new Vector2(0, 1)));
        }

        public void Execute(Player player)
        {
            KeyboardState keyState = Keyboard.GetState();


            foreach (KeyInfo k in keybinds.Keys)
            {
                if (keyState.IsKeyDown(k.Key))
                {
                    keybinds[k].Execute(player);
                    k.IsDown = true;
                }

                if (!keyState.IsKeyDown(k.Key) && k.IsDown == true)
                {

                }
            }
        }
    }

    /// <summary>
    /// KeyInfo can be used for buttonevent controls
    /// </summary>
    public class KeyInfo
    {
        public bool IsDown { get; set; }
        public Keys Key { get; set; }

        public KeyInfo(Keys key)
        {
            this.Key = key;
        }
    }
}
