' Credit to iSynthesis
' Edited by Huracan

' DARK FLAT UI

Imports System.Drawing.Drawing2D, System.ComponentModel, System.Windows.Forms

''' <summary>
''' Flat UI Theme
''' Creator: iSynthesis (HF)
''' Version: 1.0.3
''' Date Created: 17/06/2013
''' Date Changed: 20/06/2013
''' UID: 374648
''' For any bugs / errors, PM me.
''' </summary>
''' <remarks></remarks>

Module Helpers

#Region " Variables"
    Friend G As Graphics, B As Bitmap
    Friend _FlatColor As Color = Color.FromArgb(35, 168, 109)
    Friend NearSF As New StringFormat() With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Near}
    Friend CenterSF As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
#End Region

#Region " Functions"

    Public Function RoundRec(ByVal Rectangle As Rectangle, ByVal Curve As Integer) As GraphicsPath
        Dim P As GraphicsPath = New GraphicsPath()
        Dim ArcRectangleWidth As Integer = Curve * 2
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90)
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90)
        P.AddLine(New Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), New Point(Rectangle.X, Curve + Rectangle.Y))
        Return P
    End Function

    Public Function RoundRect(x!, y!, w!, h!, Optional r! = 0.3, Optional TL As Boolean = True, Optional TR As Boolean = True, Optional BR As Boolean = True, Optional BL As Boolean = True) As GraphicsPath
        Dim d! = Math.Min(w, h) * r, xw = x + w, yh = y + h
        RoundRect = New GraphicsPath

        With RoundRect
            If TL Then .AddArc(x, y, d, d, 180, 90) Else .AddLine(x, y, x, y)
            If TR Then .AddArc(xw - d, y, d, d, 270, 90) Else .AddLine(xw, y, xw, y)
            If BR Then .AddArc(xw - d, yh - d, d, d, 0, 90) Else .AddLine(xw, yh, xw, yh)
            If BL Then .AddArc(x, yh - d, d, d, 90, 90) Else .AddLine(x, yh, x, yh)

            .CloseFigure()
        End With
    End Function

    '-- Credit: AeonHack
    Public Function DrawArrow(x As Integer, y As Integer, flip As Boolean) As GraphicsPath
        Dim GP As New GraphicsPath()

        Dim W As Integer = 12
        Dim H As Integer = 6

        If flip Then
            GP.AddLine(x + 1, y, x + W + 1, y)
            GP.AddLine(x + W, y, x + H, y + H - 1)
        Else
            GP.AddLine(x, y + H, x + W, y + H)
            GP.AddLine(x + W, y + H, x + H, y)
        End If

        GP.CloseFigure()
        Return GP
    End Function

#End Region

End Module

#Region " Mouse States"
Enum MouseState As Byte
    None = 0
    Over = 1
    Down = 2
    Block = 3
End Enum
#End Region

Class FormSkin : Inherits ContainerControl

#Region " Variables"

    Private W, H As Integer
    Private Cap As Boolean = False
    Private _HeaderMaximize As Boolean = False
    Private MousePoint As New Point(0, 0)
    Private MoveHeight = 50

#End Region

#Region " Properties"

#Region " Colors"

    <Category("Colors")>
    Public Property HeaderColor() As Color
        Get
            Return _HeaderColor
        End Get
        Set(value As Color)
            _HeaderColor = value
        End Set
    End Property
    <Category("Colors")>
    Public Property BaseColor() As Color
        Get
            Return _BaseColor
        End Get
        Set(value As Color)
            _BaseColor = value
        End Set
    End Property
    <Category("Colors")>
    Public Property BorderColor() As Color
        Get
            Return _BorderColor
        End Get
        Set(value As Color)
            _BorderColor = value
        End Set
    End Property
    <Category("Colors")>
    Public Property FlatColor() As Color
        Get
            Return _FlatColor
        End Get
        Set(value As Color)
            _FlatColor = value
        End Set
    End Property

#End Region

#Region " Options"

    <Category("Options")>
    Public Property HeaderMaximize As Boolean
        Get
            Return _HeaderMaximize
        End Get
        Set(value As Boolean)
            _HeaderMaximize = value
        End Set
    End Property

