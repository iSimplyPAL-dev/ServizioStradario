Imports log4net
Imports OggettiComuniStrade

Namespace Comuni
    ''' <summary>
    ''' Pagina per la consultazione/selezione dei comuni.
    ''' Contiene i parametri di ricerca, le funzioni della comandiera e la griglia per la visualizzazione del risultato. 
    ''' </summary>
    ''' <remarks>Linee guida sviluppo 1.0</remarks>
    Partial Class Enti
        Inherits System.Web.UI.Page
        Private Shared Log As ILog = LogManager.GetLogger(GetType(Enti))

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

        Private objEnte As New OggettoEnte

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                If Not Page.IsPostBack Then
                    ' leggo i parametri passati al popup

                    ' il parametro codente è l'unico obbligatorio
                    ' CodEnte = Request.Item("CodEnte")
                    If Not Request.Item("CodBelfiore") Is Nothing Then
                        objEnte.CodBelfiore = Request.Item("CodBelfiore")
                    End If

                    If Not Request.Item("Cap") Is Nothing Then
                        objEnte.Cap = Request.Item("Cap")
                    End If

                    If Not Request.Item("CodCNC") Is Nothing Then
                        objEnte.CodCNC = Request.Item("CodCNC")
                    End If

                    If Not Request.Item("CodIstat") Is Nothing Then
                        objEnte.CodIstat = Request.Item("CodIstat")
                    End If

                    If Not Request.Item("Denominazione") Is Nothing Then
                        objEnte.Denominazione = Request.Item("Denominazione")
                    End If

                    If Not Request.Item("Provincia") Is Nothing Then
                        objEnte.Provincia = Request.Item("Provincia")
                    End If

                    PopolaGrigliaEnti(objEnte)
                    'Else
                    '    DGEnti.start_index = DGEnti.CurrentPageIndex
                    '    DGEnti.DataSource = CType(Session("ArrEnti"), OggettoEnte())
                End If
            Catch ex As Exception
                Log.Debug("Stradario.enti.page_load.errore: ", ex)
                Response.Redirect("../../PaginaErrore.aspx")
            End Try
        End Sub

        Private Sub btnRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRicerca.Click
            Try
                ' pulisco la variabile Session("Selezionata") per ribaltare la strada
                Session.Remove("Selezionata")

                'If txtDescrizioneStrada.Text <> "" Then
                objEnte.Denominazione = txtComune.Text
                objEnte.Provincia = txtProvincia.Text
                objEnte.Cap = txtCap.Text
                objEnte.CodBelfiore = txtBelfiore.Text

                PopolaGrigliaEnti(objEnte)

            Catch ex As Exception
                Log.Debug("Stradario.enti.btnRicerca_Click.errore: ", ex)
                Response.Redirect("../../PaginaErrore.aspx")
            End Try
        End Sub

        Private Sub PopolaGrigliaEnti(ByVal oEnte As OggettoEnte)
            'Private Sub PopolaGrigliaStrade(ByVal sCodEnte As String, ByVal sTipoStrada As String, ByVal sDescrizioneStrada As String, ByVal sCodTipoStrada As Integer, ByVal sCodFrazione As Integer, ByVal sFrazione As String)

            Try
                'Dim objStrada As New OggettoStrada
                Dim ArrEnti() As OggettoEnte

                '*** richiamando il ws ****
                'Dim objStradario As New WsStradario.Stradario
                'ArrEnti = objStradario.GetEnti(oEnte)
                '*** ***
                '*** richiamando direttamente il servizio ***
                Dim TypeOfRI As Type = GetType(RemotingInterfaceOpenGovStradario.IRemotingInterfaceOpenGovStradario)
                Dim RemStradario As RemotingInterfaceOpenGovStradario.IRemotingInterfaceOpenGovStradario
                RemStradario = Activator.GetObject(TypeOfRI, ConfigurationManager.AppSettings("URLServizioStradario"))

                ArrEnti = RemStradario.GetArrayEnti(ConfigurationManager.AppSettings("DBType"), ConfigurationManager.AppSettings("ConnessioneDBComuniStrade"), oEnte)
                '*** ***
                If Not ArrEnti Is Nothing Then
                    If ArrEnti.Length > 0 Then
                        If ArrEnti.Length = 1 Then
                            txtComune.Text = ArrEnti(0).Denominazione
                            txtProvincia.Text = ArrEnti(0).Provincia
                            txtCap.Text = ArrEnti(0).Cap
                            RibaltaEnte(ArrEnti(0))
                        End If
                        GrdEnti.DataSource = ArrEnti
                        ' popolo la variabile di sessione che mi permetterà di gestire la paginazione
                        Session("ArrEnti") = ArrEnti
                        GrdEnti.DataBind()
                    End If
                End If
            Catch ex As Exception
                Log.Debug("Stradario.enti.PopolaGrigliaEnti.errore: ", ex)
                Response.Redirect("../../PaginaErrore.aspx")
            End Try
        End Sub

        Protected Sub GrdRowCommand(sender As Object, e As GridViewCommandEventArgs)
            Try
                Dim IDRow As String = e.CommandArgument.ToString()
                If e.CommandName = "RowBind" Then
                    For Each myRow As GridViewRow In GrdEnti.Rows
                        If CType(myRow.FindControl("hfCodCNC"), HiddenField).Value = IDRow.ToString Then
                            objEnte = New OggettoEnte()
                            If myRow.Cells(0).Text.CompareTo("&nbsp;") <> 0 Then
                                objEnte.Denominazione = myRow.Cells(0).Text
                            End If
                            If myRow.Cells(1).Text.CompareTo("&nbsp;") <> 0 Then
                                objEnte.Provincia = myRow.Cells(1).Text
                            End If
                            If myRow.Cells(2).Text.CompareTo("&nbsp;") <> 0 Then
                                objEnte.Cap = myRow.Cells(2).Text
                            End If
                            If myRow.Cells(3).Text.CompareTo("&nbsp;") <> 0 Then
                                objEnte.CodIstat = CType(myRow.FindControl("hfCodIstat"), HiddenField).Value
                            End If
                            objEnte.CodCNC = IDRow
                            If CType(myRow.FindControl("hfCodBelfiore"), HiddenField).Value.CompareTo("&nbsp;") <> 0 Then
                                objEnte.CodBelfiore = CType(myRow.FindControl("hfCodBelfiore"), HiddenField).Value
                            End If
                            If CType(myRow.FindControl("hfStradario"), HiddenField).Value.CompareTo("&nbsp;") <> 0 Then
                                objEnte.Stradario = CBool(CType(myRow.FindControl("hfStradario"), HiddenField).Value)
                            End If

                            RibaltaEnte(objEnte)
                        End If
                    Next
                End If
            Catch ex As Exception
                Log.Debug("Stradario.Enti.GrdRowCommand::errore::", ex)
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
                GrdEnti.DataSource = CType(Session("ArrEnti"), OggettoEnte())
                If page.HasValue Then
                    GrdEnti.PageIndex = page.Value
                End If
                GrdEnti.DataBind()
            Catch ex As Exception
                Log.Debug("Stradario.Enti.LoadSearch::errore::", ex)
                Throw ex
            End Try
        End Sub
        'Private Sub DGEnti_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGEnti.SelectedIndexChanged

        '    'glbStrada.CodFrazione = DGStrade.SelectedItem.Cells(1).Text
        '    objEnte.Cap = ""
        '    objEnte.CodBelfiore = ""
        '    objEnte.CodCNC = ""
        '    objEnte.CodIstat = ""
        '    objEnte.Denominazione = ""
        '    objEnte.Provincia = ""
        '    objEnte.Stradario = False

        '    If DGEnti.SelectedItem.Cells(0).Text.CompareTo("&nbsp;") <> 0 Then
        '        objEnte.Denominazione = DGEnti.SelectedItem.Cells(0).Text
        '    End If
        '    If DGEnti.SelectedItem.Cells(1).Text.CompareTo("&nbsp;") <> 0 Then
        '        objEnte.Provincia = DGEnti.SelectedItem.Cells(1).Text
        '    End If
        '    If DGEnti.SelectedItem.Cells(2).Text.CompareTo("&nbsp;") <> 0 Then
        '        objEnte.Cap = DGEnti.SelectedItem.Cells(2).Text
        '    End If
        '    If DGEnti.SelectedItem.Cells(3).Text.CompareTo("&nbsp;") <> 0 Then
        '        objEnte.CodIstat = DGEnti.SelectedItem.Cells(3).Text
        '    End If
        '    If DGEnti.SelectedItem.Cells(4).Text.CompareTo("&nbsp;") <> 0 Then
        '        objEnte.CodCNC = DGEnti.SelectedItem.Cells(4).Text
        '    End If
        '    If DGEnti.SelectedItem.Cells(5).Text.CompareTo("&nbsp;") <> 0 Then
        '        objEnte.CodBelfiore = DGEnti.SelectedItem.Cells(5).Text
        '    End If
        '    If DGEnti.SelectedItem.Cells(6).Text.CompareTo("&nbsp;") <> 0 Then
        '        objEnte.Stradario = CBool(DGEnti.SelectedItem.Cells(6).Text)
        '    End If

        '    Session("Selezionata") = objEnte

        '    DGEnti.SelectedItem.BackColor = Color.Aqua

        'End Sub
        'Private Sub btnAssocia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssocia.Click
        '    Dim strScript As String = ""
        '    If Not Session("Selezionata") Is Nothing Then

        '        Dim objEnte As OggettoEnte = CType(Session("Selezionata"), OggettoEnte)

        '        RibaltaEnte(objEnte)
        '    Else
        '        ' se non è stata selezionata nessuna strada devo avvisare l'utente che deve selezionarla.
        '        strScript += "<script language=""javascript"" type=""text/javascript"">"
        '        strScript += "alert('Attenzione, selezionare un Ente!');"
        '        strScript += "</script>"
        '        Page.RegisterStartupScript("msgAlert", strScript)
        '    End If
        'End Sub
        Private Sub RibaltaEnte(ByVal objEnte As OggettoEnte)
            Dim strscript As String = String.Empty
            strscript += "<script language=""javascript"" type=""text/javascript"">" & vbCrLf
            'strScript += "function RibaltaOggetto(){" & vbCrLf
            strscript += "oggEnte = new OggettoEnte('" & objEnte.Denominazione.Replace("'", "\'") & "','" & objEnte.Cap & "','" & objEnte.Provincia & "','" & objEnte.CodIstat & "','" & objEnte.CodCNC & "','" & objEnte.CodBelfiore & "', '" & objEnte.Stradario & "');" & vbCrLf
            ' La funzione di ritorno deve essere contenuta all'interno della pagina che chiama il popup
            'strScript += "alert(oggEnte.Denominazione);"
            strscript += "parent.opener." + Session("FunzioneRitorno") + "(oggEnte);"
            strscript += "parent.window.close();"
            strscript += "</script>" & vbCrLf
            Page.RegisterStartupScript("msgAlert", strscript)
        End Sub
    End Class
End Namespace
