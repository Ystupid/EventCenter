using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    DestoryEntity,
    StageClear,
    GameOver,
    TaskIncrement,


    /// <summary>
    /// 刷新存钱罐
    /// </summary>
    RefreshSavingPot,

    /// <summary>
    /// 刷新货币
    /// </summary>
    RefreshCurrency,

    /// <summary>
    /// 货币增加
    /// </summary>
    CurrencyIncrement,

    /// <summary>
    /// 刷新关卡红包
    /// </summary>
    RefreshLevelRedPacket,
}
