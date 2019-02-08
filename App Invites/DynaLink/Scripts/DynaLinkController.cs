using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.DynamicLinks;
using Firebase.Leaderboard;
using System;
using Firebase;
using System.Threading.Tasks;

namespace Firebase.DynaLinks {
  public class DynaLinkController : MonoBehaviour {
    // Boolean to check state of Firebase Initialization
    private bool canCreateLink;
    private bool readyToSignin;
    private bool userSignedIn;

    // Firebase Dynamic Link Domain
    public string kDynamicLinksDomain;
    public string iOSPackageName;
    public string AndroidPackageName;

    public bool firebaseInitialized = false;
    private Firebase.Auth.FirebaseUser localUser;

    Firebase.Auth.FirebaseAuth auth;

    private void Start() {
      //// Utilizing Firebase Initializer from Leaderboard example
      FirebaseInitializer.Initialize(dependencyStatus => {
        if (dependencyStatus == DependencyStatus.Available) {
          auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
          InitializeFirebase();
          DynamicLinks.DynamicLinks.DynamicLinkReceived += OnDynamicLink;
          readyToSignin = true;
        } else {
          Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
        }
      });
    }

    private void Update() {
      if (Input.GetKeyDown(KeyCode.Escape)) {
        Application.Quit();
      }

      if (readyToSignin) {
        readyToSignin = false;
        StartCoroutine(AnonSignIn());
      }

      if (canCreateLink) {
        canCreateLink = false;
        Invite inv = CreateInvite();
        StartCoroutine(inv.CreateLongLink());
      }
    }

    private Invite CreateInvite() {
      var androidParams = String.IsNullOrEmpty(AndroidPackageName) ? null : new AndroidParameters(AndroidPackageName);
      var iosParams = String.IsNullOrEmpty(iOSPackageName) ? null : new IOSParameters(iOSPackageName);
      return new Invite(new System.Uri("/dyna-demo"), kDynamicLinksDomain, iosParams, androidParams);
    }

    // Display the dynamic link received by the application.
    private void OnDynamicLink(object sender, EventArgs args) {
      var dynamicLinkEventArgs = args as DynamicLinks.ReceivedDynamicLinkEventArgs;
      Debug.LogFormat("Received dynamic link {0}",
                      dynamicLinkEventArgs.ReceivedDynamicLink.Url.OriginalString);
    }

    private void InitializeFirebase() {
      DynamicLinks.DynamicLinks.DynamicLinkReceived += OnDynamicLink;
      firebaseInitialized = true;
    }

    private IEnumerator AnonSignIn() {
      var task = auth.SignInAnonymouslyAsync();
      while (!task.IsCompleted && !task.IsCanceled && !task.IsFaulted) {
        yield return null;
      }

      if (task.IsCanceled) {
        Debug.LogError("SignInAnonymouslyAsync was canceled.");
        yield break;
      }
      if (task.IsFaulted) {
        Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
        yield break;
      }

      localUser = task.Result;
      Debug.LogFormat("User signed in successfully: {0} ({1})",
          localUser.DisplayName, localUser.UserId);
      canCreateLink = true;
    }
    // Get Friends/Followers List from Auth Providers(Move to Auth when complete)


  }
}
