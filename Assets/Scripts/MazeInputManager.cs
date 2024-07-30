using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MazeInputManager : MonoBehaviour
{
    public static MazeInputManager Instance;

    public List<IndividualPlayerControls> players = new List<IndividualPlayerControls>();
    int maxPlayers = 4;

    public InputControls inputControls;

    public delegate void OnPlayerJoined(int PlayerID);
    public OnPlayerJoined onPlayerJoined;

    private void Awake()
    {
        /*
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeInputs();
        }
        else
        {
            Destroy(gameObject);
        }
        */
        InitializeInputs();
    }

    //sets up the inputs for detecting different players
    void InitializeInputs()
    {
        inputControls = new InputControls();

        inputControls.MasterControls.JoinButton.performed += JoinButtonPerformed;
        inputControls.Enable();
    }

    private void JoinButtonPerformed(InputAction.CallbackContext obj)
    {
        if (players.Count >= maxPlayers)
        {
            return;
        }

        //check if device is already assigned to a player
        foreach (IndividualPlayerControls player in players)
        {
            if (player.inputDevice == obj.control.device)
            {
                //this device is already assigned so we can return out of this function
                return;
            }
        }

        IndividualPlayerControls newPlayer = new IndividualPlayerControls();
        newPlayer.SetupPlayer(obj, players.Count);
        players.Add(newPlayer);

        if (onPlayerJoined != null)
        {
            onPlayerJoined.Invoke(newPlayer.playerID);
        }
    }
}
