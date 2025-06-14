// File: Assets/Scripts/CoinAnimation.cs

using UnityEngine;

/*
 * Author: Your Name
 * Date: 2025-06-14
 * Description: Rotates and bobs the coin for visual feedback.
 */

/// <summary>
/// Handles rotating and floating the coin in place.
/// </summary>
public class CoinAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float rotationSpeed = 90f; // degrees per second
    [SerializeField] private float bobHeight = 0.25f;
    [SerializeField] private float bobSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Rotate
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // Bob up/down
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
