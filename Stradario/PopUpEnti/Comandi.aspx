<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Comandi.aspx.vb" Inherits="StradarioWeb.Comuni.Comandi" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>comandi</title>
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
		<script type="text/javascript" type="text/javascript">
			function EffettuaRicerca(){
				parent.Visualizza.document.getElementById('btnRicerca').click();
			}
			function Associa(){
				parent.Visualizza.document.getElementById('btnAssocia').click();
			}
		</script>
	</HEAD>
	<body class="SfondoGenerale" bottomMargin="0" leftMargin="0" topMargin="5" rightMargin="5">
		<input class="Bottone Bottoneannulla" id="btnChiudi" style="FLOAT: right" onclick="parent.window.close();" type="button" name="btnChiudi"> 
        <input type="button" name="btnRicerca" id="btnRicerca" class="Bottone Bottonericerca" style="FLOAT:right" onclick="EffettuaRicerca();"> 
        <input type="button" name="btnAssocia" id="btnAssocia" class="Bottone Bottoneassocia" style="display:none" onclick="Associa();">
	</body>
</HTML>