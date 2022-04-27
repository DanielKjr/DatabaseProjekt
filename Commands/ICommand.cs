namespace DatabaseProjekt
{
    public interface ICommand
    {
        void Execute(Player player);

        void CastOutMeter(Player player);
    }
}
