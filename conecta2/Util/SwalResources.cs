using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace conecta2.Util
{
    public static class SwalResources
    {
        /// <summary>
        ///  Ubicacion de la librería jQuery. Por defecto se utiliza la CDN
        /// </summary>
        public static string PathJQuery { get; set; } = "https://code.jquery.com/jquery-3.7.1.min.js";

        /// <summary>
        ///  Ubicacion de la librería SweetAlert2. Por defecto se utiliza la CDN de la página
        /// </summary>
        public static string PathSweetAlert { get; set; } = "https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.js";

        /// <summary>
        ///  Ubicacion de la librería SweetAlert2. Por defecto se utiliza la CDN de la página
        /// </summary>
        public static SwalTheme Theme { get; set; } = SwalTheme.Bootstrap4;

        public static string GetHeaders()
        {
            StringBuilder header = new StringBuilder();

            header.AppendLine($"<script src='{PathJQuery}'></script>");
            header.AppendLine($"<link rel='stylesheet' href='{Themes[Theme]}' />");
            header.AppendLine($"<script src='{PathSweetAlert}'></script>");

            return header.ToString();
        }

        public static Dictionary<SwalIcon, string> Icons = new Dictionary<SwalIcon, string>()
        {
            {SwalIcon.Success, "success" },
            {SwalIcon.Error, "error" },
            {SwalIcon.Warning, "warning" },
            {SwalIcon.Info, "info" },
            {SwalIcon.Question, "question" }
        };

        public static Dictionary<SwalTheme, string> Themes = new Dictionary<SwalTheme, string>()
        {
            {SwalTheme.Dark, "https://cdn.jsdelivr.net/npm/@sweetalert2/theme-dark@5/dark.css" },
            {SwalTheme.Minimal, "https://cdn.jsdelivr.net/npm/@sweetalert2/themes@5.0.18/minimal/minimal.css" },
            {SwalTheme.Borderless, "https://cdn.jsdelivr.net/npm/@sweetalert2/themes@5.0.18/borderless/borderless.css" },
            {SwalTheme.Bootstrap4, "https://cdn.jsdelivr.net/npm/@sweetalert2/theme-bootstrap-4/bootstrap-4.css" },
            {SwalTheme.Bulma, "https://cdn.jsdelivr.net/npm/@sweetalert2/themes@5.0.18/bulma/bulma.css" },
            {SwalTheme.MaterialUI, "https://cdn.jsdelivr.net/npm/@sweetalert2/themes@5.0.18/material-ui/material-ui.css" },
            {SwalTheme.WordPressAdmin, "https://cdn.jsdelivr.net/npm/@sweetalert2/themes@5.0.18/wordpress-admin/wordpress-admin.css" }
        };
    }

    public enum SwalIcon : short
    {
        Success,
        Error,
        Warning,
        Info,
        Question,
    }

    public enum SwalTheme : short
    {
        Dark,
        Minimal,
        Borderless,
        Bootstrap4,
        Bulma,
        MaterialUI,
        WordPressAdmin
    }
}