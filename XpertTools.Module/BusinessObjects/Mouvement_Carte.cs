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
    [XafDisplayName("Mouvement Carte")]
    [XafDefaultProperty(nameof(Porteur_Carte))]
    public class Mouvement_Carte : XpertCustomObject
    { 
        public Mouvement_Carte(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        Type_Mouvement type_Mouvement;
        Porteur_Carte porteur_Carte;
        Carte_Chifa carte;
        DateTime date_Mouvement;

        [Association("Carte_Chifa-Mouvement_Carte")]
        public Carte_Chifa Carte
        {
            get => carte;
            set => SetPropertyValue(nameof(Carte), ref carte, value);
        }

        [XafDisplayName("Porteur Carte")]
        public Porteur_Carte Porteur_Carte
        {
            get => porteur_Carte;
            set => SetPropertyValue(nameof(Porteur_Carte), ref porteur_Carte, value);
        }

        [XafDisplayName("Type")]
        public Type_Mouvement Type_Mouvement
        {
            get => type_Mouvement;
            set => SetPropertyValue(nameof(Type_Mouvement), ref type_Mouvement, value);
        }


        [XafDisplayName("Date")]
        public DateTime Date_Mouvement
        {
            get => date_Mouvement;
            set => SetPropertyValue(nameof(Date_Mouvement), ref date_Mouvement, value);
        }
    }
    public enum Type_Mouvement
    {
        [XafDisplayName("Entrée")]
        Entree = 0,
        [XafDisplayName("Sortie")]
        Sortie = 1
    }
}