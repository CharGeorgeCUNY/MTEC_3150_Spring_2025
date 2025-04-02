using System.Collections;
using UnityEngine;

public class ShootWeapon : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform muzzlePoint;
    public GameObject bulletPrefab;
    public AudioSource shootSound;
    public AudioSource reloadSound;

    [Header("Weapon Model")]
    public Transform weapon;

    [Header("Weapon Settings")]
    public int ammoCount = 6;
    public int maxAmmo = 6;
    public float fireCooldown = 0.5f;
    public float reloadTime = 2f;

    [Header("Projectile Force")]
    public float bulletSpeed = 30f;

    public KeyCode fireKey = KeyCode.Mouse0;
    public KeyCode reloadKey = KeyCode.R;

    private bool readyToShoot = true;
    private bool reloading = false;

    private void Update()
    {
        if (reloading) return;

        if (Input.GetKeyDown(fireKey) && readyToShoot && ammoCount > 0)
        {
            Shoot();
            shootSound.Play();
        }
        if (Input.GetKeyDown(reloadKey) && ammoCount < maxAmmo)
        {
            StartCoroutine(Reload());
            reloadSound.Play();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, cam.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        Vector3 shotDirection = cam.forward;
        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f)) { shotDirection = (hit.point - muzzlePoint.position).normalized; }

        bulletRb.AddForce(shotDirection * bulletSpeed, ForceMode.Impulse);
        ammoCount--;

        if (ammoCount <= 0) { StartCoroutine(Reload()); reloadSound.Play(); }
        else { Invoke(nameof(ResetShoot), fireCooldown); }
    }

    private void ResetShoot()
    {
        readyToShoot = true;
    }

    private IEnumerator Reload()
    {
        reloading = true;
        readyToShoot = false;

        Vector3 originalPos = weapon.localPosition;
        Vector3 loweredPos = originalPos + Vector3.down * 2f;

        float halfReloadTime = reloadTime * 0.5f;

        for (float t = 0; t < halfReloadTime; t += Time.deltaTime)
        {
            float normalizedTime = t / halfReloadTime;
            weapon.localPosition = Vector3.Lerp(originalPos, loweredPos, normalizedTime);
            yield return null;
        }

        for (float t = 0; t < halfReloadTime; t += Time.deltaTime)
        {
            float normalizedTime = t / halfReloadTime;
            weapon.localPosition = Vector3.Lerp(loweredPos, originalPos, normalizedTime);
            yield return null;
        }

        ammoCount = maxAmmo;

        weapon.localPosition = originalPos;
        reloading = false;
        readyToShoot = true;
    }
}
