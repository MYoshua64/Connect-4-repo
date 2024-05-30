using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndWindow : MonoBehaviour
{
    [SerializeField] Text winText;
    Animator windowAnimator;

    // Start is called before the first frame update
    void Start()
    {
        windowAnimator = GetComponent<Animator>();
        GameHandler.Instance.OnGameWin += DisplayWinText;
    }

    void DisplayWinText(GameTurn endingTurn)
    {
        winText.text = (endingTurn == GameTurn.BLUE ? "BLUE " : "RED ") + "HAS WON!";
        windowAnimator.SetTrigger("Open");
        GameHandler.Instance.OnGameWin -= DisplayWinText;
    }
}
