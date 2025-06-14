// File: Assets/Scripts/SlidingDoor.cs

using UnityEngine;

/*
 * Author: Your Name
 * Date: 2025-06-14
 * Description: Sliding door that opens on interaction and closes when player leaves.
 */

/// <summary>
/// A door that slides vertically on interact and closes on exit.
/// </summary>
public class SlidingDoor : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float openHeight = 4f;
    [SerializeField] private float speed = 2f;

    private Vector3 closedPos;
    private Vector3 openPos;
    private bool isOpen = false;
    private bool playerNearby = false;

    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + Vector3.up * openHeight;
    }

    void Update()
    {
        Vector3 target = isOpen ? openPos : closedPos;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    public void Interact()
    {
        if (playerNearby) isOpen = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            isOpen = false;
        }
    }
}
