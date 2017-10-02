///// <summary>
///// keiPrioritizeState V 1.2
///// Kei Lazu
/////
///// Desc: Prioritizing what coords need to be attacked first with resource we give Output will be
///// given in Exit Prioritize State
/////
///// - Third State
///// 
///// Changelog:
///// 1.2 : Merit-Demerit System for AoE and X was implemented
///// 1.2 : (Bug Removed) Stopped or Skipped after first attack
///// 1.1 : Spatial Intelligence has been added
///// 1.1 : Almost optimal in selecting slot for attacking
///// 
///// TODO:
///// - Optimalization
///// - Basic Priority + Element
///// - Use other resource if suggestion is invalid
///// 
///// </summary>

//using UnityEngine;
//using UnityEngine.UI;
//using keiStateControlStuff;

//public class keiPrioritizeState : keiState<keiSmartAutoController>
//{
//    private static keiPrioritizeState keiInstance;

//    private keiPrioritizeState()
//    {
//        if (keiInstance != null)
//        {
//            return;
//        }

//        keiInstance = this;
//    }

//    public static keiPrioritizeState kei_getsetInstance
//    {
//        get
//        {
//            if (keiInstance == null)
//            {
//                new keiPrioritizeState();
//            }

//            return keiInstance;
//        }
//    }

//    // Start Region: Init ---------------------------------------------

//    // Region: Pathfinder
//    private keiInitializatorGameObject keiInitScript;

//    // Region: Spatial Intelligence
//    int[][] keiSpatialIntelEnemyRow = new int[3][];
//    int[][] keiSpatialIntelEnemyCD = new int[3][];

//    // Region: Intel for Decisioning
//    int keiRowAMinimumCD, keiRowBMinimumCD, keiRowCMinimumCD; // Holder to minimum value for selecting primary row
//    int keiRowAMinimumCDCol, keiRowBMinimumCDCol, keiRowCMinimumCDCol; // selecting candidate primary col
//    int keiPriorityRow; // selecting primary row
//    int keiPriorityCol; // selecting primary col
//    int keiPrioritySlot; // selecting primary attack slot

//    // Region: Merit Demerit System
//    int keiMeritAoE, keiMeritX, keiDemeritAoE, keiDemeritX;
//    int keiMeritRow, keiMeritCol, keiDemeritRow, keiDemeritCol;

//    // End Region: Init -----------------------------------------------

//    public override void keiEnterState(keiSmartAutoController kei_Owner)
//    {
//        kei_Owner.keiIsFinished = false;

//        keiMeritAoE = 0;
//        keiMeritX = 0;
//        keiDemeritAoE = 0;
//        keiDemeritX = 0;

//        keiInitScript = GameObject.Find("keiInitScript").GetComponent<keiInitializatorGameObject>();

//        keiInitScript.keiLblLogState.GetComponent<Text>().text = "State: Prioritizing";

//        keiPrioritizePosition(kei_Owner);

//    }

//    public override void keiExitState(keiSmartAutoController kei_Owner)
//    {
//        kei_Owner.keiIsFinished = false;
//    }

//    public override void keiUpdateState(keiSmartAutoController kei_Owner)
//    {
//        if (kei_Owner.keiIsFinished)
//        {
//            kei_Owner.keiStateMachine.keiChangeState(keiAttackState.kei_getsetInstance);
//        }
//    }

//    public void keiPrioritizePosition(keiSmartAutoController kei_SmartAuto)
//    {
//        keiSpatialIntelEnemyRow[0] = new int[3];
//        keiSpatialIntelEnemyRow[1] = new int[3];
//        keiSpatialIntelEnemyRow[2] = new int[3];

//        keiMeritAoE = 0;
//        keiMeritX = 0;
//        keiDemeritAoE = 0;
//        keiDemeritX = 0;

//        // Region: Positioning
//        for (int Col = 0; Col < 3; Col++)
//        {
//            for (int Row = 0; Row < 3; Row++)
//            {
//                // first row
//                keiSpatialIntelEnemyRow[Col][Row] = kei_SmartAuto.keiEnemyElemIntel[Row + (3 * Col)];

