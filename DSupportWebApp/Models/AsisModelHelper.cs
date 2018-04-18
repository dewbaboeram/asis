using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DSupportWebApp.Models
{
    public class AsisModelHelper
    {
        private static dsupportwebappEntities db = new dsupportwebappEntities();

        public static string GetAsisLanguageCode()
        {
            //IDLanguage=1 in tabel asis_language waarin de gegevens voor de standaardtaal zijn opgenomen: NL en nl-NL
            var IDLanguageCode = Convert.ToInt32(db.asis_param.Where(i => i.IDParam == 1).Select(l => l.Value).SingleOrDefault());
            //asis_language.code=NL
            var AsisLangCode = db.asis_language.Where(i => i.IDAsisLanguage == IDLanguageCode).Select(l => l.Code).SingleOrDefault();
            return AsisLangCode;
        }

        public static string GetAsisLanguageCulture()
        {
            //IDLanguage=1 in tabel asis_language waarin de gegevens voor de standaardtaal zijn opgenomen: NL en nl-NL
            var IDLanguage = Convert.ToInt32(db.asis_param.Where(i => i.IDParam == 1).Select(l => l.Value).SingleOrDefault());
            //asis_language.culture=nl-NL
            var AsisLanguageCulture = db.asis_language.Where(i => i.IDAsisLanguage == IDLanguage).Select(l => l.Culture).SingleOrDefault();
            return AsisLanguageCulture;
        }

        public static string GetMessage(int IDMessage, string prefix, string asisLangCode, object arg0 = null) //arg0 als de message aan format heeft die een waarde vervangt
        {
            var tableName = string.Format("{0}_message", prefix);
            var fieldName = string.Format("Name{0}", asisLangCode);
            var sql = string.Format("select * from {1} where IDMessage={2}", fieldName, tableName, IDMessage);
            var par = new object[] { 0 };
            var data = db.Database.SqlQuery(Type.GetType(string.Format("{0}.Models.{1}_message", HttpContext.Current.Session["AppName"], prefix)), sql, par).ToListAsync();

            var propObject = data.Result[0];
            var propInfo = propObject.GetType().GetProperty(fieldName);
            var message = propInfo.GetValue(propObject);

            var msgOutput = "";
            if (arg0 != null)
                msgOutput = string.Format(message.ToString(), arg0);
            else
                msgOutput = message.ToString();

            return msgOutput;
        }

        public static object GetDataSet(string strSQL)
        {
            var errorMessage = string.Empty;
            object resultDS = null;
            try
            {
                var par = new object[] { 0 };
                var data = db.Database.SqlQuery(typeof(object), strSQL, par).ToListAsync();
                if (data != null && data.Result == null)
                {
                    throw new Exception(GetMessage(4, "asis", Convert.ToString(HttpContext.Current.Session["asisLangCode"]))); //Er is geen recordset als resultaat
                }
                resultDS = data;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    errorMessage = string.Format(GetMessage(5, "asis", Convert.ToString(HttpContext.Current.Session["asisLangCode"])), e.InnerException.Message);
                }
                else
                {
                    errorMessage = string.Format(GetMessage(5, "asis", Convert.ToString(HttpContext.Current.Session["asisLangCode"])), e.Message);
                }
                return errorMessage;
            }
            return resultDS;
        }

        public static object GetAsisFieldName(object model, string fieldName)
        {
            //var propObject = data.Result[0];
            //var propInfo = propObject.GetType().GetProperty(fieldName);
            //var message = propInfo.GetValue(propObject);


            return fieldName;
        }

        public static int GetIDUserGroup(int IDUser)
        {
            //IDLanguage=1 in tabel asis_language waarin de gegevens voor de standaardtaal zijn opgenomen: NL en nl-NL
            var IDUserGroup = Convert.ToInt32(db.user_list.Where(i => i.IDUser == IDUser).Select(l => l.IDUserGroup).SingleOrDefault());
            return IDUserGroup;
        }


        public static bool CreateDeleteLog(object currentRecord, int IDUser, string tableName)
        {

            int IDAsisTableList = GetAsisTableID(tableName);
            if (!HasHistory(IDAsisTableList))
            {
                return false; //deze tabel heeft geen history, ER UIT!!!!!
            }

            //int IDUser = 1;
            //int IDAttRecordOperation = 1;
            DateTime DateOperation = DateTime.Now;
            string lblPrevious = AsisModelHelper.GetMessage(2, "asis", Convert.ToString(HttpContext.Current.Session["asisLangCode"])) + ": ";
            string lblCurrent = AsisModelHelper.GetMessage(3, "asis", Convert.ToString(HttpContext.Current.Session["asisLangCode"])) + ": ";
            string LF = "<br>";

            string strHistory = "";

            var index = 0;
            int IDRecord = 0;
            foreach (PropertyInfo prop in currentRecord.GetType().GetProperties())
            {
                //PropertyInfo propPrev = previousRecord.GetType().GetProperties()[index];
                //var prevFieldName = propPrev.Name;
                //var prevFieldValue = Convert.ToString(propPrev.GetValue(previousRecord));

                var fieldName = prop.Name;
                var fieldValue = Convert.ToString(prop.GetValue(currentRecord));
                if (index == 0)
                {
                    IDRecord = Convert.ToInt32(fieldValue);
                }

                    strHistory += string.Format(lblCurrent, fieldName, fieldValue);
                    strHistory += LF;
                index++;
            }

            if (!SaveHistory(IDAsisTableList, IDRecord, Convert.ToInt32(HttpContext.Current.Session["IDUser"]), 1, DateTime.Now, strHistory))
            {
                return false;
            }
            return true;
        }

        public static bool CreateChangeLog(object previousRecord, object currentRecord,  int IDUser, string tableName)
        {

            int IDAsisTableList = GetAsisTableID(tableName);
            if (!HasHistory(IDAsisTableList))
            {
                return false; //deze tabel heeft geen history, ER UIT!!!!!
            }

            //int IDUser = 1;
            //int IDAttRecordOperation = 1;
            DateTime DateOperation = DateTime.Now;
            string lblPrevious = AsisModelHelper.GetMessage(2, "asis", Convert.ToString(HttpContext.Current.Session["asisLangCode"])) + ": ";
            string lblCurrent = AsisModelHelper.GetMessage(3, "asis", Convert.ToString(HttpContext.Current.Session["asisLangCode"])) + ": ";
            string LF = "<br>";

            string strHistory = "";

            var index = 0;
            int IDRecord = 0;
            foreach (PropertyInfo prop in currentRecord.GetType().GetProperties())
            {
                PropertyInfo propPrev = previousRecord.GetType().GetProperties()[index];
                var prevFieldName = propPrev.Name;
                var prevFieldValue = Convert.ToString(propPrev.GetValue(previousRecord));

                var fieldName = prop.Name;
                var fieldValue = Convert.ToString(prop.GetValue(currentRecord));
                if (index == 0)
                {
                    IDRecord = Convert.ToInt32(fieldValue);
                }

                bool isModified = (fieldValue != prevFieldValue);
                if (isModified)
                {
                    strHistory += string.Format(lblPrevious, prevFieldName, prevFieldValue);
                    strHistory += LF;
                    strHistory += string.Format(lblCurrent, fieldName, fieldValue);
                    strHistory += LF;
                }

                index++;
            }

            if (!SaveHistory(IDAsisTableList, IDRecord, Convert.ToInt32(HttpContext.Current.Session["IDUser"]), 1, DateTime.Now, strHistory))
            {
                return false;
            }
            return true;
        }

        private static int GetAsisTableID(string tableName)
        {
            var IDTableList = db.asis_tablelist.Where(i => i.TableName == tableName).Select(l => l.IDAsisTableList).SingleOrDefault();
            return IDTableList;
        }

        private static bool HasHistory(int IDAsisTableList)
        {
            var hasHistory = db.asis_tablelist.Where(i => i.IDAsisTableList == IDAsisTableList).Select(l => l.HasHistory).Any();
            return hasHistory;
        }
        
        private static bool SaveHistory(int IDAsisTableList, int RecordID, int IDUser, int IDAttRecordOperation, DateTime dateOperation, string strHistory)
        {
            bool saved = false;
            db.asis_tablelog.Add(new asis_tablelog
            {
                IDAsisTableList = IDAsisTableList,
                RecordID = RecordID,
                IDUser = IDUser,
                IDAttRecordOperation = IDAttRecordOperation,
                DateOperation = dateOperation,
                Logchange = strHistory
            });

            saved = db.SaveChanges() > 0;

            return saved;
        }

    }

}