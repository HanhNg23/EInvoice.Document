using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoice.Document.Application.Interfaces
{
    public interface IUser
    {
        public string? UserId { get; }
        public Task<string?> AccessToken { get; }
    }
}
