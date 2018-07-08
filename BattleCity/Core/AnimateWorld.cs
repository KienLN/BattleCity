using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity.Core
{
    public class AnimateWorld : ITickObject
    {
        #region Emty Implementation
        void ITickObject.OnDestroy()
        {

        }
        void ITickObject.OnDisable()
        {

        }
        void ITickObject.OnEnable()
        {

        }
        void ITickObject.OnInit()
        {

        }
        void ITickObject.OnStart()
        {

        }
        #endregion

        void ITickObject.OnTick()
        {
            foreach (var animatedSprite in m_AnimatedSpriteList)
            {
                if (animatedSprite.TotalFrame == 1 || !animatedSprite.Looped)
                {
                    if (animatedSprite.Ended) return;
                    if (animatedSprite.TotalTime >= animatedSprite.TimePerFrame)
                    {
                        animatedSprite.TotalTime = 0;
                        if ((animatedSprite.CurrentIndex + 1) < animatedSprite.TotalFrame)
                            animatedSprite.CurrentIndex++;
                        else animatedSprite.Ended = true;
                    }
                    else
                    {
                        animatedSprite.TotalTime += Application.FrameRate;
                    }
                }
                else
                {
                    if (animatedSprite.Ended) return;
                    if (animatedSprite.TotalTime >= animatedSprite.TimePerFrame)
                    {
                        animatedSprite.TotalTime = 0;
                        if ((animatedSprite.CurrentIndex + 1) < animatedSprite.TotalFrame)
                            animatedSprite.CurrentIndex++;
                        else animatedSprite.CurrentIndex = 0;
                    }
                    else
                    {
                        animatedSprite.TotalTime += Application.FrameRate;
                    }
                }
                animatedSprite.Rect = animatedSprite.Rects[animatedSprite.CurrentIndex];
            }
        }

        public void Add(AnimatedSprite animatedSprite)
        {
            m_AnimatedSpriteList.Add(animatedSprite);
        }

        public void Remove(AnimatedSprite animatedSprite)
        {
            m_AnimatedSpriteList.Remove(animatedSprite);
        }

        HashSet<AnimatedSprite> m_AnimatedSpriteList = new HashSet<AnimatedSprite>();
    }
}
