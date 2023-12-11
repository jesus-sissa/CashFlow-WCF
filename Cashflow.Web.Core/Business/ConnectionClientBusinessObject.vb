Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Data
Imports SISSA.Cashflow.Web.Core.Entities

Namespace Business

    Public Class ConnectionClientBusinessObject

        Private _ConnectionDAL As ConnectionClientDataObject

#Region "Properties"
        Private Property _connectionString As String

        Public Property ConnectionString As String
            Get
                Return _connectionString
            End Get
            Set(value As String)
                _connectionString = value
            End Set
        End Property
#End Region

#Region "Constructor"
        Public Sub New()
            _ConnectionDAL = New ConnectionClientDataObject
        End Sub
#End Region

#Region "Methods"
        Public Function GetConnectionStringByRFC(RFC As String) As ConnectionClientEntityObject
            Return _ConnectionDAL.GetConnectionStringByRFC(RFC)
        End Function

        Public Function GetAllConnectionObjects() As List(Of ConnectionClientEntityObject)
            Return _ConnectionDAL.GetAllConnectionObjects()
        End Function

        Public Function GetCustomConnectionObjects() As List(Of ConnectionClientEntityObject)
            'DEBUG: Solo se toma las conexiones del area BANORTE para pruebas
            Dim _connectionList As List(Of ConnectionClientEntityObject)
            _connectionList = New List(Of ConnectionClientEntityObject)
            _connectionList.Add(_ConnectionDAL.GetConnectionStringByRFC("BANORTE"))
            _connectionList.Add(_ConnectionDAL.GetConnectionStringByRFC("CRA951016DT9"))
            _connectionList.Add(_ConnectionDAL.GetConnectionStringByRFC("PCA0810272Y0"))
            _connectionList.Add(_ConnectionDAL.GetConnectionStringByRFC("GSU6908079L0"))

            Return _connectionList
        End Function
    End Class
#End Region

End Namespace