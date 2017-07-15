using UnityEngine;

public class TankCannon : MonoBehaviour 
{
    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    Transform firePoint;

    [SerializeField]
    GameObject fireEffect;

    [SerializeField]
    float cooldown = 1f;

    [SerializeField]
    int maximumShotCount = 5;

    private float lastFireTime;
    private int currentShotCount;

    public bool OnCooldown()
    {
        return Time.time < lastFireTime + cooldown;
    }

	public bool Fire()
    {
        if (!OnCooldown() && currentShotCount < maximumShotCount)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(firePoint.forward));
            CannonShell cannonShell = projectile.GetComponent<CannonShell>();
            cannonShell.OnDeath += CannonShell_OnDeath;

            Instantiate(fireEffect, firePoint.position, Quaternion.identity);

            currentShotCount++;

            lastFireTime = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CannonShell_OnDeath()
    {
        currentShotCount--;
    }
}
