using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class handler : Photon.PunBehaviour, IPunObservable
{
    public Button[] Mybutton;
    public Text[] MyText;
    public Text results;
    public Text PlayerIndex;
    public Text test;
    public GameObject overlay;

    public int myvalue;
    public string sign;
    public bool attemptComplete;
    public bool playerAcive;
    public int winner;

    private string x = "X";
    private string o = "O";




    [PunRPC]
    void Update()
    {
        //PhotonNetwork.isMasterClient;
        if (photonView.isMine)
        {
            if (winner == 0)
            {
                PlayerIndex.text = "Player 1 :: [X]";

                if (!attemptComplete)
                {
                    overlay.SetActive(false);
                }
                else if (attemptComplete)
                {
                    overlay.SetActive(true);
                }
            }
            else if (winner == 1)
            {
                results.text = "won";
                overlay.SetActive(false);
            }
            else if (winner == 2)
            {
                results.text = "lose";
                overlay.SetActive(false);
            }

        }
        else if (!photonView.isMine)
        {
            PlayerIndex.text = "Player 2 :: [O]";

            if (winner == 0)
            {
                if (!attemptComplete)
                {
                    overlay.SetActive(true);
                }
                else if (attemptComplete)
                {
                    overlay.SetActive(false);
                }
            }
            else if (winner == 1)
            {
                results.text = "lose";
                overlay.SetActive(false);
            }
            else if (winner == 2)
            {
                results.text = "won";
                overlay.SetActive(false);
            }
        }

    }
    [PunRPC]
    void checkForMatch()
    {
        //Debug.Log("checkformatch is running");

        if (MyText[0].text == MyText[1].text && MyText[1].text == MyText[2].text)
        {
            checkforSign(MyText[0].text);
        }
        if (MyText[3].text == MyText[4].text && MyText[4].text == MyText[5].text)
        {
            checkforSign(MyText[3].text);
        }
        if (MyText[6].text == MyText[7].text && MyText[7].text == MyText[8].text)
        {
            checkforSign(MyText[6].text);
        }
        if (MyText[0].text == MyText[3].text && MyText[3].text == MyText[6].text)
        {
            checkforSign(MyText[0].text);
        }
        if (MyText[1].text == MyText[4].text && MyText[4].text == MyText[7].text)
        {
            checkforSign(MyText[1].text);
        }
        if (MyText[2].text == MyText[5].text && MyText[5].text == MyText[8].text)
        {
            checkforSign(MyText[2].text);
        }
        if (MyText[0].text == MyText[4].text && MyText[4].text == MyText[8].text)
        {
            checkforSign(MyText[0].text);
        }
        if (MyText[2].text == MyText[4].text && MyText[4].text == MyText[6].text)
        {
            checkforSign(MyText[2].text);
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {

        }
        else if (stream.isReading)
        {

        }

    }

    [PunRPC]
    public void MyTurn(int tst)
    {
        if (this.GetComponent<PhotonView>())
        {
            photonView.RPC("MyTurnProccess", PhotonTargets.All, tst);
            photonView.RPC("checkForMatch", PhotonTargets.All, null);
        }
        //else Debug.Log("no componenet found");
    }

    [PunRPC]
    public void MyTurnProccess(int index)
    {
        if (!attemptComplete)
        {
            MyText[index].text = x;
            Mybutton[index].interactable = false;
            attemptComplete = true;
            test.text = "player 1 turn";
        }
        else if (attemptComplete)
        {
            MyText[index].text = o;
            Mybutton[index].interactable = false;
            attemptComplete = false;
            test.text = "player 2 turn";
        }
    }

    //AIsTurn();

    //attempts += 1;
    //if (attempts == 9)
    //{
    //    finishGame(myvalue);
    //}

    void AIsTurn()
    {
        int a = 0;
        while (MyText[a].text == x || MyText[a].text == o)
        {
            a = Random.Range(0, 9);
        }
        MyText[a].text = o;
        Mybutton[a].interactable = false;
    }

    [PunRPC]
    void checkforSign(string a)
    {
        if (a == x)
        {
            //results.text = "Lose :)";
            finishGame(myvalue);
            winner = 1;

        }
        else if (a == o)
        {
            //results.text = "Win :(";
            finishGame(myvalue);
            winner = 2;
        }
    }

    [PunRPC]
    void finishGame(int tst)
    {
        foreach (Button button in Mybutton)
        {
            button.interactable = false;
        }
        test.text = tst.ToString();
    }

}
