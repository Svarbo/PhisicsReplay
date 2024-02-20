using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Recording
{
    public class RecordingUI : MonoBehaviour
    {
        [SerializeField] private Recorder _recorder;
        [SerializeField] private Slider _timelineSlider;
        [SerializeField] private Button _stopRecordButton;
        [SerializeField] private Button _startPlayModeButton;
        [SerializeField] private Button _stopPlayModeButton;

        private Coroutine _coroutine;
        private WaitForSeconds _playbackDelay = new WaitForSeconds(0.01f);

        private void Awake()
        {
            _timelineSlider.gameObject.SetActive(false);
            _startPlayModeButton.gameObject.SetActive(false);
            _stopPlayModeButton.gameObject.SetActive(false);

            _timelineSlider.wholeNumbers = true;
        }

        private void OnEnable()
        {
            _timelineSlider.onValueChanged.AddListener(OnTimelineSliderValueChanged);
            _stopRecordButton.onClick.AddListener(OnStopRecordButtonClick);
            _startPlayModeButton.onClick.AddListener(OnStartPlayModeButtonClick);
            _stopPlayModeButton.onClick.AddListener(OnStopPlayModeButtonClick);
        }

        private void OnDisable()
        {
            _timelineSlider.onValueChanged.RemoveListener(OnTimelineSliderValueChanged);
            _stopRecordButton.onClick.RemoveListener(OnStopRecordButtonClick);
            _startPlayModeButton.onClick.RemoveListener(OnStartPlayModeButtonClick);
            _stopPlayModeButton.onClick.RemoveListener(OnStopPlayModeButtonClick);
        }

        private void OnTimelineSliderValueChanged(float value) =>
            _recorder.SetRecordingObjectsPositions((int)value);

        private void OnStopRecordButtonClick()
        {
            _recorder.StopRecording();

            _startPlayModeButton.gameObject.SetActive(true);
            _stopRecordButton.gameObject.SetActive(false);
        }

        private void OnStartPlayModeButtonClick()
        {
            _startPlayModeButton.gameObject.SetActive(false);
            _timelineSlider.gameObject.SetActive(true);
            _stopPlayModeButton.gameObject.SetActive(true);

            _timelineSlider.maxValue = _recorder.RecordedFramesCount - 1;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(IncreaseSliderValue());

            _recorder.StopRecording();
        }

        private void OnStopPlayModeButtonClick()
        {
            StopCoroutine(_coroutine);
            _coroutine = null;

            _startPlayModeButton.gameObject.SetActive(true);
            _timelineSlider.gameObject.SetActive(false);
            _stopPlayModeButton.gameObject.SetActive(false);

            _recorder.ContinueRecord();
        }

        private IEnumerator IncreaseSliderValue()
        {
            for (int i = 0; i <= _timelineSlider.maxValue; i++)
            {
                _timelineSlider.value = i;

                yield return _playbackDelay;
            }
        }
    }
}