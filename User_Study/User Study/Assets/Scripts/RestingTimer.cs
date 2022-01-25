using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestingTimer : MonoBehaviour
{

    public CountdownTimer rTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        rTimer = gameObject.GetComponent<CountdownTimer>();
                
    }

    // Update is called once per frame
    void Update()
    {
        
        rTimer.CountdownStart();
        //Debug.Log(rTimer.CurrentTime.ToString("0"));
        
    }
}
