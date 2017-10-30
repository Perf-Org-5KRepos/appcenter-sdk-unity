// Copyright (c) Microsoft Corporation. All rights reserved.
//
// Licensed under the MIT license.

using System.Collections;
using Microsoft.AppCenter.Unity;
using UnityEngine;
using UnityEngine.UI;

public class DemoMobileCenter : MonoBehaviour
{
    public Toggle Enabled;
    public Dropdown LogLevel;

    void OnEnable()
    {
        MobileCenter.IsEnabledAsync().ContinueWith(task =>
        {
            Enabled.isOn = task.Result;
        });
        MobileCenter.GetInstallIdAsync().ContinueWith(task =>
        {
            if (task.Result.HasValue)
            {
                print("Install ID = " + task.Result.ToString());
            }
        });
        LogLevel.value = MobileCenter.LogLevel - Microsoft.AppCenter.Unity.LogLevel.Verbose;
    }

    public void SetEnabled(bool enabled)
    {
        StartCoroutine(SetEnabledCoroutine(enabled));
    }

    private IEnumerator SetEnabledCoroutine(bool enabled)
    {
        yield return MobileCenter.SetEnabledAsync(enabled);
        var isEnabled = MobileCenter.IsEnabledAsync();
        yield return isEnabled;
        Enabled.isOn = isEnabled.Result;
    }

    public void SetLogLevel(int logLevel)
    {
        MobileCenter.LogLevel = Microsoft.AppCenter.Unity.LogLevel.Verbose + logLevel;
    }
}
