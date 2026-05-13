using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int gameState = 0;
    public int winState = 2;
    public string titleScreen;

    public TextMeshProUGUI objectiveText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGameState(int newState)
    {
        gameState = newState;
        UpdateObjective();

        if (gameState == winState)
        {
            SceneManager.LoadScene(titleScreen);
        }
    }
    public void ChangeGameState()
    {
        ChangeGameState(gameState + 1);
    }

    private void UpdateObjective()
    {
        objectiveText.text = GetObjectiveText();
    }

    private string GetObjectiveText()
    {
        switch (gameState)
        {
            case 0:
                return "Retrieve the <color=#00BFFF>Idol of Horse</color>.";
            case 1:
                return "<color=#00BFFF>Escape</color> back through the vent.";
            default: 
                return "Uh oh.";
        }
    }
}
