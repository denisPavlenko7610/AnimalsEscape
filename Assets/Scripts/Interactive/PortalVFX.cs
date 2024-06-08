using AnimalsEscape;
using UnityEngine;
using UnityEngine.VFX;

public class PortalVFX : MonoBehaviour
{
    [SerializeField] VisualEffect _anotherPortalVisualEffect;
    [SerializeField] VisualEffect _thisPortalVisualEffect;

    [SerializeField] Material _portalMaterial;
    [SerializeField] Portal _portal;

    void Start()
    {
        _portalMaterial.EnableKeyword("_EMISSION");
    }

    private void OnEnable()
    {
       _portal.OnStateChanged += SetPortalState;
    }

    private void OnDisable()
    {
        _portal.OnStateChanged -= SetPortalState;
    }

    public void SetPortalState(bool state)
    { 
        _thisPortalVisualEffect.enabled = state;
        _anotherPortalVisualEffect.enabled = state;

        if (state)
            _portalMaterial.EnableKeyword("_EMISSION");
        else
            _portalMaterial.DisableKeyword("_EMISSION");
    }
}
