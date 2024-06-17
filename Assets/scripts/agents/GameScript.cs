using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameScript : MonoBehaviour
{
    public TMP_Text scoreToBeatText;
    public GameObject countDown;
    public TMP_Text countDownDisplay;
    public PuckScript puckScript;
    public HockeyAgent agentScript;
    public GameObject pointsChoice;
    public GameObject agent;
    public GameObject playButton;
    public GameObject rematchButton;

    private GameObject puck;
    private bool gameStarted = false;
    private int scoreToBeat = 1;

    public void Start()
    {
        puck = agentScript.puck;
        StartCoroutine(PreLoadAgent());
    }

    private IEnumerator PreLoadAgent()
    {
        agent.SetActive(true);
        yield return new WaitForSeconds(1f);
        agentScript.EndEpisode();
        agent.SetActive(false);
    }

    public void FixedUpdate()
    {
        if (gameStarted)
        {
            if (puckScript.blueScore >= scoreToBeat)
            {
                GameOver(winner: "The AI");
            }
            else if (puckScript.redScore >= scoreToBeat)
            {
                GameOver(winner: "You");
            }
        }
    }
    public void PressPlay()
    {
        countDown.SetActive(false);
        playButton.SetActive(false);
        rematchButton.SetActive(false);
        pointsChoice.SetActive(true);
    }

    public void StartGame()
    {
        pointsChoice.SetActive(false);
        countDown.SetActive(true);
        gameStarted = true;
        StartCoroutine(CountDownAndStart());
    }

    private IEnumerator CountDownAndStart()
    {
        countDownDisplay.text = "READY?";
        yield return new WaitForSeconds(1f);

        for (int i = 3; i > 0; i--)
        {
            countDownDisplay.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countDownDisplay.text = "GO!";
        yield return new WaitForSeconds(0.1f);

        countDown.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        puck.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        agent.SetActive(true);
    }

    public void IncreaseScore()
    {
        if (scoreToBeat < 5)
        {
            scoreToBeat = 5;
        }
        else
        {
            scoreToBeat += 5;
        }
        scoreToBeatText.text = scoreToBeat.ToString();
    }

    public void DecreaseScore()
    {
        if (scoreToBeat > 5)
        {
            scoreToBeat -= 5;
        }
        else
        {
            scoreToBeat = 1;
        }
        scoreToBeatText.text = scoreToBeat.ToString();
    }

    public void GameOver(string winner)
    {
        puckScript.redScore = 0;
        puckScript.blueScore = 0;

        puck.SetActive(false);
        agent.SetActive(false);
        countDown.SetActive(true);
        countDownDisplay.text = $"{winner} won!";
        rematchButton.SetActive(true);
    }
}
