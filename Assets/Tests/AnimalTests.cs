using AnimalsEscape;
using NUnit.Framework;
using UnityEngine;
namespace Tests
{
    public class AnimalTests : MonoBehaviour
    {
        Animal _animal;
        AnimalHealth _animalHealth;
        AnimalInput _animalInput;
        
        [SetUp]
        public void CommonInstall()
        {
            _animal = Instantiate(Resources.Load<Animal>("Cat"));
            _animalHealth = _animal.GetComponentInChildren<AnimalHealth>();
            _animalInput = _animal.GetComponent<AnimalInput>();
        }

        [Test]
        public void AnimalDoesNotMoveWhenHealthIsZero()
        {
            for (int i = _animalHealth.Health; i > 0; i--)
            {
                _animalHealth.DecreaseHealth();
            }
        
            Assert.IsFalse(_animalInput.IsMoving, "Animal should not move when health is zero.");
        }
    }
}