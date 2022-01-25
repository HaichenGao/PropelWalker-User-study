using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ExperimentControl : MonoBehaviour
{
    Database10 forceAandB;

    Logger logger;

    public Staircase staircase;

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
    bool stepTwo = false;
    bool saved = false;
    bool textShown = false;

    string answer;
    bool result;


    public double[] testingGroup = new double[2];

    [SerializeField]
    int stopEvery;

    // Start is called before the first frame update
    void Start()
    {
        staircase = gameObject.GetComponent<Staircase>();

        forceAandB = gameObject.GetComponent<Database10>();

        logger = GameObject.Find("Logger").GetComponent<Logger>();

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

        if (restingTimer.rTimer.TimerStart == true)
        {
            restingTime.text = "idle time: " + restingTimer.rTimer.CurrentTime.ToString("0");
            
            if (restingTimer.rTimer.CurrentTime < 1.5 && sendingMsgA == false)
            {
                if (i > 0)
                {
                    staircase.NextVal(result);                   
                    staircase.GetReversals();
                    if (staircase.Reversals.Count >= 3 && stepTwo == false)
                    {
                        staircase.StepSize = staircase.StepSize * staircase.Factor;
                        staircase.Val.RemoveAt(staircase.Val.Count - 1);
                        staircase.NextVal(result);
                        stepTwo = true;
                    }
                    
                }
                testingGroup = new double[2];
                
                testingGroup[0] = staircase.GetLast();
                testingGroup[1] = staircase.FundVal;
                testingGroup[0] = Math.Round(testingGroup[0], 2);
                testingGroup[1] = Math.Round(testingGroup[1], 2);
                forceAandB.ShuffleArray(testingGroup);
              
                serialController.SendSerialMessage(testingGroup[0].ToString("0.00"));

                // if (testingGroup[0] >= 0)
                // {
                //    Invoke("force1", 0.5f);
                // }
                // else
                // {
                //    Invoke("force2", 0.5f); 
                // }

                sendingMsgA = true;
                //Debug.Log("PWM A");
            }
        }

        if (waitingTimer.wTimer.TimerStart == true)
        {
            restingTime.text = "idle time: " + waitingTimer.wTimer.CurrentTime.ToString("0");
            if (waitingTimer.wTimer.CurrentTime < 1.5f && sendingMsgB == false)
            {
                serialController.SendSerialMessage(testingGroup[1].ToString("0.00"));

                // if (testingGroup[1] >= 0)
                // {
                //    Invoke("force1", 0.5f);
                // }
                // else
                // {
                //    Invoke("force2", 0.5f); 
                // }

                sendingMsgB = true;
                //Debug.Log("PWM B");
                
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
            GameObject.Find("EqualOptionButton").GetComponent<Button>().interactable = false;
            GameObject.Find("UnequalOptionButton").GetComponent<Button>().interactable = false;
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

        if (testingTimer.tTimer.TimerStart == false && teststop == true && textShown == false)
        {
            restingTime.fontSize = 30;
            GameObject.Find("EqualOptionButton").GetComponent<Button>().interactable = true;
            GameObject.Find("UnequalOptionButton").GetComponent<Button>().interactable = true;
            restingTime.text = "Please choose your answer";
            textShown = true;
        }


    }

    public int J
    {
        get { return j; }
        set { j = value; }
    }



    public void RestingTimerStart()
    {
        //if (i == -1)
        //{
        //    GameObject.Find("EqualOptionButton").GetComponent<Button>().interactable = true;
        //    GameObject.Find("UnequalOptionButton").GetComponent<Button>().interactable = true;
        //    GameObject.Find("StartButton").GetComponent<Button>().interactable = false;
        //}

        if (teststop == false && !staircase.ReversalLimitReached())
        {
            countdownRestingTimer.ResetTimer();
            i += 1;
            j += 1;
            //if(staircase.Reversals.Count >= 3 && stepTwo == false)
            //{
            //    staircase.StepSize = staircase.StepSize * staircase.Factor;
            //    stepTwo = true;
            //}

            restingTimer.rTimer.TimerStart = true;
            isForceAStart = true;
            trial.text = "Trial: " + j;
            //Debug.Log("i for forceAandB array: " + i);
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
        textShown = false;

        if (!staircase.ReversalLimitReached())
        {
            restingTime.fontSize = 70;
        }

        if (staircase.ReversalLimitReached())
        {
            restingTime.text = "Thanks for your participation:)";
            logger.LogVal(staircase.Val);
            logger.LogReversals(staircase.Reversals);
            logger.LogFinalVal(staircase.GetFinalVal());
            if (saved == false)
            {
                logger.CreateCSV();
                saved = true;
            }
        }


    }

    public void ButtonA()
    {
        answer = "A";
        Debug.Log("A");

        if (testingGroup[0] == testingGroup[1])
        {
            
            result = false;
        }
        else if ((staircase.Direction == -1 && testingGroup[0] < testingGroup[1]) || (staircase.Direction == 1 && testingGroup[0] > testingGroup[1]))
        {
            
            result = true;
        }
        else
        {
            result = false;
        }

        
        
        Debug.Log(result);
        logger.Log(j.ToString(), testingGroup[0].ToString("0.00"), testingGroup[1].ToString("0.00"), answer, result.ToString());


    }

    public void ButtonB()
    {
        answer = "B";
        Debug.Log("B");

        if (testingGroup[0] == testingGroup[1])
        {
            
            result = false;
        }
        else if ((staircase.Direction == -1 && testingGroup[1] < testingGroup[0]) || (staircase.Direction == 1 && testingGroup[1] > testingGroup[0]))
        {
            
            result = true;
        }
        else
        {
            result = false;
        }

        

        Debug.Log(result);
        logger.Log(j.ToString(), testingGroup[0].ToString("0.00"), testingGroup[1].ToString("0.00"), answer, result.ToString());
    }

    public void TakeABreak()
    {
        //Debug.Log("0000000000");
        if(j%stopEvery == 0)
        {
            //Debug.Log("1111111111");
            restingTime.text = "Please take a break! CLick START button when your are ready";
            
            GameObject.Find("EqualOptionButton").GetComponent<Button>().interactable = false;
            GameObject.Find("UnequalOptionButton").GetComponent<Button>().interactable = false;
            GameObject.Find("StartButton").GetComponent<Button>().interactable = true;
            
        }
        else
        {
            ResetTest();
            RestingTimerStart();
        }
    }

    public void TestStart()
    {
        
        GameObject.Find("EqualOptionButton").GetComponent<Button>().interactable = true;
        GameObject.Find("UnequalOptionButton").GetComponent<Button>().interactable = true;
        GameObject.Find("StartButton").GetComponent<Button>().interactable = false;

        ResetTest();
        
    }
}
