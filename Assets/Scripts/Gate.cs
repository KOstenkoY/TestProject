using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private float _endPositionY = -2;

    [SerializeField] private float _openDoorSpeed = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        while (transform.localPosition.y > _endPositionY)
        {
            yield return new WaitForEndOfFrame();

            transform.localPosition -= new Vector3(0, _openDoorSpeed * Time.deltaTime, 0);
        }
    }
}
