/// <summary>
/// keiPrioritizeStateV2 V 2.1
/// Kei Lazu
/// 
/// Desc:
/// Prioritizing redefined
/// 
/// Changelog:
/// 2.1: Multilayer Priority (Planning)
/// 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using keiStateControlStuff;

public class keiPrioritizeStateV2 : keiState<keiSmartAutoController> {

    private static keiPrioritizeStateV2 keiInstance;

    private keiPrioritizeStateV2()
    {
        if (keiInstance != null)
        {
            return;
        }

        keiInstance = this;
    }

    public static keiPrioritizeStateV2 kei_getsetInstance
    {
        get
        {
            if (keiInstance == null)
            {
                new keiPrioritizeStateV2();
            }

            return keiInstance;
        }
    }

    // Start Region: Init
    // Region: Pathfinder
    keiInitializatorGameObject keiInitScript;

    // Region: Reward and Punishment
    int keiMeritX, keiDemeritX;

    // Region: Position
    int keiEnemyTotalRed, keiEnemyTotalBlue, keiEnemyTotalGreen, keiEnemyTotalYellow, keiEnemyTotalMagenta;
    int keiFPCol, keiFPRow, keiFPTurn; // First Priority
    int keiSPCol, keiSPRow, keiSPTurn; // Second Priority
    int keiTPCol, keiTPRow, keiTPTurn; // Third Priority

    // Region: Judgement
    int keiTotalColor;
    int keiSelectColor;

    // Init Col Row Merit System
    //int keiColMerit = new int();
    //int keiRowMerit = new int();
    //int keiColDemerit = new int();
    //int keiRowDemerit = new int();

    // End Init

    public override void keiEnterState(keiSmartAutoController kei_Owner)
    {
        kei_Owner.keiIsFinished = false;

        keiMeritX = 0;
        keiDemeritX = 0;

        keiInitScript = GameObject.Find("keiInitScript").GetComponent<keiInitializatorGameObject>();

        keiInitScript.keiLblLogState.GetComponent<Text>().text = "State: Prioritizing";

        keiPriorityPos(kei_Owner);

    }

    public override void keiExitState(keiSmartAutoController kei_Owner)
    {
        kei_Owner.keiIsFinished = false;

    }

    public override void keiUpdateState(keiSmartAutoController kei_Owner)
    {
        if (kei_Owner.keiIsFinished)
        {
            kei_Owner.keiStateMachine.keiChangeState(keiAttackState.kei_getsetInstance);
        }

    }

    // Helper Phase 0: Get Max
    public int keiManyEnemyColor(int kei_Red, int kei_Blue, int kei_Green, int kei_Yellow, int kei_Magenta, keiSmartAutoController kei_SmartAuto)
    {        
        int[,] keiColorManagementSystem = new int[5,2] { { 1, kei_Red }, { 2, kei_Blue }, { 3, kei_Green }, { 4, kei_Yellow }, { 5, kei_Magenta } };

        keiTotalColor = 0;
        keiSelectColor = 0;

        for (int i = 0; i < 5; i++)
        {

            if (keiColorManagementSystem[i,1] > keiTotalColor)
            {
                keiTotalColor = keiColorManagementSystem[i, 1];
                keiSelectColor = i + 1;

            }

        }

        return keiSelectColor;

    }

