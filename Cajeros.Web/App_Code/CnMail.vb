Imports System.Net.Mail
Imports System.Security.Cryptography
Public Class CnMail

    Public Shared Function fn_Enviar_Mail(ByVal Asunto As String, ByVal Texto As String) As Boolean
        Dim MailServer As String
        Dim MailRemitente As String
        Dim MailClave As String

        Dim MailRemitenteNombre As String
        Dim MailUser As String
        Dim Hubo_Destinos As Boolean = False
        Dim SMTP As New System.Net.Mail.SmtpClient 'Variable con la que se envia el correo
        Dim CORREO As New System.Net.Mail.MailMessage 'Variable que amlmacena los Attachment

        MailServer = "mail.sissaseguridad.com"
        MailUser = "siac@sissaseguridad.com"
        MailRemitente = "siac@sissaseguridad.com"
        MailRemitenteNombre = "Alertas SIAC CashWeb"
        MailClave = "AlertaS.010*"

        'Configuracion del Mensaje
        CORREO.From = New System.Net.Mail.MailAddress(MailRemitente, MailRemitenteNombre, System.Text.Encoding.UTF8)

        Dim Destino As String = "soportesiac@sissaseguridad.com"

        CORREO.[To].Add(Destino)

        CORREO.Subject = Asunto
        CORREO.IsBodyHtml = False
        CORREO.Body = Texto

        SMTP.Host = MailServer
        SMTP.UseDefaultCredentials = False
        SMTP.Credentials = New System.Net.NetworkCredential(MailUser, MailClave)

        Try
            SMTP.Send(CORREO)
            fn_Enviar_Mail = True
        Catch ex As System.Net.Mail.SmtpException
            fn_Enviar_Mail = False
        Finally
            CORREO.Dispose()
        End Try
    End Function
    Public Shared Function EnviarCorreo(
                                        ByVal Asunto As String,
                                        ByVal Descripcion As String,
                                        Optional ByVal Clave_Usuario As String = "",
                                        Optional ByVal Usuario As String = "Nadie") As Boolean

        Dim smtp As New System.Net.Mail.SmtpClient
        Dim correo As New System.Net.Mail.MailMessage

        Try


            correo.To.Add("programador1@sissaseguridad.com")

            With smtp
                .Port = 587
                .Host = "mail.sissaseguridad.com"
                .Credentials = New System.Net.NetworkCredential("alertas.cajeros@sissaseguridad.com", "Pass.010")
                .EnableSsl = False
            End With

            With correo
                .From = New System.Net.Mail.MailAddress("alertas.cajeros@sissaseguridad.com")
                .Subject = Asunto 
                '.SubjectEncoding = UTF8

                .Body = Descripcion
                .IsBodyHtml = True
                .Priority = System.Net.Mail.MailPriority.High

                '.Attachments.Add(adjunto)
            End With

            smtp.Send(correo)

            Return True
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Shared Function fn_Enviar_MailHTML(ByVal Asunto As String, ByVal Texto As String) As Boolean
        Dim MailServer As String
        Dim MailRemitente As String
        Dim MailClave As String

        Dim MailRemitenteNombre As String
        Dim MailUser As String
        Dim Hubo_Destinos As Boolean = False
        Dim SMTP As New System.Net.Mail.SmtpClient 'Variable con la que se envia el correo
        Dim CORREO As New System.Net.Mail.MailMessage 'Variable que amlmacena los Attachment

        MailServer = "mail.sissaseguridad.com"
        MailUser = "siac@sissaseguridad.com"
        MailRemitente = "siac@sissaseguridad.com"
        MailRemitenteNombre = "Alertas SIAC CashWeb"
        MailClave = "AlertaS.010*"

        'Configuracion del Mensaje
        CORREO.From = New System.Net.Mail.MailAddress(MailRemitente, MailRemitenteNombre, System.Text.Encoding.UTF8)

        Dim Destino As String = "soportesiac@sissaseguridad.com"

        CORREO.[To].Add(Destino)

        CORREO.Subject = Asunto
        CORREO.IsBodyHtml = True
        CORREO.Body = Texto

        SMTP.Host = MailServer
        SMTP.UseDefaultCredentials = False
        SMTP.Credentials = New System.Net.NetworkCredential(MailUser, MailClave)

        Try
            SMTP.Send(CORREO)
            fn_Enviar_MailHTML = True
        Catch ex As System.Net.Mail.SmtpException
            fn_Enviar_MailHTML = False
        Finally
            CORREO.Dispose()
        End Try
    End Function
End Class
