using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Space : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI buttonText;

    private GameController gameController;

    public void SetControllerReference(GameController control)
    {
        gameController = control;
    }

    public void SetSpace()
    {
        button.interactable = false;
        buttonText.text = gameController.Side;
        gameController.EndTurn();
    }
}
