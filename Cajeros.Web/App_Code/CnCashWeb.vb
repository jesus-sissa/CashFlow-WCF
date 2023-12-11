Imports System.Data
Imports System.Data.SqlClient
Imports ConexionesDatos.cls_Datos

Public Class CnCashWeb

    Private Session As HttpSessionState
    Private Request As HttpRequest

    Public Sub New(ByVal MySession As HttpSessionState, ByVal MyRequest As HttpRequest)
        Session = MySession
        Request = MyRequest
    End Sub


#Region "Variables Públicas - Agregar Usuarios CashFlow"

    Public Enum Tipos_Usuarios As Byte
        Sistemassissa 'X 0
        Local
        Administrador
        Supervisor
    End Enum

    Public Enum Acciones As Byte
        X
        Login
        Nuevo
        Editar
        CambiarContrasena
        Eliminar
    End Enum

#End Region


#Region "Propiedades"

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
#End Region



    'Public Function fn_Login_Validar_Central(ByVal ClaveUsuario As String, ByVal ClaveUnica As String) As DataTable
    '    Try
    '        Dim cnn As New SqlConnection(Session("ConexionCentral"))
    '        Dim Query As String = "Select uc.Id_Usuario, " & _
    '                               "uc.Nombre_Usuario, " & _
    '                               "uc.Password, " & _
    '                               "uc.Nombre_Sesion, " & _
    '                               "uc.Nivel, " & _
    '                               "uc.Fecha_Expira, " & _
    '                               "cc.CCLIENTE, " & _
    '                               "cc.NCOMERCIAL, " & _
    '                               "cc.SVR +','+ cc.BD +','+ cc.USR+','+ cc.PWD As Cadena, " & _
    '                               "cc.CUNICA, " & _
    '                               "IsNull(uc.Mail, '') As Mail, " & _
    '                               "uc.Status " & _
    '                               "From usrctes As uc " & _
    '                               "Join cnnctes As cc On cc.ccliente = uc.ccliente AND CC.TIPO_CONEXION=1" & _
    '                               "Where cc.Cunica = '" & ClaveUnica & "'" & _
    '                               "And uc.Nombre_Sesion = '" & ClaveUsuario & "'"
    '        Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
    '        Dim dt As DataTable = EjecutaConsulta(cmd)
    '        Return dt

    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function



    Public Function fn_ConsultaDetalleD_LlenarGrid(Id_Deposito As Integer, ByVal ClaveSucursal As String) As DataTable
        Try

            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim Query As String = "Select Clave_Denominacion As Clave, " & _
                                    "Denominacion As Denominacion, " & _
                                    "Clave_Moneda As Moneda, " & _
                                    "Sum(Cantidad_Piezas) As Cantidad, " & _
                                    "dbo.Fn_Moneda(Sum(Importe)) As Importe " & _
                                    "From DepositosD " & _
                                    "Where Id_Deposito = '" & Id_Deposito & "' And Clave_Sucursal ='" & ClaveSucursal & "' " & _
                                    "Group By Clave_Denominacion, Denominacion, Clave_Moneda " & _
                                    "Order By Moneda,Denominacion Desc"

            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
            Return EjecutaConsulta(cmd)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


#Region "Conexiones Central [Crear Log / Login / Cambiar Password]"

    Public Function fn_Crear_Log(ByVal Descripcion As String) As Boolean
        Try

            Dim cnn As New SqlConnection(Session("ConexionCentral"))
            Dim Query As String = "Insert Into LOGUSRCTES " & _
                                  "(Id_Usuario,Fecha,Nombre_Usuario,Nombre_Sesion,Descripcion,CCliente,NComercial,Tipo_LE) " & _
                                  "Values (" & Id_Usuario & ",Getdate(),'" & NUsuario & "','" & NSesion & "','" & Descripcion & "','" & CCliente & "','" & NComercial & "', 'L')"
            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
            EjecutarNonQuery(cmd)
            Return True
        Catch ex As Exception
            TrataEx(ex)
            Return False
        End Try
    End Function

    Public Sub TrataEx(ByVal Ex As Exception)
        Dim cnn As New SqlConnection(Session("ConexionCentral"))

        Dim Query As String = "Insert Into LOGUSRCTES " & _
                              "(Id_Usuario,Fecha,Nombre_Usuario,Nombre_Sesion,Descripcion,CCliente,NComercial,Tipo_LE) " & _
                              "Values (" & Id_Usuario & ",Getdate(),'" & NUsuario & "','" & NSesion & "','" & Ex.Message & "','" & CCliente & "','" & NComercial & "', 'E')"
        Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)

        Try
            EjecutarNonQuery(cmd)
        Finally

        End Try

    End Sub

    Public Function fn_Login_Validar_Central(ByVal ClaveUsuario As String, ByVal ClaveUnica As String) As DataTable
        Try
            Dim cnn As New SqlConnection(Session("ConexionCentral"))
            Dim Query As String = "Select uc.Id_Usuario, " & _
                                   "uc.Nombre_Usuario, " & _
                                   "uc.Password, " & _
                                   "uc.Nombre_Sesion, " & _
                                   "uc.Nivel, " & _
                                   "uc.Fecha_Expira, " & _
                                   "cc.CCLIENTE, " & _
                                   "cc.NCOMERCIAL, " & _
                                   "cc.SVR +','+ cc.BD +','+ cc.USR+','+ cc.PWD As Cadena, " & _
                                   "cc.CUNICA, " & _
                                   "IsNull(uc.Mail, '') As Mail, " & _
                                   "uc.Status " & _
                                   "From usrctes As uc " & _
                                   "Join cnnctes As cc On cc.ccliente = uc.ccliente AND CC.TIPO_CONEXION=1" & _
                                   "Where cc.Cunica = '" & ClaveUnica & "'" & _
                                   "And uc.Nombre_Sesion = '" & ClaveUsuario & "'"
            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
            Dim dt As DataTable = EjecutaConsulta(cmd)
            Return dt

        Catch ex As Exception
            TrataEx(ex)
            Return Nothing
        End Try
    End Function

    Public Function fn_CambiarContraseña_GetPassword() As String
        Try
            Dim cnn As New SqlConnection(Session("ConexionCentral"))
            Dim cmd As SqlCommand

            Dim Query As String = "Select Password " & _
                                  "From usrctes " & _
                                  "Where Id_Usuario = " & Id_Usuario & ""
            cmd = Crea_Comando(Query, CommandType.Text, cnn)
            Dim pswd As String = EjecutarScalar(cmd)
            Return pswd
        Catch ex As Exception
            TrataEx(ex)
            Return String.Empty
        End Try
    End Function

#End Region

#Region "Consulta Retiros / Detalle Retiros"
    Public Function fn_ConsultaRetiros_LlenarGrid(ByVal Desde As Date, ByVal Hasta As Date, ByVal Usuario_Clave As Integer, ByVal ClaveSucursal As String) As DataTable
        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim Query As String = "Select r.Id_Retiro As Folio, " & _
                                  "s.Clave_Sucursal As ClaveSucursal, " & _
                                  "s.Nombre_Sucursal As NombreSucursal, " & _
                                  "CONVERT(varchar(10), r.Fecha, 103) As Fecha, " & _
                                  "CONVERT(varchar(5), r.Hora_Inicio, 108) As Hora, " & _
                                  "dbo.Fn_Moneda(r.Importe_Total) As Importe, " & _
                                  "u.Nombre As Usuario, " & _
                                  "r.Numero_Remision As Remision, " & _
                                  "r.Numero_Envase As Envase, " & _
                                  "Case r.Status " & _
                                  "When 'A' Then 'ACTIVO' " & _
                                  "When 'V' Then 'VALIDADO' " & _
                                  "When 'C' Then 'CANCELADO' End As Estatus " & _
                                  "From Retiros As r " & _
                                  "Join Usuarios As u On u.Clave_Usuario = r.Usuario_Registro " & _
                                  "Join Sucursales as s On s.Clave_Sucursal= r.Clave_Sucursal " & _
                                  "Where Convert(varchar(10), r.Fecha, 102) Between '" & Format(Desde, "yyyy.MM.dd") & " ' And '" & Format(Hasta, "yyyy.MM.dd") & "' " & _
                                  " And u.Clave_Sucursal = '" & ClaveSucursal & "'"

            If ClaveSucursal <> "0" Then
                Query = Query & "And r.Clave_Sucursal= '" & ClaveSucursal & "'"
            End If

            If Usuario_Clave <> 0 Then
                'Consulta por sólo un Usuario
                Query &= " And r.Usuario_Registro = '" & Usuario_Clave & "'"
            End If
            Query = Query & " Order By s.Nombre_Sucursal,r.Fecha,CONVERT(varchar(5), r.Hora_Inicio, 108) "

            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
            Return EjecutaConsulta(cmd)
        Catch ex As Exception
            TrataEx(ex)
            Return Nothing
        End Try
    End Function

    Public Function fn_ConsultaDetalleR_LlenarGrid(ByVal Id_Retiro As Integer, ByVal ClaveSucursal As String) As DataTable
        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()

            Dim Query As String = "Select d.Folio As Folio, " & _
                                "CONVERT(varchar(10), d.Fecha, 103) As Fecha, " & _
                                 "CONVERT(varchar(5), d.Hora_Inicio, 108) As Hora, " & _
                                 "dbo.Fn_Moneda(d.Importe_Total) As Importe, " & _
                                 "u.Nombre As NombreUser " & _
                                 "From Depositos As d " & _
                                 "Join Usuarios As u On  u.Clave_Usuario = d.Usuario_Registro " & _
                                 "Where d.Id_Retiro = '" & Id_Retiro & "' And d.Clave_Sucursal ='" & ClaveSucursal & "' " & _
                                 "Order By d.id_Deposito, d.Fecha"
            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
            Return EjecutaConsulta(cmd)

        Catch ex As Exception
            TrataEx(ex)
            Return Nothing
        End Try
    End Function

