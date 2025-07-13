using System.Runtime.InteropServices;
using UnityEngine;

public class MobileDetector : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern bool IsMobile();

    [SerializeField] private GameObject mobileObj;
    [SerializeField] private GameObject pcObj;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(isRunningOnMobile())
        {
            mobileObj.SetActive(true);
            pcObj.SetActive(false);
        }
        else
        {
            mobileObj.SetActive(false);
            pcObj.SetActive(true);
        }
    }

    

    private bool isRunningOnMobile()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return IsMobile();
#else
        return false;
#endif
    }
}
