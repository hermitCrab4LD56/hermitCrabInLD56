using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class FallingTrash : MonoBehaviour
{
    public Text resultText; // Assign a UI Text element in the Inspector

    private enum Choice { Rock, Paper, Scissors }

    private Choice player1Choice;
    private Choice player2Choice;
    private bool player1Ready = false;
    private bool player2Ready = false;

    public PlayerHealth pOneHealth;
    public PlayerHealth pTwoHealth;

    public GameObject crabOne;
    public GameObject crabTwo;
    public GameObject crabOnet;
    public GameObject crabTwot;

    Vector3 playerOneStartPos;
    Vector3 playerTwoStartPos;

    public GameObject guessPart;
    public SpriteRenderer[] chara;
    public Sprite[] charaImages;

    public GameObject successUI;
    public GameObject missUI;
    public GameObject jointUI;
    //pOne.currentHealth
    private void Start()
    {
        playerOneStartPos = crabOne.transform.position;
        playerTwoStartPos = crabTwo.transform.position;
    }
    public void RockStart()
    {
        //crabOne.SetActive(false);
        crabOne.GetComponent<SpriteRenderer>().enabled = false;
        //crabOne.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        //crabTwo.SetActive(false);
        crabTwo.GetComponent<SpriteRenderer>().enabled = false;
        //crabTwo.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        crabOnet.SetActive(true);
        crabTwot.SetActive(true);

        guessPart.SetActive(true);
        StartCoroutine("BlinkOne");
        StartCoroutine("BlinkTwo");
    }

    public void AfterPressKeyOne()
    {
        StopCoroutine("BlinkOne");
        for (int i = 0; i < 3; i++)
        {
            chara[i].sprite = charaImages[i * 2];
        }
    }
    public void AfterPressKeyTwo()
    {
        StopCoroutine("BlinkTwo");
        for (int i = 3; i < 6; i++)
        {
            chara[i].sprite = charaImages[i * 2];
        }
    }

    public void Update()
    {
        //
        //if ((pOneHealth.currentHealth == 0) || (pTwoHealth.currentHealth == 0))
        if (crabOnet.activeSelf || crabTwot.activeSelf)
        {
            // Player 1 Input
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SetPlayerChoice(1, Choice.Rock);
                AfterPressKeyOne();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                SetPlayerChoice(1, Choice.Paper);
                AfterPressKeyOne();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                SetPlayerChoice(1, Choice.Scissors);
                AfterPressKeyOne();
            }

            // Player 2 Input
            if (Input.GetKeyDown(KeyCode.U))
            {
                SetPlayerChoice(2, Choice.Rock);
                AfterPressKeyTwo();
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                SetPlayerChoice(2, Choice.Paper);
                AfterPressKeyTwo();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                SetPlayerChoice(2, Choice.Scissors);
                AfterPressKeyTwo();
            }

            // Check if both players are ready
            if (player1Ready && player2Ready)
            {
                if (player1Choice == Choice.Rock) { chara[0].sprite = charaImages[1]; }
                if (player1Choice == Choice.Paper) { chara[1].sprite = charaImages[3]; }
                if (player1Choice == Choice.Scissors) { chara[2].sprite = charaImages[5]; }
                if (player2Choice == Choice.Rock) { chara[3].sprite = charaImages[7]; }
                if (player2Choice == Choice.Paper) { chara[4].sprite = charaImages[9]; }
                if (player2Choice == Choice.Scissors) { chara[5].sprite = charaImages[11]; }
                DetermineWinner();
                ResetChoices();
            }

            //crabOnes.SetActive(false);
            //crabTwos.SetActive(false);
            //crabOne.GetComponent<SpriteRenderer>().enabled = true;
            //crabTwo.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void SetPlayerChoice(int player, Choice choice)
    {
        if (player == 1)
        {
            player1Choice = choice;
            player1Ready = true;
        }
        else
        {
            player2Choice = choice;
            player2Ready = true;
        }
    }

    private void DetermineWinner()
    {
        if (player1Choice == player2Choice)
            successUI.SetActive(true);
        //resultText.text = $"Both chose {player1Choice}. Dodge Trash Success!";
        else
        {
            pOneHealth.lives -= 1;
            pTwoHealth.lives -= 1;
            pOneHealth.UpdateLives();
            pTwoHealth.UpdateLives();
            missUI.SetActive(true);
            //resultText.text = $"You chose the different key. Missed";
        }
        //play animation
        StartCoroutine("ResetToBattle");
    }

    private void ResetChoices()
    {
        player1Ready = false;
        player2Ready = false;
    }

    IEnumerator ResetToBattle()
    {
        yield return new WaitForSeconds(2);
        crabOne.GetComponent<SpriteRenderer>().enabled = true;
        //crabOne.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        crabTwo.GetComponent<SpriteRenderer>().enabled = true;
        //crabTwo.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        crabOnet.SetActive(false);
        crabTwot.SetActive(false);
        //pOneHealth.currentHealth = pOneHealth.maxHealth;
        //pTwoHealth.currentHealth = pTwoHealth.maxHealth;
        crabOne.transform.position = playerOneStartPos;
        crabTwo.transform.position = playerTwoStartPos;
        guessPart.SetActive(false);
    }

    IEnumerator BlinkOne()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                chara[i].sprite = charaImages[i * 2];
            }
            yield return new WaitForSeconds(0.35f);
            for (int i = 0; i < 3; i++)
            {
                chara[i].sprite = charaImages[i * 2 + 1];
            }
            yield return new WaitForSeconds(0.35f);
        }
    }
    IEnumerator BlinkTwo()
    {
        while (true)
        {
            for (int i = 3; i < 6; i++)
            {
                chara[i].sprite = charaImages[i * 2];
            }
            yield return new WaitForSeconds(0.35f);
            for (int i = 3; i < 6; i++)
            {
                chara[i].sprite = charaImages[i * 2 + 1];
            }
            yield return new WaitForSeconds(0.35f);
        }
    }
}
