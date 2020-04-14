using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityDefiner", menuName = "ScriptableObjects/AbilityDefiner", order = 1)]
public class AbilityDefiner:ScriptableObject
{

    [SerializeField,
    AngleAttribute(5),
    Tooltip("The angle of the bisecting vector of the radial arc.")]
    public string displayName;





    public InitialSelectType initialSelectType;
    public TargetingType targetingType;
    public AbilityMovement abilityMovement;
    public Preparation preparationType;
    public AbilityTypeNew abilityTypeNew;
    public ReturnMovement returnMovement;
    public EndAbility endAbility;

    public CooldownType cooldownType;




    //public Vector3 fixedVector;

    #region Movement Variables
    public float movementSpeed;
    public Transform movementTarget;
    public Vector2 relativeTargetPosition;
    #endregion Movement Variables



    #region PreparationVariables
    public float prepTime;


    #endregion PreparationVariables





    #region Projectile Variables

    public float projectileFireAngle;
    public int numberOfProjectiles;
    public float timeBetweenProjectiles;
    public ProjectileSource projectileSource;
    public ProjectileSourceOrder projectileSourceOrder;
    public ProjectileSpreadType projectileSpreadType;

        #region Spread Type Variables
        public float spreadAngle; //this is the number of degrees on either side of the projectile fire angle to spread. Maybe I should make this a different name. 
        public SpreadDistribution spreadDistribution;

        #endregion Spread Type Variables





    public GameObject projectile;
    #endregion Projectile Variables

    #region Cooldown Variables
    public float cooldownTime;


    #endregion Cooldown Variables
}

public enum ReturnMovement
{
    None = 0,
    ReturnToStartingPoint = 1,
}

public enum EndAbility
{
    None = 0,
    Standard = 1,

}
