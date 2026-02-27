using System.Collections;
using UnityEngine;

public class Belt : MonoBehaviour
{
    private static int beltID = 0;

    public Belt beltInSequence;
    public ItemSpawner spawner;
    public bool isSpaceOccupied;

    private BeltManager beltManager;

    private void Start()
    {
        beltInSequence = null;
        beltInSequence = FindNextBelt();
        gameObject.name = $"Belt: {beltID++}";
    }
    public Vector3 GetItemPosition()
    {
        return Vector3.zero;
    }

    private IEnumerator startBeltMove()
    {
        yield return new WaitForEndOfFrame();
    }

    private Belt FindNextBelt()
    {
        return null;
    }
}
