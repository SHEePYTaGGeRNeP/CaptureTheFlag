﻿using UnityEngine;

public class AndroidNFCReader : MonoBehaviour
{
    public const string ERROR = "ERROR";
    public const string NO_HARDWARE = "NO_HARDWARE";
    public const string CANCELLED = "CANCELLED";
    public const string NO_ALLOWED_OS = "NO_ALLOWED_OS";
    

    public static void enableBackgroundScan()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
		AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity"); 
		javaObject.Call("enableBackgroundScan");
#endif
    }

    public static void disableBackgroundScan()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
		AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity"); 
		javaObject.Call("disableBackgroundScan");
#endif
    }


    public static void ScanNFC(string objectName, string functionName)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
                javaObject.Call("scanNFC", objectName, functionName);
                break;
            default:
                GameObject.Find(objectName).SendMessage(functionName, NO_ALLOWED_OS);
                break;
        }
    }
}
