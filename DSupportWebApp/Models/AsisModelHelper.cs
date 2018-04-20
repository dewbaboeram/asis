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

        public static string GetViewBagLabel(dynamic ViewBag, string defaultValue, string IDSort)
        {
            var translate = ViewBag.Translation as List<asis_controllerviewitem>;
            if (translate != null && translate.Count() > 0)
            {
                var result = translate.Where(n => n.SortID == IDSort).SingleOrDefault();
                if (result != null)
                {
                    return GetFieldValue("Name", result) as string;
                }
            }
            return defaultValue;
        }

        public static string GetDisplayColumnName(int IDDisplay)
        {
            var label = db.asis_tablelistdisplay.Where(i => i.IDAsisTableListDisplay == IDDisplay).
                Select(l => l).SingleOrDefault();

            return GetDisplayColumnValue(label.FieldName, label) as string;
        }

        internal static object GetDisplayColumnValue(string v, object asisObject)
        {
            var propName = AsisModelHelper.GetFieldName("Display");
            var propObject = asisObject;
            var propInfo = propObject.GetType().GetProperty(propName);
            var result = propInfo.GetValue(propObject);

            return result;
        }
        public static string GetAsisLanguageCode()
        {
            //IDLanguage=1 in tabel asis_language waarin de gegevens voor de standaardtaal zijn opgenomen: NL en nl-NL
            var IDLanguageCode = Convert.ToInt32(db.asis_param.Where(i => i.IDParam == 1).Select(l => l.Value).SingleOrDefault());
            //asis_language.code=NL
            var AsisLangCode = db.asis_language.Where(i => i.IDAsisLanguage == IDLanguageCode).Select(l => l.Code).SingleOrDefault();
            return AsisLangCode;
        }

        internal static object GetFieldValue(string v, object asisObject)
        {
            var propName = AsisModelHelper.GetFieldName(v);
            var propObject = asisObject;
            var propInfo = propObject.GetType().GetProperty(propName);
            var result = propInfo.GetValue(propObject);

            return result;
        }

        internal static string GetFieldName(string v)
        {
            var result = v + "_" + HttpContext.Current.Session["asisLangCode"];
            return result;

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
            var fieldName = string.Format("Name_{0}", asisLangCode);
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

            var IDRecord = Convert.ToInt32(currentRecord.GetType().GetProperties()[0].GetValue(currentRecord));
            int IDAsisTableLog = SaveAsisTablelistLog(IDAsisTableList, IDRecord, Convert.ToInt32(HttpContext.Current.Session["IDUser"]), 1, DateTime.Now);
           
            //int IDAttRecordOperation = 2;
            DateTime DateOperation = DateTime.Now;
            string lblPrevious = AsisModelHelper.GetMessage(2, "asis", Convert.ToString(HttpContext.Current.Session["asisLangCode"])) + ": ";
            string lblCurrent = AsisModelHelper.GetMessage(3, "asis", Convert.ToString(HttpContext.Current.Session["asisLangCode"])) + ": ";
            string LF = "<br>";

            string strHistory = "";

            var index = 0; 
            foreach (PropertyInfo prop in currentRecord.GetType().GetProperties())
            {
                var fieldName = prop.Name;
                var afterfieldValue = Convert.ToString(prop.GetValue(currentRecord));
                var fieldValue = Convert.ToString(prop.GetValue(currentRecord));

                if (index == 0)
                {
                    IDRecord = Convert.ToInt32(fieldValue);
                }

                if (!SaveAsisTablelistLogchange(IDAsisTableLog, fieldName, "", afterfieldValue, DateTime.Now))
                {
                    return false;
                }
                strHistory += string.Format(lblCurrent, fieldName, fieldValue);
                strHistory += LF;
                index++;
            }

            //if (!SaveHistory(IDAsisTableList, IDRecord, Convert.ToInt32(HttpContext.Current.Session["IDUser"]), 2, DateTime.Now, strHistory))
            //{
            //    return false;
            //}


            return true;
        }

        public static bool CreateChangeLog(object previousRecord, object currentRecord,  int IDUser, string tableName)
        {
            
            int IDAsisTableList = GetAsisTableID(tableName);
            if (!HasHistory(IDAsisTableList))
            {
                return false; //deze tabel heeft geen history, ER UIT!!!!!
            }

            var IDRecord = Convert.ToInt32(currentRecord.GetType().GetProperties()[0].GetValue(currentRecord));  
            int IDAsisTableLog = SaveAsisTablelistLog(IDAsisTableList, IDRecord, Convert.ToInt32(HttpContext.Current.Session["IDUser"]), 1, DateTime.Now);

            //int IDAttRecordOperation = 1;
            DateTime DateOperation = DateTime.Now;
            string lblPrevious = AsisModelHelper.GetMessage(2, "asis", Convert.ToString(HttpContext.Current.Session["asisLangCode"])) + ": ";
            string lblCurrent = AsisModelHelper.GetMessage(3, "asis", Convert.ToString(HttpContext.Current.Session["asisLangCode"])) + ": ";
            string LF = "<br>";

            string strHistory = "";

            var index = 0;;
            foreach (PropertyInfo prop in currentRecord.GetType().GetProperties())
            {
                PropertyInfo propPrev = previousRecord.GetType().GetProperties()[index];
                var fieldName = propPrev.Name;
                var beforeFieldValue = Convert.ToString(propPrev.GetValue(previousRecord));
                var afterfieldValue = Convert.ToString(prop.GetValue(currentRecord));

                bool isModified = (beforeFieldValue != afterfieldValue);
                if (isModified)
                {
                    strHistory += string.Format(lblPrevious, fieldName, beforeFieldValue);
                    strHistory += LF;
                    strHistory += string.Format(lblCurrent, fieldName, afterfieldValue);
                    strHistory += LF;

                    if (!SaveAsisTablelistLogchange(IDAsisTableLog, fieldName, beforeFieldValue, afterfieldValue, DateTime.Now))
                    {
                        return false;
                    }

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
            var hasHistory = db.asis_tablelist.Where(i => i.IDAsisTableList == IDAsisTableList &&
            i.HasHistory
            ).Select(l => l.HasHistory).Any();
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

        private static int SaveAsisTablelistLog(int IDAsisTableList, int RecordID, int IDUser, int IDAttRecordOperation, DateTime dateOperation)
        {
            bool saved = false;
            var result = db.asis_tablelog.Add(new asis_tablelog
            {
                IDAsisTableList = IDAsisTableList,
                RecordID = RecordID,
                IDUser = IDUser,
                IDAttRecordOperation = IDAttRecordOperation,
                DateOperation = dateOperation
            });

            saved = db.SaveChanges() > 0;

            return result.IDAsisTableLog;
        }

        private static bool SaveAsisTablelistLogchange(int IDAsisTableLog, string fieldName, string beforeChange, string afterChange, DateTime dateOperation)
        {
            bool saved = false;
            var result = db.asis_tablelogchange.Add(new asis_tablelogchange
            {
                IDAsisTableLog = IDAsisTableLog,
                DateTimeOperation = dateOperation,
                FieldName = fieldName,
                BeforeChange= beforeChange,
                AfterChange = afterChange

            });

            saved = db.SaveChanges() > 0;

            return saved;
        }

    }

}