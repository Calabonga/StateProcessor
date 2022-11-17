using System.Data.Entity;
using Calabonga.StateManager.ConsoleTest.Entities;
using Calabonga.StatusProcessor;

namespace Calabonga.StateManager.ConsoleTest.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(): base("DatabaseConnection")
        {
            
        }

        public DbSet<Accident> Accidents { get; set; }

    }
}
