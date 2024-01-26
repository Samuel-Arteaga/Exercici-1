using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRotation : MonoBehaviour
{
    public Vector3 _maxRotation;
    public Vector3 _minRotation;
    private float offset = -51.6f;
    public GameObject ShootPoint;
    public GameObject Bullet;
    public float ProjectileSpeed = 0;
    public float MaxSpeed;
    public float MinSpeed;
    public GameObject PotencyBar;
    private float initialScaleX;

    private void Awake()
    {
        initialScaleX = PotencyBar.transform.localScale.x;
    }
    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var distancia = mousePos - Bullet.transform.position;//distància entre el click i la bala
        float angle = Mathf.Atan2(distancia.y, distancia.x) * 180f / Mathf.PI + offset;
        Quaternion targetRotation= Quaternion.Euler(0f, 0f, angle);
        transform.rotation = targetRotation;

        if (Input.GetMouseButton(0))
        {
            ProjectileSpeed += Time.deltaTime * 4;
        }
        if(Input.GetMouseButtonUp(0))
        {
            var projectile = Instantiate(Bullet, ShootPoint.transform.position, Quaternion.identity); //On s'instancia?
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(distancia.x, distancia.y).normalized * ProjectileSpeed; //quina velocitat ha de tenir la bala? s'ha de fer alguna cosa al vector direcció?
            ProjectileSpeed = 0f;
        }
        CalculateBarScale();


    }
    public void CalculateBarScale()
    {
        PotencyBar.transform.localScale = new Vector3(Mathf.Lerp(0, initialScaleX, ProjectileSpeed / MaxSpeed),
            transform.localScale.y,
            transform.localScale.z);
    }
}
