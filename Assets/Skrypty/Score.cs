using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    public Task1_Bramka lodz;
    public UnityEngine.UI.Text scoreText;
    		
	// Update is called once per frame
	void Update () {
        int taskScore = lodz.GetComponent<Task1_Bramka>().Score;
        scoreText.text = taskScore.ToString();
	}
}
