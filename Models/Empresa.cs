﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AFP_WEBAPI_PRUEBA.Models;

[Table("EMPRESA")]
public partial class Empresa
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [Column("NOMBRE")]
    [StringLength(300)]
    public string Nombre { get; set; }

    [Required]
    [Column("RAZON")]
    [StringLength(300)]
    public string Razon { get; set; }

    [Column("FECHA_REGISTRO", TypeName = "datetime")]
    public DateTime? FechaRegistro { get; set; }

    [Column("FECHA_CREACION", TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [Required]
    [Column("USR_CREACION")]
    [StringLength(50)]
    public string UsrCreacion { get; set; }

    [InverseProperty("Empresa")]
    public virtual ICollection<Departamento> Departamento { get; } = new List<Departamento>();
}