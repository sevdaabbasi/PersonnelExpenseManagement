using PersonnelExpenseManagement.Application.Interfaces;

namespace PersonnelExpenseManagement.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly string _uploadDirectory;
    private readonly string[] _allowedExtensions = { ".pdf", ".jpg", ".jpeg", ".png" };
    private const long MaxFileSize = 10 * 1024 * 1024; // 10MB

    public FileService(string uploadDirectory)
    {
        _uploadDirectory = uploadDirectory;
        if (!Directory.Exists(_uploadDirectory))
        {
            Directory.CreateDirectory(_uploadDirectory);
        }
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        if (!IsValidFileType(fileName))
        {
            throw new ArgumentException("Invalid file type");
        }

        if (!IsValidFileSize(fileStream.Length))
        {
            throw new ArgumentException("File size exceeds limit");
        }

        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
        var filePath = Path.Combine(_uploadDirectory, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.CopyToAsync(stream);
        }

        return uniqueFileName;
    }

    public async Task<Stream> GetFileAsync(string filePath)
    {
        var fullPath = Path.Combine(_uploadDirectory, filePath);
        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }

        var memory = new MemoryStream();
        using (var stream = new FileStream(fullPath, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;
        return memory;
    }

    public async Task DeleteFileAsync(string filePath)
    {
        var fullPath = Path.Combine(_uploadDirectory, filePath);
        if (File.Exists(fullPath))
        {
            await Task.Run(() => File.Delete(fullPath));
        }
    }

    public bool IsValidFileType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return _allowedExtensions.Contains(extension);
    }

    public bool IsValidFileSize(long fileSize)
    {
        return fileSize <= MaxFileSize;
    }
} 