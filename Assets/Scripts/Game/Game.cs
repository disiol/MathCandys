using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
   [SerializeField] private int gridSize = 8;
   [SerializeField] private GameObject candyPrefab;

    private Candy[,] _candies;

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        _candies = new Candy[gridSize, gridSize];

        // Generate candies on the grid
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                GameObject candyObject = Instantiate(candyPrefab, new Vector2(row, col), Quaternion.identity);
                Candy candy = candyObject.GetComponent<Candy>();
                _candies[row, col] = candy;
                candy.Initialize(row, col);
            }
        }
    }

    public void CheckMatches()
    {
        List<Candy> matchedCandies = new List<Candy>();

        // Check for horizontal matches
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize - 2; col++)
            {
                Candy candy = _candies[row, col];

                if (candy.IsMatched) continue;

                if (_candies[row, col + 1].Type == candy.Type && _candies[row, col + 2].Type == candy.Type)
                {
                    matchedCandies.Add(candy);
                    matchedCandies.Add(_candies[row, col + 1]);
                    matchedCandies.Add(_candies[row, col + 2]);
                }
            }
        }

        // Check for vertical matches
        for (int col = 0; col < gridSize; col++)
        {
            for (int row = 0; row < gridSize - 2; row++)
            {
                Candy candy = _candies[row, col];

                if (candy.IsMatched) continue;

                if (_candies[row + 1, col].Type == candy.Type && _candies[row + 2, col].Type == candy.Type)
                {
                    matchedCandies.Add(candy);
                    matchedCandies.Add(_candies[row + 1, col]);
                    matchedCandies.Add(_candies[row + 2, col]);
                }
            }
        }

        // Remove matched candies and calculate scores
        foreach (Candy candy in matchedCandies)
        {
            candy.RemoveFromGrid();
            // Add score based on candy type and other conditions
        }

        // Shift candies down and fill empty spaces
        for (int col = 0; col < gridSize; col++)
        {
            int emptySpaces = 0;

            for (int row = 0; row < gridSize; row++)
            {
                if (_candies[row, col] == null)
                {
                    emptySpaces++;
                }
                else if (emptySpaces > 0)
                {
                    _candies[row, col].MoveTo(row - emptySpaces, col);
                    _candies[row - emptySpaces, col] = _candies[row, col];
                    _candies[row, col] = null;
                }
            }

            for (int i = 0; i < emptySpaces; i++)
            {
                // Generate new candies for the empty spaces
                GameObject candyObject =
                    Instantiate(candyPrefab, new Vector2(gridSize - 1 - i, col), Quaternion.identity);
                Candy candy = candyObject.GetComponent<Candy>();
                _candies[gridSize - 1 - i, col] = candy;
                candy.Initialize(gridSize - i, col);
            }
        }
    }
}