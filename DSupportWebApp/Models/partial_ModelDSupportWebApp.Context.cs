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
        //int IDAttRecordOperation { get; set; }
        object currentRecord { get; set; }
        object previousRecord { get; set; }
    }

    public partial class dsupportwebappEntities
    {
        public IAsisObject asisObject { get; set; }
            
        public override int SaveChanges()
        {
           
            var modifiedEntities = ChangeTracker.Entries()
                .Where(p => p.Entity.GetType().Name != "asis_tablelog" && (p.State == EntityState.Added || p.State == EntityState.Modified || p.State == EntityState.Deleted)).ToList();
            var now = DateTime.UtcNow;
            
            foreach (var change in modifiedEntities)
            {
                var entityName = change.Entity.GetType().Name;

                //if (entityName == "asis_tablelog")
                //{
                //    continue;
                //}
                
                asisObject.currentRecord = change.Entity;
                
                switch (change.State)
                {
                    case EntityState.Added:
                        //asisObject.IDAttRecordOperation = 3;
                        break;

                    case EntityState.Deleted:
                        //asisObject.IDAttRecordOperation = 2;
                        foreach (var prop in change.OriginalValues.PropertyNames)
                        {
                                AsisModelHelper.CreateChangeLog(asisObject.previousRecord,asisObject.currentRecord, asisObject.IDUser, entityName);
                        }
                        break;

                    case EntityState.Modified:
                        //asisObject.IDAttRecordOperation = 1;
                        AsisModelHelper.CreateChangeLog(asisObject.previousRecord, asisObject.currentRecord , asisObject.IDUser, entityName);


                        //foreach (var prop in change.OriginalValues.PropertyNames)
                        //{
                        ////    if (prop == "IDUserModified")
                        ////    {
                        ////        prop
                        ////    }
                        //    var originalValue = change.OriginalValues[prop].ToString();
                        //    var currentValue = change.CurrentValues[prop].ToString();
                        //    if (originalValue != currentValue) //Only create a log if the value changes
                        //    {
                        //        //Create the Change Log
                        //        AsisModelHelper.CreateChangeLog(prop, asisObject.IDAttRecordOperation, asisObject.IDUser, asisObject.GetType().Name);
                        //    }
                        //}
                        break;
                }
            }

            return base.SaveChanges();
        }

    }
}