//                // Merit Demerit System
//                keiMeritDemeritSystemAoE(Row, Col, kei_SmartAuto);

//            }

//        }

//        // output: (for easier reading)
//        //
//        // Col 0 Col 1 Col 2
//        //  []    []    []  - Row 0: A
//        //  []    []    []  - Row 1: B
//        //  []    []    []  - Row 2: C
//        //

//        keiMeritDemeritSystemX(kei_SmartAuto);
//        keiPrioritizeCountDown(kei_SmartAuto);
//        //Debug.Log("Merit AoE: " + keiMeritAoE + " || Demerit AoE: " + keiDemeritAoE);
//        //Debug.Log("Merit X: " + keiMeritX + " || Demerit X: " + keiDemeritX);

//        // code below is deprecated
//        //for (int i = 0; i < 3; i++)
//        //{
//        //    // first row
//        //    keiSpatialIntelEnemyRow[0][i] = kei_SmartAuto.keiEnemyElemIntel[i];
//        //    //Debug.Log("Row: A-" + keiSpatialIntelEnemyPos[0][i]);

//        //    // and then second row
//        //    keiSpatialIntelEnemyRow[1][i] = kei_SmartAuto.keiEnemyElemIntel[i + 3];
//        //    //Debug.Log("Row: B-" + keiSpatialIntelEnemyPos[1][i]);

//        //    // and then the last row
//        //    keiSpatialIntelEnemyRow[2][i] = kei_SmartAuto.keiEnemyElemIntel[i + 6];
//        //    //Debug.Log("Row: C-" + keiSpatialIntelEnemyPos[2][i]);

//        //}

//    }

//    public int keiPositionCountdownRow(int kei_RowA, int kei_RowB, int kei_RowC)
//    {
//        // return 0 = Row A
//        // return 1 = Row B
//        // return 2 = Row C

//        // return the position of row, not the nominal
//        if (kei_RowA <= kei_RowB && kei_RowA <= kei_RowC)
//        {
//            return 0;

//        }
//        else
//        {
//            if (kei_RowB <= kei_RowC)
//            {
//                return 1;

//            }
//            else
//            {
//                return 2;

//            }

//        }

//    }

//    public int keiPositionCountdownCol(int kei_Col0, int kei_Col1, int kei_Col2, int kei_PriorityRow)
//    {
//        // return 0 = Col 0
//        // return 1 = Col 1
//        // return 2 = Col 2

//        switch (kei_PriorityRow)
//        {
//            case 0:
//                return kei_Col0;

//            case 1:
//                return kei_Col1;

//            case 2:
//                return kei_Col2;

//            default:
//                return kei_Col0;
//        }

//    }

//    public void keiPrioritizeCountDown(keiSmartAutoController kei_SmartAuto)
//    {
//        keiSpatialIntelEnemyCD[0] = new int[3];
//        keiSpatialIntelEnemyCD[1] = new int[3];
//        keiSpatialIntelEnemyCD[2] = new int[3];
        
//        keiRowAMinimumCD = 9;
//        keiRowBMinimumCD = 9;
//        keiRowCMinimumCD = 9;

//        // Region: Priority
//        for (int i = 0; i < 3; i++)
//        {
//            // First Row
//            keiSpatialIntelEnemyCD[0][i] = kei_SmartAuto.keiEnemyCountDownIntel[i];
//            if (keiSpatialIntelEnemyCD[0][i] > 0 && keiSpatialIntelEnemyCD[0][i] < keiRowAMinimumCD)
//            {
//                keiRowAMinimumCD = keiSpatialIntelEnemyCD[0][i];
//                keiRowAMinimumCDCol = i;
//                //Debug.Log("A: " + keiRowAMinimumCDCol);

//            }

