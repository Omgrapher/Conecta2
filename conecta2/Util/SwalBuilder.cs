using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace conecta2.Util
{
    public class SwalBuilder<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public string Build()
        {
            StringBuilder script = new StringBuilder();

            script.AppendLine($"{SwalResources.GetHeaders()}");

            script.AppendLine("<script>");
            script.AppendLine("$(document).ready(function () {");
            script.AppendLine("Swal.fire({");

            foreach (KeyValuePair<TKey, TValue> param in this)
            {
                script.AppendLine($"{param.Key}: '{param.Value}', ");
            }

            script.AppendLine("})");
            script.AppendLine("});");
            script.AppendLine("</script>");

            return script.ToString();
        }
    }
}