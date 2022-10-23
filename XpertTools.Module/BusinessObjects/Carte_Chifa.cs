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
    [XafDisplayName("Carte")]
    [XafDefaultProperty(nameof(Num_Carte))]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Carte_Chifa : XpertCustomObject
    { 
        public Carte_Chifa(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        Assure_Chifa assure_Chifa;
        string num_Carte;
        [XafDisplayName("N° carte")]
        [RuleRequiredField]
        public string Num_Carte
        {
            get => num_Carte;
            set => SetPropertyValue(nameof(Num_Carte), ref num_Carte, value);
        }

        [XafDisplayName("Assuré")]
        public Assure_Chifa Assure_Chifa
        {
            get => assure_Chifa;
            set => SetPropertyValue(nameof(Assure_Chifa), ref assure_Chifa, value);
        }

       

        [Association("Carte_Chifa-Mouvement_Carte")]
        [XafDisplayName("Mouvements")]
        [ModelDefault("AllowEdit", "False")]
        public XPCollection<Mouvement_Carte> Mouvements
        {
            get
            {
                return GetCollection<Mouvement_Carte>(nameof(Mouvements));
            }
        }

    }
}