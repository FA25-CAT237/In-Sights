using UnityEngine;

public class TitleCameraMover : MonoBehaviour
{
    // nodes for movement
    [SerializeField] private GameObject titleScreenNode;
    [SerializeField] private GameObject levelSelectNode;
    [SerializeField] private GameObject creditsNode;
    private GameObject targetNode;

    // bool for whether or not we're moving
    private bool takingInput = true;

    public float speed;

    // AWAKE:
    // - set targetNode
    void Awake()
    {
        targetNode = titleScreenNode;
    }

    // UPDATE:
    void Update()
    {
        // move if not at targetNode
        float step = speed * Time.deltaTime;

        if(transform.position != targetNode.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetNode.transform.position, step);
            takingInput = false;
        }
        else if(takingInput == false)
            takingInput = true;
    }

    // SETNODE:
    public void SetNode(int whichNode)
    {
        if(takingInput == true)
        {
            if(whichNode == 0)
                targetNode = titleScreenNode;
            else if(whichNode == 1)
                targetNode = levelSelectNode;
            else if(whichNode == 2)
                targetNode = creditsNode;
            else
                Debug.Log("Set node is out of bounds.");
        }
    }
}
