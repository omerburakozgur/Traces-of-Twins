using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FillFuelButton : MonoBehaviour
{
    [SerializeField] private FillFuelManager FillFuelManagerReference;
    [SerializeField] private int fuelClickCounter;
    [SerializeField] private int fuelTargetCounter;

    [SerializeField] private GameObject fuelCover;
    [SerializeField] private GameObject fuelLid;

    public void AddFuelPoints()
    {
        fuelClickCounter++;
        FillFuelManagerReference.AddFuelPoints();

    }

}
