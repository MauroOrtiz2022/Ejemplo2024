using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ejemplo.Helpers
{
    public class StorageHelper
    {
        public const string URL_imagen_default = "https://tacos.blob.core.windows.net/contenedor-taquitos/Captura.PNG";

        public static async Task<string> SubirArchivo(Stream contenido, string nombre, AzureStoreConfig config)
        {
            string url = $"https://{config.Cuenta}.blob.core.windows.net/{config.Contenedor}/{nombre}";
            Uri uri = new Uri(url);
            var credenciales = new StorageSharedKeyCredential(config.Cuenta, config.Llave);
            var cliente = new BlobClient(uri, credenciales);
            await cliente.UploadAsync(contenido);
            return url ;
        }
        public static async Task<bool> EliminarArchivo(string url, AzureStoreConfig config)
        {
            Uri uri = new Uri(url);
            var credenciales = new StorageSharedKeyCredential(config.Cuenta, config.Llave);
            var cliente = new BlobClient(uri, credenciales);
            var respuesta = await cliente.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
            return respuesta.Value;
        }
        public static string GetURLFromFileName(string name, AzureStoreConfig config)
        {
            return $"https://{config.Cuenta}.blob.core.windows.net/{config.Contenedor}/{name}";
        }
    }
}