#End Region

#Region "Monedas"
    Public Function fn_Monedas_LlenarGridview() As DataTable
        'esta funcion tablen llena el combo monedas en Denominaciones
        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim Query As String = "Select Clave_Moneda As Clave, " & _
                                 "Nombre_Moneda As Nombre " & _
                                  "From Monedas "
            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
            Return EjecutaConsulta(cmd)


        Catch ex As Exception
            TrataEx(ex)
            Return Nothing
        End Try
    End Function

    Public Function fn_Monedas_Create(ByVal Clave_Moneda As String, ByVal NombreMoneda As String) As Integer

        'Validaciones
        If Clave_Moneda.Trim = "" Then
            Return 0
        End If

        If fn_Monedas_Existe(Clave_Moneda) Then
            Return 3
        End If

        If NombreMoneda.Trim = "" Then
            Return 2
        End If

        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim Query As String = "Insert Into Monedas " & _
                                  "(Clave_Moneda,Nombre_Moneda) " & _
                                  "Values ('" & Clave_Moneda & "','" & NombreMoneda & "')"
            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)

            EjecutarNonQuery(cmd)
            Return 1

        Catch ex As Exception
            TrataEx(ex)
            Return -1
        End Try
    End Function

    Public Function fn_Monedas_Existe(ByVal ClaveMoneda As String) As Boolean

        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim Query As String = "Select Clave_Moneda " & _
                                  "From Monedas " & _
                                  "Where Clave_Moneda= '" & ClaveMoneda & "'"
            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
            Dim dt_Monedas As DataTable = EjecutaConsulta(cmd)

            If dt_Monedas IsNot Nothing AndAlso dt_Monedas.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            TrataEx(ex)
            Return False
        End Try
    End Function

    Public Function fn_Monedas_Modificar(ByVal Clave_Moneda As String, ByVal cveM_modif As String, ByVal NombreMoneda As String) As Integer

        'Validaciones
        If Clave_Moneda.Trim = "" Then
            Return 0
        End If

        If fn_Monedas_Existe(Clave_Moneda) AndAlso Clave_Moneda <> cveM_modif Then
            Return 3
        End If

        If NombreMoneda.Trim = "" Then
            Return 2
        End If

        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim cmd As SqlCommand

            Dim Query As String = "Select * From Denominaciones " & _
                                  "Where Clave_Moneda='" & cveM_modif & "' "

            cmd = Crea_Comando(Query, CommandType.Text, cnn)
            Dim dt_Denominaciones As DataTable = EjecutaConsulta(cmd)

            If dt_Denominaciones.Rows.Count > 0 Then
                Return 4 'no se puede modificar
            End If

            Query = "Update Monedas " & _
                  "Set Clave_Moneda ='" & Clave_Moneda & "', " & _
                  "Nombre_Moneda ='" & NombreMoneda & "' " & _
                  "Where Clave_Moneda='" & cveM_modif & "' "
            cmd = Crea_Comando(Query, CommandType.Text, cnn)
            If EjecutarNonQuery(cmd) > 0 Then
                Return 1
            Else
                Return -1
            End If
        Catch ex As Exception
            TrataEx(ex)
            Return -1
        End Try
    End Function

    Public Function fn_Monedas_Delete(ByVal Clave_Moneda As String) As Integer
        Try

            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim cmd As SqlCommand

            Dim Query As String = "Select * From Denominaciones " & _
                                  "Where Clave_Moneda = '" & Clave_Moneda & "'"
            cmd = Crea_Comando(Query, CommandType.Text, cnn)
            Dim dt_Denominaciones As DataTable = EjecutaConsulta(cmd)
            If dt_Denominaciones.Rows.Count > 0 Then
                Return 0
            End If

            Query = "Delete Monedas " & _
                    "Where Clave_Moneda = '" & Clave_Moneda & "'"
            cmd = Crea_Comando(Query, CommandType.Text, cnn)
            EjecutarNonQuery(cmd)
            Return 1

        Catch ex As Exception
            TrataEx(ex)
            Return -1
        End Try
    End Function

#End Region


#Region "Deposito"

    Public Function fn_Depositos_Consulta(ByVal Desde As Date, ByVal Hasta As Date, ByVal Usuario_Clave As String, ByVal ClaveSucursal As String) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim dt As DataTable = Nothing
        Dim cnn As SqlConnection = Crea_ConexionSTD()

        If Usuario_Clave = "0" Then Usuario_Clave = "0"
        If ClaveSucursal = "0" Then ClaveSucursal = "T"

        Try
            cmd = Crea_Comando("Depositos_Get1", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)
            Crea_Parametro(cmd, "@Fecha_Desde", SqlDbType.Date, Desde)
            Crea_Parametro(cmd, "@Fecha_Hasta", SqlDbType.Date, Hasta)
            Crea_Parametro(cmd, "@Usuario_Registro", SqlDbType.VarChar, Usuario_Clave)
            dt = EjecutaConsulta(cmd)
        Catch ex As Exception
            cnn.Dispose()
            cmd.Dispose()
            Return Nothing
        End Try
        Return dt
    End Function

    Public Function fn_DepositosSuma_Consulta(Clave_Sucursal As String, Clave_Usuario As String, FechaInicio As Date, FechaFin As Date) As DataTable
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim dt As DataTable = Nothing
        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Depositos_Get3", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, Clave_Sucursal)
            Crea_Parametro(cmd, "Clave_Usuario", SqlDbType.VarChar, Clave_Usuario)
            Crea_Parametro(cmd, "Fecha_Inicio", SqlDbType.Date, FechaInicio)
            Crea_Parametro(cmd, "Fecha_Fin", SqlDbType.Date, FechaFin)
            dt = EjecutaConsulta(cmd)
        Catch ex As Exception
            cnn.Dispose()
            cmd.Dispose()
            Return Nothing
        End Try
        Return dt
    End Function
#End Region

#Region "Consulta Saldos"
    Public Function fn_ConsultaSaldos_LlenarGrid(ByVal ClaveMoneda As String) As DataTable
        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()

            'Consulta muestra Saldo--> solo se muestran los Depositos "Finalizados" 'F' de Cada Sucursal
            Dim Query As String = "Select d.Clave_Sucursal As ClaveSucursal, " & _
                                  "s.Nombre_Sucursal As NombreS, " & _
                                  "dd.Clave_Moneda As Moneda, " & _
                                  "dbo.Fn_Moneda(Sum(dd.Importe)) As Importe " & _
                                  "From Depositos As d " & _
                                  "Join DepositosD As dd On d.Id_Deposito=dd.Id_deposito And d.Clave_Sucursal= dd.Clave_Sucursal " & _
                                  "Join Sucursales As s On s.Clave_Sucursal= d.Clave_Sucursal " & _
                                  "Where d.Status= 'F' "

            If ClaveMoneda <> "0" Then
                Query = Query & " And dd.Clave_Moneda= '" & ClaveMoneda & "'"
            End If

            Query = Query & " Group By d.Clave_Sucursal,s.Nombre_Sucursal,dd.Clave_Moneda "
            Query = Query & " Order By s.Nombre_Sucursal "

            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
            Dim dt_Saldo As DataTable = EjecutaConsulta(cmd)

            Return dt_Saldo

        Catch ex As Exception
            TrataEx(ex)
            Return Nothing
        End Try
    End Function

    Public Function fn_ConsultaDetalleSaldo_LlenarGrid(ByVal ClaveSucursal As String) As DataTable
        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim cmd As SqlCommand
            Dim Query As String = ""

            '--------Esto es para mostar detalle denominaciones Total general Modif join ClaveSuc 12/12/2013
            Query = "Select dd.Clave_Denominacion As Clave, " & _
                    "dd.Denominacion As Denominacion, " & _
                     "dd.Clave_Moneda As Moneda, " & _
                    "Sum(dd.Cantidad_Piezas) As Cantidad, " & _
                     "dbo.Fn_Moneda(Sum(dd.Importe)) As Importe " & _
                    "From DepositosD As dd " & _
                    "Join Depositos As d On dd.Id_Deposito = d.Id_Deposito And dd.Clave_Sucursal='" & ClaveSucursal & "' " & _
                    "Where d.Status = 'F' And d.Clave_Sucursal='" & ClaveSucursal & "'  " & _
                    "Group By dd.Clave_Denominacion, dd.Denominacion,dd.Clave_Moneda " & _
                    "Order By dd.Clave_Moneda,dd.Denominacion"
            ''--------------------------------------------------

            cmd = Crea_Comando(Query, CommandType.Text, cnn)
            Return EjecutaConsulta(cmd)

        Catch ex As Exception
            TrataEx(ex)
            Return Nothing
        End Try
    End Function

    Public Function fn_Consulta_ObtenerValidadores(ByVal ClaveSucursal As String) As DataTable
        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim cmd As SqlCommand
            Dim Query As String = ""

            '---Esta consuta Saca La cantidad de Validadores  modifique join clavesuc 12/12/2013 
            Query = "Select dd.Serie_Validador As SerieVal, " & _
                    " dd.Serie_Caset As SerieCaset " & _
                    "From DepositosD As dd " & _
                    "Join Depositos As d On dd.Id_Deposito = d.Id_Deposito  And dd.Clave_Sucursal='" & ClaveSucursal & "' " & _
                    "Where d.Status = 'F' And d.Clave_Sucursal='" & ClaveSucursal & "'" & _
                    "Group By  dd.serie_validador, dd.Serie_Caset "
            cmd = Crea_Comando(Query, CommandType.Text, cnn)
            Return EjecutaConsulta(cmd)

        Catch ex As Exception
            TrataEx(ex)
            Return Nothing
        End Try
    End Function

    Public Function fn_ConsultaDetalleSaldoporValidador_LlenarGrid(ByVal ClaveSucursal As String, SerieVal As String) As DataTable
        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim cmd As SqlCommand
            Dim Query As String = ""

            '---Esto es para Obneter el Detalla x Validador-- modifike JOin en clave sucursal lo puse en el Where 12/12/2013
            Query = "Select dd.Clave_Denominacion As Clave, " & _
                    "dd.Denominacion As Denominacion, " & _
                     "dd.Clave_Moneda As Moneda, " & _
                    "Sum(dd.Cantidad_Piezas) As Cantidad, " & _
                     "dbo.Fn_Moneda(Sum(dd.Importe)) As Importe " & _
                    "From DepositosD As dd " & _
                    "Join Depositos As d On dd.Id_Deposito = d.Id_Deposito And dd.Serie_Validador ='" & SerieVal & "' " & _
                    "Where d.Status = 'F' And d.Clave_Sucursal='" & ClaveSucursal & "' " & _
                    "Group By dd.Clave_Denominacion, dd.Denominacion,dd.Clave_Moneda " & _
                    "Order By dd.Clave_Moneda,dd.Denominacion"

            cmd = Crea_Comando(Query, CommandType.Text, cnn)
            Return EjecutaConsulta(cmd)

        Catch ex As Exception
            TrataEx(ex)
            Return Nothing
        End Try
    End Function
