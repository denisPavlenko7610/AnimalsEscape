using System;
using System.Collections.Generic;
using AnimalsEscape.Enums;
using UnityEngine;

namespace AnimalsEscape.Player
{
    [CreateAssetMenu(fileName = "AnimalSettingsSo", menuName = "Animal/AnimalSettings")]
    public class AnimalSettingsSo : ScriptableObject
    {
        public List<AnimalSetting> AnimalSettings = new();
    }
    
    [Serializable]
    public struct AnimalSetting
    {
        public AnimalType AnimalType;
        public Animal AnimalPrefab;
    }
}
