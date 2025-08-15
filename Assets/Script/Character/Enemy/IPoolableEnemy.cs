using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public interface IPoolableEnemy
{
    void InitializePool(ObjectPool<GameObject> pool);
}
