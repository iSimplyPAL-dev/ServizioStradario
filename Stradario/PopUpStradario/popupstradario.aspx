<%@ Page Language="vb" AutoEventWireup="false" Codebehind="popupstradario.aspx.vb" Inherits="StradarioWeb.popUpStradario"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>Stradario</TITLE>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<frameset rows="40,*,0,0" framespacing="0" border="1" frameborder="no" id="frameVisualizza">
		<FRAME id="Comandi" name="Comandi" src="comandi.aspx" scrolling="no" noresize>
		<FRAME id="Visualizza" name="Visualizza" src="strade.aspx?CodEnte=<% = Request.Item("CodEnte") %>&TipoStrada=<% = Request.Item("TipoStrada") %>&Strada=<% = Request.Item("Strada") %>&CodStrada=<% = Request.Item("CodStrada") %>&CodTipoStrada=<% = Request.Item("CodTipoStrada") %>&Frazione=<% = Request.Item("Frazione") %>&CodFrazione=<% = Request.Item("CodFrazione") %>" scrolling="auto" noresize>
	</frameset>
</HTML>
