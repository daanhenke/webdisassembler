using Microsoft.AspNetCore.Mvc;
using WebDisassembler.Core.Application.Services;
using WebDisassembler.Core.Identity;

namespace WebDisassembler.Api.Controllers;

[ApiController, Route("api/files")]
public class FileController : ControllerBase
{
    private readonly FileService _fileService;
    private readonly IUserIdentity _userIdentity;

    public FileController(FileService fileService, IUserIdentity userIdentity)
    {
        _fileService = fileService;
        _userIdentity = userIdentity;
    }

    [HttpPost("temporary/upload")]
    public async ValueTask<Guid> UploadTemporaryFile(IFormFile file)
    {
        return await _fileService.UploadTemporaryFile(_userIdentity.UserId, file.OpenReadStream(), file.FileName);
    }
}
