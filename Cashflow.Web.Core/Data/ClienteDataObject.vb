Option Explicit On
Option Strict Off

Imports System.Text
Imports SISSA.Cashflow.Web.Core.Entities

Namespace Data

    Public Class ClienteDataObject
        Inherits BaseDataObject

        Public Sub New()
            Me.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("ConexionCajerosEmpresariales").ConnectionString
        End Sub

        Public Function GetCatalogoClientes() As DataTable
            Dim storeProcedure As String = "CE_CatalogoClientes"
            Dim dt As DataTable

            Try

                dt = ExecuteStoreProcedureDatatable(storeProcedure, Nothing, Nothing)
                Return dt

            Catch ex As Exception

            End Try
        End Function

        Public Function GetCatalogoSucursales()
            Dim storeProcedure As String = "CE_CatalogoSucursales"
            Dim dt As DataTable

            Try

                dt = ExecuteStoreProcedureDatatable(storeProcedure, Nothing, Nothing)
                Return dt

            Catch ex As Exception

            End Try
        End Function

        Public Function GetCatalogoSucursalesByCliente(ClaveCliente As String)
            Dim storeProcedure As String = "CE_CatalogoSucursalesByCliente"
            Dim dt As DataTable
            Dim params() As String
            Dim values() As Object

            Try
                params = New String() {"@ClaveCliente"}
                values = New Object() {ClaveCliente}
                dt = ExecuteStoreProcedureDatatable(storeProcedure, params, values)
                Return dt

            Catch ex As Exception

            End Try
        End Function
    End Class

End Namespace