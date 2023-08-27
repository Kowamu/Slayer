using System;
using UniRx;
using UniRx.Diagnostics;
using Unity.Assertions;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using WanwanLand.Slayer.Extensions.InputSystems;
using WanwanLand.Slayer.Extensions.InputSystems.Operetors;

namespace Slayer.Presentation.Input.VirtualPads
{
    /// <summary>
    /// 仮想パッドを表すビュークラス
    /// </summary>
    public class VirtualPad : MonoBehaviour
    {
        [SerializeField]
        private Transform _background;
        
        [SerializeField]
        private Transform _knob;
        
        [SerializeField]
        private float _radius = 100f;
        
        private ReadOnlyReactiveProperty<float2> _axis;
        private ReadOnlyReactiveProperty<bool> _pressed;
        private InputActionMap _inputActions;
        private IObservable<float2> _position;

        /// <summary>
        /// ボタンが押されているか
        /// </summary>
        public IReadOnlyReactiveProperty<bool> Pressed => _pressed;

        /// <summary>
        /// 軸の入力値
        /// </summary>
        public IReadOnlyReactiveProperty<float2> Axis => _axis;
        
        private void Awake()
        {
            Assert.IsNotNull(_background, "_background != null");
            Assert.IsNotNull(_knob, "_knob != null");

            InitializeInputActions();
            InitializeObservables();
        }

        private void Start()
        {
            _pressed
                .TakeFirst(pressed => pressed)
                .Select(_ => float2.zero)
                .Concat(_axis.TakeUntil(_pressed.TakeFirst(pressed => !pressed)))
                .SkipFirst()
                .Repeat()
                .Subscribe(axis =>
                {
                    _knob.localPosition = new float3(axis, 0f);
                })
                .AddTo(this);

            _pressed
                .TakeFirst(pressed => pressed)
                .Select(_ => float2.zero)
                .Concat(_position.TakeFirst())
                .SkipFirst()
                .Repeat()
                .Subscribe(position =>
                {
                    _background.position = new float3(position, 0f);
                })
                .AddTo(this);
        }

        private void OnDestroy()
        {
            _pressed.Dispose();
            _axis.Dispose();
            
            _inputActions.Disable();
            _inputActions.Dispose();
        }
        
        private void InitializeInputActions()
        {
            _inputActions = new InputActionMap("VirtualPad");
            
            _inputActions.AddAction(
                name: "Press", 
                type: InputActionType.PassThrough, 
                binding: "<Pointer>/Press", 
                expectedControlLayout: "Button");
            
            _inputActions.AddAction(
                name: "Position", 
                type: InputActionType.PassThrough, 
                binding: "<Pointer>/Position", 
                expectedControlLayout: "Vector2");
            
            _inputActions.Enable();
        }
        
        private void InitializeObservables()
        {
            _pressed = _inputActions["Press"].PerformedAsObservable()
                .Select(context => context.ReadValueAsButton())
                .ToReadOnlyReactiveProperty();

            _position = _inputActions["Position"].PerformedAsObservable()
                .Select(context => (float2)context.ReadValue<Vector2>());

            var deltaStream = _position
                .TakeUntil(_pressed.TakeFirst(pressed => !pressed))
                .Pairwise((previous, current) => current - previous)
                .Scan((previousTotal, delta) => previousTotal + delta)
                .Select(delta =>
                {
                    var length = math.length(delta);
                    if (length == 0) return delta;
                    var clampLength = math.clamp(length, 0f, 1f);
                    return delta / length * clampLength;
                });

            _axis = _pressed
                .TakeFirst(pressed => pressed)
                .Select(_ => float2.zero)
                .Concat(deltaStream)
                .Concat(Observable.Return(float2.zero))
                .Repeat()
                .ToReadOnlyReactiveProperty();
        }
    }
}