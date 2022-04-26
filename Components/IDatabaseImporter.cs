namespace DatabaseProjekt
{
    public interface IDatabaseImporter
    {
        /// <summary>
        /// Open connection to databse
        /// </summary>
        void Open();
        /// <summary>
        /// Close connection to database
        /// </summary>
        void Close();
        /// <summary>
        /// Returns texture from database
        /// </summary>
        void GetTexture();
        /// <summary>
        /// Returns the Id
        /// </summary>
        /// <param name="type"></param>
        void GetId(string type);

    }
}
