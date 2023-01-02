using System;
using AnimalsEscape.Utils;
using RDTools.AutoAttach;
using UnityEngine;

namespace AnimalsEscape
{
    [RequireComponent(typeof(Collider), typeof(ButtonAnimation))]
    public class ButtonPress : MonoBehaviour
    {
        [SerializeField, Attach] ButtonAnimation _buttonAnimation;
        
        bool _isButtonPressed;
        
        public event Action OnPressed; 
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.AnimalTag) || _isButtonPressed)
                return;
            
            OnPressed?.Invoke();
            _buttonAnimation.Play();
            _isButtonPressed = true;
        }
    }
}