using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAssignScreenScript : MonoBehaviour
{
    MultiverseGodScript godlet;
    // Start is called before the first frame update
    void Start()
    {
        godlet = GameObject.FindGameObjectWithTag("Godlet").GetComponent<MultiverseGodScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
