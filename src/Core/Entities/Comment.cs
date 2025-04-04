﻿namespace Core.Entities;

public partial class Comentario
{
    public int IdComentario { get; set; }

    public int IdPublicacion { get; set; }

    public int IdUsuario { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public bool Activo { get; set; }

    public virtual Post IdPostNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
