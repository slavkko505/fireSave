using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderHealth : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private HelthFire _helthFire;

    public UnityAction<float> sliderHealth;

    private void OnEnable()
    {
        sliderHealth += OnHealthChange;
    }

    private void OnDisable()
    {
        sliderHealth -= OnHealthChange;
    }

    private void Start()
    {
        _helthFire = FindObjectOfType<HelthFire>();
        slider.value = _helthFire.currentHealth / 100;
    }

    public void OnHealthChange(float amount)
    {
        slider.value = amount /100;
    }

}
