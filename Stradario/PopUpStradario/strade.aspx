<%@ Page Language="vb" AutoEventWireup="false" Codebehind="strade.aspx.vb" Inherits="StradarioWeb.strade" EnableEventValidation="false"%>
<%@ Register Assembly="Ribes.OPENgov.WebControls" Namespace="Ribes.OPENgov.WebControls" TagPrefix="Grd" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>strade</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../../Styles.css" type="text/css" rel="stylesheet">
		<%        If Session("SOLA_LETTURA") = "1" Then%>
		<link href="../../solalettura.css" type="text/css" rel="stylesheet">
		<%end if%>
	    <script type="text/javascript" src="../../_js/jquery.min.js?newversion"></script>
        <script type="text/javascript" src="../../_js/Custom.js?newversion"></script>
		<script src="../../_js/funzioniStradario.js?newversion" type="text/javascript" language="javascript" />
	</HEAD>
	<body class="SfondoVisualizza">
		<form id="Form1" runat="server" method="post">
			<fieldset class="classeFiledSetRicerca"><legend class="Legend">Ricerca Strade</legend>
				<table style="MARGIN: 0px 10px 10px">
					<tr>
						<td style="WIDTH: 180px"><asp:label id="lblTipoStrada" CssClass="Input_Label" Runat="server">Tipo Strada: </asp:label><br>
							<asp:dropdownlist id="ddlTipoStrada" CssClass="Input_Text" Runat="server" Width="150px"></asp:dropdownlist></td>
						<td><asp:label id="lblDescStrada" CssClass="Input_Label" Runat="server">Descrizione Strada: </asp:label><br>
							<asp:textbox id="txtDescrizioneStrada" runat="server" CssClass="Input_Text" Width="200px"></asp:textbox></td>
					</tr>
				</table>
			</fieldset>
			<br>
			<br>
            <Grd:RibesGridView ID="GrdStrade" runat="server" BorderStyle="None" 
                BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%"
                AutoGenerateColumns="False" AllowPaging="true" AllowSorting="false" PageSize="10"
                ErrorStyle-Font-Bold="True" ErrorStyle-ForeColor="Red"
                OnRowCommand="GrdRowCommand" OnPageIndexChanging="GrdPageIndexChanging">
                <PagerSettings Position="Bottom"></PagerSettings>
                <PagerStyle CssClass="CartListFooter" />
                <RowStyle CssClass="CartListItem"></RowStyle>
                <HeaderStyle CssClass="CartListHead"></HeaderStyle>
                <AlternatingRowStyle CssClass="CartListItem"></AlternatingRowStyle>
				<Columns>
					<asp:BoundField DataField="TipoStrada" HeaderText="Tipo Strada"></asp:BoundField>
					<asp:BoundField DataField="DenominazioneStrada" HeaderText="Denominazione"></asp:BoundField>
					<asp:BoundField DataField="Frazione" HeaderText="Frazione"></asp:BoundField>
					<asp:BoundField DataField="CodiceStrada" HeaderText="CodStrada"></asp:BoundField>
					<asp:TemplateField HeaderText="">
						<headerstyle horizontalalign="Center"></headerstyle>
						<itemstyle horizontalalign="Center"></itemstyle>
						<itemtemplate>
							<asp:ImageButton runat="server" Cssclass="BottoneGrd BottoneAssociaGrd" CommandName="RowBind" CommandArgument='<%# Eval("codicestrada") %>' alt=""></asp:ImageButton>
                            <asp:HiddenField ID="hfCodTipoStrada" runat="server" Value='<%# Eval("CodTipoStrada") %>' />
                            <asp:HiddenField ID="hfCodFrazione" runat="server" Value='<%# Eval("CodFrazione") %>' />
						</itemtemplate>
					</asp:TemplateField>
				</Columns>
			</Grd:RibesGridView>
			<asp:button id="btnRicerca" style="DISPLAY:none" runat="server" Text="Button"></asp:button>
			<asp:button id="btnAssocia" style="DISPLAY:none" runat="server" Text="Button"></asp:button>
		</form>
	</body>
</HTML>
