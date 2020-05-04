using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "FlipBetweenMG", menuName = "SubAbility/MiniGames/FlipBetweenMG", order = 1)]
public class FlipBetweenMiniGame : SubAbility
{


    public Color selectedColor;
    public Color greyedOutColor;

    private int curSelection = 0;

    private float inputDelay = 0f;

    private bool skip = true;

    private float nextFlipTime = 0f;

    private float flipInterval;

    public float startFlipInterval;

    public float nextFlipIntervalMultiplyer;

    [System.Serializable]
    public class MGSeleciton
    {
        public Vector3 directionData;
        public GameObject display;
    }

    public List<MGSeleciton> mgSelections;

    public override void DoInitialSubAbility(Ability ab)
    {
        if (mgSelections.Count <= 0)
        {
            for (int i = 0; i < mgSelections.Count; i++)
            {
                mgSelections[i].display = Instantiate(mgSelections[i].display, ab.owner.transform);
                //mgSelections[i].sR = mgSelections[i].display.GetComponent<SpriteRenderer>();
                //mgSelections[i].sR.color = greyedOutColor;
                mgSelections[i].display.SetActive(false);
            }
        }
        curSelection = 0;
        mgSelections[curSelection].display.SetActive(true);
        flipInterval = startFlipInterval;
        
    }


    public override void DoSubAbility(Ability ab)
    {
        if(nextFlipTime < Time.time)
        {
            nextFlipTime = Time.time + flipInterval;

            flipInterval *= nextFlipIntervalMultiplyer;

            mgSelections[curSelection].display.SetActive(false);
                curSelection++;
                if (curSelection >= mgSelections.Count)
                {
                    curSelection = 0;
                }
            mgSelections[curSelection].display.SetActive(true);
        }

        if(Input.GetKeyDown("A"))
        {
            ab.positionTargets.Add(mgSelections[curSelection].directionData);
            EndSubAbility();
        }
    }

}
 
