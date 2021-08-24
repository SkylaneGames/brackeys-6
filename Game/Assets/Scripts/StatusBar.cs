using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    [RequireComponent(typeof(Slider))]
    public class StatusBar : MonoBehaviour
    {
        [SerializeField]
        private StatusSystem _statusSystem;

        public Gradient gradient;
        public Image fill;

        [Tooltip("Number of seconds to display the health bar for after being hit. (Set 0 for it to always be visible)")]
        public float displayForSeconds = 3f;

        private float duration = 0;

        private Slider slider;

        private void UpdateValue(float normalisedValue)
        {
            slider.value = Mathf.Clamp01(normalisedValue);
            fill.color = gradient.Evaluate(slider.normalizedValue);
            gameObject.SetActive(slider.normalizedValue < 1);
            duration = displayForSeconds;
        }

        void Awake()
        {
            slider = GetComponent<Slider>();
        }

        private void Start()
        {
            _statusSystem.ValueChanged += OnValueChanged;
            gameObject.SetActive(false);
        }

        private void OnValueChanged(object sender, float value)
        {
            UpdateValue(value / _statusSystem.MaxValue);
        }

        void Update()
        {
            if (duration > 0)
            {
                duration -= Time.deltaTime;
                if (duration  <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
