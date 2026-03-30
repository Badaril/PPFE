using UnityEngine;

public class TutoChecker : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public void UpdateTutoState()
    {
            gameManager.CheckTutoState();

    }
}
