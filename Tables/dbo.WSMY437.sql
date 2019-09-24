CREATE TABLE [dbo].[WSMY437]
(
[IdSubCategoria] [int] NOT NULL,
[Descripcion] [varchar] (50) COLLATE Modern_Spanish_CI_AS NULL,
[IdCategoria] [int] NULL,
[IdProductoCuota] [int] NULL,
[ValidaMC] [char] (1) COLLATE Modern_Spanish_CI_AS NULL CONSTRAINT [DF_WSMY437_ValidaMC_1] DEFAULT (''),
[DiasCorreo] [int] NULL,
[PorcCYPJ] [decimal] (18, 4) NULL,
[PorcCYPJMinimo] [decimal] (18, 4) NULL,
[TipoMaterialSAP] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[GrupoArticulo] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[Sector] [nvarchar] (2) COLLATE Modern_Spanish_CI_AS NULL,
[Imputacion] [nvarchar] (2) COLLATE Modern_Spanish_CI_AS NULL,
[TipoPosicion] [nvarchar] (4) COLLATE Modern_Spanish_CI_AS NULL,
[GrupoMaterial] [nvarchar] (2) COLLATE Modern_Spanish_CI_AS NULL,
[GrupoMaterial1] [nvarchar] (3) COLLATE Modern_Spanish_CI_AS NULL,
[GrupoMaterial2] [nvarchar] (3) COLLATE Modern_Spanish_CI_AS NULL,
[GrupoMaterial3] [nvarchar] (3) COLLATE Modern_Spanish_CI_AS NULL,
[CentroDeBeneficio] [nvarchar] (10) COLLATE Modern_Spanish_CI_AS NULL,
[IndicadorABC] [nvarchar] (1) COLLATE Modern_Spanish_CI_AS NULL,
[PlanificacionNecesidades] [nvarchar] (3) COLLATE Modern_Spanish_CI_AS NULL,
[CategoriaValoracion] [nvarchar] (4) COLLATE Modern_Spanish_CI_AS NULL,
[TipoValoracion] [nvarchar] (1) COLLATE Modern_Spanish_CI_AS NULL,
[IndicadorControldePrecios] [nvarchar] (1) COLLATE Modern_Spanish_CI_AS NULL,
[PorcCYPJGob] [decimal] (18, 4) NULL,
[PorcCYPJGobMinimo] [decimal] (18, 4) NULL,
[IdTablaFlatComision] [int] NULL,
[IdCuotaComision] [int] NULL
)
GO
ALTER TABLE [dbo].[WSMY437] ADD CONSTRAINT [PK_WSMY437] PRIMARY KEY CLUSTERED  ([IdSubCategoria])
GO
EXEC sp_addextendedproperty N'MS_Description', N'Flag validación mc', 'SCHEMA', N'dbo', 'TABLE', N'WSMY437', 'COLUMN', N'ValidaMC'
GO
