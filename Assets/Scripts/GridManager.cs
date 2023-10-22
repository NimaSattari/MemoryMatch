using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] int howManyInARow = 5;

    private void GenerateGrid(Transform father, Card prefab, int howMany, float tileSize)
    {
        int rows = howMany / howManyInARow;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < howManyInARow; col++)
            {
                Card tile = Instantiate(prefab, father);
                float posX = col * tileSize;
                float posY = row * tileSize;
                tile.transform.position = new Vector2(posX, posY);
            }
        }

        float gridW = howManyInARow * tileSize;
        float gridH = rows * tileSize;
        father.position = new Vector2(gridW - gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
    }
}
