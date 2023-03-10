using System.Reflection;

namespace Trm.MaLogger.App.Services
{
    public class VersionHelper
    {
        public static string GetAssemblyVersion()
        {
            AssemblyInformationalVersionAttribute? infoVersion = (AssemblyInformationalVersionAttribute?)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false).FirstOrDefault();
            return infoVersion == null ? "" : infoVersion.InformationalVersion;
        }
    }
}
