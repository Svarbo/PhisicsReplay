using System.Collections.Generic;
using UnityEngine;

namespace Recording
{
    [RequireComponent(typeof(Transform))]
    [RequireComponent(typeof(Rigidbody2D))]
    internal class RecordingObject : MonoBehaviour
    {
        private Transform _transform;
        private Rigidbody2D _rigidbody2D;
        private Vector3 _currentPosition;
        private Quaternion _currentRotation;
        private List<float> _xCoordinates = new List<float>();
        private List<float> _yCoordinates = new List<float>();
        private List<float> _zRotationValues = new List<float>();
        private List<Vector2> _velocities = new List<Vector2>();

        internal int RecordedFramesCount => _xCoordinates.Count;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _currentPosition = transform.position;
            _currentRotation = transform.rotation;
        }

        internal void RecordNewFrame()
        {
            _xCoordinates.Add(_transform.position.x);
            _yCoordinates.Add(_transform.position.y);
            _zRotationValues.Add(_transform.rotation.z);
            _velocities.Add(_rigidbody2D.velocity);
        }

        internal void SetPosition(int frameNumber)
        {
            _currentPosition.x = _xCoordinates[frameNumber];
            _currentPosition.y = _yCoordinates[frameNumber];
            _currentRotation.z = _zRotationValues[frameNumber];

            _transform.position = _currentPosition;
            _transform.rotation = _currentRotation;
        }

        internal void DetermineVelocity(int frameNumber) =>
            _rigidbody2D.velocity = _velocities[frameNumber];

        internal void StartMoving() =>
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

        internal void StopMoving() =>
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
    }
}