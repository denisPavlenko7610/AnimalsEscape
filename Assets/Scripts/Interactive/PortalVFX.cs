using AnimalsEscape.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

public class PortalVFX : MonoBehaviour
{
    [SerializeField] VisualEffect _anotherPortalVisualEffect;
    [SerializeField] VisualEffect _thisPortalVisualEffect;

    [SerializeField] Material _portalMaterial;

    int _timeToReloadPortalsInMs = 10000;

    private bool _isActive = true;
    public bool IsActive
    {
        get { return _isActive; }
        set { _isActive = value; }
    }

    void Start() => _portalMaterial.EnableKeyword("_EMISSION");

    void OnTriggerEnter(Collider other)
    {
        if (!IsActive)
            return;

        if (other.CompareTag(Constants.AnimalTag))
            DeactivatePortalsAsync();
    }

    async void DeactivatePortalsAsync()
    {
        SetPortalState(false);
        await UniTask.Delay(_timeToReloadPortalsInMs);
        SetPortalState(true);
    }

    void SetPortalState(bool state)
    {
        _isActive = state;

        _thisPortalVisualEffect.enabled = state;
        _anotherPortalVisualEffect.enabled = state;

        if (state)
            _portalMaterial.EnableKeyword("_EMISSION");
        else
            _portalMaterial.DisableKeyword("_EMISSION");
    }
}
