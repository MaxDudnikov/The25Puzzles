using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CupAnimate : MonoBehaviour
{
    private void OnEnable()
    {
        BloksController.e_Win += EndGame;
        Restart.e_ResetStats += NewGame;
    }

    private void OnDisable()
    {
        BloksController.e_Win -= EndGame;
        Restart.e_ResetStats -= NewGame;
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
