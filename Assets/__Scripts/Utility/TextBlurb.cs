using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using FMODUnity;

public class TextBlurb : MonoBehaviour
{
        public bool useSlowMotion = false;
        [SerializeField] private TextMeshProUGUI bodyText;
        private RectTransform rectTransform;

        public float clearDuration;
        public float writeDuration;
        
        public GameObject head1;
        public GameObject head2;
        public GameObject head3;
        
        private Vector3 head1OriginalScale;
        private Vector3 head2OriginalScale;
        private Vector3 head3OriginalScale;
        
        [field: Header("Roar SFX")]
        [field: SerializeField] public EventReference dadRoarAudio { get; private set; }
        [field: SerializeField] public EventReference momRoarAudio { get; private set; }
        [field: SerializeField] public EventReference sisRoarAudio { get; private set; }
    

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            head1OriginalScale = head1.transform.localScale;
            head2OriginalScale = head2.transform.localScale;
            head3OriginalScale = head3.transform.localScale;
        }

        public void SetText(string newText, int dragonHead = -1)
        {
            rectTransform.DOPunchScale(Vector3.one * 0.1f, 0.2f).SetUpdate(true);

            TweenText(newText, dragonHead);
        }
        
        private void TweenText(string newText, int dragonHead)
        {
            string currentText = bodyText.text;
            
            // Handle dragon head scaling
            if (dragonHead != -1)
            {
                bool shouldRoar = (GameManager.Instance.currentWave.currentState !=
                                   WaveController.WaveState.goodEnding && GameManager.Instance.currentWave.currentState != WaveController.WaveState.prologue);
                
                switch (dragonHead)
                {
                    case 1:
                        if (shouldRoar) AudioManager.instance.PlayOneShot(dadRoarAudio, this.transform.position);
                        head1.transform.localScale = head1OriginalScale * 1.2f;
                        break;
                    case 2:
                        if (shouldRoar) AudioManager.instance.PlayOneShot(momRoarAudio, this.transform.position);
                        head2.transform.localScale = head2OriginalScale * 1.2f;
                        break;
                    case 3:
                        if (shouldRoar) AudioManager.instance.PlayOneShot(sisRoarAudio, this.transform.position);
                        head3.transform.localScale = head3OriginalScale * 1.2f;
                        break;
                    case 4:
                        if (shouldRoar)
                        {
                            AudioManager.instance.PlayOneShot(dadRoarAudio, this.transform.position);
                            AudioManager.instance.PlayOneShot(momRoarAudio, this.transform.position);
                            AudioManager.instance.PlayOneShot(sisRoarAudio, this.transform.position);
                        }
                        head1.transform.localScale = head1OriginalScale * 1.2f;
                        head2.transform.localScale = head2OriginalScale * 1.2f;
                        head3.transform.localScale = head3OriginalScale * 1.2f;
                        break;

            }
            }
            

            // if(useSlowMotion)
            //     Time.timeScale = GameManager.Instance.dialogueTimeScale;

            // Tween to clear the current text
            DOTween.To(() => 1f,
                    x => bodyText.text = currentText.Substring(0, Mathf.FloorToInt(x * currentText.Length)), 0f,
                    clearDuration).SetUpdate(true)
                .OnComplete(() =>
                {
                    // Tween to write the new text
                    DOTween.To(() => 0f,
                        x => bodyText.text = newText.Substring(0, Mathf.FloorToInt(x * newText.Length)), 1f,
                        writeDuration).SetUpdate(true).OnComplete(() =>
                    {
                        if (dragonHead != -1)
                        {
                            switch (dragonHead)
                            {
                                case 1:
                                    head1.transform.localScale = head1OriginalScale;
                                    break;
                                case 2:
                                    head2.transform.localScale = head2OriginalScale;
                                    break;
                                case 3:
                                    head3.transform.localScale = head3OriginalScale;
                                    break;

                                case 4:
                                    head1.transform.localScale = head1OriginalScale;
                                    head2.transform.localScale = head2OriginalScale;
                                    head3.transform.localScale = head3OriginalScale;
                                    break;
                            }
                        }
                    });
                });
        }

}
