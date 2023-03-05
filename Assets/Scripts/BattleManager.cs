using TacticsX.GridImplementation;

public class BattleManager
{
    private static BattleManager instance;

    private Participant player1;
    private Participant player2;

    public static void StartBattle(Participant player1, Participant player2)
    {
        GetInstance().privStartBattle(player1, player2);
    }

    public static void CompleteBattle(Participant winner)
    {
        GetInstance().privCompleteBattle(winner);
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
        TacticsXGameManager.GetScenes().NextScene(Scenes.BattleFieldRealTime);
    }

    private void privCompleteBattle(Participant loser)
    {
        TacticsXGameManager.GetScenes().NextScene(Scenes.GridDemo);
        Grid.RemoveGamePiece(loser.GamePiece, true);
    }

    private static BattleManager GetInstance()
    {
        if (instance == null) instance = new BattleManager();
        return instance;
    }
}
