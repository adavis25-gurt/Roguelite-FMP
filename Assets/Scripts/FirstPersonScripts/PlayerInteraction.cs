using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public Camera mainCam;
    public float interactionDistance = 2f;

    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;

    public GameObject lastHitObject;
    public bool hitObject = false;

    private void Update()
    {
        InteractionRay();
        if (lastHitObject && hitObject == false) lastHitObject.GetComponent<MeshRenderer>().materials[1].SetFloat("_OutlineScale", 1f);
    }

    void InteractionRay()
    {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;

        bool hitSomething = false;
        hitObject = false;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                Material[] materials = hit.transform.gameObject.GetComponent<MeshRenderer>().materials;
                Material highlightMaterial = materials[1];
                highlightMaterial.SetFloat("_OutlineScale", 1.03f);
                hitSomething = true;
                interactionText.text = interactable.GetDescription();

                lastHitObject = hit.transform.gameObject;
                hitObject = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }
        interactionUI.SetActive(hitSomething);
    }
}
