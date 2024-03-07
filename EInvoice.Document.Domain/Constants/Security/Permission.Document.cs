using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoice.Document.Application.Security.Permisstions
{
    public static class Permission
    {
        public const string Get = "get:document";
        public const string Dismiss = "dismiss:document";
        // can set for other permissions: set, approve, reject, etc.
    }
}
