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
    public Renderer targetRenderer1; // Reference to the first renderer of the specific object
    public Renderer targetRenderer2; // Reference to the second renderer of the specific object
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

            // Change the material of the specific object's mesh renderer 1
            if (targetRenderer1 != null && targetRenderer1.materials.Length > materialIndex)
            {
                Material[] materials1 = targetRenderer1.materials;
                materials1[1] = connectedMaterial;
                targetRenderer1.materials = materials1;
            }

            // Change the material of the specific object's mesh renderer 2
            if (targetRenderer2 != null && targetRenderer2.materials.Length > materialIndex)
            {
                Material[] materials2 = targetRenderer2.materials;
                materials2[1] = connectedMaterial;
                targetRenderer2.materials = materials2;
            }

        }
        else
        {
            // Change the material of the specific object's mesh renderer 1
            if (targetRenderer1 != null && targetRenderer1.materials.Length > materialIndex)
            {
                Material[] materials1 = targetRenderer1.materials;
                materials1[materialIndex] = unConnectedMaterial;
                targetRenderer1.materials = materials1;
            }

            // Change the material of the specific object's mesh renderer 2
            if (targetRenderer2 != null && targetRenderer2.materials.Length > materialIndex)
            {
                Material[] materials2 = targetRenderer2.materials;
                materials2[materialIndex] = unConnectedMaterial;
                targetRenderer2.materials = materials2;
            }
        }
    }
}
