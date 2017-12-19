﻿
namespace LDAP
{
    public class LDAPUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        //Ajoute pays

        public LDAPUser(string firstname, string lastname, string username, string password, string email, string position, string address)
        {
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Password = password;
            Email = email;
            Position = position;
            Address = address;
        }
    }
}