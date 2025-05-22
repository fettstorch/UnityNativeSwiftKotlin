#import <Foundation/Foundation.h>

// This Objective-C interface serves as a required bridge between C (which Unity calls) and Swift.
// Swift methods cannot be called directly from a C function, so this class handles the call.
@interface MyPluginBridge : NSObject

// Calls the Swift method and returns its result as NSString*
+ (NSString*)runSwiftCode:(NSString*)value;

@end

// This is the C-callable function that Unity invokes via DllImport.
// It converts const char* to NSString*, passes it to the Obj-C method,
// and returns the Swift result as const char*.
const char* _RunSwiftCode(const char* value);