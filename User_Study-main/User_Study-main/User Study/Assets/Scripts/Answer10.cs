using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer10 : MonoBehaviour
{
    public Button button1;
    public Button button2;

    public string answer;
    public bool result;

    bool saved = false;

    Logger logger;
    ExperimentControl experiment;

    double forceA;
    double forceB;

    void OnEnable()
    {
        //Register Button Events
        button1.onClick.AddListener(() => buttonCallBack(button1));
        button2.onClick.AddListener(() => buttonCallBack(button2));

        logger = GameObject.Find("Logger").GetComponent<Logger>();
        experiment = GameObject.Find("Main Camera").GetComponent<ExperimentControl>();
        

    }

    private void buttonCallBack(Button buttonPressed)
    {
        forceA = experiment.testingGroup[0];
        forceB = experiment.testingGroup[1];

        if (experiment.staircase.Direction == -1)
        {
            if((buttonPressed == button1 && forceA < forceB) || (buttonPressed == button2 && forceB < forceA))
            {
                Debug.Log("true");
                result = true;
            }
            else
            {
                Debug.Log("false");
                result = false;
            }
        }
        else
        {
            if((buttonPressed == button1 && forceA > forceB) || (buttonPressed == button2 && forceB > forceA))
            {
                Debug.Log("true");
                result = true;
            }
            else
            {
                Debug.Log("false");
                result = false;
            }
        }

        //if (buttonPressed == button1)
        //{
        //    answer = "1";
        //    Debug.Log("=");
        //    if (forceA == forceB)
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        result = false;
        //    }
        //    //result = false;
        //    Debug.Log(result);
        //}

        //if (buttonPressed == button2)
        //{
        //    answer = "2";
        //    Debug.Log("!=");
        //    if (forceA != forceB)
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        result = false;
        //    }
        //    //result = true;
        //    Debug.Log(result);
        //}

        //logger.Log("A", (experiment.J - 1).ToString(), experiment.testingGroup[0].ToString("0.0"), result.ToString());
        //logger.Log("B", (experiment.J - 1).ToString(), experiment.testingGroup[1].ToString("0.0"), answer);

        //if (experiment.J >= 30)
        //{
        //    experiment.J +=1 ;
        //}

        if (experiment.staircase.ReversalLimitReached() && saved == false)
        {
            logger.CreateCSV();
            saved = true;
        }

    }

    //public string Answer
    //{
    //    get { return answer; }
    //}
}
