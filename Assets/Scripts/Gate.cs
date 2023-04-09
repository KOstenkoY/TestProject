using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Transform _door = null;
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
        while (_door.localPosition.y > _endPositionY)
        {
            yield return new WaitForEndOfFrame();

            _door.localPosition -= new Vector3(0, _openDoorSpeed * Time.deltaTime, 0);
        }
    }
}
