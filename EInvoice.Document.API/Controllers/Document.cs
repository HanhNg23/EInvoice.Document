
using EInvoice.Document.Application.Models.Documents;
using EInvoice.Document.Application.UseCase.Document.Queries.GetDocument;
using EInvoice.Document.Application.UseCase.Document.Queries.GetDocumentDetails;
using EInvoice.Document.Application.UseCase.Document.Queries.SearchDocuments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EInvoice.Document.API.Controllers
{
    [Route("api/documents")]
    [ApiController]
    public class Document : ControllerBase
    {
        [HttpGet]
        [Route("{uuid}")]
        public async Task<IResult> GetDocument(ISender sender, [FromRoute] string uuid)
        {
            //if (uuid != invoke.uuid) return Results.BadRequest();
            await sender.Send(new GetDocumentById() { uuid = uuid});
            return Results.NotFound();
        }


        [HttpGet]
        [Route("{uuid}/details")]
        public async Task<IResult> GetDocumentDetails(ISender sender, [FromRoute]  string uuid)
        {
            //if (uuid != invoke.uuid) return Results.BadRequest();
            await sender.Send(new GetDocumentDetailsById() { uuid = uuid});
            return Results.NotFound();
        }


        [HttpGet]
        [Route("search")]
        public async Task<SearchDocumentResult> SearchDocument(ISender sender, [FromQuery] SearchDocument invoke)
        {
            return await sender.Send(invoke);
        }



    }
}
