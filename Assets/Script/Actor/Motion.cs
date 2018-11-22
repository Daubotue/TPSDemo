using System;
using UnityEngine;

namespace Game.Actor {
    public class Motion : MonoBehaviour {
        private Identity identity;
        private Vector3 velocity;

        protected void Start() {
            this.identity = this.GetComponent<Identity>();
            this.identity.BindEvent(typeof(Snapshots.Move), this.Move);
        }

        protected void FixedUpdate() {
            var x = Input.GetAxis("Vertical");
            var z = -Input.GetAxis("Horizontal");

            if (this.velocity.x != x || this.velocity.z != z) {
                var move = new Snapshots.Move() {
                    velocity = new Vector3(x, 0, z),
                    position = this.transform.position
                };
                this.identity.Input(move);
            }

            this.Simulate();
        }

        public void Simulate() {
            if (this.velocity != Vector3.zero) {
                this.transform.Translate(this.velocity);
            }
        }

        private void Move(Snapshot snapshot) {
            var move = snapshot as Snapshots.Move;
            //this.transform.position = move.position;
            this.velocity = move.velocity;
        }
    }
}