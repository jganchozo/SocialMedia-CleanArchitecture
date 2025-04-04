﻿namespace Core.Entities;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string? Telefono { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Comment> Comentario { get; set; } = new List<Comment>();

    public virtual ICollection<Post> Publicacion { get; set; } = new List<Post>();
}
