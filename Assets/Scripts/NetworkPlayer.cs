using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }

    public override void Spawned()
    {
        base.Spawned();
        if (Object.HasInputAuthority)
        {
            Local = this;
            Debug.Log("Spawn local player");
        }
        else
        {
            Debug.Log("Spawned Remote player");
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        if(player == Object.HasInputAuthority)
        {
            Runner.Despawn(Object);
        }
    }
}
