Imports System.ComponentModel
Public Class Form1
    Dim color_list As New List(Of String)()
    Dim textBrush As Brush
    Dim red, green, blue
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Long) As Integer
    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Timer1.Stop()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If (GetAsyncKeyState(117)) Then
            Timer1.Start()
        End If
        If (GetAsyncKeyState(118)) Then
            Timer1.Stop()
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim BMP As New Drawing.Bitmap(1, 1)
        Dim GFX As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(BMP)
        GFX.CopyFromScreen(New Drawing.Point(MousePosition.X, MousePosition.Y),
        New Drawing.Point(0, 0), BMP.Size)
        Dim Pixel As Drawing.Color = BMP.GetPixel(0, 0)
        CPpanel.BackColor = Pixel
        txt_red.Text = Pixel.R
        txt_green.Text = Pixel.G
        txt_blue.Text = Pixel.B
        txt_hex.Text = ColorTranslator.ToHtml(Color.FromArgb(Pixel.ToArgb))


        Dim orasis As New Drawing.Bitmap(32, 25)
        Dim gx As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(orasis)
        gx.CopyFromScreen(New Drawing.Point(MousePosition.X - (10), MousePosition.Y - (10)), New Drawing.Point(0, 0), orasis.Size)
        Me.PictureBox1.Size = New Size(orasis.Size.Width * 5, orasis.Size.Height * 5)
        Me.PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        Me.PictureBox1.Image = orasis
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        Me.KeyPreview = True
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.Control AndAlso (e.KeyCode = Keys.Z)) Then
            Dim rgb = txt_red.Text & "," & txt_green.Text & "," & txt_blue.Text
            Clipboard.SetDataObject(rgb)
            lst_colors.Items.Add(rgb)
            color_list.Add(txt_hex.Text)
        End If
        If (e.Control AndAlso (e.KeyCode = Keys.X)) Then
            Clipboard.SetDataObject(txt_hex.Text)
            lst_colors.Items.Add(txt_hex.Text)
            color_list.Add(txt_hex.Text)
        End If
        lst_colors.DrawMode = DrawMode.OwnerDrawFixed
    End Sub
    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        about.Show()
    End Sub
    Private Sub ShortcutsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShortcutsToolStripMenuItem.Click
        MsgBox("Press Ctrl+X to copy Hex Color Code clipboard" & vbNewLine & "Press Ctrl+Z to copy RGB Color Code to clipboard ")
    End Sub

    Private Sub HowToUseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HowToUseToolStripMenuItem.Click
        MsgBox("Just move your mouse pointer/cursor around your object from which you want to pick a color." & vbNewLine & "Then press Ctril+Z or Ctrl+X to copy color code to clipboard")
    End Sub
    Public Function ConvertToRbg(ByVal HexColor As String) As Color
        HexColor = Replace(HexColor, "#", "")
        red = Val("&H" & Mid(HexColor, 1, 2))
        green = Val("&H" & Mid(HexColor, 3, 2))
        blue = Val("&H" & Mid(HexColor, 5, 2))
    End Function
    Private Sub lst_colors_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles lst_colors.DrawItem
        Dim drawFont As Font = e.Font
        Select Case e.Index
            Case e.Index
                Dim HexColor = ""
                HexColor = color_list(e.Index)
                ConvertToRbg(HexColor)
                textBrush = New Drawing.SolidBrush(Color.FromArgb(Convert.ToInt32(red), Convert.ToInt32(green), Convert.ToInt32(blue)))
                e.Graphics.FillRectangle(textBrush, e.Bounds)
                Exit Select
        End Select
        e.Graphics.DrawString(DirectCast(sender, ListBox).Items(e.Index).ToString(), e.Font, Brushes.White, e.Bounds, StringFormat.GenericDefault)
    End Sub
End Class
