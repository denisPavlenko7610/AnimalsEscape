using RDTools.AutoAttach;
using UnityEngine;

namespace AnimalsEscape
{
    [RequireComponent(typeof(Collider))]
    public class Fence : MonoBehaviour
    {
        [SerializeField, Attach(Attach.Scene, false)] ButtonPress buttonPressToPress;
        [SerializeField, Attach] private Collider _collider;
        [SerializeField, Attach(Attach.Child)] private MeshRenderer[] _meshRenderers;

        private void OnEnable() => buttonPressToPress.OnPressed += ButtonPressToPressPressed;

        private void OnDisable() => buttonPressToPress.OnPressed -= ButtonPressToPressPressed;

        private void ButtonPressToPressPressed()
        {
            foreach (var meshRenderer in _meshRenderers)
            {
                meshRenderer.enabled = false;
            }

            _collider.enabled = false;
        }
    }
}
