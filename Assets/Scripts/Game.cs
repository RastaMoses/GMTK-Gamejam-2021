using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    //Serialize Params
    [SerializeField] int pointsPerSave = 10;
    [SerializeField] float secondsUntilMultiplierReset = 10f;

    //State
    public int score;
    public int multiplier;
    int startingMultiplier = 1;
    Coroutine multiplierCoroutine;
    bool coroutineRunning = false;

    //Cached Comps
    UIController uIController;
    // Start is called before the first frame update
    void Start()
    {
        uIController = GetComponent<UIController>();
        score = 0;
        multiplier = 1;
        uIController.UpdateTexts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreIncrease()
    {
        if (coroutineRunning)
        {
            StopCoroutine(multiplierCoroutine);
        }
        score = score + (pointsPerSave * multiplier);
        multiplier++;
        uIController.UpdateTexts();
        multiplierCoroutine = StartCoroutine(ResetMultiplier());
    }

    IEnumerator ResetMultiplier()
    {
        coroutineRunning = true;
        yield return new WaitForSeconds(secondsUntilMultiplierReset);
        multiplier = startingMultiplier;
        uIController.UpdateTexts();
        coroutineRunning = false;
    }

    public int GetScore()
    {
        return score;
    }
    public int GetMultiplier()
    {
        return multiplier;
    }
}
