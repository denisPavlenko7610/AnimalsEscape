using RDTools.AutoAttach;
using UnityEngine;

namespace AnimalsEscape
{
    [RequireComponent(typeof(Collider))]
    public class Fence : MonoBehaviour
    {
        [SerializeField, Attach(Attach.Scene, false)] ButtonPress buttonPressToPress;
        [SerializeField, Attach] private Collider _collider;
        [SerializeField, Attach(Attach.Child)] MeshRenderer[] _meshRenderers;

        void OnEnable()
        {
            if (buttonPressToPress == null)
                return;
            
            buttonPressToPress.OnPressed += ButtonPressToPressPressed;
        }

        void OnDisable()
        {
            if (buttonPressToPress == null)
                return;
            
            buttonPressToPress.OnPressed -= ButtonPressToPressPressed;
        }

        void ButtonPressToPressPressed()
        {
            foreach (MeshRenderer meshRenderer in _meshRenderers)
            {
                meshRenderer.enabled = false;
            }

            _collider.enabled = false;
        }
    }
}
