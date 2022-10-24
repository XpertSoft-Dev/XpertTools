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
    [XafDisplayName("Ayants Droit")]
    [XafDefaultProperty(nameof(Num_Assure))]
    public class Ayants_Droit : XpertCustomObject, IAssure_AyantDroit
    {
        public Ayants_Droit(Session session)
             : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        Lien_Parente lien_Parente;
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

        [RuleRequiredField]
        public string Nom
        {
            get => nom;
            set => SetPropertyValue(nameof(Nom), ref nom, value);
        }

        [XafDisplayName("Prénom")]
        [RuleRequiredField]
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
        [RuleRequiredField]
        public Carte_Chifa Carte_Chifa
        {
            get => carte_Chifa;
            set => SetPropertyValue(nameof(Carte_Chifa), ref carte_Chifa, value);
        }
        [XafDisplayName("Lien Parenté")]
        public Lien_Parente Lien_Parente
        {
            get => lien_Parente;
            set => SetPropertyValue(nameof(Lien_Parente), ref lien_Parente, value);
        }
        protected override void OnSaving()
        {
            base.OnSaving();
          
        }
    }
}