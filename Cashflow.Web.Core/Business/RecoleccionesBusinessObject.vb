Option Explicit On
Option Strict Off

Imports SISSA.Cashflow.Web.Core.Data
Imports SISSA.Cashflow.Web.Core.Entities
Imports SISSA.Cashflow.Web.Core.Business.Interfaces

Namespace Business

    Public Class RecoleccionesBusinessObject


        Private _RecoleccionesDAO As RecoleccioneDataObject

#Region "Properties"
        Private Property _connectionString As String

        Public Property ConnectionString As String
            Get
                Return _connectionString
            End Get
            Set(value As String)
                _connectionString = value
                If _RecoleccionesDAO IsNot Nothing Then
                    _RecoleccionesDAO.ConnectionString = _connectionString
                End If
            End Set
        End Property
#End Region

#Region "Constructor"
        Public Sub New()
            _RecoleccionesDAO = New RecoleccioneDataObject()
        End Sub

        Public Sub New(ConnectionString As String)
            _connectionString = ConnectionString
            _RecoleccionesDAO = New RecoleccioneDataObject()
            _RecoleccionesDAO.ConnectionString = _connectionString
        End Sub
#End Region

#Region "Methods"
        Public Function InsertRecoleciones(RecoleccionObj As RecoleccionEntityObject)
            _RecoleccionesDAO.InsertRecoleccion(RecoleccionObj)
        End Function

        Public Function CheckDivisafromCuenta(cuenta As String, divisa As Int32) As Boolean
            Return _RecoleccionesDAO.CheckDivisafromCuenta(cuenta, divisa)
        End Function
#End Region

    End Class
End Namespace

