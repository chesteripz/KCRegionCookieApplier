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
        <h1>艦娘餅乾烤爐 KCRegionCookieApplier</h1>
        <h2>成功烤餅乾<br>Cookie applied.</h2>
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

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        http = New HttpListener()
        http.Prefixes.Add("http://+:80/")
        http.Start()
        http.BeginGetContext(AddressOf requestWait, Nothing)
        Try
            File.AppendAllText("C:\Windows\System32\drivers\etc\hosts", vbCrLf & "127.0.0.1 dmm.com" & vbCrLf & "127.0.0.1 www.dmm.com")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ERROR")
        End Try
    End Sub

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
        File.WriteAllText("C:\Windows\System32\drivers\etc\hosts",
                              System.IO.File.ReadAllText("C:\Windows\System32\drivers\etc\hosts").
                              Replace(vbCrLf & "127.0.0.1 dmm.com", "").
                              Replace(vbCrLf & "127.0.0.1 www.dmm.com", ""))
        http.Stop()
        Me.Close()

    End Sub


End Class