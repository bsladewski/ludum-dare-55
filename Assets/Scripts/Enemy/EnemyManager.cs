using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private void Awake()
    {
        Player.OnSolutionEntered += Player_OnSolutionEntered;
    }

    private void Player_OnSolutionEntered(int solution)
    {
        Debug.Log(string.Format("Solution entered: {0}", solution));
    }
}