#End Region


#Region "Casets-Modif-18/feb/2015"
    Public Function fn_Casets_Llenar(ByVal ClaveSucursal As String, ByVal Status As Char) As DataTable
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim dt_Casets As DataTable = Nothing
        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Casets_Get2", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)
            Crea_Parametro(cmd, "@Status", SqlDbType.VarChar, Status)
            dt_Casets = EjecutaConsulta(cmd)
        Catch ex As Exception
            cnn.Dispose()
            cmd.Dispose()
            Return Nothing
        End Try
        Return dt_Casets
    End Function

    Public Function fn_Casets_Create(ByVal ClaveSucursal As String, ByVal Clave_Caset As String, _
                                            ByVal Serie_Caset As String, ByVal Capacidad As String, _
                                            ByVal PorcentajeAlerta As String) As Integer

        'Validaciones
        If Clave_Caset.Trim = "" Then
            Return 0
        End If

        If fn_Casets_ClaveExiste(ClaveSucursal, Clave_Caset) Then
            Return 2 'clave ya existe
        End If

        If Serie_Caset.Trim = "" Then
            Return 3
        End If

        If fn_Casets_NumeroserieExiste(ClaveSucursal, Serie_Caset) Then
            Return 10 ' serie ya existe
        End If

        If Capacidad.Trim = "" Then
            Return 4
        End If

        Dim rCapacidad As Integer
        If Not Integer.TryParse(Capacidad, rCapacidad) Then
            Return 4
        End If

        'If CInt(Capacidad) < 10 Or CInt(Capacidad) > 1200 Then
        '    Return 9
        'End If

        If PorcentajeAlerta.Trim = "" Then
            Return 5
        End If

        Dim rPorcentajeAlerta As Integer
        If Not Integer.TryParse(PorcentajeAlerta, rPorcentajeAlerta) Then
            Return 5
        End If

        If CInt(PorcentajeAlerta) < 40 OrElse CInt(PorcentajeAlerta) > 95 Then
            Return 6
        End If

        If ClaveSucursal = "0" Then
            Return 7
        End If

        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing

        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Casets_Create", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)
            Crea_Parametro(cmd, "@Clave_Caset", SqlDbType.VarChar, Clave_Caset)
            Crea_Parametro(cmd, "@Serie_Caset", SqlDbType.VarChar, Serie_Caset)
            Crea_Parametro(cmd, "@Capacidad", SqlDbType.Int, rCapacidad)
            Crea_Parametro(cmd, "@Porcentaje_Alerta", SqlDbType.Int, rPorcentajeAlerta)
            Crea_Parametro(cmd, "@Status", SqlDbType.VarChar, "A")
            Crea_Parametro(cmd, "@FechaSync", SqlDbType.VarChar, "")
            Crea_Parametro(cmd, "@Status2", SqlDbType.VarChar, "A")

            EjecutarNonQuery(cmd)
            Return 1

        Catch ex As Exception
            TrataEx(ex)
            cnn.Dispose()
            cmd.Dispose()
            Return -1
        End Try
    End Function

    Public Function fn_Casets_ClaveExiste(ByVal ClaveSucursal As String, ByVal ClaveCaset As String) As Boolean
        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim Query As String = "Select Clave_Caset " & _
                                  "From Casets " & _
                                  "Where Clave_Sucursal= '" & ClaveSucursal & "' And Clave_Caset ='" & ClaveCaset & "'"
            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)

            If EjecutaConsulta(cmd).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            TrataEx(ex)
            Return False
        End Try
    End Function

    Public Function fn_Casets_NumeroserieExiste(ClaveSucursal As String, ByVal SerieCaset As String) As Boolean
        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim Query As String = "Select Serie_Caset " & _
                                  "From Casets " & _
                                  "Where Clave_Sucursal= '" & ClaveSucursal & "' And Serie_Caset ='" & SerieCaset & "'"
            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)

            If EjecutaConsulta(cmd).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            TrataEx(ex)
            Return False
        End Try
    End Function

    Public Function fn_Casets_Modificar(ByVal cveCasetModif As String, serieCasetModif As String, ByVal cveSucursalModif As String, ByVal ClaveSucursalNew As String, _
                                               ByVal ClaveCasetNew As String, ByVal SerieCasetNew As String, ByVal Capacidad As String, _
                                               ByVal PorcentajeAlerta As String) As Integer


        'Verificar si tiene depositos antes de Modificar
        Dim CasetenUso As Integer = fn_Casets_VerificarEstaenUSo(cveSucursalModif, serieCasetModif)

        If CasetenUso > 0 Then
            Return 11 'caset en uso
        ElseIf CasetenUso = -1 Then
            Return -1
        End If

        If ClaveCasetNew.Trim = "" Then
            Return 0
        End If

        If cveCasetModif <> ClaveCasetNew OrElse cveSucursalModif <> ClaveSucursalNew Then
            If fn_Casets_ClaveExiste(ClaveSucursalNew, ClaveCasetNew) Then
                Return 0
            End If
        End If

        If serieCasetModif <> SerieCasetNew OrElse cveSucursalModif <> ClaveSucursalNew Then
            If fn_Casets_NumeroserieExiste(ClaveSucursalNew, SerieCasetNew) Then
                Return 2 'pendiente
            End If
        End If

        If SerieCasetNew.Trim = "" Then
            Return 2
        End If

        If Capacidad.Trim = "" Then
            Return 3
        End If

        Dim rCapacidad As Integer
        If Not Integer.TryParse(Capacidad, rCapacidad) Then
            Return 3
        End If

        If PorcentajeAlerta.Trim = "" Then
            Return 4
        End If

        Dim rPorcentajeAlerta As Integer
        If Not Integer.TryParse(PorcentajeAlerta, rPorcentajeAlerta) Then
            Return 4
        End If

        If CInt(PorcentajeAlerta) < 40 OrElse CInt(PorcentajeAlerta) > 95 Then
            Return 6
        End If

        If ClaveSucursalNew = "0" Then
            Return 5
        End If

        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim cmd As SqlCommand
            Dim Query As String = "Select count(Serie_Caset) As cantCasets From DepositosD " & _
                                  "Where Clave_Sucursal= '" & cveSucursalModif & "' And Serie_Caset ='" & serieCasetModif & "'"
            cmd = Crea_Comando(Query, CommandType.Text, cnn)
            Dim cant_DepositosD As Int32 = EjecutarScalar(cmd)

            If cant_DepositosD > 0 Then
                Return 8
            End If

            Query = "Update Casets " & _
                    "Set Clave_Caset='" & ClaveCasetNew & "', " & _
                    "Clave_Sucursal='" & ClaveSucursalNew & "', " & _
                    "Numero_Serie='" & SerieCasetNew & "', " & _
                    "Capacidad=" & CDec(Capacidad) & ", " & _
                     "FechaModifica= Getdate(),  " & _
                    "Porcentaje_Alerta= " & CDec(PorcentajeAlerta) & "" & _
                    "Where Clave_Sucursal= '" & cveSucursalModif & "' And Clave_Caset ='" & cveCasetModif & "'"
            cmd = Crea_Comando(Query, CommandType.Text, cnn)
            EjecutarNonQuery(cmd)
            Return 1
        Catch ex As Exception
            TrataEx(ex)
            Return -1
        End Try

    End Function

    Public Function fn_Casets_Eliminar(ByVal ClaveSucursal As String, ByVal NumserieCaset As String) As Integer
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("DepositosD_Get", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)
            Crea_Parametro(cmd, "@Serie_Caset", SqlDbType.VarChar, NumserieCaset)

            Dim cant_DepositosD As Integer = EjecutarScalar(cmd)

            If cant_DepositosD > 0 Then Return 0


            cmd = Crea_Comando("Casets_Update2", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)
            Crea_Parametro(cmd, "@Serie_Caset", SqlDbType.VarChar, NumserieCaset)
            Crea_Parametro(cmd, "@Status2", SqlDbType.VarChar, "D")
            EjecutarNonQuery(cmd)
            Return 1
        Catch ex As Exception
            cnn.Dispose()
            cmd.Dispose()
            TrataEx(ex)
            Return -1
        End Try
    End Function

    Public Function fn_Casets_VerificarEstaenUSo(ByVal ClaveSucursal As String, SerieCaset As String) As Integer

        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim Query As String = "Select count(Serie_Caset) As cantCasets From DepositosD " & _
                                  "Where Clave_Sucursal= '" & ClaveSucursal & "' And Serie_Caset ='" & SerieCaset & "'"

            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
            Dim cant_CantidadCaset As Int32 = EjecutarScalar(cmd)


            'Dim cantidadCaset As Integer = 0

            'If dt_CantidadCaset IsNot Nothing AndAlso dt_CantidadCaset.Rows.Count > 0 Then
            '    cantidadCaset = dt_CantidadCaset.Rows(0)("cantCasets")
            'End If
            Return cant_CantidadCaset
        Catch ex As Exception
            TrataEx(ex)
            Return -1
        End Try
    End Function

    Public Function fn_Casets_ObtenerCapacidad(ByVal ClaveSucursal As String, serieCaset As String) As Integer

        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim Query As String = "Select Capacidad " & _
                                  "From Casets " & _
                                  "Where Serie_Caset= '" & serieCaset & "' And Clave_Sucursal ='" & ClaveSucursal & "'"

            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
            Dim dt_capacidad As DataTable = EjecutaConsulta(cmd)
            Dim Capacidad As Integer = 0
            If dt_capacidad IsNot Nothing AndAlso dt_capacidad.Rows.Count > 0 Then
                Capacidad = CInt(dt_capacidad.Rows(0)("Capacidad"))
            End If
            Return Capacidad

        Catch ex As Exception
            TrataEx(ex)
            Return 0
        End Try
    End Function
