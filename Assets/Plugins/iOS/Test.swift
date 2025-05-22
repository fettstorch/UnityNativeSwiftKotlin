import Foundation

/*
* This class provides a static method that can be called from Objective-C.
* It receives a String from Unity and returns a String to Unity.
* It also uses UserDefaults just to demonstrate that this is possible.
*/

// @objc is required to make things visible to Objective-C

@objc public class Test: NSObject {

	// Static method callable from Objective-C (bridged into Unity)
    // Receives a String from Unity and returns a String to Unity
	@objc public static func run(_ value: String) -> String {
		var message = ""
        // Read
        if let name = UserDefaults.standard.string(forKey: "username") {
            let foundMessage = "\(name) says hello from iOS"
            print("[TEST] " + foundMessage)
            message += "\n" + foundMessage
        } else {
            let notFoundMessage = "No username found on iOS. Writing it"
            print("[TEST] " + notFoundMessage)
            message += "\n" + notFoundMessage
        }
        
        // Write
        UserDefaults.standard.set(value, forKey: "username")
        
        return message
	}
}
