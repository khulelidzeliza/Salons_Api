using Microsoft.AspNetCore.Http;

namespace Salons_Project.Application.Features.Upload_Picture;

public interface IUploadService
{
    Task<List<string>> UploadAsync(List<IFormFile>? files, string folder, CancellationToken ct = default);

    void Delete(IEnumerable<string>? paths);
}
