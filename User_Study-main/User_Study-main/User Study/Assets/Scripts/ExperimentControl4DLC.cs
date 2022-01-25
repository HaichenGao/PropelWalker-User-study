using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ExperimentControl4DLC : MonoBehaviour
{
    Database database;

    Logger4DLC logger;

    //public Staircase staircase;

    RestingTimer restingTimer;
    CountdownTimer countdownRestingTimer;

    TestingTimer testingTimer;
    CountdownTimer countdownTestingTimer;

    WaitingTimer waitingTimer;
    CountdownTimer countdownWaitingTimer;

    Timer timer;

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


    //public double[] testingGroup = new double[2];

    [SerializeField]
    int stopEvery;

    // Start is called before the first frame update
    void Start()
    {
        //staircase = gameObject.GetComponent<Staircase>();

        database = gameObject.GetComponent<Database>();

        logger = GameObject.Find("Logger4DLC").GetComponent<Logger4DLC>();

        restingTimer = GameObject.Find("RestingTimer").GetComponent<RestingTimer>();
        countdownRestingTimer = GameObject.Find("RestingTimer").GetComponent<CountdownTimer>();
        restingTime = GameObject.Find("CountdownText").GetComponent<TextMeshProUGUI>();

        testingTimer = GameObject.Find("TestingTimer").GetComponent<TestingTimer>();
        countdownTestingTimer = GameObject.Find("TestingTimer").GetComponent<CountdownTimer>();

        //waitingTimer = GameObject.Find("WaitingTimer").GetComponent<WaitingTimer>();
        //countdownWaitingTimer = GameObject.Find("WaitingTimer").GetComponent<CountdownTimer>();

        timer = GameObject.Find("Timer").GetComponent<Timer>();

        trial = GameObject.Find("Trial").GetComponent<TextMeshProUGUI>();

        GameObject.Find("Water").GetComponent<Button>().interactable = false;
        GameObject.Find("Air").GetComponent<Button>().interactable = false;
        GameObject.Find("Sand").GetComponent<Button>().interactable = false;
        GameObject.Find("Mud").GetComponent<Button>().interactable = false;

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
                

                serialController.SendSerialMessage(database.ForceA[i].ToString("0.00"));

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

        if (restingTimer.rTimer.TimerStart == false && isForceAStart == true)
        {
            //GameObject.Find("EqualOptionButton").GetComponent<Button>().interactable = false;
            //GameObject.Find("UnequalOptionButton").GetComponent<Button>().interactable = false;
            this.TestingTimerStart();
            //void
            restingTime.text = "Stepping";
            Debug.Log("A:" + database.ForceA[i]);
            isForceAStart = false;
            isForceAFinish = true;
            sendingStopMsg = false;
            teststop = true;
        }

        if (testingTimer.tTimer.TimerStart == false && sendingStopMsg == false)
        {
            serialController.SendSerialMessage("99");
            sendingStopMsg = true;
            Debug.Log("STOP");
        }



        //if (testingTimer.tTimer.TimerStart == false && isForceAFinish == true && isWaitingStart == true)
        //{
        //    this.WaitingTimerStart();
        //    isWaitingStart = false;
        //}

        //if (waitingTimer.wTimer.TimerStart == false && isForceBStart == true)
        //{
        //    this.TestingTimerStart();
        //    isForceBStart = false;
        //    restingTime.text = "B";
        //    Debug.Log("B: " + testingGroup[1]);
        //    sendingStopMsg = false;
        //    teststop = true;
        //}

        if (testingTimer.tTimer.TimerStart == false && teststop == true && textShown == false)
        {
            restingTime.fontSize = 30;
            GameObject.Find("Water").GetComponent<Button>().interactable = true;
            GameObject.Find("Air").GetComponent<Button>().interactable = true;
            GameObject.Find("Sand").GetComponent<Button>().interactable = true;
            GameObject.Find("Mud").GetComponent<Button>().interactable = true;
            restingTime.text = "Please choose your answer";
            textShown = true;
            timer.TimerStart = true;
        }


    }

    public int J
    {
        get { return j; }
        set { j = value; }
    }



    public void RestingTimerStart()
    {
        GameObject.Find("Water").GetComponent<Button>().interactable = false;
        GameObject.Find("Air").GetComponent<Button>().interactable = false;
        GameObject.Find("Sand").GetComponent<Button>().interactable = false;
        GameObject.Find("Mud").GetComponent<Button>().interactable = false;


        if (teststop == false && i < database.ForceA.Length - 1)
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

    //public void WaitingTimerStart()
    //{
    //    if (teststop == false)
    //    {
    //        countdownWaitingTimer.ResetTimer();
    //        waitingTimer.wTimer.TimerStart = true;
    //        isForceBStart = true;
    //        Debug.Log("waiting timer start");
    //    }

    //}

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

        if (i < database.ForceA.Length - 1)
        {
            restingTime.fontSize = 70;
        }

        if (i == database.ForceA.Length - 1)
        {
            restingTime.text = "Thanks for your participation:)";
            //logger.LogVal(staircase.Val);
            //logger.LogReversals(staircase.Reversals);
            //logger.LogFinalVal(staircase.GetFinalVal());
            if (saved == false)
            {
                logger.CreateCSV();
                saved = true;
            }
        }


    }

    public void ButtonA()
    {
        answer = "water";
        Debug.Log("water");

        if (database.ForceA[i] == database.Water)
        {

            result = true;
        }
        else
        {
            result = false;
        }

        timer.TimerStart = false;
        Debug.Log(timer.CurrentTime);

        Debug.Log(result);
        logger.Log(j.ToString(), database.ForceA[i].ToString("0.00"), answer, result.ToString(), timer.CurrentTime.ToString("0.00"));
        timer.ResetTimer();
    }

    public void ButtonB()
    {
        answer = "air";
        Debug.Log("air");

        if (database.ForceA[i] == database.Air)
        {

            result = true;
        }
        else
        {
            result = false;
        }

        timer.TimerStart = false;
        Debug.Log(timer.CurrentTime);

        Debug.Log(result);
        logger.Log(j.ToString(), database.ForceA[i].ToString("0.00"), answer, result.ToString(), timer.CurrentTime.ToString("0.00"));
        timer.ResetTimer();
    }


    public void ButtonC()
    {
        answer = "sand";
        Debug.Log("sand");

        if (database.ForceA[i] == database.Sand)
        {

            result = true;
        }
        else
        {
            result = false;
        }

        timer.TimerStart = false;
        Debug.Log(timer.CurrentTime);

        Debug.Log(result);
        logger.Log(j.ToString(), database.ForceA[i].ToString("0.00"), answer, result.ToString(), timer.CurrentTime.ToString("0.00"));
        timer.ResetTimer();

    }

    public void ButtonD()
    {
        answer = "mud";
        Debug.Log("mud");

        if (database.ForceA[i] == database.Mud)
        {

            result = true;
        }
        else
        {
            result = false;
        }

        timer.TimerStart = false;
        Debug.Log(timer.CurrentTime);

        Debug.Log(result);
        logger.Log(j.ToString(), database.ForceA[i].ToString("0.00"), answer, result.ToString(), timer.CurrentTime.ToString("0.00"));
        timer.ResetTimer();

    }

    public void TakeABreak()
    {
        //Debug.Log("0000000000");
        if (j % stopEvery == 0)
        {
            //Debug.Log("1111111111");
            restingTime.text = "Please take a break! CLick START button when your are ready";

            GameObject.Find("Water").GetComponent<Button>().interactable = false;
            GameObject.Find("Air").GetComponent<Button>().interactable = false;
            GameObject.Find("Sand").GetComponent<Button>().interactable = false;
            GameObject.Find("Mud").GetComponent<Button>().interactable = false;
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

        GameObject.Find("Water").GetComponent<Button>().interactable = false;
        GameObject.Find("Air").GetComponent<Button>().interactable = false;
        GameObject.Find("Sand").GetComponent<Button>().interactable = false;
        GameObject.Find("Mud").GetComponent<Button>().interactable = false;
        GameObject.Find("StartButton").GetComponent<Button>().interactable = false;

        ResetTest();

    }
}
