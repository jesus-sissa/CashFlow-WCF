Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Entities
Imports SISSA.Cashflow.Web.Core.Data.Interfaces
Imports System.Data.SqlClient

Namespace Data

    Public Class ConnectionClientDataObject
        Inherits BaseDataObject

        Public Function GetConnectionStringByRFC(RFC As String) As ConnectionClientEntityObject
            Dim sql As String
            Dim dt As DataTable

            Dim _connectionInfo As ConnectionClientEntityObject = Nothing

            Try
                sql = String.Format("select CUNICA,CCLIENTE,NCOMERCIAL,SVR,BD,USR,PWD,TIPO_CONEXION,DOMICILIO,[STATUS] " &
                                     "from CNNCTES " &
                                     "where CUNICA = '{0}' " &
                                     "and TIPO_CONEXION = 1;", RFC)

                dt = ExecuteDataTable(sql, Nothing, Nothing)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    _connectionInfo = GetEntityObject(dt.Rows(0))
                End If

                Return _connectionInfo

            Catch ex As Exception

            End Try
        End Function

        Public Function GetAllConnectionObjects() As List(Of ConnectionClientEntityObject)
            Dim sql As String
            Dim dt As DataTable

            Dim _connectionsList As List(Of ConnectionClientEntityObject)
            Dim _connectionInfo As ConnectionClientEntityObject = Nothing

            Try
                sql = String.Format("select CUNICA,CCLIENTE,NCOMERCIAL,SVR,BD,USR,PWD,TIPO_CONEXION,DOMICILIO,[STATUS] " &
                                     "from CNNCTES " &
                                     "where TIPO_CONEXION = 1;", "")

                dt = ExecuteDataTable(sql, Nothing, Nothing)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    _ConnectionsList = New List(Of ConnectionClientEntityObject)

                    For Each row As DataRow In dt.Rows
                        _connectionInfo = GetEntityObject(row)
                        _connectionsList.Add(_connectionInfo)
                    Next

                End If

                Return _connectionsList

            Catch ex As Exception

            End Try
        End Function

        Private Function GetEntityObject(row As DataRow) As ConnectionClientEntityObject
            Dim _connectionInfo As ConnectionClientEntityObject

            If row IsNot Nothing Then
                _ConnectionInfo = New ConnectionClientEntityObject()

                With _connectionInfo
                    .RFC = row("CUNICA").ToString
                    .NombreComercial = row("NCOMERCIAL").ToString
                    .DatabaseServer = row("SVR").ToString
                    .DatabaseUser = row("USR").ToString
                    .DatabasePassword = row("PWD").ToString
                    .DatabaseName = row("BD").ToString

                    'Se desencriptan los valores de registro de conexion
                    .DatabaseServer = Tools.DecodeData(.DatabaseServer)
                    .DatabaseUser = Tools.DecodeData(.DatabaseUser)
                    .DatabasePassword = Tools.DecodeData(.DatabasePassword)
                    .DatabaseName = Tools.DecodeData(.DatabaseName)
                End With
            End If

            Return _connectionInfo
        End Function
    End Class

End Namespace