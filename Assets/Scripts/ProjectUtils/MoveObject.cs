using UnityEngine;
using DG.Tweening;
public class MoveObject : MonoBehaviour
{
    public Vector3 target;
    [SerializeField] private float time = 1;

    public void Move()
    {
        transform.DOMove(transform.position + target, time);
    }

    public void MoveTo()
    {
        transform.DOMove(target, time);
    }

    public void MoveToX(float value) => transform.DOMoveX(value, time);
    public void MoveX(float units) => transform.DOMoveX(transform.position.x + units, time);
    public void MoveToY(float value) => transform.DOMoveY(value, time);
    public void MoveY(float units) => transform.DOMoveY(transform.position.y + units, time);
    public void MoveToZ(float value) => transform.DOMoveZ(value, time);
    public void MoveZ(float units) => transform.DOMoveZ(transform.position.z + units, time);
}
