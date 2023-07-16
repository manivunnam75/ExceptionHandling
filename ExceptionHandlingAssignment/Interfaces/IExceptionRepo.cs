namespace ExceptionHandlingAssignment.Interfaces
{
    public interface IExceptionRepo
    {
       public bool CheckSqlSyntax();
       public bool CheckSqlConnection();
    }
}
