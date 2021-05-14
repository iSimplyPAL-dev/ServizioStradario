using System;
using System.Configuration;
using RemotingInterfaceOpenGovStradario;
using StradarioOpenGov;
using OggettiComuniStrade;
using log4net;


namespace ServizioStradarioOpenGov
{
	/// <summary>
	/// Summary description for StradarioOpengovService.
	/// </summary>
	public class StradarioOpengovService:MarshalByRefObject,IRemotingInterfaceOpenGovStradario
	{
		public StradarioOpengovService()
		{
			//
			// TODO: Add constructor logic here
			//

		}

		public OggettoEnte[] GetArrayEnti(string TypeDB,string sConnection,OggettoEnte objEnte)
		{
			StradeManager objStradario = new StradeManager(sConnection);
			return objStradario.GetArrayEnti(TypeDB,sConnection,objEnte);
		}


		public OggettoTipoStrada[] GetArrayTipoStrada(string TypeDB, string sConnection,OggettoTipoStrada objTipoStrada)
		{
			StradeManager objStradario = new StradeManager(sConnection);
			return objStradario.GetArrayTipoStrada(TypeDB,sConnection,objTipoStrada);
		}

		public OggettoStrada[] GetArrayOggettoStrade(string TypeDB, string sConnection,OggettoStrada objStrada)
		{
			StradeManager objStradario = new StradeManager(sConnection);
			return objStradario.GetArrayOggettoStrade(TypeDB,sConnection,objStrada);
		}
	}
}
