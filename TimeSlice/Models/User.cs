using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSlice.Models
{
    public class User
    {
        public String Firstname { get; set; }
        public String Lastname { get; set; }

        [Required]
        public String Username { get; set; }

        public int Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        public int Roleid { get; set; }

        public User(int _id, String _username, String _password, String _firstname, String _lastname, int _roleid)
        {
            Username = _username;
            Id = _id;
            Password = _password;
            Firstname = _firstname;
            Lastname = _lastname;
            Roleid = _roleid;
        }

    }
}
