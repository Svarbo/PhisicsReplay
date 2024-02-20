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
        private List<Vector2> _positions = new List<Vector2>();
        private List<Vector3> _eulerAngles = new List<Vector3>();
        private List<Vector2> _velocities = new List<Vector2>();
        private List<float> _angularVelocities = new List<float>();

        internal int RecordedFramesCount => _positions.Count;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        internal void RecordNewFrame()
        {
            _positions.Add(_transform.position);
            _eulerAngles.Add(_transform.eulerAngles);

            _velocities.Add(_rigidbody2D.velocity);
            _angularVelocities.Add(_rigidbody2D.angularVelocity);
        }

        internal void SetPosition(int frameNumber)
        {
            _transform.eulerAngles = _eulerAngles[frameNumber];
            _transform.position = _positions[frameNumber];
        }

        internal void DetermineVelocities(int frameNumber)
        {
            _rigidbody2D.velocity = _velocities[frameNumber];
            _rigidbody2D.angularVelocity = _angularVelocities[frameNumber];
        }

        internal void RemoveUselessFrames(int firstUselessFrame)
        {
            int removingFramesCount = RecordedFramesCount - firstUselessFrame;

            if (firstUselessFrame <= RecordedFramesCount)
            {
                _positions.RemoveRange(firstUselessFrame, removingFramesCount);
                _eulerAngles.RemoveRange(firstUselessFrame, removingFramesCount);
                _velocities.RemoveRange(firstUselessFrame, removingFramesCount);
                _angularVelocities.RemoveRange(firstUselessFrame, removingFramesCount);
            }
        }

        internal void StartMoving() =>
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

        internal void StopMoving() =>
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
    }
}