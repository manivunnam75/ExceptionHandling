using Microsoft.EntityFrameworkCore;

namespace ExceptionHandlingAssignment.Repository
{
    public class ExceptionContext : DbContext 
    {
        public ExceptionContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
