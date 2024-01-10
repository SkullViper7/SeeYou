using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unity.Netcode.Samples
{

    public class PreyMovement : PlayerMovement
    {

        public Vector3 direction;
        float speed;
        PreyManager manager;
        

        protected override void Start()
        {
            manager = GetComponent<PreyManager>();
            manager.preyMovement = this;
            speed = 5;
            base.Start();
        }

        protected override void FixedUpdate()
        {
            MoveOnServer();
        }

        void Move()
        {
            
            transform.Translate(direction * speed * Time.deltaTime);
        }

        void MoveCamera()
        {
            base.FixedUpdate();
        }

        [ServerRpc]
        void MoveOnServer()
        {
            Move();
            MoveCamera();
        }
    }
}

