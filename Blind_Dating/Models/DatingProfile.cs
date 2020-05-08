using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blind_Dating.Models
{
    public partial class DatingProfile
    {
        public DatingProfile()
        {
            MailMessagesFromProfile = new HashSet<MailMessages>();
            MailMessagesToProfile = new HashSet<MailMessages>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string Bio { get; set; }
        public string UserAccountId { get; set; }
        [Required]
        public string DisplayName { get; set; }
        public string PhotoPath { get; set; }

        public ICollection<MailMessages> MailMessagesFromProfile { get; set; }
        public ICollection<MailMessages> MailMessagesToProfile { get; set; }
    }
}
