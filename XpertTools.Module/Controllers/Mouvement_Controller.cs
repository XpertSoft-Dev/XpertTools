using DevExpress.Charts.Model;
using DevExpress.DashboardExport.Map;
using DevExpress.Data.Filtering;
using DevExpress.DataAccess.Wizard.Presenters;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.Cms;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraGauges.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using XpertTools.Module.BusinessObjects;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace XpertTools.Module.Controllers
{
    public partial class Mouvement_Controller : ObjectViewController<DetailView, Mouvement_Carte>
    {
        PopupWindowShowAction GenerateSortie;
        PopupWindowShowAction CheckPasword;


        public Mouvement_Controller()
        {
            InitializeComponent();
            GenerateSortie = new PopupWindowShowAction(this, "GenerateSortie", PredefinedCategory.RecordEdit)
            {
                Caption = "Générate sortie",
            };
            GenerateSortie.CustomizePopupWindowParams += GenerateSortie_CustomizePopupWindowParams;

            CheckPasword = new PopupWindowShowAction(this, "SouvgarderMouvement", PredefinedCategory.Save)
            {
                Caption = "Souvgarder",
                ImageName = "save",
            };
            CheckPasword.CustomizePopupWindowParams += CheckPasword_CustomizePopupWindowParams;
        }

        private void CheckPasword_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var nonPersistentOS = Application.CreateObjectSpace(typeof(PasswordEnter));
            PasswordEnter itemStyleList = nonPersistentOS.CreateObject<PasswordEnter>();
            e.View = Application.CreateDetailView(nonPersistentOS, itemStyleList);
            e.Action.Execute += Action_Execute1;
        }

        private void GenerateSortie_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var nonPersistentOS = Application.CreateObjectSpace(typeof(PasswordEnter));
            PasswordEnter itemStyleList = nonPersistentOS.CreateObject<PasswordEnter>();
            e.View = Application.CreateDetailView(nonPersistentOS, itemStyleList);
            e.Action.Execute += Action_Execute;
            e.DialogController.AcceptAction.Active.SetItemValue("Popup", true);

        }





        private void Action_Execute1(object sender, PopupWindowShowActionExecuteEventArgs e)
        {

            Session session = ((XPObjectSpace)ObjectSpace).Session;

            PasswordEnter Password = e.PopupWindowViewCurrentObject as PasswordEnter;
            List<ApplicationUser> users = ObjectSpace.GetObjectsQuery<ApplicationUser>().ToList();
            ApplicationUser UserEqual = users.FirstOrDefault();
            bool areEqual = false;
            foreach (ApplicationUser applicationUser in users)
            {
                var user = session.Query<PermissionPolicyUser>().FirstOrDefault(u => u.UserName == applicationUser.UserName);
                var saltedPassword = (string)user?.GetMemberValue("StoredPassword");
                areEqual = PasswordCryptographer.VerifyHashedPasswordDelegate(saltedPassword, Password.Password?.ToString());
                if (areEqual == true)
                {
                    UserEqual = applicationUser;
                    break;

                }
            }
            if (!areEqual)
            {
                throw new UserFriendlyException("Mot de passe incorrect");
            }
            if (areEqual)

            {
                Mouvement_Carte obj = new Mouvement_Carte(session);
                View detailView = Application.CreateDetailView(ObjectSpace, obj, false);
                obj.Carte = ViewCurrentObject.Carte;
                obj.Type_Mouvement = Type_Mouvement.Sortie;
                obj.Date_Mouvement = DateTime.Now;
                obj.CreatedBy = UserEqual?.UserName?.ToString();
                ObjectSpace.CommitChanges();
            }


        }

        private void Action_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Session session = ((XPObjectSpace)ObjectSpace).Session;

            PasswordEnter Password = e.PopupWindowViewCurrentObject as PasswordEnter;
            List<ApplicationUser> users = ObjectSpace.GetObjectsQuery<ApplicationUser>().ToList();
            ApplicationUser UserEqual = users.FirstOrDefault();
            bool areEqual = false;
            foreach (ApplicationUser applicationUser in users)
            {
                var user = session.Query<PermissionPolicyUser>().FirstOrDefault(u => u.UserName == applicationUser.UserName);
                var saltedPassword = (string)user?.GetMemberValue("StoredPassword");
                areEqual = PasswordCryptographer.VerifyHashedPasswordDelegate(saltedPassword, Password.Password?.ToString());
                if (areEqual == true)
                {
                    UserEqual = applicationUser;
                    break;

                }
            }
            if (!areEqual)
            {
                throw new UserFriendlyException("Mot de passe incorrect");
            }

            if (areEqual)

            {
                IObjectSpace objectSpace_Popup = Application.CreateObjectSpace();
                Session session_popup = ((XPObjectSpace)objectSpace_Popup).Session;
                Mouvement_Carte obj = new Mouvement_Carte(session_popup);
                View detailView = Application.CreateDetailView(objectSpace_Popup, obj, false);
                obj.Carte = objectSpace_Popup.GetObject( ViewCurrentObject.Carte);
                obj.Type_Mouvement = Type_Mouvement.Sortie;
                obj.Date_Mouvement = DateTime.Now;
                obj.CreatedBy = UserEqual?.UserName?.ToString();
                ViewCurrentObject.SortieGenerated = true;
                ShowViewParameters showViewParameters = new ShowViewParameters(detailView);
                Application.ShowViewStrategy.ShowViewInPopupWindow(detailView, () =>
                    {
                        objectSpace_Popup.CommitChanges();
                        detailView.Close();
                    }
                );
            }
            ObjectSpace.CommitChanges();

        }



        protected override void OnActivated()
        {
            base.OnActivated();


        }

        //private void SaveAction_Executing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    var nonPersistentOS = Application.CreateObjectSpace(typeof(PasswordEnter));
        //    PasswordEnter itemStyleList = nonPersistentOS.CreateObject<PasswordEnter>();
        //    ShowViewParameters showViewParameters = new ShowViewParameters();
        //    showViewParameters.TargetWindow = TargetWindow.NewModalWindow;
        //    ShowViewSource viewSource = new ShowViewSource(this.Frame, null);
        //    bool areEqual = false;
        //    DetailView detailview = Application.CreateDetailView(itemStyleList);
        //    Application.ShowViewStrategy.ShowViewInPopupWindow(detailview,
        //        () =>
        //        {
        //            List<ApplicationUser> users = ObjectSpace.GetObjectsQuery<ApplicationUser>().ToList();
        //            ApplicationUser UserEqual = users.FirstOrDefault();
        //            foreach (ApplicationUser applicationUser in users)
        //            {
        //                var user = ObjectSpace.FirstOrDefault<PermissionPolicyUser>(u => u.UserName == applicationUser.UserName);
        //                var saltedPassword = (string)user?.GetMemberValue("StoredPassword");
        //                areEqual = PasswordCryptographer.VerifyHashedPasswordDelegate(saltedPassword, itemStyleList.Password?.ToString());
        //                if (areEqual == true)
        //                {
        //                    UserEqual = applicationUser;
        //                    break;

        //                }
        //            }
        //            if (!areEqual)
        //            {
        //                e.Cancel = true;
        //            }

        //            if (areEqual)

        //            {
        //                //Mouvement_Carte obj = new Mouvement_Carte(session);
        //                //View detailView = Application.CreateDetailView(ObjectSpace, obj, false);
        //                //obj.Carte = ViewCurrentObject.Carte;
        //                //obj.Type_Mouvement = Type_Mouvement.Sortie;
        //                //obj.Date_Mouvement = DateTime.Now;
        //                //obj.CreatedBy = UserEqual?.UserName?.ToString();
        //                //ViewCurrentObject.SortieGenerated = true;
        //                //ShowViewParameters showViewParameters = new ShowViewParameters(detailView);
        //                //showViewParameters.TargetWindow = TargetWindow.NewModalWindow;
        //                //ShowViewSource viewSource = new ShowViewSource(this.Frame, null);
        //                //Application.ShowViewStrategy.ShowView(showViewParameters, viewSource);
        //            }
        //        }
        //        );


        //}

        //private void SaveAction_Execute(object sender, SimpleActionExecuteEventArgs eventar)
        //{
        //    var nonPersistentOS = Application.CreateObjectSpace(typeof(PasswordEnter));
        //    PasswordEnter itemStyleList = nonPersistentOS.CreateObject<PasswordEnter>();
        //    ShowViewParameters showViewParameters = new ShowViewParameters();
        //    showViewParameters.TargetWindow = TargetWindow.NewModalWindow;
        //    ShowViewSource viewSource = new ShowViewSource(this.Frame, null);
        //    bool areEqual = false;
        //    Application.ShowViewStrategy.ShowViewInPopupWindow(this.View,
        //        () =>
        //        {
        //            List<ApplicationUser> users = nonPersistentOS.GetObjectsQuery<ApplicationUser>().ToList();
        //            ApplicationUser UserEqual = users.FirstOrDefault();
        //            foreach (ApplicationUser applicationUser in users)
        //            {
        //                var user = nonPersistentOS.FirstOrDefault<PermissionPolicyUser>(u => u.UserName == applicationUser.UserName);
        //                var saltedPassword = (string)user?.GetMemberValue("StoredPassword");
        //                areEqual = PasswordCryptographer.VerifyHashedPasswordDelegate(saltedPassword, itemStyleList.Password?.ToString());
        //                if (areEqual == true)
        //                {
        //                    UserEqual = applicationUser;
        //                    break;

        //                }
        //            }

        //        }
        //        );

        //    if (!areEqual)
        //    {

        //    }

        //    if (areEqual)

        //    {
        //        //Mouvement_Carte obj = new Mouvement_Carte(session);
        //        //View detailView = Application.CreateDetailView(ObjectSpace, obj, false);
        //        //obj.Carte = ViewCurrentObject.Carte;
        //        //obj.Type_Mouvement = Type_Mouvement.Sortie;
        //        //obj.Date_Mouvement = DateTime.Now;
        //        //obj.CreatedBy = UserEqual?.UserName?.ToString();
        //        //ViewCurrentObject.SortieGenerated = true;
        //        //ShowViewParameters showViewParameters = new ShowViewParameters(detailView);
        //        //showViewParameters.TargetWindow = TargetWindow.NewModalWindow;
        //        //ShowViewSource viewSource = new ShowViewSource(this.Frame, null);
        //        //Application.ShowViewStrategy.ShowView(showViewParameters, viewSource);
        //    }
        //}


        //private void ObjectSpace_Committing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    try
        //    {

        //        IObjectSpace nonPersistentObjectSpace = Application.CreateObjectSpace(typeof(PasswordEnter));
        //        PasswordEnter issueStatistics = new PasswordEnter();
        //        PasswordEnter obj = nonPersistentObjectSpace.CreateObject<PasswordEnter>();
        //        View detailView = Application.CreateDetailView(obj);
        //        ShowViewParameters showViewParameters = new ShowViewParameters(detailView);
        //        showViewParameters.TargetWindow = TargetWindow.NewModalWindow;
        //        ShowViewSource viewSource = new ShowViewSource(this.Frame, null);

        //        Application.ShowViewStrategy.ShowViewInPopupWindow(detailView, () =>
        //        {
        //            List<ApplicationUser> users = ObjectSpace.GetObjectsQuery<ApplicationUser>().ToList();
        //            ApplicationUser UserEqual = users.FirstOrDefault();
        //            bool areEqual = false;
        //            foreach (ApplicationUser applicationUser in users)
        //            {
        //                var user = ObjectSpace.GetObjectsQuery<PermissionPolicyUser>().FirstOrDefault(u => u.UserName == applicationUser.UserName);
        //                var saltedPassword = (string)user?.GetMemberValue("StoredPassword");
        //                areEqual = PasswordCryptographer.VerifyHashedPasswordDelegate(saltedPassword, obj.Password?.ToString());
        //                ViewCurrentObject.Validated = true;
        //                if (areEqual == true)
        //                {
        //                    UserEqual = applicationUser;
        //                    ViewCurrentObject.Validated = true;
        //                    break;
        //                }
        //                else
        //                {
        //                    ViewCurrentObject.Validated = false;
        //                    e.Cancel = true;

        //                }
        //                throw new UserFriendlyException("Wrong Password !");
        //            }
        //        });
        //    }
        //    catch (UserFriendlyException ex)
        //    {
        //        throw new UserFriendlyException(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
    }
}
