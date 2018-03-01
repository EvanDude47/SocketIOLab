using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using SocketIO;

public class GameDataEditor : EditorWindow
{
    string gameDataFilePath = "/StreamingAssets/data.json";

    public GameData editorData;
    private GameObject server;
    private SocketIOComponent socket;

    [MenuItem("Window/Game Data Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(GameDataEditor)).Show();
    }

    void OnGUI()
    {
        if(editorData != null)
        {
            //Display json data
            SerializedObject serializedObj = new SerializedObject(this);
            SerializedProperty serializedProp = serializedObj.FindProperty("editorData");
            EditorGUILayout.PropertyField(serializedProp, true);
            serializedObj.ApplyModifiedProperties();

            if(GUILayout.Button("Save Game Data"))
            {
                SaveGameData();
            }

            if (GUILayout.Button("Send Game Data"))
            {
                SendGameData();
            }
        }

        if (GUILayout.Button("Load Game Data"))
        {
            LoadGameData();
        }
    }

    void LoadGameData()
    {
        string filePath = Application.dataPath + gameDataFilePath;

        //if(File.Exists(filePath))
        //{
        //    string gameData = File.ReadAllText(filePath);
        //    editorData = JsonUtility.FromJson<GameData>(gameData);
        //}
        //else
        //{
        //    editorData = new GameData();
        //}

        try
        {
            string gameData = File.ReadAllText(filePath);
            editorData = JsonUtility.FromJson<GameData>(gameData);
        }
        catch
        {
            editorData = new GameData();
        }
    }

    void SaveGameData()
    {
        string jsonObj = JsonUtility.ToJson(editorData);

        string filePath = Application.dataPath + gameDataFilePath;
        File.WriteAllText(filePath, jsonObj);
    }

    void SendGameData()
    {
        string jsonObj = JsonUtility.ToJson(editorData);

        server = GameObject.Find("Server");
        socket = server.GetComponent<SocketIOComponent>();

        socket.Emit("send data", new JSONObject(jsonObj));
    }
}
