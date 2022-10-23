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
    [XafDisplayName("Assuré")]
    [XafDefaultProperty(nameof(Num_Assure))]

    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Assure_Chifa : XpertCustomObject
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
            get => Nom + " " + Prenom;
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
            set => SetPropertyValue(nameof(Carte_Chifa), ref carte_Chifa, value);
        }

        protected override void OnSaving()
        {
            if ( this.Session.IsNewObject(this))
            {
                Porteur_Carte porteur_Carte =  new Porteur_Carte(this.Session)
                {
                    Num_Carte_Identite = Num_Assure,
                    Nom_Prenom = Nom + " " + Prenom,
                };
            }
            base.OnSaving();
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