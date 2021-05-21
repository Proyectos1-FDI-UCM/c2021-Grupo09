using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraTorre : MonoBehaviour
{
    public Slider slider;
    public void SetLifetime(float lifetime)
    {
        slider.maxValue = lifetime;
        slider.value = lifetime;
    }
    public void SetTimeRemaining(float timeRemaining)
    {
        slider.value = timeRemaining;
    }
}
