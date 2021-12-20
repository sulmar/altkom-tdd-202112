using System;

namespace TestApp
{
    public class Rent
    {
        public User Rentee { get; set; }

        public Rent(User rentee)
        {
            if (rentee == null)
                throw new ArgumentNullException();

            this.Rentee = rentee;
        }

        public bool CanReturn(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            if (user.IsAdmin)
                return true;

            if (Rentee == user)
                return true;

            return false;
        }

        public void Return(User user)
        {
            throw new NotImplementedException();
        }

    }


    public class User
    {
        public bool IsAdmin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
