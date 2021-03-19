//
//  KVAAdNetworkProduct.h
//  KochavaAdNetwork
//
//  Created by John Bushnell on 8/20/20.
//  Copyright © 2020 - 2021 Kochava, Inc.  All rights reserved.
//



#ifndef KVAAdNetworkProduct_h
#define KVAAdNetworkProduct_h



#pragma mark - IMPORT



#ifdef KOCHAVA_FRAMEWORK
#import <KochavaCore/KochavaCore.h>
#else
#import "KVAProduct.h"
#import "KVASharedPropertyProvider.h"
#endif



#pragma mark - INTERFACE



/*!
 @class KVAAdNetworkProduct
 
 @brief A class which defines an adnetwork product.
 
 @discussion A product in this context generally refers to the result of a build.
 
 Inherits from: KVAProduct
 
 @author John Bushnell
 
 @copyright 2020 - 2021 Kochava, Inc.
 */
@interface KVAAdNetworkProduct : KVAProduct <KVASharedPropertyProvider>



#pragma mark - SHARED INSTANCE (SINGLETON)



/*!
 @property shared
 
 @brief The singleton shared instance.
 */
@property (class, readonly, strong, nonnull) KVAAdNetworkProduct *shared;



@end



#endif



