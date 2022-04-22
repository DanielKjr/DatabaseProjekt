using Microsoft.Xna.Framework;

namespace DatabaseProjekt
{
    public class MoveCommand : ICommand
    {
        private Vector2 velocity;

        public MoveCommand(Vector2 _velocity)
        {
            velocity = _velocity;
        }

        public void Execute(Player player)
        {
            player.Move(velocity);
        }
    }
}
