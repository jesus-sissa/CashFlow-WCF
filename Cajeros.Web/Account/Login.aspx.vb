
Public Class Login
    Inherits PaginaBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Exit Sub
        End If

        Session("ConexionLocal") = ""
        Session("ConexionCentral") = ""
        Id_Usuario = 0
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Call validar()
    End Sub

    Public Function validar() As Boolean

        If tbx_ClaveUnica.Text = "" Then
            fn_Alerta("Capture la Clave Unica de Sucursal.")
            Return False
        End If

        If tbx_ClaveUsuario.Text = "" Then
            fn_Alerta("Capture la Clave de Usuario.")
            Return False
        End If

        If tbx_Contrasena.Text = "" Then
            fn_Alerta("Indique la Contraseña")
            Return False
        End If


        'If Validar_Captcha() = False Then
        '    fn_Alerta("NO ENVIADO, Válida que eres humano.")
        '    Return False
        'End If

        Dim Dt_Usuario As New DataTable
        Dt_Usuario = cn.fn_Login_Validar_Central(tbx_ClaveUsuario.Text, tbx_ClaveUnica.Text)

        If Dt_Usuario Is Nothing OrElse Dt_Usuario.Rows.Count = 0 Then
            fn_Alerta("El usuario es Incorrecto.")
            Return False
        End If

        If tbx_ClaveUnica.Text <> Dt_Usuario.Rows(0)("CUNICA") Then
            Dt_Usuario.Dispose()
            fn_Alerta("El R.F.C es Incorrecto.")
            Return False
        End If

        Dim hsh_Constraseña = FormsAuthentication.HashPasswordForStoringInConfigFile(tbx_Contrasena.Text, "SHA1")

        If hsh_Constraseña <> Dt_Usuario.Rows(0)("Password") Then
            Dt_Usuario.Dispose()
            fn_Alerta("Contraseña Incorrecta.")
            Return False
        End If


        Id_Usuario = Dt_Usuario.Rows(0)("Id_Usuario")
        NUsuario = Dt_Usuario.Rows(0)("Nombre_Usuario")
        NSesion = Dt_Usuario.Rows(0)("Nombre_Sesion")
        CCliente = Dt_Usuario.Rows(0)("CCLIENTE")
        NComercial = Dt_Usuario.Rows(0)("NCOMERCIAL")

        Dim Cadena() As String = Split(Dt_Usuario.Rows(0)("Cadena"), ",")
        Cadena(0) = PaginaBase.fn_Decode(Cadena(0))
        Cadena(1) = PaginaBase.fn_Decode(Cadena(1))
        Cadena(2) = PaginaBase.fn_Decode(Cadena(2))
        Cadena(3) = PaginaBase.fn_Decode(Cadena(3))

        'Esta conexionLocal es la que se maneja en el cn_datos  para trabajar con la sucursales
        Session("ConexionLocal") = "Data Source=" & Cadena(0) & "; Initial Catalog=" & Cadena(1) & ";User ID=" & Cadena(2) & ";Password=" & Cadena(3) & ";"

        '----Probar conexion con el servidor de la sucursal con los datos obtenidos del corporativo
        Try
            Dim cnn As New SqlClient.SqlConnection(Session("ConexionLocal"))
            cnn.Open()
            cnn.Close()
        Catch ex As Exception
            fn_Alerta("Ocurrió un error al conectarse al servidor del cliente.")
            Return False
        End Try

        FormsAuthentication.RedirectFromLoginPage(tbx_ClaveUsuario.Text, False)

        '---Evaluar si se debe mostrar notificaciones

        Session("Cunica") = tbx_ClaveUnica.Text

        Return True

    End Function

    Private Function Validar_Captcha() As Boolean

        Dim Response As String = Request("g-recaptcha-response") 'Getting Response String Appned to Post Method

        Dim Valid As Boolean = False
        'Request to Google Server

        Dim req As Net.HttpWebRequest = CType(Net.WebRequest.Create(" https://www.google.com/recaptcha/api/siteverify?secret=6LdtDQwUAAAAAFriH9JnNvzyW5OScGaN5MKDIuAz&response=" & Response), Net.HttpWebRequest)
        Try

            'Google recaptcha Responce
            Using wResponse As Net.WebResponse = req.GetResponse()

                Using readStream As New IO.StreamReader(wResponse.GetResponseStream())

                    Dim jsonResponse As String = readStream.ReadToEnd()

                    Dim js As New Script.Serialization.JavaScriptSerializer()
                    Dim data As MyObject = js.Deserialize(Of MyObject)(jsonResponse) ' Deserialize Json
                    Valid = Convert.ToBoolean(data.success)

                End Using
            End Using
            Return Valid
        Catch ex As Exception
            ' msgerror = ex.ToString
            Return False

        End Try

    End Function

    Public Class MyObject
        Public Property success() As String
            Get
                Return m_success
            End Get
            Set(value As String)
                m_success = value
            End Set
        End Property
        Private m_success As String
    End Class
End Class