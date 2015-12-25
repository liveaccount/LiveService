namespace NancyApp
{
    using System;
    
    public class Client
    {
        private readonly Int32 id;
        private readonly String name;

        public Int32 Id
        {
            get { return id; }
        }

        public String Name
        {
            get { return name; }
        }

        public Client(Int32 id, String name)
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