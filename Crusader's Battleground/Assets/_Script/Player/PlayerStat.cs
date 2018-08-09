using System.Collections;
using UnityEngine;


public class PlayerStat : MonoBehaviour{
    [SerializeField]
    private string _playerName;
    [SerializeField]
    private float _maxHp, _speed, _jumpForce;
    
    public string PlayerName{
        get{
            return _playerName;
        }

        set{
            _playerName = value;
        }
    }

    public float MaxHp {
        get {
            return _maxHp;
        }
    }

    public float Speed {
        get {
            return _speed;
        }

        set {
            _speed = value;
        }
    }

    public float jumpForce {
        get {
            return _jumpForce;
        }

        set {
            _jumpForce = value;
        }
    }
}
