#import "MyPluginBridge.h"
#import "UnityAppcontroller.h"
#import "UnityFramework/UnityFramework-Swift.h"

@implementation MyPluginBridge

// Objective-C class method that safely calls into Swift.
// Required because Swift methods must be called via Objective-C, not plain C.
+ (NSString*)runSwiftCode:(NSString*)value {
	return [Test run:value];
}
@end

// Static variable to retain the Swift-returned NSString.
// Required because the return value from [NSString UTF8String] is a raw C pointer.
// If we don't retain the NSString, that pointer could become dangling or invalid.
// Making it static ensures the NSString stays alive long enough for Unity to read the result.
static NSString* _cachedResult = nil;

// C-callable function, exposed to Unity.
// Converts incoming const char* to NSString*,
// delegates the call to the Obj-C method (which calls Swift),
// and returns the result as const char* for Unity.
const char* _RunSwiftCode(const char* value) {
	NSString* str = [NSString stringWithUTF8String:value];
	_cachedResult = [MyPluginBridge runSwiftCode:str];
	return _cachedResult.UTF8String;
}
