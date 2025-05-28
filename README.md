# Unity Native Plugin with Swift (iOS) and Kotlin (Android) — Minimal Example

This repository demonstrates how to call native platform code from Unity C# using minimal native interop — for both **iOS (Swift)** and **Android (Kotlin)**.

> 🛠️ Built with Unity 2022.3.1f1

✅ No third-party libraries — only platform-native tools.

The example demonstrates:
* Basic native code integration
* Passing data between Unity and native code

It will on Scene start invoke the native code - pass a string argument given through the editor (Bootstrapper) and return some other string from the native code:
<img width="875" alt="image" src="https://github.com/user-attachments/assets/5c0e6386-704f-42b9-9363-017f85729dc7" />


---

## 📚 Table of Contents

* [🔍 Overview](#-overview)
* [🍎 iOS (Swift) Plugin](#-ios-swift-plugin)
  * [🔄 iOS Bridge Overview](#-ios-bridge-overview)
  * [📂 iOS File Structure](#-ios-file-structure)
  * [🛠 Setup Instructions (iOS)](#-setup-instructions-ios)
* [🤖 Android (Kotlin) Plugin](#-android-kotlin-plugin)
  * [🔄 Android Bridge Overview](#-android-bridge-overview)
  * [📂 Android File Structure](#-android-file-structure)
  * [🛠 Setup Instructions (Android)](#-setup-instructions-android)

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

This section shows how to:

* Call Kotlin from Unity C#
* Pass a string from Unity to Kotlin
* Store the passed string in Android SharedPreferences
* Return a string from Kotlin back to Unity

### 🔄 Android Bridge Overview

```text
Unity C#
  ↓ (AndroidJavaClass/AndroidJavaObject)
JNI Bridge (automatic)
  ↓
Kotlin companion object (Test.kt) → SharedPreferences
```

### 📂 Android File Structure

```
Project Root/
├── AndroidProjectForGeneratingAar/    # Android Studio project
│   └── app/src/main/java/com/JulianLearningAbout/NativeCodeFromUnity/
│       └── Test.kt                    # Kotlin implementation
└── Assets/
    └── Plugins/
        └── Android/
            └── app-debug.aar    # Generated Android library
```

### 🛠 Setup Instructions (Android)

> ℹ️ Note that while Unity will create an xcode project for you in order to build an iOS app, this is **not** the case here! Unity expects a finished .aar and thus you will need to have your own android porject either separately or - like here - inside the unity project (but preferably outside the Assets folder).

1. **Build the Android Library**:
   * Open terminal in `AndroidProjectForGeneratingAar`
   * Run `./gradlew assembleRelease`
   * Copy the resulting aar to the Assets/Plugin/Android directory

2. **Build and Run**:
   * Build for Android
   * The example will:
     * Store the provided string in SharedPreferences
     * Show a toast message with the received value
     * Return a confirmation message to Unity

> ℹ️ The Android implementation uses Unity's `AndroidJavaClass` and `AndroidJavaObject` for seamless Java/Kotlin interop. No manual JNI code is required.
