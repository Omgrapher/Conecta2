using System;
using System.Collections.Generic;

namespace ESP8266.Models;

public partial class Usuario
{
    public int id { get; set; }

    public string username { get; set; } = null!;

    public string password { get; set; } = null!;
}
