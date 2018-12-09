using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proairetiki4
{
    [Serializable]
    public class Contact
    {
        private string name;
        private string surname;
        private string telephone;
        private string email;
        private string address;
        private string birthday;

        public Contact AddName(string name)
        {
            this.name = name;
            return this;
        }

        public Contact AddSurname(string surname)
        {
            this.surname = surname;
            return this;
        }

        public Contact AddTelephone(string telephone)
        {
            this.telephone = telephone;
            return this;
        }

        public Contact AddEmail(string email)
        {
            this.email = email;
            return this;
        }

        public Contact AddAddress(string address)
        {
            this.address = address;
            return this;
        }

        public Contact AddBirthday(string birthday)
        {
            this.birthday = birthday;
            return this;
        }

        public string GetName()
        {
            return name;
        }

        public string GetSurname()
        {
            return surname;
        }

        public string GetTelephone()
        {
            return telephone;
        }

        public string GetEmail()
        {
            return email;
        }

        public string GetAddress()
        {
            return address;
        }

        public string GetBirthday()
        {
            return birthday;
        }

        public override string ToString()
        {
            return name + " " + surname;
        }
    }
}
