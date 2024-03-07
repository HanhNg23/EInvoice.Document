using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoice.Document.Application.Security.Policies
{
    public static class Policy
    {
        public const string HasConfirmedEmail = "RequireEmailConfirmed";
        public const string SelfOrAdmin = "SelfOrAdmin";
    }
}
