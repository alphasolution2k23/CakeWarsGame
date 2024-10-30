using EmeraldAI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ControlFreak2.TouchControl;

public class EnemyDealDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<WeaponType>(out WeaponType type))
        {
            switch (type.weaponType)
            {
                case WeaponTypeEnum.SmartPhone:
                    StartCoroutine(SmartPhoneAttack());
                    break;
                case WeaponTypeEnum.Roses:
                    StartCoroutine(RosesAttack());
                    break;
                case WeaponTypeEnum.PearlNecklace:
                    StartCoroutine(PearlNecklaceAttack());
                    break;
                case WeaponTypeEnum.ExplosiveCake:
                    StartCoroutine(ExplosiveCakeAttack());
                    break;
                case WeaponTypeEnum.FrostyCocktail:
                    StartCoroutine(FrostyCockrailAttack());
                    break;
                case WeaponTypeEnum.RcBlast:
                    StartCoroutine(RcBlastAttack());
                    break;
            }
        }
    }


    #region DealDamages
    private IEnumerator SmartPhoneAttack()
    {
        GetComponent<EmeraldAISystem>().EnableSmartPhone();

        yield return new WaitForSeconds(20f);
        GetComponent<EmeraldAISystem>().DisableSmartPhone();
    }
    private IEnumerator RosesAttack()
    {
        GetComponent<EmeraldAISystem>().DisableCombatState();
        GetComponent<EmeraldAISystem>().MakeEnemyFriendly();

        yield return new WaitForSeconds(7f);
        GetComponent<EmeraldAISystem>().MakeEnemyAggressive();
    }
    private IEnumerator PearlNecklaceAttack()
    {
        GetComponent<PearlLocator>().Pearl.SetActive(true);
        GetComponent<EmeraldAISystem>().CombatStateRef = EmeraldAISystem.CombatState.NotActive;
        GetComponent<EmeraldAISystem>().IsMoving = false;

        yield return new WaitForSeconds(10f);

        GetComponent<EmeraldAISystem>().CombatStateRef = EmeraldAISystem.CombatState.Active;
        GetComponent<EmeraldAISystem>().IsMoving = true;
        GetComponent<PearlLocator>().Pearl.SetActive(false);
        GetComponent<EmeraldAISystem>().BackToCombat();
    }
    private IEnumerator ExplosiveCakeAttack()
    {
        GetComponent<EmeraldAISystem>().Damage(10);
        yield break;
    }
    private IEnumerator FrostyCockrailAttack()
    {
        GetComponent<EmeraldAISystem>().EnableCocktale();
        yield return new WaitForSeconds(10f); 
        GetComponent<EmeraldAISystem>().BackToCombat();
    }
    private IEnumerator RcBlastAttack()
    {
        Debug.Log("Rc Car Blast Attack");
        GetComponent<EmeraldAISystem>().Damage(500);
        yield break;
    }

    #endregion
}
