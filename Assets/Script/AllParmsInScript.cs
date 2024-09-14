using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://docs.unity3d.com/ScriptReference/IMGUI.Controls.AdvancedDropdown.html")]
[RequireComponent(typeof(AllParmsInScript))]
public class AllParmsInScript : MonoBehaviour
{


    [Space(30)]
    [Header("Platform Generator")]
    [Space(5)]
    public PlatformGenerator platformscript;
    public List<GameObject> S_allplatforms = new List<GameObject>();
    public int[] S_values;
    public List<GameObject> S_breake_allplatforms = new List<GameObject>();
    public int[] S_breake_values;
    public GameObject S_hightPlatform;
    public int S_valueOfHightOlatform;
    [Range(0.0f, 100.0f)]
    public int S_minCountPlatform;
    [Range(0.0f, 100.0f)]
    public int S_maxCountPlatform;

    [Space(30)]
    [Header("Ground Generator")]
    [Space(5)]
    public GroundGenerator groundGeneratirScript;
    public GameObject S_Ground;
    public GameObject[] S_Abyss;

    [Space(30)]
    [Header("Coin Generator")]
    [Space(5)]
    public MonetkaGenerator monetkaGeneratorScript;
    public int S_totalCoinsMinValue = 30;
    public int S_totalCoinsMaxValue = 80;
    public float S_spawnMonetcaCD;

    void Start()
    {
        //Platforma Spawn
        platformscript.allPlatforms = S_allplatforms;
        platformscript.values = S_values;
        platformscript.breakePlatforms = S_breake_allplatforms;
        platformscript.breakeValues = S_breake_values;
        platformscript.hightPlatform = S_hightPlatform;
        platformscript.valueOfHightPlatform = S_valueOfHightOlatform;
        platformscript.minCountPlatform = S_minCountPlatform;
        platformscript.maxCountPlatform = S_maxCountPlatform;

        //Ground Generator
        //groundGeneratirScript.ground = S_Ground;
        //groundGeneratirScript.abyss = S_Abyss;


        //Monetka Generator
        monetkaGeneratorScript.totalCoinsMinValue = S_totalCoinsMinValue;
        monetkaGeneratorScript.totalCoinsMaxValue = S_totalCoinsMaxValue;
        monetkaGeneratorScript.spawnMonetcaCD = S_spawnMonetcaCD;
    }
}
