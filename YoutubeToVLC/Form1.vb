

'Here was a slightly frustrated attempt to try to do everything internally, some references may be missing,
'so don 't worry if it doesn't work, here it really wasn't meant to do that!


Public Class Form1

    Private Const WM_DRAWCLIPBOARD As Integer = &H308
    Private Declare Function SetClipboardViewer Lib "user32" Alias "SetClipboardViewer" (ByVal hwnd As Integer) As Integer
    Dim UrlVideo As String

    Dim Index As Integer
    Dim volVid As Integer = 75

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        'Dim result As Integer = SetClipboardViewer(Handle.ToInt32)

        'Dim Options As String() = New String() {":network-caching=3000"}

        'For i = 0 To ListBox1.Items.Count - 1
        '    vlc.playlist.add(ListBox1.Items(i).ToString, Nothing, Options)
        'Next

        ListBox1.SelectedIndex = 0
        Index = ListBox1.SelectedIndex

        'Debug.Print(ListBox1.SelectedIndex.ToString)
        'Debug.Print(ListBox1.GetItemText(ListBox1.SelectedItem))

    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)

        If m.Msg = WM_DRAWCLIPBOARD And Clipboard.GetText.Contains("youtube") = True Then

            Process.Start("vlc " & Clipboard.GetText)

            '        If TextBox1.Text = Nothing Then
            '            TextBox1.Text = Clipboard.GetText.ToString
            '        Else
            '            TextBox1.Text = TextBox1.Text & vbNewLine & Clipboard.GetText.ToString
            '        End If

            '        ' AxVLCPlugin21.playlist.add(Clipboard.GetText.ToString)
            '        'AxVLCPlugin21.playlist.play()

            '        'UrlVideo = AxVLCPlugin21.mediaDescription.url.ToString

        End If

        MyBase.WndProc(m)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        vlc.playlist.add(ListBox1.GetItemText(ListBox1.SelectedItem))
        vlc.playlist.play()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        vlc.playlist.pause()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        vlc.video.toggleFullscreen()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        If Index >= 0 + 1 Then

            ' Button4.Enabled = True

            Index -= 1
            ListBox1.SelectedIndex = Index

            vlc.playlist.stop()
            vlc.playlist.items.clear()
            vlc.playlist.add(ListBox1.GetItemText(ListBox1.SelectedItem))
            vlc.playlist.play()

            ' Else
            ' Button5.Enabled = False
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If Index < ListBox1.Items.Count - 1 Then
            ' Button5.Enabled = True

            Index += 1
            ListBox1.SelectedIndex = Index

            vlc.playlist.stop()
            vlc.playlist.items.clear()
            vlc.playlist.add(ListBox1.GetItemText(ListBox1.SelectedItem))
            vlc.playlist.play()

            ' Else
            '  Button4.Enabled = False
        End If

    End Sub

    Private Enum InputState
        IDLE = 0
        OPENING = 1
        BUFFERING = 2
        PLAYING = 3
        PAUSED = 4
        STOPPING = 5
        ENDED = 6
        ERRORSTATE = 7
    End Enum

    Private Sub InfoTimer_Tick_1(sender As Object, e As EventArgs) Handles InfoTimer.Tick

        Dim state As InputState = vlc.input.state

        Select Case state
            Case InputState.IDLE, InputState.OPENING, InputState.BUFFERING
                Label1.Text = state.ToString()
                Exit Select
            Case InputState.PLAYING
                Dim title As String = vlc.mediaDescription.title

                Debug.Print("Tocando...")

                If Not title.Contains("watch") Then
                    Label2.Text = vlc.mediaDescription.title
                End If

                Dim current As TimeSpan = TimeSpan.FromMilliseconds(vlc.input.time)
                Dim total As TimeSpan = TimeSpan.FromMilliseconds(vlc.input.length)
                Dim pos As Double = vlc.input.position

                'Label1.Text = String.Format("{0} {1} {2}:{3:D2}/{4}:{5:D2} {6:P}", state, title, current.Minutes, current.Seconds, total.Minutes, total.Seconds, pos)

                Label1.Text = String.Format("{0} {1}:{2:D2}/{3}:{4:D2} {5:P}", state, current.Minutes, current.Seconds, total.Minutes, total.Seconds, pos)

                Exit Select
            Case InputState.PAUSED
                Label1.Text = String.Format("{0} {1}", state, IO.Path.GetFileName(vlc.mediaDescription.title))
                Exit Select
            Case InputState.STOPPING, InputState.ENDED

                Label1.Text = state.ToString()

                Debug.Print("Acabou")

                Exit Select
            Case InputState.ERRORSTATE
                Label1.Text = String.Format("{0} {1}", state, vlc.mediaDescription.title)
                Exit Select
            Case Else
                Label1.Text = state.ToString()
                Exit Select
        End Select

    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll

        ' If vlc.playlist.isPlaying = True Then

        'vlc.audio.volume = TrackBar1.Value

        'Debug.Print(TrackBar1.Value)

        ' End If

    End Sub

    Private Sub TrackBar1_MouseUp(sender As Object, e As MouseEventArgs) Handles TrackBar1.MouseUp
        vlc.audio.volume = TrackBar1.Value
        Debug.Print("Volume é: " & vlc.audio.volume)
    End Sub

    Private Sub vlc_MediaPlayerBuffering(sender As Object, e As AxAXVLC.DVLCEvents_MediaPlayerBufferingEvent) Handles vlc.MediaPlayerBuffering
        Debug.Print("Buffering...")
    End Sub

    Private Sub vlc_MediaPlayerPlaying(sender As Object, e As EventArgs) Handles vlc.MediaPlayerPlaying
        Debug.Print("Playing...")
    End Sub

End Class