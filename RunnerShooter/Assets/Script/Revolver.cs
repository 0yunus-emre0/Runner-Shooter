using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Revolver : MonoBehaviour
{
   [SerializeField] GameObject[] bullets;
   [SerializeField] int activeIndex = 0;
   [SerializeField] Transform revolverBoxPosition;
   [SerializeField] YearChecker yearChecker;

   private void Start() {
    for(int i = 0; i < bullets.Length; i++){
        if(bullets[i].activeSelf) bullets[i].SetActive(false);
    }
   }
    
   private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("PlayerBullet")){
            if(activeIndex < bullets.Length){
                bullets[activeIndex].SetActive(true);
                activeIndex++;
                Vector3 nextRotation = new Vector3(0,0,transform.rotation.eulerAngles.z + 45);
                transform.DORotate(nextRotation, .5f);
            }
            else GoToSideway();
        }
        
   }

   public void GoToSideway(){
        transform.DOMoveX(-4.4f,1f).OnComplete(() => transform.DOMove(revolverBoxPosition.position,3f).OnComplete(ResetRevolver));
   }
   public void ResetRevolver(){
        yearChecker.AddBullets(activeIndex);
        activeIndex = 0;
        for(int i = 0; i < bullets.Length; i++){
            if(bullets[i].activeSelf) bullets[i].SetActive(false);
        }
        transform.rotation = Quaternion.Euler(Vector3.zero);
        gameObject.SetActive(false);
        
   }
}
