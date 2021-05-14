using System;

namespace OggettiComuniStrade
{


	/// <summary>
	/// Oggetto Strada che contiene tutte le proprietà della strada 
	/// </summary>
	[Serializable()]
	public class OggettoStrada
	{
		#region Property
			
		private int		_codicestrada = -1;
		private int		_codicestradademografico = -1;
		private string	_denominazionestrada = "";
		private string	_strada = "";
		private int		_codtipostrada = -1;
		private string	_tipostrada = "";
		private string	_codiceente = "-1";
		private int		_codfrazione = -1;
		private string	_frazione = "";
			
		/// <summary>
		/// Codice Strada inizializzato a -1
		/// </summary>
		public int CodiceStrada
		{
			get{return _codicestrada;}
			set{_codicestrada = value;}
		}
        /// <summary>
        /// 
        /// </summary>
		public int CodiceStradaDemografico
		{
			get{return _codicestradademografico;}
			set{_codicestradademografico = value;}
		}
		
		/// <summary>
		/// Denominazione Strada inizializzato a ""
		/// </summary>
		public string DenominazioneStrada
		{
			get{return _denominazionestrada;}
			set{_denominazionestrada = value;}
		}
		/// <summary>
		/// Toponimo+Denominazione Strada inizializzato a ""
		/// </summary>
		public string Strada
		{
			get{return _strada;}
			set{_strada = value;}
		}
		/// <summary>
		/// Codice Tipo Strada inizializzato a -1
		/// </summary>
		public int CodTipoStrada
		{
			get{return _codtipostrada;}	
			set{_codtipostrada = value;}
		}

		/// <summary>
		/// Tipo Strada inizializzato a ""
		/// </summary>
		public string TipoStrada
		{
			get{return _tipostrada;}
			set{_tipostrada = value;}
		}

		/// <summary>
		/// Codice Ente inizializzato a ""
		/// </summary>
		public string CodiceEnte
		{
			get{return _codiceente;}	
			set{_codiceente = value;}
		}

		/// <summary>
		/// Codice Frazione inizializzato a -1
		/// </summary>
		public int CodFrazione
		{
			get{return _codfrazione;}
			set{_codfrazione = value;}
		}

		/// <summary>
		/// Frazione inizializzato a ""
		/// </summary>
		public string Frazione
		{
			get{return _frazione;}
			set{_frazione = value;}
		}
			

		#endregion
	}
	/// <summary>
	/// Oggetto Tipo Strada contiene la codifica eventualmente contenuta in stradario e la descrizione
	/// </summary>
	[Serializable()]
	public class OggettoTipoStrada
	{
		#region Property
		
		private string _codente = "-1";
		private int _codtipostrada = -1;
		private string _tipostrada = "";
		
		/// <summary>
		/// Codice Ente inizializzato a valore "-1"
		/// </summary>
		public string CodiceEnte
		{
			get{return _codente;}
			set{_codente = value;}
		}

		/// <summary>
		/// Codice Tipo Strada inizializzato a -1
		/// </summary>
		public int CodTipoStrada
		{
			get{return _codtipostrada;}
			set{_codtipostrada = value;}
		}
		/// <summary>
		/// Tipo Strada inizializzato a ""
		/// </summary>
		public string TipoStrada
		{
			get{return _tipostrada;}
			set{_tipostrada = value;}
		}
		#endregion
	}


/// <summary>
/// OggettoEnte contiene tutti i dati di un ente
/// </summary>
	[Serializable()]
	public class OggettoEnte
	{
        #region Property
        private int _ID = -1;
		private string _codbelfiore = string.Empty;
		private string _denominazione = string.Empty;
		private string _codistat = string.Empty;
		private string _provincia = string.Empty;
		private string _cap = string.Empty;
		private string _codcnc = string.Empty;
		private bool _stradario = false;

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
	/// <summary>
    /// 
    /// </summary>
		public string CodBelfiore
		{
			get{return _codbelfiore;}
			set{_codbelfiore = value;}
		}
        /// <summary>
        /// 
        /// </summary>
		public string Denominazione
		{
			get{return _denominazione;}
			set{_denominazione = value;}
		}
        /// <summary>
        /// 
        /// </summary>
		public string Provincia
		{
			get{return _provincia;}
			set{_provincia = value;}
		}
        /// <summary>
        /// 
        /// </summary>
		public string Cap
		{
			get{return _cap;}
			set{_cap = value;}
		}
        /// <summary>
        /// 
        /// </summary>
		public string CodCNC
		{
			get{return _codcnc;}
			set{_codcnc = value;}
		}
        /// <summary>
        /// 
        /// </summary>
		public string CodIstat
		{
			get{return _codistat;}
			set{_codistat = value;}
		}
        /// <summary>
        /// 
        /// </summary>
		public bool Stradario
		{
			get{return _stradario;}
			set{_stradario = value;}
		}

		#endregion


		
	}
}
