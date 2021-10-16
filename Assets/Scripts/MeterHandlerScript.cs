using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterHandlerScript : MonoBehaviour
{
    [SerializeField] Slider p1Health, p2Health, p1Lerper, p2Lerper;
    [SerializeField] Text p1Combo, p2Combo, timeText;
    public UniversalPlayerScript playerOne, playerTwo;
    [SerializeField] int timeFrames = 60, time = 10;
    [SerializeField] GameObject timeUpText, winText, closeWinText;
    void Update()
    {
        p1Health.value = playerOne.health;
        p2Health.value = playerTwo.health;
        p1Lerper.value = Mathf.Lerp(p1Lerper.value, p1Health.value, Time.deltaTime);
        p2Lerper.value = Mathf.Lerp(p2Lerper.value, p2Health.value, Time.deltaTime);
        p1Combo.gameObject.SetActive(playerOne.currentComboCount > 1); 
        p2Combo.gameObject.SetActive(playerTwo.currentComboCount > 1);
        if (p1Combo.gameObject.activeSelf) p1Combo.text = "" + playerOne.currentComboCount;
        if (p2Combo.gameObject.activeSelf) p2Combo.text = "" + playerTwo.currentComboCount;

        timeText.text = "" + time;
    }

    private void FixedUpdate()
    {
        timeFrames--;
        if(timeFrames <= 0)
        {
            time--;
            timeFrames = 60;
        }
        if (time <= 0) timeUpText.SetActive(true);
    }
}
