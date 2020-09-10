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

    //private float inputDelay = 0f;

    private bool skip = false;

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

            for (int i = 0; i < mgSelections.Count; i++)
            {
                mgSelections[i].display = BattlePooler.ProduceObject(mgSelections[i].display, ab.Owner.transform);
            mgSelections[i].display.transform.position += new Vector3(0f,1f, 0f); 
                //mgSelections[i].sR = mgSelections[i].display.GetComponent<SpriteRenderer>();
                //mgSelections[i].sR.color = greyedOutColor;
            mgSelections[i].display.SetActive(false);
            }
        
        curSelection = 0;
        mgSelections[curSelection].display.SetActive(true);
        flipInterval = startFlipInterval;
        if(MultiInput.GetAButtonDown())
        {
            skip = true;
        }
    }


    public override void DoSubAbility(Ability ab)
    {
        if(skip == true)//TODO: automatically skip a frame between Starting an ability and the first run through so that this is not needed
        {
            skip = false;
            return;
        }
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

        if(MultiInput.GetAButtonDown())
        {
            ab.positionTargets.Add(mgSelections[curSelection].directionData);
            EndSubAbility();
        }
    }

}
 
