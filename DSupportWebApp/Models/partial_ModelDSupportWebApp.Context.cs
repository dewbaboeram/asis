namespace DSupportWebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    public interface IAsisObject
    {
        Type GetAsisObjectModelType();
        object FindAsisObjectModel(string controllerName, int id);
        int IDUser { get; }
        object currentRecord { get; set; }
        object previousRecord { get; set; }
    }

    public partial class dsupportwebappEntities
    {
        public IAsisObject asisObject { get; set; }
            
        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(p => p.Entity.GetType().Name != "asis_tablelog" &&
                p.Entity.GetType().Name != "asis_tablelogchange" &&
                (p.State == EntityState.Added || p.State == EntityState.Modified || p.State == EntityState.Deleted)).ToList();
            var now = DateTime.UtcNow;
            
            foreach (var change in modifiedEntities)
            {
                var entityName = change.Entity.GetType().Name;               
                asisObject.currentRecord = change.Entity;
                
                switch (change.State)
                {
                    case EntityState.Added:
                        //asisObject.IDAttRecordOperation = 3;
                        break;

                    case EntityState.Deleted:
                        //asisObject.IDAttRecordOperation = 2;
                        AsisModelHelper.CreateDeleteLog(asisObject.currentRecord, asisObject.IDUser, entityName);
                        break;

                    case EntityState.Modified:
                        //asisObject.IDAttRecordOperation = 1;
                        AsisModelHelper.CreateChangeLog(asisObject.previousRecord, asisObject.currentRecord , asisObject.IDUser, entityName);
                        break;
                }
            }

            return base.SaveChanges();
        }

    }
}