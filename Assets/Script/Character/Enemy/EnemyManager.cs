using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    [Header("��������")]
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
        // ����ɳأ�������ڣ�
        foreach (var pool in enemyPools)
        {
            pool.Clear();
        }
        enemyPools.Clear();

        // Ϊÿ���������ʹ��������
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
            Debug.LogError("����Ԥ���������ɵ�������ƥ�䣡");
            return;
        }

        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            var enemy = enemyPools[i].Get();
            enemy.transform.position = spawnPoints[i].position;

            // �ؼ��޸ĵ㣺��ȡ�ӿڶ��Ǿ������
            var poolableEnemy = enemy.GetComponent<IPoolableEnemy>();
            if (poolableEnemy != null)
            {
                poolableEnemy.InitializePool(enemyPools[i]);
            }
            else
            {
                Debug.LogError($"���� {enemy.name} δʵ��IPoolableEnemy�ӿ�");
            }
        }
    }

    // ����ж��ʱ����
    private void OnDestroy()
    {
        foreach (var pool in enemyPools)
        {
            pool.Clear();
        }

        // ����������ܲ����ĵ��˶���
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.activeInHierarchy)
            {
                Destroy(enemy);
            }
        }
    }
}
