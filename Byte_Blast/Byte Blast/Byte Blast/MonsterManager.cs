using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Byte_Blast
{
    class MonsterManager
    {
        private const int MaxMonsters = 5;
        private const float TimerReset = 10.0f;
        private const float QueLength = 70.0f;

        public const int NumberOfTextures = 1;
        public const int TextureWidth = 164, TextureHeight = 164;
        public const float ConfrontXPosition = 120.0f;

        private Texture2D[] m_MonsterSprites = new Texture2D[NumberOfTextures];
        private Texture2D m_GUISprite, m_HPSprite;
        private SpriteFont m_LevelFont;
        private List<Monster> m_Monsters = new List<Monster>();
        private float m_MonsterSpawnTimer;
        private bool m_Confronting = false;

        public List<Monster> GetMonsters() { return m_Monsters; }
        public bool IsConfronting() { return m_Confronting; }

        public MonsterManager()
        {
            m_MonsterSpawnTimer = TimerReset;
        }

        public void Load(ContentManager Content)
        {
            for (int i = 0; i < NumberOfTextures; i++)
            {
                try
                {
                    m_MonsterSprites[i] = Content.Load<Texture2D>("monster_" + i.ToString());
                    m_GUISprite = Content.Load<Texture2D>("monstergui");
                    m_HPSprite = Content.Load<Texture2D>("monsterhpbar");
                    m_LevelFont = Content.Load<SpriteFont>("levelfont");
                }
                catch (Exception E)
                {
                    Console.WriteLine("Could not load Monster Sprites, Error: " + E.Message);
                }
            }
        }

        public void Update(float speed)
        {
            m_MonsterSpawnTimer -= 0.1f;
            if (m_MonsterSpawnTimer < 0.0f)
            {
                m_MonsterSpawnTimer += (TimerReset + new Random().Next(-5, 10));
                if (m_Monsters.Count < MaxMonsters && !m_Confronting)                
                    m_Monsters.Add(new Monster(1, ref m_Monsters));
            }

            bool confronting = false;
            int i = 0;
            foreach (Monster m in m_Monsters)
            {
                bool blocked = false;

                if (i > 0)
                {
                    if (m.GetXPosition() < m_Monsters[i - 1].GetXPosition() + QueLength)
                        blocked = true;
                }

                if (m.GetXPosition() > ConfrontXPosition && !blocked)
                {
                    m.Update(speed);
                }
                else { confronting = true; }
                
                i++;
            }

            m_Confronting = confronting;

            foreach (Monster m in m_Monsters)
            {
                bool monsterremoved = false;
                if (m.IsDead())
                {
                    m_Monsters.Remove(m);
                    monsterremoved = true;
                }

                if (monsterremoved)
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Monster m in m_Monsters)
            {
                try
                {
                    m.Draw(spriteBatch, m_MonsterSprites[m.GetTextureID()], m_GUISprite, m_HPSprite, m_LevelFont);
                }
                catch (Exception E)
                {
                    Console.WriteLine("Error Drawing Monster Sprites: " + E.Message);
                }
            }
        }

        public void DamageMonster(int damage)
        {
            if (m_Monsters.Count > 0)
            {
                if (m_Monsters[0].GetXPosition() <= ConfrontXPosition + 40.0f)
                    m_Monsters[0].TakeDamage(damage);
            }
        }
    }
}
