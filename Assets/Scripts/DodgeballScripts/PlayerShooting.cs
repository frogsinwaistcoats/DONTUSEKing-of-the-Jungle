using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public int playerID;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed = 10f;
    public bool isShooter; // Set this in the inspector or dynamically

    private InputControls playerControls;

    private void Start()
    {
        InputManager.instance.onPlayerJoined += AssignInputs;
    }

    private void OnDisable()
    {
        if (playerControls != null)
        {
            playerControls.Disable();
        }
    }

    private void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            InputManager.instance.onPlayerJoined -= AssignInputs;
            playerControls = InputManager.instance.players[playerID].playerControls;

            if (isShooter)
            {
                playerControls.Player.Shoot.performed += ctx => Shoot();
            }
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = shootPoint.forward * projectileSpeed; // Use forward direction for shooting
    }
}
