using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoltButtonPiece : MonoBehaviour
{
    [SerializeField] private TireChangeManager TireChangeManagerReference;
    [SerializeField] private int boltClickCounter;
    [SerializeField] private int boltTargetCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddPoints()
    {
        boltClickCounter++;
        AudioManager.PlayRandomSound(AudioManager.Instance.tireChangeClickSounds); // bolt is opened
        if (boltClickCounter == boltTargetCounter)
        {
            TireChangeManagerReference.AddBoltPoints();
            Destroy(gameObject);
        }

    }
    
}
