using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Logger : MonoBehaviour
{
    string id;
    string timeStamp;
    List<string[]> rowData = new List<string[]>();
    string[] rowDataTemp = new string[6];

    // Start is called before the first frame update
    void Start()
    {
        timeStamp = System.DateTime.Now.ToString("yyyymmdd_hhmmss");
        rowDataTemp[0] = "ID";
        rowDataTemp[1] = "Trial";
        rowDataTemp[2] = "A";
        rowDataTemp[3] = "B";
        rowDataTemp[4] = "Answer";
        rowDataTemp[5] = "Result";
        rowData.Add(rowDataTemp);
        //Debug.Log("addddd");
    }

    // Update is called once per frame
    //public void Log(string type, string round, string force, string result)
    //{
    //    rowDataTemp = new string[5];
    //    rowDataTemp[0] = id;
    //    rowDataTemp[1] = type;
    //    rowDataTemp[2] = round;
    //    rowDataTemp[3] = force;
    //    rowDataTemp[4] = result;
    //    rowData.Add(rowDataTemp);
    //}

    public void Log(string trial, string forceA, string forceB, string ans, string res)
    {
        rowDataTemp = new string[6];
        rowDataTemp[0] = id;
        rowDataTemp[1] = trial;
        rowDataTemp[2] = forceA;
        rowDataTemp[3] = forceB;
        rowDataTemp[4] = ans;
        rowDataTemp[5] = res;
        rowData.Add(rowDataTemp);
    }

    public void LogVal(List<float> lv)
    {
        lv.ToArray();
        string[] n = new string[lv.Count];
        for(int i = 0; i < lv.Count; i ++)
        {
            n[i] = lv[i].ToString();
        }
        rowData.Add(n);
    }

    public void LogReversals(List<float> lr)
    {
        lr.ToArray();
        string[] n = new string[lr.Count];
        for (int i = 0; i < lr.Count; i++)
        {
            n[i] = lr[i].ToString();
        }
        rowData.Add(n);
    }

    public void LogFinalVal(float lf)
    {       
        string[] n = new string[1];
        n[0] = lf.ToString();
        rowData.Add(n);

    }

    public void CreateCSV()
    {
        string[][] output = new string[rowData.Count][];

        for(int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for(int index = 0; index < length; index++)
        {
            sb.AppendLine(string.Join(delimiter, output[index]));
        }

        string filePath = GetPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    public void GetUserID()
    {
        id = GameObject.Find("UserIDInputField").GetComponent<TMP_InputField>().text;

    }

    private string GetPath()
    {
        //return Application.dataPath + "/Logs/" + id + "_" + timeStamp + "_event.csv";
        return Application.dataPath + "/" + id + "_" + timeStamp + "_event.csv";
    }
}
