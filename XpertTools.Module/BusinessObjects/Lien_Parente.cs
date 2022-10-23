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
    [DefaultClassOptions]
    [XafDefaultProperty(nameof(Rang_Designation))]
    [XafDisplayName("Lien Parenté")]
    public class Lien_Parente : BaseObject
    { 
        public Lien_Parente(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        string rang;
        [XafDisplayName("Lien_Parente")]
        public string Rang_Designation
        {
            get => rang;
            set => SetPropertyValue(nameof(Rang_Designation), ref rang, value);
        }
    }
}