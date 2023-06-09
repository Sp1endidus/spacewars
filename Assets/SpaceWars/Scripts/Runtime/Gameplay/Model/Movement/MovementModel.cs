using SpaceWars.Runtime.Configs.Movement;
using UnityEngine;

namespace SpaceWars.Runtime.Gameplay.Model.Movement {
    public class MovementModel : MonoBehaviour {
        private MovementData _data;
        private Rigidbody2D _rigidbody2D;

        private float _acceleration;
        private float _rotation;

        public void Initialize(MovementData data) {
            _data = data;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void SetInput(float acceleration, float rotation) {
            _acceleration = acceleration;
            _rotation = rotation;
        }

        private void FixedUpdate() {
            if (_data == null) {
                return;
            }
            Move();
        }

        private void Move() {
            var worldDirection = _rigidbody2D.transform.TransformDirection(Vector2.up * _acceleration);
            _rigidbody2D.AddForce(worldDirection * _data.MoveSpeed, ForceMode2D.Force);
            _rigidbody2D.AddTorque(_rotation * _data.RotationSpeed, ForceMode2D.Force);
        }
    }
}