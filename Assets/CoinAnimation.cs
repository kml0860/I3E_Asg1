// File: Assets/Scripts/CoinAnimation.cs

using UnityEngine;

/*
 * Author: Kai Ming
 * Date: 2025-06-14
 * Description: Rotates and bobs the coin for visual feedback.
 */

/// <summary>
/// Handles rotating and floating the coin in place to draw player attention.
/// </summary>
public class CoinAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float rotationSpeed = 90f; // degrees per second
    [SerializeField] private float bobHeight = 0.25f;   // vertical movement distance
    [SerializeField] private float bobSpeed = 2f;       // bobbing frequency

    private Vector3 startPos; // original starting position of the coin

    void Start()
    {
        // Record initial position to base bobbing offset on
        startPos = transform.position;
    }

    void Update()
    {
        // Rotate around Y-axis (global space)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // Apply vertical bobbing motion using a sine wave
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
