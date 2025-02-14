# Extreal.SampleApp.Holiday

## How to play

- This application uses Vivox for voice and text chat. Create an account on [Vivox Developer Portal](https://developer.vivox.com/) and create an application to connect to from this application.
- Clone the repository.
- Open the cloned directory in the Unity editor.
  - If "Link your Unity project" appears, close it without setting it.
- Refer to the following page to import Mixamo model files into your project.
  - [Mixamoの無料3DモデルをUnityにインポートする方法](https://zenn.dev/gaku_moriya/articles/d1b451b288786b)
    - Please implement from "3Dモデルを入手する" to "Materialの最適化".
    - No animation required.
    - Please import "Amy" and "Michelle" from Mixamo into the following path.
      - /Assets/Mixamo/Amy
      - /Assets/Mixamo/Michelle
    - Rename FBX files to their respective avatar names (e.g. Amy.fbx).
- Create avatar prefabs into the `/Assets/Holiday.MultiplayCommon` directory.
  - Duplicate the `AvatarArmature` prefab and rename it to "AvatarAmy".
  - Remove `Geometry` under `AvatarAmy` in the Hierarchy.
  - Drag and drop `Amy.fbx` under `AvatarAmy` in Hierarchy and unpack completely.
  - Remove the `Animator` component of Amy in Inspector.
  - Change `Avatar` in the `Animator` component in `AvatarAmy` to `AmyAvatar`.
  - Add the `AvatarAmy` asset to Addressables with the name `AvatarAmy`.
  - Create an avatar prefab about `Michelle` in the same way as above.
- Set the prefabs above to `NetworkPrefabs` in the `NetworkManager` component in the `NetworkManager` prefab.
- Create a `ChatConfig` from the Create Assets menu in the `/Assets/Holiday/App/Config` directory and set the Vivox access information in the inspector.
  - Path in the Create Assets menu: `Holiday > ChatConfig`
- Create a `MultiplayConfig` from the Create Assets menu in the `/Assets/Holiday/App/Config` directory. Default values are set so there is no need to set them in the inspector.
  - Path in the Create Assets menu: `Holiday > MultiplayConfig`
- Open `/Assets/Holiday/App/App` scene and set `ChatConfig` and `MultiplayConfig` in the `Scope` object inspector.
- Open multiple Unity editors using [ParrelSync.](https://github.com/VeriorPies/ParrelSync). ParrelSync is already installed in this project.
- Run a multiplayer server.
  - Run the following scene: `/Assets/Holiday.MultiplayServer/MultilayServer`
- Run the application.
  - Runs the following scene: `/Assets/Holiday/App/App`
- Enjoy playing!
