using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //GameSetting
    GameObject GameSetting;

    //Player Stats
    private float _speed;
    private float _jumpForce;
    private string _playerName;
    private float _currentHP = 100;

    //Controller KeyCode
    private string[] _controllerNames;
    private int _controllerIndex;
    private KeyCode _jumpKey;
    private KeyCode _portalKey;
    private KeyCode _meeleKey;
    private KeyCode _dropKey;
    private KeyCode _toRightSkillKey;
    private KeyCode _toLeftSkillKey;
    private KeyCode _pickUpKey;

    //Controller Trigger
    private float _spellTrigger;
    private float _horizontalMovement;

    //Player MeeleAttack
    private float _attackRange = 6f;
    private float _attackSpeed = 0.5f;
    private float _knockBack = 1000f;
    private float _attackDamage = 5.0f;

    //Player State
    private bool _facingRight;
    private bool _isAttacking = false;
    private bool _canJump = true;
    private bool _isWalking;
    private bool _hasHealth = false;

    //Player Layer
    LayerMask _playerLayer;
    int layerInt;

    //Player Animation
    Animator anim;

    // Use this for initialization
    void Start() {
        GameSetting = GameObject.Find("GameSetting");
        _controllerNames = new string[4];
        

        PlayerStat playerStat = GetComponentInParent<PlayerStat>();
        playerStat.PlayerName = transform.parent.name;
        _speed = playerStat.Speed;
        _jumpForce = playerStat.jumpForce;
        _playerName = playerStat.PlayerName;

        anim = this.GetComponentInParent<Animator>();

        

        SetLayer();
    }

    private void FixedUpdate()
    {
        Map_Player();

        if (!_isAttacking)
        {
            Movement();
        }
    }

    // Update is called once per frame
    void Update() {
        PlayerState();
        PlayAnimation();
        MeeleAttack();
        if (Check_Dead())
        {
            Player_Dead();
        }

    }

    private void PlayerState() {
        _currentHP = GetComponentInParent<PlayerHP>().GetCurrentHP();
    }

    private void Movement()
    {
        _isWalking = (_horizontalMovement != 0) ? true : false;
        Vector3 movement = new Vector3(_horizontalMovement, 0, 0);
        transform.Translate(-movement * _speed * Time.deltaTime);
        Flip();
        Jump(Input.GetKey(_jumpKey));
    }

    private void SetLayer() {
        _playerLayer |= (1 << LayerMask.NameToLayer(_playerName));
        _playerLayer |= (1 << LayerMask.NameToLayer("Gas"));
        _playerLayer = ~_playerLayer;
        _playerLayer = _playerLayer.value;
    }

    private void PlayAnimation() {
        anim.SetFloat("x", _horizontalMovement);
        anim.SetBool("Walk", _isWalking);
    }

    void MeeleAttack()
    {
        if (Input.GetKeyDown(_meeleKey))
        {
            if (_isAttacking == false)
            {
                StartCoroutine(Coroutine_MeeleAttack());
            }
        }
    }

    void Flip() {
        if (_horizontalMovement < 0)
        {
            _facingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_horizontalMovement > 0) {
            _facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Jump(bool space) {
        if (space) {
            if (_canJump) {
                _canJump = false;
                this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _jumpForce);
            }
        }
    }



    public bool Check_Dead() {
        if (_currentHP <= 0)
        {
            return true;
        }
        else {
            return false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            _canJump = true;
        }
    }


    IEnumerator Coroutine_MeeleAttack() {
        _isAttacking = true;
        anim.Play("attack1");
        Vector2 attackDirection;
        Vector3 playerHeight = new Vector3(0, 2, 0);

        attackDirection = _facingRight ? -transform.right : transform.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + playerHeight, attackDirection, _attackRange, _playerLayer);
        if (hit.collider != null)
        {
            switch (hit.collider.tag) {
                case "Box":
                    hit.transform.GetComponent<Box>().boxHp -= _attackDamage;
                    break;
                case "Player":
                    hit.transform.GetComponent<PlayerHP>().DamageToCurrentHP(_attackDamage);
                    hit.transform.GetComponent<Rigidbody2D>().AddForce(-hit.normal * _knockBack);
                    break;
            }
        }
        yield return new WaitForSeconds(_attackSpeed);
        _isAttacking = false;
        yield return null;
    }

    public float GetSpellTriggerKey() {
        return _spellTrigger;
    }

    public KeyCode GetKeyCode(string keyName) {
        switch (keyName) {
            case "jumpKey":
                return _jumpKey;
            case "meeleKey":
                return _meeleKey;
            case "dropKey":
                return _dropKey;
            case "portalKey":
                return _portalKey;
            case "right":
                return _toRightSkillKey;
            case "left":
                return _toLeftSkillKey;
            case "pickUpKey":
                return _pickUpKey;
            default:
                return KeyCode.None;
        }
    }

    public bool GetPlayerDirection() {
        return _facingRight;
    }

    public void SetPlayerAttackState(bool state) {
        _isAttacking = state;
    }

    void Player_Dead() {
        anim.Play("death");
        this.GetComponent<PlayerController>().enabled = false;
    }

    void Map_Player() {
        switch (_playerName) {
            case "Player1":
                if (GameSetting.GetComponent<ControllerSetUp>().GetController(0) != null) {
                    _controllerNames[0] = GameSetting.GetComponent<ControllerSetUp>().GetController(0);
                    setKeys(_controllerNames[0]);
                }
                break;
            case "Player2":
                if (GameSetting.GetComponent<ControllerSetUp>().GetController(0) != null)
                {
                    _controllerNames[1] = GameSetting.GetComponent<ControllerSetUp>().GetController(1);
                    setKeys(_controllerNames[1]);
                }
                break;
            case "Player3":
                if (GameSetting.GetComponent<ControllerSetUp>().GetController(0) != null)
                {
                    _controllerNames[2] = GameSetting.GetComponent<ControllerSetUp>().GetController(2);
                    setKeys(_controllerNames[2]);
                }
                break;
        }
    }

    void setKeys(string name) {

        switch (name)
        {
            case "Joystick1":
                _jumpKey = KeyCode.Joystick1Button0; //A
                _meeleKey = KeyCode.Joystick1Button1; //B
                _portalKey = KeyCode.Joystick1Button2; //X
                _pickUpKey = KeyCode.Joystick1Button3; //Y
                _dropKey = KeyCode.Joystick1Button9; //RightThumbStick Press
                _toRightSkillKey = KeyCode.Joystick1Button5; // Right Bumper
                _toLeftSkillKey = KeyCode.Joystick1Button4; // Left Bumper
                _horizontalMovement = Input.GetAxisRaw("Horizontal1");
                _spellTrigger = Input.GetAxisRaw("Fire11");
                break;

            case "Joystick2":
                _jumpKey = KeyCode.Joystick2Button0; //A
                _meeleKey = KeyCode.Joystick2Button1; //B
                _portalKey = KeyCode.Joystick2Button2; //X
                _pickUpKey = KeyCode.Joystick2Button3; //Y
                _dropKey = KeyCode.Joystick2Button9; //RightThumbStick Press
                _toRightSkillKey = KeyCode.Joystick2Button5; // Right Bumper
                _toLeftSkillKey = KeyCode.Joystick2Button4; // Left Bumper
                _horizontalMovement = Input.GetAxisRaw("Horizontal2");
                _spellTrigger = Input.GetAxisRaw("Fire22");
                break;

            case "Joystick3":
                _jumpKey = KeyCode.Joystick3Button0; //A
                _meeleKey = KeyCode.Joystick3Button1; //B
                _portalKey = KeyCode.Joystick3Button2; //X
                _pickUpKey = KeyCode.Joystick3Button3; //Y
                _dropKey = KeyCode.Joystick3Button9; //RightThumbStick Press
                _toRightSkillKey = KeyCode.Joystick3Button5; // Right Bumper
                _toLeftSkillKey = KeyCode.Joystick3Button4; // Left Bumper
                _horizontalMovement = Input.GetAxisRaw("Horizontal3");
                _spellTrigger = Input.GetAxisRaw("Fire3");
                break;
            case "Joystick4":
                _jumpKey = KeyCode.Joystick4Button0; //A
                _meeleKey = KeyCode.Joystick4Button1; //B
                _portalKey = KeyCode.Joystick4Button2; //X
                _pickUpKey = KeyCode.Joystick4Button3; //Y
                _dropKey = KeyCode.Joystick4Button9; //RightThumbStick Press
                _toRightSkillKey = KeyCode.Joystick4Button5; // Right Bumper
                _toLeftSkillKey = KeyCode.Joystick4Button4; // Left Bumper
                _horizontalMovement = Input.GetAxisRaw("Horizontal4");
                _spellTrigger = Input.GetAxisRaw("Fire4");
                break;
            case "Joystick5":
                _jumpKey = KeyCode.Joystick5Button0; //A
                _meeleKey = KeyCode.Joystick5Button1; //B
                _portalKey = KeyCode.Joystick5Button2; //X
                _pickUpKey = KeyCode.Joystick5Button3; //Y
                _dropKey = KeyCode.Joystick5Button9; //RightThumbStick Press
                _toRightSkillKey = KeyCode.Joystick5Button5; // Right Bumper
                _toLeftSkillKey = KeyCode.Joystick5Button4; // Left Bumper
                _horizontalMovement = Input.GetAxisRaw("Horizontal5");
                _spellTrigger = Input.GetAxisRaw("Fire5");
                break;
            case "Joystick6":
                _jumpKey = KeyCode.Joystick6Button0; //A
                _meeleKey = KeyCode.Joystick6Button1; //B
                _portalKey = KeyCode.Joystick6Button2; //X
                _pickUpKey = KeyCode.Joystick6Button3; //Y
                _dropKey = KeyCode.Joystick6Button9; //RightThumbStick Press
                _toRightSkillKey = KeyCode.Joystick6Button5; // Right Bumper
                _toLeftSkillKey = KeyCode.Joystick6Button4; // Left Bumper
                _horizontalMovement = Input.GetAxisRaw("Horizontal6");
                _spellTrigger = Input.GetAxisRaw("Fire6");
                break;
            case "Joystick7":
                _jumpKey = KeyCode.Joystick7Button0; //A
                _meeleKey = KeyCode.Joystick7Button1; //B
                _portalKey = KeyCode.Joystick7Button2; //X
                _pickUpKey = KeyCode.Joystick7Button3; //Y
                _dropKey = KeyCode.Joystick7Button9; //RightThumbStick Press
                _toRightSkillKey = KeyCode.Joystick7Button5; // Right Bumper
                _toLeftSkillKey = KeyCode.Joystick7Button4; // Left Bumper
                _horizontalMovement = Input.GetAxisRaw("Horizontal7");
                _spellTrigger = Input.GetAxisRaw("Fire7");
                break;
        }
    }
}
