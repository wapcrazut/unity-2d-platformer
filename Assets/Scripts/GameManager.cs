using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject score;
    [SerializeField] PlayerController player;
    TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = score.GetComponent<TextMeshProUGUI>();
        player = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = player.GetScore().ToString();
    }
}
