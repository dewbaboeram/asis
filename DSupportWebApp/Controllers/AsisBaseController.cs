using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DSupportWebApp.Models;

namespace DSupportWebApp.Controllers
{
    public class AsisBaseController : Controller, IAsisObject
    {
        public bool IsPostBack { get; set; }

        //IAsisObject
        public int IDUser => Convert.ToInt32 (Session["IDUser"]);
        //public int IDAttRecordOperation { get; set; }
        public object previousRecord { get; set; }
        public object currentRecord { get; set;}

        public  dsupportwebappEntities db = new dsupportwebappEntities();

        public AsisBaseController() {
            db.asisObject = this;
        }



        // GET: AsisBase
        public ActionResult Intialize(ActionResult result, string prefix, int IDControllerView)
        {
            //TranslateController(prefix, IDControllerView);
            return CheckAuthorisation(result);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (GetAsisObjectModelType() != null)
            {
                Intialize(null, "asis", 1);

                if (filterContext.ActionParameters.Count > 0)
                {
                    IsPostBack = ((System.Web.HttpRequestWrapper)((System.Web.HttpContextWrapper)filterContext.HttpContext).Request).HttpMethod == "POST";

                    if (!IsPostBack)
                    {
                        switch (filterContext.ActionDescriptor.ActionName)
                        {
                            case "Edit":
                                if (filterContext.ActionParameters.Where(n => n.Key == "id").Any())
                                {
                                    var id = Convert.ToInt32(filterContext.ActionParameters["id"]);
                                    var asis_object = FindAsisObjectModel(GetAsisObjectModelType().Name, id);
                                    if (asis_object != null)
                                    {
                                        BeforeEdit(asis_object);
                                    }
                                }
                                break;

                            case "Delete":
                                if (filterContext.ActionParameters.Where(n => n.Key == "id").Any())
                                {
                                    var id = Convert.ToInt32(filterContext.ActionParameters["id"]);
                                    var asis_object = FindAsisObjectModel(GetAsisObjectModelType().Name, id);
                                    if (asis_object != null)
                                    {
                                        BeforeDelete(asis_object);
                                    }
                                }
                                break;

                                //default:


                        }


                    }
                    else
                    {
                        switch (filterContext.ActionDescriptor.ActionName)
                        {
                            case "Edit":
                                var id = Convert.ToInt32(filterContext.RouteData.Values["id"]);
                                this.previousRecord = Session["prevRecord"]; //FindAsisObjectModel(GetAsisObjectModelType().Name, id);
                                this.currentRecord = filterContext.ActionParameters[GetAsisObjectModelType().Name];
                                break;

                            case "Delete":
                                //var id = Convert.ToInt32(filterContext.RouteData.Values["id"]);
                                //this.previousRecord = Session["prevRecord"]; //FindAsisObjectModel(GetAsisObjectModelType().Name, id);
                                //this.currentRecord = filterContext.ActionParameters[GetAsisObjectModelType().Name];
                                break;

                        }
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }

        private void BeforeDelete(object asis_object)
        {
            //throw new NotImplementedException();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            if (GetAsisObjectModelType() != null)
            {
                if (IsPostBack && filterContext.ActionDescriptor.ActionName == "Edit")
                {
                    //var asis_object = ((System.Web.Mvc.ViewResultBase)filterContext.Result).Model;
                    //if (asis_object != null)
                    //{
                    //    AfterEdit(asis_object, 1, Convert.ToInt32(Session["IDUser"]), filterContext.ActionDescriptor.ControllerDescriptor.ControllerName /*b.v.: "asis_param"*/);
                    //}
                }
            }

            base.OnActionExecuted(filterContext);
        }

        private void TranslateController(string prefix, int IDControllerView)
        {
            //ViewBag.EditText = "Bewerk";
            //ViewBag.DeleteText = "Verwijder";
            //ViewBag.BackToListText = "Terug naar de lijst";
            //object ds = null;
            string strSQL = "";
            strSQL += "SELECT Name{0} ";
            strSQL += "FROM {1}_ControllerViewTranslate";
            strSQL += "WHERE IDControllerView={2} ";
            strSQL += "ORDER BY SortID";
            strSQL = string.Format(strSQL, Session["asisLangCode"] as string, prefix, IDControllerView);
            //var ds = AsisModelHelper.GetDataSet(strSQL);

            ViewBag.label = new List<string>();

            //ViewBag.label[0] = ds.jdjdj;
            // convert object tot arraylist
            //int Total = 10;
            //string arlist[Total];

            //// add records in an arraylist
            //ViewBag.Collection = arlist;

        }



        // GET: AsisBase
        public ActionResult CheckAuthorisation(ActionResult result = null)
        {

            if (!IsUserAuthorized()) // NOT in vusual basic is ! i C#
            {
                ViewBag.Name = AsisModelHelper.GetMessage(1, "asis", Session["asisLangCode"] as string);
                //throw new Exception(ViewBag.Name);
                //return null;
                return View("AsisError");
            }
            return result;
        }

        // de constructur is een routine met dezelfde naam als de class en public
        public bool IsUserAuthorized()
        {
            if (Session["IDUserGroup"] == null || Session["IDUser"] == null) // OR in vb is || in C# en AND in VB is && in C#
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        internal void BeforeEdit(object record)
        {
            Session["prevRecord"] = record;
        }

        internal void AfterEdit(object record,  int IDUser, string tableName)
        {

        }

        public virtual object FindAsisObjectModel(string controllerName, int id)
        {
            //RL: Opmerking: Dit is net alsof wij db.asis_param.Find(id) uitvoeren

            //asis_object_type.FindMembers(MemberTypes.All, BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance, null, null)
            var asis_object_members = db.GetType().GetMember(controllerName);
            var asis_object_type = ((System.Reflection.PropertyInfo)asis_object_members[0]).PropertyType;
            var asis_object_instance = ((System.Reflection.PropertyInfo)asis_object_members[0]).GetValue(db);

            object asis_object = (object)(asis_object_type.InvokeMember(
                                   "Find",
                                   BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance,
                                   null,
                                   asis_object_instance,
                                   new object[] { id }));
            return asis_object;
        }

        public virtual Type GetAsisObjectModelType()
        {
            return null;
        }
    }
}