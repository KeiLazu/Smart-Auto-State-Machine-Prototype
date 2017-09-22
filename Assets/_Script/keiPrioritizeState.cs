using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using keiStateControlStuff;

public class keiPrioritizeState : keiState<keiSmartAutoController>
{

    private static keiPrioritizeState kei_Instance;

    private keiPrioritizeState()
    {
        if (kei_Instance != null)
        {
            return;

        }

        kei_Instance = this;

    }

    public static keiPrioritizeState kei_getsetInstance
    {
        get
        {
            if (kei_Instance == null)
            {
                new keiPrioritizeState();

            }

            return kei_Instance;

        }

    }

    public override void keiEnterState(keiSmartAutoController kei_Owner)
    {
        Debug.Log("Entering Prioritize State");
    }

    public override void keiExitState(keiSmartAutoController kei_Owner)
    {
        Debug.Log("Exiting Prioritize State");
    }

    public override void keiUpdateState(keiSmartAutoController kei_Owner)
    {
        if (!kei_Owner.keiSwitchState)
        {
            kei_Owner.keiStateMachine.keiChangeState(keiScanState.kei_getsetInstance);

        }
    }
}
