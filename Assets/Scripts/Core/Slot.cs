using System.Collections;
using System.Collections.Generic;
using Twinster.UI;
using UnityEngine;

namespace Twinster.Core
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] TwinsEnum twinEnum;
        public TwinsEnum TwinEnum {
            set { twinEnum = value; } 
            get { return twinEnum; }
        }
        [SerializeField] TripletsEnum tripletEnum;
        public TripletsEnum TripletEnum {
            set { tripletEnum = value; } 
            get { return tripletEnum; }
        }
        
        [SerializeField] ParticleSystem blip;
        [SerializeField] ParticleSystem ball = null;

        RectTransform fireballTarget = null;

        void Start()
        {
            fireballTarget = GameObject.FindWithTag("UILeftHoleBackground").GetComponent<RectTransform>();
        }


        public void DisappearSlot()
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            blip.Play();
            FireBall();
        }

        public void SetBallParticle(ParticleSystem ball)
        {
            this.ball = ball;
        }

        private void FireBall()
        {
            if (ball == null) { 
                Debug.Log("Ball particle not set in Slot");
                return; }

            var targetPos = Camera.main.ScreenToWorldPoint(fireballTarget.transform.position) + Vector3.down;
            
            Vector3 objectPos = transform.position;
            ball.gameObject.SetActive(true);
             
            Vector3 calculatedTarPos = new Vector3();
            Transform tail = ball.GetComponentInChildren<Transform>();
            calculatedTarPos.x = targetPos.x - tail.position.x;
            calculatedTarPos.y = targetPos.y - tail.position.y;
            calculatedTarPos.z = targetPos.z;
 
            float angle = Mathf.Atan2(calculatedTarPos.y, calculatedTarPos.x) * Mathf.Rad2Deg;
            tail.rotation = Quaternion.Euler(new Vector3(angle, 0, 0));

            LeanTween.move(ball.gameObject, targetPos, 0.5f).setDelay(0.2f).setOnComplete( () => {
                LeanTween.cancel(ball.gameObject);
                Destroy(ball.gameObject);
            });
        }
    }
}

