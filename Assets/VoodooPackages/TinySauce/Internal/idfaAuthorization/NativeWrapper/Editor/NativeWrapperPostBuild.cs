#if UNITY_IOS || UNITY_TVOS
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Collections.Generic;
namespace Voodoo.Sauce.Internal.IdfaAuthorization
{
    public class NativeWrapperPostBuild
    {
        private const string UserTrackingUsageDescriptionKey = "NSUserTrackingUsageDescription";
        private static string skanPath = "Assets/VoodooPackages/TinySauce/Internal/EnvironmentSettings/SKAN/SkanIds.txt";
        private static List<string> skanIds;

        [PostProcessBuild(1000)]
        public static void PostprocessBuild(BuildTarget buildTarget, string buildPath)
        {
            skanIds = new List<string>();

            FileInfo skanFile = new FileInfo(skanPath);
            StreamReader reader = skanFile.OpenText();

            string text;
            do
            {
                text = reader.ReadLine();
                //Debug.Log(text);
                if (text != null)
                    skanIds.Add(text.Split('=')[0]);
            } while (text != null);
            if (IdfaAuthorizationUtils.IsAttEnabled())
            {
                if (buildTarget != BuildTarget.iOS)
                    return;
                var plistPath = Path.Combine(buildPath, "Info.plist");
                var plist = new PlistDocument();
                plist.ReadFromFile(plistPath);
                PlistElementArray skAdNetworks = plist.root.CreateArray("SKAdNetworkItems");
                for (int i = 0; i < skanIds.Count; i++)
                {
                    skAdNetworks.AddDict().SetString("SKAdNetworkIdentifier", skanIds[i].ToLower());
                }
                plist.root.SetString(UserTrackingUsageDescriptionKey, IdfaAuthorizationConstants.UserTrackingUsageDescription);
                plist.WriteToFile(plistPath);
            }
        }

    }
}
#endif