#End Region

#Region "Consulta de Movimientos X Sucursal"
    Public Function fn_ConsultaMovimientos_LlenarGridByVal(Desde As Date, ByVal Hasta As Date, ByVal ClaveSucursal As String) As DataTable
        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()

            Dim Query As String = " Select CONVERT(varchar(10), d.Fecha, 103) As Fecha, " & _
                                    "CONVERT(varchar(8), d.Hora_Inicio, 108) As HoraInicio, " & _
                                    "CONVERT(varchar(8), d.Hora_Fin, 108) As HoraFin, " & _
                                    "u.Nombre As Nombre, " & _
                                    "dbo.Fn_Moneda(d.Importe_Total) As Deposito, " & _
                                    "dbo.Fn_Moneda(0.00) As Retiro " & _
                                    "From Depositos As d " & _
                                    "Join Usuarios  As u	on u.clave_usuario= d.Usuario_Registro " & _
                                    "Where d.Clave_Sucursal = '" & ClaveSucursal & "' " & _
                                    "And CONVERT(varchar(10), d.Fecha, 102) Between " & _
                                    "'" & Format(Desde, "yyyy.MM.dd") & "' And '" & Format(Hasta, "yyyy.MM.dd") & "' " & _
                                    "And (d.status='R' Or d.status='F') " & _
                                        "Union " & _
                                    "Select CONVERT(varchar(10), r.Fecha, 103) As Fecha, " & _
                                    "CONVERT(varchar(8), r.Hora_Inicio, 108) As HoraInicio, " & _
                                    "CONVERT(varchar(8), r.Hora_Fin, 108) As HoraFin, " & _
                                    "u.Nombre As Nombre, " & _
                                    "dbo.Fn_Moneda(0.00) As Deposito, " & _
                                    "dbo.Fn_Moneda(r.Importe_Total) As Retiro " & _
                                    "From Retiros As r " & _
                                    "Join Usuarios  As u	on u.clave_usuario= r.Usuario_Registro " & _
                                    "Where r.Clave_Sucursal = '" & ClaveSucursal & "' " & _
                                    "And CONVERT(varchar(10), r.Fecha, 102) Between " & _
                                    "'" & Format(Desde, "yyyy.MM.dd") & "' And '" & Format(Hasta, "yyyy.MM.dd") & "' " & _
                                    "And (r.status='V' Or r.status='F' Or r.status='A') "

            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
            Return EjecutaConsulta(cmd)
        Catch ex As Exception
            TrataEx(ex)
            Return Nothing
        End Try

    End Function

#End Region

#Region "Usuarios / Privilegios"

    Public Function fn_Usuarios_Llenar(ByVal ClaveSucursal As String, ByVal Status As Char) As DataTable
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing

        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Usuarios_Get2", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)
            Crea_Parametro(cmd, "@Status", SqlDbType.VarChar, Status)
            'Dim cnn As SqlConnection = Crea_ConexionSTD()
            'Dim Query As String = "Select u.Clave_Usuario As Clave, " & _
            '                     "u.Nombre As Nombre, " & _
            '                       "u.Nombre_Corto As NombreCorto, " & _
            '                      "s.Nombre_Sucursal As NombreSucursal, " & _
            '                      "Case u.Tipo_Usuario " & _
            '                      " When 1 Then 'LOCAL' " & _
            '                      " When 2 Then 'ADMINISTRADOR' " & _
            '                      " When 3 Then 'SUPERVISOR' End As Tipo, " & _
            '                      "CONVERT(varchar(10),u.Fecha_Registro,103) As FechaRegistro, " & _
            '                      "Case u.Status " & _
            '                      " When 'A' Then 'ACTIVO' " & _
            '                      " When 'B' Then 'BAJA' End As Estatus, " & _
            '                      "Convert(Varchar(10),u.Fecha_Expira,103) As 'FechaExpira' " & _
            '                      "From Usuarios As u " & _
            '                      "Join Sucursales as s On s.Clave_Sucursal= u.Clave_Sucursal " & _
            '                      "Where u.Tipo_Usuario In (1,2,3) And u.Usuario_Registro <> 0"

            'If ClaveSucursal <> "0" Then
            '    Query = Query & " And u.Clave_Sucursal = '" & ClaveSucursal & "' "
            'End If
            'If Status <> "0" Then
            '    Query = Query & " And u.Status = '" & Status & "' "
            'End If
            'Query = Query & " Order By s.Nombre_Sucursal, u.Nombre"

            'Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)

            Return EjecutaConsulta(cmd)
        Catch ex As Exception
            TrataEx(ex)
            Return Nothing
        End Try
    End Function

    Public Function fn_UsuariosPrivilegios_LlenarGrid(ByVal ClaveSucursal As String, ByVal Status As Char) As DataTable
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing

        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Usuarios_Get6", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)
            Crea_Parametro(cmd, "@Status", SqlDbType.VarChar, Status)
            Return EjecutaConsulta(cmd)
        Catch ex As Exception
            TrataEx(ex)
            cnn.Dispose()
            cmd.Dispose()
            Return Nothing
        End Try
    End Function

    Public Function fn_Privilegios_LlenarGrid(ByVal TipoUser As Byte) As DataTable
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing

        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Opciones_Get1", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Tipo", SqlDbType.TinyInt, TipoUser)
            Return EjecutaConsulta(cmd)
        Catch ex As Exception
            TrataEx(ex)
            cnn.Dispose()
            cmd.Dispose()
            Return Nothing
        End Try
    End Function

    Public Function fn_PrivilegiosAsigna2_LlenarGrid(ByVal ClaveU As String, ClaveSucursal As String) As DataTable
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing

        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Privilegios_Get1", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)
            Crea_Parametro(cmd, "@Clave_Usuarios", SqlDbType.VarChar, ClaveU)
            Return EjecutaConsulta(cmd)
        Catch ex As Exception
            TrataEx(ex)
            cnn.Dispose()
            cmd.Dispose()
            Return Nothing
        End Try
    End Function
    Public Function fn_PrivilegiosAsignados(ByVal ClaveU As String, ClaveSucursal As String) As DataTable
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing

        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Privilegios_Get2", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)
            Crea_Parametro(cmd, "@Clave_Usuarios", SqlDbType.VarChar, ClaveU)
            Return EjecutaConsulta(cmd)
        Catch ex As Exception
            TrataEx(ex)
            cnn.Dispose()
            cmd.Dispose()
            Return Nothing
        End Try
    End Function



    Public Function fn_AgregaPrivilegios(ByVal Cve_Opcion As String, ByVal ClaveU As String, ClaveSucursal As String) As Boolean
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Try

            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Privilegios_Create", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)
            Crea_Parametro(cmd, "@Clave_Usuario", SqlDbType.VarChar, ClaveU)
            Crea_Parametro(cmd, "@Clave_Opcion", SqlDbType.VarChar, Cve_Opcion)
            Crea_Parametro(cmd, "@Status2", SqlDbType.VarChar, "A")
            EjecutarNonQuery(cmd)
        Catch ex As Exception
            TrataEx(ex)
            cmd.Dispose()
            cnn.Dispose()
            Return False
        End Try
        Return True
    End Function

    Public Function fn_BorrarPrivilegios(ByVal Cve_Opcion As String, ByVal ClaveU As String, ClaveSucursal As String, Status As String) As Boolean
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Privilegios_Delete3", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)
            Crea_Parametro(cmd, "@Clave_Usuario", SqlDbType.VarChar, ClaveU)
            Crea_Parametro(cmd, "@Clave_Opcion", SqlDbType.VarChar, Cve_Opcion)
            Crea_Parametro(cmd, "@Status2", SqlDbType.VarChar, Status)
            EjecutarNonQuery(cmd)
        Catch ex As Exception
            TrataEx(ex)
            cmd.Dispose()
            cnn.Dispose()
            Return False
        End Try
        Return True
    End Function

