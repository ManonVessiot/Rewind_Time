using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToRewindTime : MonoBehaviour
{
    public float _timeToRecord = 5f;

    bool _rewindingTime = false;
    List<PointInTime> _pointsInTime;
    Rigidbody _rg;

    private void Start()
    {
        _pointsInTime = new List<PointInTime>();
        _rg = GetComponent<Rigidbody>();

        StartCoroutine(RecordTime());
    }

    public void StopRewind()
    {
        if (!_rewindingTime)
            return;

        _rewindingTime = false;
        if (_rg)
        {
            _rg.isKinematic = false;
        }
        StartCoroutine(RecordTime());
    }

    IEnumerator RecordTime()
    {
        while (!_rewindingTime)
        {
            if (_pointsInTime.Count > _timeToRecord / Time.fixedDeltaTime)
            {
                _pointsInTime.RemoveAt(_pointsInTime.Count - 1);
            }

            _pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));

            yield return new WaitForFixedUpdate();
        }
    }

    public void StartRewind()
    {
        if (_rewindingTime)
            return;

        _rewindingTime = true;
        if (_rg)
        {
            _rg.isKinematic = true;
        }
        StartCoroutine(RewindingTime());

    }

    IEnumerator RewindingTime()
    {
        while (_rewindingTime)
        {
            if (_pointsInTime.Count == 0)
            {
                StopRewind();
                break;
            }

            PointInTime currentPoint = _pointsInTime[0];
            transform.position = currentPoint._position;
            transform.rotation = currentPoint._rotation;
            _pointsInTime.RemoveAt(0);

            yield return new WaitForFixedUpdate();
        }
    }
}
