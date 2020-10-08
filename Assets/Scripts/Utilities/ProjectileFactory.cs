using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProjectileFactory
{
    public static GameObject ProduceProjectile(GameObject projectilePrefab, Ability sourceAbility)
    {
        return ProduceProjectile(projectilePrefab, sourceAbility, null, null);
    }

    public static GameObject ProduceProjectile(GameObject projectilePrefab, Ability sourceAbility, Vector3? position)
    {
        return ProduceProjectile(projectilePrefab, sourceAbility, position, null);
    }

    public static GameObject ProduceProjectile(GameObject projectilePrefab, Ability sourceAbility, Vector3? position, Quaternion? rotation)
    {
        GameObject tempProjectile;
        tempProjectile = BattlePooler.ProduceObject(projectilePrefab);
        tempProjectile.GetComponent<Fireable>().SetupProjectile(sourceAbility);
        tempProjectile.transform.position = position == null ? projectilePrefab.transform.position : (Vector3)position;
        tempProjectile.transform.rotation = rotation == null ? projectilePrefab.transform.rotation : (Quaternion)rotation;

        return tempProjectile;
    }
}