//            // Second Row
//            keiSpatialIntelEnemyCD[1][i] = kei_SmartAuto.keiEnemyCountDownIntel[i + 3];
//            if (keiSpatialIntelEnemyCD[1][i] > 0 && keiSpatialIntelEnemyCD[1][i] < keiRowBMinimumCD)
//            {
//                keiRowBMinimumCD = keiSpatialIntelEnemyCD[1][i];
//                keiRowBMinimumCDCol = i;
//                //Debug.Log("B: " + keiRowBMinimumCDCol);

//            }

//            // Third Row
//            keiSpatialIntelEnemyCD[2][i] = kei_SmartAuto.keiEnemyCountDownIntel[i + 6];
//            if (keiSpatialIntelEnemyCD[2][i] > 0 && keiSpatialIntelEnemyCD[2][i] < keiRowCMinimumCD)
//            {
//                keiRowCMinimumCD = keiSpatialIntelEnemyCD[2][i];
//                keiRowCMinimumCDCol = i;
//                //Debug.Log("C: " + keiRowCMinimumCDCol);

//            }

//            // output: (for easier reading)
//            //
//            // Col 0 Col 1 Col 2
//            //  []    []    []  - Row 0: A
//            //  []    []    []  - Row 1: B
//            //  []    []    []  - Row 2: C
//            //
            
//        }

//        keiPriorityRow = keiPositionCountdownRow(keiRowAMinimumCD, keiRowBMinimumCD, keiRowCMinimumCD);
//        keiPriorityCol = keiPositionCountdownCol(keiRowAMinimumCDCol, keiRowBMinimumCDCol, keiRowCMinimumCDCol, keiPriorityRow);
//        //Debug.Log("Priority Row: " + keiPriorityRow + " || Priority Col: " + keiPriorityCol); // this line confirms that the minimal countdown from enemy will be prioritized
//        keiCheckNeighborFromPrimaryPriority(keiPriorityRow, keiPriorityCol, kei_SmartAuto);

//    }

//    public void keiMeritDemeritSystemX(keiSmartAutoController kei_SmartAuto)
//    {
//        for (int i = 0; i < kei_SmartAuto.keiEnemyElemIntel.Length; i += 2)
//        {
//            if (kei_SmartAuto.keiEnemyElemIntel[i] > 0)
//            {
//                keiMeritX++;

//            }
//            else
//            {
//                keiDemeritX++;

//            }

//        }

//    }

//    public void keiMeritDemeritSystemAoE(int kei_Row, int kei_Col, keiSmartAutoController kei_SmartAuto)
//    {
//        // Check merit AoE
//        if (kei_SmartAuto.keiEnemyElemIntel[kei_Row + (3 * kei_Col)] > 0)
//        {
//            keiMeritAoE++;

//        }
//        else
//        {
//            keiDemeritAoE++;

//        }

//    }

//    public void keiAoEXSuggestion(int kei_Row, int kei_Col, keiSmartAutoController kei_SmartAuto)
//    {
//        //Debug.Log("AoEX Active");
//        //if (((kei_Row == 0 || kei_Row == 2) && (kei_Col == 0 || kei_Col == 2)) || (kei_Row == 1 && kei_Col == 1))

//        for (int i = 0; i < kei_SmartAuto.keiPlayerTypeResource.Length; i++)
//        {
//            if (kei_SmartAuto.keiPlayerTypeResource[i] == 4 && kei_SmartAuto.keiDecisionAttack == 9)
//            {
//                if (keiMeritAoE > keiDemeritAoE)
//                {
//                    Debug.Log("AoE In");
//                    kei_SmartAuto.keiDecisionAttack = 4;
//                    kei_SmartAuto.keiDecisionAttackSlot = i;
//                    keiPrepareAttack(kei_SmartAuto);
//                    break;

//                }
//                else
//                {
//                    break;

//                }

//            }
//            else
//            {
//                continue;

//            }

//        }

//        //if (keiMeritAoE > keiDemeritAoE)
//        //{
//        //    //Debug.Log("Suggest X or AoE");
//        //    for (int i = 0; i < kei_SmartAuto.keiPlayerTypeResource.Length; i++)
//        //    {

