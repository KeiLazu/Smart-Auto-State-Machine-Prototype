using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using keiStateControlStuff;

public class keiScanState : keiState<keiSmartAutoController>
{

    private static keiScanState kei_Instance;

    private keiScanState()
    {
        if (kei_Instance != null)
        {
            return;

        }

        kei_Instance = this;

    }

    public static keiScanState kei_getsetInstance
    {
        get
        {
            if (kei_Instance == null)
            {
                new keiScanState();

            }

            return kei_Instance;

        }

    }

    public override void keiEnterState(keiSmartAutoController kei_Owner)
    {
        Debug.Log("Entering Scan State");
    }

    public override void keiExitState(keiSmartAutoController kei_Owner)
    {
        Debug.Log("Exiting Scan State");
    }

    public override void keiUpdateState(keiSmartAutoController kei_Owner)
    {
        if (kei_Owner.keiSwitchState)
        {
            kei_Owner.keiStateMachine.keiChangeState(keiPrioritizeState.kei_getsetInstance);

        }
    }
}
