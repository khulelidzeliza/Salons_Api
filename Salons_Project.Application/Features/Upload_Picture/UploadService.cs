using Microsoft.AspNetCore.Http;

namespace Salons_Project.Application.Features.Upload_Picture;

public class UploadService : IUploadService
{
    private const string RootFolder = "uploads";
 
    public async Task<List<string>> UploadAsync(List<IFormFile>? files, string folder, CancellationToken ct = default)
    {
        var paths = new List<string>();
 
        if (files is null || files.Count == 0) return paths;
 
        var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), RootFolder, folder);
        Directory.CreateDirectory(uploadDir);
 
        foreach (var file in files)
        {
            if (file.Length == 0) continue;
 
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(uploadDir, fileName);
 
            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream, ct);
 
            paths.Add($"/{RootFolder}/{folder}/{fileName}");
        }
 
        return paths;
    }
 
    public void Delete(IEnumerable<string>? paths)
    {
        if (paths is null) return;
 
        foreach (var path in paths)
        {
            if (string.IsNullOrWhiteSpace(path)) continue;
 
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), path.TrimStart('/'));
 
            if (File.Exists(fullPath)) File.Delete(fullPath);
        }
    }
}