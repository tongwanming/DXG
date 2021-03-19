//
//  KVAAttribution+Internal.h
//  KochavaTracker
//
//  Created by John Bushnell on 5/18/20.
//  Copyright © 2017 - 2020 Kochava, Inc.  All rights reserved.
//



#pragma mark - IMPORT



#import "KVAAttribution.h"

#import "KVAConfigureWithObjectProtocol.h"



#pragma mark - CLASS



@class KVAAdapter;
@class KVABoolAdapter;
@class KVAConsent;
@class KVANetworking;
@class KVATask;
@class KVAValue;



#pragma mark - PROTOCOL



@protocol KVAChildObjectDidMutateProtocol;
@protocol KVADispatchQueueDelegate;
@protocol KVAInstallProvider;



@protocol KVAAttributionProvider <NSObject>



/*!
 @property attribution
 
 @brief An instance of class KVAAttribution.
 */
@property (strong, nonatomic, nonnull, readonly) KVAAttribution *attribution;



@end



@protocol KVAAttributionGeneralDelegate <NSObject, KVADispatchQueueDelegate>

- (nullable NSDecimalNumber *)startedDateAgoTimeIntervalDecimalNumber;

@end



@protocol KVAAttributionSleepDelegate <NSObject>

@property (strong, nonatomic, nonnull, readonly) KVABoolAdapter *sleepBoolAdapter;

@end



#pragma mark - INTERFACE



@interface KVAAttribution () <KVAConfigureWithObjectProtocol>



#pragma mark - TYPEDEF



/*!
 @typedef KVAAttributionResultDidRetrieveBlock
 
 @brief A block which is called when attribution is retrieved.
 */
typedef void (^ KVAAttributionResultDidRetrieveBlock) (KVAAttribution * _Nonnull attribution, KVAAttributionResult * _Nonnull attributionResult);



#pragma mark - CONFIGURATION



/*!
 @method - configureWithConsent:baseAdapter:delegate:
 
 @param consent A consent object.
 
 @param baseAdapter The baseAdapter.
 */
- (void)configureWithConsent:(nonnull KVAConsent *)consent baseAdapter:(nonnull KVAAdapter *)baseAdapter networking:(nonnull KVANetworking *)networking retrieveTaskPrerequisiteTaskArray:(nullable NSArray<KVATask *> *)retrieveTaskPrerequisiteTaskArray delegate:(nonnull id<KVAAttributionGeneralDelegate, KVAInstallProvider, KVAAttributionSleepDelegate, KVAChildObjectDidMutateProtocol, KVADispatchQueueDelegate>)delegate NS_SWIFT_NAME(configure(withConsent:baseAdapter:networking:retrieveTaskPrerequisiteTaskArray:delegate:));



#pragma mark - LIFECYCLE



/*!
 @method - invalidate
 
 @discussion Invalidates an instance.  This primarily prevents timer(s) from firing that would result in new activity.
 */
- (void)invalidate;



#pragma mark - PARAMETERS



/*!
@property didRetrieveResultBlock
 
@brief A block which is called when attribution is retrieved.
 */
@property (strong, nonatomic, nullable, readwrite) KVAAttributionResultDidRetrieveBlock didRetrieveResultBlock;



/*!
 @property enabledBool
 
 @brief A boolean indicating if the instance of KVAAttribution is enabled.
 */
@property (readonly) BOOL enabledBool;



/*!
 @property resultAdapter_initialValue
 
 @brief A value to be assigned upon initialization.
 
 @discussion Adapters cannot be made to operate through a set prior to being fully configured with a baseAdapter.  For this reason during the process of constructing an object (such as with kva_fromObject) this property may be employed to temporarily hold an initial value.  It will be assigned to the adapter's value following its natural initialization.
 */
@property (strong, nonatomic, nullable) KVAValue *resultAdapter_initialValue;



/*!
 @property resultGetNetTransactionURLString_parameter
 
 @brief A string containing the URL to get the attribution result.
 */
@property (strong, nonatomic, nullable) NSString *resultGetNetTransactionURLString_parameter;



/*!
 @property stalenessTimeIntervalNumber
 
 @brief The time interval after which a location sample gathering period times out.  An NSNumber wrapping an NSTimeInterval.
 */
@property (strong, nonatomic, nullable) NSDecimalNumber *stalenessTimeIntervalNumber;



#pragma mark - PROPERTIES



/*!
 @property resultAdapter
 
 @brief A dictionary containing the current Apple Search Ads result information which is pending to be sent up to the server in the install.
 */
@property (strong, nonatomic, nonnull, readonly) KVAAdapter *resultAdapter;



/*!
 @property resultRetrieveTask
 
 @brief A task that gets the value of attribution.resultAdapter.
 
 @discussion When complete, attribution.resultAdapter will contain a value with a KVAAttributionResult.
 */
@property (strong, nonatomic, nonnull, readonly) KVATask *resultRetrieveTask;



#pragma mark - GENERAL



/*!
 @method - valueSourceArrayDictionary
 
 @brief Returns a dictionary of value sources for this class instance.
 */
- (nonnull NSDictionary *)valueSourceArrayDictionary;



/*!
 @method - waitTimeInterval
 
 @brief Returns a time interval to wait prior to retrieving an attribution result.
 
 @discussion This time interval starts from the time when the install completes being sent.
 */
- (NSTimeInterval)waitTimeInterval;



@end



