using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Linq;
using System;

public class NetworkRunnerHandler : MonoBehaviour
{
    public NetworkRunner networkRunnerPr;
    NetworkRunner networkRunner;

    // Start is called before the first frame update
    void Start() 
    {
        networkRunner = Instantiate(networkRunnerPr);
        networkRunner.name = "Network Runner";

        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient, NetAddress.Any() ,SceneManager.GetActiveScene().buildIndex, null);

        Debug.Log($"Server has started");
    }
    
    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, NetAddress netAddress, SceneRef scene, Action<NetworkRunner> initialized)
    {
        var sceneMgr = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();
        if (sceneMgr != null) 
        {
            sceneMgr = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }
        runner.ProvideInput = true;

        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Address = netAddress,
            Scene = scene,
            SessionName = "Test Room",
            Initialized = initialized,
            SceneManager = sceneMgr
        });
    }
}
