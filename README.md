  ## Setting Up / Permissions

  #### iOS info.plist:
  
  ```xml
  <key>NSContactsUsageDescription</key>
  <string>This app requires access to contacts.</string>
  ```

  #### Android Manifest:

  ```xml
<manifest xmlns:android="http://schemas.android.com/apk/res/android"
  ...>
	<application ...>...</application>
	<uses-permission android:name="android.permission.READ_CONTACTS" />
</manifest>
```

## Listing Contacts

```c#
var provider = new DeviceContacts(); //iOSMainActivity
var contacts = contacts.GetAll();
```

