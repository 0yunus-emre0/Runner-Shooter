using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI yearGainText;
    [SerializeField] Animator yearGainTextAnimator;
    [SerializeField] CanvasGroup startClickPanel;
    [SerializeField] CanvasGroup levelFailedPanel;
    [SerializeField] CanvasGroup levelComplatedPanel;
    private void Awake() {
        GameManager.Instance.OnYearChanged += OnYearChanged;
        GameManager.Instance.OnLevelChanged += OnLevelStatusChanged;
    }
    private void Update() {
        if(Input.GetMouseButtonDown(0) && !GameManager.Instance.isGameStarted){
            startClickPanel.DOFade(0,1f);
            GameManager.Instance.isGameStarted = true;
        }
    }
    void OnYearChanged(int amount){
        yearGainText.text ="+" +amount.ToString() + "YEAR";
        yearGainTextAnimator.Play("YearGainTextShow");
        Debug.Log("AnimationPlayed");

    }
    void OnLevelStatusChanged(bool status){
        if(!status) levelFailedPanel.DOFade(1,1f);
        else levelComplatedPanel.DOFade(1,1f);
    }
    private void OnDestroy() {
        GameManager.Instance.OnYearChanged -= OnYearChanged;
        GameManager.Instance.OnLevelChanged -= OnLevelStatusChanged;
    }
}
