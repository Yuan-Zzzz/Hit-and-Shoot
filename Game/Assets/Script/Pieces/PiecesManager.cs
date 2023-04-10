using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesManager : MonoBehaviour
{
    private void OnEnable()
    {
        PoolManager.Instance.CreateNewPool(Resources.Load<GameObject>("Prefabs/Pieces"),10,PoolName.PiecesPool);
    }

    private void OnDisable()
    {
        PoolManager.Instance.Clear(PoolName.PiecesPool);
    }
}
