using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Byte_Blast
{
    class CameraManager
    {
        public enum FollowState { SMOOTH = 0, LINEAR = 1 }

        private Camera2D m_Camera;
        private Transform2D m_CameraTarget;
        private bool m_FollowTarget;
        private int m_FollowState;
        private float m_FollowSpeed;

        /// <summary>
        /// Gets the Camera from the Camera Manager
        /// </summary>
        /// <returns>Managed 2D Camera</returns>
        public Camera2D GetCamera() { return m_Camera; }

        /// <summary>
        /// Constructor for Objects of CameraManager
        /// </summary>
        /// <param name="viewport">Viewport bounds for correct rotations</param>
        public CameraManager(Viewport viewport)
        {
            m_Camera = new Camera2D(viewport);
            m_CameraTarget = new Transform2D();
            m_FollowTarget = false;
            m_FollowState = (int)FollowState.LINEAR;
            m_FollowSpeed = 10.0f;
        }

        /// <summary>
        /// Updates the CameraManager Object
        /// </summary>
        public void Update()
        {
            if (m_FollowTarget)
            {
                switch (m_FollowState)
                {
                    case (int)FollowState.LINEAR:
                        LinearCameraMovement();
                        break;

                    case (int)FollowState.SMOOTH:
                        SmoothCameraMovement();
                        break;

                    default:
                        break;
                }
            }

            // Late Update
            m_Camera.Update();
        }

        /// <summary>
        /// Move the Camera in a Linear motion (Constant Speed)
        /// </summary>
        public void LinearCameraMovement()
        {
            float distance = Vector2.Distance(m_Camera.Pos, m_CameraTarget.Pos);

            if (distance > m_FollowSpeed * 2)
            {
                Vector2 TravelVector = Vector2.Normalize((m_CameraTarget.Pos - m_Camera.Pos));
                m_Camera.Pos += TravelVector * m_FollowSpeed;
            }
        }

        /// <summary>
        /// Move the Camera is a smooth motion (Linear Interpolation)
        /// </summary>
        public void SmoothCameraMovement()
        {
            m_Camera.Pos = Vector2.Lerp(m_Camera.Pos, m_CameraTarget.Pos, 0.01f * m_FollowSpeed);
            m_Camera.Rotation = MathHelper.Lerp(m_Camera.Rotation, m_CameraTarget.Rotation, 0.01f * m_FollowSpeed);
            m_Camera.Zoom = MathHelper.Lerp(m_Camera.Zoom, m_CameraTarget.Zoom, 0.01f * m_FollowSpeed);
        }

        /// <summary>
        /// Set the Camera Transform
        /// </summary>
        /// <param name="position">Camera Position</param>
        /// <param name="zoom">Camera Zoom</param>
        /// <param name="rotation">Camera Rotation</param>
        public void SetCamera(Vector2 position, float zoom, float rotation)
        {
            m_Camera.Pos = position;
            m_Camera.Zoom = zoom;
            m_Camera.Rotation = rotation;
        }        
        /// <summary>
        /// Set the Camera Transform
        /// </summary>
        /// <param name="transform">Transform to set the Camera to</param>
        public void SetCamera(Transform2D transform)
        {
            m_Camera.Pos = transform.Pos;
            m_Camera.Zoom = transform.Zoom;
            m_Camera.Rotation = transform.Rotation;
        }

        /// <summary>
        /// Set Camera Target Transform
        /// </summary>
        /// <param name="position">Camera Target Position</param>
        /// <param name="zoom">Camera Target Zoom</param>
        /// <param name="rotation">Camera Target Rotation</param>
        /// <param name="followspeed">Speed at which the Camera Moves</param>
        public void SetTarget(Vector2 position, float zoom, float rotation, float followspeed)
        {
            m_CameraTarget.Pos = position;
            m_CameraTarget.Zoom = zoom;
            m_CameraTarget.Rotation = rotation; m_FollowSpeed = followspeed;
        }

        /// <summary>
        /// Enables Camera Target Following
        /// </summary>
        /// <param name="followstate">How the Camera should move (Constant Speed vs Linear Interpolation)</param>
        public void EnableTargetFollow(int followstate)
        {
            m_FollowTarget = true;
            m_FollowState = followstate;
        }
    }
}
