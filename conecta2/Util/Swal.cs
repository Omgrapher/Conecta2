using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace conecta2.Util
{
    /// <summary>
    ///  Alerta de SweetAlert2
    /// </summary>
    public static class Swal
    {
        /// <summary>
        /// Muestra una alerta solamente con descripcion
        /// </summary>
        public static string Fire(string text, string redirectUrl)
        {
            StringBuilder alert = new StringBuilder();

            alert.AppendLine($"{SwalResources.GetHeaders()}");

            alert.AppendLine("<script>");
            alert.AppendLine("$(document).ready(function () {");

            alert.AppendLine($"Swal.fire({{text: '{text}', showConfirmButton: false}})");
            // Redirigir después de 2 segundos
            alert.AppendLine("});");
            alert.AppendLine("</script>");

            return alert.ToString();
        }

        /// <summary>
        /// Muestra una alerta con descripcion y titulo
        /// </summary>
        public static string Fire(string text, string title, SwalIcon icon)
        {
            StringBuilder alert = new StringBuilder();

            alert.AppendLine($"{SwalResources.GetHeaders()}");

            alert.AppendLine("<script>");
            alert.AppendLine("$(document).ready(function () {");

            alert.AppendLine($"Swal.fire({{title: '{title}', text: '{text}', icon: '{SwalResources.Icons[icon]}'}})");

            alert.AppendLine("});");
            alert.AppendLine("</script>");

            return alert.ToString();
        }

        /// <summary>
        /// Muestra una alerta con icono el cual puede ser Success, Error, Info, Warning y Question
        /// </summary>
        public static string Fire(string text, string title, SwalIcon icon, string redirectUrl)
        {
            StringBuilder alert = new StringBuilder();

            alert.AppendLine($"{SwalResources.GetHeaders()}");

            alert.AppendLine("<script>");
            alert.AppendLine("$(document).ready(function () {");

            alert.AppendLine($"Swal.fire({{title: '{title}', text: '{text}', icon: '{SwalResources.Icons[icon]}'}})");
            alert.AppendLine($"setTimeout(function() {{ window.location.href = '{redirectUrl}'; }}, 2000);");

            alert.AppendLine("});");
            alert.AppendLine("</script>");

            return alert.ToString();
        }

        public static string FireWithParams(Action<Dictionary<string, string>> parms)
        {
            SwalBuilder<string, string> builder = new SwalBuilder<string, string>();

            parms(builder);

            return builder.Build();
        }
    }
}