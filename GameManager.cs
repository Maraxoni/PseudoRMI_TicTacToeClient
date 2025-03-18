using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameManager
{
    private char[,] board =
    {
        { ' ', ' ', ' ' },
        { ' ', ' ', ' ' },
        { ' ', ' ', ' ' }
    };

    private char currentPlayer = 'X';

    public string MakeMove(int row, int col, char player)
    {
        if (board[row, col] == ' ')
        {
            board[row, col] = player;
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            return CheckWin();
        }
        return "Invalid move!";
    }

    public string GetBoard()
    {
        string boardStr = "";
        for (int i = 0; i < 3; i++)
        {
            boardStr += $" {board[i, 0]} | {board[i, 1]} | {board[i, 2]} \n";
            if (i < 2)
                boardStr += "---+---+---\n";
        }
        return boardStr;
    }

    private string CheckWin()
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != ' ' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                return $"Player {board[i, 0]} wins!";
            if (board[0, i] != ' ' && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                return $"Player {board[0, i]} wins!";
        }
        if (board[0, 0] != ' ' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            return $"Player {board[0, 0]} wins!";
        if (board[0, 2] != ' ' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            return $"Player {board[0, 2]} wins!";
        return "Continue";
    }
}

