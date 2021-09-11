using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 事件中心扩展函数
/// </summary>
public static class EventCenterExtension
{
    public static void Foreach(this Enum eventType, EventAction<Delegate> action)
    {
        EventCenter.Instance.Foreach(eventType, action);
    }

    public static void Foreach(this Enum eventType, EventAction<int, Delegate> action)
    {
        EventCenter.Instance.Foreach(eventType, action);
    }

    public static void InvertedForeach(this Enum eventType, EventAction<Delegate> action)
    {
        EventCenter.Instance.InvertedForeach(eventType, action);
    }

    public static void InvertedForeach(this Enum eventType, EventAction<int, Delegate> action)
    {
        EventCenter.Instance.InvertedForeach(eventType, action);
    }
}