#End Region

#Region "Variables Privadas Usuarios CashFlow"

    Private Const _Secuencia As String = "012345678909876543210"
    Private _Accion As Acciones
    Private _Clave As String
    Private _Nombre As String
    Private _NombreCortoUser As String
    Private _ContrasenaActual As String
    Private _ContrasenaNueva As String
    Private _Confirmar As String
    Private _ClaveSucursal As String
    Private _Tipo_Usuario As Tipos_Usuarios
    Private _Status2_Usuario As String

#End Region

#Region "Propiedades Públicas Usuarios CashFlow"

    Public WriteOnly Property pAccion() As Acciones
        Set(ByVal value As Acciones)
            _Accion = value
        End Set
    End Property

    Public WriteOnly Property pClave() As String
        Set(ByVal value As String)
            _Clave = value
        End Set
    End Property

    Public WriteOnly Property pNombre() As String
        Set(ByVal value As String)
            _Nombre = value
        End Set
    End Property

    Public WriteOnly Property pNombreCortoUser() As String
        Set(ByVal value As String)
            _NombreCortoUser = value
        End Set
    End Property

    Public WriteOnly Property pContrasenaActual() As String
        Set(ByVal value As String)
            _ContrasenaActual = value
        End Set
    End Property

    Public WriteOnly Property pContrasenaNueva() As String
        Set(ByVal value As String)
            _ContrasenaNueva = value
        End Set
    End Property

    Public WriteOnly Property pConfirmar() As String
        Set(ByVal value As String)
            _Confirmar = value
        End Set
    End Property

    Public WriteOnly Property pTipo_Usuario() As Tipos_Usuarios
        Set(ByVal value As Tipos_Usuarios)
            _Tipo_Usuario = value
        End Set
    End Property

    Public WriteOnly Property pClave_Sucursal() As String
        Set(ByVal value As String)
            _ClaveSucursal = value
        End Set
    End Property

    Public WriteOnly Property pStatus2_Usuario As Object
        Set(value As Object)
            _Status2_Usuario = value
        End Set
    End Property
#End Region

#Region "Métodos Privados Usuarios CashFlow"

    ''' <summary>
    ''' Validación de Usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Public Function fn_Usuario_Validar() As Integer
        '-1 -> Error
        '0 -> Valor Correcto
        '1 -> Clave Incorrecta
        '2 -> Contraseña Incorrecta
        '3 -> Contraseña Incorrecta
        '4 -> Confirmación de Contraseña Incorrecta
        '5 -> Tipo Usuario Incorrecto
        '6 -> Nombre Incorrecto
        '7 -> Sucursal No Seleccionado o Incorrecto

        Dim Validacion As Byte

        Select Case _Accion
            Case Acciones.Login

                '------------Primero valida usuario...
                Validacion = fn_Datos_Validar()

                If Validacion <> 0 Then
                    Call fn_Propiedades_Limpiar()
                    Return Validacion
                End If

            Case Acciones.Nuevo, Acciones.Editar
                Validacion = fn_Datos_Validar()

                If Validacion <> 0 Then
                    Call fn_Propiedades_Limpiar()
                    Return Validacion
                End If

                Dim cnn As SqlConnection = Nothing
                Dim cmd As SqlCommand = Nothing
                Dim Tr As SqlTransaction = Nothing
                Try
                    If _Accion = Acciones.Nuevo Then

                        Dim ContraCod As String = PaginaBase.fn_Encode(_Clave)
                        If fn_Usuarios_Nuevo(_ClaveSucursal, _Clave, _Nombre, _NombreCortoUser, ContraCod, _Tipo_Usuario) Then
                            Return 0
                        End If

                    Else ' si es Modificar

                        cnn = Crea_ConexionSTD()
                        Tr = crear_Trans(cnn)
                        cmd = Crea_ComandoT(cnn, Tr, CommandType.StoredProcedure, "Usuarios_Get3")
                        Crea_Parametro(cmd, "@Clave_Usuario", SqlDbType.VarChar, _Clave)
                        Dim tipoAnt As Integer = EjecutarScalarT(cmd)



                        'A).-Revisa si era Usario tipo 2(administrador)
                        'Query = "Select Tipo_Usuario " & _
                        '        "From Usuarios " & _
                        '        "Where Clave_Usuario = '" & _Clave & "'"
                        'cmd = Crea_Comando(Query, CommandType.Text, cnn)
                        'Dim tipoAnt As Integer = EjecutarScalar(cmd)

                        If tipoAnt = 2 AndAlso (_Tipo_Usuario = Tipos_Usuarios.Local) Then

                            cmd = Crea_ComandoT(cnn, Tr, CommandType.StoredProcedure, "Privilegios_Delete2")
                            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, _ClaveSucursal)
                            Crea_Parametro(cmd, "@Clave_Usuario", SqlDbType.VarChar, _ClaveSucursal)


                            'Query = "Delete P  " & _
                            '            "From Privilegios As P " & _
                            '            "Join Opciones As O On O.Clave_Opcion = P.Clave_Opcion  " & _
                            '            "Where P.Clave_Usuario = '" & _Clave & "' " & _
                            '            "And P.Clave_Sucursal = '" & _ClaveSucursal & "' " & _
                            '            "And O.Tipo = 2 "
                            'cmd = Crea_Comando(Query, CommandType.Text, cnn)
                            EjecutarNonQueryT(cmd)
                        End If


                        cmd = Crea_ComandoT(cnn, Tr, CommandType.StoredProcedure, "Usuarios_Update")
                        Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, _ClaveSucursal)
                        Crea_Parametro(cmd, "@Clave_Usuario", SqlDbType.VarChar, _Clave)
                        Crea_Parametro(cmd, "@Nombre", SqlDbType.VarChar, _Nombre)
                        Crea_Parametro(cmd, "@Nombre_Corto", SqlDbType.VarChar, _NombreCortoUser)
                        Crea_Parametro(cmd, "@Tipo_Usuario", SqlDbType.TinyInt, CByte(_Tipo_Usuario))
                        Crea_Parametro(cmd, "@Status2", SqlDbType.VarChar, _Status2_Usuario)


                        'Query = "Update Usuarios " & _
                        '        "Set Nombre = '" & _Nombre & "', " & _
                        '         " Nombre_Corto = '" & _NombreCortoUser & "', " & _
                        '        "Clave_Sucursal= '" & _ClaveSucursal & "', " & _
                        '         "FechaModifica= GetDate(), " & _
                        '        "Tipo_Usuario = '" & _Tipo_Usuario & "' " & _
                        '        "Where Clave_Usuario = '" & _Clave & "'"
                        'cmd = Crea_Comando(Query, CommandType.Text, cnn)

                        EjecutarNonQueryT(cmd)
                        Tr.Commit()
                        Return 0

                    End If

                Catch ex As Exception
                    Tr.Rollback()
                    cnn.Dispose()
                    Tr.Dispose()
                    TrataEx(ex)
                    Return -1
                End Try

            Case Acciones.CambiarContrasena

                'Se le asigna este valor porque es el mismo usuario firmado que se cambiará su contraseña
                Validacion = fn_Datos_Validar()
                If Validacion <> 0 Then
                    'Call fn_Propiedades_Limpiar()
                    Return Validacion
                End If

                Try
                    Dim Dias_Expira As Byte = 35

                    Dim cnn As New SqlConnection(Session("ConexionCentral"))
                    Dim PasswordHASH As String = FormsAuthentication.HashPasswordForStoringInConfigFile(_ContrasenaNueva, "SHA1")
                    Dim Query As String = "Update usrctes " & _
                                          "Set Password = '" & PasswordHASH & "', " & _
                                          "Fecha_Expira= Getdate() + " & Dias_Expira & " " & _
                                          "Where Id_usuario = '" & Id_Usuario & "'"
                    Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)

                    EjecutarNonQuery(cmd)
                    Return 0

                Catch ex As Exception
                    TrataEx(ex)
                    Return -1
                End Try

            Case Acciones.Eliminar
                Dim cnn As SqlConnection = Nothing
                Dim Tr As SqlTransaction = Nothing
                Dim cmd As SqlCommand = Nothing

                Dim cantidadReg As Integer = 0
                ' Dim dt_depRet As DataTable = Nothing

                Try


                    '--------Checar si tiene Depositos----
                    cnn = Crea_ConexionSTD()
                    Tr = crear_Trans(cnn)
                    cmd = Crea_ComandoT(cnn, Tr, CommandType.StoredProcedure, "Depositos_Get2")
                    Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, _ClaveSucursal)
                    Crea_Parametro(cmd, "@Usuario_Registro", SqlDbType.VarChar, _Clave)



                    'Query = "Select count(Usuario_Registro) As cantUser From Depositos " & _
                    '        "Where Usuario_Registro='" & _Clave & "'"
                    'cmd = Crea_Comando(Query, CommandType.Text, cnn)
                    cantidadReg = EjecutarScalarT(cmd)

                    If cantidadReg > 0 Then
                        Return 1
                    End If
                    '---------checar en Retiros

                    cmd = Crea_ComandoT(cnn, Tr, CommandType.StoredProcedure, "Retiros_Get1")
                    Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, _ClaveSucursal)
                    Crea_Parametro(cmd, "@Usuario_Registro", SqlDbType.VarChar, _Clave)



                    'Query = "Select count(Usuario_Registro) As cantUser From Retiros " & _
                    '        "Where Usuario_Registro='" & _Clave & "'"
                    ''cmd = Crea_Comando(Query, CommandType.Text, cnn)
                    cantidadReg = EjecutarScalarT(cmd)

                    If cantidadReg > 0 Then
                        Return 1
                    End If
                    '-------------------------------

                    cmd = Crea_ComandoT(cnn, Tr, CommandType.StoredProcedure, "Privilegios_Delete")
                    Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, _ClaveSucursal)
                    Crea_Parametro(cmd, "@Clave_Usuario", SqlDbType.VarChar, _Clave)

                    'Query = "Delete Privilegios " & _
                    '       "Where Clave_Usuario = '" & _Clave & "'"
                    'cmd = Crea_Comando(Query, CommandType.Text, cnn)
                    EjecutarNonQueryT(cmd)

                    cmd = Crea_ComandoT(cnn, Tr, CommandType.StoredProcedure, "Usuarios_Delete2")
                    Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, _ClaveSucursal)
                    Crea_Parametro(cmd, "@Clave_Usuario", SqlDbType.VarChar, _Clave)
                    Crea_Parametro(cmd, "@Status2", SqlDbType.VarChar, _Status2_Usuario)

                    'Query = "Delete Usuarios " & _
                    '        "Where Clave_Usuario = '" & _Clave & "'"
                    'cmd = Crea_Comando(Query, CommandType.Text, cnn)
                    EjecutarNonQueryT(cmd)
                    Tr.Commit()
                    Return 0
                Catch ex As Exception
                    Tr.Rollback()
                    Tr.Dispose()
                    cmd.Dispose()
                    cnn.Dispose()
                    TrataEx(ex)
                    Return -1
                End Try
        End Select
        Return 0 '
    End Function
