using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;


public class PlugController : MonoBehaviour
{
    public bool isConected = false;
    public UnityEvent OnWirePlugged;
    public Transform plugPosition;

    [HideInInspector]
    public Transform endAnchor;
    [HideInInspector]
    public Rigidbody endAnchorRB;
    [HideInInspector]
    public WireController wireController;

    public Material connectedMaterial; // Material to apply when connected
    public Material unConnectedMaterial;
    public Renderer targetRenderer; // Reference to the renderer of the specific object
    public int materialIndex = 1; // Index of the material element to change

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == endAnchor.gameObject)
        {
            isConected = true;
            endAnchorRB.isKinematic = true;
            endAnchor.transform.position = plugPosition.position;
            endAnchor.transform.rotation = transform.rotation;

            // Invoke the event if it's set
            if (OnWirePlugged != null)
                OnWirePlugged.Invoke();
        }
    }

    private void Update()
    {
        if (isConected)
        {
            endAnchorRB.isKinematic = true;
            endAnchor.transform.position = plugPosition.position;
            Vector3 eulerRotation = new Vector3(this.transform.eulerAngles.x + 90, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            endAnchor.transform.rotation = Quaternion.Euler(eulerRotation);

            // Change the material of the specific object's mesh renderer
            if (targetRenderer != null && targetRenderer.materials.Length > materialIndex)
            {
                Material[] materials = targetRenderer.materials;
                materials[1] = connectedMaterial;
                targetRenderer.materials = materials;
            }

        }
        else
        {
            if (targetRenderer != null && targetRenderer.materials.Length > materialIndex)
            {
                Material[] materials = targetRenderer.materials;
                materials[materialIndex] = unConnectedMaterial;
                targetRenderer.materials = materials;
            }
        }
    }
}
