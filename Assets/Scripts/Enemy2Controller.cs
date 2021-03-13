using System.Collections;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    public Rigidbody Player;

    public float _maxSpeed = 6f;
    public float _maxTurn = 4f;
    private Rigidbody _rb;
    private PlayerController _player;
    private Rigidbody _playerRb;
    private Vector3 _prevVel;
    private float maxSI;
    private float stunTime = -1f;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = Player.GetComponent<PlayerController>();
        maxSI = _maxSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_player.State == PlayerInteract.Machine)
        {
            _rb.velocity = Vector3.zero;
            _prevVel = Vector3.zero;
            return;
        }
        
        if (stunTime > 0)
            stunTime -= Time.deltaTime;
        else
            _maxSpeed = maxSI;

        _prevVel = _rb.velocity;
        Vector3 toPlayer = (Player.position - _rb.position).normalized;
        _rb.velocity = Vector3.RotateTowards(_rb.velocity, _maxSpeed * toPlayer, _maxTurn * Time.deltaTime, 2f * Time.deltaTime);
    }

    /*private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        var dirToCol = (other.GetContact(0).point - _rb.position).normalized;
        var force = dirToCol * Mathf.Max(0, Vector3.Dot(_prevVel, dirToCol));
        Player.velocity = 100*force;
        _rb.velocity = -100 * new Vector3(force.x, force.z);
    }*/

    public void Stun()
    {
        _maxSpeed = maxSI / 2;
        stunTime = 3f;
    }

    public void Hit()
    {
        transform.position = new Vector3(Random.Range(-5f, -45f), transform.position.y, Random.Range(-20f, 20f));
    }
}
