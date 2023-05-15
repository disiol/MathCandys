using UnityEngine;

public class Candy : MonoBehaviour
{
    public CandyType Type { get; private set; }
    public bool IsMatched { get; private set; }


    private int row;
    private int col;

    public void Initialize(int row, int col)
    {
        this.row = row;
        this.col = col;

        // Set the candy type randomly
        Type = (CandyType)Random.Range(0, (int)CandyType.Count);
    }

    public void RemoveFromGrid()
    {
        IsMatched = true;
        Destroy(gameObject);
    }

    public void MoveTo(int newRow, int newCol)
    {
        row = newRow;
        col = newCol;
        transform.position = new Vector2(row, col);
    }

}

public enum CandyType
{
    Orange,
    Green,
    Blue,
    Yellow,
    Purple,
// Add more candy types as needed
    Count // Keep this as the last element for counting purposes
}