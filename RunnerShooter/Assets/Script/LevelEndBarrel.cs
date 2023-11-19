using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class LevelEndBarrel : MonoBehaviour
{
    [SerializeField] int barrelAmount;
    [SerializeField] TextMeshPro amountText;

    private void Start() {
        amountText.text = barrelAmount.ToString();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("PlayerBullet")){
            barrelAmount--;
            amountText.text = barrelAmount.ToString();
            transform.DOScale(Vector3.one*.9f,.08f).OnComplete(()=>transform.DOScale(Vector3.one,.08f));
            if(barrelAmount <= 0) gameObject.SetActive(false);
        }
        else if(other.CompareTag("Player")){
            Debug.Log("LevelFailed");
            GameManager.Instance.SetLevelStatus(false);
        }
    }
}
