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
    [XafDisplayName("Porteur Carte")]
    [XafDefaultProperty(nameof(Nom_Prenom))]
    public class Porteur_Carte : XpertCustomObject
    { 
        public Porteur_Carte(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        string num_Carte_Identite;
        string nom_Prenom;

        [XafDisplayName("Num Carte Identité")]
        public string Num_Carte_Identite
        {
            get => num_Carte_Identite;
            set => SetPropertyValue(nameof(Num_Carte_Identite), ref num_Carte_Identite, value);
        }
        [XafDisplayName("Nom et Prénom")]
        public string Nom_Prenom
        {
            get => nom_Prenom;
            set => SetPropertyValue(nameof(Nom_Prenom), ref nom_Prenom, value);
        }
    }
}