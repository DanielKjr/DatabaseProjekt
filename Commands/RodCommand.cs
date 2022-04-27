using Microsoft.Xna.Framework;

namespace DatabaseProjekt
{
    public class RodCommand : ICommand
    {
        private Vector2 velocity;
        private double power;

        public RodCommand(double power)
        {
            this.power += power;

        }



        public void CastOutMeter(Player player)
        {

            player.CastOutMeter(power);
           

        }

        public void Execute(Player player)
        {
            // player.Move(velocity);

            player.CastOut();
           
        }
    }
}
