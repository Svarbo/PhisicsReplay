using UnityEngine;
using UnityEngine.UI;

namespace Recording
{
    internal class RecordingUI : MonoBehaviour
    {
        [SerializeField] private Recorder _recorder;
        [SerializeField] private TimelineSlider _timelineSlider;
        [SerializeField] private Button _stopRecordButton;
        [SerializeField] private Button _startPlayModeButton;
        [SerializeField] private Button _stopPlayModeButton;

        private int _currentFrameNumber => (int)_timelineSlider.Slider.value;

        private void Awake()
        {
            _timelineSlider.gameObject.SetActive(false);
            _startPlayModeButton.gameObject.SetActive(false);
            _stopPlayModeButton.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _timelineSlider.Slider.onValueChanged.AddListener(OnTimelineSliderValueChanged);
            _stopRecordButton.onClick.AddListener(OnStopRecordButtonClick);
            _startPlayModeButton.onClick.AddListener(OnStartPlayModeButtonClick);
            _stopPlayModeButton.onClick.AddListener(OnStopPlayModeButtonClick);
        }

        private void OnDisable()
        {
            _timelineSlider.Slider.onValueChanged.RemoveListener(OnTimelineSliderValueChanged);
            _stopRecordButton.onClick.RemoveListener(OnStopRecordButtonClick);
            _startPlayModeButton.onClick.RemoveListener(OnStartPlayModeButtonClick);
            _stopPlayModeButton.onClick.RemoveListener(OnStopPlayModeButtonClick);
        }

        private void OnTimelineSliderValueChanged(float value) =>
            _recorder.SetRecordingObjectsPositions((int)value);

        private void OnStopRecordButtonClick()
        {
            _startPlayModeButton.gameObject.SetActive(true);
            _stopRecordButton.gameObject.SetActive(false);

            _recorder.StopRecording();
        }

        private void OnStartPlayModeButtonClick()
        {
            _startPlayModeButton.gameObject.SetActive(false);
            _timelineSlider.gameObject.SetActive(true);
            _stopPlayModeButton.gameObject.SetActive(true);

            _timelineSlider.Slider.maxValue = _recorder.RecordedFramesCount - 1;
            _timelineSlider.StartIncreaseCoroutine();

            _recorder.StopRecording();
        }

        private void OnStopPlayModeButtonClick()
        {
            _startPlayModeButton.gameObject.SetActive(true);
            _timelineSlider.gameObject.SetActive(false);
            _stopPlayModeButton.gameObject.SetActive(false);

            _recorder.ContinueRecord(_currentFrameNumber);
        }
    }
}