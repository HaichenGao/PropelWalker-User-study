using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staircase : MonoBehaviour
{
    [SerializeField]
    float firstVal;

    [SerializeField]
    float fundVal;

    [SerializeField]
    float stepSize;

    [SerializeField]
    float factor;

    [SerializeField]
    int down;

    [SerializeField]
    int up;

    [SerializeField]
    int direction;

    [SerializeField]
    int reversalLimit;

    int successiveGood = 0;
    int successiveBad = 0;

    List<float> val = new List<float>();
    List<float> reversals = new List<float>();


    void Start()
    {
        val.Add(firstVal);
    }

    public List<float> Val
    {
        get { return val; }
    }

    public List<float> Reversals
    {
        get { return reversals; }
    }

    public float FundVal
    {
        get { return fundVal; }
    }

    public float StepSize
    {
        get { return stepSize; }
        set { stepSize = value; }
    }

    public float Factor
    {
        get { return factor; }
    }

    public int Direction
    {
        get { return direction; }
    }

    public float NextVal(bool answer)
    {
        if (answer)
        {
            if(direction == 1)
            {
                successiveGood++;
                successiveBad = 0;
                if(successiveGood >= down)
                {
                    val.Add(val[val.Count - 1] + stepSize);
                    successiveGood = 0;
                    //return Mathf.Round(val[val.Count - 1], 2);
                    return val[val.Count - 1];
                    
                }
                else
                {
                    val.Add(val[val.Count - 1]);
                    return val[val.Count - 1];
                }
            }
            else
            {
                successiveGood++;
                successiveBad = 0;
                if (successiveGood >= down)
                {
                    val.Add(val[val.Count - 1] - stepSize);
                    successiveGood = 0;
                    return val[val.Count - 1];

                }
                else
                {
                    val.Add(val[val.Count - 1]);
                    return val[val.Count - 1];
                }

            }
        }
        else
        {
            if(direction == 1)
            {
                successiveGood = 0;
                successiveBad++;
                if(successiveBad >= up)
                {
                    val.Add(val[val.Count - 1] - stepSize);
                    successiveBad = 0;
                    return val[val.Count - 1];
                }
                else
                {
                    val.Add(val[val.Count - 1]);
                    return val[val.Count - 1];
                }
            }
            else
            {
                successiveGood = 0;
                successiveBad++;
                if (successiveBad >= up)
                {
                    val.Add(val[val.Count - 1] + stepSize);
                    successiveBad = 0;
                    return val[val.Count - 1];

                }
                else
                {
                    val.Add(val[val.Count - 1]);
                    return val[val.Count - 1];
                }
            }
        }
    }

    public float GetLast()
    {
        return val[val.Count - 1];
    }

    public void GetReversals()
    {
        ResetReversals();
        int dir = direction * -1;
        for(int i = 1; i < val.Count; i++)
        {
            if((dir == 1 && val[i] > val[i - 1]) || (dir == -1 && val[i] < val[i - 1]))
            {
                reversals.Add(val[i - 1]);
                dir = dir * -1;
            }
        }
    }

    public void ResetReversals()
    {
        reversals.Clear();
    }

    public bool ReversalLimitReached()
    {
        GetReversals();
        if(reversals.Count >= reversalLimit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float GetFinalVal()
    {
        float sum = 0;
        for(int i = 5; i < reversals.Count; i++)
        {
            sum = sum + reversals[i];
        }
        return sum / (reversals.Count - 5);
    }
}
