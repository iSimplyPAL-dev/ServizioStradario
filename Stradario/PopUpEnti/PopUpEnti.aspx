<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PopUpEnti.aspx.vb" Inherits="StradarioWeb.Comuni.PopUpEnti" %>
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
		<frame id="Comandi" name="Comandi" src="comandi.aspx" scrolling="no" noresize>
		<frame id="Visualizza" name="Visualizza" src="Enti.aspx?FunzioneRitorno=<% = Request.Item("FunzioneRitorno") %>&CodBelfiore=<% = Request.Item("CodBelfiore") %>&Cap=<% = Request.Item("Cap") %>&CodCNC=<% = Request.Item("CodCNC") %>&CodIstat=<% = Request.Item("CodIstat") %>&Denominazione=<% = Request.Item("Denominazione") %>&Provincia=<% = Request.Item("Provincia") %>" scrolling="auto" noresize>
	</frameset>
</HTML>
