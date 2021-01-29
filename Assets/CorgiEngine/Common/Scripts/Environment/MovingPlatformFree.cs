using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.CorgiEngine
{
    public class MovingPlatformFree : MMPathMovement
    {
        /// a debug display of this platform's current speed
        [MMReadOnly]
        [Tooltip("a debug display of this platform's current speed")]
        public Vector3 DebugCurrentSpeed; 

        protected Vector3 _newSpeed;
        protected Vector3 _positionLastFrame;

        protected override void Update()
        {
            _newSpeed = (this.transform.position - _positionLastFrame) / Time.deltaTime;
            CurrentSpeed = _newSpeed;
            DebugCurrentSpeed = CurrentSpeed;
        }

        protected override void LateUpdate()
        {
            _positionLastFrame = this.transform.position;

        }
    }
}
