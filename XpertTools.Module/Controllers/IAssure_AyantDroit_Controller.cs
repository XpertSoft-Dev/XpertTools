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
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XpertTools.Module.BusinessObjects;
using XpertTools.Module.BusinessObjects.Interfaces;

namespace XpertTools.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class IAssure_AyantDroit_Controller : ObjectViewController<DetailView, IAssure_AyantDroit>
    {
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public IAssure_AyantDroit_Controller()
        {
            InitializeComponent();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ObjectSpace.ObjectSaving += ObjectSpace_ObjectSaving;
        }

        private void ObjectSpace_ObjectSaving(object sender, ObjectManipulatingEventArgs e)
        {
            Session session = ((XPObjectSpace)ObjectSpace).Session;

            Porteur_Carte LP = ObjectSpace.FirstOrDefault<Porteur_Carte>(x=> x.Num_Origin == (ViewCurrentObject.Oid).ToString());
            if (LP == null)
            {
                LP = new Porteur_Carte(session)
                {
                    Num_Origin = ( ViewCurrentObject.Oid ).ToString(),
                    Nom_Prenom = ViewCurrentObject.Nom_Complet
                };
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
    }
}
