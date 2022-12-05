using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CupAnimate : MonoBehaviour
{
    private void OnEnable()
    {
        Game.OnWin += EndGame;
        Restart.OnResetStats += NewGame;
    }

    private void OnDisable()
    {
        Game.OnWin -= EndGame;
        Restart.OnResetStats -= NewGame;
    }

    private void NewGame()
    {
        GetComponent<Animator>().SetTrigger("CloseIt");
    }

    private void EndGame()
    {
        GetComponent<Animator>().ResetTrigger("CloseIt");
        GetComponent<Animator>().SetTrigger("OpenIt");
    }
}
