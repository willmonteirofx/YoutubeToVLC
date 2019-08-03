





'Hi! Welcome, it's a mess here, but if this app gets any attention, I promise to comment and fix it all!






Public Class Form3

    Dim urlVideo As String = ""
    Dim urlVideoS As String = ""
    Dim Started As Boolean = False

    Dim CountVideos As Integer = 0

    Private Const WM_DRAWCLIPBOARD As Integer = &H308
    Private Declare Function SetClipboardViewer Lib "user32" Alias "SetClipboardViewer" (ByVal hwnd As Integer) As Integer

    Sub Clear()

        TextBox1.Text = Nothing
        urlVideo = Nothing
        urlVideoS = Nothing
        CountVideos = 0

    End Sub

    Sub hideForm()

        Opacity = 0

    End Sub

    Sub showForm()

        Opacity = 100
        Activate()

    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Top = 0
        Me.Left = (My.Computer.Screen.WorkingArea.Width \ 2) - (Me.Width \ 2)

        SetClipboardViewer(Handle.ToInt32)

        hideForm()

        NotifyIcon1.Visible = True
        NotifyIcon1.ShowBalloonTip(3000)

        Started = True

    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)

        If m.Msg = WM_DRAWCLIPBOARD And Clipboard.GetText.Contains("youtube") And Started = True Then

            urlVideo = Clipboard.GetText

            If TextBox1.Text = Nothing Then
                TextBox1.Text = """" & urlVideo & """" & " "
            Else
                TextBox1.Text = TextBox1.Text & """" & urlVideo & """" & " "
            End If

            showForm()

            Debug.Print(urlVideo)

            urlVideoS = TextBox1.Text

            CountVideos += 1
            Label1.Text = CountVideos

        End If

        MyBase.WndProc(m)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim process As Process = New Process()
        Dim startInfo As ProcessStartInfo = New ProcessStartInfo()
        startInfo.WindowStyle = ProcessWindowStyle.Hidden
        startInfo.FileName = "vlc.exe"
        startInfo.Arguments = "--fullscreen " & urlVideoS
        process.StartInfo = startInfo
        process.Start()

        hideForm()

        Clear()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        hideForm()

    End Sub

    Private Sub NotifyIcon1_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then 'Checks if the pressed button is the Right Mouse
            Close()
        End If
    End Sub
End Class