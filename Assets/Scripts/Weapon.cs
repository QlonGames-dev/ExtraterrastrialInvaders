using UnityEngine;
using CnControls;
using System.Collections;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0;
    public int Damage = 10;
    public LayerMask whatToHit; //По чему стрелять?

    public Transform BulletTrailPrefab; //Префаб пули
    public Transform MuzzleFlashPrefab; //Префаб вспышки от выстрела
    public Transform HitPrefab; // Префаб попадания пули
	public Transform HitPrefabBlood; // Префаб крови
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;

    public float camShakeAmt = 0.05f;
    public float camShakeLength = 0.1f;
    CameraShake camShake;

    float timeToFire = 0;
    Transform firePoint;
    Transform fireVector;
	bool IsEnemy;
		
	public string weaponShootSound = "DefaultShot";

	AudioManager audioManager;

    void Awake()
    {
		fireVector = transform.Find("FireVector");
		firePoint = transform.Find("FirePoint");
    }

    void Start()
    {
        camShake = GameMaster.gm.GetComponent<CameraShake>();
        if(camShake == null)
        {
            Debug.Log("CameraShake not found");
        }

		audioManager = AudioManager.instance;
    }

    void Update()
    {
        if (fireRate == 0)
        {
            if (CnInputManager.GetButtonDown("Fire"))
            {
                Shoot();
            }
        }
        else
        {
			if (CnInputManager.GetButton("Fire") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Vector2 fireVectorPosition = new Vector2(fireVector.position.x, fireVector.position.y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, fireVectorPosition - firePointPosition, 100, whatToHit);
 
        Debug.DrawLine(firePointPosition, (fireVectorPosition - firePointPosition) * 100, Color.cyan);

        if (hit.collider != null)
        {
			IsEnemy = false;
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.DamageEnemy(Damage);
				IsEnemy = true;
                Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage.");
            }

        }

        if (Time.time >= timeToSpawnEffect)
        {
            Vector3 hitPos;
            Vector3 hitNormal;

            if (hit.collider == null)
            {
                hitPos = (fireVectorPosition - firePointPosition) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            }
            else
            {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }

            Effect(hitPos, hitNormal);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }

    }

    void Effect(Vector3 hitPos, Vector3 hitNormal)
    {
        Transform trail = Instantiate (BulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform;
        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if(lr != null)
        {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPos);
        }

        Destroy(trail.gameObject, 0.03f);

        if(hitNormal != new Vector3(9999, 9999, 9999))
        {
			if (IsEnemy) 
			{
				Transform hitParticleBlood = Instantiate (HitPrefabBlood, hitPos, Quaternion.FromToRotation (Vector3.right, hitNormal)) as Transform;
				Destroy (hitParticleBlood.gameObject, 0.5f);
			} 
			else if(!IsEnemy)
			{
				Transform hitParticle = Instantiate (HitPrefab, hitPos, Quaternion.FromToRotation (Vector3.right, hitNormal)) as Transform;
				Destroy (hitParticle.gameObject, 0.5f);
			}
        }

        Transform clone = Instantiate (MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range (0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.02f);

        //Shake the camera
        camShake.Shake(camShakeAmt, camShakeLength);

		audioManager.PlaySound(weaponShootSound);
    }
}