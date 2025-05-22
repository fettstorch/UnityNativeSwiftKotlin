using System;
using System.Runtime.InteropServices;

public class MyPlugin
{
    // This is a C# declaration of a native function implemented in Objective-C (iOS).
    // Unity will use this to call the native function `_RunSwiftCode` defined in MyPluginBridge.m.
    // On iOS, "__Internal" refers to the symbols linked in the final binary,
    // and requires the function to be marked as `extern "C"` (i.e., not inside a class).
    [DllImport("__Internal")]
    private static extern IntPtr _RunSwiftCode(string value);

    public static string Run(string value)
    {
        #if UNITY_EDITOR
        return "-- Neither iOS nor Android runtime --";
        #elif UNITY_IOS
        return Marshal.PtrToStringAnsi(_RunSwiftCode(value));
        // TODO: Add other platforms e.g. Android
        #endif
    }
}
