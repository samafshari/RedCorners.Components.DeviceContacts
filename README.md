## Setting Up / Permissions

### iOS

#### iOS info.plist:

```xml
<key>NSContactsUsageDescription</key>
<string>This app requires access to contacts.</string>
```

### Android

#### Android Manifest:

```xml
<manifest xmlns:android="http://schemas.android.com/apk/res/android"
...>
  <application ...>...</application>
  <uses-permission android:name="android.permission.READ_CONTACTS" />
</manifest>
```

#### MainActivity.cs

```cs
protected override void OnCreate(Bundle savedInstanceState)
{
  ...
  global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
  DeviceContacts.Init(this);
  LoadApplication(new App());
  ...
}

public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
{
  DeviceContacts.OnRequestPermissionsResult(requestCode, permissions, grantResults);
  ...
  base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
}
```

## Listing Contacts

```cs
var provider = new DeviceContacts();
var contacts = await contacts.GetAllAsync();
```

It's strongly advised that you check for exceptions there, because `GetAllAsync` throws exceptions if the permission to read contacts is not granted:

```cs
try
{
  var provider = new DeviceContacts();
  var contacts = await contacts.GetAllAsync();
}
catch (Exception ex)
{
  App.Instance.DisplayAlert("Error", ex.ToString(), "OK");
}
```

## Asking for permissions

When you call `await contacts.GetAllAsync()`, the library automatically displays the permission request dialog.
