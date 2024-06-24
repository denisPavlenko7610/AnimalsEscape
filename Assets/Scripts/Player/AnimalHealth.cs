using AnimalsEscape;
using Player;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Windows;
using Zenject;

public class AnimalHealth : MonoBehaviour
{
    public static int Health { get; private set; } = 5;

    [SerializeField] Animal _animal;
    [SerializeField] AnimalStatus _animalStatus;
    [SerializeField] AnimalInput _input;
    [SerializeField] Collider[] _colliders;

    HealthUI _healthUI;
    RewardAd _rewardedAd;

<<<<<<< Updated upstream
    //[Inject]
    //public void Construct(HealthUI healthUI)
    //{
    //    _healthUI = healthUI;
    //}
=======
    [Inject]
    public void Construct(HealthUI healthUI, RewardAd rewardedAd)
    {
        _healthUI = healthUI;
        _rewardedAd = rewardedAd;
    }
>>>>>>> Stashed changes

    void OnEnable()
    {
        _animal.OnBulletCollision += DecreaseHealth;
        _healthUI.OnHealed += IncreaseHealthPoint;
        _rewardedAd.OnRewardedClosed += IncreaseHealthPoint; // <--------------------------------
    }

    void OnDisable()
    {
        _animal.OnBulletCollision -= DecreaseHealth;
        _healthUI.OnHealed -= IncreaseHealthPoint;
        _rewardedAd.OnRewardedClosed -= IncreaseHealthPoint; // <--------------------------------
    }

    void Start()
    {
        _healthUI.Setup(Health);
    }

    public void DecreaseHealth()
    {
        Health--;
        _healthUI.Setup(Health);
        _animalStatus._animalState = AnimalState.dead;
        _input.enabled = false;

        foreach (Collider collider in _colliders)
            collider.enabled = false;

        StartCountdownToIncreaseHealth();

        if (Health <= 0)
        {
            //You cant play anymore! 
        }
    }

    void StartCountdownToIncreaseHealth()
    {
        if (Health < 5 && !_healthUI.IsCouroutineReadyToStop)
        {
            _healthUI.SetStateCountdownToHealText(true);
            _healthUI.StartCountdownUI();
        }
    }

    void IncreaseHealthPoint()
    {
        Health++;
        _healthUI.Setup(Health);

        _healthUI.IsCouroutineReadyToStop = false;
        _healthUI.IsCoroutineRunning = false;

        if (Health == 5)
            _healthUI.SetStateCountdownToHealText(false);

        StartCountdownToIncreaseHealth();
    }
}