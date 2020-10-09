using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxShield(int maxShield)
    {
        slider.maxValue = maxShield;
        slider.value = maxShield;
    }

    public void SetShield(int shields)
    {
        slider.value = shields;
    }
}