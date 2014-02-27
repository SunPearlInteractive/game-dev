using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Byte_Blast
{
    class Camera2D : Transform2D
    {
        // Class Fields
        protected Viewport m_Viewport; // Camera's Viewport

        /// <summary>
        /// Updates the Camera object
        /// </summary>
        public void Update()
        {
            // Clamp zoom value
            m_Zoom = MathHelper.Clamp(m_Zoom, 0.5f, 4.0f);

            // Clamp rotation value
            m_Rotation = ClampAngle(m_Rotation);

            Matrix CreateRotationMatrix = Matrix.CreateTranslation(-m_Viewport.Width / 2, -m_Viewport.Height / 2, 0.0f) *
                                          Matrix.CreateScale(new Vector3(m_Zoom, m_Zoom, 1.0f)) *
                                          Matrix.CreateRotationZ(m_Rotation) *
                                          Matrix.CreateTranslation(m_Viewport.Width / 2, m_Viewport.Height / 2, 0.0f);

            Matrix CreatePositionMatrix = Matrix.CreateTranslation(-m_Pos.X, -m_Pos.Y, 0.0f);

            // Create View Matrix
            m_Transform = CreatePositionMatrix * CreateRotationMatrix;

            // Update Inverse Matrix
            m_InverseTransform = Matrix.Invert(m_Transform);
        }

        /// <summary>
        /// Constructor for a objects of Camera2D
        /// </summary>
        /// <param name="viewport">Viewport bounds for correct rotations</param>
        public Camera2D(Viewport viewport)
        {
            m_Zoom = 1.0f;
            m_Rotation = 0.0f;
            m_Pos = Vector2.Zero;
            m_Viewport = viewport;
        }

        /// <summary>
        /// Clamps a radian value between -pi and pi
        /// </summary>
        /// <param name="radians">angle to be clamped</param>
        /// <returns>angle (radians)</returns>
        protected float ClampAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }
    }
}
