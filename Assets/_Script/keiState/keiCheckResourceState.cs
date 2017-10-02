/// <summary>
/// keiCheckResourceState V1.2
/// Kei Lazu
/// 
/// Desc;
/// Check Resource that we give
/// Input: Player Resources
/// Output: Data player resources filled
/// 
/// - Second State
/// 
/// Changelog:
/// 1.2 - Change Output source to more reliable source
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
            kei_Owner.keiStateMachine.keiChangeState(keiPrioritizeStateV2.kei_getsetInstance);

        } else
        {
            //Debug.Log("wait");

        }
        

    }

    public void keiCheckingResource(keiInitializatorGameObject kei_Init, keiSmartAutoController kei_SmartAuto)
    {

        kei_SmartAuto.keiPlayerResource[0][0] = kei_Init.keiDropResTypeSlot1.value;
        kei_SmartAuto.keiPlayerResource[1][0] = kei_Init.keiDropResTypeSlot2.value;
        kei_SmartAuto.keiPlayerResource[2][0] = kei_Init.keiDropResTypeSlot3.value;
        kei_SmartAuto.keiPlayerResource[3][0] = kei_Init.keiDropResTypeSlot4.value;
        kei_SmartAuto.keiPlayerResource[4][0] = kei_Init.keiDropResTypeSlot5.value;

        kei_SmartAuto.keiPlayerResource[0][1] = kei_Init.keiDropResElemSlot1.value;
        kei_SmartAuto.keiPlayerResource[1][1] = kei_Init.keiDropResElemSlot2.value;
        kei_SmartAuto.keiPlayerResource[2][1] = kei_Init.keiDropResElemSlot3.value;
        kei_SmartAuto.keiPlayerResource[3][1] = kei_Init.keiDropResElemSlot4.value;
        kei_SmartAuto.keiPlayerResource[4][1] = kei_Init.keiDropResElemSlot5.value;

        //kei_SmartAuto.keiIntelChecker();
        kei_SmartAuto.keiIsFinished = true;

    }

}
