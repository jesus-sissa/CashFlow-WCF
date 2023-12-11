Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Data

Namespace Business

    Public Class CatalogoBusinessObject

        Private _CatalogoDAO As CatalogoDataObject

#Region "Properties"
        Private Property _connectionString As String

        Public Property ConnectionString As String
            Get
                Return _connectionString
            End Get
            Set(value As String)
                _connectionString = value
                If _CatalogoDAO IsNot Nothing Then
                    _CatalogoDAO.ConnectionString = _connectionString
                End If
            End Set
        End Property
#End Region

#Region "Constructor"
        Public Sub New()
            _CatalogoDAO = New CatalogoDataObject()
        End Sub

        Public Sub New(ConnectionString As String)
            _connectionString = ConnectionString
            _CatalogoDAO = New CatalogoDataObject()
            _CatalogoDAO.ConnectionString = _connectionString
        End Sub
#End Region

#Region "Methods"
        Public Function GetCatalogoBancosTable() As DataTable
            Return _CatalogoDAO.GetCatalogoBancosTable
        End Function

        Public Function GetCatalogoCuentasTable() As DataTable
            Return _CatalogoDAO.GetCatalogoCuentasTable
        End Function

        Public Function GetCatalogoCajasBancarias()
            Return _CatalogoDAO.GetCatalogoCajasBancarias
        End Function
#End Region



    End Class

End Namespace