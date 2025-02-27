using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCont : MonoBehaviour
{
    public int WhoseTurn;  // 0= X and 1 = O turn
    public int TurnCount;   //counts the number of turns played
    public GameObject[] turnIcons; //Displays whose turn is 
    public Sprite[] playerIcons;  //0=X icon and 1=O icon
    public Button[] tictacspaces;  //playable space for our game
    public int[] markedspaces; //spaces marked by the specific player
    public TextMeshProUGUI WinnerText;   //Hold who will win text
    public GameObject[] WinLine;  //holds the winning lines
    public GameObject winnerpanel;
    public GameObject drawPanel;
    public TextMeshProUGUI drawText;
    public int xPlayersScore;
    public int oPlayerScore;
    public TextMeshProUGUI xPlayerScoreText;
    public TextMeshProUGUI oPlayerScoreText;
    public Button xPlayerButton;
    public Button oPlayerButton;
    public GameObject DrawImage;
    public AudioSource buttonclickAudio;

    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        WhoseTurn = 0;
        TurnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);

        for (int i=0;i<tictacspaces.Length;i++)
        {
            tictacspaces[i].interactable = true;
            tictacspaces[i].GetComponent<Image>().sprite = null;
        }

        for (int i=0; i<markedspaces.Length;i++)
        {
            markedspaces[i] = -100;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TicTacButton(int whichNumber)
    {
        xPlayerButton.interactable = false;
        oPlayerButton.interactable = false;
        tictacspaces[whichNumber].image.sprite = playerIcons[WhoseTurn];
        tictacspaces[whichNumber].interactable = false;

        markedspaces[whichNumber] = WhoseTurn+1;
        TurnCount++;

        if(TurnCount>4)
        {
            bool iswinner = WinnerCheck();
            if (TurnCount == 9 && iswinner == false)
            {
                draw();
            }
        }

        if (WhoseTurn == 0)
        {
            WhoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            WhoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }



    bool WinnerCheck()
    {
        int s1 = markedspaces[0] + markedspaces[1] + markedspaces[2];
        int s2 = markedspaces[3] + markedspaces[4] + markedspaces[5];
        int s3 = markedspaces[6] + markedspaces[7] + markedspaces[8];
        int s4 = markedspaces[0] + markedspaces[3] + markedspaces[6];
        int s5 = markedspaces[1] + markedspaces[4] + markedspaces[7];
        int s6 = markedspaces[2] + markedspaces[5] + markedspaces[8];
        int s7 = markedspaces[2] + markedspaces[4] + markedspaces[6];
        int s8 = markedspaces[0] + markedspaces[4] + markedspaces[8];

        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
        for(int i=0;i<solutions.Length;i++)
        {
            if(solutions[i]==3*(WhoseTurn+1))
            {
                WinnerDisplay(i);
                return true;
            }
        }
        return false;
    }

    void WinnerDisplay(int indexIn)
    {
        winnerpanel.gameObject.SetActive(true);
        if(WhoseTurn==0)
        {
            xPlayersScore++;
            xPlayerScoreText.text = xPlayersScore.ToString();
            WinnerText.text = "Player X wins!";


        }
        else if(WhoseTurn == 1)
        {
            oPlayerScore++;
            oPlayerScoreText.text = oPlayerScore.ToString();
            WinnerText.text = "Player O wins!";
        }
        WinLine[indexIn].SetActive(true);
    }


    public void Rematch()
    {
        GameSetup();
        for (int i=0;i<WinLine.Length;i++)
        {
            WinLine[i].SetActive(false);
        }
        winnerpanel.SetActive(false);
        drawPanel.SetActive(false);
        xPlayerButton.interactable = true;
        oPlayerButton.interactable = true;
        DrawImage.SetActive(false);
    }

    public void Restart()
    {
        Rematch();
        xPlayersScore = 0;
        oPlayerScore = 0;
        xPlayerScoreText.text = "0";
        oPlayerScoreText.text = "0";
    }    

    public void SwitchPlayer(int whichPlayer)
    {
        if(whichPlayer==0)
        {
            WhoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
        else if(whichPlayer==1)
        {
            WhoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
    }

    void draw()
    {
        //winnerpanel.SetActive(true);
        drawPanel.gameObject.SetActive(true);
        drawText.text = "Draw!";
        DrawImage.SetActive(true);
    }

    public void PlayOnClick()
    {
        buttonclickAudio.Play();
    }

}
