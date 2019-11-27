using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPhoton : MonoBehaviour
{
    public string versionName = "0.1";
    public Text ConnectionStatus;
    public string myroomName;
    public InputField createRoomInput, joinRoomInput;
    public GameObject loginMenu;


    private string playerCount;
    private bool connected;


    public void Update()
    {
        //Debug.Log("playersActive  ::"+PhotonNetwork.countOfPlayers);
        if (connected)
        {
            ConnectionStatus.text = "Connected ::" + "   Active Players:" + PhotonNetwork.countOfPlayers;
        }
        else if (!connected)
        {
            ConnectionStatus.text = "Disconnected ::" + "   Active Players:" + PhotonNetwork.countOfPlayers;
        }
        else Debug.Log("unknown connection problem");

    }

    public void Start()
    {
        PhotonNetwork.ConnectUsingSettings(versionName);
    }



    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("we are connected to master");
    }

    private void OnJoinedLobby()
    {
        connected = true;
        ConnectionStatus.color = Color.green;
    }

    private void OnDisconnectedFromPhoton()
    {
        connected = false;
        ConnectionStatus.color = Color.red;
    }
    private void OnJoinedRoom()
    {
        Debug.Log("Room joined");
        loginMenu.SetActive(false);
    }


    public void createMyRoom()
    {
        if (createRoomInput.text.Length >= 1)
            PhotonNetwork.CreateRoom(createRoomInput.text, new RoomOptions() { MaxPlayers = 2 }, null); ;
    }
    public void joinMyRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomInput.text);
    }

}


