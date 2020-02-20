using System;

namespace Models
{
    public class Person
    {
        private string _firstName;
        public Guid Id { get; set; }
        public string FirstName { get { return _firstName; } set { _firstName = value.Trim(); } }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public TimeSpan Age
        {
            get
            {
                if (BirthDate == null)
                {
                    return TimeSpan.MinValue;
                }
                
                return DateTime.Now - BirthDate;
            }
        }

        public override string ToString()
        {
            return (FirstName + " " + LastName).Trim();
        }
    }
}
