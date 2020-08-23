using UnityEngine;

public class RewindTime : MonoBehaviour
{
    ObjectToRewindTime[] objectsToRewind;
    public float _timeToRecord = 5f;

    private static RewindTime _instance;
    public static RewindTime Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<RewindTime>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("RewindTime");
                    _instance = container.AddComponent<RewindTime>();
                }
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            objectsToRewind = FindObjectsOfType<ObjectToRewindTime>();
            if (_timeToRecord > 0)
            {
                foreach (ObjectToRewindTime item in objectsToRewind)
                {
                    item._timeToRecord = _timeToRecord;
                }
            }
        } else
        {
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        ControllerEvents.ReturnKeyDown += StartRewindAll;
        ControllerEvents.ReturnKeyUp += StopRewindAll;
    }

    private void OnDisable()
    {
        ControllerEvents.ReturnKeyDown -= StartRewindAll;
        ControllerEvents.ReturnKeyUp -= StopRewindAll;
    }


    public void StopRewindAt(int index)
    {
        if (index >= 0 && index < objectsToRewind.Length)
        {
            objectsToRewind[index].StopRewind();
        }
    }
    public void StopRewind(ObjectToRewindTime item)
    {
        item.StopRewind();
    }
    public void StopRewindAll()
    {
        foreach (ObjectToRewindTime item in objectsToRewind)
        {
            item.StopRewind();
        }
    }

    public void StartRewindAt(int index)
    {
        if (index >= 0 && index < objectsToRewind.Length)
        {
            objectsToRewind[index].StartRewind();
        }
    }
    public void StartRewind(ObjectToRewindTime item)
    {
        item.StartRewind();
    }
    public void StartRewindAll()
    {
        foreach (ObjectToRewindTime item in objectsToRewind)
        {
            item.StartRewind();
        }
    }
}
