using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineScript : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    private Renderer outlineRenderer;

    private Component rigBod;
    private Component theCollider;
    void Start()
    {
        outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
        outlineRenderer.enabled = true;
    }

    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color)
    {

        GameObject outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);

        theCollider = outlineObject.gameObject.GetComponent(typeof(Collider));
        Destroy(theCollider);

        rigBod = outlineObject.gameObject.GetComponent(typeof(Rigidbody));
        Destroy(rigBod);

        Renderer rend = outlineObject.GetComponent<Renderer>();
        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_ScaleFactor", scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        outlineObject.GetComponent<OutlineScript>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;
        rend.enabled = false;

        return rend;
    }

}