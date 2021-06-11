using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunsController : MonoBehaviour
{
    public GameObject[] guns;
    public int fireLevel;
    public int UpgradeFire()
    {
        if (fireLevel + 1 == guns.Length)
        {
            return fireLevel;
        }
        fireLevel++;
        for (int i = 0; i < guns.Length; i++)
        {
            if(i == fireLevel)
            {
                guns[i].SetActive(true);
            }
            else
            {
                guns[i].SetActive(false);
            }
        }
        return fireLevel;
    }
    public void ResumeFire()
    {
        fireLevel = 0;
        for (int i = 0; i < guns.Length; i++)
        {
            if (i == fireLevel)
            {
                guns[i].SetActive(true);
            }
            else
            {
                guns[i].SetActive(false);
            }
        }
    }
}
