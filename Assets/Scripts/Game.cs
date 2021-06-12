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
        score = score + (pointsPerSave * multiplier);
        multiplier++;
        uIController.UpdateTexts();
        StopCoroutine(ResetMultiplier());
        StartCoroutine(ResetMultiplier());
    }

    IEnumerator ResetMultiplier()
    {
        yield return new WaitForSeconds(secondsUntilMultiplierReset);
        multiplier = startingMultiplier;
        uIController.UpdateTexts();
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
