// File: Assets/Scripts/LockedDoor.cs

using UnityEngine;

/*
 * Author: Kai Ming
 * Date: 2025-06-14
 * Description: Door that opens only if player has a matching key.
 */

/// <summary>
/// Door that opens only if player has a matching key.
/// </summary>
public class LockedDoor : MonoBehaviour
{
    [SerializeField] private string requiredKey = "Green"; // The tag of the key required to open this door
    [SerializeField] private Vector3 openOffset = new Vector3(0, 3, 0); // The offset from the closed position when the door is open
    [SerializeField] private float openSpeed = 2f; // The speed at which the door opens and closes

    private Vector3 closedPos; 
    private Vector3 openPos; 
    private bool isOpen = false; 
    private bool playerNearby = false; // True if the player is within the trigger zone (not used in current logic but kept for consistency with original)

    /// <summary>
    /// Initializes the closed and open positions of the door.
    /// </summary>
    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + openOffset;
    }

    /// <summary>
    /// Moves the door towards its target position (open or closed) each frame.
    /// </summary>
    void Update()
    {
        Vector3 target = isOpen ? openPos : closedPos;
        transform.position = Vector3.MoveTowards(transform.position, target, openSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Attempts to open the door if the player has the required key.
    /// </summary>
    public void TryOpen(PlayerBehaviour player)
    {
        if (player.HasKey(requiredKey))
        {
            isOpen = true; // Set door state to open
            AudioManager.Instance.PlaySFX(AudioManager.Instance.doorClip); // Play door opening sound effect
            Debug.Log($"Opened door with key: {requiredKey}");
        }
        else
        {
            Debug.Log($"Missing key: {requiredKey}");
            UIManager.Instance?.ShowKeyRequirement(requiredKey); // Display key requirement to the UI
        }
    }

    /// <summary>
    /// Resets the door to its closed state when the player exits the trigger.
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