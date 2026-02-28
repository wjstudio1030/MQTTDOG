using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
   [SerializeField] Gradient gradient;
   [SerializeField] Image fill;

   public void SetMaxHealth(int health)
   {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(2f);
   }
   public void SetHealth(int health)
   {
        slider.value = health;
    
        fill.color = gradient.Evaluate(slider.normalizedValue);
   }
}
