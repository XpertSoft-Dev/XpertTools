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
            set
            {
                if (assure_Chifa == value)
                    return;

                // Store a reference to the former owner.
                Assure_Chifa prevObject = assure_Chifa;
                assure_Chifa = value;

                if (IsLoading) return;

                // Remove an owner's reference to this building, if exists.
                if (prevObject != null && prevObject.Carte_Chifa == this)
                    prevObject.Carte_Chifa = null;

                // Specify that the building is a new owner's house.
                if (assure_Chifa != null)
                    Assure_Chifa.Carte_Chifa = this;
                
                if (this.Assure_Chifa as Assure_Chifa == null)
                    this.Assure_Chifa = null;

                OnChanged(nameof(Assure_Chifa));

            }
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