﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SPF_ClassLib
{
    public class Login
    {

        public Users GetUsers()
        {
            Users result = Helpers.ReadFromXmlFile<Users>(Properties.Settings.Default.UsersXML_path);// ("c:\\users\\ondrej\\source\\repos\\SpravaProjektov\\SPF_ClassLib\\Users.xml");
            return result;
        }

        public List<User> GetUsers(string email, string password)
        {
            Users result = new Users();
            result = GetUsers();
            
            return result.UserList.Where(a => a.Email == email && a.Password == password).ToList();
        }
    }

    [XmlRoot("users")]
    public class Users
    {
        [XmlElement("user")]
        public List<User> UserList { get; set; }
    }

    public class User
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("email")]
        public string Email { get; set; }
        [XmlElement("password")]
        public string Password { get; set; }
    }


}
