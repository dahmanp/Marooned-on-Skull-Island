using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public GameBehavior gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Speed Boost, but -5 HP!");

            PlayerBehavior Player = collision.gameObject.GetComponent<PlayerBehavior>();
            gameManager.moveSpeed += 10;
            gameManager.HP -= 5;
        }
    }
}
