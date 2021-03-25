using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BeaconButton : MonoBehaviour {
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material darkMaterial;

    [SerializeField] private Goal _goal;

    private MeshRenderer buttonMeshRenderer;
    private bool canUseButton;

    private void Awake() {
        // buttonTransform = transform.Find("Button");
        buttonMeshRenderer = GetComponent<MeshRenderer>();
        canUseButton = true;
    }

    private void Start() {
        ResetButton();
    }

    public bool CanUseButton() {
        return canUseButton;
    }

    public void UseButton() {
        if (canUseButton) {
            buttonMeshRenderer.material = darkMaterial;
            transform.localScale = new Vector3(.5f, .2f, .5f);
            canUseButton = false;
            
            _goal.Spawn();
        }
    }
    
    public void ResetButton() {
        buttonMeshRenderer.material = greenMaterial;
        transform.localScale = new Vector3(.5f, .5f, .5f);

        transform.localPosition = new Vector3(
            transform.localPosition.x, transform.localPosition.y, Random.Range(-2f, +2f));
        
        canUseButton = true;
    }

}
