/// <summary>
/// keiInitializatorGameObject V 1.0 Kei Lazu
///
/// Desc: Initialize Gameobject here, so no need to initialize in other script
/// </summary>

using UnityEngine;
using UnityEngine.UI;

public class keiInitializatorGameObject : MonoBehaviour
{
    // Start Region: Init -------------------------------
    // Region: Pathfinder
    public GameObject keiLogSystem;
    public GameObject keiWaveControlSystem;
    public GameObject keiEnemyPos;
    public GameObject keiPlayerResource;
    public GameObject keiNatureRules;

    // Region: Image
    public Image keiEnemyA1, keiEnemyA2, keiEnemyA3,
        keiEnemyB1, keiEnemyB2, keiEnemyB3,
        keiEnemyC1, keiEnemyC2, keiEnemyC3; // Enemy Coords

    // Region: Text
    public Text keiLblLogState, keiLblLogAttack, keiLblLogSlot, keiLblLogCoords; // Logging State
    public Text keiLblLogEnemyCount, keiLblLogWave;

    public Text keiEnemyCDA1, keiEnemyCDA2, keiEnemyCDA3,
        keiEnemyCDB1, keiEnemyCDB2, keiEnemyCDB3,
        keiEnemyCDC1, keiEnemyCDC2, keiEnemyCDC3;

    // Region: Dropdown
    public Dropdown keiDropResTypeSlot1, keiDropResTypeSlot2, keiDropResTypeSlot3, keiDropResTypeSlot4, keiDropResTypeSlot5,
        keiDropResElemSlot1, keiDropResElemSlot2, keiDropResElemSlot3, keiDropResElemSlot4, keiDropResElemSlot5;

    // Initialization
    private void Awake()
    {
        // Region: Pathfinder Init
        keiLogSystem = GameObject.FindGameObjectWithTag("keiLogSystem");
        keiEnemyPos = GameObject.FindGameObjectWithTag("keiEnemyPos");
        keiPlayerResource = GameObject.FindGameObjectWithTag("keiPlayerResource");
        keiWaveControlSystem = GameObject.Find("keiWaveControlScript");
        keiNatureRules = GameObject.Find("keiNatureRules");

        // Region: Logging
        keiInitLogging(keiLogSystem);

        keiInitEnemy(keiEnemyPos);
        keiInitPlayerResource(keiPlayerResource);
    }

    private void keiInitLogging(GameObject kei_LogSystem)
    {
        keiLblLogState = kei_LogSystem.transform.Find("keiLblLogState").GetComponent<Text>();
        keiLblLogAttack = kei_LogSystem.transform.Find("keiLblLogAttack").GetComponent<Text>();
        keiLblLogSlot = kei_LogSystem.transform.Find("keiLblLogSlot").GetComponent<Text>();
        keiLblLogCoords = kei_LogSystem.transform.Find("keiLblLogCoords").GetComponent<Text>();
        keiLblLogEnemyCount = kei_LogSystem.transform.Find("keiLblLogEnemyCounter").GetComponent<Text>();
        keiLblLogWave = kei_LogSystem.transform.Find("keiLblLogWave").GetComponent<Text>();

    }

    private void keiInitEnemy(GameObject kei_EnemyPos)
    {
        keiEnemyA1 = kei_EnemyPos.transform.Find("keiEnemyA1").GetComponent<Image>();
        keiEnemyA2 = kei_EnemyPos.transform.Find("keiEnemyA2").GetComponent<Image>();
        keiEnemyA3 = kei_EnemyPos.transform.Find("keiEnemyA3").GetComponent<Image>();

        keiEnemyB1 = kei_EnemyPos.transform.Find("keiEnemyB1").GetComponent<Image>();
        keiEnemyB2 = kei_EnemyPos.transform.Find("keiEnemyB2").GetComponent<Image>();
        keiEnemyB3 = kei_EnemyPos.transform.Find("keiEnemyB3").GetComponent<Image>();

        keiEnemyC1 = kei_EnemyPos.transform.Find("keiEnemyC1").GetComponent<Image>();
        keiEnemyC2 = kei_EnemyPos.transform.Find("keiEnemyC2").GetComponent<Image>();
        keiEnemyC3 = kei_EnemyPos.transform.Find("keiEnemyC3").GetComponent<Image>();

        keiInitEnemyCD();
    }

    private void keiInitEnemyCD()
    {
        keiEnemyCDA1 = keiEnemyA1.gameObject.transform.GetChild(0).GetComponent<Text>();
        keiEnemyCDA2 = keiEnemyA2.gameObject.transform.GetChild(0).GetComponent<Text>();
        keiEnemyCDA3 = keiEnemyA3.gameObject.transform.GetChild(0).GetComponent<Text>();

        keiEnemyCDB1 = keiEnemyB1.gameObject.transform.GetChild(0).GetComponent<Text>();
        keiEnemyCDB2 = keiEnemyB2.gameObject.transform.GetChild(0).GetComponent<Text>();
        keiEnemyCDB3 = keiEnemyB3.gameObject.transform.GetChild(0).GetComponent<Text>();

        keiEnemyCDC1 = keiEnemyC1.gameObject.transform.GetChild(0).GetComponent<Text>();
        keiEnemyCDC2 = keiEnemyC2.gameObject.transform.GetChild(0).GetComponent<Text>();
        keiEnemyCDC3 = keiEnemyC3.gameObject.transform.GetChild(0).GetComponent<Text>();
    }

    private void keiInitPlayerResource(GameObject kei_PlayerResource)
    {
        keiDropResTypeSlot1 = kei_PlayerResource.transform.GetChild(0).transform.Find("keiDropPlayerAttTypeSlot1").GetComponent<Dropdown>();
        keiDropResTypeSlot2 = kei_PlayerResource.transform.GetChild(0).transform.Find("keiDropPlayerAttTypeSlot2").GetComponent<Dropdown>();
        keiDropResTypeSlot3 = kei_PlayerResource.transform.GetChild(0).transform.Find("keiDropPlayerAttTypeSlot3").GetComponent<Dropdown>();
        keiDropResTypeSlot4 = kei_PlayerResource.transform.GetChild(0).transform.Find("keiDropPlayerAttTypeSlot4").GetComponent<Dropdown>();
        keiDropResTypeSlot5 = kei_PlayerResource.transform.GetChild(0).transform.Find("keiDropPlayerAttTypeSlot5").GetComponent<Dropdown>();

        keiDropResElemSlot1 = kei_PlayerResource.transform.GetChild(1).transform.Find("keiDropPlayerAttElemSlot1").GetComponent<Dropdown>();
        keiDropResElemSlot2 = kei_PlayerResource.transform.GetChild(1).transform.Find("keiDropPlayerAttElemSlot2").GetComponent<Dropdown>();
        keiDropResElemSlot3 = kei_PlayerResource.transform.GetChild(1).transform.Find("keiDropPlayerAttElemSlot3").GetComponent<Dropdown>();
        keiDropResElemSlot4 = kei_PlayerResource.transform.GetChild(1).transform.Find("keiDropPlayerAttElemSlot4").GetComponent<Dropdown>();
        keiDropResElemSlot5 = kei_PlayerResource.transform.GetChild(1).transform.Find("keiDropPlayerAttElemSlot5").GetComponent<Dropdown>();
    }

    // End Region: Init ---------------------------------
}