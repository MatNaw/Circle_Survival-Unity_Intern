using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnCircles circles;
    public GameMechanicsParameters GameMechanicsParameters;

    #region SINGLETON
    private static GameManager _i;
    public static GameManager i
    {
        get
        {
            if(_i == null)
            {
                _i = GameObject.FindObjectOfType<GameManager>();
            }
            return _i;
        }
        set
        {
            _i = value;
        }
    }
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
