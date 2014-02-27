using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Byte_Blast
{
    class Transform2D
    {
        protected float m_Zoom; // Camera Zoom Value
        protected Matrix m_Transform; // Camera Transform Matrix
        protected Matrix m_InverseTransform; // Inverse of Transform Matrix
        protected Vector2 m_Pos; // Camera Position
        protected float m_Rotation; // Camera Rotation Value (Radians)

        public Transform2D()
        {
            m_Pos = Vector2.Zero;
            m_Zoom = 0.0f;
            m_Rotation = 0.0f;
        }

        /// <summary>
        /// Camera Zoom Property
        /// </summary>
        public float Zoom
        {
            get { return m_Zoom; }
            set
            {
                m_Zoom = value;
            }
        }

        /// <summary>
        /// Camera View Matrix Property
        /// </summary>
        public Matrix Transform
        {
            get { return m_Transform; }
            set { m_Transform = value; }
        }

        /// <summary>
        /// Inverse of the view matrix, can be used to get objects screen coordinates
        /// </summary>
        public Matrix InverseTransform
        {
            get { return m_InverseTransform; }
        }

        /// <summary>
        /// Camera Position Property
        /// </summary>
        public Vector2 Pos
        {
            get { return m_Pos; }
            set { m_Pos = value; }
        }

        /// <summary>
        /// Camera Rotation Property (Radians)
        /// </summary>
        public float Rotation
        {
            get { return m_Rotation; }
            set { m_Rotation = value; }
        }
    }
}
