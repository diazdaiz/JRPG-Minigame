using UnityEngine;

//Note: ini caraku untuk memudahkan akses ke komponen-komponen umum di game, temporary code aja.
//      Nanti bisa menggunakan struktur/susunan code yang sudah ada.
public class Game : MonoBehaviour {
    public static DataManager Data;
    public static SceneManager Scene;
    public static AudioManager Audio;
    public static CameraManager Camera;

    private void Awake() {
        Data = GetComponentInChildren<DataManager>();
        Scene = GetComponentInChildren<SceneManager>();
        Audio = GetComponentInChildren<AudioManager>();
        Camera = GetComponentInChildren<CameraManager>();
    }
}
