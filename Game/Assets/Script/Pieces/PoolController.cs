using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    private void OnEnable()
    {
        PoolManager.Instance.CreateNewPool(Resources.Load<GameObject>("Prefabs/Pieces"),10,PoolName.PiecesPool);
        PoolManager.Instance.CreateNewPool(Resources.Load<GameObject>("Prefabs/Projectile"),10, PoolName.ProjectilePool);
        PoolManager.Instance.CreateNewPool(Resources.Load<GameObject>("Prefabs/Flash"), 10, PoolName.FlashPool);
    }

    private void OnDisable()
    {
        PoolManager.Instance.Clear(PoolName.PiecesPool);
        PoolManager.Instance.Clear(PoolName.ProjectilePool);
        PoolManager.Instance.Clear(PoolName.FlashPool);
    }
}
