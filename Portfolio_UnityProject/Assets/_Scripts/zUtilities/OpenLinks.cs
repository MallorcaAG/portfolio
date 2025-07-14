using System.Runtime.InteropServices;
using UnityEngine;


public class OpenLinks : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void OpenTab(string url);

    public static void OpenURL(string url)
    {
    #if !UNITY_EDITOR && UNITY_WEBGL
        OpenTab(url);
    #endif
    }
}
