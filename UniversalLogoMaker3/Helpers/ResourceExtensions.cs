namespace UniversalLogoMaker3.Helpers
{
    using Windows.ApplicationModel.Resources;

    internal static class ResourceExtensions
    {
        private static readonly ResourceLoader ResourceLoader = new ResourceLoader();

        public static string GetLocalized(this string resourceKey)
        {
            return ResourceLoader.GetString(resourceKey);
        }
    }
}
