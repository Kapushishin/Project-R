using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerUnit : MonoBehaviour
{
    #region Unit Stats
    // unit stats
    [SerializeField] string _name = "Player";
    [SerializeField] float _damage = 1f;
    [SerializeField] float _HugeDamage = 5f;
    [SerializeField] float _maxHealth = 10f;
    [SerializeField] public int _attackCD = 5;
    [SerializeField] public int _hugeAttackCD = 15;
    [SerializeField] public float _movementSpeed = 1f;
    [SerializeField] public float _dashSpeed = 3f;
    [SerializeField] public int _dashTimer = 15;
    [SerializeField] public int _blockTimer = 10;
    [SerializeField] public int _globalCD = 60;
    #endregion

    #region Misc Variables
    [HideInInspector] public GameObject _mainCamera;
    [HideInInspector] public float _currentHealth;
    [HideInInspector] public Rigidbody _rigidbody;
    [HideInInspector] public CharacterController _characterController;
    [HideInInspector] public bool _groundedPlayer;
    [SerializeField] public float _offset;
    [HideInInspector] public bool _canTakeDamage;
    [HideInInspector] public bool _canAttack;
    [HideInInspector] public bool _canHugeAttack;
    [SerializeField] public Transform _weapon;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float _rotationSmoothTime = 0.12f;
    [HideInInspector] public float _targetRotation = 0.0f;
    [HideInInspector] public float _rotationVelocity;
    [HideInInspector] public float _verticalVelocity;
    public float _terminalVelocity = 53.0f;

    // timeout deltatime
    [HideInInspector] public float _jumpTimeoutDelta;
    [HideInInspector] public float _fallTimeoutDelta;

    [Space(10)]
    [Tooltip("The height the player can jump")]
    public float _jumpHeight = 1.2f;

    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float _gravity = -15.0f;

    [Space(10)]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float _jumpTimeout = 0.50f;

    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float _fallTimeout = 0.15f;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool _grounded = true;

    [Tooltip("Useful for rough ground")]
    public float _groundedOffset = -0.14f;

    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float _groundedRadius = 0.28f;

    [Tooltip("What layers the character uses as ground")]
    public LayerMask _groundLayers;

    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject _cinemachineCameraTarget;

    [Tooltip("How far in degrees can you move the camera up")]
    public float _topClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float _bottomClamp = -30.0f;

    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float _cameraAngleOverride = 0.0f;

    [Tooltip("For locking the camera position on all axis")]
    public bool _lockCameraPosition = false;

    [HideInInspector] public bool _isCurrentDeviceMouse;

    // cinemachine
    [HideInInspector] public float _cinemachineTargetYaw;
    [HideInInspector] public float _cinemachineTargetPitch;

    [HideInInspector] public float _threshold = 0.01f;

    [Tooltip("Acceleration and deceleration")]
    public float _speedChangeRate = 10.0f;
    [HideInInspector] public float _animationBlend;
    #endregion

    #region Action Interfaces
    // actions interfaces
    public BasicStateMachine _stateMachine;
    public IState _basicState;
    public IState _jumpState;
    public IState _dashState;
    public IState _blockState;
    public IState _attackState;
    public IState _hugeAttackState;
    public ITakeDamage _takedamage;
    public IDoDamage _dodamage;
    public IActions _actions;
    public IInputController _inputController;
    [HideInInspector] public BasicInput _input;
    #endregion

    #region Initialization
    // ==============================
    //      Initialization Part
    // ==============================
    private void OnEnable()
    {
        InitMiscVariables();
        InitUnitActions();
        StateMachineInit();
    }

    private void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void Start()
    {
        _cinemachineTargetYaw = _cinemachineCameraTarget.transform.rotation.eulerAngles.y;

        _jumpTimeoutDelta = _jumpTimeout;
        _fallTimeoutDelta = _fallTimeout;
    }

    public void InitUnitActions()
    {
        _stateMachine = new BasicStateMachine();
        _takedamage = new BasicTakeDamage();
        _dodamage = new BasicDoDamage();
        _actions = new NoActions();
        _inputController = new BasicInputController();
        _input = gameObject.AddComponent<BasicInput>();
    }

    public void InitMiscVariables()
    {
        //_rigidbody = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
    }
    #endregion

    private void Update()
    {
        StateMachineUpdate();
    }

    private void FixedUpdate()
    {
        StateMachinePhysicsUpdate();
    }

    private void LateUpdate()
    {
    }

    #region States Part
    // ==============================
    //        States Part
    // ==============================
    public void StateMachineInit()
    {
        _basicState = new BasicState();
        _jumpState = new JumpState();
        _dashState = new DashState();
        _blockState = new BlockState();
        _attackState = new AttackState();
        _hugeAttackState = new HugeAttackState();
        _stateMachine.Initialize(this, _basicState);
    }

    public void StateMachineUpdate()
    {
        _stateMachine.CurrentState.HandleInput(this);
        _stateMachine.CurrentState.LogicUpdate(this);
    }

    public void StateMachinePhysicsUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate(this);
    }
    #endregion

    #region Damage Part
    // ==============================
    //        Damage Part
    // ==============================
    public void TakeDamage(float damage)
    {
        //_currentHealth = _takedamage.OnTakeDamage(this, damage);
    }

    public void DoDamage()
    {
        _dodamage.OnDoDamage(_damage);
    }
    #endregion
}
