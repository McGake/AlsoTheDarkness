using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ChooseDirectionMG", menuName = "SubAbility/MiniGames/ChooseDirectionMG", order = 1)]
public class ChooseDirectionMiniGame : SubAbility
{


    public Color selectedColor;
    public Color greyedOutColor;

    private int curSelection = 0;

    private float inputDelay = 0f;

    //private bool skip = true;

    [System.Serializable]
    public class MGSeleciton
    {
        public SpriteRenderer sR;
        public Vector3 directionData;
        public GameObject display;
    }

    public List<MGSeleciton> mgSelections;

    public override void DoInitialSubAbility(Ability ab)
    {
        for(int i = 0; i < mgSelections.Count; i ++)
        {
            mgSelections[i].display = BattlePooler.ProduceObject(mgSelections[i].display);
            mgSelections[i].sR = mgSelections[i].display.GetComponent<SpriteRenderer>();
            mgSelections[i].sR.color = greyedOutColor;
        }
        mgSelections[0].sR.color = selectedColor;
        
    }


    public override void DoSubAbility(Ability ab)
    {
        if(inputDelay < Time.time)
        {
            if (Input.GetAxis("Horizontal") > .3f)
            {
                inputDelay = Time.time + .3f;

                curSelection++;
                if (curSelection >= mgSelections.Count)
                {
                    curSelection = 0;
                }
            }

            if (Input.GetAxis("Horizontal") < -.3f)
            {
                inputDelay = Time.time + .3f;

                curSelection++;
                if (curSelection < 0)
                {
                    curSelection = mgSelections.Count-1;
                }
            }
        }

        if(Input.GetKeyDown("A"))
        {
            ab.positionTargets.Add(mgSelections[curSelection].directionData);
            EndSubAbility();
        }
    }


}
 
