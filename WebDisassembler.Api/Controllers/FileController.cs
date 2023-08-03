using Microsoft.AspNetCore.Mvc;
using WebDisassembler.Core.Application.Services;
using WebDisassembler.Core.Identity;

namespace WebDisassembler.Api.Controllers;

[ApiController, Route("files")]
public class FileController : ControllerBase
{
    private readonly FileService _fileService;
    private readonly IdentityDetails _identityDetails;

    public FileController(FileService fileService, IdentityDetails identityDetails)
    {
        _fileService = fileService;
        _identityDetails = identityDetails;
    }

    [HttpPost("temporary/upload")]
    public async ValueTask<Guid> UploadTemporaryFile(IFormFile file)
    {
        return await _fileService.UploadTemporaryFile(_identityDetails.UserId!.Value, file.OpenReadStream(), file.FileName);
    }
}
