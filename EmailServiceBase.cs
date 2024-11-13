using EmailPortal.Models;
using System;
using System.Collections.Generic;

namespace EmailPortal.Services
{
    public class EmailServiceBase
    {
        internal List<Email> FetchEmailsInProgress()
        {
            throw new NotImplementedException();
        }

        internal List<Email> FetchEmailsProcessed()
        {
            throw new NotImplementedException();
        }
    }
}