#End Region

#Region "Funciones Privadas Usuarios CashFlow"

    Public Function fn_Usuarios_Read(ByVal Clave_Usuario As Integer) As DataTable

        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim Query As String = "Select Clave_Usuario, " & _
                                   "Nombre, " & _
                                     "Nombre_Corto, " & _
                                   "Tipo_Usuario, " & _
                                   "Fecha_Expira, " & _
                                   "Clave_Sucursal, " & _
                                   "Password " & _
                                   "From Usuarios " & _
                                   "Where Clave_Usuario = '" & Clave_Usuario & "'"
            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
            Dim dt As DataTable = EjecutaConsulta(cmd)
            Return dt

        Catch ex As Exception
            TrataEx(ex)
            Return Nothing
        End Try

    End Function

    Public Function fn_Usuarios_Nuevo(ByVal ClaveS As String, ByVal ClaveU As String, ByVal Nombre As String, ByVal NombreCortoU As String, ByVal Contra As String, ByVal TipoUser As Byte) As Boolean
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing

        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Usuarios_Create", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveS)
            Crea_Parametro(cmd, "@Clave_Usuario", SqlDbType.VarChar, ClaveU)
            Crea_Parametro(cmd, "@Nombre", SqlDbType.VarChar, Nombre)
            Crea_Parametro(cmd, "@Nombre_Corto", SqlDbType.VarChar, NombreCortoU)
            Crea_Parametro(cmd, "@Password", SqlDbType.VarChar, Contra)
            Crea_Parametro(cmd, "@Tipo_Usuario", SqlDbType.TinyInt, TipoUser)
            Crea_Parametro(cmd, "@Usuario_Registro", SqlDbType.VarChar, Id_Usuario)
            Crea_Parametro(cmd, "@Status", SqlDbType.VarChar, "A")



            'Dim cnn As SqlConnection = Crea_ConexionSTD()
            ''Dim contraCod As String = cn_Encripta.fn_Encode(Contra)
            'Dim Query As String = "Insert Into Usuarios " & _
            '        "(Clave_Sucursal,Clave_Usuario,Nombre, Nombre_Corto, Password,Tipo_Usuario,Usuario_Registro,F  Status,Fecha_Expira, FechaSync, FechaModifica) " & _
            '        "Values ('" & ClaveS & "','" & ClaveU & "','" & Nombre & "','" & NombreCortoU & "','" & Contra & "'," & TipoUser & "," & Id_Usuario & ",Getdate(),'A',Getdate()-1, Null, Null)"

            EjecutarNonQuery(cmd)
            Return True
        Catch ex As Exception
            cnn.Dispose()
            cmd.Dispose()
            TrataEx(ex)
            Return False
        End Try
    End Function

    Public Function fn_Usuarios_LlenaCombo(ByVal ClaveSucursal As String) As DataTable
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Usuarios_Get5", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)
            Return EjecutaConsulta(cmd)
        Catch ex As Exception
            cnn.Dispose()
            cmd.Dispose()
            TrataEx(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Limpiar valores de las Propiedades
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub fn_Propiedades_Limpiar()
        pAccion = Acciones.X
        pClave = String.Empty
        pNombre = String.Empty
        pContrasenaActual = String.Empty
        pContrasenaNueva = String.Empty
        pConfirmar = String.Empty
        pClave_Sucursal = String.Empty
        pTipo_Usuario = Tipos_Usuarios.Sistemassissa 'X
    End Sub

    ''' <summary>
    ''' Revisar Existencia de la Clave de Usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fn_Clave_Existe() As Boolean
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing

        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Usuarios_Read2", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Usuario", SqlDbType.VarChar, _Clave)
            Dim dt_Usuario As DataTable = EjecutaConsulta(cmd)

            If dt_Usuario.Rows.Count > 0 Then
                Select Case _Accion
                    Case Acciones.Login
                        'Sólo el Login se debe de asignarle el Tipo Usuario
                        If dt_Usuario IsNot Nothing AndAlso dt_Usuario.Rows.Count > 0 Then
                            If dt_Usuario.Rows(0)("Status") = "A" Then
                                pTipo_Usuario = dt_Usuario.Rows(0)("Tipo_Usuario")
                                Session("Tipo_Usuario") = dt_Usuario.Rows(0)("Tipo_Usuario") '23febr p3endiente
                            End If
                        End If
                        Return True

                    Case Else
                        Return dt_Usuario IsNot Nothing AndAlso dt_Usuario.Rows.Count > 0
                End Select
            Else
                Return False
            End If
        Catch ex As Exception
            TrataEx(ex)
        End Try

        'Dim cnn As SqlConnection = Crea_ConexionSTD()
        ''editado 12marzo 10:12 add status
        'Dim Query As String = "Select Tipo_Usuario, " & _
        '                      "Status " & _
        '                      "From Usuarios " & _
        '                      "Where Clave_Usuario = '" & _Clave & "'" ' 

        '' And Clave_Sucursal='"&  &"'
        'Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
        'Dim dt_Usuario As DataTable = EjecutaConsulta(cmd)

        'If dt_Usuario.Rows.Count > 0 Then
        '    Select Case _Accion
        '        Case Acciones.Login
        '            'Sólo el Login se debe de asignarle el Tipo Usuario
        '            If dt_Usuario IsNot Nothing AndAlso dt_Usuario.Rows.Count > 0 Then
        '                If dt_Usuario.Rows(0)("Status") = "A" Then
        '                    pTipo_Usuario = dt_Usuario.Rows(0)("Tipo_Usuario")
        '                    Session("Tipo_Usuario") = dt_Usuario.Rows(0)("Tipo_Usuario") '23febr p3endiente
        '                End If
        '            End If
        '            Return True

        '        Case Else
        '            Return dt_Usuario IsNot Nothing AndAlso dt_Usuario.Rows.Count > 0

        '    End Select
        'Else
        '    Return False
        'End If
    End Function

    ''' <summary>
    ''' Validación de la estructura de la Clave
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fn_Clave_Validar() As Boolean
        If _Secuencia.Contains(_Clave) Then Return False
        Return ValidarRepeticion(_Clave)
    End Function

    ''' <summary>
    ''' Revisar Existencia de la Contrasena de Usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fn_Contrasena_Existe(ByVal Contrasena_Existe As String, ByVal ClaveUsuario As String) As Boolean

        Dim cnn As SqlConnection = Crea_ConexionSTD()

        'Dim ContraCod As String = cn_Encripta.fn_Encode(Contrasena_Existe)
        Dim Query As String = "Select Clave_Usuario " & _
                              "From Usuarios " & _
                              "Where Password = '" & Contrasena_Existe & "' And Clave_Usuario= '" & ClaveUsuario & "'"

        Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
        Dim Clav As Integer = EjecutarScalar(cmd)

        If Clav = CInt(_Clave) Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Validación de la estructura de la Contraseña
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fn_Contrasena_Validar() As Boolean
        If _Secuencia.Contains(_ContrasenaNueva) Then Return False
        Return ValidarRepeticion(_ContrasenaNueva)
    End Function

    ''' <summary>
    ''' Validar la Repetición de los Dígitos Capturados
    ''' </summary>
    ''' <param name="Valor">El Datos que se Validará en Repetición de su contenido</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidarRepeticion(ByVal Valor As String) As Boolean
        Dim Ultima As Char = Nothing
        Dim Penultima As Char = Nothing

        For Each Letra As Char In Valor
            If Ultima = Nothing Then
                Ultima = Letra
            Else
                If Letra <> Ultima Then
                    Return True
                End If
            End If
        Next

        Return False
    End Function

    ''' <summary>
    ''' Validaciones Sencillas de los Datos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fn_Datos_Validar() As Integer
        '-1 -> Error
        '0 -> Valor Correcto
        '1 -> Clave Incorrecta
        '2 -> Contraseña Incorrecta
        '3 -> Contraseña Incorrecta
        '4 -> Confirmación de Contraseña Incorrecta
        '5 -> Tipo Usuario Incorrecto
        '6  capture el nombre de usurio
        '7 seleccione alguna sucursal

        'Dim rContrasena As Integer
        Dim rClave As Integer

        Select Case _Accion
            Case Acciones.Login
                'Validar Clave
                If _Clave.Trim = "" Then
                    Return 1
                End If
                If _Clave.Length < 4 OrElse _Clave.Length > 4 Then
                    Return 1
                End If
                If Not Integer.TryParse(_Clave, rClave) Then
                    Return 1
                End If
                If Not fn_Clave_Existe() Then
                    Return 1
                End If

                Session("Clave_Usuario") = CInt(_Clave)

                'Validar Contraseña Actual
                If _ContrasenaActual.Trim = "" Then
                    Session("Clave_Usuario") = 0
                    Return 2
                End If
                If _ContrasenaActual.Length < 8 OrElse _ContrasenaActual.Length > 8 Then
                    Session("Clave_Usuario") = 0
                    Return 2
                End If

                If Not fn_Contrasena_Existe(_ContrasenaActual, Session("Clave_Usuario")) Then
                    Session("Clave_Usuario") = 0
                    Return 2
                End If
                'valida que sea Usuario de Tipo 3..
                If Session("Tipo_Usuario") <> 3 Then
                    Session("Clave_Usuario") = 0
                    Return 5
                End If
                Return 8

            Case Acciones.Nuevo
                'Validar Clave
                If _Clave.Trim = "" Then
                    Return 1
                End If

                'If CInt(_Clave) < 1000 Then
                '    Return 12 ' la clave no debe inicar en 0 ejemplo :'0989'
                'End If

                If _Clave.StartsWith("0") Then
                    Return 12
                End If


                If _Clave.Length > 4 Then
                    Return 1
                End If
                If Not Integer.TryParse(_Clave, rClave) Then
                    Return 1
                End If

                If fn_Clave_Existe() Then
                    Return 1
                End If
                'If Not fn_Clave_Validar() Then
                '    Return 1
                'End If

                'Validar Nombre
                If _Nombre.Trim = "" Then
                    Return 6
                End If
                If _Nombre.Length > 50 Then
                    Return 6
                End If

                ''Validar Contraseña Nueva 'cometando 15/10/2015
                'If _ContrasenaNueva.Trim = "" Then
                '    Return 3
                'End If
                'If _ContrasenaNueva.Length < 8 OrElse _ContrasenaNueva.Length > 8 Then
                '    Return 3
                'End If

                'If Not PaginaBase.fn_Valida_Contra(_ContrasenaNueva) Then
                '    Return 3
                'End If

                ''Validar Confirmación de Contraseña
                'If _Confirmar.Trim = "" Then
                '    Return 4
                'End If
                'If _Confirmar <> _ContrasenaNueva Then
                '    Return 4
                'End If
                '*-----------------

                'Valida Tipo Usuario
                If _Tipo_Usuario = Tipos_Usuarios.Sistemassissa Then
                    Return 5
                End If
                If _Tipo_Usuario <> 3 Then
                    'Valida Sucursal
                    If _ClaveSucursal = "0" Or _ClaveSucursal = "0000" Then
                        Return 7
                    End If
                End If

            Case Acciones.Editar
                'Validar Nombre
                If _Nombre.Trim = "" Then
                    Return 6
                End If
                If _Nombre.Length > 50 Then
                    Return 6
                End If

                If _Tipo_Usuario = Tipos_Usuarios.Sistemassissa Then
                    Return 5
                End If
                If _Tipo_Usuario <> 3 Then
                    'Valida Sucursal
                    If _ClaveSucursal = "0" OrElse _ClaveSucursal = "0000" OrElse _ClaveSucursal = Nothing Then
                        Return 7
                    End If
                End If

            Case Acciones.CambiarContrasena

                'If _ContrasenaNueva.Length < 8 OrElse _Confirmar.Length < 8 Then
                '    Return 2
                'End If

                'Validar Contraseña Nueva
                If _ContrasenaNueva.Trim = "" Then
                    Return 3
                End If

                'Validar Confirmación de Contraseña
                If _Confirmar.Trim = "" Then
                    Return 4
                End If
                If _Confirmar <> _ContrasenaNueva Then
                    Return 4
                End If

        End Select

        Return 0
    End Function

