using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//对象池类
public class ObjectPool
{
    public GameObject objectPrefab;
    public Queue<GameObject> pool;
    public int objectCount;

    public ObjectPool(GameObject _objectPrefab, Queue<GameObject> _pool, int _objectCount)
    {
        objectPrefab = _objectPrefab;
        pool = _pool;
        objectCount = _objectCount;
    }

}

/// <summary>
/// 对象池管理类
/// </summary>
public class PoolManager : SingletonMono<PoolManager>
{
    public Dictionary<PoolName, ObjectPool> poolDic = new Dictionary<PoolName, ObjectPool>();
    /// <summary>
    /// 创建新的对象池
    /// </summary>
    /// <param name="_objectPrefab">添加到对象池中的预制体</param>
    /// <param name="_objectCount">数量</param>
    /// <param name="_poolName">对象池的名称</param>
    public void CreateNewPool(GameObject _objectPrefab, int _objectCount, PoolName _poolName)
    {

        if (!poolDic.ContainsKey(_poolName))
        {
            //生成新对象池
            poolDic.Add(_poolName, new ObjectPool(_objectPrefab, new Queue<GameObject>(), _objectCount));
        }
        //向对象池中添加对象
        for (int i = 0; i < poolDic[_poolName].objectCount; i++)
        {
            var newObject = Instantiate(_objectPrefab);
            //poolDic[_poolName].pool.Enqueue(newObject);
            ReturnPool(_poolName, newObject);
        }
    }

    /// <summary>
    /// 让对象返回对象池
    /// </summary>
    /// <param name="_poolName">对象池的名称</param>
    /// <param name="_newObject">返回对象池的对象</param>
    public void ReturnPool(PoolName _poolName, GameObject _newObject)
    {
        poolDic[_poolName].pool.Enqueue(_newObject);
        _newObject.SetActive(false);
    }

    /// <summary>
    /// 从对象池中取出对象
    /// </summary>
    /// <param name="_poolName">对象池的名称</param>
    /// <returns></returns>
    public GameObject GetFromPool(PoolName _poolName)
    {
        //当对象池中对象不够用时，再次生成
        if (poolDic[_poolName].pool.Count == 0)
        {
            CreateNewPool(poolDic[_poolName].objectPrefab, poolDic[_poolName].objectCount, _poolName);
        }
        var newObject = poolDic[_poolName].pool.Dequeue();
        newObject.SetActive(true);
        return newObject;
    }
    /// <summary>
    /// 清空对象池
    /// </summary>
    /// <param name="_poolName">对象池名称</param>
    public void Clear(PoolName _poolName)
    {
        poolDic[_poolName].pool.Clear();
    }
    /// <summary>
    /// 清空所有对象池
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
    }
}
