using UnityEngine;

public class EffectController : MonoBehaviour
{
    private void OnEnable() {
        Invoke(nameof(DisableEffect),1f);
    }
    void DisableEffect(){
        gameObject.SetActive(false);
    }
}
