using UnityEngine;

public class Slot : MonoBehaviour
{
    public int row;
    public int column;
    private GameManager gameManager;
    private bool isOccupied = false;
    
    public void Init(GameManager manager, int r, int c)
    {
        gameManager = manager;
        row = r;
        column = c;
    }

    private void OnMouseDown()
    {
        if (!isOccupied && gameManager.CanPlay())
        {
            isOccupied = true;
            gameManager.OccupySlot(row, column, this);
        }
    }

    public void SetSymbol(Sprite symbol)
    {
        GetComponent<SpriteRenderer>().sprite = symbol;
    }

    public void ClearSlot()
    {
        isOccupied = false;
        GetComponent<SpriteRenderer>().sprite = null;
    }
}
