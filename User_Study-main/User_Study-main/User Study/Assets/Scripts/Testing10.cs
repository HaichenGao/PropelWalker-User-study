using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Testing10 : MonoBehaviour
{
    Database10 forceAandB;

    RestingTimer restingTimer;
    CountdownTimer countdownRestingTimer;
        
    TestingTimer testingTimer;
    CountdownTimer countdownTestingTimer;

    WaitingTimer waitingTimer;
    CountdownTimer countdownWaitingTimer;

    TextMeshProUGUI restingTime;
    TextMeshProUGUI trial;

    SerialController serialController;

    public int i = -1;
    public int j = 0;

    bool isForceAStart = false;
    bool isForceAFinish = false;
    bool isForceBStart = false;
    bool isWaitingStart = false;
    bool teststop = false;
    bool sendingMsgA = false;
    bool sendingMsgB = false;
    bool sendingStopMsg = false;


    public double[] testingGroup = new double[2];

    // Start is called before the first frame update
    void Start()
    {
        

        forceAandB = gameObject.GetComponent<Database10>();
        
        restingTimer = GameObject.Find("RestingTimer").GetComponent<RestingTimer>();
        countdownRestingTimer = GameObject.Find("RestingTimer").GetComponent<CountdownTimer>();
        restingTime = GameObject.Find("CountdownText").GetComponent<TextMeshProUGUI>();
        
        testingTimer = GameObject.Find("TestingTimer").GetComponent<TestingTimer>();
        countdownTestingTimer = GameObject.Find("TestingTimer").GetComponent<CountdownTimer>();
        
        waitingTimer = GameObject.Find("WaitingTimer").GetComponent<WaitingTimer>();
        countdownWaitingTimer = GameObject.Find("WaitingTimer").GetComponent<CountdownTimer>();

        trial = GameObject.Find("Trial").GetComponent<TextMeshProUGUI>();

        GameObject.Find("EqualOptionButton").GetComponent<Button>().interactable = false;
        GameObject.Find("UnequalOptionButton").GetComponent<Button>().interactable = false;

        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
                
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(waitingTimer.wTimer.TimerStart);
        //Debug.Log(isForceAStart);

        if (restingTimer.rTimer.TimerStart == true)
        {
            restingTime.text = restingTimer.rTimer.CurrentTime.ToString("0");
            if (restingTimer.rTimer.CurrentTime < 2 && sendingMsgA == false)
            {
                serialController.SendSerialMessage(testingGroup[0].ToString("0.0"));
                sendingMsgA = true;
                Debug.Log("PWM A");
            }
        }

        if (waitingTimer.wTimer.TimerStart == true)
        {
            restingTime.text = waitingTimer.wTimer.CurrentTime.ToString("0");
            if (waitingTimer.wTimer.CurrentTime < 2 && sendingMsgB == false)
            {
                serialController.SendSerialMessage(testingGroup[1].ToString("0.0"));
                sendingMsgB = true;
                Debug.Log("PWM B");
            }
        }

        if (testingTimer.tTimer.TimerStart == false && sendingStopMsg == false)
        {
            serialController.SendSerialMessage("99");
            sendingStopMsg = true;
            Debug.Log("STOP");
        }

        if (restingTimer.rTimer.TimerStart == false && isForceAStart == true)
        {
            this.TestingTimerStart();
            //void
            restingTime.text = "A";
            Debug.Log("A:" + testingGroup[0]);
            isForceAStart = false;
            isForceAFinish = true;
            sendingStopMsg = false;
        }

        if (testingTimer.tTimer.TimerStart == false && isForceAFinish == true && isWaitingStart == true)
        {
            this.WaitingTimerStart();
            isWaitingStart = false;
        }

        if (waitingTimer.wTimer.TimerStart == false && isForceBStart == true)
        {
            this.TestingTimerStart();
            isForceBStart = false;
            restingTime.text = "B";
            Debug.Log("B: " + testingGroup[1]);
            sendingStopMsg = false;
            teststop = true;
        }

        if(testingTimer.tTimer.TimerStart == false && teststop == true)
        {
            restingTime.fontSize = 30;
            restingTime.text = "Please choose your answer";
        }
        

    }

    public int J
    {
        get { return j; }
        set { j = value; }
    }

    public void RestingTimerStart()
    {
        if(i == -1)
        {
            forceAandB.Combine();
            forceAandB.Shuffle();
            Debug.Log("shuffle");
            GameObject.Find("EqualOptionButton").GetComponent<Button>().interactable = true;
            GameObject.Find("UnequalOptionButton").GetComponent<Button>().interactable = true;
            GameObject.Find("StartButton").GetComponent<Button>().interactable = false;
        }

        if(teststop == false && i < 29)
        {
            countdownRestingTimer.ResetTimer();
            i += 1;
            j += 1;
            //
            testingGroup[0] = forceAandB.ForceGroup[i][0];
            testingGroup[1] = forceAandB.ForceGroup[i][1];
            forceAandB.ShuffleArray(testingGroup);
            //
            restingTimer.rTimer.TimerStart = true;
            isForceAStart = true;
            //trial.text = "Trial: " + (i + 1);
            trial.text = "Trial: " + j;
            Debug.Log("i for forceAandB array: " + i);
        }
        

    }

    public void TestingTimerStart()
    {
        if (teststop == false)
        {
            countdownTestingTimer.ResetTimer();
            testingTimer.tTimer.TimerStart = true;
            isWaitingStart = true;
            Debug.Log("testing timer start");
        }
            
    }

    public void WaitingTimerStart()
    {
        if (teststop == false)
        {
            countdownWaitingTimer.ResetTimer();
            waitingTimer.wTimer.TimerStart = true;
            isForceBStart = true;
            Debug.Log("waiting timer start");
        }
            
    }

    public void ResetTest()
    {
        isForceAStart = false;
        isForceAFinish = false;
        isForceBStart = false;
        isWaitingStart = false;
        teststop = false;
        sendingMsgA = false;
        sendingMsgB = false;

        if(i < 29)
        {
            restingTime.fontSize = 100;
        }

        if (i >= 29)
        {
            restingTime.text = "Thanks for your participation:)";
        }


    }

    public void sendtest()
    {
        serialController.SendSerialMessage("1000");
    }
}
