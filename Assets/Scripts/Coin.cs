using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int scoreReward;
    float startYPosition;

    void Start()
    {
        startYPosition = transform.position.y;
    }

    private void Update()
    {
        MoveVertically();
    }

    private void MoveVertically()
    {
        float newYPos = startYPosition + (Mathf.Sin(Time.time * 1f) * 0.2f);
        transform.position = new Vector2(transform.position.x, newYPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().IncreaseScore(scoreReward);
            Destroy(gameObject);
        }
    }
}
