using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextmindlogo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = new Vector3(-7, 3, 0);
    }

    public void BCIDisconnected()
    {
        Destroy(this.gameObject);
    }
}
