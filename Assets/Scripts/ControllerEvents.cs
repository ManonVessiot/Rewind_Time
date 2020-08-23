using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerEvents : MonoBehaviour
{
    private static ControllerEvents _instance;
    public static ControllerEvents Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ControllerEvents>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("ControllerEvents");
                    _instance = container.AddComponent<ControllerEvents>();
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
        }
        else
        {
            Destroy(this);
        }
    }

    public delegate void Key();
    public static  Key ReturnKeyDown;
    public static Key ReturnKeyUp;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (ReturnKeyDown != null)
            {
                ReturnKeyDown();
            }
            //RewindTime.Instance.StartRewindAll();
        }
        else if (Input.GetKeyUp(KeyCode.Return))
        {
            if (ReturnKeyUp != null)
            {
                ReturnKeyUp();
            }
            //RewindTime.Instance.StopRewindAll();
        }
    }
}
