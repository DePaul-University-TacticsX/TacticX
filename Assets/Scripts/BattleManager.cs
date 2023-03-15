using TacticsX.GridImplementation;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class BattleManager
{
    private static BattleManager instance;

    private Participant player1;
    private Participant player2;

    GameObject[] disabledObjects;

    public static void StartBattle(Participant player1, Participant player2)
    {
        GetInstance().privStartBattle(player1, player2);
        Analytics.CustomEvent("Battle_Started");
    }

    public static void CompleteBattle(Participant loser)
    {
        GetInstance().privCompleteBattle(loser);
        Analytics.CustomEvent("Battle_Complete");
    }

    public static Participant GetPlayer1()
    {
        return GetInstance().player1;
    }

    public static Participant GetPlayer2()
    {
        return GetInstance().player2;
    }

    private void privStartBattle(Participant player1, Participant player2)
    {
        this.player1 = player1;
        this.player2 = player2;

        disabledObjects = GameObject.FindObjectsOfType<GameObject>();
        Debug.Log(disabledObjects.Length);
        for (int i = 0;i < disabledObjects.Length;i++)
        {
            string name = disabledObjects[i].name;

            if (name == "GameManager") continue;
            if (name == "Music") continue;
            if (name == "[DOTween]") continue;
            if (name == "~LeanTween") continue;

            disabledObjects[i].SetActive(false);
        }

        TacticsXGameManager.GetScenes().NextScene(Scenes.BattleFieldRealTime, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    private void privCompleteBattle(Participant loser)
    {
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();

        for (int i = 0; i < objects.Length; i++)
        {
            string name = objects[i].name;
            if (objects[i].activeInHierarchy == false) continue;
            if (name == "GameManager") continue;
            if (name == "Music") continue;
            if (name == "[DOTween]") continue;
            if (name == "~LeanTween") continue;

            GameObject.Destroy(objects[i]);
        }

        TacticsXGameManager.GetScenes().UnloadSceneAsync(Scenes.BattleFieldRealTime);

        //objects = GameObject.FindObjectsOfType<GameObject>();
        Debug.Log(disabledObjects.Length);
        for (int i = 0; i < disabledObjects.Length; i++)
        {
            disabledObjects[i].SetActive(true);
        }

        Debug.Log("DFD");

        TacticsX.GridImplementation.Grid.RemoveGamePiece(loser.GamePiece, true);
    }

    private static BattleManager GetInstance()
    {
        if (instance == null) instance = new BattleManager();
        return instance;
    }
}
