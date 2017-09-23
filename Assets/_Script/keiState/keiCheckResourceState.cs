/// <summary>
/// keiCheckResourceState V1.1
/// Kei Lazu
/// 
/// Desc;
/// Check Resource that we give
/// Input: 
/// 
/// - Second State
/// 
/// Changelog:
/// 1.1 - Can now add resource into smart auto control
/// 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using keiStateControlStuff;
using UnityEngine.UI;

public class keiCheckResourceState : keiState<keiSmartAutoController>
{
    public static keiCheckResourceState keiInstance;

    private keiCheckResourceState()
    {
        if (keiInstance != null)
        {
            return;

        }

        keiInstance = this;

    }

    public static keiCheckResourceState kei_getsetInstance
    {
        get
        {
            if (keiInstance == null)
            {
                new keiCheckResourceState();

            }

            return keiInstance;

        }

    }

    // Start Region: Init ---------------------------------------------

    // Region: Pathfinder
    keiInitializatorGameObject keiInitScript;

    // End Region: Init -----------------------------------------------


    public override void keiEnterState(keiSmartAutoController kei_Owner)
    {
        kei_Owner.keiIsFinished = false;

        keiInitScript = GameObject.Find("keiInitScript").GetComponent<keiInitializatorGameObject>();

        keiInitScript.keiLblLogState.GetComponent<Text>().text = "State: Checking Resource";

        keiCheckingResource(keiInitScript, kei_Owner);
        kei_Owner.keiChangingState();

    }

    public override void keiExitState(keiSmartAutoController kei_Owner)
    {
        kei_Owner.keiIsFinished = false;

    }

    public override void keiUpdateState(keiSmartAutoController kei_Owner)
    {
        if (kei_Owner.keiIsFinished)
        {
            kei_Owner.keiStateMachine.keiChangeState(keiPrioritizeState.kei_getsetInstance);

        } else
        {
            //Debug.Log("wait");

        }
        

    }

    public void keiCheckingResource(keiInitializatorGameObject kei_Init, keiSmartAutoController kei_SmartAuto)
    {
        kei_SmartAuto.keiPlayerTypeResource[0] = kei_Init.keiDropResTypeSlot1.value;
        kei_SmartAuto.keiPlayerTypeResource[1] = kei_Init.keiDropResTypeSlot2.value;
        kei_SmartAuto.keiPlayerTypeResource[2] = kei_Init.keiDropResTypeSlot3.value;
        kei_SmartAuto.keiPlayerTypeResource[3] = kei_Init.keiDropResTypeSlot4.value;
        kei_SmartAuto.keiPlayerTypeResource[4] = kei_Init.keiDropResTypeSlot5.value;

        kei_SmartAuto.keiPlayerElemResource[0] = kei_Init.keiDropResElemSlot1.value;
        kei_SmartAuto.keiPlayerElemResource[1] = kei_Init.keiDropResElemSlot2.value;
        kei_SmartAuto.keiPlayerElemResource[2] = kei_Init.keiDropResElemSlot3.value;
        kei_SmartAuto.keiPlayerElemResource[3] = kei_Init.keiDropResElemSlot4.value;
        kei_SmartAuto.keiPlayerElemResource[4] = kei_Init.keiDropResElemSlot5.value;

        //kei_SmartAuto.keiIntelChecker();
        kei_SmartAuto.keiIsFinished = true;

    }

}
