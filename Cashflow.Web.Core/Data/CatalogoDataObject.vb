Option Explicit On
Option Strict Off

Imports System.Data
Imports System.Text

Namespace Data

    Public Class CatalogoDataObject
        Inherits BaseDataObject

        Public Sub New()
            Me.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConexionCajerosEmpresariales").ConnectionString
        End Sub

        Public Function GetCatalogoBancosTable() As DataTable

        End Function

        Public Function GetCatalogoCuentasTable() As DataTable

        End Function

        Public Function GetCatalogoCajasBancarias()
            Dim storeProcedure As String = "CE_CatalogoCajasBancarias"
            Dim dt As DataTable

            Try

                dt = ExecuteStoreProcedureDatatable(storeProcedure, Nothing, Nothing)
                Return dt

            Catch ex As Exception

            End Try
        End Function

#Region "Functions"

#End Region

    End Class

End Namespace