using ZTaxiApp.Helpers;

namespace ZTaxiApp.UIModel
{
    public class QuickAction
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public ActionEnum Action { get; set; }

        public DashboardActionType ActionType { get; set; }
    }
}
