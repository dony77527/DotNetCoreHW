using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HomeWork1.Models
{
    public partial class ContosoUniversityContext : DbContext
    {
        public override int SaveChanges()
        {
            var changeSet = ChangeTracker.Entries();
            if(changeSet != null){
                foreach (var dbEntity in changeSet.Where(p => p.State == EntityState.Added || p.State ==EntityState.Modified ))
                {   
                    if(dbEntity.Entity is EntityRule.IDateModified)
                    {
                        (dbEntity.Entity as EntityRule.IDateModified).DateModified = System.DateTime.Now;
                    }
                }
            }        
            return base.SaveChanges();
        }
    }
}