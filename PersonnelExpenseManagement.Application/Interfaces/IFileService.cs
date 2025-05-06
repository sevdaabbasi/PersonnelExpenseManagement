namespace PersonnelExpenseManagement.Application.Interfaces;

public interface IFileService
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
    Task<Stream> GetFileAsync(string filePath);
    Task DeleteFileAsync(string filePath);
    bool IsValidFileType(string fileName);
    bool IsValidFileSize(long fileSize);
} 