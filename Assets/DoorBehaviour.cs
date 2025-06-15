// File: Assets/Scripts/DoorBehaviour.cs

using UnityEngine;

/*
 * Author: Kai Ming
 * Date: 2025-06-14
 * Description: Sliding door that opens on interaction and closes when player leaves.
 */

/// <summary>
/// A door that slides vertically on interact and automatically closes when the player walks away.
/// </summary>
public class SlidingDoor : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float openHeight = 4f; // How far the door slides upward
    [SerializeField] private float speed = 2f;      // Door movement speed

    private Vector3 closedPos; // Original door position
    private Vector3 openPos;   // Target open position
    private bool isOpen = false;
    private bool playerNearby = false;

    void Start()
    {
        // Calculate positions at startup
        closedPos = transform.position;
        openPos = closedPos + Vector3.up * openHeight;
    }

    void Update()
    {
        // Smoothly move the door toward open or closed position
        Vector3 target = isOpen ? openPos : closedPos;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    /// <summary>
    /// Called by player on interact (E). Opens door only if player is nearby.
    /// </summary>
    public void Interact()
    {
        if (playerNearby)
        {
            isOpen = true;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.doorClip);
        }
    }

    /// <summary>
    /// Tracks if player is in door trigger zone.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    /// <summary>
    /// When player leaves, automatically closes the door.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            isOpen = false;
        }
    }
}
