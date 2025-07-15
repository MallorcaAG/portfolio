using System.Runtime.InteropServices;
using UnityEngine;


public class OpenLinks : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void OpenNewTab(string url);
    [DllImport("__Internal")]
    private static extern void OpenInCurrentTab(string url);

    public static void OpenURLInNew(string url)
    {
    #if !UNITY_EDITOR && UNITY_WEBGL
        OpenNewTab(url);
    #endif
    }

    public static void OpenURLInCurrent(string url)
    {
    #if !UNITY_EDITOR && UNITY_WEBGL
        OpenInCurrentTab(url);
    #endif
    }
}
