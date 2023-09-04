using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }

public class BattleSystem : MonoBehaviour
{
   [SerializeField] BattleUnit playerUnit;
   [SerializeField] BattleUnit enemyUnit;
   [SerializeField] BattleHUD playerHUD;
   [SerializeField] BattleHUD enemyHUD;
   [SerializeField] BattleDialogueBox dialogueBox;

   BattleState state;

   public void Start()
   {
        StartCoroutine(SetupBattle());
   }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        playerHUD.SetData(playerUnit.Pokemon);
        enemyUnit.Setup();
        enemyHUD.SetData(enemyUnit.Pokemon);

        yield return dialogueBox.TypeDialogue($"A wild {enemyUnit.Pokemon.Base.Name} appeared!");
        yield return new WaitForSeconds(1f);

        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogueBox.TypeDialogue($"What will {playerUnit.Pokemon.Base.Name} do?"));
        dialogueBox.EnableActionSelector(true);
        
    }
}
