Public Class Form2

    Dim ScreenCoord As Point
    Dim BrowserCoord As Point
    Dim elem As HtmlElement

    Dim NumVideos As Integer

    Dim GetedURL As String

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click

        GetedURL = WebBrowser1.Document.ActiveElement.GetAttribute("href").ToString

        If GetedURL.Contains("watch") Then

            Form1.ListBox1.Items.Add(GetedURL)

            NumVideos += 1

            If NumVideos = 1 Then
                Button2.Text = NumVideos & " Video"
            Else
                Button2.Text = NumVideos & " Videos"
            End If

        End If

    End Sub

    'Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click

    '    Debug.Print(elem.GetAttribute("href"))
    '    Form1.ListBox1.Items.Add(elem.GetAttribute("href"))
    '    TextBox1.Text = TextBox1.Text & vbNewLine & WebBrowser1.Document.ActiveElement.GetAttribute("href").ToString

    'End Sub

    'Private Sub ContextMenuStrip1_Opening(sender As Object, e As EventArgs) Handles ContextMenuStrip1.Opening

    '    ScreenCoord = New Point(MousePosition.X, MousePosition.Y)
    '    BrowserCoord = WebBrowser1.PointToClient(ScreenCoord)
    '    elem = WebBrowser1.Document.GetElementFromPoint(BrowserCoord)

    'End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click, Button2.Click
        Form1.Show()
    End Sub

End Class