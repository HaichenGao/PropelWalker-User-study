using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database10 : MonoBehaviour
{
    float[] forceA = new float[3] { -1.2f, -2.4f, 0.4f };
    float[] forceB = new float[10] { -2.8f, -2.4f, -2.0f, -1.6f, -1.2f, -0.8f, -0.4f, 0.0f, 0.4f, 0.8f };

    //float[,] forceAandB = new float[60, 2];
    float[] combineAandB = new float[2];
    float[][] forceGroup = new float[30][];

    void Start()
    {
        
        for (int i = 0; i < 30; i++)
        {
            forceGroup[i] = new float[2];
            
        }
                
    }

        
    public float[] ForceA
    {
        get { return forceA; }
    }

    public float[] ForceB
    {
        get { return forceB; }
    }

    public float[][] ForceGroup
    { 
        get { return forceGroup; }
    }

    
    public void Shuffle()
    {
        
        for (int i = forceGroup.Length; i > 0; i--)
        {
            int rnd = Random.Range(0, i);
            float[] temp = new float[2];
            temp[0] = forceGroup[i - 1][0];
            temp[1] = forceGroup[i - 1][1];
            forceGroup[i - 1][0] = forceGroup[rnd][0];
            forceGroup[i - 1][1] = forceGroup[rnd][1];
            forceGroup[rnd][0] = temp[0];
            forceGroup[rnd][1] = temp[1];
            //Debug.Log(rnd);
        }
    }

    public void ShuffleArray(double[] array)
    {
        for(int i = array.Length; i > 0; i--)
        {
            int rnd = Random.Range(0, i);
            double temp = array[i - 1];
            array[i - 1] = array[rnd];
            array[rnd] = temp;
        }
    }



    public void Combine()
    {
        
        for(int j = 0; j < 30; j++)
        {
            if(j < 10)
            {
                combineAandB[0] = forceA[0];               
                combineAandB[1] = forceB[j];
                forceGroup[j][0] = combineAandB[0];
                forceGroup[j][1] = combineAandB[1];
                
            }
            else if(10 <=j && j < 20)
            {
                combineAandB[0] = forceA[1];
                combineAandB[1] = forceB[j-10];
                forceGroup[j][0] = combineAandB[0];
                forceGroup[j][1] = combineAandB[1];
                
            }
            else 
            {
                combineAandB[0] = forceA[2];
                combineAandB[1] = forceB[j-20];
                forceGroup[j][0] = combineAandB[0];
                forceGroup[j][1] = combineAandB[1];
                
            }
        }
 
    }



}
