using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private BoardManager boardManager;
    private UImanager m_ui;

    private void Awake()
    {
        boardManager = FindObjectOfType<BoardManager>();
       
    }
    private void OnMouseDown()
    {
        StartCoroutine(boardManager.Flood((int)transform.position.x, (int)transform.position.y, Color.red,this.gameObject));
        StartCoroutine(boardManager.DownTile());
        StartCoroutine(boardManager.upTile());
        StartCoroutine(boardManager.createAtFirstRow());
       
    }
  
}
