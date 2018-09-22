Imports System.Net
Imports System.Text
Imports System.IO

Public Class Form1
    Private http As HttpListener
    Const webpage As String = "<html>
        <head>
        <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> 
        </head>
        <body>
        <h1>Kantai Collection Cookies Region Changer</h1>
        <h1>成功轉換地區</h1>
        <script>
        document.cookie = 'cklg=welcome;expires=Sun, 09 Feb 2022 09:00:09 GMT;domain=.dmm.com;path=/';
        document.cookie = 'cklg=welcome;expires=Sun, 09 Feb 2022 0900:09 GMT;domain=.dmm.com;path=/netgame/';
        document.cookie = 'cklg=welcome;expires=Sun, 09 Feb 2022 0900:09 GMT;domain=.dmm.com;path=/netgame_s/';
        document.cookie = 'ckcy=1;expires=Sun, 09 Feb 2022 0900:09 GMT;domain=.dmm.com;path=/';
        document.cookie = 'ckcy=1;expires=Sun, 09 Feb 2022 0900:09 GMT;domain=.dmm.com;path=/netgame/';
        document.cookie = 'ckcy=1;expires=Sun, 09 Feb 2022 0900:09 GMT;domain=.dmm.com;path=/netgame_s/';
        if(window.location.href != 'http://www.dmm.com/netgame/social/-/gadgets/=/app_id=854854/'){
            window.location = 'http://www.dmm.com/netgame/social/-/gadgets/=/app_id=854854/';
        }
        </script>
        </body>
        </html>"

    Private Sub requestWait(ByVal ar As IAsyncResult)
        If Not http.IsListening Then
            Return
        End If
        Dim c = http.EndGetContext(ar)
        http.BeginGetContext(AddressOf requestWait, Nothing)
        returnDirContents(c)
    End Sub

    Private Sub returnDirContents(ByVal context As HttpListenerContext)
        context.Response.ContentType = "text/html"
        context.Response.ContentEncoding = Encoding.UTF8
        Using sw = New StreamWriter(context.Response.OutputStream)
            sw.WriteLine(webpage)
        End Using
        context.Response.OutputStream.Close()
    End Sub

    Private Sub FlatToggle1_CheckedChanged(sender As Object) Handles FlatToggle1.CheckedChanged
        If FlatToggle1.Checked Then
            http = New HttpListener()
            http.Prefixes.Add("http://+:80/")
            http.Start()
            http.BeginGetContext(AddressOf requestWait, Nothing)
            Try
                File.AppendAllText("C:\Windows\System32\drivers\etc\hosts", vbCrLf & "127.0.0.1 dmm.com" & vbCrLf & "127.0.0.1 www.dmm.com")
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ERROR")
            End Try
        Else
            File.WriteAllText("C:\Windows\System32\drivers\etc\hosts",
                              System.IO.File.ReadAllText("C:\Windows\System32\drivers\etc\hosts").
                              Replace(vbCrLf & "127.0.0.1 dmm.com", "").
                              Replace(vbCrLf & "127.0.0.1 www.dmm.com", ""))
            http.Stop()
        End If
    End Sub

End Class