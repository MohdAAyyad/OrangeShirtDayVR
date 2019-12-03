using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
     void BeingInteractedWith(); //Called by children when the player is interacting with them.
     void LookedAway(); //Called by children when the player looks away from the button
}
