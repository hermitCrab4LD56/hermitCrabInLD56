using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFalseAfterTime : MonoBehaviour
{
    float timer;
    void Start()
    {
        timer = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                this.gameObject.SetActive(false);
                timer = 3f;
            }
            
        }
        
    }
}
