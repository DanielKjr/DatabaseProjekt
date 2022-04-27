using Microsoft.Xna.Framework;

namespace DatabaseProjekt
{
    public class RodCommand : ICommand
    {
        private Vector2 velocity;
        public static double Power { get; set; }

        public RodCommand(double power)
        {
            Power += power;

        }



        public void CastOutMeter(Player player)
        {
            
            if (Power >= -100 )
            {
                Power -= -1d;
            }
           

        }

        public void Execute(Player player)
        {
            // player.Move(velocity);

            player.CastOut(Power);
        }
    }
}
