using AskApp.Ask.DAL.Entities;
using AskApp.Common.TOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Ask.DAL.Extensions
{
    public static class AskUserExtensions
    {
        public static AskUserTO ToTransferObject(this AskUserEF askUser)
        {
            if (askUser is null)
                throw new ArgumentNullException(nameof(askUser));

            return new AskUserTO
            {
                Id = askUser.Id,
                FirstName = askUser.FirstName,
                LastName = askUser.LastName,
            };
        }

        public static AskUserEF ToEF(this AskUserTO askUser)
        {
            if (askUser is null)
                throw new ArgumentNullException(nameof(askUser));

            return new AskUserEF
            {
                Id = askUser.Id,
                FirstName = askUser.FirstName,
                LastName = askUser.LastName,
            };
        }
        public static AskUserEF ToTrackedEF(this AskUserTO askUser, AskUserEF askUserToModify)
        {
            if (askUserToModify is null)
                throw new ArgumentNullException(nameof(askUserToModify));
            if (askUser is null)
                throw new ArgumentNullException(nameof(askUser));

            askUserToModify.Id = askUser.Id;
            askUserToModify.FirstName = askUser.FirstName;
            askUserToModify.LastName = askUser.LastName;

            return askUserToModify;

        }
    }
}
