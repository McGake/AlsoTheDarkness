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

    public List<MGSeleciton> inspectorSelections;

    private List<MGSeleciton> mgSelections = new List<MGSeleciton>();

    public void Awake()
    {
        for (int i = 0; i < inspectorSelections.Count; i++)
        {
            mgSelections.Add(new MGSeleciton());
            mgSelections[i].display = BattlePooler.ProduceObject(inspectorSelections[i].display);
            mgSelections[i].directionData = inspectorSelections[i].directionData;
            mgSelections[i].display.SetActive(false);
        }
    }


    public override void DoInitialSubAbility(Ability ab)
    {

            for (int i = 0; i < mgSelections.Count; i++)
            {
            mgSelections[i].display.transform.parent = ab.Owner.transform;
            mgSelections[i].display.transform.position = ab.Owner.transform.position + new Vector3(0f,1f, 0f); 
            mgSelections[i].display.SetActive(false);
            }
        
        curSelection = 0;
        mgSelections[curSelection].display.SetActive(true);
        flipInterval = startFlipInterval;
        if(MultiInput.GetAButtonDown() )
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

        if(MultiInput.GetAButtonDown() && ab.IsCurrentSelectedHero(ab.Owner))
        {
            ab.positionTargets.Add(mgSelections[curSelection].directionData);
            mgSelections[curSelection].display.SetActive(false);
            EndSubAbility();
        }
    }

}
 
