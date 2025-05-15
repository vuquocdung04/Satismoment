using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_10Ctrl : MonoBehaviour
{
    public L10_Hammer hammer;
    public bool isDragging;

    // Weight settings
    [SerializeField] private float weightFactor = 0.7f; // Adjust between 0-1 (0: no weight, 1: very heavy)
    [SerializeField] private float maxMoveSpeed = 10f; // Maximum movement speed
    [SerializeField] private float dragRecoveryRate = 5f;

    Vector3 targetPosition;
    Vector3 weightedMovement;
    Vector3 movement;
    float maxDistance;

    Vector3 mousePos;
    Vector3 prevMousePos;
    Vector3 mouseDelta;
    float distance;
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        HandleDraggHammer();
    }

    void HandleDraggHammer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);

            if (hit.collider == null) return;
            hammer = hit.collider.GetComponent<L10_Hammer>();
            if (hammer == null) return;

            hammer.rb.gravityScale = 0;
            isDragging = true;
            prevMousePos = mousePos;

        }
        if (isDragging && hammer != null)
        {
            // Calculate mouse movement delta
            mouseDelta = mousePos - prevMousePos;

            // Apply weight effect - the hammer follows the mouse with delay
            targetPosition = mousePos;  // Target is now direct mouse position for better responsiveness
            weightedMovement = Vector3.Lerp(
                hammer.transform.position,
                targetPosition,
                Time.deltaTime * dragRecoveryRate * (1 - weightFactor * 0.6f)  // Reduced weight impact
            );

            // Limit movement speed based on weight
            maxDistance = maxMoveSpeed * Time.deltaTime * (1 - weightFactor * 0.8f);
            movement = weightedMovement - hammer.transform.position;
            if (movement.magnitude > maxDistance)
            {
                movement = movement.normalized * maxDistance;
            }

            // Apply the weighted movement
            hammer.transform.position += movement;

            // Calculate distance to calculate nail depth later
            distance = hammer.transform.position.y - (-3f);

            // Update previous mouse position
            prevMousePos = mousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            if (hammer == null) return;
            hammer.rb.gravityScale = 2;
            hammer.nailDepth = distance * 0.05f;
            hammer = null;

        }
    }
}
