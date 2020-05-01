using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TestSpecialAnim", menuName = "SubAbilities/Test/TestSpecialAnim", order = 1)]
public class TestSpecialAnim : SubAbility
{
    public AnimationClip testAnim;

    public override void DoInitialSubAbility(Ability ab)
    {
        SetNewSpecialAnimation(testAnim, ab);
    }

    public override void DoSubAbility(Ability ab)
    {
            
        
    }

    public override void OnSelectionFinished(List<GameObject> selectedObjects)
    {

    }


}
 
