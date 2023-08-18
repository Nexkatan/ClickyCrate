using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongButton : MonoBehaviour
{
    private GameManager gameManager;
    public Button button;
    public int difficulty;


    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.isSongMode = true;
        button.onClick.AddListener(gameManager.StartSongGame);
    }
}
