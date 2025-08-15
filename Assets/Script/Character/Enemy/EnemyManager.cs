using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    [Header("敌人配置")]
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public List<Transform> spawnPoints = new List<Transform>();

    private List<ObjectPool<GameObject>> enemyPools = new List<ObjectPool<GameObject>>();
    private bool hasSpawned = false;

    private void OnEnable()
    {
        EventHandler.AfterSceneUnLoadEvent += OnSceneLoaded;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneUnLoadEvent -= OnSceneLoaded;
    }

    private void OnSceneLoaded()
    {
        if (hasSpawned || gameObject.scene != SceneManager.GetActiveScene())
            return;

        InitializePools();
        SpawnEnemies();
        hasSpawned = true;
    }

    private void InitializePools()
    {
        // 清理旧池（如果存在）
        foreach (var pool in enemyPools)
        {
            pool.Clear();
        }
        enemyPools.Clear();

        // 为每个敌人类型创建对象池
        foreach (var prefab in enemyPrefabs)
        {
            var pool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(prefab),
                actionOnGet: (obj) => obj.SetActive(true),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: false,
                defaultCapacity: 20,
                maxSize: 40
            );

         

            enemyPools.Add(pool);
        }
    }

    public void SpawnEnemies()
    {
        if (enemyPrefabs.Count != spawnPoints.Count)
        {
            Debug.LogError("敌人预制体与生成点数量不匹配！");
            return;
        }

        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            var enemy = enemyPools[i].Get();
            enemy.transform.position = spawnPoints[i].position;

            // 关键修改点：获取接口而非具体组件
            var poolableEnemy = enemy.GetComponent<IPoolableEnemy>();
            if (poolableEnemy != null)
            {
                poolableEnemy.InitializePool(enemyPools[i]);
            }
            else
            {
                Debug.LogError($"敌人 {enemy.name} 未实现IPoolableEnemy接口");
            }
        }
    }

    // 场景卸载时清理
    private void OnDestroy()
    {
        foreach (var pool in enemyPools)
        {
            pool.Clear();
        }

        // 额外清理可能残留的敌人对象
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.activeInHierarchy)
            {
                Destroy(enemy);
            }
        }
    }
}
