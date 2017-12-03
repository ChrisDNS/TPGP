using System.Collections.Generic;

namespace TPGP.Models.Jobs
{
    public class User
    {
        public long Id { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Level { get; set; }

        public User()
        {
        }

        public User(long id, string firstname, string lastname, string username, string emailAddress, 
                    string passwordHash, string passwordSalt, string level)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            EmailAddress = emailAddress;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Level = level;
        }

        public override bool Equals(object obj)
        {
            var user = obj as User;

            return user != null &&
                   Id == user.Id &&
                   Firstname == user.Firstname &&
                   Lastname == user.Lastname &&
                   Username == user.Username &&
                   EmailAddress == user.EmailAddress &&
                   PasswordHash == user.PasswordHash &&
                   PasswordSalt == user.PasswordSalt &&
                   Level == user.Level;
        }

        public override int GetHashCode()
        {
            var hashCode = -308751961;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Firstname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Lastname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Username);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(EmailAddress);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PasswordHash);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PasswordSalt);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Level);

            return hashCode;
        }
    }
}