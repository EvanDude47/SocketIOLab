using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class Network : MonoBehaviour {
    static SocketIOComponent socket;
    public GameObject playerPrefab;

	//Use this for initialization
	void Start () {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);
        socket.On("spawn player", OnSpawned);
	}
	
    //Tells us that we are connected
	void OnConnected(SocketIOEvent e)
    {
        Debug.Log("We are Connected");
        socket.Emit("playerhere");
    }

    void OnSpawned(SocketIOEvent e)
    {
        Debug.Log("Player Spawned!");
        Instantiate(playerPrefab);
    }
}
