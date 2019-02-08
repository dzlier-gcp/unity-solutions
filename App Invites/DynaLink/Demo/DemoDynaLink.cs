using System.Collections;
using System.Collections.Generic;
using Firebase;
using UnityEngine;

public class DemoDynaLink : MonoBehaviour
{

    Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

    private bool readyToInitialize;
    private Firebase.DependencyStatus dependencyStatus;

    // Use this for initialization
    void Start()
    {
        //Firebase.Leaderboard.FirebaseInitializer.Initialize(dependencyStatus => {
        //    if (dependencyStatus == DependencyStatus.Available)
        //    {
        //        Debug.Log("Firebase database ready.");
        //        AnonSignIn();
        //        readyToInitialize = true;
        //    }
        //    else
        //    {
        //        Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
        //    }
        //});
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log("Dependency Status Available");

            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AnonSignIn()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }
}
