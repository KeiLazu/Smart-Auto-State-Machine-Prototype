using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using keiStateControlStuff;

public class keiSmartAutoController : MonoBehaviour {

    public bool keiSwitchState = false;
    public float keiTimer;
    public int keiSec = 0;

    public keiStateMachine<keiSmartAutoController> keiStateMachine { get; set; }

    private void Start()
    {
        keiStateMachine = new keiStateMachine<keiSmartAutoController>(this);
        keiStateMachine.keiChangeState(keiScanState.kei_getsetInstance);
        keiTimer = Time.time;
        
    }

    private void Update()
    {
        if (Time.time > keiTimer + 1)
        {
            keiTimer = Time.time;
            keiSec++;
            Debug.Log(keiSec);

        }

        if (keiSec == 3)
        {
            keiSec = 0;
            keiSwitchState = !keiSwitchState;

        }

        keiStateMachine.keiUpdate();
        
    }

}
