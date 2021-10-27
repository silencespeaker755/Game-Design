using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;

namespace Platformer.Mechanics
{
    public class BoxController : MonoBehaviour
    {
        public bool moving = false;
        internal Vector2 initPosition;
        // Start is called before the first frame update
        void Start()
        {

        }

        private void Awake()
        {
            initPosition = GetComponent<Transform>().position;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!moving) {
                moving = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (moving) {
                //position = GetComponent<Transform>().position;
                if (transform.position.y <= initPosition.y + 0.4f)
                    this.transform.Translate(0, 0.02f, 0);
                else
                    moving = false;
            }
            else
            {
                if (transform.position.y > initPosition.y)
                    this.transform.Translate(0, -0.02f, 0);
            }
        }
    }
}
