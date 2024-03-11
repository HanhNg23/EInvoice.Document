using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoice.Document.Application.Exceptions
{
    public class InvokeApiFailException : Exception
    {
        public InvokeApiFailException(string message) : base(message)
        {

        }

    }
}
