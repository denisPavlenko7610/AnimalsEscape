using AnimalsEscape;
using AnimalsEscape.UI;
using AnimalsEscape.Utils;
using Player;
using UnityEngine;
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
    LevelText _levelText;

    [Inject]
    public void Construct(HealthUI healthUI, RewardAd rewardedAd, LevelText levelText)
    {
        _healthUI = healthUI;
        _rewardedAd = rewardedAd;
        _levelText = levelText;
    }

    void OnEnable()
    {
        Health = (int)Storage.Load(Constants.HEALTHKEY, Health);

        _animal.OnBulletCollision += DecreaseHealth;
        _healthUI.OnHealed += IncreaseHealthPoint;
        _rewardedAd.OnRewardedClosed += IncreaseHealthPoint;
        _levelText.GetVideoButton.onClick.AddListener(_rewardedAd.ShowRewardedAd);
    }

    void OnDisable()
    {
        _animal.OnBulletCollision -= DecreaseHealth;
        _healthUI.OnHealed -= IncreaseHealthPoint;
        _rewardedAd.OnRewardedClosed -= IncreaseHealthPoint;
        _levelText.GetVideoButton.onClick.RemoveListener(_rewardedAd.ShowRewardedAd);

        Storage.Save(Constants.HEALTHKEY, Health);
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

        _healthUI.StopCountdownUI();
        _healthUI.Setup(Health);
        _healthUI.IsCouroutineReadyToStop = false;

        if (Health == 5)
            _healthUI.SetStateCountdownToHealText(false);

        StartCountdownToIncreaseHealth();
    }
}