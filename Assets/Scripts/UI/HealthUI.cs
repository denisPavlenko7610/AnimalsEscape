using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _healthCounter;
    [SerializeField] TextMeshProUGUI _countdownToHeal;
    [SerializeField] GameObject _imageRewarded;

    public Action OnHealed;

    float _minutes;
    float _seconds;
    float _delta;

    public bool IsCouroutineReadyToStop { get; set; }
    public bool IsCoroutineRunning { get; set; }

    WaitForSeconds _timeOfOneSecond = new WaitForSeconds(1);

    public void SetStateCountdownToHealText(bool state)
    {
        _countdownToHeal.enabled = state;
        _imageRewarded.SetActive(state);
    }

    public void Setup(int health)
    {
        _healthCounter.text = health.ToString();
    }

    public void StartCountdownUI()
    {
        if (!IsCoroutineRunning)
            StartCoroutine(TimerUIToHeal());
    }

    void SetupStartTime()
    {
        _minutes = 4;
        _seconds = 60;
        _delta = 1;
    }

    IEnumerator TimerUIToHeal()
    {
        SetupStartTime();
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
    }
}
