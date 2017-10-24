using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindings : MonoBehaviour {

    private static KeyCode forwardKey = KeyCode.W;
    private static KeyCode leftKey = KeyCode.A;
    private static KeyCode backwardKey = KeyCode.S;
    private static KeyCode rightKey = KeyCode.D;
    private static KeyCode jumpKey = KeyCode.Space;
    private static KeyCode crouchKey = KeyCode.LeftControl;
    private static KeyCode sprintKey = KeyCode.LeftShift;
    private static KeyCode shootKey = KeyCode.Mouse0;
    private static KeyCode focusAimKey = KeyCode.Mouse1;
    private static KeyCode meleeKey = KeyCode.V;
    private static KeyCode reloadKey = KeyCode.R;

    public static KeyCode ForwardKey
    {
        get
        {
            return forwardKey;
        }

        set
        {
            forwardKey = value;
        }
    }

    public static KeyCode LeftKey
    {
        get
        {
            return leftKey;
        }

        set
        {
            leftKey = value;
        }
    }

    public static KeyCode BackwardKey
    {
        get
        {
            return backwardKey;
        }

        set
        {
            backwardKey = value;
        }
    }

    public static KeyCode RightKey
    {
        get
        {
            return rightKey;
        }

        set
        {
            rightKey = value;
        }
    }

    public static KeyCode JumpKey
    {
        get
        {
            return jumpKey;
        }

        set
        {
            jumpKey = value;
        }
    }

    public static KeyCode CrouchKey
    {
        get
        {
            return crouchKey;
        }

        set
        {
            crouchKey = value;
        }
    }

    public static KeyCode SprintKey
    {
        get
        {
            return sprintKey;
        }

        set
        {
            sprintKey = value;
        }
    }

    public static KeyCode ShootKey
    {
        get
        {
            return shootKey;
        }

        set
        {
            shootKey = value;
        }
    }

    public static KeyCode FocusAimKey
    {
        get
        {
            return focusAimKey;
        }

        set
        {
            focusAimKey = value;
        }
    }

    public static KeyCode MeleeKey
    {
        get
        {
            return meleeKey;
        }

        set
        {
            meleeKey = value;
        }
    }

    public static KeyCode ReloadKey
    {
        get
        {
            return reloadKey;
        }

        set
        {
            reloadKey = value;
        }
    }
}
