using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PoolGenerator poolGenerator;
    private void Start() {
        poolGenerator = GameObject.FindGameObjectWithTag("PoolGenerator").GetComponent<PoolGenerator>();
    }
    private void OnCollisionEnter(Collision other) {
        DestroyBullet();
    }
    private void OnTriggerEnter(Collider other) {
        
        if(other.CompareTag("Booster") || other.CompareTag("Revolver") || other.CompareTag("LevelEndBarrel")){
            DestroyBullet();
        }
    
    }
    void DestroyBullet(){
        //pool bulletVFx
        var vfx = poolGenerator.GetFromPool(3);
        vfx.transform.position = transform.position;
        gameObject.SetActive(false);
    }
}
