using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;



namespace Compensation.Client.Helpers
{
    public static class IJSExtensions
    {

        public static ValueTask<object> GuardarComo(this IJSRuntime js, string nombreArchivo, byte[] archivo)
        {
            return js.InvokeAsync<object>("saveAsFile",
                nombreArchivo,
                Convert.ToBase64String(archivo));
        }


        public static ValueTask<object> MostrarMensaje(this IJSRuntime js, string mensaje)
        {
            return js.InvokeAsync<object>("Swal.fire", mensaje);
        }

        public static ValueTask<object> MostrarMensaje(this IJSRuntime js, string titulo, string mensaje, TipoMensajeSweetAlert tipoMensajeSweetAlert)
        {
            return js.InvokeAsync<object>("Swal.fire", titulo, mensaje, tipoMensajeSweetAlert.ToString());
        }

        public async static ValueTask<bool> Confirm(this IJSRuntime js, string titulo, string mensaje, TipoMensajeSweetAlert tipoMensajeSweetAlert)
        {
            return await js.InvokeAsync<bool>("CustomConfirm", titulo, mensaje, tipoMensajeSweetAlert.ToString());
        }

        public async static Task SetInLocalStorage(this IJSRuntime js, string key, string content)
            => await js.InvokeAsync<object>(
            "localStorage.setItem",
            key, content
        );

        public  static async Task<string> GetFromLocalStorage(this IJSRuntime js, string key)
            => await js.InvokeAsync<string>(
                "localStorage.getItem",
                key
                );

        public async static Task RemoveItem(this IJSRuntime js, string key)
            => await js.InvokeAsync<object>(
                "localStorage.removeItem",
                key);



        public enum TipoMensajeSweetAlert
        {
            question, warning, error, success, info
        }


   
    
    
    }





}
