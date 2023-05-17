using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    private Memory_Card firstSelectedCard;
    private Memory_Card secondSelectedCard;

    public int scorePlayer1;
    public int scorePlayer2;
    public int clickCounter;

    public AudioSource cardUp;
    public AudioSource cardDown;
    public AudioSource matchCard;

    public bool playerIndex = true;
    private bool canClick = true;

    private Vector3 testVector = new Vector3(0, 0, 0);
    public Vector3[] position = new Vector3[16];

    private void Start()
    {

    }
    public void ClickCard(Memory_Card card)
    {
        // if second card is selected, method is abonded
        if (canClick == false)
        {
            return;
        }

        // if no card is already selected
        if (firstSelectedCard == null)
        {
            firstSelectedCard = card;
            cardUp.Play();         
            card.targetHeight = 0.35f;
            card.targetRotation = 90;
            clickCounter++;
        }

        // if one card is selected, method goes for second card
        else
        {
            secondSelectedCard = card;
            cardUp.Play();
            card.targetRotation = 90;
            card.targetHeight = 0.35f;
            clickCounter = 0;
            canClick = false;
            Invoke("CompareCards", 1);
        }
    }

    public void CompareCards()
    {
        if (firstSelectedCard.ID == secondSelectedCard.ID && firstSelectedCard != secondSelectedCard)
        {
            if (playerIndex == true)
            {
                scorePlayer1++;
            }
            else
            {
                scorePlayer2++;
            }
            matchCard.Play();
            Destroy(firstSelectedCard.gameObject);
            Destroy(secondSelectedCard.gameObject);
            canClick = true;
        }
        else
        {
            SwitchPlayer();
            cardDown.Play();           
            firstSelectedCard.targetRotation = -90;
            secondSelectedCard.targetRotation = -90;
            firstSelectedCard.targetHeight = 0.05f;
            secondSelectedCard.targetHeight = 0.05f;
            firstSelectedCard = null;
            secondSelectedCard = null;
            canClick = true;
        }
    }

    bool SwitchPlayer()
    {
        if (playerIndex == true)
        {
            playerIndex = false;
        }
        else
        {
            playerIndex = true;
        }
        return playerIndex;
    }


    public void SetNewPosition(Memory_Card card)
    {
        bool positionSet = false;
        do
        {
            int rndIndex = Random.Range(0, 16);
            if (position[rndIndex] != testVector)
            {
                card.transform.position = position[rndIndex];
                position[rndIndex] = testVector;
                positionSet = true;
            }
        } while (positionSet == false);
    }   
}