#End Region

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If e.Button = Windows.Forms.MouseButtons.Left And New Rectangle(0, 0, Width, MoveHeight).Contains(e.Location) Then
            Cap = True
            MousePoint = e.Location
        End If
    End Sub

    Private Sub FormSkin_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Me.MouseDoubleClick
        If HeaderMaximize Then
            If e.Button = Windows.Forms.MouseButtons.Left And New Rectangle(0, 0, Width, MoveHeight).Contains(e.Location) Then
                If FindForm.WindowState = FormWindowState.Normal Then
                    FindForm.WindowState = FormWindowState.Maximized : FindForm.Refresh()
                ElseIf FindForm.WindowState = FormWindowState.Maximized Then
                    FindForm.WindowState = FormWindowState.Normal : FindForm.Refresh()
                End If
            End If
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e) : Cap = False
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        If Cap Then
            Parent.Location = MousePosition - MousePoint
        End If
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        ParentForm.FormBorderStyle = FormBorderStyle.None
        ParentForm.AllowTransparency = False
        ParentForm.TransparencyKey = Color.Fuchsia
        ParentForm.FindForm.StartPosition = FormStartPosition.CenterScreen
        Dock = DockStyle.Fill
        Invalidate()
    End Sub

#End Region

#Region " Colors"

#Region " Dark Colors"

    Private _HeaderColor As Color = Color.FromArgb(50, 50, 50)
    Private _BaseColor As Color = Color.FromArgb(50, 50, 50)
    Private _BorderColor As Color = Color.FromArgb(0, 170, 220)
    Private TextColor As Color = Color.FromArgb(212, 198, 209)

#End Region

#Region " Light Colors"

    Private _HeaderLight As Color = Color.FromArgb(171, 171, 172)
    Private _BaseLight As Color = Color.FromArgb(196, 199, 200)
    Public TextLight As Color = Color.FromArgb(45, 47, 49)

#End Region

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        BackColor = Color.White
        Font = New Font("Segoe UI", 12)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width : H = Height

        Dim Base As New Rectangle(0, 0, W, H), Header As New Rectangle(0, 0, W, 50)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Base
            .FillRectangle(New SolidBrush(_BaseColor), Base)

            '-- Header
            .FillRectangle(New SolidBrush(_HeaderColor), Header)

            '-- Logo
            .FillRectangle(New SolidBrush(Color.FromArgb(243, 243, 243)), New Rectangle(8, 16, 4, 18))
            .FillRectangle(New SolidBrush(Color.FromArgb(0, 170, 220)), 16, 16, 4, 18)
            .DrawString(Text, Font, New SolidBrush(TextColor), New Rectangle(26, 15, W, H), NearSF)

            '-- Border
            .DrawRectangle(New Pen(_BorderColor), Base)
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

Class FlatClose : Inherits Control

#Region " Variables"

    Private State As MouseState = MouseState.None
    Private x As Integer

#End Region

#Region " Properties"

#Region " Mouse States"

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        x = e.X : Invalidate()
    End Sub

    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
        Environment.Exit(0)
    End Sub

#End Region

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Size = New Size(18, 18)
    End Sub

#Region " Colors"

    <Category("Colors")>
    Public Property BaseColor As Color
        Get
            Return _BaseColor
        End Get
        Set(value As Color)
            _BaseColor = value
        End Set
    End Property

    <Category("Colors")>
    Public Property TextColor As Color
        Get
            Return _TextColor
        End Get
        Set(value As Color)
            _TextColor = value
        End Set
    End Property

#End Region

#End Region

#Region " Colors"

    Private _BaseColor As Color = Color.FromArgb(50, 50, 50)
    Private _TextColor As Color = Color.FromArgb(220, 220, 220)

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        BackColor = Color.White
        Size = New Size(18, 18)
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Font = New Font("Marlett", 13)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)

        Dim Base As New Rectangle(0, 0, Width, Height)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Base
            .FillRectangle(New SolidBrush(_BaseColor), Base)

            '-- X
            .DrawString("r", Font, New SolidBrush(TextColor), New Rectangle(0, 1, Width, Height), CenterSF)

            '-- Hover/down
            Select Case State
                Case MouseState.Over
                    .FillRectangle(New SolidBrush(Color.FromArgb(60, Color.Red)), Base)
                Case MouseState.Down
                    .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.Black)), Base)
            End Select
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class
