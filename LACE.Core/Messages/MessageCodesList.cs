using Nedesk.Extensions;
using Nedesk.MessagesCodes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Messages
{
    public static class MessageCodesList
    {

        public static MessageCode Get(string key)
        {
            return _messages.Find(x => x.Key == key)?.Clone() ?? throw new InvalidProgramException("Key not found.");
        }

        private static List<MessageCode> _messages = new List<MessageCode>()
        {
            new MessageCode
            {
                Key = "LCEAUTH001",
                DefaultMessage = "User Not Found.",
                Description = "User could not be found."
            },
            new MessageCode
            {
                Key = "LCEAUTH002",
                DefaultMessage = "Passwords don't match.",
                Description = "Password and Confirm Password do not match."
            },
            new MessageCode
            {
                Key = "LCEAUTH003",
                DefaultMessage = "Can't update password since don't match with the previous one.",
                Description = "Password and Confirm Password do not match."
            },
        };

        public static IReadOnlyCollection<MessageCode> Messages
        {
            get
            {
                string[] keys = _messages.Select(x => x.Key).ToArray();

                foreach(string key in keys)
                {
                    if (_messages.FindAll(x => x.Key == key).Count() > 1)
                    {
                        throw new InvalidProgramException("Duplicated keys");
                    }
                }

                return _messages;
            }
        } 
    }
}
