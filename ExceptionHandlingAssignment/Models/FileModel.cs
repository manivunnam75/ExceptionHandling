namespace ExceptionHandlingAssignment.Models
{
    public class FileModel
    {
        public IFormFile ExcelFile { get; set; }
        public bool IsEditable { get; set; }
        public bool IsCheck { get; set; }

        public string Option { get; set; }
    }
}
