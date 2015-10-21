using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TroopStateController
{
    //TroopState initialState;
    TroopState activeState;
    TroopOnField troop;
    List<TroopState> states;

    public TroopStateController(TroopOnField troop)
    {
        //Declare all states and put them in List<TroopState>
        Standby s = new Standby();
        March m = new March();

        //Declare all possible transitions
        T_S_M t_s_m = new T_S_M(s, m);
        T_M_S t_m_s = new T_M_S(m, s);

        //Create transition list for each state
        List<Transition> transition_s = new List<Transition>();
        transition_s.Add(t_s_m);
        List<Transition> transition_m = new List<Transition>();
        transition_m.Add(t_m_s);

        //Put relevant transitions in each of the state
        s.initialize(troop, transition_s);
        m.initialize(troop, transition_m);

        //Set up the first state
        activeState = s;
    }

    public void Update()
    {
        bool validTransition = false;
        foreach(Transition t in activeState.transitions)
        {
            if (t.isValid())
            {
                Debug.Log("changing state");
                activeState.onExit();
                activeState = t.nextState;
                activeState.onEnter();
                validTransition = true;
                break;
            }
        }
        if (!validTransition)
            activeState.Update();
    }
}


public class TroopState
{
    public List<Transition> transitions;
    public TroopOnField troop;

    public virtual void onEnter() { }
    public virtual void Update() { }
    public virtual void onExit() { }
    public virtual bool checkTaskDone() { return false; } //check if done. If yes go to Standby Mode

    public void initialize(TroopOnField t, List<Transition> transitions)
    {
        this.troop = t;
        this.transitions = transitions;
    }
}

public class Standby : TroopState
{
    public override void onEnter()
    {
        troop.target = null;
        base.onEnter();
    }

    public override void Update()
    {
        //Actually does nothing
        Debug.Log("In Standby Update");
    }
}

public class March : TroopState
{
    private bool arrived;
    private int turn_is_clockwise;
    private bool turned;
    public Target target { get { return troop.target; } }
    private Target oldTarget;

    public override void onEnter()
    {
        base.onEnter();
        arrived = false;
        turned = false;
        oldTarget = target;
        turn_is_clockwise = Vector3.Cross(troop.direction, (target.targetPos - troop.transform.position)).z < 0 ? 1 : -1;
    }

    public override void Update()
    {
        if (oldTarget != target)
        {
            Debug.Log(troop.Name + "'s target has changed! from " + oldTarget.targetPos + " to " + target.targetPos);
            arrived = false;
            turned = false;
            turn_is_clockwise = Vector3.Cross(troop.direction, (target.targetPos - troop.transform.position)).z < 0 ? 1 : -1;
            oldTarget = target;
        }

        if (troop.target != null && troop.target.targetPos != null)
        {
            moveTowardsTarget();
        }
        Debug.Log("In March Update with arrived: "+arrived+" "+turned);
    }

    public override bool checkTaskDone()
    {
        return arrived && turned;
    }

    void moveTowardsTarget()
    {
        if (!arrived)
        {
            //Displacement
            Vector3 displacement = (target.targetPos - troop.gameObject.transform.position).normalized * BattleFieldManager.deltaTime * troop.troop_stat.Type.MarchSpeed;
            //check if distance already close
            if (Vector3.Distance(target.targetPos, troop.transform.position) < displacement.magnitude)
            {
                troop.transform.position = target.targetPos;
                arrived = true;
            }
            else troop.transform.position += displacement;
        }

        //Turn Angle
        if (!turned)
        {
            //Debug.Log("Current Direction: " + troop.direction + " target direction " + (target.targetPos - troop.transform.position) + " has angle " + Vector3.Angle(direction, (target.targetPos - this.transform.position).normalized));
            if (Vector3.Angle(troop.direction, (target.targetPos - troop.transform.position).normalized) < TroopOnField.turnAngle)
            {
                troop.direction = (target.targetPos - troop.transform.position).normalized;
                turned = true;
            }
            else troop.direction = (Quaternion.Euler(0, 0, -turn_is_clockwise * TroopOnField.turnAngle) * troop.direction).normalized;
        }
    }
}


