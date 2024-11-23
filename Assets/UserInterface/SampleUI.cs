using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UserInterface
{
    public class SampleUI : MonoBehaviour
    {
        private VisualElement _ui;
        private Button _sampleButton;

        private void Awake()
        {
            _ui = GetComponent<UIDocument>().rootVisualElement;
        }

        private void OnEnable()
        {
            _sampleButton = _ui.Q<Button>("testButton");
            _sampleButton.clicked += OnSampleButtonClicked;
        }

        private void OnSampleButtonClicked()
        {
            Debug.Log("Sample");
        }
    }
}