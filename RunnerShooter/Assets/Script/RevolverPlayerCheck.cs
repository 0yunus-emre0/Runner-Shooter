using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverPlayerCheck : MonoBehaviour
{
    [SerializeField] Revolver revolver;
   private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            revolver.GoToSideway();
        }
   }
}