//        //        if (kei_SmartAuto.keiPlayerTypeResource[i] == 4 && kei_SmartAuto.keiDecisionAttack == 9)
//        //        {
//        //            kei_SmartAuto.keiDecisionAttack = 4;
//        //            kei_SmartAuto.keiDecisionAttackSlot = i;
//        //            keiPrepareAttack(kei_SmartAuto);
//        //            //Debug.Log("Suggest AoE");
//        //            break;

//        //        }
//        //        //else if (kei_SmartAuto.keiPlayerTypeResource[i] == 3 && kei_SmartAuto.keiDecisionAttack == 9)
//        //        //{
//        //        //    kei_SmartAuto.keiDecisionAttack = 3;
//        //        //    kei_SmartAuto.keiDecisionAttackSlot = i;
//        //        //    keiPrepareAttack(kei_SmartAuto);
//        //        //    //Debug.Log("Suggest X");
//        //        //    break;

//        //        //}

//        //        Debug.Log("For AoEX Active");

//        //    }

//        //    Debug.Log("If AoEX Active");

//        //}

//        if (((kei_Row == 0 || kei_Row == 2) || (kei_Col == 0 || kei_Col == 2)) || (kei_Row == 1 && kei_Col == 1))
//        {
//            for (int i = 0; i < kei_SmartAuto.keiPlayerTypeResource.Length; i++)
//            {
//                if (kei_SmartAuto.keiPlayerTypeResource[i] == 3 && kei_SmartAuto.keiDecisionAttack == 9)
//                {
//                    if (keiMeritX > keiDemeritX)
//                    {
//                        kei_SmartAuto.keiDecisionAttack = 3;
//                        kei_SmartAuto.keiDecisionAttackSlot = i;
//                        keiPrepareAttack(kei_SmartAuto);
//                        Debug.Log("Suggest X");
//                        break;

//                    }
//                    else
//                    {
//                        continue;

//                    }

//                }

//            }

//        }

//        //if ((keiMeritX > keiDemeritX) && ((kei_Row == 0 || kei_Row == 2) && (kei_Col == 0 || kei_Col == 2)) || (kei_Row == 1 && kei_Col == 1))
//        //{
//        //    Debug.Log("Else If X Active");

//        //    for (int i = 0; i < kei_SmartAuto.keiPlayerTypeResource.Length; i++)
//        //    {
//        //        if (kei_SmartAuto.keiPlayerTypeResource[i] == 3 && kei_SmartAuto.keiDecisionAttack == 9)
//        //        {
//        //            kei_SmartAuto.keiDecisionAttack = 3;
//        //            kei_SmartAuto.keiDecisionAttackSlot = i;
//        //            keiPrepareAttack(kei_SmartAuto);
//        //            Debug.Log("Suggest X");
//        //            break;

//        //        }
//        //        else { continue; }

//        //    }

//        //}
        
//        if (kei_SmartAuto.keiDecisionAttack == 9)
//        {
//            Debug.Log("Run to RowColSuggestion");
//            keiRowColSuggestion(kei_Row, kei_Col, kei_SmartAuto);

//        }

//    }

//    public void keiRowColSuggestion(int kei_Row, int kei_Col, keiSmartAutoController kei_SmartAuto)
//    {
//        keiMeritCol = 0;
//        keiMeritRow = 0;
//        keiDemeritCol = 0;
//        keiDemeritRow = 0;

//        //Debug.Log("keiRowColSuggestion Active");
//        for (int i = 0; i < keiSpatialIntelEnemyRow.Length; i++)
//        {
//            //Debug.Log("keiRowCol For Active");
//            if (keiSpatialIntelEnemyRow[i][kei_Col] > 0)
//            {
//                keiMeritCol++;

//            }
//            else
//            {
//                keiDemeritCol++;

//            }

//            if (keiSpatialIntelEnemyRow[kei_Row][i] > 0)
//            {
//                keiMeritRow++;

