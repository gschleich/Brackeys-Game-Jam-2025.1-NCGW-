using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    // value refers to health or any slider value
    public void UpdateHealthBar(float currentValue, float maxValue){
        slider.value = currentValue / maxValue;
    }

    void Update(){}
}
