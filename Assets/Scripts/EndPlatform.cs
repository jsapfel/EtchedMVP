using UnityEngine;

public class EndPlatform : MonoBehaviour
{
    public Transform FinalPos;
    private bool move;
    private Transform parent;

    private void Start()
    {
        parent = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!move) return;
        parent.position = Vector3.MoveTowards(parent.position, FinalPos.position, 2*Time.deltaTime);
        if (Mathf.Approximately(0f, (parent.position - FinalPos.position).sqrMagnitude))
        {
            move = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            move = true;

    }
}
