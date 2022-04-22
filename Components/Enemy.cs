using Microsoft.Xna.Framework;

namespace DatabaseProjekt
{
    public class Enemy : Component
    {
        private float speed;
        private Vector2 velocity;

        public Enemy(float _speed, Vector2 _velocity)
        {
            speed = _speed;
            velocity = new Vector2(_velocity.X, _velocity.Y);
        }
    }
}
