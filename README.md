# Unity Native Plugin with Swift (iOS) and Kotlin (Android) — Minimal Example

This repository demonstrates how to call native platform code from Unity C# using minimal native interop — for both **iOS (Swift)** and **Android (Kotlin)**.

> 🛠️ Built with Unity 2022.3.1f1

✅ No third-party libraries — only platform-native tools.

The example demonstrates:
* Basic native code integration
* Passing data between Unity and native code

---

## 📚 Table of Contents

* [Overview](#overview)
* [iOS (Swift) Plugin](#ios-swift-plugin)
  * [iOS Bridge Overview](#ios-bridge-overview)
  * [iOS File Structure](#ios-file-structure)
  * [Setup Instructions (iOS)](#setup-instructions-ios)
* [Android (Kotlin) Plugin](#android-kotlin-plugin)

---

## 🔍 Overview

| Platform | Language | Bridge Path                                       |
| -------- | -------- | ------------------------------------------------- |
| iOS      | Swift    | Unity C# → DllImport (C) → Objective-C → Swift    |
| Android  | Kotlin   | Unity C# → AndroidJavaObject (JNI) → Kotlin class |

> ⚠️ Unity requires specific directory structures for plugins:
> * iOS plugins must be in `Assets/Plugins/iOS/`
> * Android plugins must be in `Assets/Plugins/Android/`
>
> These paths are mandatory for Unity to properly include the plugins during platform-specific builds.

---

## 🍎 iOS (Swift) Plugin

This section shows how to:

* Call Swift from Unity C#
* Pass a string from Unity to Swift
* Store the passed string in iOS UserDefaults
* Return a string from Swift back to Unity

### 🔄 iOS Bridge Overview

```text
Unity C#
  ↓ (DllImport)
C function in Obj-C
  ↓
Objective-C bridge class (required)
  ↓
Swift static method (Test.swift) → UserDefaults
```

### 📂 iOS File Structure

```
Assets/
└── Plugins/
    └── iOS/
        ├── MyPlugin.cs      (initiates the bridge call)
        ├── MyPluginBridge.h (defines the bridge interface)
        ├── MyPluginBridge.m (implements the bridge)
        └── Test.swift       (provides the desired swift code)
```

### 🛠 Setup Instructions (iOS)

1. Place `.swift`, `.m` & `.h` files in `Assets/Plugins/iOS/`
2. Build the Unity project for iOS

> ℹ️ When first opening the Xcode project, the build will fail due to missing signing configuration. This is normal - just configure your development team in the project settings.

3. If the build doesn't work out of the box, open Xcode and:

   * Select **UnityFramework** target
   * **Build Settings**:

     * `Defines Module = YES`
     * `Always Embed Swift Standard Libraries = YES`
     * Add `$(BUILT_PRODUCTS_DIR)/UnityFramework.framework/Headers` to **Header Search Paths** (non-recursive)
   * **Build Phases** → add `Test.swift` and `MyPluginBridge.m` to **Compile Sources**
4. Run the project on an emulator or device:
   * You should see the message "Julian says hello from iOS"
   * You can modify the argument in the Bootstrapper component in Unity's default scene

---

## 🤖 Android (Kotlin) Plugin

Will follow soon 🙏🏻
