using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


//What makes him the God of the Multiverse?? DontDestroyOnLoad!!!!!!!
public class MultiverseGodScript : MonoBehaviour
{
    public InputDevice p1Device, p2Device; //this does NOT want to serialize and i think that is cringe
    public string deviceOne, deviceTwo;
    public Scene characterSelect, actualGame;
    PlayerInputManager inputManager;

    //ASSIGN SCREEN SPECIFIC
    public bool sideOneLockedIn, sideTwoLockedIn;
    GameObject leftLockedInText, rightLockedInText;

    //CHARACTER SELECT SCREEN SPECIFIC
    public CharacterFile characterOne, characterTwo;
    public bool oneReady, twoReady;
    Text p1Name, p2Name;

    //GAME SCREEN SPECIFIC
    CameraScript camScript;
    MeterHandlerScript meterHandler;

    // Start is called before the first frame update
    void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
        leftLockedInText = GameObject.Find("LEFTLOCKEDIN");
        rightLockedInText = GameObject.Find("RIGHTLOCKEDIN");
        //kill other godlets. there can be only one
        GameObject[] godlets = GameObject.FindGameObjectsWithTag("Godlet");
        if(godlets.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            //assign controls to playerInputs on lovely little pointers
            Debug.Log("DING!!");
            GameObject.Find("P1Selector").GetComponent<PlayerInput>().SwitchCurrentControlScheme(p1Device);
            GameObject.Find("P2Selector").GetComponent<PlayerInput>().SwitchCurrentControlScheme(p2Device);
        }
        if(level == 2)
        {
            spawnPlayers();
        }
    }

    private void Update()
    {
        if (p1Device != null)
            deviceOne = p1Device.ToString();
        else deviceOne = "none";

        if (p2Device != null)
            deviceTwo = p2Device.ToString();
        else deviceTwo = "none";

        switch (SceneManager.GetActiveScene().buildIndex)
        {
                //assign
            case 0:
                if (sideOneLockedIn && sideTwoLockedIn)
                {
                    inputManager.DisableJoining();
                    SceneManager.LoadScene(1);
                }
                leftLockedInText.SetActive(sideOneLockedIn);
                rightLockedInText.SetActive(sideTwoLockedIn);
                break;
                //select
            case 1:
                if (oneReady && twoReady) SceneManager.LoadScene(2);
                break;
        }
    }

    public void spawnPlayers()
    {
        UniversalPlayerScript playerOne = Instantiate(characterOne.character, new Vector3(-3.25f, 1.5f, 0f), Quaternion.identity).GetComponent<UniversalPlayerScript>();
        UniversalPlayerScript playerTwo = Instantiate(characterTwo.character, new Vector3(3.25f, 1.5f, 0f), Quaternion.identity).GetComponent<UniversalPlayerScript>();
        playerOne.P2 = false;
        playerTwo.P2 = true;
        playerOne.target = playerTwo.transform;
        playerTwo.target = playerOne.transform;
        //set targets for camera
        camScript = GameObject.Find("CamBuddy").GetComponent<CameraScript>();
        camScript.playerList.Add(playerOne.transform);
        camScript.playerList.Add(playerTwo.transform);
        //hook up meters
        meterHandler = GameObject.Find("GOD").GetComponent<MeterHandlerScript>();
        meterHandler.playerOne = playerOne;
        meterHandler.playerTwo = playerTwo;
        playerOne.gameObject.GetComponent<PlayerInput>().SwitchCurrentControlScheme(p1Device);
        playerTwo.gameObject.GetComponent<PlayerInput>().SwitchCurrentControlScheme(p2Device);
        //nameplates
        p1Name = GameObject.Find("Canvas/P1Name").GetComponent<Text>();
        p2Name = GameObject.Find("Canvas/P2Name").GetComponent<Text>();
        p1Name.text = characterOne.characterName;
        p2Name.text = characterTwo.characterName;

    }
}
