using System;
using System.Linq;
using AnimalsEscape.Enums;
using UnityEngine;

namespace AnimalsEscape.Player
{
    public class AnimalFactory : MonoBehaviour
    {
        [SerializeField] private AnimalType _animalType;
        [SerializeField] private AnimalSettingsSo _animalSettingsSo;

        public Animal Create()
        {
            foreach (var animalSetting in _animalSettingsSo.AnimalSettings.Where(s =>
                s.AnimalType == _animalType))
            {
                return Instantiate(animalSetting.AnimalPrefab, transform.position, Quaternion.identity, transform);
            }

            throw new NullReferenceException("Can`t create animal");
        }
    }
}