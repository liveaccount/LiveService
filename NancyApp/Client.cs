namespace NancyApp
{
    using System;
    
    public class Client
    {
        private readonly int id;
        private readonly string name;

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public Client(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        #region Public Override Methods

        public override string ToString()
        {
            return name;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Client;

            return other != null ? id.Equals(other.id) : false;
        }

        #endregion Public Override Methods
    }
}