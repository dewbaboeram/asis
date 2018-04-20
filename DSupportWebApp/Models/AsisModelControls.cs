using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DSupportWebApp.Models
{
    public class AsisModelControls
    {

        private static dsupportwebappEntities db = new dsupportwebappEntities();

        public static string GetRelPersonFullNameFromIDUser(int IDUser)
        {
            var record = (
                from u in db.user_list
                where u.IDUser == IDUser
                from r in db.rel_person
                where r.IDRelPerson == u.IDRelPerson
                select r).SingleOrDefault();
                return record.FirstName + " " + record.MiddleName + " " + record.LastName;
        }

        public static string GetRelPersonFullNameFromIDRelPerson(int IDRelPerson)
        {
            var record = (
                from p in db.rel_person
                where p.IDRelPerson == IDRelPerson
                select p).SingleOrDefault();
            return record.FirstName+ " " + record.MiddleName + " " + record.LastName;
        }

        public static string GetRecordHistory(int IDRecord)
        {

            var fullname = Convert.ToString((
                from t in db.asis_tablelog
                where t.IDAsisTableList == 1 && t.RecordID == IDRecord
                select t.DateOperation));

            return fullname;
        }



    }
}