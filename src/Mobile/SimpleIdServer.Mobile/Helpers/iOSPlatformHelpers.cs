﻿namespace SimpleIdServer.Mobile.Helpers;

public class iOSPlatformHelpers : IPlatformHelpers
{
    public Task<PermissionStatus> CheckAndRequestBluetoothPermissions()
    {
        return Task.FromResult(PermissionStatus.Granted);
    }
}
