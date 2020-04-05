using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion, playerExplosion;
    public int score;
    private GameController controller;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag("Boundary") && !collider.CompareTag("Enemy"))
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            if (!collider.CompareTag("Player"))
            {
                controller.AddScore(score);
            }
            if (collider.CompareTag("Player"))
            {
                controller.GameOver();
                Instantiate(playerExplosion, collider.transform.position, collider.transform.rotation);
            }
        }
    }
}