using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private SnakePartController snakePartPrefab;
    [SerializeField] private int snakePartsStartingNumber = 2;
    private List<SnakePartController> snakeParts=new List<SnakePartController>();
    private float partSize;

    private void Awake()
    {
        partSize = snakePartPrefab.GetComponent<SpriteRenderer>().bounds.size.x+0.1f;
    }

    private void Start()
    {
        CheckSnakePartsSatringNumber();
        StartSnake();
    }

    void CheckSnakePartsSatringNumber()
    {
        snakePartsStartingNumber = (snakePartsStartingNumber < 2) ? 2 : snakePartsStartingNumber;
    }

    void AddNewSnakePart(int _index, Vector3 _position)
    {
        SnakePartController newSnakePart = Instantiate(snakePartPrefab, this.transform, false);
        newSnakePart.transform.position = _position;
        snakeParts.Insert(_index,newSnakePart);
    }

    void StartSnake()
    {
        for (int i = 0; i < snakePartsStartingNumber; i++)
        {
            Vector3 newPosition = new Vector3(-i * partSize, 0f, 0f);
            AddNewSnakePart(i, newPosition);
        }
    }
}
