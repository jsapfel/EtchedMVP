using System;
using System.Collections;
using System.Collections.Generic;
using RPGCharacterAnimsFREE;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerInteract
{
    Machine,
    Circle1, Circle2,
    LineSegment1, LineSegment2,
    ExtendLine1,
    RightAngle1, RightAngle2, RightAngle3,
    ParallelLine1, ParallelLine2,
    Justify1U11, Justify1U12, Justify1U21, Justify1U22,
    Justify2U1, Justify2U21, Justify2U22, Justify2U23, Justify2U31, Justify2U32, Justify2U33
}

public class PlayerController : MonoBehaviour
{
    public PlayerInteract State;
    public Transform FloorSpawn;

    public Machine_Circle Circle_Machine;
    public Machine_LineSegment Segment_Machine;
    public Machine_Parallel Parallel_Machine;
    public Machine_Extend Extend_Machine;
    public Machine_RightAngle RA_Machine;
    public Machine_Justify1 Justify1_Machine;
    public Machine_Justify2 Justify2_Machine;

    private Vector3 _prevPos;
    private bool _alive;
    private PlayerInteract _prevState;
    private List<Transform> _inTriggers;
    
    private CanvasController canvas;
    private RPGCharacterControllerFREE otherController;
    private RPGCharacterMovementControllerFREE movementController;
    private Rigidbody _rb;
    private float baseRunSpeed;
    private float baseAirSpeed;
    private float stunnedRun;
    private float stunnedAir;
    private float stunTime = -1f;

    // Start is called before the first frame update
    void Start()
    {
        State = PlayerInteract.Machine;
        _inTriggers = new List<Transform>();
        _prevPos = transform.position;
        _alive = true;
        _prevState = PlayerInteract.Machine;
        
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>();

        _rb = GetComponent<Rigidbody>();
        otherController = GetComponent<RPGCharacterControllerFREE>();
        movementController = GetComponent<RPGCharacterMovementControllerFREE>();

        baseRunSpeed = movementController.runSpeed;
        baseAirSpeed = movementController.inAirSpeed;
        stunnedAir = baseAirSpeed / 2;
        stunnedRun = baseRunSpeed / 2;
    }

