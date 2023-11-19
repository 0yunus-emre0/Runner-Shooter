using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolGenerator : MonoBehaviour
{
    [SerializeField] Pool[] pools = null;
    
    public GameObject GetFromPool(int objectType){
        GameObject obj = pools[objectType].pooledBullets.Dequeue();
        obj.SetActive(true);
        pools[objectType].pooledBullets.Enqueue(obj);
        return obj;
    }
    void Awake() {
        
        GeneratePool();
    }
    void GeneratePool(){
        GameObject gameObjectPool = new GameObject("Object Pools");
        for(int a=0;a<pools.Length;a++){
            GameObject pooledGameObjects = new GameObject(pools[a].poolName);
            pooledGameObjects.transform.SetParent(gameObjectPool.transform);
            pools[a].pooledBullets = new Queue<GameObject>();
            for(int i = 0; i < pools[a].poolSize; i++) {
                GameObject obj = Instantiate(pools[a].objectPrefab);
                obj.name = pools[a].poolName +" "+ (i+1);
                obj.transform.SetParent(pooledGameObjects.transform);
                obj.SetActive(false);
                pools[a].pooledBullets.Enqueue(obj);
            }
        }
    }
}
[Serializable]
public struct Pool{
    public Queue<GameObject> pooledBullets;
    public string poolName;
    public GameObject objectPrefab;
    public int poolSize;
        

}
