using UnityEngine;

public enum Player { X, O }

public class GameManager : MonoBehaviour
{
    public Sprite xSprite, oSprite;
    private Player currentPlayer = Player.X;
    private string[,] board = new string[3, 3];
    private bool gameOver = false;

    private void Start()
    {
        Slot[] slots = FindObjectsByType<Slot>(FindObjectsSortMode.InstanceID);

        foreach (Slot slot in slots)
        {
            string[] nameParts = slot.gameObject.name.Split('_');

            if (nameParts.Length == 3 && nameParts[0] == "Slot")
            {
                int row = int.Parse(nameParts[1]);
                int col = int.Parse(nameParts[2]);

                slot.Init(this, row, col);
                board[row, col] = "";
            }
            else
            {
                Debug.LogWarning("Slot nomeado incorretamente! Use o formato: Slot_row_col");
            }
        }
    }

    public void OccupySlot(int row, int col, Slot slot)
    {
        string symbol = currentPlayer == Player.X ? "X" : "O";
        board[row, col] = symbol;
        slot.SetSymbol(currentPlayer == Player.X ? xSprite : oSprite);

        if (CheckVictory(row, col, symbol))
        {
            gameOver = true;
            Debug.Log($"{symbol} venceu!");
        }
        else if (CheckDraw())
        {
            gameOver = true;
            Debug.Log("Empate!");
        }
        else
        {
            SwitchTurn();
        }
    }

    public bool CanPlay() => !gameOver;

    private void SwitchTurn()
    {
        currentPlayer = currentPlayer == Player.X ? Player.O : Player.X;
    }

    private bool CheckDraw()
    {
        foreach (var cell in board)
            if (string.IsNullOrEmpty(cell))
                return false;
        return true;
    }

    private bool CheckVictory(int row, int col, string symbol)
    {
        // Verificar Linha
        if (board[row, 0] == symbol && board[row, 1] == symbol && board[row, 2] == symbol)
            return true;

        // Verificar Coluna
        if (board[0, col] == symbol && board[1, col] == symbol && board[2, col] == symbol)
            return true;

        // Verificar Diagonal Principal
        if (row == col && board[0,0] == symbol && board[1,1] == symbol && board[2,2] == symbol)
            return true;

        // Verificar Diagonal Secundária
        if (row + col == 2 && board[0,2] == symbol && board[1,1] == symbol && board[2,0] == symbol)
            return true;

        return false;
    }
}
