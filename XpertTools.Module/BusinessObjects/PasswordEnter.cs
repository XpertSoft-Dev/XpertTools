using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace XpertTools.Module.BusinessObjects
{
    [NonPersistent]
    [DomainComponent]
    public class PasswordEnter : NonPersistentBaseObject
    {
        string password;
        [XafDisplayName("Mot de passe")]
        [ModelDefault("IsPassword", "true")]
        public string Password
        {
            get { return password; }
            set { SetPropertyValue(ref password, value); }
        }
    }
}