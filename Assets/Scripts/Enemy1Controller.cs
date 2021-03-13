using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    public Transform Player;
    public Rigidbody StunPrefab;
    public Rigidbody KnockPrefab;
    //public bool on

    private float _rotSpeed = 12f;
    private float _shootTime;
    private Vector3 _startPos;
    
    public float stunTime = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        _shootTime = Random.Range(3f, 4f);
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (stunTime > 0)
        {
            stunTime -= Time.deltaTime;
            return;
        }
        
        Quaternion finalRot = Quaternion.LookRotation(Player.position + Vector3.up - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, finalRot, _rotSpeed * Time.deltaTime);
        _shootTime -= Time.deltaTime;
        if (_shootTime < 0)
        {
            _shootTime = Random.Range(3f, 4f);
            Rigidbody projectile = Instantiate(KnockPrefab, transform.GetChild(0).position, Quaternion.identity);
                //: Instantiate(StunPrefab, transform.GetChild(0).position, Quaternion.identity);
            projectile.velocity = 12 * transform.forward;
            Destroy(projectile.gameObject, 8f);
        }
    }

    public void Stun()
    {
        _shootTime = 4f;
        stunTime = 4f;
    }
    
    public void Hit()
    {
        _shootTime = 4f;
        stunTime = 6f;
    }
}