//            }
//            else
//            {
//                keiDemeritRow++;

//            }

//        }

//        //Debug.Log("Col Sum: " + keiPriorityColSum + " || Row Sum: " + keiPriorityRowSum);

//        if (keiMeritCol > keiDemeritCol && kei_SmartAuto.keiDecisionAttack == 9)
//        {
//            //Debug.Log("If Priority Col Active");
//            for (int i = 0; i < kei_SmartAuto.keiPlayerTypeResource.Length; i++)
//            {
//                //Debug.Log("For in If Priority Col Active");

//                if (kei_SmartAuto.keiPlayerTypeResource[i] == 2)
//                {
//                    //Debug.Log("Suggest Col Attack");
//                    kei_SmartAuto.keiDecisionAttack = 2;
//                    kei_SmartAuto.keiDecisionAttackSlot = i;
//                    keiPrepareAttack(kei_SmartAuto);
//                    break;

//                }
//                //else
//                //{
//                //    Debug.Log("Continue For");
//                //    continue;
//                //}

//            }

//        }

//        if (keiMeritRow > keiDemeritRow && kei_SmartAuto.keiDecisionAttack == 9)
//        {
//            //Debug.Log("Else if Priority Row Sum");
//            for (int i = 0; i < kei_SmartAuto.keiPlayerTypeResource.Length; i++)
//            {
//                if (kei_SmartAuto.keiPlayerTypeResource[i] == 1)
//                {
//                    //Debug.Log("Suggest Row Attack");
//                    kei_SmartAuto.keiDecisionAttack = 1;
//                    kei_SmartAuto.keiDecisionAttackSlot = i;
//                    keiPrepareAttack(kei_SmartAuto);
//                    break;

//                }

//            }

//        }

//        if (kei_SmartAuto.keiDecisionAttack == 9)
//        {
//            //Debug.Log("Else Run To Single");
//            keiSingleSuggestion(kei_SmartAuto);
//        }

//    }

//    public void keiSingleSuggestion(keiSmartAutoController kei_SmartAuto)
//    {
//        //Debug.Log("keiSingle Active");

//        for (int i = 0; i < kei_SmartAuto.keiPlayerTypeResource.Length; i++)
//        {
//            //Debug.Log("For Single Active");

//            if (kei_SmartAuto.keiPlayerTypeResource[i] == 0 && kei_SmartAuto.keiDecisionAttack == 9)
//            {
//                //Debug.Log("Suggest Single Attack");
//                kei_SmartAuto.keiDecisionAttack = 0;
//                kei_SmartAuto.keiDecisionAttackSlot = i;
//                break;

//            }

//            //Debug.Log(kei_SmartAuto.keiDecisionAttack + " " + kei_SmartAuto.keiDecisionAttackSlot);

//        }

//        if (kei_SmartAuto.keiDecisionAttack == 9)
//        {

//            // not attacking
//            Debug.Log("Not Attacking");
//            kei_SmartAuto.keiDecisionAttack = 9;

//        }

//        keiPrepareAttack(kei_SmartAuto);

//    }

//    public void keiCheckNeighborFromPrimaryPriority(int kei_Row, int kei_Col, keiSmartAutoController kei_SmartAuto)
//    {
//        // AoE need Merit/Demerit System, so i'll lump this with X for this time being
//        //Debug.Log("keiCheckNeighbor Active || Row: " + kei_Row + " || Col: " + kei_Col);
//        keiAoEXSuggestion(kei_Row, kei_Col, kei_SmartAuto);

//    }

//    public void keiPrepareAttack(keiSmartAutoController kei_SmartAuto)
//    {
//        //Debug.Log("Preparing for attacking");
//        kei_SmartAuto.keiDecisionAttackRow = keiPriorityRow;
//        kei_SmartAuto.keiDecisionAttackCol = keiPriorityCol;
//        kei_SmartAuto.keiIsFinished = true;
//        kei_SmartAuto.keiChangingState();

//    }

//}