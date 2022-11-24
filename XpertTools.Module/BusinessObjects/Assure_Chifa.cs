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
using XpertTools.Module.BusinessObjects.Interfaces;

namespace XpertTools.Module.BusinessObjects
{
    [DefaultClassOptions]
    [XafDisplayName("Assuré")]
    [XafDefaultProperty(nameof(Nom_Complet))]

    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Assure_Chifa : XpertCustomObject, IAssure_AyantDroit
    { 
        public Assure_Chifa(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        string num_Assure;
        Carte_Chifa carte_Chifa;
        Type_Maladie type_Maladie;
        string num_Telephone;
        string adresse;
        string prenom;
        string nom;

        [XafDisplayName("Type Maladie")]
        public Type_Maladie Type_Maladie
        {
            get => type_Maladie;
            set => SetPropertyValue(nameof(Type_Maladie), ref type_Maladie, value);
        }

        [XafDisplayName("N° Assuré")]
        public string Num_Assure
        {
            get => num_Assure;
            set => SetPropertyValue(nameof(Num_Assure), ref num_Assure, value);
        }
        public string Nom
        {
            get => nom;
            set => SetPropertyValue(nameof(Nom), ref nom, value);
        }

        [XafDisplayName("Prénom")]
        public string Prenom
        {
            get => prenom;
            set => SetPropertyValue(nameof(Prenom), ref prenom, value);
        }

        [XafDisplayName("Nom et Prénom")]
        public string Nom_Complet
        {
            get => Num_Assure + " " + Nom + " " + Prenom;
        }
        public string Adresse
        {
            get => adresse;
            set => SetPropertyValue(nameof(Adresse), ref adresse, value);
        }

        [XafDisplayName("Numéro Téléphone")]
        public string Num_Telephone
        {
            get => num_Telephone;
            set => SetPropertyValue(nameof(Num_Telephone), ref num_Telephone, value);
        }

        [XafDisplayName("Carte")]
        public Carte_Chifa Carte_Chifa
        {
            get => carte_Chifa;
            set
            {
                if (carte_Chifa == value)
                    return;

                // Store a reference to the former owner.
                Carte_Chifa prevObject = carte_Chifa;
                carte_Chifa = value;

                if (IsLoading) return;

                // Remove an owner's reference to this building, if exists.
                if (prevObject != null && prevObject.Assure_Chifa == this)
                    prevObject.Assure_Chifa = null;

                // Specify that the building is a new owner's house.
                if (Carte_Chifa != null)
                    Carte_Chifa.Assure_Chifa = this;

                if (Carte_Chifa as Carte_Chifa == null)
                    this.Carte_Chifa = null;

                OnChanged(nameof(Carte_Chifa));
            }
        }

    }
    public enum Type_Maladie
    {
        [XafDisplayName("Non Chronique")]
        NonChronique = 0,
        [XafDisplayName("Chronique")]
        Chronique = 1,
    }
}