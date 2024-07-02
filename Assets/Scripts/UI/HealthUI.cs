using System;
using System.Collections;
using AnimalsEscape.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _healthCounter;
    [SerializeField] TextMeshProUGUI _countdownToHeal;
    [SerializeField] Image _imageRewarded;

    public Action OnHealed;

    float _minutes;
    float _seconds;
    float _delta = 1;

    public bool IsCouroutineReadyToStop { get; set; }
    public bool IsCoroutineRunning { get; set; }

    WaitForSecondsRealtime _timeOfOneSecond = new WaitForSecondsRealtime(1);
    Coroutine _currentCoroutine;

    void Awake()
    {
        _minutes = Storage.Load(Constants.MinutesKey,4);
        _seconds = Storage.Load(Constants.SecondsKey,60);
        _imageRewarded.enabled = false;
    }

    public void SetStateCountdownToHealText(bool state)
    {
        _countdownToHeal.enabled = state;
        _imageRewarded.enabled = state;
    }

    public void Setup(int health)
    {
        _healthCounter.text = health.ToString();
        if (health<5)
        {
            SetStateCountdownToHealText(true);
            StartCountdownUI();
        }
    }

    public void StartCountdownUI()
    {
        if (!IsCoroutineRunning)
            _currentCoroutine = StartCoroutine(TimerUIToHeal());
    }

    public void StopCountdownUI()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            IsCoroutineRunning = false;
            _currentCoroutine = null;
        }
    }

    void SetupStartTime()
    {
        _minutes = 4;
        _seconds = 60;
        _delta = 1;
    }

    IEnumerator TimerUIToHeal()
    {
        IsCoroutineRunning = true;
        while (!IsCouroutineReadyToStop)
        {
            if (_seconds == 0)
            {
                _minutes--;
                _seconds = 60;
            }
            _seconds -= _delta;
            _countdownToHeal.text = _minutes.ToString("00") + " : " + _seconds.ToString("00");

            if (_seconds == 0 && _minutes == 0)
            {
                OnHealed?.Invoke();
                break;
            }

            yield return _timeOfOneSecond;
        }
        IsCoroutineRunning = false;
        SetupStartTime();
    }

    private void OnDisable()
    {
        Storage.Save(Constants.MinutesKey,_minutes);
        Storage.Save(Constants.SecondsKey,_seconds);
    }
}
