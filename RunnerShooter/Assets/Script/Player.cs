using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class Player : MonoBehaviour
{
    [Header("Player Refferances: ")]
    Camera mainCamera;
    [SerializeField] PoolGenerator poolGenerator;
    [SerializeField] Animator playerAnim;
    [SerializeField] Animator playerModelsAnim;
    [SerializeField] TextMeshProUGUI yearText;
    [SerializeField] GameObject[] guns;
    [SerializeField] Transform[] aims;
    [Header("Player Variables: ")]
    [SerializeField] float attackRange;
    [SerializeField] float fireRate;
    [SerializeField] float speed;
    [SerializeField] int year = 1815;
    [SerializeField] int activeGunIndex= 0;
    private bool _isAlreadyAttacked = false;

    private void Awake() {
        GameManager.Instance.OnAttackRangeChanged += OnAttackRangeChanged;
        GameManager.Instance.OnFireRateChanged += OnFireRateChanged;
        GameManager.Instance.OnYearChanged += OnYearChanged;
        GameManager.Instance.OnLevelChanged += OnLevelStatusChanged;
    }
    void Start()
    {
        mainCamera = Camera.main;
        yearText.text = year.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.isGameFailed && GameManager.Instance.isGameStarted && !GameManager.Instance.isLevelCompleted){
            Move(speed);
            FireBullet();
        }
        
    }

    void Move(float speed){
        Rigidbody rb = GetComponent<Rigidbody>();
        //Vector3 forwadDirection = Vector3.forward;
        if(Input.GetMouseButton(0)){
            Vector3 mousePos = Input.mousePosition;
            Vector3 playerpos = transform.position;
            Vector3 screenPos = mainCamera.WorldToScreenPoint(playerpos);
            Vector3 moveDirection = mousePos - screenPos;
            
            rb.velocity = new Vector3(moveDirection.x,0,0) * speed * Time.deltaTime + Vector3.forward * speed;
        }
        else {
            rb.velocity = Vector3.forward * speed;
        }
    }
    void FireBullet(){
        if(!_isAlreadyAttacked){
            //pool bullet
            var obj = poolGenerator.GetFromPool(0);
            obj.transform.position = aims[activeGunIndex].transform.position;
            obj.GetComponent<Rigidbody>().AddForce(aims[activeGunIndex].transform.forward*attackRange,ForceMode.Impulse);

            //pool bulletVFx
            var vfx = poolGenerator.GetFromPool(2);
            vfx.transform.position = aims[activeGunIndex].transform.position;

            playerAnim.Play("PlayerShoot");
            _isAlreadyAttacked = true;
            Invoke(nameof(ResetAttack),fireRate);
        }
    }
    void OnAttackRangeChanged(float amount){
        attackRange += amount;
        Debug.Log("Attack Range Changed to: " + attackRange);
    }
    void OnFireRateChanged(float amount){
        if(amount == 0) return;
        else if(amount > 0) fireRate *= (1 - amount / 100);
        else if(amount < 0) fireRate *= (1 + amount / 100);
        fireRate = Mathf.Max(0.0f,fireRate);
        Debug.Log("Fire rate Changed to: "+ fireRate);
        
    }
    void OnYearChanged(int amount){
        year += amount;
        yearText.text = year.ToString();
        SetGun();
    }
    void OnLevelStatusChanged(bool levelStatus){
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if(!levelStatus){
            
            playerAnim.Play("PlayerFail");
        }
        else{

        }
        
    }
    void SetGun(){
        if(year >= 1820 && year < 1840){            
            guns[activeGunIndex].SetActive(false);
            activeGunIndex = 1;
            guns[activeGunIndex].SetActive(true);
            playerModelsAnim.Play("PlayerYearGainSpin");

        }
        else if(year >= 1840 && year < 1880){
            guns[activeGunIndex].SetActive(false);
            activeGunIndex = 2;
            guns[activeGunIndex].SetActive(true);
            playerModelsAnim.Play("PlayerYearGainSpin");
        }
        else if(year >= 1880 && year < 1920){
            guns[activeGunIndex].SetActive(false);
            activeGunIndex = 3;
            guns[activeGunIndex].SetActive(true);
            playerModelsAnim.Play("PlayerYearGainSpin");
        }
        else if(year >= 1920 && year < 1940){
            guns[activeGunIndex].SetActive(false);
            activeGunIndex = 4;
            guns[activeGunIndex].SetActive(true);
            playerModelsAnim.Play("PlayerYearGainSpin");
        }
        else if(year >= 1940 && year < 1980){
            guns[activeGunIndex].SetActive(false);
            activeGunIndex = 5;
            guns[activeGunIndex].SetActive(true);
            playerModelsAnim.Play("PlayerYearGainSpin");
        }
        else if(year >= 1980 && year < 2000){
            guns[activeGunIndex].SetActive(false);
            activeGunIndex = 6;
            guns[activeGunIndex].SetActive(true);
            playerModelsAnim.Play("PlayerYearGainSpin");
        }
        else if(year >= 2000 && year < 2020){
            guns[activeGunIndex].SetActive(false);
            activeGunIndex = 7;
            guns[activeGunIndex].SetActive(true);
            playerModelsAnim.Play("PlayerYearGainSpin");
        }
        else if(year >= 2020 && year < 2040){
            guns[activeGunIndex].SetActive(false);
            activeGunIndex = 8;
            guns[activeGunIndex].SetActive(true);
            playerModelsAnim.Play("PlayerYearGainSpin");
        }


        
    }
    void ResetAttack(){
        _isAlreadyAttacked = false;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("FinishLine")) GameManager.Instance.SetLevelStatus(true);
    }

    private void OnDestroy() {
        GameManager.Instance.OnAttackRangeChanged -= OnAttackRangeChanged;
        GameManager.Instance.OnFireRateChanged -= OnFireRateChanged;
        GameManager.Instance.OnYearChanged -= OnYearChanged;
        GameManager.Instance.OnLevelChanged -= OnLevelStatusChanged;
    }
}
