using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Object = System.Object;

public class StateMachine
{
    private IState state;

    private Dictionary<Type, List<Transition>> transitionsByState = new Dictionary<Type, List<Transition>>(); //key: state, value: transitions
    private List<Transition> prioTransitions = new List<Transition>(); //transitions with priority
    private List<Transition> currentTransitions = new List<Transition>(); //the transitions we have available for the current state

    private static List<Transition> EmptyTransitions = new List<Transition>(0);

    public void Tick()
    {
        var transition = GetTransition();
        if (transition != null)
            SetState(transition.To);

        state?.Tick();
    }

    public void SetState(IState newState)
    {
        if (newState == state)
            return;

        state?.OnExit();
        state = newState;

        transitionsByState.TryGetValue(state.GetType(), out currentTransitions);
        if (currentTransitions == null)
            currentTransitions = EmptyTransitions;

        state.OnEnter();
    }

    public void AddTransition(IState from, IState to, Func<bool> predicate)
    {
        if (transitionsByState.TryGetValue(from.GetType(), out var transitions) == false)
        {
            transitions = new List<Transition>();
            transitionsByState[from.GetType()] = transitions;
        }

        transitions.Add(new Transition(to, predicate));
    }

    public void AddPrioTransition(IState state, Func<bool> predicate)
    {
        prioTransitions.Add(new Transition(state, predicate));
    }

    private class Transition
    {
        public Func<bool> Condition { get; }
        public IState To { get; }

        public Transition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }

    private Transition GetTransition()
    {
        foreach (var transition in prioTransitions)
            if (transition.Condition())
                return transition;

        foreach (var transition in currentTransitions)
            if (transition.Condition())
                return transition;

        return null;
    }

    public override string ToString()
    {
        return "State machine: " + state.ToString();
    }
}
