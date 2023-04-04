using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//可变参数的委托
public delegate void EventData(params object[] args);

/// <summary>
/// 事件管理器
/// </summary>
public static class EventManager
{
    //存储事件的字典
    private static Dictionary<EventName, EventData> eventDict = new Dictionary<EventName, EventData>();
    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="_eventName">事件名</param>
    /// <param name="_action">监听函数</param>
    public static void Register(EventName _eventName, EventData _action)
    {
        if (eventDict.ContainsKey(_eventName)) eventDict[_eventName] += _action;
        else eventDict.Add(_eventName, _action);
    }
    /// <summary>
    /// 注销事件
    /// </summary>
    /// <param name="_eventName">事件名称</param>
    /// <param name="_action">监听函数</param>
    public static void Remove(EventName _eventName, EventData _action)
    {
        if (eventDict.ContainsKey(_eventName)) eventDict[_eventName] -= _action;
    }
    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="_eventName">事件名称</param>
    /// <param name="_args">事件参数</param>
    public static void Send(EventName _eventName, params object[] _args)
    {
        if (eventDict.ContainsKey(_eventName)) eventDict[_eventName]?.Invoke(_args);
    }
}
