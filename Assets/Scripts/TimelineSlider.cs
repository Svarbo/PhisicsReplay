using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Recording
{
    [RequireComponent(typeof(Slider))]
    internal class TimelineSlider : MonoBehaviour, IPointerDownHandler
    {
        internal Slider Slider { get; private set; }

        private Coroutine _coroutine;
        private WaitForSeconds _playbackDelay = new WaitForSeconds(0.01f);

        private void Awake()
        {
            Slider = GetComponent<Slider>();
            Slider.wholeNumbers = true;
        }

        public void OnPointerDown(PointerEventData eventData) =>
            StopIncreaseCoroutine();

        internal void StartIncreaseCoroutine()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(IncreaseSliderValue());
        }

        internal void StopIncreaseCoroutine()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        private IEnumerator IncreaseSliderValue()
        {
            for (int i = 0; i <= Slider.maxValue; i++)
            {
                Slider.value = i;

                yield return _playbackDelay;
            }
        }
    }
}