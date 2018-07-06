using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace BattleCity.Core
{
    public abstract class Actor : ITickObject
    {
        #region Inheritance
        protected virtual void OnDestroy() { }
        protected virtual void OnInit() { }
        protected virtual void OnStart() { }
        protected virtual void OnTick() { }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }

        void ITickObject.OnInit() => OnInit();
        void ITickObject.OnStart() => OnStart();
        void ITickObject.OnTick() => OnTick();

        void ITickObject.OnEnable()
        {
            OnStateChange?.Invoke(ActorState.Active);
            //StateChange?.Invoke(this, new ActorStateChangeEventArgs(ActorStateChangeEventArgs.ActorState.Active));
            OnEnable();
        }
        void ITickObject.OnDisable()
        {
            OnStateChange?.Invoke(ActorState.Inactive);
            //StateChange?.Invoke(this, new ActorStateChangeEventArgs(ActorStateChangeEventArgs.ActorState.Inactive));
            OnDisable();
        }
        void ITickObject.OnDestroy()
        {
            OnStateChange?.Invoke(ActorState.Destroy);
            //StateChange?.Invoke(this, new ActorStateChangeEventArgs(ActorStateChangeEventArgs.ActorState.Destroy));
            OnDestroy();
        }
        #endregion

        public bool Active { get { return TickManager.IsActive(this); } set { TickManager.SetActive(this, value); } }
        public static void Destroy(Actor actor) { TickManager.Destroy(actor); }


        public static T Spawn<T>(Vector2 position, float rotation = 0, float scale = 1) where T : Actor, new()
        {
            var actor = TickManager.Create<T>();

            if (actor.Transform.Position == Transform.Default.Position) actor.Transform.Position = position;
            if (actor.Transform.Rotation == Transform.Default.Rotation) actor.Transform.Rotation = rotation;
            if (actor.Transform.Scale == Transform.Default.Scale) actor.Transform.Scale = scale;
            TickManager.Spawn(actor);

            return actor;
        }

        public static Actor Spawn(string typeName, Vector2 position)
        {
            var actor = TickManager.Create(GameObjectNameSpace + typeName) as Actor;
            Debug.Assert(actor != null, "Cannot create actor.");

            if (actor.Transform.Position == Transform.Default.Position)
                actor.Transform.Position = new Vector2(position.X, position.Y);

            TickManager.Spawn(actor);

            return actor;
        }

        const string GameObjectNameSpace = "BattleCity.Gameplay.GameObject.";

        public Transform Transform { get; set; } = new Transform();


        public enum ActorState
        {
            Active, Inactive, Destroy
        }

        public delegate void StateChangeDelegate(ActorState actorState);
        public event StateChangeDelegate OnStateChange;

        //event EventHandler<ActorStateChangeEventArgs> StateChange; // Chạy trước khi OnEnable() và OnDisable() được gọi.
    }
}
