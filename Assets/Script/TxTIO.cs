using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
public class TxTIO
{
    public static StreamWriter writer;
    public static StreamReader reader;
    private static List<int> allScore;
    public static void Init()
    {
        allScore = new List<int>();
        FileInfo file = new FileInfo(Application.dataPath + "/Data.txt");
        if (file.Exists)
        {
            file.Delete();
            file.Refresh();
        }
    }
    //把所有的数据写入文本中
    public static void WriteIntoTxt(string message)
    {
        FileInfo file = new FileInfo(Application.dataPath + "../../Data.txt");
        if (!file.Exists)
        {
            writer = file.CreateText();
        }
        else
        {
            writer = file.AppendText();
        }
        
        writer.Flush();
        writer.WriteLine(message);
        writer.Dispose();
        writer.Close();
    }
    //读取分数 存储到列表中
    public static int ReadMaxScore()
    {
        reader = new StreamReader(Application.dataPath + "../../Data.txt", Encoding.UTF8);
        string text;
        int maxScore = 0;
        while ((text = reader.ReadLine()) != null)
        {
            int score = int.Parse(text);
            allScore.Add(score);
            maxScore = Mathf.Max(maxScore, score);
        }
        reader.Dispose();
        reader.Close();
        return maxScore;
    }
}