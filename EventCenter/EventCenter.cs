using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventCenter : Singleton<EventCenter>
{
    private Dictionary<ForeachType, IForeachable<Delegate>> m_ForeachMap;
    private Dictionary<Enum, Type> m_TypeMap;
    private Dictionary<Type, Dictionary<Enum, List<Delegate>>> m_EventMap;

    protected override void Init()
    {
        base.Init();
        if (m_TypeMap == null) m_TypeMap = new Dictionary<Enum, Type>();
        if (m_EventMap == null) m_EventMap = new Dictionary<Type, Dictionary<Enum, List<Delegate>>>();
        if(m_ForeachMap == null)
        {
            m_ForeachMap = new Dictionary<ForeachType, IForeachable<Delegate>>();
            m_ForeachMap.Add(ForeachType.Inverted, new InvertedForeach());
            m_ForeachMap.Add(ForeachType.Positive, new PositiveForeach());
        }
    }

    public Type GetType(Enum eventType)
    {
        if (!m_TypeMap.ContainsKey(eventType))
            m_TypeMap.Add(eventType, eventType.GetType());
        return m_TypeMap[eventType];
    }

    #region RegisterEvent
    private List<Delegate> RegisterKey(Enum eventType)
    {
        var type = GetType(eventType);

        if (!m_EventMap.ContainsKey(type))
            m_EventMap.Add(type, new Dictionary<Enum, List<Delegate>>());

        if (!m_EventMap[type].ContainsKey(eventType))
            m_EventMap[type].Add(eventType, new List<Delegate>());

        return m_EventMap[type][eventType];
    }
    private void Internal_RegisterEvent(Enum eventType, Delegate action)
    {
        RegisterKey(eventType).Add(action);
    }
    public void RegisterEvent(Enum eventType, EventAction action) => Internal_RegisterEvent(eventType, action);
    public void RegisterEvent<T>(Enum eventType, EventAction<T> action) => Internal_RegisterEvent(eventType, action);
    public void RegisterEvent<T1, T2>(Enum eventType, EventAction<T1, T2> action) => Internal_RegisterEvent(eventType, action);
    public void RegisterEvent<T1, T2, T3>(Enum eventType, EventAction<T1, T2, T3> action) => Internal_RegisterEvent(eventType, action);
    public void RegisterEvent<T1, T2, T3, T4>(Enum eventType, EventAction<T1, T2, T3, T4> action) => Internal_RegisterEvent(eventType, action);
    public void RegisterEvent<T1, T2, T3, T4, T5>(Enum eventType, EventAction<T1, T2, T3, T4, T5> action) => Internal_RegisterEvent(eventType, action);
    public void RegisterEvent<T1, T2, T3, T4, T5, T6>(Enum eventType, EventAction<T1, T2, T3, T4, T5, T6> action) => Internal_RegisterEvent(eventType, action);
    #endregion

    #region TriggerEvent
    public void TriggerEvent(Enum eventType)
    {
        eventType.Foreach(action =>
        {
            if (action is EventAction)
            {
                var callback = action as EventAction;
                callback?.Invoke();
            }
        });
    }
    public void TriggerEvent<T>(Enum eventType, T arg)
    {
        eventType.Foreach(action =>
        {
            if (action is EventAction<T>)
            {
                var callback = action as EventAction<T>;
                callback?.Invoke(arg);
            }
        });
    }
    public void TriggerEvent<T1, T2>(Enum eventType, T1 arg0, T2 arg1)
    {
        eventType.Foreach(action =>
        {
            if (action is EventAction<T1, T2>)
            {
                var callback = action as EventAction<T1, T2>;
                callback?.Invoke(arg0, arg1);
            }
        });
    }
    public void TriggerEvent<T1, T2, T3>(Enum eventType, T1 arg0, T2 arg1, T3 arg2)
    {
        eventType.Foreach(action =>
        {
            if (action is EventAction<T1, T2, T3>)
            {
                var callback = action as EventAction<T1, T2, T3>;
                callback?.Invoke(arg0, arg1, arg2);
            }
        });
    }
    public void TriggerEvent<T1, T2, T3, T4>(Enum eventType, T1 arg0, T2 arg1, T3 arg2, T4 arg3)
    {
        eventType.Foreach(action =>
        {
            if (action is EventAction<T1, T2, T3, T4>)
            {
                var callback = action as EventAction<T1, T2, T3, T4>;
                callback?.Invoke(arg0, arg1, arg2, arg3);
            }
        });
    }
    public void TriggerEvent<T1, T2, T3, T4, T5>(Enum eventType, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4)
    {
        eventType.Foreach(action =>
        {
            if (action is EventAction<T1, T2, T3, T4, T5>)
            {
                var callback = action as EventAction<T1, T2, T3, T4, T5>;
                callback?.Invoke(arg0, arg1, arg2, arg3, arg4);
            }
        });
    }
    public void TriggerEvent<T1, T2, T3, T4, T5, T6>(Enum eventType, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5)
    {
        eventType.Foreach(action =>
        {
            if (action is EventAction<T1, T2, T3, T4, T5, T6>)
            {
                var callback = action as EventAction<T1, T2, T3, T4, T5, T6>;
                callback?.Invoke(arg0, arg1, arg2, arg3, arg4, arg5);
            }
        });
    }
    #endregion

    #region RemoveEvent
    public void RemoveEvent(Enum eventType, Delegate action)
    {
        eventType.InvertedForeach((index, callback) =>
        {
            if (Equals(callback, action))
            {
                var type = GetType(eventType);
                m_EventMap[type][eventType].RemoveAt(index);
                return;
            }
        });
    }

    public void RemoveEvents(Enum eventType)
    {
        var type = GetType(eventType);

        eventType.InvertedForeach((index, callback) =>
        {
            m_EventMap[type][eventType].RemoveAt(index);
        });
    }

    public void Clear()
    {
        foreach (var eventList in m_EventMap.Values)
            eventList.Clear();
    }
    #endregion

    public int EventCount(Enum eventType)
    {
        var type = GetType(eventType);

        if (!m_EventMap.ContainsKey(type)) return 0;
        if (!m_EventMap[type].ContainsKey(eventType)) return 0;

        return m_EventMap[type][eventType].Count;
    }

    #region Foreach
    public void Foreach(Enum eventType, EventAction<Delegate> action)
    {
        if (action == null) return;
        Foreach(eventType, (index, _Delegate) => action(_Delegate));
    }
    public void Foreach(Enum eventType, EventAction<int, Delegate> action)
    {
        var type = GetType(eventType);
        if (!m_EventMap[type].ContainsKey(eventType))
            return;

        var eventList = m_EventMap[type][eventType];

        m_ForeachMap[ForeachType.Positive].Foreach(eventList, (index, _Delegate) =>
        {
            action(index,_Delegate);
        });
    }

    public void InvertedForeach(Enum eventType, EventAction<Delegate> action)
    {
        if (action == null) return;
        InvertedForeach(eventType, (index, _Delegate) => action(_Delegate));
    }

    public void InvertedForeach(Enum eventType, EventAction<int, Delegate> action)
    {
        var type = GetType(eventType);
        if (!m_EventMap[type].ContainsKey(eventType))
            return;

        var eventList = m_EventMap[type][eventType];

        m_ForeachMap[ForeachType.Inverted].Foreach(eventList, (index, _Delegate) =>
        {
            action(index, _Delegate);
        });
    }
    #endregion

    public static void DeLog(string content)
    {
        Debug.Log(content);
    }
}
