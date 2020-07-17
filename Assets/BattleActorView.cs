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

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private delegate void DelOngoingDisplayBehavior();
    private DelOngoingDisplayBehavior OngoingDisplaybehavior;//TODO: if needed, make this a list of behaviors

#pragma warning restore 649

    public void Start()
    {
        SetupBattleActorView();
        OngoingDisplaybehavior = Nothing;//TODO: this is a cludge
    }

    private void SetupBattleActorView()
    {

    }

    public void Update()
    {
        OngoingDisplaybehavior();
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

    private float nextBlinkTime;
    private float blinkInterval = .25f;

    public void StartBlink()
    {
        nextBlinkTime = Time.time;
        OngoingDisplaybehavior = Blink;
    }

    private void Blink()
    {
        if(Time.time >= nextBlinkTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            nextBlinkTime = Time.time + blinkInterval;
            
        }
    }

    public void StopBlink()
    {
        spriteRenderer.enabled = true;
        OngoingDisplaybehavior = Nothing;
    }

    private void Nothing()//TODO: remove this cludge
    {

    }

}
