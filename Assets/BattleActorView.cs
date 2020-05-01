using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleActorView : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField]
    private GameObject popoutTextBoxPrefab;

    [SerializeField]
    private GameObject worldSpaceCanvas;

    [SerializeField]
    private Animator animator;
#pragma warning restore 649

    public void Start()
    {
        SetupBattleActorView();
    }

    private void SetupBattleActorView()
    {

    }

    public void ShowDamage(int damage)
    {
        GameObject explodingText = GameObject.Instantiate(popoutTextBoxPrefab, transform.position, Quaternion.identity, worldSpaceCanvas.transform);
        int damageAsInt = Mathf.RoundToInt(damage);
        explodingText.GetComponent<TextMeshProUGUI>().text = damageAsInt.ToString();
        Collider2D tempCollider = transform.GetChild(0).GetComponent<Collider2D>();

        explodingText.GetComponent<OnlyCollideWithOneThing>().thingToCollideWith = tempCollider;
        Vector2 force = new Vector2(Random.Range(-35f, 35f), 75f);
        force.Normalize();
        explodingText.GetComponent<Rigidbody2D>().AddForce(force * 250);
    }

    public void ShowBuff(Stats statBuff)
    {

    }

    public void ShowStatus(Status statusToShow)
    {
        string statusName = statusToShow.GetType().ToString();
        animator.SetBool(statusName,true);
    }

    public void StopShowStatus(Status statusToRemove)
    {
        string statusName = statusToRemove.GetType().ToString();
        animator.SetBool(statusName, false);
    }

    private void SetAnimationByStatus(Status statusToShow)
    {

    }
}
