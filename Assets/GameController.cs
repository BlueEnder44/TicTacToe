using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI[] spaceList;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    private string side = "X";
    private int moves = 0;

    public string Side { get => side; private set => side = value; }

    // Start is called before the first frame update
    private void Start()
    {
        SetGameControllerReferenceForButtons();
        Restart();
    }

    public void Restart()
    {
        side = "X";
        moves = 0;
        gameOverPanel.SetActive(false);
        SetInteractable(true);
        for (int i = 0; i < spaceList.Length; i++)
            spaceList[i].text = "";
    }

    private void SetGameControllerReferenceForButtons()
    {
        for (int i = 0; i < spaceList.Length; i++)
            spaceList[i].GetComponentInParent<Space>().SetControllerReference(this);
    }

    private void ChangeSide()
    {
        side = (side == "X") ? "O" : "X";
    }

    private static readonly List<int[]> winningCombinations = new()
    {
        new int[] { 0, 1, 2 },
        new int[] { 3, 4, 5 },
        new int[] { 6, 7, 8 },
        new int[] { 0, 3, 6 },
        new int[] { 1, 4, 7 },
        new int[] { 2, 5, 8 },
        new int[] { 0, 4, 8 },
        new int[] { 2, 4, 6 }
    };

    private bool IsWin()
    {
        foreach (var winningCombination in winningCombinations)
        {
            if (IsWin(winningCombination))
                return true;
        }

        return false;
    }

    private bool IsWin(int[] winningCombination)
    {
        foreach (var space in winningCombination)
        {
            if (spaceList[space].text != side)
                return false;
        }

        return true;
    }

    public void EndTurn()
    {
        moves++;
        if (IsWin())
            GameOver();
        
        else if (moves >= 9)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "Tie!";
        }
        
        else
            ChangeSide();
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = side + " wins!";
        for (int i = 0; i < spaceList.Length; i++)
            SetInteractable(false);
    }

    private void SetInteractable(bool setting)
    {
        for (int i = 0; i < spaceList.Length; i++)
            spaceList[i].GetComponentInParent<Button>().interactable = setting;
    }
}
