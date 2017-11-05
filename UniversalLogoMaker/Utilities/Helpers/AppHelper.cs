namespace UniversalLogoMaker.Utilities.Helpers
{
    using ViewModels;

    public static class AppHelper
    {
        public static void SetStatus(string status)
        {
            StaticData.StartVm.Status = status;
        }

        public static void ResetStatus()
        {
            StaticData.StartVm.Status = "Ready";
        }
    }
}
