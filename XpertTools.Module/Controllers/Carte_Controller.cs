using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XpertTools.Module.BusinessObjects;

namespace XpertTools.Module.Controllers
{
    public partial class Carte_Controller : ObjectViewController<ObjectView, Carte_Chifa>
    {
        public Carte_Controller()
        {
            InitializeComponent();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ObjectSpace.ObjectDeleting += ObjectSpace_ObjectDeleting;

        }

        private void ObjectSpace_ObjectDeleting(object sender, ObjectsManipulatingEventArgs e)
        {
            foreach(Object Delting in e.Objects)
            {
                if (Delting as Carte_Chifa != null)
                {
                    (Delting as Carte_Chifa).Assure_Chifa.Carte_Chifa = null;
                    Ayants_Droit ayants_Droit = ObjectSpace.FirstOrDefault<Ayants_Droit>(x => x.Carte_Chifa.Oid == (Delting as Carte_Chifa).Oid);
                    while(ayants_Droit != null)
                    {
                        ayants_Droit.Carte_Chifa = null;
                        ObjectSpace.CommitChanges();
                        ayants_Droit = ObjectSpace.FirstOrDefault<Ayants_Droit>(x => x.Carte_Chifa.Oid == (Delting as Carte_Chifa).Oid);
                    }
                }
            }
            ObjectSpace.CommitChanges();
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
