namespace DatabaseProjekt
{
    /// <summary>
    /// Interface for importing database data into the object
    /// </summary>
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
        /// Fetches the Id from database and attaches to the object
        /// </summary>
        /// <param name="type"></param>
        void GetId(string type);

        /// <summary>
        /// Attaches the attributes from database to the object
        /// </summary>
        void GetAttributes();

    }
}
