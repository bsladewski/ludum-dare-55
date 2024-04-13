using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();

        playerControls.Player._0.started += (_) => OnNumberInput(0);
        playerControls.Player._1.started += (_) => OnNumberInput(1);
        playerControls.Player._2.started += (_) => OnNumberInput(2);
        playerControls.Player._3.started += (_) => OnNumberInput(3);
        playerControls.Player._4.started += (_) => OnNumberInput(4);
        playerControls.Player._5.started += (_) => OnNumberInput(5);
        playerControls.Player._6.started += (_) => OnNumberInput(6);
        playerControls.Player._7.started += (_) => OnNumberInput(7);
        playerControls.Player._8.started += (_) => OnNumberInput(8);
        playerControls.Player._9.started += (_) => OnNumberInput(9);

        playerControls.Player.Enter.started += (_) => OnEnterPressed();
        playerControls.Player.Backspace.started += (_) => OnBackspacePressed();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnNumberInput(int number)
    {
        Debug.Log(number);
    }

    private void OnEnterPressed()
    {
        Debug.Log("Enter pressed.");
    }

    private void OnBackspacePressed()
    {
        Debug.Log("Backspace pressed.");
    }
}
