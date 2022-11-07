using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
using XpertTools.Blazor.Server.Services;

namespace XpertTools.Blazor.Server;

public class XpertToolsBlazorApplication : BlazorApplication
{
    public XpertToolsBlazorApplication()
    {
        ApplicationName = "XpertTools";
        CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
        DatabaseVersionMismatch += XpertToolsBlazorApplication_DatabaseVersionMismatch;
    }
    protected override void OnSetupStarted()
    {
        base.OnSetupStarted();
        DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
    }
    private void XpertToolsBlazorApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e)
    {
        e.Updater.Update();
        e.Handled = true;
    }
}