#End Region

#Region "Soporte / Alertas"
    Public Function fn_ReporteFallaCashweb(ByVal NombreRegistro As String, ByVal Mail As String, ByVal Asunto As String, _
                                                  ByVal Descripcion As String, ByVal Cunica As String) As Boolean
        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim Query As String = "Insert Into Soporte " & _
                                  "(Fecha_Registro,Nombre,Mail,Asunto,Descripcion,Clave_Unica,Status) " & _
                                  "Values (Getdate(), '" & NombreRegistro & "','" & Mail & "','" & Asunto & "','" & Descripcion & "', " & _
                                  "'" & Cunica & "', 'A') "

            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)

            EjecutarNonQuery(cmd)
            Return True

        Catch ex As Exception
            TrataEx(ex)
            Return False
        End Try
    End Function

    Public Function fn_Consulta_Alertas(ByVal Desde As Date, ByVal Hasta As Date, ByVal ClaveSucursal As String) As DataTable

        Try
            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim cmd As SqlCommand
            Dim Query As String = "Select s.Nombre_Sucursal As NombreS, " & _
                                  "a.Descripcion, " & _
                                  "a.Detalles, " & _
                                  "a.Clave_UsuarioGenera As Usuario, " & _
                                  "Convert(Varchar(10),a.Fecha_Genera,103) As Fecha, " & _
                                  "Convert(Varchar(5),a.Fecha_Genera,108) As Hora " & _
                                  "From Alertas As a " & _
                                  "Join Sucursales as s On s.Clave_Sucursal= a.Clave_Sucursal " & _
                                  "Where CONVERT(varchar(10),a.Fecha_Genera, 102) Between " & _
                                  "'" & Format(Desde, "yyyy.MM.dd") & "' And '" & Format(Hasta, "yyyy.MM.dd") & "'"

            If ClaveSucursal <> "0" Then
                Query = Query & "And a.Clave_Sucursal= '" & ClaveSucursal & "'"
            End If
            Query = Query & " Order By CONVERT(varchar(10),a.Fecha_Genera, 102) "

            cmd = Crea_Comando(Query, CommandType.Text, cnn)
            Return EjecutaConsulta(cmd)

        Catch ex As Exception
            TrataEx(ex)
            Return Nothing
        End Try

    End Function

#End Region

