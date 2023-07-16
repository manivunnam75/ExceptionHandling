namespace ExceptionHandlingAssignment
{
    public interface IFileProcessing
    {
        bool IsFileFound(string file);
        bool IsExcelFile(IFormFile filename);
        bool InsufficientFilePermissions(IFormFile filename);
    }
}
