using System;
using OggettiComuniStrade;

namespace RemotingInterfaceOpenGovStradario
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public interface IRemotingInterfaceOpenGovStradario
		{
			// bool EstraiDichiarazioni(string AnnoRiferimento,string Ente, string CodContribuente,string[] strigheConfigurazione);			
			// Array di Oggetti TipoStrada;
			OggettoEnte[] GetArrayEnti(string TypeDB,string sConnection,OggettiComuniStrade.OggettoEnte objEnte);

			OggettoTipoStrada[] GetArrayTipoStrada(string TypeDB, string sConnection,OggettiComuniStrade.OggettoTipoStrada TipoStrada);
			
			OggettoStrada[] GetArrayOggettoStrade(string TypeDB, string sConnection,OggettiComuniStrade.OggettoStrada objStrada);			
		}
	
}
