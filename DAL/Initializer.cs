using System.Data.Entity;

namespace DAL
{
    internal class Initializer : CreateDatabaseIfNotExists<MonopolyDbContext>
    {
        protected override void Seed(MonopolyDbContext context)
        {
            base.Seed(context);

            
            context.SaveChanges();
        }
    }
}