using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    //Serialize Params
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI multiplierText;

    //Cached Comp
    Game game;
    // Start is called before the first frame update
    void Start()
    {
        game = GetComponent<Game>();
    }


    public void UpdateTexts()
    {
        multiplierText.text = game.GetMultiplier().ToString();
        scoreText.text = game.GetScore().ToString();
    }


}
