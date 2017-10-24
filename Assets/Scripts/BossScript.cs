using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        GameObject.Find("Victory").GetComponent<RawImage>().enabled = false;
    }

    public void death()
    {
        GameObject.Find("Victory").GetComponent<RawImage>().enabled = true;
        GameObject.Find("Player").GetComponent<PlayerHealthManager>().gameWon = true;
        GameObject.Find("Player").GetComponent<PlayerHealthManager>().win();
    }
}