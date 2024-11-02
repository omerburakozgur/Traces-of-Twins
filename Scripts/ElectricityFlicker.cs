using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityFlicker : MonoBehaviour
{
    
    public void flickerLight()
    {
        GameManager.Instance.ToggleLightmaps();
    }
}
