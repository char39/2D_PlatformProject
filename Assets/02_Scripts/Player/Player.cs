using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCtrl
{
    public class Player : MonoBehaviour
    {
        [HideInInspector]
        public Vector2 velocity;

        internal PlayerInput playerInput;        
        internal Transform tr;
        internal SpriteRenderer sr;
        internal Rigidbody2D rb;

        private int horizontal;

        void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            tr = GetComponent<Transform>();
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            
        }

        void FixedUpdate()
        {
            Move();
        }

        void Update()
        {
            GetValue();
        }

        private void GetValue()
        {
            velocity = rb.velocity;
            horizontal = playerInput.Horizontal_1p;
        }

        private void Move()
        {
            if (horizontal != 0)
                rb.velocity = new Vector2(horizontal * 5f, rb.velocity.y);
        }


    }
}