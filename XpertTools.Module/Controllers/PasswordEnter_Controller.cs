using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XpertTools.Module.BusinessObjects;

namespace XpertTools.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class PasswordEnter_Controller : ObjectViewController<DetailView, PasswordEnter>
    {
        SimpleAction ValidePassword;
        public PasswordEnter_Controller()
        {
            //InitializeComponent();
            //ValidePassword = new SimpleAction(this, "ValidePassword", PredefinedCategory.PopupActions)
            //{
            //    Caption = "Valider",
            //};
            //ValidePassword.Execute += ValidePassword_Execute;
            //// Target required Views (via the TargetXXX properties) and create their Actions.
        }

        private void ValidePassword_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            PasswordEnter Password = ViewCurrentObject as PasswordEnter;
            IObjectSpace objectSpace = Application.CreateObjectSpace();

            List<ApplicationUser> users = objectSpace.GetObjectsQuery<ApplicationUser>().ToList();

            ApplicationUser UserEqual = users.FirstOrDefault();
            bool areEqual = false;
            foreach (ApplicationUser applicationUser in users)
            {
                var user = objectSpace.GetObjectsQuery<PermissionPolicyUser>().FirstOrDefault(u => u.UserName == applicationUser.UserName);
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
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
