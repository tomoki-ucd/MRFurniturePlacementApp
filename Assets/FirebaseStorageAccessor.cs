using UnityEngine;
using Firebase.Storage;

public class FirebaseStorageAccessor : MonoBehaviour
{
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

    }
}