    public void Update()
    {
        if (transform.position.y < -30f)
            transform.position = new Vector3(transform.position.x, -29.5f, transform.position.z);
        if (transform.position.y < FloorSpawn.position.y && State == PlayerInteract.Machine && _alive &&
            movementController.MaintainingGround() && otherController.canAction)
        {
            _alive = false;
            _rb.velocity = Vector3.zero;
            otherController.Death();
            _rb.velocity = Vector3.zero;
            canvas.LostLife();
            return;
        }

        if (stunTime >= 0) stunTime -= Time.deltaTime;
        //movementController.runSpeed = baseRunSpeed;
            //movementController.inAirSpeed = baseAirSpeed;

        if (Mouse.current.rightButton.wasPressedThisFrame && stunTime < 0)
            Stun();
        
        if (!Keyboard.current.iKey.wasPressedThisFrame) return;
        
        if (State == PlayerInteract.Machine)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position + Vector3.up,
                1f, LayerMask.GetMask("Walkable"));
            foreach (var c in colliders)
            {
                string t = c.tag;
                switch (t)
                {
                    case "Machine_Circle":
                        State = PlayerInteract.Circle1;
                        break;
                    case "Machine_LineSegment":
                        State = PlayerInteract.LineSegment1;
                        break;
                    case "Machine_ExtendLine":
                        State = PlayerInteract.ExtendLine1;
                        break;
                    case "Machine_RightAngle":
                        State = PlayerInteract.RightAngle1;
                        break;
                    case "Machine_ParallelLine":
                        State = PlayerInteract.ParallelLine1;
                        break;
                    case "Machine_Justify1":
                        State = Justify1_Machine.FirstUseDone ? PlayerInteract.Justify1U21 : PlayerInteract.Justify1U11;
                        break;
                    case "Machine_Justify2":
                        if (Justify2_Machine.SecondUseDone) State = PlayerInteract.Justify2U31;
                        else if (Justify2_Machine.FirstUseDone) State = PlayerInteract.Justify2U21;
                        else State = PlayerInteract.Justify2U1;
                        break;
                }

                if (State == PlayerInteract.Machine) continue;
                _prevPos = transform.position;
                transform.position = FloorSpawn.position;
                break;
            }
            return;
        }
        
        _prevState = State;
        foreach (var t in _inTriggers)
        {
            if (t.CompareTag("Point"))
            {
                switch (State)
                {
                    case PlayerInteract.Circle1:
                        Circle_Machine.NewC = t;
                        ++State;
                        break;
                    case PlayerInteract.Circle2:
                        Circle_Machine.NewCircle(Vector3.Distance(Circle_Machine.NewC.position, t.position));
                        State = PlayerInteract.Machine;
                        break;
                    case PlayerInteract.LineSegment1:
                        Segment_Machine.NewPoint = t;
                        ++State;
                        break;
                    case PlayerInteract.LineSegment2:
                        Segment_Machine.NewSegment(t);
                        State = PlayerInteract.Machine;
                        break;
                    case PlayerInteract.ParallelLine2:
                        Parallel_Machine.NewLine(t);
                        State = PlayerInteract.Machine;
                        break;
                    case PlayerInteract.ExtendLine1:
                        State = PlayerInteract.Machine;
                        break;
                    case PlayerInteract.RightAngle1:
                        RA_Machine.P1 = t;
                        ++State;
                        break;
                    case PlayerInteract.RightAngle2:
                        RA_Machine.P2 = t;
                        ++State;
                        break;
                    case PlayerInteract.RightAngle3:
                        RA_Machine.NewRA(t);
                        State = PlayerInteract.Machine;
                        break;
                    case PlayerInteract.Justify2U21:
                        Justify2_Machine.Use2P1 = t;
                        ++State;
                        break;
                    case PlayerInteract.Justify2U22:
                        Justify2_Machine.Use2P2 = t;
                        ++State;
                        break;
                    case PlayerInteract.Justify2U23:
                        Justify2_Machine.Use2P3 = t;
                        Justify2_Machine.Use2();
                        State = PlayerInteract.Machine;
                        break;
                    case PlayerInteract.Justify2U31:
                        Justify2_Machine.Use3P1 = t;
                        ++State;
                        break;
                    case PlayerInteract.Justify2U32:
                        Justify2_Machine.Use3P2 = t;
                        ++State;
                        break;
                    case PlayerInteract.Justify2U33:
                        Justify2_Machine.Use3P3 = t;
                        Justify2_Machine.Use3();
                        State = PlayerInteract.Machine;
                        break;
                }
            }
            else if (t.CompareTag("Segment"))
            {
                LineSegment s = t.GetComponent<LineSegment>();
                switch (State)
                {
                    case PlayerInteract.ParallelLine1:
                        Parallel_Machine.Segment = s;
                        ++State;
                        break; 
                    case PlayerInteract.ExtendLine1:
                        Extend_Machine.NewLine(s);
                        State = PlayerInteract.Machine;
                        break;
                    case PlayerInteract.Justify1U11:
                        Justify1_Machine.Use1S1 = s;
                        ++State;
                        break;
                    case PlayerInteract.Justify1U12:
                        Justify1_Machine.Use1S2 = s;
                        Justify1_Machine.Use1();
                        State = PlayerInteract.Machine;
                        break;
                    case PlayerInteract.Justify1U21:
                        Justify1_Machine.Use2S1 = s;
                        ++State;
                        break;
                    case PlayerInteract.Justify1U22:
                        Justify1_Machine.Use2S2 = s;
                        Justify1_Machine.Use2();
                        State = PlayerInteract.Machine;
                        break;
                    case PlayerInteract.Justify2U1:
                        Justify2_Machine.Use1S = s;
                        Justify2_Machine.Use1();
                        State = PlayerInteract.Machine;
                        break;
                }
            }

            if (State == _prevState) continue;
            if (State == PlayerInteract.Machine)
            {
                canvas.UsedMachine();
                transform.position = _prevPos;
            }
            break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
            _inTriggers.Insert(0, other.transform);
        else if (other.CompareTag("Segment"))
            _inTriggers.Add(other.transform);
        else if (other.CompareTag("Finish"))
        {
            otherController.Lock(false, true, false, 0f, -1f);
            movementController.canMove = false;
            canvas.CompletedLevel();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Point") || other.CompareTag("Segment"))
            _inTriggers.Remove(other.transform);
    }

    public void Restart()
    {
        transform.position = _prevPos;
        while(!otherController.isDead){}
        otherController.Revive();
        _alive = true;
    }

    public void Stun()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + Vector3.up,
            6f, LayerMask.GetMask("Walkable"));
        foreach (var c in colliders)
        {
            if (c.CompareTag("Enemy1"))
                c.GetComponent<Enemy1Controller>().Stun();
            else if (c.CompareTag("Enemy2"))
                c.GetComponent<Enemy2Controller>().Stun();
        }
        stunTime = 8f;
        //movementController.runSpeed = stunnedRun;
        //movementController.inAirSpeed = stunnedAir;
    }
}
