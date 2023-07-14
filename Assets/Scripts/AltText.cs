using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Troll
{
    public class AltText : MonoBehaviour
    {
        [SerializeField]
        private string altText;
        [SerializeField]
        private TextMeshProUGUI label;
        [SerializeField]
        private float characterCd = 0.5f;
        private string originalText;
        private bool isPlaying = false;
        private RectTransform rect;

        // Start is called before the first frame update
        void Start()
        {
            label = GetComponent<TextMeshProUGUI>();
            rect = GetComponent<RectTransform>();
            isPlaying = false;
            originalText = label.text;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isPlaying && RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition, Camera.main))
            {
                StartCoroutine(PlayingAround());
            }
        }

        IEnumerator PlayingAround()
        {
            int index = 0;
            isPlaying = true;
            char[] characters = new char[Mathf.Max(originalText.Length, altText.Length)];
            System.Array.Copy(originalText.ToCharArray(), characters, originalText.Length);
            label.SetText(originalText);
            float cd = characterCd;
            bool isAltLonger = altText.Length > originalText.Length;
            while (isPlaying)
            {
                yield return null;
                if (!RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition, Camera.main))
                    break;
                else if (index >= altText.Length)
                {
                    continue;
                }
                cd -= Time.deltaTime;
                if (cd <= 0)
                {
                    cd = characterCd;
                    if (!isAltLonger && index >= altText.Length - 1)
                        label.SetText(altText);
                    else
                    {
                        characters[index] = altText[index];
                        label.SetText(characters);
                    }
                    index++;
                }
            }
            label.SetText(originalText);
            isPlaying = false;
            yield break;
        }
    }
}