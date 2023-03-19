using UnityEngine;

public class MoveArea : MonoBehaviour
{
    public Vector3 size;

    public Vector3 RandomPosition()
    {
        float xPos = Random.Range(transform.position.x - size.x * 0.5f, transform.position.x + size.x * 0.5f);
        float zPos = Random.Range(transform.position.z - size.z * 0.5f, transform.position.z + size.z * 0.5f);

        return new Vector3(xPos, 0, zPos);
    }

    public Vector3 RandomPositionAtPos(Vector3 pos)
    {
        float xPos = Random.Range(pos.x - size.x * 0.5f, pos.x + size.x * 0.5f);
        float zPos = Random.Range(pos.z - size.z * 0.5f, pos.z + size.z * 0.5f);

        return new Vector3(xPos, 0, zPos);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 1, 0.5f);
        Gizmos.DrawCube(transform.position, size);
    }
}
