using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;

public class FireAuth : MonoBehaviour {

    Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

    private bool readyToInitialize;
    private Firebase.DependencyStatus dependencyStatus;

    // Use this for initialization
    void Start () {
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
	void Update () {
        

    }

    //private void emailPassSignUp()
    //{
    //    auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
    //        if (task.IsCanceled)
    //        {
    //            Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
    //            return;
    //        }
    //        if (task.IsFaulted)
    //        {
    //            Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
    //            return;
    //        }

    //        // Firebase user has been created.
    //        Firebase.Auth.FirebaseUser newUser = task.Result;
    //        Debug.LogFormat("Firebase user created successfully: {0} ({1})",
    //            newUser.DisplayName, newUser.UserId);
    //    });
    //}

    //private void emailPassSignIn()
    //{
    //    auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
    //        if (task.IsCanceled)
    //        {
    //            Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
    //            return;
    //        }
    //        if (task.IsFaulted)
    //        {
    //            Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
    //            return;
    //        }

    //        Firebase.Auth.FirebaseUser newUser = task.Result;
    //        Debug.LogFormat("User signed in successfully: {0} ({1})",
    //            newUser.DisplayName, newUser.UserId);
    //    });
    //}

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

    //private void GoogleSignIn()
    //{
    //    Firebase.Auth.Credential credential =
    //Firebase.Auth.GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
    //    auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
    //        if (task.IsCanceled)
    //        {
    //            Debug.LogError("SignInWithCredentialAsync was canceled.");
    //            return;
    //        }
    //        if (task.IsFaulted)
    //        {
    //            Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
    //            return;
    //        }

    //        Firebase.Auth.FirebaseUser newUser = task.Result;
    //        Debug.LogFormat("User signed in successfully: {0} ({1})",
    //            newUser.DisplayName, newUser.UserId);
    //    });
    //}

    //public void FirebaseTwitterLogin()
    //{
    //    Firebase.Auth.Credential credential =
    //Firebase.Auth.TwitterAuthProvider.GetCredential(accessToken, secret);
    //    auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
    //        if (task.IsCanceled)
    //        {
    //            Debug.LogError("SignInWithCredentialAsync was canceled.");
    //            return;
    //        }
    //        if (task.IsFaulted)
    //        {
    //            Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
    //            return;
    //        }

    //        Firebase.Auth.FirebaseUser newUser = task.Result;
    //        Debug.LogFormat("User signed in successfully: {0} ({1})",
    //            newUser.DisplayName, newUser.UserId);
    //    });
    //}
}
