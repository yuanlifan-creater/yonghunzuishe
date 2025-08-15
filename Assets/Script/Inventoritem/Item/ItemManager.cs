using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace yhzs.Inventory { 
public class ItemManager : MonoBehaviour
{
        public Item itemPrefab;
        private Dictionary<string, List<SceneItem>> sceneItemDict = new Dictionary<string, List<SceneItem>>();
        // Start is called before the first frame update


        private void OnEnable()
        {
            EventHandler.BeforeSceneUnLoadEvent += OnBeforeSceneUnLoadEvent;
            EventHandler.AfterSceneUnLoadEvent += OnAfterSceneUnLoadEvent;
        }

        private void OnDisable()
        {
            EventHandler.BeforeSceneUnLoadEvent -= OnBeforeSceneUnLoadEvent;
            EventHandler.AfterSceneUnLoadEvent -= OnAfterSceneUnLoadEvent;
        }

        private void OnAfterSceneUnLoadEvent()
        {
            RecreatAllItems();
        }

        private void OnBeforeSceneUnLoadEvent()
        {
            GetAllSceneItems();
        }

        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void GetAllSceneItems()
        {
            List<SceneItem> currentSceneItems = new List<SceneItem>();
            foreach(var item in FindObjectsOfType<Item>())
            {
                SceneItem sceneItem = new SceneItem
                {
                    itemID = item.itemID,
                    position = new SerializableVector3(item.transform.position)


                 };
            currentSceneItems.Add(sceneItem);
            }

            if (sceneItemDict.ContainsKey(SceneManager.GetActiveScene().name))
            {
                sceneItemDict[SceneManager.GetActiveScene().name] = currentSceneItems;
            }
            else
            {
                sceneItemDict.Add(SceneManager.GetActiveScene().name, currentSceneItems);
            }
        }

        private void RecreatAllItems()
        {
            List<SceneItem> currentItems = new List<SceneItem>();
            if (sceneItemDict.TryGetValue(SceneManager.GetActiveScene().name, out currentItems))
            {
                if (currentItems != null)
                {
                    foreach(var item in FindObjectsOfType<Item>())
                    {
                        Destroy(item.gameObject);
                    }
                    foreach (var item in currentItems)
                    {
                        Item newItem = Instantiate(itemPrefab, item.position.ToVector3(), Quaternion.identity);
                        newItem.Init(item.itemID);
                    }
                }
            }
        }


}
}