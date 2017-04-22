using UnityEngine;
using System.Collections;

public static class InputMap {

    // STRING TAGS FOR THE INPUTS SINCE THEY ARE KINDA
    // HARD TO READ. I CAN CHANGE THEM TO SOMETHING LIKE:
    // TriggerR > ShootProjectile
    // HorizontalL > Rotate
    // WITHOUT CHANGING THE INPUT MANAGER ASSET. (IT CAN GET TIME CONSUMMING)

    // THE INPUTS HANDLE PLAYERS 1-4 AND KEYBOARD CONTROLLING PLAYER 1 FOR TESTING

    public const string HorizontalL = "L_XAxis_";
    public const string VerticalL = "L_YAxis_";
    public const string ButtonA = "A_";
    public const string ButtonB = "B_";
    public const string ButtonY = "Y_";
    public const string ButtonX = "X_";
    public const string ButtonLB = "LB_";
    public const string ButtonRB = "RB_";

    public const string Back = "Back_";
    public const string Start = "Start_";

    public const string StickClickL = "LS_";
    public const string StickClickR = "RS_";

    public const string HorizontalD = "DPad_XAxis_";
    public const string VerticalD = "DPad_YAxis_";

    public const string HorizontalR = "R_XAxis_";
    public const string VerticalR = "R_YAxis_";

    public const string TriggerL = "TriggersL_";
    public const string TriggerR = "TriggersR_";

    public const string GlobalOK = "Submit";
    public const string GlobalCancel = "Cancel";
}
