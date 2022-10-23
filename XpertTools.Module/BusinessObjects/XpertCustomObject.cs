using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertTools.Module.BusinessObjects
{
    //[Appearance("AllowEditDocCloture", AppearanceItemType = "ViewItem", TargetItems = "*", Context = "Any",
    //    Criteria = "Document_Cloture= True",
    //    Enabled = false), ModelDefault("AllowEdit", "False")]

    [NonPersistent]
    public class XpertCustomObject : BaseObject
    {
        public XpertCustomObject(Session session)
            : base(session)
        {
            //XPObject
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CreatedBy = GetCurrentUser()?.UserName;
       
        }

        DateTime modifiedOn;
        DateTime createdOn;
        string modifiedBy;
        string createdBy;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("Création par")]
        [VisibleInDetailView(false)]
        public string CreatedBy
        {
            get => createdBy;
            set => SetPropertyValue(nameof(CreatedBy), ref createdBy, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("Modification par")]
        [VisibleInListView(false), VisibleInDetailView(false)]
        public string ModifiedBy
        {
            get => modifiedBy;
            set => SetPropertyValue(nameof(ModifiedBy), ref modifiedBy, value);
        }

        [ModelDefault("DisplayFormat", "{dd/MM/yyyy HH:mm:ss.SSS}")]
        [XafDisplayName("Date Création")]
        [VisibleInDetailView(false)]
        public DateTime CreatedOn
        {
            get => createdOn;
            set => SetPropertyValue(nameof(CreatedOn), ref createdOn, value);
        }

        [ModelDefault("DisplayFormat", "{dd/MM/yyyy HH:mm:ss}")]
        [XafDisplayName("Date Modification")]
        [VisibleInDetailView(false)]
        public DateTime ModifiedOn
        {
            get => modifiedOn;
            set => SetPropertyValue(nameof(ModifiedOn), ref modifiedOn, value);
        }
        protected override void OnSaving()
        {
            if (Session.IsNewObject(this))
            {
                if (XpertHelper.IsNullOrEmpty(CreatedOn) || (XpertHelper.IsNotNullAndNotEmpty(CreatedOn) && CreatedOn.Equals(DateTime.MinValue)))
                {
                    CreatedOn = XpertHelper.GetDate();
                }
                CreatedBy = GetCurrentUser()?.UserName;
            }
            else
            {
                ModifiedOn = XpertHelper.GetDate();
                ModifiedBy = GetCurrentUser()?.UserName;
            }
            //   string DisplayNamePersistent = GetDisplayNamePersistent();
            //GetCircuitValidation();
            base.OnSaving();
        }

        public PermissionPolicyUser GetCurrentUser()
        {
            return Session.GetObjectByKey<PermissionPolicyUser>(SecuritySystem.CurrentUserId);  // In XAF apps for versions older than v20.1.7.
            //return Session.FindObject<PermissionPolicyUser>(CriteriaOperator.Parse("Oid=CurrentUserId()"));  // In non-XAF apps where SecuritySystem.Instance is unavailable (v20.1.7+).
        }

    }
}
