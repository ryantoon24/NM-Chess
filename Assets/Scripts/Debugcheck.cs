using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugcheck : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite check, x;
    // public GameObject checker;

    private void Start()
    {
        // Instantiate(checker, new Vector3(), Quaternion.identity);
        this.GetComponent<SpriteRenderer>().sprite = x;

        this.gameObject.transform.position = new Vector3(-5, 0, 0);
    }
    public void FocusDown()
    {
        this.GetComponent<SpriteRenderer>().sprite = check;
    }
    public void FocusOff()
    {
        this.GetComponent<SpriteRenderer>().sprite = x;
    }
}
