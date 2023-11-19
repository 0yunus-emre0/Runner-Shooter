using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Booster : MonoBehaviour
{
    [SerializeField] BoosterTypes boosterType;
    [SerializeField] AmountStatus amountStatus;
    [Header("Amounts")]
    [SerializeField] float boostMultiplier;
    [SerializeField] float boostAmount = 0;


    [Header("Texts")]
    [SerializeField] TextMeshPro multiplierText;
    [SerializeField] TextMeshPro amountText;

    [Header("Refferances")]
    [SerializeField] SpriteRenderer boosterImage;
    private void Start() {
        multiplierText.text = boostMultiplier.ToString();
        CheckAmounts();
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("PlayerBullet")){
            boostAmount += boostMultiplier;
            transform.DOScale(Vector3.one*.9f,.08f).OnComplete(()=>transform.DOScale(Vector3.one,.08f));
            CheckAmounts();

        }
        else if(other.CompareTag("Player")){
            Debug.Log("Applying boost");
            ApplyBoost();
        }
    }
    void ApplyBoost(){
        switch(boosterType){
            case BoosterTypes.FireRate:
                GameManager.Instance.SetFireRate(boostAmount);
                break;
            case BoosterTypes.AttackRange:
                GameManager.Instance.SetAttackRange(boostAmount);
                break;
        }
        boostAmount = 0;
        gameObject.SetActive(false);
    }
    void CheckAmounts(){
        if(boostAmount >= 0){
            amountText.text = "+" + boostAmount.ToString();
            SetAmountStatus(AmountStatus.Green);
        } 
        else if(boostAmount < 0){
            amountText.text = boostAmount.ToString();
            SetAmountStatus(AmountStatus.Red);
        } 
    }
    void SetAmountStatus(AmountStatus status){
        if(amountStatus == status) return;
        amountStatus = status;
        switch(amountStatus){
            case AmountStatus.Green:
                boosterImage.DOColor(Color.green,.3f).OnComplete(()=> Debug.Log("Complated green"));
                break;
            case AmountStatus.Red:
                boosterImage.DOColor(Color.red,.3f).OnComplete(()=> Debug.Log("Complated green"));
                break;
        }
        Debug.Log("Status Changed to: "+ amountStatus);
    }
}
public enum BoosterTypes{
    FireRate,
    AttackRange
}
public enum AmountStatus{
    Green,
    Red
}
