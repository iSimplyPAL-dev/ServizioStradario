<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Enti.aspx.vb" Inherits="StradarioWeb.Comuni.Enti" %>
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
		<%if Session("SOLA_LETTURA")="1" then%>
		<link href="../../solalettura.css" type="text/css" rel="stylesheet">
		<%end if%>
	    <script type="text/javascript" src="../../_js/jquery.min.js?newversion"></script>
        <script type="text/javascript" src="../../_js/Custom.js?newversion"></script>
		<script type="text/javascript" src="../../_js/funzioniEnti.js?newversion" type="text/javascript"></script>
		<script type="text/javascript" type="text/javascript">
			function MessaggioDebug(msg){
				alert(msg);
			}
		</SCRIPT>
</HEAD>
	<body class="SfondoVisualizza">
		<form id="Form1" runat="server" method="post">
			<fieldset class="classeFiledSetRicerca"><legend class="Legend">Ricerca Comuni</legend>
				<table style="MARGIN: 0px 10px 10px">
					<tr>
						<td style="WIDTH: 222px"><asp:label id="lblComune" CssClass="Input_Label" Runat="server">Comune: </asp:label><br>
							<asp:textbox id="txtComune" runat="server" CssClass="Input_Text" Width="200px"></asp:textbox></td>
						<td style="WIDTH: 73px"><asp:label id="lblProv" CssClass="Input_Label" Runat="server">Provincia : </asp:label><br>
							<asp:textbox id="txtProvincia" runat="server" CssClass="Input_Text" Width="50px"></asp:textbox></td>
						<td><asp:label id="Label1" CssClass="Input_Label" Runat="server">Cap : </asp:label><br>
							<asp:textbox id="txtCap" runat="server" CssClass="Input_Text" Width="50px"></asp:textbox></td>
						<td><asp:label id="Label2" CssClass="Input_Label" Runat="server">Belfiore : </asp:label><br>
							<asp:textbox id="txtBelfiore" runat="server" CssClass="Input_Text" Width="50px"></asp:textbox></td>
					</tr>
				</table>
			</fieldset>
			<br>
			<br>
            <Grd:RibesGridView ID="GrdEnti" runat="server" BorderStyle="None" 
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
					<asp:BoundField DataField="Denominazione" HeaderText="Comune"></asp:BoundField>
					<asp:BoundField DataField="Provincia" HeaderText="PV">
                        <HeaderStyle Width="60px" />
                        <ItemStyle Width="60px" />
					</asp:BoundField>
					<asp:BoundField DataField="Cap" HeaderText="CAP">
                        <HeaderStyle Width="80px" />
                        <ItemStyle Width="80px" />
					</asp:BoundField>
					<asp:TemplateField HeaderText="">
						<headerstyle horizontalalign="Center"></headerstyle>
						<itemstyle horizontalalign="Center"></itemstyle>
						<itemtemplate>
							<asp:ImageButton runat="server" Cssclass="BottoneGrd BottoneAssociaGrd" CommandName="RowBind" CommandArgument='<%# Eval("CodCNC") %>' alt=""></asp:ImageButton>
                            <asp:HiddenField runat="server" ID="hfCodCNC" Value='<%# Eval("CodCNC") %>' />
                            <asp:HiddenField runat="server" ID="hfCodBelfiore" Value='<%# Eval("CodBelfiore") %>' />
                            <asp:HiddenField runat="server" ID="hfStradario" Value='<%# Eval("Stradario") %>' />
                            <asp:HiddenField runat="server" ID="hfCodIstat" Value='<%# Eval("CodIstat") %>' />
						</itemtemplate>
					</asp:TemplateField>
					<asp:BoundField visible="false" DataField="CodIstat" HeaderText="Cod_Istat"></asp:BoundField>
				</Columns>
			</Grd:RibesGridView>
			<asp:button id="btnRicerca" runat="server" Text="btnRicerca" style="display:none"></asp:button>
			<asp:button id="btnAssocia" runat="server" Text="btnAssocia" style="display:none"></asp:button></form>
	</body>
</HTML>
