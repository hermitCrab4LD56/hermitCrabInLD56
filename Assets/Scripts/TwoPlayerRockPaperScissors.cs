using UnityEngine;
using UnityEngine.UI;

public class TwoPlayerRockPaperScissors : MonoBehaviour
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
    public GameObject crabOnes;
    public GameObject crabTwos;

    public Transform playerOneStartTrans;
    public Transform playerTwoStartTrans;



    //pOne.currentHealth

    public void RockStart()
    {
        //crabOne.SetActive(false);
        crabOne.GetComponent<SpriteRenderer>().enabled = false;
        crabOne.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        //crabTwo.SetActive(false);
        crabTwo.GetComponent<SpriteRenderer>().enabled = false;
        crabTwo.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        crabOnes.SetActive(true);
        crabTwos.SetActive(true);
    }

    public void Update()
    {
        //if ((pOneHealth.currentHealth == 0) || (pTwoHealth.currentHealth == 0))
        if (crabOnes.activeSelf || crabTwos.activeSelf)
        {// Player 1 Input
            if (Input.GetKeyDown(KeyCode.Q)) { SetPlayerChoice(1, Choice.Rock); }
            if (Input.GetKeyDown(KeyCode.W)) { SetPlayerChoice(1, Choice.Paper); }
            if (Input.GetKeyDown(KeyCode.E)) { SetPlayerChoice(1, Choice.Scissors); }

            // Player 2 Input
            if (Input.GetKeyDown(KeyCode.U)) { SetPlayerChoice(2, Choice.Rock); }
            if (Input.GetKeyDown(KeyCode.I)) { SetPlayerChoice(2, Choice.Paper); }
            if (Input.GetKeyDown(KeyCode.O)) { SetPlayerChoice(2, Choice.Scissors); }

            // Check if both players are ready
            if (player1Ready && player2Ready)
            {
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
            resultText.text = $"Both chose {player1Choice}. It's a tie!";
        else
        {
            string winner = null;
            if (pOneHealth.currentHealth != 0)
            {
                pTwoHealth.lives -= 1;
                winner = "Player 1 wins!";
            }
            else
            {
                pOneHealth.lives -= 1;
                winner = "Player 2 wins!";
            }
            resultText.text = $"Player 1 chose: {player1Choice}\nPlayer 2 chose: {player2Choice}\n{winner}";
            pOneHealth.currentHealth = pOneHealth.maxHealth;
            pTwoHealth.currentHealth = pTwoHealth.maxHealth;
            crabOne.transform.position = playerOneStartTrans.position;
            crabTwo.transform.position = playerTwoStartTrans.position;
        }

    }

    private void ResetChoices()
    {
        player1Ready = false;
        player2Ready = false;
    }
}
