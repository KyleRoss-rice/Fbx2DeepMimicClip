using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JointTracker : MonoBehaviour
{
    public static float[] jointData = new float[44];
    public Text uiText;

    public int[] IDs;
    public bool isRevolute = false;
    public bool trackPosition = false;
    public bool trackRotation = false;
    public int dir = 1;
    public bool useX = false;

    private Vector3 initRot;
    void Start()
    {
        initRot = transform.rotation.eulerAngles;
    }

    void Update()
    {
        if (isRevolute)
        {
            var rot = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, initRot.y, initRot.z));
            transform.rotation = rot;
            float a;
            if (useX)
                a = transform.localRotation.eulerAngles.x;
            else
                a = transform.localRotation.eulerAngles.y;
            
            a *= -Mathf.Deg2Rad * dir;
            
            jointData[IDs[0]] = a;
            uiText.text = $"xyz: {transform.position}\nangle: {a}";
            
            if (!trackRotation)
                jointData[IDs[0]] = 0;
        }
        else
        {
            if (trackPosition)
            {
                jointData[IDs[0]] = transform.position.z;
                jointData[IDs[1]] = transform.position.y - 0.11f;
                jointData[IDs[2]] = transform.position.x;
                var rotation = transform.localRotation;
                jointData[IDs[3]] = rotation.w;
                jointData[IDs[4]] = -rotation.z;
                jointData[IDs[5]] = -rotation.y;
                jointData[IDs[6]] = -rotation.x;
                uiText.text = $"xyz: {transform.position}\nwxyz: {rotation}";
                if (!trackRotation)
                {
                    jointData[IDs[3]] = 1;
                    jointData[IDs[4]] = 0;
                    jointData[IDs[5]] = 0;
                    jointData[IDs[6]] = 0;
                }
            }
            else
            {
                var rotation = transform.localRotation;
                jointData[IDs[0]] = rotation.w;
                jointData[IDs[1]] = -rotation.z * dir;
                jointData[IDs[2]] = -rotation.y * dir;
                jointData[IDs[3]] = -rotation.x * dir;
                
                uiText.text = $"xyz: {transform.position}\nwxyz: {rotation}";
                if (!trackRotation)
                {
                    jointData[IDs[0]] = 1;
                    jointData[IDs[1]] = 0;
                    jointData[IDs[2]] = 0;
                    jointData[IDs[3]] = 0;
                }
            }
        }
    }
}
