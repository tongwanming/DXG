#import "KochavaTracker.h"
#import "KVAAttribution+Internal.h"
#import "KochavaAdNetwork.h"

char* AutonomousStringCopy (const char* string)
{
    if (string == NULL)
        return NULL;

    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

@interface NativeWrapper: NSObject

@end

@implementation NativeWrapper

// Decodes the attribution result from the callback or getter and returns a string.
+ (NSString *)decodeAttributionResult:(KVAAttributionResult *)attributionResult {
    if(!attributionResult.attributedBool) {
        return @"{\"attribution\":\"false\"}";
    }
    
    NSDictionary *attributionDictionary = attributionResult.rawDictionary;
    if (attributionDictionary == nil || ![NSJSONSerialization isValidJSONObject:attributionDictionary])
    {
        return @"";
    }
    return [NativeWrapper serializeNSDictionary:attributionDictionary];
}

// Serialize an NSDictionary into an NSString
+ (NSString *) serializeNSDictionary:(NSDictionary *)dictionary {
    NSError *error = nil;
    NSData *dictionaryJSONData = [NSJSONSerialization dataWithJSONObject:dictionary options:0 error:&error];
    if(dictionaryJSONData == nil) {
        return @"";
    }

    NSString *result = [[NSString alloc] initWithData:dictionaryJSONData encoding:NSUTF8StringEncoding];
    if(result == nil) {
        return @"";
    }

    return result;
}

@end

// convert a c# stringified dictionary to an NSDictionary
NSMutableDictionary * ConvertToNSDictionary(const char *stringifiedDictionary) {
    if(stringifiedDictionary == NULL)
    {
        return nil;
    }

    NSString *str = [NSString stringWithUTF8String:stringifiedDictionary];
    NSData* data = [str dataUsingEncoding:NSUTF8StringEncoding];

    NSError *jsonSerializationError;
    id responseObject = [NSJSONSerialization JSONObjectWithData: data options: NSJSONReadingAllowFragments|NSJSONReadingMutableContainers error: &jsonSerializationError];

    return responseObject;
}

extern "C" {

// migrate the previously persisted data from unity v1
void nativeMigrate()
{
    // MIGRATE LEGACY UNITY SDK'S NSUSERDEFAULTS
    // oldDeviceIdStringKey, oldWatchlistPropertiesKey, oldKochavaQueueStorageKey, and oldAttributionDictionaryStringKey
    NSString * const oldDeviceIdStringKey = @"kochava_device_id";

    NSString * const oldWatchlistPropertiesKey = @"watchlistProperties";

    NSString * const oldKochavaQueueStorageKey = @"kochava_queue_storage";

    NSString * const oldAttributionDictionaryStringKey = @"attribution";

    // oldDeviceIdString
    NSString *oldDeviceIdString = [NSUserDefaults.standardUserDefaults objectForKey:oldDeviceIdStringKey];

    // Discussion:  We only proceed if we find an oldDeviceIdString.  If we don't, we assume that this is either a new install or else already migrated.
    if (oldDeviceIdString != nil)
    {
        // watchlistPropertiesObject
        NSObject *watchlistPropertiesObject = [NSUserDefaults.standardUserDefaults objectForKey:oldWatchlistPropertiesKey];

        // oldKochavaQueueStorageObject
        NSObject *oldKochavaQueueStorageObject = [NSUserDefaults.standardUserDefaults objectForKey:oldKochavaQueueStorageKey];

        // oldKochavaQueueStorageString
        NSString *oldKochavaQueueStorageString = nil;

        if ([oldKochavaQueueStorageObject isKindOfClass:NSString.class])
        {
            oldKochavaQueueStorageString = (NSString *)oldKochavaQueueStorageObject;
        }

        // watchlistPropertiesExistsBool
        BOOL watchlistPropertiesExistsBool = (watchlistPropertiesObject != nil);

        // oldKochavaQueueStorageContainsInitialBool
        BOOL oldKochavaQueueStorageContainsInitialBool = NO;

        if ((oldKochavaQueueStorageString != nil) && (oldKochavaQueueStorageString.length > 0))
        {
            NSRange range = [oldKochavaQueueStorageString rangeOfString:@"initial" options:NSCaseInsensitiveSearch];

            if (range.location != NSNotFound)
            {
                oldKochavaQueueStorageContainsInitialBool = YES;
            }
        }

        // oldAttributionObject
        NSObject *oldAttributionObject = [NSUserDefaults.standardUserDefaults objectForKey:oldAttributionDictionaryStringKey];

        // oldAttributionString
        NSString *oldAttributionString = nil;

        if ([oldAttributionObject isKindOfClass:NSString.class])
        {
            oldAttributionString = (NSString *)oldAttributionObject;
        }

        // oldAttributionDictionary
        NSDictionary *oldAttributionDictionary = nil;

        if ([oldAttributionObject isKindOfClass:NSDictionary.class])
        {
            oldAttributionDictionary = (NSDictionary *)oldAttributionObject;
        }

        // oldAttributionStringData
        NSData *oldAttributionStringData = nil;

        if ((oldAttributionDictionary == nil) && (oldAttributionString != nil))
        {
            oldAttributionStringData = [oldAttributionString dataUsingEncoding:NSUTF8StringEncoding];
        }

        // attributionJSONObject and oldAttributionStringDataError
        id oldAttributionJSONObject = nil;

        NSError *oldAttributionStringDataError = nil;

        if (oldAttributionStringData != nil)
        {
            oldAttributionJSONObject = [NSJSONSerialization JSONObjectWithData:oldAttributionStringData options:NSJSONReadingMutableContainers error:&oldAttributionStringDataError];
        }

        // oldAttributionJSONDictionary
        NSDictionary *oldAttributionJSONDictionary = nil;

        if ([oldAttributionJSONObject isKindOfClass:NSDictionary.class])
        {
            oldAttributionJSONDictionary = (NSDictionary *)oldAttributionJSONObject;
        }

        // newAttributionDictionary
        NSDictionary *newAttributionDictionary = nil;

        if (oldAttributionDictionary != nil)
        {
            newAttributionDictionary = oldAttributionDictionary;
        }
        else if (oldAttributionJSONDictionary != nil)
        {
            newAttributionDictionary = oldAttributionJSONDictionary;
        }
        else if (oldAttributionString != nil)
        {
            newAttributionDictionary = @{ @"attribution": oldAttributionString };
        }

        // installNetTransactionFirstCompletedBool
        BOOL installNetTransactionFirstCompletedBool = (watchlistPropertiesExistsBool && !oldKochavaQueueStorageContainsInitialBool);

        // deviceIdStringAdapterDictionary
        NSDictionary *deviceIdStringAdapterDictionary = nil;

        if (oldDeviceIdString != nil)
        {
            NSMutableDictionary *deviceIdStringAdapterValueDictionary = NSMutableDictionary.dictionary;
            deviceIdStringAdapterValueDictionary[@"rawObject"] = oldDeviceIdString;
            deviceIdStringAdapterValueDictionary[@"valueSourceNameString"] = @"Tracker.deviceIdStringAdapter";
            deviceIdStringAdapterValueDictionary[@"serverObject"] = oldDeviceIdString;
            deviceIdStringAdapterValueDictionary[@"startDate"] = NSDate.date; // Normally a iso8601DateString now, but NSDate is also supported.

            deviceIdStringAdapterDictionary = @{ @"value" :  deviceIdStringAdapterValueDictionary };
        }

        // installSentBoolAdapterValueDictionary
        NSMutableDictionary *installSentBoolAdapterValueDictionary = NSMutableDictionary.dictionary;
        installSentBoolAdapterValueDictionary[@"rawObject"] = @(installNetTransactionFirstCompletedBool);
        installSentBoolAdapterValueDictionary[@"valueSourceNameString"] = @"Tracker.installSentBoolAdapter";
        installSentBoolAdapterValueDictionary[@"serverObject"] = @(installNetTransactionFirstCompletedBool);
        installSentBoolAdapterValueDictionary[@"startDate"] = NSDate.date; // Normally a iso860DateString now, but NSDate is also supported.

        // installSentBoolAdapterDictionary
        NSDictionary *installSentBoolAdapterDictionary = @{ @"value" : installSentBoolAdapterValueDictionary  };

        // attributionDictionaryAdapterDictionary
        NSDictionary *attributionDictionaryAdapterDictionary = nil;

        if (newAttributionDictionary != nil)
        {
            NSMutableDictionary *attributionDictionaryAdapterValueDictionary = NSMutableDictionary.dictionary;
            attributionDictionaryAdapterValueDictionary[@"rawObject"] = newAttributionDictionary;
            attributionDictionaryAdapterValueDictionary[@"valueSourceNameString"] = @"Tracker.attributionDictionaryAdapter";
            attributionDictionaryAdapterValueDictionary[@"serverObject"] = newAttributionDictionary;
            attributionDictionaryAdapterValueDictionary[@"startDate"] = NSDate.date; // Normally a iso8601DateString now, but NSDate is also supported.

            attributionDictionaryAdapterDictionary = @{ @"value" : attributionDictionaryAdapterValueDictionary  };
        }

        // NSUserDefaults.standardUserDefaults
        // ... set the new keys
        if (attributionDictionaryAdapterDictionary != nil)
        {
            [NSUserDefaults.standardUserDefaults setObject:attributionDictionaryAdapterDictionary forKey:@"com.kochava.KochavaTracker.Tracker.attributionDictionaryAdapter"];
        }

        [NSUserDefaults.standardUserDefaults setObject:installSentBoolAdapterDictionary forKey:@"com.kochava.KochavaTracker.Tracker.installSentBoolAdapter"];

        if (deviceIdStringAdapterDictionary != nil)
        {
            [NSUserDefaults.standardUserDefaults setObject:deviceIdStringAdapterDictionary forKey:@"com.kochava.KochavaTracker.Tracker.deviceIdStringAdapter"];
        }

        // ... remove the old keys
        [NSUserDefaults.standardUserDefaults removeObjectForKey:oldAttributionDictionaryStringKey];

        [NSUserDefaults.standardUserDefaults removeObjectForKey:oldKochavaQueueStorageKey];

        [NSUserDefaults.standardUserDefaults removeObjectForKey:oldWatchlistPropertiesKey];

        [NSUserDefaults.standardUserDefaults removeObjectForKey:oldDeviceIdStringKey];
    }
}

// Execute an advanced sdk instruction. Primarily used for testing.
void iOSNativeExecuteAdvancedInstruction(const char *key, const char *command)
{
    NSString *keyString = [NSString stringWithUTF8String:key];
    NSString *commandString = [NSString stringWithUTF8String:command];
    
    // Check for the existence of the hidden unconfigure key. Note: This will be migrated away to a proper shutdown method in the future.
    if ([@"INTERNAL_UNCONFIGURE" isEqualToString:keyString])
    {
        NSLog(@"KochavaWrapper.configure UnConfigure.");
        [KVATrackerProduct.shared shutdownWithDeleteLocalDataBool:NO];
        return;
    }

    // Check for the existence of the hidden reset key. Note: This will be migrated away to a proper shutdown method in the future.
    if ([@"INTERNAL_RESET" isEqualToString:keyString])
    {
        NSLog(@"KochavaWrapper.configure Reset.");
        [KVATrackerProduct.shared shutdownWithDeleteLocalDataBool:YES];
        return;
    }
    
    [KVATracker.shared executeAdvancedInstructionWithIdentifierString:keyString valueObject:commandString];
}

// Sets the container app group identifier for use in App Clip to Full App conversions.
void iOSNativeSetContainerAppGroupIdentifier(const char *identifier)
{
    NSString *identifierString = nil;
    if(identifier != NULL)
    {
        identifierString = [NSString stringWithUTF8String:identifier];
    }
    if(identifierString == nil)
    {
        return;
    }

    KVAAppGroups.shared.deviceAppGroupIdentifierString = identifierString;
}

// Enable ATT auto request.
void iOSNativeEnableAppTrackingTransparencyAutoRequest()
{
    KVATracker.shared.appTrackingTransparency.autoRequestTrackingAuthorizationBool = YES;
}

// Set all ATT settings.
void iOSNativeSetAppTrackingTransparency(bool enabled, double waitTime, bool autoRequest)
{
    KVATracker.shared.appTrackingTransparency.enabledBool = enabled;
    KVATracker.shared.appTrackingTransparency.authorizationStatusWaitTimeInterval = waitTime;
    KVATracker.shared.appTrackingTransparency.autoRequestTrackingAuthorizationBool = autoRequest;
}

// Set the sdk log level.
void iOSNativeSetLogLevel(const char *logLevel)
{
    NSString *logLevelString = [NSString stringWithUTF8String:logLevel];
    if ([@"never" isEqualToString:logLevelString])
    {
        [KVALog.shared setLevel:KVALogLevel.never];
    } else if ([@"error" isEqualToString:logLevelString])
    {
        [KVALog.shared setLevel:KVALogLevel.error];
    } else if ([@"warn" isEqualToString:logLevelString])
    {
        [KVALog.shared setLevel:KVALogLevel.warn];
    } else if ([@"info" isEqualToString:logLevelString])
    {
        [KVALog.shared setLevel:KVALogLevel.info];
    } else if ([@"debug" isEqualToString:logLevelString])
    {
        [KVALog.shared setLevel:KVALogLevel.debug];
    } else if ([@"trace" isEqualToString:logLevelString])
    {
        [KVALog.shared setLevel:KVALogLevel.trace];
    }
}

void iOSNativeStartWithAppGuid(const char *appGuid)
{
    // migrate settings from the previous v1 unity sdk if applicable
    nativeMigrate();
    
    // Register the ad network product for SKaD support.
    [KVAAdNetworkProduct.shared register];

    // Start
    NSString *appGuidString = [NSString stringWithUTF8String:appGuid];
    [KVATracker.shared startWithAppGUIDString:appGuidString];
}

void iOSNativeStartWithPartnerName(const char *partnerName)
{
    // migrate settings from the previous v1 unity sdk if applicable
    nativeMigrate();
    
    // Register the ad network product for SKaD support.
    [KVAAdNetworkProduct.shared register];

    // Start
    NSString *partnerNameString = [NSString stringWithUTF8String:partnerName];
    [KVATracker.shared startWithPartnerNameString:partnerNameString];
}

void iOSNativeSetCustom(const char *custom)
{
    NSMutableDictionary *customDictionary = ConvertToNSDictionary(custom);
    for(id key in customDictionary)
    {
        NSString *value = customDictionary[key];
        [KVATracker.shared.customIdentifiers registerWithNameString:key identifierString:value];
    }
}

void iOSNativeRequestAttribution()
{
    KVATracker.shared.attribution.retrieveResultBool = YES;
    KVATracker.shared.attribution.didRetrieveResultBlock = ^(KVAAttribution * _Nonnull attribution, KVAAttributionResult * _Nonnull attributionResult)
    {
        // For legacy purposes this needs to be made to look like the v3 response until we change the attribution handler over on the Unity side.
        NSString * attributionString = [NativeWrapper decodeAttributionResult:attributionResult];
     
        // send this message back to the host app, which must always have a game object and listener method with these names
        const char* a = "KochavaTracker";
        const char* b = "KochavaAttributionListener";
        UnitySendMessage(a, b, AutonomousStringCopy([attributionString UTF8String]));
    };
}

void iOSNativeSetIcm(bool icmEnabled, bool manualManaged)
{
    KVATracker.shared.consent.intelligentManagementBool = icmEnabled;
    KVATracker.shared.consent.manualManagedRequirementsBool = manualManaged;
    if(icmEnabled)
    {
        KVATracker.shared.consent.didUpdateBlock = ^(KVAConsent * _Nonnull consent)
        {
         const char* a = "KochavaTracker";
         const char* b = "KochavaConsentStatusChangeListener";
         UnitySendMessage(a, b, AutonomousStringCopy([@"{}" UTF8String]));
        };
    }
}

void iOSNativeSendEvent(const char *eventName, const char *eventInfo)
{
    NSString *evName = nil;
    if(eventName != NULL)
    {
        evName = [NSString stringWithUTF8String:eventName];
    }
    NSString *evInfo = nil;
    if(eventInfo != NULL)
    {
        evInfo = [NSString stringWithUTF8String:eventInfo];
    }

    [KVAEvent sendCustomWithNameString:evName infoString:evInfo];
}

void iOSNativeSendKochavaEvent(const char *eventName, const char *kochavaEventStringifiedDictionary, const char *appStoreReceiptBase64EncodedString)
{
    // create a native KochavaEvent.
    NSString *customEventName = [NSString stringWithUTF8String:eventName];
    KVAEvent *event = [KVAEvent customEventWithNameString:customEventName];
    if(event == nil)
    {
        return;
    }

    // Set the parameters dictionary.
    NSMutableDictionary *stdParamsDictionary = ConvertToNSDictionary(kochavaEventStringifiedDictionary);
    if (stdParamsDictionary != nil)
    {
        event.infoDictionary = stdParamsDictionary;
    }

    // If a receipt exists then set it.
    if(appStoreReceiptBase64EncodedString != NULL)
    {
        event.appStoreReceiptBase64EncodedString = [NSString stringWithUTF8String:appStoreReceiptBase64EncodedString];
    }

    // now send it
    [event send];
}

void iOSNativeSendDeepLink(const char *openURL, const char *sourceApplicationString)
{
    // Decode the url (can be nil)
    NSString *strOpenUrl = nil;
    if(openURL != NULL)
    {
        strOpenUrl = [NSString stringWithUTF8String:openURL];
    }

    // Decode the source application (can be nil)
    NSString *strSourceApplicationString = nil;
    if(sourceApplicationString != NULL)
    {
        strSourceApplicationString = [NSString stringWithUTF8String:sourceApplicationString];
    }

    // Create and send the deeplink event.
    KVAEvent *event = [KVAEvent eventWithType:KVAEventType.deeplink];
    event.uriString = strOpenUrl;
    event.sourceString = strSourceApplicationString;
    [event send];
}

void iOSNativeSendIdentityLink(const char *identityLinkDictionary)
{
    NSMutableDictionary *sendIdLinkDictionary = ConvertToNSDictionary(identityLinkDictionary);
    for(id key in sendIdLinkDictionary)
    {
        NSString *value = sendIdLinkDictionary[key];
        [[KVATracker.shared identityLink] registerWithNameString:key identifierString:value];
    }
}

char* iOSNativeGetDeviceId()
{
    NSString *kochavaTrackerDeviceIdString = KVATracker.shared.deviceIdString;
    if(kochavaTrackerDeviceIdString == nil)
    {
        kochavaTrackerDeviceIdString = @"";
    }
    return AutonomousStringCopy([kochavaTrackerDeviceIdString UTF8String]);
}

char* iOSNativeGetAttributionString()
{
    NSString * attributionString = [NativeWrapper decodeAttributionResult:KVATracker.shared.attribution.result];
    return AutonomousStringCopy([attributionString UTF8String]);
}

void iOSNativeSetAppLimitAdTrackingBool(bool value) {
    [KVATracker.shared setAppLimitAdTrackingBool:value];
}

void iOSNativeSetSleepBool(bool value) {
    [KVATracker.shared setSleepBool:value];
}

bool iOSNativeGetSleepBool() {
    return [KVATracker.shared sleepBool];
}

char* iOSNativeGetVersion()
{
    NSString *kochavaTrackerVersionString = KVATracker.shared.sdkVersionString;
    if(kochavaTrackerVersionString == nil)
    {
        kochavaTrackerVersionString = @"";
    }
    return AutonomousStringCopy([kochavaTrackerVersionString UTF8String]);
}

void iOSNativeAddPushToken(char* bytes, unsigned long length)
{
    NSData *deviceToken = nil;
    if(bytes != NULL)
    {
        deviceToken = [NSData dataWithBytesNoCopy:bytes length:length freeWhenDone:NO];
    }
    [KVAPushNotificationsToken addWithData:deviceToken];
}

void iOSNativeRemovePushToken(char* bytes, unsigned long length)
{
    NSData *deviceToken = nil;
    if(bytes != NULL)
    {
        deviceToken = [NSData dataWithBytesNoCopy:bytes length:length freeWhenDone:NO];
    }
    [KVAPushNotificationsToken removeWithData:deviceToken];
}

char* iOSNativeGetConsentDescription()
{
    NSString *descriptionString = KVATracker.shared.consent.descriptionString;
    if(descriptionString == nil)
    {
        descriptionString = @"";
    }
    return AutonomousStringCopy([descriptionString UTF8String]);
}

long iOSNativeGetConsentResponseTime()
{
    NSDate *didRespondDate = KVATracker.shared.consent.responseDate;
    if(didRespondDate == nil)
    {
        return 0;
    }
    return [@([didRespondDate timeIntervalSince1970]) longValue];
}

bool iOSNativeGetConsentRequired()
{
    return KVATracker.shared.consent.requiredBool;
}

void iOSNativeSetConsentRequired(bool value) {
    KVATracker.shared.consent.requiredBool = value;
}

bool iOSNativeGetConsentGranted()
{
    return KVATracker.shared.consent.isGrantedBool;
}

char* iOSNativeGetConsentPartnersJson()
{
    NSString *consentPartnersString = nil;
    NSObject *consentPartnersAsForContextObject = [(NSObject<KVAAsForContextObjectProtocol> *)KVATracker.shared.consent.partnerArray kva_asForContextObjectWithContext:KVAContext.sdkWrapper];
    if (consentPartnersAsForContextObject != nil)
    {
        NSError *error = nil;
        NSData *consentPartnersJSONData = [NSJSONSerialization dataWithJSONObject:consentPartnersAsForContextObject options:0 error:&error];

        if (consentPartnersJSONData != nil)
        {
            consentPartnersString = [[NSString alloc] initWithData:consentPartnersJSONData encoding:NSUTF8StringEncoding];
        }
    }

    if(consentPartnersString == nil)
    {
        consentPartnersString = @"[]";
    }
    return AutonomousStringCopy([consentPartnersString UTF8String]);
}

void iOSNativeSetConsentGranted(bool isGranted)
{
    NSNumber *consentGranted = @(isGranted);
    if(consentGranted != nil)
    {
        [KVATracker.shared.consent didPromptWithDidGrantBoolNumber:consentGranted];
    }
}

bool iOSNativeGetConsentShouldPrompt()
{
    return KVATracker.shared.consent.shouldPromptBool;
}

void iOSNativeSetConsentPrompted()
{
    [KVATracker.shared.consent willPrompt];
}

bool iOSNativeGetConsentRequirementsKnown()
{
    return KVATracker.shared.consent.requiredBoolNumber != nil;
}

void iOSNativeProcessDeeplink(const char *requestId, const char *path, double timeout)
{
    NSString *requestIdString = nil;
    if(requestId != NULL)
    {
        requestIdString = [NSString stringWithUTF8String:requestId];
    }
    NSString *pathString = nil;
    if(path != NULL)
    {
        pathString = [NSString stringWithUTF8String:path];
    }
    NSURL *pathUrl = nil;
    if (pathString.length > 0)
    {
        pathUrl = [NSURL URLWithString:pathString];
    }
    NSTimeInterval timeoutTimeInterval = timeout;

    // Process the deeplink.
    [KVADeeplink processWithURL:pathUrl timeoutTimeInterval:timeoutTimeInterval completionHandler:^(KVADeeplink * _Nonnull deeplink) {
        // Serialize the response deeplink inside a dictionary with the request id.
        NSObject *deeplinkAsForContextObject = [deeplink kva_asForContextObjectWithContext:KVAContext.sdkWrapper];
        NSDictionary *deeplinkDictionary = [deeplinkAsForContextObject isKindOfClass:NSDictionary.class] ? (NSDictionary *)deeplinkAsForContextObject : nil;

        NSMutableDictionary *responseDictionary = [[NSDictionary alloc] init].mutableCopy;
        responseDictionary[@"requestId"] = requestIdString;
        responseDictionary[@"deeplink"] = [NativeWrapper serializeNSDictionary: deeplinkDictionary];

        NSString *response = [NativeWrapper serializeNSDictionary: responseDictionary];
        if(response.length == 0) {
            response = @"{}";
        }

        // send this message back to the host app, which must always have a game object and listener method with these names
        const char* a = "KochavaTracker";
        const char* b = "KochavaDeeplinkListener";
        UnitySendMessage(a, b, AutonomousStringCopy([response UTF8String]));
    }];
}

}
