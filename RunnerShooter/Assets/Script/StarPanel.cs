using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class StarPanel : MonoBehaviour
{
    [SerializeField] StarType starType;
    [SerializeField] YearChecker yearChecker;
    public bool isPanelAvaliable = false;
    [SerializeField] int yearCount;
    [SerializeField] TextMeshPro yearCountText;
    [SerializeField] SpriteRenderer[] starImages;
    [SerializeField] SpriteRenderer revolverImage;
    [SerializeField] Sprite emptyStar,fullStar;
    [SerializeField] Sprite revolverIcon,revolverIconEmpty;

    private void Start() {
        yearCountText.text = "+"+yearCount.ToString();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && isPanelAvaliable){
            Debug.Log(yearCount + " Year Gained");
            ApplyYearBoost();
        }
    }
    public void UnlockPanel(){
        if(!isPanelAvaliable){
            for(int i=0; i<starImages.Length;i++){
                starImages[i].sprite = fullStar;
            }
            revolverImage.sprite = revolverIcon;
            yearCountText.DOColor(Color.green,1f);
            isPanelAvaliable = true;
        }
    }
    void ApplyYearBoost(){
        switch(starType){
            case StarType.StarOne:
                for(int i = 0; i < 5; i++){
                    yearChecker.yearBullets[i].transform.DOMoveY(10f,1f);
                }
                break;
            case StarType.StarTwo:
                for(int i = 5; i < 10; i++){
                    yearChecker.yearBullets[i].transform.DOMoveY(10f,1f);
                }
                break;
            case StarType.StarThree:
                for(int i = 10; i < 15; i++){
                    yearChecker.yearBullets[i].transform.DOMoveY(10f,1f);
                }
                break;
        }
        GameManager.Instance.SetYear(yearCount);
    }
}
public enum StarType{
    StarOne,
    StarTwo,
    StarThree
}
