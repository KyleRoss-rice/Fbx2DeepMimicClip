using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class dataWriter : MonoBehaviour
{
    public static bool isAnimationDone;
    public static bool isAnimationPlaying;

    private static float[] data = JointTracker.jointData;
    private static int n = data.Length;
    private string path = "../DeepMimic/data/motions/humanoid3d_drunk_idle.txt";
    private StreamWriter writer;
    private StringBuilder sb;
    
    void Start()
    {
        sb = new StringBuilder(45 * 21);
        writer = new StreamWriter(path, false, Encoding.ASCII);
        sb.AppendLine("{\n\"Loop\": \"none\",\n\"Frames\":\n[");
        writer.AutoFlush = false;
    }

    private void LateUpdate()
    {
        if (isAnimationPlaying)
        {
            sb.Append("[");
            for (int i = 0; i < n-1; ++i)
            {
                sb.Append($"{data[i]:0.0000000000},".PadLeft(21));
            }
            sb.Append($"{data[n-1]:0.0000000000}".PadLeft(21));
            sb.Append("],\n");
        }
        else if (isAnimationDone)
        {
            sb.Remove(sb.Length - 2, 2);
            sb.AppendLine("]\n}");
            writer.Write(sb.ToString());
            writer.Flush();
            writer.Close();
            gameObject.SetActive(false);
        }
    }
}
