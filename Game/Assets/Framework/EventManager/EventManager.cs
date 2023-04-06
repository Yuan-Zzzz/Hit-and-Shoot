using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo { }
public class EventInfo : IEventInfo
{
    public UnityAction action;
}
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> action;
}
public class EventInfo<T, K> : IEventInfo
{
    public UnityAction<T, K> action;
}
public static class EventManager
{
    /// <summary>
    /// key为事件的名称
    /// value为监听该事件的委托函数
    /// </summary>
    private static Dictionary<EventName, IEventInfo> eventDict = new Dictionary<EventName, IEventInfo>();

    #region 添加事件监听
    /// <summary>
    /// 添加事件监听
    /// 无参数
    /// </summary>
    /// <param name="_eventName">事件的名称</param>
    /// <param name="_action">准备用来处理事件的委托函数</param>
    public static void Register(EventName _eventName, UnityAction action)
    {
        //判断字典里是否有该事件
        if (eventDict.ContainsKey(_eventName))
            (eventDict[_eventName] as EventInfo).action += action;
        else
            eventDict.Add(_eventName, new EventInfo() { action = action });
    }
    /// <summary>
    /// 添加事件监听
    /// 一个参数
    /// </summary>
    /// <typeparam name="T">参数1</typeparam>
    /// <param name="_eventName">事件的名称</param>
    /// <param name="action">准备用来处理事件的委托函数</param>
    public static void Register<T>(EventName _eventName, UnityAction<T> action)
    {
        if (eventDict.ContainsKey(_eventName))
            (eventDict[_eventName] as EventInfo<T>).action += action;
        else
            eventDict.Add(_eventName, new EventInfo<T>() { action = action });
    }
    /// <summary>
    /// 添加事件监听
    /// 两个参数
    /// </summary>
    /// <typeparam name="T">参数1</typeparam>
    /// <typeparam name="K">参数2</typeparam>
    /// <param name="_eventName">事件名称</param>
    /// <param name="action">准备用来处理事件的委托函数</param>
    public static void Register<T, K>(EventName _eventName, UnityAction<T, K> action)
    {
        if (eventDict.ContainsKey(_eventName))
            (eventDict[_eventName] as EventInfo<T, K>).action += action;
        else
            eventDict.Add(_eventName, new EventInfo<T, K>() { action = action });
    }
    #endregion

    #region 删除事件监听
    /// <summary>
    /// 移除事件监听
    /// 无参数
    /// </summary>
    /// <param name="_eventName">事件名称</param>
    /// <param name="action">要删除的处理事件的委托函数</param>
    public static void Remove(EventName _eventName, UnityAction action)
    {
        if (eventDict.ContainsKey(_eventName))
            (eventDict[_eventName] as EventInfo).action -= action;
    }
    /// <summary>
    /// 移除事件监听
    /// 一个参数
    /// </summary>
    /// <typeparam name="T">参数1</typeparam>
    /// <param name="_eventName">事件名称</param>
    /// <param name="action">要删除的处理事件的委托函数</param>
    public static void Remove<T>(EventName _eventName, UnityAction<T> action)
    {
        if (eventDict.ContainsKey(_eventName))
            (eventDict[_eventName] as EventInfo<T>).action -= action;
    }
    /// <summary>
    /// 移除事件监听
    /// 两个参数
    /// </summary>
    /// <typeparam name="T">参数1</typeparam>
    /// <typeparam name="K">参数2</typeparam>
    /// <param name="_eventName">事件名称</param>
    /// <param name="action">要删除的处理事件的委托函数</param>
    public static void Remove<T, K>(EventName _eventName, UnityAction<T, K> action)
    {
        if (eventDict.ContainsKey(_eventName))
            (eventDict[_eventName] as EventInfo<T, K>).action -= action;

    }
    #endregion

    #region 事件触发
    /// <summary>
    /// 事件触发
    /// 无参数
    /// </summary>
    /// <param name="_eventName">事件的名字</param>
    public static void Send(EventName _eventName)
    {
        if (eventDict.ContainsKey(_eventName))
        {
            (eventDict[_eventName] as EventInfo).action.Invoke();
        }
    }
    /// <summary>
    /// 事件触发
    /// 一个参数
    /// </summary>
    /// <typeparam name="T">参数1</typeparam>
    /// <param name="_eventName">事件的名字</param>
    /// <param name="t">参数1</param>
    public static void Send<T>(EventName _eventName, T t)
    {
        if (eventDict.ContainsKey(_eventName))
        {
            (eventDict[_eventName] as EventInfo<T>).action.Invoke(t);
        }
    }
    /// <summary>
    /// 事件触发
    /// 两个参数
    /// </summary>
    /// <typeparam name="T">参数1</typeparam>
    /// <typeparam name="K">参数2</typeparam>
    /// <param name="_eventName">事件的名字</param>
    /// <param name="t">参数1</param>
    /// <param name="k">参数2</param>
    public static void Send<T, K>(EventName _eventName, T t, K k)
    {
        if (eventDict.ContainsKey(_eventName))
        {
            (eventDict[_eventName] as EventInfo<T, K>).action.Invoke(t, k);
        }
    }
    #endregion

    #region 清空事件
    /// <summary>
    /// 清空事件
    /// </summary>
    public static void Clear()
    {
        eventDict.Clear();
    }
    #endregion
}
