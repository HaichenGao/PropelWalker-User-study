using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    [SerializeField]
    float water;

    [SerializeField]
    float air;

    [SerializeField]
    float sand;

    [SerializeField]
    float mud;

    float[] forceA = new float[20];
    

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            forceA[i] = water;
        }

        for (int i = 5; i < 10; i++)
        {
            forceA[i] = air;
        }

        for (int i = 10; i < 15; i++)
        {
            forceA[i] = sand;
        }

        for (int i = 15; i < 20; i++)
        {
            forceA[i] = mud;
        }

        ShuffleArray(forceA);

        for (int k = 0; k < 20; k++)
        {
            Debug.Log(forceA[k]);
        }
        

    }


    //public float[] ForceA
    //{
    //    get { return forceA; }
    //}

    //public float[] ForceB
    //{
    //    get { return forceB; }
    //}

    //public float[][] ForceGroup
    //{ 
    //    get { return forceGroup; }
    //}


    //public void Shuffle()
    //{

    //    for (int i = forceGroup.Length; i > 0; i--)
    //    {
    //        int rnd = Random.Range(0, i);
    //        float[] temp = new float[2];
    //        temp[0] = forceGroup[i - 1][0];
    //        temp[1] = forceGroup[i - 1][1];
    //        forceGroup[i - 1][0] = forceGroup[rnd][0];
    //        forceGroup[i - 1][1] = forceGroup[rnd][1];
    //        forceGroup[rnd][0] = temp[0];
    //        forceGroup[rnd][1] = temp[1];
    //        //Debug.Log(rnd);
    //    }
    //}

    public float[] ForceA
    {
        get { return forceA; }
    }

    public float Water
    {
        get { return water; }
    }

    public float Air
    {
        get { return air; }
    }

    public float Sand
    {
        get { return sand; }
    }

    public float Mud
    {
        get { return mud; }
    }


    public void ShuffleArray(float[] array)
    {
        for(int i = array.Length; i > 0; i--)
        {
            int rnd = Random.Range(0, i);
            float temp = array[i - 1];
            array[i - 1] = array[rnd];
            array[rnd] = temp;
        }
    }



    //public void Combine()
    //{
        
    //    for(int j = 0; j < 57; j++)
    //    {
    //        if(j < 19)
    //        {
    //            combineAandB[0] = forceA[0];               
    //            combineAandB[1] = forceB[j];
    //            forceGroup[j][0] = combineAandB[0];
    //            forceGroup[j][1] = combineAandB[1];
                
    //        }
    //        else if(19 <=j && j < 38)
    //        {
    //            combineAandB[0] = forceA[1];
    //            combineAandB[1] = forceB[j-19];
    //            forceGroup[j][0] = combineAandB[0];
    //            forceGroup[j][1] = combineAandB[1];
                
    //        }
    //        else 
    //        {
    //            combineAandB[0] = forceA[2];
    //            combineAandB[1] = forceB[j-38];
    //            forceGroup[j][0] = combineAandB[0];
    //            forceGroup[j][1] = combineAandB[1];
                
    //        }
    //    }
 
    //}



}
