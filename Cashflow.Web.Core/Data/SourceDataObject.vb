Option Explicit On
Option Strict Off

Imports System.Data
Imports SISSA.Cashflow.Web.Core.Entities

Namespace Data

    Public Class SourceDataObject

        Public Function GetDepositosTransactionsTable(FechaInicial As DateTime, FechaFinal As DateTime) As List(Of AcreditacionEntityObject)
            Dim dt As DataTable = CreateDepositosDataExample()
            Dim rows() As DataRow
            Dim _acreditacionesList As List(Of AcreditacionEntityObject) = New List(Of AcreditacionEntityObject)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                rows = dt.Select(String.Format("Fecha = '{0}'", FechaInicial.ToString("yyyy-MM-dd")))

                For Each row In rows
                    _acreditacionesList.Add(GetDepositosEntityObject(row))
                Next
            End If

            Return _acreditacionesList
        End Function

        Public Function GetRetirosTransactionsTable(FechaInicial As Date, FechaFinal As Date) As List(Of RecoleccionEntityObject)
            Dim dt As DataTable = CreateRetirosDataExample()
            Dim rows() As DataRow
            Dim _recoleccionesList As List(Of RecoleccionEntityObject) = New List(Of RecoleccionEntityObject)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                rows = dt.Select(String.Format("Fecha = '{0}'", FechaInicial.ToString("yyyy-MM-dd")))

                For Each row In rows
                    _recoleccionesList.Add(GetRetirosEntityObject(row))
                Next
            End If

            Return _recoleccionesList
        End Function

        Public Function CreateDepositosDataExample() As DataTable
            Dim dt1 As DataTable

            dt1 = New DataTable("Archivos")
            dt1.Columns.Add("Fecha", System.Type.GetType("System.DateTime"))
            dt1.Columns.Add("Archivo", System.Type.GetType("System.String"))
            dt1.Columns.Add("Importe", System.Type.GetType("System.Decimal"))

            dt1.Rows.Add(New Object() {New Date(2020, 7, 20), "D815990021200302_40.txt", 4077990})
            dt1.Rows.Add(New Object() {New Date(2020, 7, 20), "D815990021200302_41.txt", 13960})
            dt1.Rows.Add(New Object() {New Date(2020, 7, 20), "D815990021200302_42.txt", 556490})

            dt1.Rows.Add(New Object() {New Date(2020, 7, 21), "D815990021200302_43.txt", 4077990})
            dt1.Rows.Add(New Object() {New Date(2020, 7, 21), "D815990021200302_44.txt", 13960})

            Return dt1
        End Function

        Public Function CreateRetirosDataExample() As DataTable
            Dim dt2 As DataTable

            dt2 = New DataTable("Retiros")
            dt2.Columns.Add("Fecha", System.Type.GetType("System.DateTime"))
            dt2.Columns.Add("Cliente", System.Type.GetType("System.String"))
            dt2.Columns.Add("Cajero", System.Type.GetType("System.String"))
            dt2.Columns.Add("Folio", System.Type.GetType("System.Int32"))
            dt2.Columns.Add("Importe", System.Type.GetType("System.Decimal"))

            dt2.Rows.Add(New Object() {New Date(2020, 7, 20), "LUSA", "327", 77, 158850})
            dt2.Rows.Add(New Object() {New Date(2020, 7, 20), "LUSA", "327", 78, 202400})
            dt2.Rows.Add(New Object() {New Date(2020, 7, 20), "LUSA", "328", 83, 124350})
            dt2.Rows.Add(New Object() {New Date(2020, 7, 20), "LUSA", "328", 84, 90130})
            dt2.Rows.Add(New Object() {New Date(2020, 7, 20), "LUSA", "328", 86, 109260})
            dt2.Rows.Add(New Object() {New Date(2020, 7, 20), "COMBO HIDALGO", "329", 38, 164200})
            dt2.Rows.Add(New Object() {New Date(2020, 7, 20), "COMBO HIDALGO", "329", 39, 106840})
            dt2.Rows.Add(New Object() {New Date(2020, 7, 20), "COMBO HIDALGO", "329", 40, 121030})

            dt2.Rows.Add(New Object() {New Date(2020, 7, 21), "LUSA", "327", 77, 158850})
            dt2.Rows.Add(New Object() {New Date(2020, 7, 21), "LUSA", "328", 84, 90130})
            dt2.Rows.Add(New Object() {New Date(2020, 7, 21), "LUSA", "328", 86, 109260})
            dt2.Rows.Add(New Object() {New Date(2020, 7, 21), "COMBO HIDALGO", "329", 38, 164200})
            dt2.Rows.Add(New Object() {New Date(2020, 7, 21), "COMBO HIDALGO", "329", 40, 121030})

            Return dt2
        End Function

        Public Function GetDepositosEntityObject(row As DataRow) As AcreditacionEntityObject
            Dim _AcreditacionInfo As AcreditacionEntityObject = New AcreditacionEntityObject()

            If row IsNot Nothing Then
                _AcreditacionInfo.ID = 0
                _AcreditacionInfo.Fecha = row("Fecha")
                _AcreditacionInfo.Archivo = row("Archivo")
                _AcreditacionInfo.Importe = row("Importe")
            End If

            Return _AcreditacionInfo
        End Function

        Public Function GetRetirosEntityObject(row As DataRow) As RecoleccionEntityObject
            Dim _RecoleccionInfo As RecoleccionEntityObject = New RecoleccionEntityObject()

            If row IsNot Nothing Then
                _RecoleccionInfo.Fecha = row("Fecha")
                _RecoleccionInfo.Cliente = row("Cliente")
                _RecoleccionInfo.Cajero = row("Cajero")
                _RecoleccionInfo.Folio = row("Folio")
                _RecoleccionInfo.Importe = row("Importe")
            End If

            Return _RecoleccionInfo
        End Function
    End Class

End Namespace