#Region "Sucursales"

    Public Function fn_Sucursales_Monitoreo(ByVal MinutosConectado As Integer) As DataTable
        Try

            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim Query As String = "SELECT M.Clave_Sucursal As Clave, " &
                                            "M.Nombre_Corto As Sucursal, " &
                                            " (Case 	When (DateDiff(minute, M.ultima_conexion, Getdate())) > " & MinutosConectado &
                                                    " Then '~/Content/Images/ATM_DESCONECTADO.png'  " &
                                                    " Else '~/Content/Images/ATM_CONECTADO.png' End ) As Conexion " &
                                            "FROM Monitoreo As M " &
                                             "JOIN Sucursales As S On S.Clave_Sucursal = M.Clave_Sucursal " &
                                             "ORDER BY M.Nombre_Corto"
            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
            Return EjecutaConsulta(cmd)

        Catch ex As Exception
            TrataEx(ex)
            Return Nothing
        End Try

    End Function

    Public Function fn_GetConexionMonitoreo() As DataTable

        Dim cnn As SqlConnection = Crea_ConexionSTD()
        Dim cmd As SqlCommand
        Try
            cmd = Crea_Comando("[CashflowWEB_GetStatusCxnCajeros]", CommandType.StoredProcedure, cnn)
            Return EjecutaConsulta(cmd)
        Catch ex As Exception
            TrataEx(ex)
            Return Nothing
        End Try

    End Function
    Public Function fn_Sucursales_MonitoreoDetalle(ByVal claveSucursal As String) As DataTable
        Try

            Dim cnn As SqlConnection = Crea_ConexionSTD()
            Dim Query As String = "Select S.Nombre_corto, Isnull(S.Telefono,'') As Telefono, S.Domicilio ," & _
                                            "Convert(Varchar(8), GetDate() - Cast(Convert(Varchar(8),  M.ultima_conexion, 112) + ' ' " & _
                                            "+ Convert(Varchar(8), M.ultima_conexion, 108) AS DATETIME), 108) TiempoDesconexion " & _
                                            "From Monitoreo As M " & _
                                            "Join Sucursales As S On S.Clave_Sucursal = M.Clave_Sucursal " & _
                                            "Where S.Clave_Sucursal = '" & claveSucursal & "' "

            Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)

            Return EjecutaConsulta(cmd)

        Catch ex As Exception
            TrataEx(ex)
            Return Nothing
        End Try

    End Function

    Public Function fn_ConsultaSucursales() As DataTable
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Sucursales_Get1", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, "0000")


            'Dim cnn As SqlConnection = Crea_ConexionSTD()
            'Dim Query As String = "Select Clave_Sucursal As Clave, " & _
            '                      "Nombre_Sucursal As Descripcion " & _
            '                      "From Sucursales " & _
            '                      "Where Clave_Sucursal <> '0000'"
            'Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)

            Return EjecutaConsulta(cmd)

        Catch ex As Exception
            TrataEx(ex)
            Return Nothing
        End Try

    End Function

    Public Function fn_ConsultaSucursales_Llenagridview() As DataTable
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim dt_Sucursales As DataTable = Nothing

        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Sucursales_Get1", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, "0000")
            dt_Sucursales = EjecutaConsulta(cmd)
            'Dim cnn As SqlConnection = Crea_ConexionSTD()
            'Dim Query As String = "Select Clave_Sucursal As Clave, " & _
            '                      "Nombre_Sucursal As NombreSucursal, " & _
            '                      "Nombre_Corto As NombreCorto, " & _
            '                      "Clave_Cliente As ClaveCliente, " & _
            '                      "Domicilio, " & _
            '                      "Isnull(Telefono, ' ') As Telefono, " & _
            '                      "Num_Validadores As NumValidadores, " & _
            '                      "Case Status " & _
            '                              " When 'A' Then 'ACTIVO' " & _
            '                              " When 'B' Then 'BAJA' End As EStatus " & _
            '                      "From Sucursales " & _
            '                      "Where Clave_Sucursal <> '0000' " & _
            '                      "Order By Clave_Sucursal, Nombre_Sucursal "
            'Dim cmd As SqlCommand = Crea_Comando(Query, CommandType.Text, cnn)
            'Return EjecutaConsulta(cmd)
            Return dt_Sucursales
        Catch ex As Exception
            cnn.Dispose()
            cmd.Dispose()
            TrataEx(ex)
            Return Nothing
        End Try
    End Function

    Public Function fn_Sucursales_Nuevo(ByVal claveSucursal As String, ByVal Nombre As String, NombreCorto As String, _
                                               ByVal ClaveCliente As String, ByVal Domicilio As String, NumValidadores As String, Telefono As String) As Integer
        If claveSucursal.Trim = "" Then
            Return 0
        End If

        If Nombre.Trim = "" Then
            Return 2
        End If

        If Domicilio.Trim = "" Then
            Return 3
        End If

        If ClaveCliente.Trim = "" Then
            Return 4
        End If
        If NombreCorto.Trim = "" Then
            Return 5
        End If
        If NumValidadores = "" Then
            Return 6
        End If

        If CByte(NumValidadores) = 0 OrElse CByte(NumValidadores) > 2 Then
            Return 7
        End If
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing

        Try

            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Sucursales_Create1", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, claveSucursal)
            Crea_Parametro(cmd, "@Nombre_Sucursal", SqlDbType.VarChar, Nombre)
            Crea_Parametro(cmd, "@Nombre_Corto", SqlDbType.VarChar, NombreCorto)
            Crea_Parametro(cmd, "@Clave_Cliente", SqlDbType.VarChar, ClaveCliente)
            Crea_Parametro(cmd, "@Domicilio", SqlDbType.VarChar, Domicilio)
            Crea_Parametro(cmd, "@Status", SqlDbType.VarChar, "A")
            Crea_Parametro(cmd, "@Num_Validadores", SqlDbType.TinyInt, NumValidadores)
            Crea_Parametro(cmd, "@Telefono", SqlDbType.VarChar, Telefono)
            EjecutarNonQuery(cmd)
            Return 1
        Catch ex As Exception
            cnn.Dispose()
            cmd.Dispose()
            TrataEx(ex)
            Return -1
        End Try
    End Function

    Public Function fn_Sucursales_ClaveExiste(ByVal ClaveSucursal As String) As Boolean
        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim dt_Clave As DataTable

        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Sucursales_ClaveExistente", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)
            dt_Clave = EjecutaConsulta(cmd)

            If dt_Clave.Rows.Count > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            cnn.Dispose()
            cmd.Dispose()
            TrataEx(ex)
            Return False
        End Try
    End Function

    Public Function fn_Sucursales_Modificar(ByVal ClaveSucursal As String, ByVal NombreSucursal As String, NombreCorto As String, _
                                                   ByVal ClaveCliente As String, ByVal Domicilio As String, NumValidadores As String, telefono As String) As Integer
        If NombreSucursal.Trim = "" Then
            Return 2
        End If

        If Domicilio.Trim = "" Then
            Return 3
        End If

        If ClaveCliente.Trim = "" Then
            Return 4
        End If

        If NombreCorto.Trim = "" Then
            Return 5
        End If

        If NumValidadores = 0 Then
            Return 6
        End If

        If CByte(NumValidadores) = 0 OrElse CByte(NumValidadores) > 2 Then
            Return 7
        End If

        Dim cnn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing

        Try
            cnn = Crea_ConexionSTD()
            cmd = Crea_Comando("Sucursales_Update1", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)
            Crea_Parametro(cmd, "@Nombre_Sucursal", SqlDbType.VarChar, NombreSucursal)
            Crea_Parametro(cmd, "@Nombre_Corto", SqlDbType.VarChar, NombreCorto)
            Crea_Parametro(cmd, "@Clave_Cliente", SqlDbType.VarChar, ClaveCliente)
            Crea_Parametro(cmd, "@Domicilio", SqlDbType.VarChar, Domicilio)
            Crea_Parametro(cmd, "@Telefono", SqlDbType.VarChar, telefono)
            EjecutarNonQuery(cmd)
            Return 1
        Catch ex As Exception
            TrataEx(ex)
            Return -1
        End Try
    End Function

    Public Function fn_Sucursales_Eliminar(ByVal ClaveSucursal As String) As Integer
        Dim cnn As SqlConnection = Crea_ConexionSTD()
        Dim cmd As SqlCommand

        Try
            cmd = Crea_Comando("Usuarios_Get1", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)

            Dim cant_Usuarios As Int32 = EjecutarScalar(cmd)

            If cant_Usuarios > 0 Then
                Return 0
            End If

            cmd = Crea_Comando("Casets_Get1", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)



            Dim cant_Casets As Int32 = EjecutarScalar(cmd)
            If cant_Casets > 0 Then
                Return 0
            End If

            cmd = Crea_Comando("Sucursales_Delete1", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)

            EjecutarNonQuery(cmd)
            Return 1
        Catch ex As Exception
            TrataEx(ex)
            Return -1
        End Try
    End Function

#End Region


    Public Function fn_Depositos_Consulta_Banorte(ByVal Desde As Date, ByVal Hasta As Date, ByVal ClaveSucursal As String) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim dt As DataTable = Nothing
        Dim cnn As SqlConnection = Crea_ConexionSTD()

        'If Usuario_Clave = "0" Then Usuario_Clave = "0"
        If ClaveSucursal = "0" Then ClaveSucursal = "T"

        Try
            cmd = Crea_Comando("Depositos_GetBanorte", CommandType.StoredProcedure, cnn)
            Crea_Parametro(cmd, "@Clave_Sucursal", SqlDbType.VarChar, ClaveSucursal)
            Crea_Parametro(cmd, "@Fecha_Desde", SqlDbType.Date, Desde)
            Crea_Parametro(cmd, "@Fecha_Hasta", SqlDbType.Date, Hasta)
            dt = EjecutaConsulta(cmd)
        Catch ex As Exception
            cnn.Dispose()
            cmd.Dispose()
            Return Nothing
        End Try
        Return dt
    End Function
End Class
