using System;
using UnityEngine;
using Zenject;

public class AnimalHealth : MonoBehaviour
{
    public static int Health { get; private set; } = 10;
    public Action OnHealthDecreased;

    //HealthUI _healthUI;
    //[Inject]

    [SerializeField] HealthUI _healthUI;

    public void Construct(HealthUI healthUI)
    {
        _healthUI = healthUI;
    }

    void Start()
    {
        _healthUI = FindObjectOfType<HealthUI>();
        _healthUI.Setup(Health);
    }

    public void DecreaseHealth()
    {
        Health--;
        if (Health <= 0)
        {

        }
    }

}
