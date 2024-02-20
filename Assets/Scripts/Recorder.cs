using System.Collections.Generic;
using UnityEngine;

namespace Recording
{
    internal class Recorder : MonoBehaviour
    {
        [SerializeField] private List<RecordingObject> _recordingObjects = new List<RecordingObject>();

        private bool _isRecording = true;

        public int RecordedFramesCount => _recordingObjects[0].RecordedFramesCount;

        private void FixedUpdate()
        {
            if (_isRecording)
            {
                foreach (RecordingObject recordingObject in _recordingObjects)
                    recordingObject.RecordNewFrame();
            }
        }

        internal void StopRecording()
        {
            foreach (RecordingObject recordingObject in _recordingObjects)
                recordingObject.StopMoving();

            _isRecording = false;
        }

        internal void SetRecordingObjectsPositions(int frameNumber)
        {
            foreach (RecordingObject recordingObject in _recordingObjects)
                recordingObject.SetPosition(frameNumber);
        }

        internal void ContinueRecord()
        {
            foreach (RecordingObject recordingObject in _recordingObjects)
            {
                recordingObject.StartMoving();

                recordingObject.SetPosition(RecordedFramesCount - 1);
                recordingObject.DetermineVelocities(RecordedFramesCount - 1);
            }

            _isRecording = true;
        }
    }
}