using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unity.Netcode.Samples
{

    public class PreyMovement : MonoBehaviour
    {

        public Vector3 direction;
        float speed;
        PreyManager manager;
        

        protected void Start()
        {
            manager = GetComponent<PreyManager>();
            manager.preyMovement = this;
            speed = 5;
        }

        protected void FixedUpdate()
        {
            Move();
        }

        void Move()
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

    }
}

