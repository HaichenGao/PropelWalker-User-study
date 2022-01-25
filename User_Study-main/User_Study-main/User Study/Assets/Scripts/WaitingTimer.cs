using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingTimer : MonoBehaviour
{
    public CountdownTimer wTimer;

    // Start is called before the first frame update
    void Start()
    {
        wTimer = gameObject.GetComponent<CountdownTimer>();

    }

    // Update is called once per frame
    void Update()
    {

        wTimer.CountdownStart();

    }
}
