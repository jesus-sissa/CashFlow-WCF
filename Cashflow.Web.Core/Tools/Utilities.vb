Option Explicit On
Option Strict Off
Imports System.Text

Namespace Tools

    Module Utilities

        Public Function EncodeData(ByVal Data As String) As String
            Try
                Dim encyrpt(0 To Data.Length - 1) As Byte
                encyrpt = System.Text.Encoding.UTF8.GetBytes(Data)
                Dim _encodedata As String = Convert.ToBase64String(encyrpt)
                Return _encodedata
            Catch ex As Exception
                Return ""
            End Try
        End Function

        Public Function DecodeData(ByVal Data As String) As String
            Try
                Dim encoder As New UTF8Encoding()
                Dim _decode As Decoder = encoder.GetDecoder()
                Dim bytes As Byte() = Convert.FromBase64String(Data)
                Dim count As Integer = _decode.GetCharCount(bytes, 0, bytes.Length)
                Dim decodechar(0 To count - 1) As Char
                _decode.GetChars(bytes, 0, bytes.Length, decodechar, 0)
                Dim result As New String(decodechar)
                Return result
            Catch ex As Exception
                Return ""
            End Try
        End Function
    End Module

End Namespace