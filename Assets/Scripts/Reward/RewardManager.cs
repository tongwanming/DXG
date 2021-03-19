using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

/**
 * 奖励管理
 */
public class RewardManager {
    private static List<RewardInfo> mCurrentList;

    private static string readFile() {
        TextAsset textAsset = Resources.Load<TextAsset>("File/number");
        return textAsset.text;
    }

    private static List<RewardInfo> getAllReward() {
        var res = readFile();
        List<RewardInfo> rewardInfos = JsonMapper.ToObject<List<RewardInfo>>(res);
        return rewardInfos;
    }

    /**
     * 获取下一次的奖励值
     */
    public static RewardInfo getNextReward(double currentMoney) {
        if (mCurrentList == null) {
            mCurrentList = getAllReward();
        }

        RewardInfo reward = null;
        foreach (var info in mCurrentList) {
            if (!(info.money > currentMoney)) continue;
            reward = info;
            break;
        }

        return reward ?? (reward = new RewardInfo {reward = 0f});
    }
}