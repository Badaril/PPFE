using UnityEngine;

public class TutoChecker : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    [SerializeField] private float index;
    [SerializeField] private float indexByCondition = -1;


    public void UpdateTutoState()
    {
        if (index == gameManager.currentTutoRowIndex || indexByCondition == gameManager.currentTutoRowIndex)
        {
            gameManager.UpdateTutoText();

        }
    }
}
