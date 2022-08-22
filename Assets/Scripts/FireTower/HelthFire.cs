using System;
using UnityEngine;
using UnityEngine.Events;

public class HelthFire : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    [SerializeField] private Transform FireAlha;
    [SerializeField] private Transform FireAdd;
    public float StandartSizeParticlFire = 0.4f;
    public float DicreaseSizeParticleByHit = 0.04f;

    private float currentParticleSIze;
    
    public UnityAction<int> HealthFire;

    private void OnEnable()
    {
        HealthFire += MinusHealth;
    }

    private void OnDisable()
    {
        HealthFire -= MinusHealth;
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        currentParticleSIze = StandartSizeParticlFire;
        SetParticleSize(StandartSizeParticlFire);
    }
    

    public void MinusHealth(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        
        currentParticleSIze -= DicreaseSizeParticleByHit;
        SetParticleSize(currentParticleSIze);
        
    }

    private void SetParticleSize(float currentSize)
    {
        FireAlha.localScale = new Vector3(currentSize,currentSize,currentSize);
        FireAdd.localScale = new Vector3(currentSize,currentSize,currentSize);
    }
}
