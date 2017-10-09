using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour {

    public void incrementScore() {
        GameObject.FindGameObjectWithTag("score").GetComponent<Text>().text = (int.Parse(GameObject.FindGameObjectWithTag("score").GetComponent<Text>().text) + 1).ToString();
    }
}
