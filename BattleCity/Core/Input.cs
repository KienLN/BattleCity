using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace BattleCity.Core
{
    public sealed class Input : ITickObject
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
            oldState = newState;
            newState = Keyboard.GetState();
        }

        public bool IsKeyPressed(Keys key)
        {
            if (oldState.IsKeyUp(key) && newState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public bool IsKeyDown(Keys key)
        {
            if (oldState.IsKeyDown(key) && newState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public bool IsKeyReleased(Keys key)
        {
            if (oldState.IsKeyDown(key) && newState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }


        KeyboardState newState;
        KeyboardState oldState;
    }
}
