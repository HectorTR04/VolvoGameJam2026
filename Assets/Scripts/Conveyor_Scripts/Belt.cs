using System.Collections;
using UnityEngine;

public class Belt : MonoBehaviour
{
    private static int beltID = 0;

    public Belt beltInSequence;
    public BeltItem beltItem;
    public bool isSpaceOccupied;
    
    private BeltManager beltManager;

    private void Start()
    {
        beltManager = FindFirstObjectByType<BeltManager>();
        beltInSequence = null;
        beltInSequence = FindNextBelt();
        gameObject.name = $"Belt: {beltID++}";
    }
    private void Update()
    {
        if (beltInSequence == null)
            beltInSequence = FindNextBelt();

        if (beltItem != null && beltItem.item != null)
        {
            StartCoroutine(startBeltMove());
        }
    }

    public Vector3 GetItemPosition()
    {
        var padding = 0.3f;
        var position = transform.position;
        return new Vector3(position.x, position.y + padding, position.z);
    }

    private IEnumerator startBeltMove()
    {
        isSpaceOccupied = true;

        if (beltItem.item != null && beltInSequence != null && beltInSequence.isSpaceOccupied == false)
        {
            Vector3 toPos = beltInSequence.GetItemPosition();
            beltInSequence.isSpaceOccupied = true;

            var step = beltManager.speed * Time.deltaTime;

            while (beltItem.item.transform.position != toPos)
            {
                beltItem.item.transform.position = Vector3.MoveTowards(beltItem.transform.position, toPos, step);

                yield return null;
            }

            isSpaceOccupied = false;
            beltInSequence.beltItem = beltItem;
            beltItem = null;
        }
    }

    private Belt FindNextBelt()
    {
        Transform currentBeltTransform = transform;
        RaycastHit hit;

        var forward = transform.forward;

        Ray ray = new Ray(currentBeltTransform.position, forward);
        if (Physics.Raycast(ray, out hit, 1f))
        {
            Belt belt = hit.collider.GetComponent<Belt>();

            if (belt != null)
            {
                return belt;
            }
        }

        return null;
    }
}
