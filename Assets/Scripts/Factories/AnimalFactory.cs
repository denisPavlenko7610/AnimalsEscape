using System.Linq;
using AnimalsEscape.Enums;
using UnityEngine;
using Zenject;

namespace AnimalsEscape.Player
{
    public class AnimalFactory : MonoBehaviour, IFactory<Animal>
    {
        [SerializeField] AnimalType _animalType;
        [SerializeField] AnimalSettingsSo _animalSettingsSo;

        public Animal Create()
        {
            foreach (var animalSetting in _animalSettingsSo.AnimalSettings.Where(s =>
                s.AnimalType == _animalType))
            {
                return Instantiate(animalSetting.AnimalPrefab, transform.position, transform.rotation, transform);
            }

            Debug.LogError($"Can`t create {_animalType}");
            return null;
        }
    }
}