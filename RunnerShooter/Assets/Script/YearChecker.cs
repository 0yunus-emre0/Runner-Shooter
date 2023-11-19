using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class YearChecker : MonoBehaviour
{
    public GameObject[] yearBullets = new GameObject[15];
    [SerializeField] PoolGenerator poolGenerator;
    [SerializeField] StarPanel[] starPanels;
    [SerializeField] float[] bulletPositions;
    [SerializeField] int activeBulletIndex = 0;
    [SerializeField] Transform bulletSpawnPoint;
    public int bulletsOnTable;

    public void AddBullets(int bulletCount){
        for(int i = 0; i < bulletCount; i++){
            //pool bullets
            var obj = poolGenerator.GetFromPool(1);
            obj.transform.position = bulletSpawnPoint.position;
            obj.transform.DOMoveX(bulletPositions[activeBulletIndex],.5f);
            yearBullets[activeBulletIndex] = obj;
            activeBulletIndex++;
        }
        bulletsOnTable+= bulletCount;
        CheckStars();
    }
    void CheckStars(){
        int bulletCheckCount = 5;
        for(int i = 0; i < starPanels.Length;i++){
            if(bulletsOnTable >= bulletCheckCount && !starPanels[i].isPanelAvaliable){
                starPanels[i].UnlockPanel();
            }
            bulletCheckCount+=5;
        }  
    }
}
