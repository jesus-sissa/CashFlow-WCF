Option Explicit On
Option Strict Off

Namespace Tools

    Public Class CashflowEventLog

        ''' <summary>
        ''' Escribe un registro en el visor de eventos de windows.
        ''' </summary>
        ''' <param name="Entry">Representa un string con el mensaje que se desea enviar al visor de eventos.</param>
        ''' <param name="AppName">Nombre de la aplicación que está generando este registro en el visor de eventos. </param>
        ''' <param name="EventType">Representa una enumeración que permite elegir cual es la naturaleza del mensaje que se desea enviar al registro de sucesos de windows.</param>
        ''' <param name="LogName">Indica el nombre del log de sucesos que se usará para almacenar el mensaje a enviar. Por default es Application.</param>
        ''' <returns>Verdadero si el registro pudo ser creado en el visor de eventos de forma satisfactoria.</returns>
        ''' <remarks>Enviar un mensaje al visor de suscesos de windows de la computadora que está ejecutando esta función.</remarks>
        Public Shared Function WriteToEventLog(ByVal Entry As String, Optional ByVal AppName As String = "Cashflow", Optional ByVal EventType As EventLogEntryType = EventLogEntryType.Information, Optional ByVal LogName As String = "Application") As Boolean
            Dim objEventLog As New EventLog()

            Try
                'Register the App as an Event Source
                If Not EventLog.SourceExists(AppName) Then
                    EventLog.CreateEventSource(AppName, LogName)
                End If

                objEventLog.Source = AppName

                'WriteEntry is overloaded; this is one
                'of 10 ways to call it
                objEventLog.WriteEntry(Entry, EventType)
                Return True
            Catch Ex As Exception
                Return False
            Finally
                objEventLog.Dispose()
            End Try

        End Function

        Public Shared Sub AppendToLogFile(ByVal Message As String)
            '' Verifica que el directorio de Logs exista...
            Dim LogPath As String = IO.Path.Combine(Environment.CurrentDirectory, "log\\")
            If Not IO.Directory.Exists(LogPath) Then IO.Directory.CreateDirectory(LogPath)
            Dim LogFileName As String = IO.Path.Combine(LogPath, String.Format("{0}Log{1}.txt", LogPath, DateTime.Now.ToString("yyyyMMdd")))

            Dim sw As IO.StreamWriter
            If Not System.IO.File.Exists(LogFileName) Then
                sw = IO.File.CreateText(LogFileName)
            Else
                sw = IO.File.AppendText(LogFileName)
            End If
            sw.WriteLine(String.Format("{0} - {1}", DateTime.Now.ToString(), Message))
            sw.Flush()
            sw.Close()
        End Sub
    End Class

End Namespace