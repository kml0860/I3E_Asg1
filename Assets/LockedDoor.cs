// File: Assets/Scripts/LockedDoor.cs

using UnityEngine;

/// <summary>
/// Door that opens only if player has a matching key.
/// </summary>
public class LockedDoor : MonoBehaviour
{
    [SerializeField] private string requiredKey = "Green";
    [SerializeField] private Vector3 openOffset = new Vector3(0, 3, 0);
    [SerializeField] private float openSpeed = 2f;

    private Vector3 closedPos;
    private Vector3 openPos;
    private bool isOpen = false;
    private bool playerNearby = false;

    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + openOffset;
    }

    void Update()
    {
        Vector3 target = isOpen ? openPos : closedPos;
        transform.position = Vector3.MoveTowards(transform.position, target, openSpeed * Time.deltaTime);
    }

    public void TryOpen(PlayerBehaviour player)
    {
        if (player.HasKey(requiredKey))
        {
            isOpen = true;
            Debug.Log($"Opened door with key: {requiredKey}");
        }
        else
        {
            Debug.Log($"Missing key: {requiredKey}");
        }
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
