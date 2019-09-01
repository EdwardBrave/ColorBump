using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public GameObject FinishLine;

    public event Action<object, GameOverEventArgs> GameOver;

    public List<Collider> colisionExcludes;

    private Color playerColor;

    private void Start()
    {
        playerColor = gameObject.GetComponent<Renderer>().material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!enabled || colisionExcludes?.IndexOf(collision.collider) != -1)
            return;
        if (collision.gameObject.GetComponent<Renderer>().material.color != playerColor)
            GameOver(this, new GameOverEventArgs(GameOverEventArgs.LOOSE));
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject == FinishLine)
            GameOver(this, new GameOverEventArgs(GameOverEventArgs.WIN));
    }

}

public class GameOverEventArgs : EventArgs
{
    public const string WIN = "GameOver:win";
    public const string LEAVE = "GameOver:leave";
    public const string LOOSE = "GameOver:loose";
    public readonly string result;

    public GameOverEventArgs(string result)
    {
        this.result = result;
    }
}
