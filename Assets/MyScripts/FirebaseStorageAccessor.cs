using UnityEngine;
using Firebase.Storage;
using Firebase.Extensions;

public class FirebaseStorageAccessor : MonoBehaviour
{
    private void Start()
    {
        RetrieveFurnitureData();
    }

    public void RetrieveFurnitureData()
    {
        // Get a reference to the storage service, using the default Firebase App
        // Firebase.Storage.FirebaseStorage is the entry point for all Firebase Storage operations
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        // Create a storage reference from the storage service
        // gs stands for google storage
        StorageReference storageRef = storage.GetReferenceFromUrl("gs://my-first-test-project-df530.firebasestorage.app");

        // Create a child reference
        // Child ref allows to point to a specific object in the storage bucket
        StorageReference prefabRef = storageRef.Child("prefabs");

        // Creaet a child reference to point to bookshelf.prefab
        // ChildReferece can also take paths delimited by '/' such as "prefabs/bookshelf.prefab"
        StorageReference bookshelfRef = prefabRef.Child("bookshelf.prefab");

        // Create a fullpath instead of using child reference
        StorageReference bookshelfFullPathRef = storageRef.Child("prefabs/bookshelf.prefab");

        // Navigation with Parent, Child and Root is supported. I don't explain it here.

        // You can get Reference path and name in string.
        // string path = bookshelfRef.Path; // "prefabs/bookshelf.prefab"
        // string name = bookshelfRef.Name; // "bookshelf.prefab"
        // string bucketPath = bookshelfRef.Bucket;

        // Download the file
        // GetDownloadUrlAsyncはURLをダウンロードしているだけで、ファイルをダウンロードしているわけではない
        // storageRefをURLに変換するにはGetDownloadUrlAsyncを使う
        storageRef.GetDownloadUrlAsync().ContinueWithOnMainThread(task => {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log("Download URL:" + task.Result);
            }
            else
            {
                Debug.Log("GetDownloadUrlAsync failed");
            }
        });
    }
}
