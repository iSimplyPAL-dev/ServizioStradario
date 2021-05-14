'Imports StradarioWeb.WsStradario.OggettoStrada
Imports OggettiComuniStrade
Imports log4net
''' <summary>
''' Pagina dei comandi per la consultazione/selezione delle vie.
''' Contiene i parametri di ricerca, le funzioni della comandiera e la griglia per la visualizzazione del risultato. 
''' </summary>
''' <remarks>Linee guida sviluppo 1.0</remarks>
Partial Class strade
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private Shared Log As ILog = LogManager.GetLogger(GetType(strade))
    Private objStrada As New OggettoStrada

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Not Page.IsPostBack Then
                ' leggo i parametri passati al popup
                ' il parametro codente è l'unico obbligatorio
                If Not Request.Item("CodEnte") Is Nothing Then
                    objStrada.CodiceEnte = Request.Item("CodEnte")
                Else
                    objStrada.CodiceEnte = ""
                End If
                objStrada.CodiceEnte = objStrada.CodiceEnte.PadLeft(6, CChar("0"))
                ' parametro Tipo Strada può essere inizializzato a stringa vuota
                If Not Request.Item("TipoStrada") Is Nothing And Request.Item("TipoStrada") <> "" Then
                    objStrada.TipoStrada = Request.Item("TipoStrada")
                End If
                If Not Request.Item("Strada") Is Nothing And Request.Item("Strada") <> "" Then
                    objStrada.DenominazioneStrada = Request.Item("Strada")
                End If
                If Not Request.Item("CodStrada") Is Nothing And Request.Item("CodStrada") <> "" Then
                    objStrada.CodiceStrada = Request.Item("CodStrada")
                End If
                If Not Request.Item("CodTipoStrada") Is Nothing And Request.Item("CodTipoStrada") <> "" Then
                    objStrada.CodTipoStrada = Integer.Parse(Request.Item("CodTipoStrada").ToString())
                End If
                If Not Request.Item("Frazione") Is Nothing And Request.Item("Frazione") <> "" Then
                    objStrada.Frazione = Request.Item("Frazione")
                End If
                If Not Request.Item("CodFrazione") Is Nothing And Request.Item("CodFrazione") <> "" Then
                    objStrada.CodFrazione = Integer.Parse(Request.Item("CodFrazione").ToString)
                End If

                If objStrada.CodiceEnte.CompareTo("") <> 0 Then
                    PopolaComboTipo()
                    If objStrada.CodiceStrada <> -1 Then
                        objStrada.DenominazioneStrada = ""
                    End If
                    PopolaGrigliaStrade(objStrada)
                End If
            End If
        Catch ex As Exception
            Log.Debug("Stradario.strade.page_load.errore: ", ex)
            Response.Redirect("../../PaginaErrore.aspx")
        End Try
    End Sub

    Private Sub btnRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRicerca.Click
        Try
            ' pulisco la variabile Session("Selezionata") per ribaltare la strada
            Session.Remove("Selezionata")
            objStrada.DenominazioneStrada = txtDescrizioneStrada.Text
            objStrada.CodiceEnte = Request.Item("CodEnte")
            If ddlTipoStrada.SelectedItem.Text = "..." Then
                objStrada.TipoStrada = ""
            Else
                objStrada.TipoStrada = ddlTipoStrada.SelectedItem.Text
            End If

            If ddlTipoStrada.SelectedItem.Value = "" Then
                objStrada.CodTipoStrada = -1
            Else
                objStrada.CodTipoStrada = Integer.Parse(ddlTipoStrada.SelectedItem.Value.ToString)
            End If

            PopolaGrigliaStrade(objStrada)
        Catch ex As Exception
            Log.Debug("Stradario.strade.btnRicerca_Click.errore: ", ex)
            Response.Redirect("../../PaginaErrore.aspx")
        End Try
    End Sub

    Private Sub PopolaGrigliaStrade(ByVal oStrada As OggettoStrada)
        Try
            Dim ArrStrade() As OggettoStrada

            '*** richiamando direttamente il servizio ***
            Dim TypeOfRI As Type = GetType(RemotingInterfaceOpenGovStradario.IRemotingInterfaceOpenGovStradario)
            Dim RemStradario As RemotingInterfaceOpenGovStradario.IRemotingInterfaceOpenGovStradario
            RemStradario = Activator.GetObject(TypeOfRI, ConfigurationManager.AppSettings("URLServizioStradario"))
            Log.Debug("PopolaGrigliaStrade::richiamo servizio GetArrayOggettoStrade::" & ConfigurationManager.AppSettings("URLServizioStradario") & "::connessione::" & System.Configuration.ConfigurationManager.AppSettings("ConnessioneDBComuniStrade") & "::ente::" & oStrada.CodiceEnte)
            ArrStrade = RemStradario.GetArrayOggettoStrade(ConfigurationManager.AppSettings("DBType"), ConfigurationManager.AppSettings("ConnessioneDBComuniStrade"), oStrada)
            '*** ***

            If Not ArrStrade Is Nothing Then
                If ArrStrade.Length > 0 Then
                    If ArrStrade.Length = 1 Then
                        txtDescrizioneStrada.Text = ArrStrade(0).DenominazioneStrada
                        ddlTipoStrada.SelectedIndex = ddlTipoStrada.Items.IndexOf(ddlTipoStrada.Items.FindByText(ArrStrade(0).TipoStrada))
                    End If
                    GrdStrade.DataSource = ArrStrade
                    ' popolo la variabile di sessione che mi permetterà di gestire la paginazione
                    Session("ArrStrade") = ArrStrade
                    GrdStrade.DataBind()
                End If
            Else
                Log.Debug("PopolaGrigliaStrade::no trovato strade")
            End If
        Catch ex As Exception
            Log.Debug("PopolaGrigliaStrade:.si è verificato il seguente errore::" & ex.Message)
            Response.Redirect("../../PaginaErrore.aspx")
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function PopolaComboTipo() As OggettoTipoStrada()
        Dim ArrTipoStrada() As OggettoTipoStrada
        Try
            Dim objTipoStrada As New OggettoTipoStrada

            objTipoStrada.CodiceEnte = objStrada.CodiceEnte

            '*** richiamando direttamente il servizio ***
            Dim TypeOfRI As Type = GetType(RemotingInterfaceOpenGovStradario.IRemotingInterfaceOpenGovStradario)
            Dim RemStradario As RemotingInterfaceOpenGovStradario.IRemotingInterfaceOpenGovStradario
            RemStradario = Activator.GetObject(TypeOfRI, ConfigurationManager.AppSettings("URLServizioStradario"))
            Log.Debug("PopolaComboTipo::richiamo servizio GetArrayTipoStrada::" & ConfigurationManager.AppSettings("URLServizioStradario") & "::connessione::" & System.Configuration.ConfigurationManager.AppSettings("ConnessioneDBComuniStrade") & "::ente::" & objTipoStrada.CodiceEnte)
            ArrTipoStrada = RemStradario.GetArrayTipoStrada(ConfigurationManager.AppSettings("DBType"), ConfigurationManager.AppSettings("ConnessioneDBComuniStrade"), objTipoStrada)
            '*** ***

            If Not ArrTipoStrada Is Nothing Then
                Dim myListItem As New ListItem

                myListItem.Text = "..."
                myListItem.Value = ""

                ddlTipoStrada.Items.Add(myListItem)
                Dim i As Integer
                For i = 0 To ArrTipoStrada.Length - 1
                    Dim MyListItem1 As New ListItem

                    MyListItem1.Text = ArrTipoStrada(i).TipoStrada
                    MyListItem1.Value = ArrTipoStrada(i).CodTipoStrada
                    ddlTipoStrada.Items.Add(MyListItem1)
                Next
            Else
                Log.Debug("PopolaComboTipo::no trovato toponimi")
            End If
        Catch ex As Exception
            Log.Debug("PopolaComboTipo:.si è verificato il seguente errore::" & ex.Message)
            Response.Redirect("../../PaginaErrore.aspx")
        End Try
        Return ArrTipoStrada
    End Function

    Public Sub WriteLOG(ByVal sFile As String, ByVal sDati As String)
        Dim MyFileToWrite As IO.StreamWriter = IO.File.AppendText(sFile)
        Try
            MyFileToWrite.WriteLine(sDati)
            MyFileToWrite.Flush()
        Catch ex As Exception
        Finally
            MyFileToWrite.Close()
        End Try
    End Sub

    Protected Sub GrdRowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try
            Dim IDRow As Integer = CInt(e.CommandArgument.ToString())
            If e.CommandName = "RowBind" Then
                For Each myRow As GridViewRow In GrdStrade.Rows
                    If myRow.Cells(3).Text = IDRow.ToString Then
                        objStrada = New OggettoStrada
                        objStrada.TipoStrada = myRow.Cells(0).Text
                        objStrada.DenominazioneStrada = myRow.Cells(1).Text
                        If LCase(myRow.Cells(2).Text).CompareTo("&nbsp;") <> 0 Then
                            objStrada.Frazione = myRow.Cells(2).Text
                        End If
                        objStrada.CodiceStrada = myRow.Cells(3).Text
                        objStrada.CodTipoStrada = CType(myRow.FindControl("hfCodTipoStrada"), HiddenField).Value
                        objStrada.CodFrazione = CType(myRow.FindControl("hfCodFrazione"), HiddenField).Value

                        Dim strScript As String = "<script language=""javascript"" type=""text/javascript"">"
                        strScript += "oggStrada = new OggettoStrada('" & objStrada.CodiceEnte & "','" & objStrada.CodiceStrada & "','" & objStrada.DenominazioneStrada.Replace("'", "\'") & "','" & objStrada.TipoStrada & "','" & objStrada.CodTipoStrada & "','" & objStrada.Frazione & "','" & objStrada.CodFrazione & "');"
                        'strScript += "alert(oggStrada.CodStrada);" 
                        strScript += "parent.opener." & Session("FunzioneRitorno") & "(oggStrada);"
                        strScript += "parent.window.close();"
                        'strScript += "}" & vbCrLf
                        strScript += "</script>"
                        Page.RegisterStartupScript("msgAlert", strScript)
                    End If
                Next
            End If
        Catch ex As Exception
            Log.Debug("Stradario.strade.GrdRowCommand::errore::", ex)
            Response.Redirect("../../PaginaErrore.aspx")
        End Try
    End Sub
    ''' <summary>
    ''' Gestione del cambio pagina della griglia.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub GrdPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        LoadSearch(e.NewPageIndex)
    End Sub
    ''' <summary>
    ''' Funzione per il popolamento della griglia con riposizionamento nella pagina selezionata.
    ''' </summary>
    ''' <param name="page"></param>
    Private Sub LoadSearch(Optional ByVal page As Integer? = 0)
        Try
            GrdStrade.DataSource = CType(Session("ArrStrade"), OggettoStrada())
            If page.HasValue Then
                GrdStrade.PageIndex = page.Value
            End If
            GrdStrade.DataBind()
        Catch ex As Exception
            Log.Debug("AStradario.strade.LoadSearch::errore::", ex)
            Throw ex
        End Try
    End Sub
    'Private Sub DGStrade_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGStrade.SelectedIndexChanged

    '    'glbStrada.CodFrazione = DGStrade.SelectedItem.Cells(1).Text
    '    glbStrada.TipoStrada = DGStrade.SelectedItem.Cells(0).Text
    '    glbStrada.DenominazioneStrada = DGStrade.SelectedItem.Cells(1).Text
    '    If LCase(DGStrade.SelectedItem.Cells(2).Text).CompareTo("&nbsp;") <> 0 Then
    '        glbStrada.Frazione = DGStrade.SelectedItem.Cells(2).Text
    '    End If
    '    glbStrada.CodiceStrada = DGStrade.SelectedItem.Cells(3).Text
    '    glbStrada.CodTipoStrada = DGStrade.SelectedItem.Cells(4).Text
    '    glbStrada.CodFrazione = DGStrade.SelectedItem.Cells(5).Text

    '    Session("Selezionata") = glbStrada

    '    DGStrade.SelectedItem.BackColor = Color.Aqua

    'End Sub

    'Private Sub btnAssocia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssocia.Click
    '    Dim strScript As String = ""
    '    If Not Session("Selezionata") Is Nothing Then
    '        ' se c'è una strada selezionata vado a ribaltare i campi
    '        'OggettoStrada(
    '        '	CodiceEnte,
    '        '	CodStrada,
    '        '	Strada,
    '        '	TipoStrada,
    '        '	CodTipoStrada,
    '        '	Frazione,
    '        '   CodFrazione()
    '        ')
    '        Dim objStrada As OggettoStrada = CType(Session("Selezionata"), OggettoStrada)

    '        strScript += "<script language=""javascript"" type=""text/javascript"">" & vbCrLf
    '        'strScript += "function RibaltaOggetto(){" & vbCrLf
    '        strScript += "oggStrada = new OggettoStrada('" & objStrada.CodiceEnte & "','" & objStrada.CodiceStrada & "','" & objStrada.DenominazioneStrada.Replace("'", "\'") & "','" & objStrada.TipoStrada & "','" & objStrada.CodTipoStrada & "','" & objStrada.Frazione & "','" & objStrada.CodFrazione & "');" & vbCrLf
    '        strScript += "//alert(oggStrada.CodStrada);" & vbCrLf
    '        strScript += "parent.opener." & Session("FunzioneRitorno") & "(oggStrada);"
    '        strScript += "parent.window.close();"
    '        'strScript += "}" & vbCrLf
    '        strScript += "</script>" & vbCrLf
    '        Page.RegisterStartupScript("msgAlert", strScript)
    '    Else
    '        ' se non è stata selezionata nessuna strada devo avvisare l'utente che deve selezionarla.
    '        strScript += "<script language=""javascript"" type=""text/javascript"">"
    '        strScript += "alert('Attenzione, selezionare una Strada!');"
    '        strScript += "</script>"
    '        Page.RegisterStartupScript("msgAlert", strScript)
    '    End If
    'End Sub
End Class
