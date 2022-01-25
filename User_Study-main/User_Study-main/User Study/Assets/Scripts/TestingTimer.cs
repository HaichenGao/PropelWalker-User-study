using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingTimer : MonoBehaviour
{

    public CountdownTimer tTimer;

    // Start is called before the first frame update
    void Start()
    {
        tTimer = gameObject.GetComponent<CountdownTimer>();

    }

    // Update is called once per frame
    void Update()
    {

        tTimer.CountdownStart();

    }
}
