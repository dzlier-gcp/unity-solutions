using Firebase.DynamicLinks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Firebase.DynaLinks {
  public class Invite {
    public static System.Uri baseLink;
    public static string kDynamicLinkDomain;
    public static Firebase.DynamicLinks.IOSParameters iOSParameters;
    public static Firebase.DynamicLinks.AndroidParameters androidParameters;

    public Invite(System.Uri uri, string domain, Firebase.DynamicLinks.IOSParameters ios = null, Firebase.DynamicLinks.AndroidParameters and = null) {
      baseLink = uri;
      kDynamicLinkDomain = domain;
      iOSParameters = ios;
      androidParameters = and;
    }

    // Create Invite(via dynamic links)
    public DynamicLinkComponents CreateLongLinkInvite() {
      return new Firebase.DynamicLinks.DynamicLinkComponents(baseLink, kDynamicLinkDomain) {
        IOSParameters = iOSParameters,
        AndroidParameters = androidParameters,
      };
    }

    // Create Invite w/ Components
    public IEnumerator CreateLongLink() {
      Debug.Log("Creating long dynamic link.");
      var components = CreateLongLinkInvite();
      Debug.LogFormat("Long dynamic link created: {0}\nCreating short link...", components.LongDynamicLink.ToString());
      var shortLinkRequest = Firebase.DynamicLinks.DynamicLinks.GetShortLinkAsync(components);
      while (!shortLinkRequest.IsCompleted && !shortLinkRequest.IsCanceled && !shortLinkRequest.IsFaulted) {
        yield return null;
      }
      if (shortLinkRequest.IsCanceled) {
        Debug.LogWarning("Creating short link canceled. " + shortLinkRequest.ToString());
        yield break;
      }
      if (shortLinkRequest.IsFaulted) {
        Debug.LogWarning("Creating short link fauled. " + shortLinkRequest.ToString());
        yield break;
      }
      var link = shortLinkRequest.Result;
      Debug.LogFormat("Short dynamic link created: {0}.", link.Url.ToString());
      var warnings = new System.Collections.Generic.List<string>(link.Warnings);
      foreach (var warning in warnings) {
        Debug.LogWarning("Warning while creating short link: " + warning);
      }
    }
  }
}
