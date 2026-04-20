using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
    protected Dictionary<Tick, System.Action> tickFunctions = new Dictionary<Tick, System.Action>();


    protected void Initialize(params Tick[] ticks) { }


    // Update is called once per frame
    abstract protected void Effect();

    protected class Tick
    {
        float realSeconds;
        bool canFreeze;

        public Tick(float tickLength, bool freezeable) => (realSeconds, canFreeze) = (tickLength, freezeable);
    }

}