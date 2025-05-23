using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class MyPlugin
{
    #if UNITY_IOS
    // This is a C# declaration of a native function implemented in Objective-C (iOS).
    // Unity will use this to call the native function `_RunSwiftCode` defined in MyPluginBridge.m.
    // On iOS, "__Internal" refers to the symbols linked in the final binary,
    // and requires the function to be marked as `extern "C"` (i.e., not inside a class).
    [DllImport("__Internal")]
    private static extern IntPtr _RunSwiftCode(string value);
    #elif UNITY_ANDROID
    // Call into a static Kotlin method using Unity's AndroidJavaClass bridge.
    // See: https://docs.unity3d.com/ScriptReference/AndroidJavaClass.html
    private const string AndroidPluginClass = "com.JulianLearningAbout.NativeCodeFromUnity.Test";
    #endif

    /*
    * Runs either iOS or Android code, depending on the platform.
    */
    public static string Run(string value)
    {
        #if UNITY_EDITOR
        return "-- Neither iOS nor Android runtime --";
        #elif UNITY_IOS
        return Marshal.PtrToStringAnsi(_RunSwiftCode(value));
        #elif UNITY_ANDROID
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (var pluginClass = new AndroidJavaClass(AndroidPluginClass))
        {
            return pluginClass.CallStatic<string>("run", value, context);
        }
        #endif
    }
}