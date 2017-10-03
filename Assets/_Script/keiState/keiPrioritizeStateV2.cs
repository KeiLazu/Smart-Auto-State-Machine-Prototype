/// <summary>
/// keiPrioritizeStateV2 V 2.0
/// Kei Lazu
/// 
/// Desc:
/// Prioritizing redefined
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

    // Region: Judgement
    int keiTotalColor;
    int keiSelectColor;

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

    // Phase 1.1: Primary Position
    public void keiPriorityPos(keiSmartAutoController kei_SmartAuto)
    {
        for (int keiCol = 0; keiCol < 3; keiCol++)
        {
            for (int keiRow = 0; keiRow < 3; keiRow++)
            {
                // TODO: get First Priority, Second Priority, and Third Priority
                Debug.Log("Type: " + kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][0] + "\nTurn: " + kei_SmartAuto.keiEnemyIntel[keiCol][keiRow][1] + "\nCol: " + keiCol + "\nRow: " + keiRow);

            }

        }

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
                    keiCheckEnemyColor(kei_SmartAuto);

                    int keiBenefitParam = new int();

                    keiBenefitParam = keiManyEnemyColor(keiEnemyTotalRed,
                                                            keiEnemyTotalBlue,
                                                            keiEnemyTotalGreen,
                                                            keiEnemyTotalYellow,
                                                            keiEnemyTotalMagenta,
                                                            kei_SmartAuto);

                    // Red(0) <- Blue(1) <- Green(2) <- Red(0)

                    switch (keiBenefitParam)
                    {
                        case 1:
                            if (kei_SmartAuto.keiPlayerResource[i][1] != 2 && keiTotalColor > 4)
                            {
                                // suggest

                            }
                            break;

                        case 2:
                            if (kei_SmartAuto.keiPlayerResource[i][1] != 0 && keiTotalColor > 4)
                            {
                                // suggest

                            }
                            break;

                        case 3:
                            if (kei_SmartAuto.keiPlayerResource[i][1] != 1 && keiTotalColor > 4)
                            {
                                // suggest

                            }
                            break;

                        case 4:
                            // suggest
                            break;

                        case 5:
                            // suggest
                            break;

                    }

                    break;

                default:
                    break;

            }

        }

        if (kei_SmartAuto.keiDecisionAttack == 9)
        {
            keiCheckColRow(kei_SmartAuto);

        }
        else
        {
            // TODO: finish, and attack

        }

    }

    // Phase 3: Check Col and Row
    public void keiCheckColRow(keiSmartAutoController kei_SmartAuto)
    {
        // TODO: Check Col and Row, if any compare element, if none run to single

    }

    //public void keiCheckNature()
    //{
    //    keiInitScript.keiNatureRules.GetComponent<keiNature>().keiTestNature();

    //}

}
