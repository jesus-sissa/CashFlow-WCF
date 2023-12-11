Imports System.Data

Public Class PaginaBase
    Inherits Page

    Public cn As CnCashWeb

    Private Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        cn = New CnCashWeb(Session, Request)

        Session("ConexionCentral") = ConfigurationManager.ConnectionStrings("ConexionCentral").ConnectionString

        If Id_Usuario = 0 Then
            If Request.Url.AbsolutePath.EndsWith("/Account/Login.aspx") Or Request.Url.AbsolutePath.EndsWith("/Account/Login") Then
                Exit Sub
            Else
                System.Web.Security.FormsAuthentication.SignOut()
                System.Web.Security.FormsAuthentication.RedirectToLoginPage()
            End If
        End If

        If Request.Url.AbsolutePath.Contains("/CambiarPassword.aspx") Then
            Exit Sub
        End If

        ' en este apartado deberia ir a alguna funcion a validar privilegios
        'If Not cn.fn_ValidaPermisos() Then
        '    System.Web.Security.FormsAuthentication.SignOut()
        '    Session.Clear()
        '    Response.Redirect("~\Login\Login.aspx")
        'End If
    End Sub


    Public Property Id_Usuario() As Integer
        Get
            Dim res As Integer = 0
            Integer.TryParse(Session("Id_Usuario"), res)
            Return res
        End Get
        Set(ByVal value As Integer)
            Session("Id_Usuario") = value
        End Set
    End Property

    Public Property NUsuario() As String
        Get
            Return Session("Nombre_Usuario")
        End Get
        Set(ByVal value As String)
            Session("Nombre_Usuario") = value
        End Set
    End Property

    Public Property NSesion() As String
        Get
            Return Session("Nombre_Sesion")
        End Get
        Set(ByVal value As String)
            Session("Nombre_Sesion") = value
        End Set
    End Property

    Public Property NComercial() As String
        Get
            Return Session("NComercial")
        End Get
        Set(ByVal value As String)
            Session("NComercial") = value
        End Set
    End Property

    Public Property CCliente() As String
        Get
            Return Session("CCliente")
        End Get
        Set(ByVal value As String)
            Session("CCliente") = value
        End Set
    End Property

    Public Function GetCn() As CnCashWeb
        Return cn
    End Function


    Public Shared Function fn_Encode(ByVal data As String) As String
        Try
            Dim encyrpt(0 To data.Length - 1) As Byte
            encyrpt = System.Text.Encoding.UTF8.GetBytes(data)
            Dim encodedata As String = Convert.ToBase64String(encyrpt)
            Return encodedata
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function fn_Decode(ByVal data As String) As String
        Try
            Dim encoder As New UTF8Encoding()
            Dim decode As Decoder = encoder.GetDecoder()
            Dim bytes As Byte() = Convert.FromBase64String(data)
            Dim count As Integer = decode.GetCharCount(bytes, 0, bytes.Length)
            Dim decodechar(0 To count - 1) As Char
            decode.GetChars(bytes, 0, bytes.Length, decodechar, 0)
            Dim result As New String(decodechar)
            Return result
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub fn_LlenarDDL(ByVal ddl As DropDownList, ByVal tabla As DataTable, ByVal texto As String, ByVal valor As String, ByVal valorinicial As String)

        Dim ItemCero As ListItem = New ListItem("Seleccione ...", valorinicial)
        ddl.DataSource = tabla
        Dim cant As Integer = tabla.Rows.Count
        ddl.DataTextField = texto
        ddl.DataValueField = valor

        ddl.DataBind()
        ddl.Items.Insert(0, ItemCero)
        ddl.SelectedIndex = 0

    End Sub

    Public Function fn_CreaGridVacio(ByVal campos As String) As DataTable
        Dim dt As New DataTable
        Dim column As New DataColumn

        Dim arr() As String = Split(campos, ",")
        For x As Integer = 0 To arr.Length - 1
            dt.Columns.Add(New DataColumn(arr(x), GetType(String)))
        Next

        Dim Dr As DataRow = dt.NewRow
        For x As Integer = 0 To arr.Length - 1
            Dr(arr(x)) = ""
        Next

        dt.Rows.Add(Dr)

        Return dt

    End Function

    Public Property Tabla(ByVal Clave As String) As DataTable
        Get
            Dim Ds As New DataSet
            If ViewState("TablaXML") = Nothing Then Return Nothing
            Ds.ReadXml(New System.IO.StringReader(ViewState("TablaXML")))
            Return Ds.Tables(Clave)
        End Get
        Set(ByVal value As DataTable)

            Dim Ds As New DataSet
            If (ViewState("TablaXML") <> "") Then Ds.ReadXml(New System.IO.StringReader(ViewState("TablaXML")))
            If Ds.Tables(Clave) IsNot Nothing Then Ds.Tables.Remove(Clave)
            If value Is Nothing Then Exit Property

            value.TableName = Clave
            Ds.Tables.Add(value.Copy)

            ViewState("TablaXML") = Ds.GetXml

        End Set
    End Property

    Public Sub fn_Alerta(Mensaje As String)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType(), "keyScript", "showModal('" & Me.Title & "','" & Mensaje & "');", True)
    End Sub

    Public Sub fn_Modificar(tab As String)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType(), "keyUpdate", "openTab('" + tab + "')", True)
    End Sub

    Public Sub fn_AsignarFecha()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType(), "keyPicker", "setDatePicker()", True)
    End Sub

    Public Function FormatarFechaYYYMMDD(fecha As String)
        Return Format(CDate(fecha), "yyyy/MM/dd")
    End Function


    Public Shared Function fn_Exportar_Excel(ByVal Grilla As GridView, ByVal Titulo As String, ByVal Cadena1 As String, ByVal Cadena2 As String) As Boolean
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)

        Dim NombreArchivo = FechaExcel()
        Try
            Grilla.RenderControl(htw)
            With HttpContext.Current.Response
                .Clear()
                .Buffer = True
                .ContentType = "application/vnd.ms-excel" 'vnd.ms-word'exporta a word
                .AddHeader("Content-Disposition", "attachment;filename=" & NombreArchivo & ".xls")
                .Charset = "UTF-8"
                .ContentEncoding = Encoding.Default
                .Output.Write("<br><b>" & Titulo & "</b>")
                .Output.Write("<br><b>" & Cadena1 & "</b>")
                .Output.Write("<br><b>" & Cadena2 & "</b>" & "<br>")
                .Output.Write(sb.ToString())
                .Flush()
                .End()
            End With

        Catch ex As Exception

            Return False
        Finally
            ' HttpContext.Current.Response.End()
        End Try
        Return True
    End Function


    Public Shared Function FechaExcel() As String
        Dim FechaHora As String = ""
        FechaHora = Now.ToString("yyyyMMdd hhmmss")
        Return FechaHora
    End Function

#Region "Llena Combo Activos/Bajas"
    Public Sub fn_llenaDDL_ActivosBajas(ByVal ddl As DropDownList)

        Dim dt_Status As New DataTable
        dt_Status.Columns.Add("value")
        dt_Status.Columns.Add("display")
        dt_Status.Rows.Add("0", "Seleccione...")
        dt_Status.Rows.Add("A", "ACTIVOS")
        dt_Status.Rows.Add("B", "BAJAS")

        ddl.DataSource = dt_Status
        ddl.DataBind()
    End Sub

#End Region

    Public Function fn_ValidarMail(ByVal sMail As String) As Boolean
        Return System.Text.RegularExpressions.Regex.IsMatch(sMail, "^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$")
    End Function

    Public Sub AbrirPestaña(nombreTab As String)
        ScriptManager.RegisterClientScriptBlock(Me, Me.Page.GetType, "keyTab", "openTab('" & nombreTab & "')", True)
    End Sub
End Class
