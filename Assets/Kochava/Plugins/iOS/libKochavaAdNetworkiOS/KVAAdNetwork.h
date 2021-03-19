//
//  KVAAdNetwork.h
//  KochavaAdNetwork
//
//  Created by John Bushnell on 8/20/20.
//  Copyright © 2020 - 2021 Kochava, Inc.  All rights reserved.
//



#ifndef KVAAdNetwork_h
#define KVAAdNetwork_h



#pragma mark - IMPORT



#ifdef KOCHAVA_FRAMEWORK
#import <KochavaCore/KochavaCore.h>
#else
#import "KVAAsForContextObjectProtocol.h"
#import "KVAConfigureWithObjectProtocol.h"
#import "KVAFromObjectProtocol.h"
#endif



#pragma mark - INTERFACE



/*!
 @class KVAAdNetwork
 
 @brief A controller for working with advertising networks.
 
 @discussion This class provides a high-level interface for SKAdNetwork.
 
 @author John Bushnell
 
 @copyright 2020 - 2021 Kochava, Inc.
 */
@interface KVAAdNetwork : NSObject <KVAAsForContextObjectProtocol, KVAConfigureWithObjectProtocol, KVAFromObjectProtocol>



@end



#endif



