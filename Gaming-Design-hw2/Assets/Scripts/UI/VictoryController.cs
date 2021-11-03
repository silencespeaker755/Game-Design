using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using TMPro;

public class VictoryController : MonoBehaviour
{
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text time;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time.SetText(string.Format("Total Cost Time:     {0}",FindObjectOfType<GameController>()._passTime.text));
        score.SetText("Final Score:                 {0}", FindObjectOfType<PlayerController>().point);
    }
}
