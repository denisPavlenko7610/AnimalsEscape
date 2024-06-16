using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _healthCounter;

    public void Setup(int health)
    {
        _healthCounter.text = health.ToString();
    }

}
