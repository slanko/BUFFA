using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterHandlerScript : MonoBehaviour
{
    [SerializeField] Slider p1Health, p2Health, p1Lerper, p2Lerper;
    public UniversalPlayerScript playerOne, playerTwo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        p1Health.value = playerOne.health;
        p2Health.value = playerTwo.health;
        p1Lerper.value = Mathf.Lerp(p1Lerper.value, p1Health.value, Time.deltaTime);
        p2Lerper.value = Mathf.Lerp(p2Lerper.value, p2Health.value, Time.deltaTime);
    }
}
