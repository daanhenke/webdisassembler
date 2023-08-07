using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebDisassembler.Core.Application.Services.Admin;

namespace WebDisassembler.Api.Controllers.Admin;

[ApiController, Route("api/admin/search")]
public class SearchController : ControllerBase
{
    private readonly SearchService _searchService;

    public SearchController(SearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet("reindex/all"), SwaggerOperation(OperationId = nameof(ReindexAll))]
    public async ValueTask<ActionResult> ReindexAll()
    {
        await _searchService.ReindexAll();
        return Ok();
    }
}
