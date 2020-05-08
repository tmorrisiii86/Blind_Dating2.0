using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blind_Dating.Models;

namespace Blind_Dating.ViewModels
{
    public class InboxViewModel
    {
        public IEnumerable<MailMessages> mailMessages;
        public IEnumerable<DatingProfile> fromProfiles;
    }
}
