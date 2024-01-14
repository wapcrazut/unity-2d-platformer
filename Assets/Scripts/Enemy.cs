using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 moveOffset;
    Vector3 startPosition;
    Vector3 targetPosition;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = transform.position + moveOffset;
    }

    void Update()
    {
        MoveToTargetPosition();
    }

    private void MoveToTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // All positions are equal? Then set the offset as target
        if (transform.position == targetPosition && targetPosition == startPosition)
        {
            targetPosition = startPosition + moveOffset;
        } 
        // Reached the end? Set the start position as target
        else if (transform.position == targetPosition)
        {
            targetPosition = startPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().GameOver();
        }
    }
}
