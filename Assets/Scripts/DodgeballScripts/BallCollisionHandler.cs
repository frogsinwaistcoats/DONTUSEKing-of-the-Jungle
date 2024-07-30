using UnityEngine;

public class BallCollisionHandler : MonoBehaviour
{
    public GameManagerDodgeball gameManager;

    private void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManagerDodgeball>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the ball touches the floor
        if (other.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }

        // Check if the ball touches the target player
        if (other.gameObject.CompareTag("TargetPlayer"))
        {
            Destroy(gameObject);
            gameManager.EndGame();
        }
    }
}