using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteractable = false;

    public virtual void Interact()
    {
        Debug.Log($"Interacting with {gameObject.name}", gameObject);
    }

    private void Update()
    {
        if (!isFocus || hasInteractable) return;

        float distance = Vector3.Distance(player.position, interactionTransform.position);
        if (distance <= radius)
        {
            Interact();
            hasInteractable = true;
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteractable = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteractable = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    private void OnValidate()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
    }
}