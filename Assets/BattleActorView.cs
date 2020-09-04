using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Animations;
using UnityEngine.UI;

public enum BuffStyle
{
    None = 0,
    Positive = 1,
    Negative = 2,
    Warning = 3,

}
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
    private Animator pcAnimator;

    [SerializeField]
    private List<Animator> oneTimeEffectAnimators;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private float stopBlinkTime;

    private float blinkDuration = 2f;

    private delegate void DelOngoingDisplayBehavior();
    private DelOngoingDisplayBehavior OngoingDisplaybehavior;//TODO: if needed, make this a list of behaviors

    public BattlePooler battlePooler;

    [SerializeField]
    private Image healthBar;

#pragma warning restore 649

    public void Start()
    {
        SetupBattleActorView();
        OngoingDisplaybehavior = Nothing;//TODO: this is a cludge
    }

    private void SetupBattleActorView()
    {
        worldSpaceCanvas = GameObject.Find("WorldSpaceUI");
    }

    public void Update()
    {
        OngoingDisplaybehavior();
    }
    public void ShowDamage(int damage)
    {
        int damageAsInt = Mathf.RoundToInt(damage);
        ExplodeText(damageAsInt.ToString(), Color.red);
        if (pcAnimator != null) //TODO: change all of these to account for pcs vs enemies without using if statements like this.
        {
            pcAnimator.Play("freak", 0, 0f);
        }
    }

    public void ShowHeal(int healing)
    {
        ExplodeText(healing.ToString(), Color.green);
    }

    public void ShowBuff(int buffAmount, string textDesignation, BuffStyle buffStyle)
    {
        string buffText = buffAmount.ToString() + "\n" + textDesignation;
        Color textColor = ReturnColorFromBuffStyle(buffStyle);
        ExplodeText(buffText, textColor);
    }

    private void ExplodeText(string text, Color color)
    {
        Debug.Log(popoutTextBoxPrefab.name);
        GameObject explodingText = battlePooler.ProduceObject(popoutTextBoxPrefab, transform.position, Quaternion.identity, transform);

        Debug.Log(explodingText.name);

        TextMeshPro textMesh = explodingText.GetComponent<TextMeshPro>();
        textMesh.text = text;
        textMesh.color = color;
        textMesh.faceColor = color;

    }

    private Color ReturnColorFromBuffStyle(BuffStyle buffStyle)
    {
        switch (buffStyle)
        {
            case BuffStyle.Positive:
                return Color.green;
              //  break;
            case BuffStyle.Negative:
                return Color.yellow;
             //   break;
            case BuffStyle.Warning:
                return Color.white;
             //   break;
        }
        return Color.gray;
    }

    public void UpdateHealthBar(int curHealth, int maxHealth)
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)curHealth / maxHealth;
        }
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

    public void ShowOneTimeEffect(AnimatorOverrideController oneTimeAnimatorController)
    {
        oneTimeEffectAnimators[0].runtimeAnimatorController = oneTimeAnimatorController;

        oneTimeEffectAnimators[0].Play("OneTimeEffect", 0, 0f);
        //oneTimeEffectAnimators[0].SetBool("OneTimeEffect", true);
        //make the game instantiate more oneTimeEffectAnimators if there are not enough here rather than interupting animations in progress

    }

    public void ShowOneTimeCast(AnimatorOverrideController oneTimeAnimatorController)
    {
        oneTimeEffectAnimators[0].runtimeAnimatorController = oneTimeAnimatorController;
       

        oneTimeEffectAnimators[0].Play("OneTimeCast", 0, 0f);
        //oneTimeEffectAnimators[0].SetBool("OneTimeEffect", true);
        //make the game instantiate more oneTimeEffectAnimators if there are not enough here rather than interupting animations in progress

    }

    public void PlayInteruptingCharacterAnimation(string stateName)
    {
        pcAnimator.Play("down", 0, 0f);
    }

    private string lastAnimSet = "stand";
    public void PlayCharacterAnimation(string stateName)
    {
        pcAnimator.CrossFade(stateName, 0f);
        pcAnimator.SetBool(lastAnimSet, false);
        pcAnimator.SetBool(stateName, true);
        lastAnimSet = stateName;
    }

    public void PlayCharacterAnimationAtSpeed(string stateName, float speed)
    {
        PlayCharacterAnimation(stateName);
        pcAnimator.speed = speed;
        OngoingDisplaybehavior += SetSpeedToDefaultOnAnimDone;
    }

    private void SetSpeedToDefaultOnAnimDone()
    {
        if(pcAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= .99f)
        {
            pcAnimator.speed = 1f;
            OngoingDisplaybehavior -= SetSpeedToDefaultOnAnimDone;
        }
    }

    private float nextBlinkTime;
    private float blinkInterval = .18f;

    public void StartBlink()
    {
        nextBlinkTime = Time.time;
        stopBlinkTime = Time.time + blinkDuration;
        OngoingDisplaybehavior += Blink;
    }

    private void Blink()
    {

        if (Time.time >= stopBlinkTime)
        {
            StopBlink();
            return;
        }
        if (Time.time >= nextBlinkTime)
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

    private float nextFlashTime = 0;

    private Color flashColor = new Color(255f, 191f, 81f);

    private float flashInterval;

    private int timesToFlash;

    private int flashes;

    private float flashDuration = .15f;

    private float nextFlashDuration =0f;

    private bool turnOnFlash = true;
    public void StartFlash(int times = 2, float interval = .2f)
    {
        OngoingDisplaybehavior += Flash;
        nextFlashTime = 0f;
        flashes = 0;
        timesToFlash = times;
        flashInterval = interval;
        Flash();
    }
    private void Flash() //ToDo: Implement timer and redo this convolution
    {
        if(Time.time >= nextFlashTime)
        {
            if (turnOnFlash == true)
            {
                spriteRenderer.color = flashColor;
                nextFlashDuration = Time.time + flashDuration;
                turnOnFlash = false;
            }

            if (Time.time >= nextFlashDuration)
            {
                spriteRenderer.color = Color.white;
                nextFlashTime = Time.time + flashInterval;
                flashes++;
                turnOnFlash = true;
            }

            if (flashes >= timesToFlash)
            {
                spriteRenderer.color = Color.white;
                StopFlash();
            }
        }

    }

    private void StopFlash()
    {
        OngoingDisplaybehavior = Nothing;
    }

    private void Nothing()//TODO: remove this cludge
    {

    }

}
