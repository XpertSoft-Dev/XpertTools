using DevExpress.Blazor;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
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

namespace XpertTools.Blazor.Server.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CustomizeLookupPropertyEditorController : ViewController<DetailView>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            View.CustomizeViewItemControl<LookupPropertyEditor>(this, CustomizeLookupPropertyEditor);
        }
        private void CustomizeLookupPropertyEditor(LookupPropertyEditor lookupPropertyEditor)
        {
            if (lookupPropertyEditor.Control is DxComboBoxAdapter comboBoxAdapter)
            {
                comboBoxAdapter.ComponentModel.ListRenderMode = ListRenderMode.Entire;
            }
        }
    }
}
