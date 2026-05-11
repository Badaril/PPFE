using UnityEngine;

public class TutoChecker : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    [SerializeField] private float index;

    public void UpdateTutoState()
    {
        if (index == gameManager.currentTutoRowIndex)
        {
            gameManager.UpdateTutoText();

        }
    }
}
