using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public int playerID;
    MultiplayerInputManager inputManager; //Change this
    public Vector2 moveInput;
    public float moveSpeed;
    private Rigidbody rb;

    public Collider spinCollider;
    public Collider finishCollider;
    public float spinDuration = 1f;

    InputControls inputControls;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //Add this
        inputManager = MultiplayerInputManager.instance;
        if (inputManager.players.Count >= playerID + 1)
        {
            AssignInputs(playerID);
        }
        else
        {
            inputManager.onPlayerJoined += AssignInputs;
        }
        ////////////
    }

    private void OnDisable()
    {
        if (inputControls != null)
        {
            inputControls.MasterControls.Movement.performed -= OnMove;
            inputControls.MasterControls.Movement.canceled -= OnMove;
        }
        else
        {
            inputManager.onPlayerJoined -= AssignInputs;
        }
    }

    void AssignInputs(int ID)
    {
        if (playerID == ID)
        {
            inputManager.onPlayerJoined -= AssignInputs;
            inputControls = inputManager.players[playerID].playerControls;
            inputControls.MasterControls.Movement.performed += OnMove;
            inputControls.MasterControls.Movement.canceled += OnMove;
        }
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        moveInput = obj.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position +  movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == spinCollider)
        {            
            StartCoroutine(SpinPlayer());
        }

        if (other == finishCollider)
        {

        }
    }

    private IEnumerator SpinPlayer()
    {
        OnDisable();
        spinCollider.enabled = false;

        float timeElapsed = 0f;
        float totalSpins = 2f;
        float spinsPerSecond = totalSpins / spinDuration;
        float rotationSpeed = spinsPerSecond * 360f;

        while (timeElapsed < spinDuration)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        SpinControls(playerID);
    }

    private void SpinControls(int ID)
    {
        if (playerID == ID)
        {
            inputControls = inputManager.players[playerID].playerControls;

            //randomly generate number
            int randomControl = Random.Range(1, 4);

            switch (randomControl)
            {
                case 1:
                    inputControls.SpinControl1.Movement.performed += OnMove;
                    inputControls.SpinControl1.Movement.canceled += OnMove;
                    break;
                case 2:
                    inputControls.SpinControl2.Movement.performed += OnMove;
                    inputControls.SpinControl2.Movement.canceled += OnMove;
                    break;
                case 3:
                    inputControls.SpinControl3.Movement.performed += OnMove;
                    inputControls.SpinControl3.Movement.canceled += OnMove;
                    break;
            }
        }
    }
}