    // Phase 0: Check Enemy In Color
    public void keiCheckEnemyColor(keiSmartAutoController kei_SmartAuto)
    {
        keiEnemyTotalRed = 0;
        keiEnemyTotalBlue = 0;
        keiEnemyTotalGreen = 0;
        keiEnemyTotalYellow = 0;
        keiEnemyTotalMagenta = 0;

        for (int keiCol = 0; keiCol < kei_SmartAuto.keiEnemyIntel.Length; keiCol++)
        {
            for (int keiRow = 0; keiRow < kei_SmartAuto.keiEnemyIntel.Length; keiRow++)
            {
                switch (kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][0])
                {
                    case 1:
                        keiEnemyTotalRed++;
                        break;

                    case 2:
                        keiEnemyTotalBlue++;
                        break;

                    case 3:
                        keiEnemyTotalGreen++;
                        break;

                    case 4:
                        keiEnemyTotalYellow++;
                        break;

                    case 5:
                        keiEnemyTotalMagenta++;
                        break;

                    default:
                        break;

                }

            }

        }

    }

    // Phase 1.1: Primary Position (clear)
    public void keiPriorityPos(keiSmartAutoController kei_SmartAuto)
    {
        // emptying turn
        keiFPTurn = 9;
        keiSPTurn = 9;
        keiTPTurn = 9;

        //// for Col Row Merit System
        //keiColMerit = 0;
        //keiRowMerit = 0;
        //keiColDemerit = 0;
        //keiRowDemerit = 0;

        for (int keiCol = 0; keiCol < 3; keiCol++)
        {
            for (int keiRow = 0; keiRow < 3; keiRow++)
            {
                //Debug.Log("Type: " + kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][0] + "\nTurn: " + kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][1] + "\nCol: " + keiCol + "\nRow: " + keiRow);

                if (kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][0] > 0)
                {

                    if (keiFPTurn > kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][1])
                    {
                        // shift turn
                        keiTPTurn = keiSPTurn;
                        keiSPTurn = keiFPTurn;
                        keiFPTurn = kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][1];

                        // shift pos
                        keiTPCol = keiSPCol;
                        keiTPRow = keiSPRow;

                        keiSPCol = keiFPCol;
                        keiSPRow = keiFPRow;

                        keiFPCol = keiCol;
                        keiFPRow = keiRow;

                        //Debug.Log("FPCol: " + keiCol + " | FPRow: " + keiRow);
                        //Debug.Log("First Priority: " + keiFPTurn + " | Second Priority: " + keiSPTurn + " | Third Priority: " + keiTPTurn);
                        continue;

                    }
                    else if (keiSPTurn > kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][1])
                    {
                        // shift turn
                        keiTPTurn = keiSPTurn;
                        keiSPTurn = kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][1];

                        // shift pos
                        keiTPCol = keiSPCol;
                        keiTPRow = keiSPRow;

                        keiSPCol = keiCol;
                        keiSPRow = keiRow;

                        //Debug.Log("SPCol: " + keiCol + " | SPRow: " + keiRow);
                        //Debug.Log("Second Priority: " + keiSPTurn + " | Third Priority: " + keiTPTurn);
                        continue;

                    }

                }

            }

        }

        Debug.Log("FPPos: " + keiFPCol + ":" + keiFPRow + " FPTurn: " + keiFPTurn +
            "\nSPPos: " + keiSPCol + ":" + keiSPRow + " SPTurn: " + keiSPTurn + 
            "\nTPPos: " + keiTPCol + ":" + keiTPRow + " TPTurn: " + keiTPTurn);

        keiCheckAoEX(kei_SmartAuto);

    }

    // Phase 2: Check AoE and X
    public void keiCheckAoEX(keiSmartAutoController kei_SmartAuto)
    {
        for (int i = 0; i < kei_SmartAuto.keiPlayerResource.Length; i++)
        {

            switch (kei_SmartAuto.keiPlayerResource[i][0])
            {
                case 3:

                    break;

                case 4:
                    keiAoESystem(kei_SmartAuto, i);

                    break;

                default:
                    break;

            }

        }

        if (kei_SmartAuto.keiDecisionAttack == 9)
        {
            keiCheckColRow(kei_SmartAuto);

        }

    }

    // (Helper) Phase 2.1: Aoe Intensifies
    public void keiAoESystem(keiSmartAutoController kei_SmartAuto, int keiIFromFor)
    {
        keiCheckEnemyColor(kei_SmartAuto);

        int keiBenefitParam = new int();

        keiBenefitParam = keiManyEnemyColor(keiEnemyTotalRed,
                                                keiEnemyTotalBlue,
                                                keiEnemyTotalGreen,
                                                keiEnemyTotalYellow,
                                                keiEnemyTotalMagenta,
                                                kei_SmartAuto);

        // Red(0) <- Blue(1) <- Green(2) <- Red(0)

        //Debug.Log(keiBenefitParam + " is Benefit Param for this wave");

        switch (keiBenefitParam)
        {
            case 1:
                if (kei_SmartAuto.keiPlayerResource[keiIFromFor][1] != 2 && keiTotalColor > 4)
                {
                    // suggest
                    Debug.Log("Check Case 1 Benefit param");
                    kei_SmartAuto.keiDecisionAttack = 4;
                    kei_SmartAuto.keiDecisionAttackSlot = keiIFromFor;

                }

                break;

            case 2:
                if (kei_SmartAuto.keiPlayerResource[keiIFromFor][1] != 0 && keiTotalColor > 4)
                {
                    // suggest
                    Debug.Log("Check Case 2 Benefit Param");
                    kei_SmartAuto.keiDecisionAttack = 4;
                    kei_SmartAuto.keiDecisionAttackSlot = keiIFromFor;

                }

                break;

            case 3:
                if (kei_SmartAuto.keiPlayerResource[keiIFromFor][1] != 1 && keiTotalColor > 4)
                {
                    // suggest
                    Debug.Log("Check Case 3 Benefit Param");
                    kei_SmartAuto.keiDecisionAttack = 4;
                    kei_SmartAuto.keiDecisionAttackSlot = keiIFromFor;

                }

                break;

            case 4:
                // suggest
                Debug.Log("Check Case 4 Benefit Param");
                kei_SmartAuto.keiDecisionAttack = 4;
                kei_SmartAuto.keiDecisionAttackSlot = keiIFromFor;

                break;

            case 5:
                // suggest
                Debug.Log("Check Case 5 Benefit Param");
                kei_SmartAuto.keiDecisionAttack = 4;
                kei_SmartAuto.keiDecisionAttackSlot = keiIFromFor;

                break;

            default:
                break;

        }

    }

    // (Helper) Phase 3.1: Col and Row Calculator
    public void keiColRowDecision(keiSmartAutoController kei_SmartAuto, int keiLimiterElement, int keiSwitcher, int keiSlotResource)
    {
        int keiMeritRow = new int();
        keiMeritRow = 0;

        switch (keiSwitcher)
        {
            case 0:
                for (int keiCheckCol = 0; keiCheckCol < 3; keiCheckCol++)
                {
                    if (kei_SmartAuto.keiEnemyIntel[keiCheckCol][keiFPRow][0] > 0 && kei_SmartAuto.keiEnemyIntel[keiCheckCol][keiFPRow][0] != keiLimiterElement)
                    {
                        keiMeritRow++;

                    }

                }

                if (keiMeritRow >= 2)
                {
                    // Suggest
                    Debug.Log("Suggesting Slot: " + (keiSlotResource + 1));
                    kei_SmartAuto.keiDecisionAttack = 1;
                    kei_SmartAuto.keiDecisionAttackSlot = keiSlotResource + 1;

                }

                Debug.Log(keiMeritRow);
                break;

            case 1:
                for (int keiCheckRow = 0; keiCheckRow < 3; keiCheckRow++)
                {
                    if (kei_SmartAuto.keiEnemyIntel[keiFPCol][keiCheckRow][0] > 0 && kei_SmartAuto.keiEnemyIntel[keiFPCol][keiCheckRow][0] != keiLimiterElement)
                    {
                        keiMeritRow++;

                    }

                }

                if (keiMeritRow >= 2)
                {
                    // Suggest
                    Debug.Log("Suggesting Slot: " + (keiSlotResource + 1));
                    kei_SmartAuto.keiDecisionAttack = 1;
                    kei_SmartAuto.keiDecisionAttackSlot = keiSlotResource + 1;

                }

                Debug.Log(keiMeritRow);
                break;

            default:
                break;
        }


    }

    // Phase 3: Check Col and Row
    public void keiCheckColRow(keiSmartAutoController kei_SmartAuto)
    {
        // TODO: Check Col and Row, if any compare element, if none run to single
        for (int i = 0; i < kei_SmartAuto.keiPlayerResource.Length; i++)
        {
            switch (kei_SmartAuto.keiPlayerResource[i][0])
            {
                case 1:
                    // Row
                    switch (kei_SmartAuto.keiPlayerResource[i][1])
                    {
                        case 0:
                            // Fire
                            keiColRowDecision(kei_SmartAuto, 2, 0, i);

                            break;

                        case 1:
                            // Water
                            keiColRowDecision(kei_SmartAuto, 3, 0, i);

                            break;

                        case 2:
                            // Earth
                            keiColRowDecision(kei_SmartAuto, 1, 0, i);

                            break;

                        case 3:
                            // Light
                            keiColRowDecision(kei_SmartAuto, 0, 0, i);

                            break;

                        case 4:
                            // Dark
                            keiColRowDecision(kei_SmartAuto, 0, 0, i);

                            break;

                        default:
                            break;

                    }

                    break;

                case 2:
                    // Col
                    switch (kei_SmartAuto.keiPlayerResource[i][1])
                    {
                        case 0:
                            // Fire

                            break;

                        case 1:
                            // Water

                            break;

                        case 2:
                            // Earth

                            break;

                        case 3:
                            // Light

                            break;

                        case 4:
                            // Dark

                            break;

                        default:
                            break;
                    }

                    break;

                default:
                    break;

            }

        }

    }

    //public void keiCheckNature()
    //{
    //    keiInitScript.keiNatureRules.GetComponent<keiNature>().keiTestNature();

    //}

}
