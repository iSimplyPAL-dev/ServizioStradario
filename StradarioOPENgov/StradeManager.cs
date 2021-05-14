using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using RemotingInterfaceOpenGovStradario;
using Utility;
using OggettiComuniStrade;
using log4net;

namespace StradarioOpenGov
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public class StradeManager : IRemotingInterfaceOpenGovStradario
    {
        private string connectionString;
        private static readonly ILog log = LogManager.GetLogger(typeof(StradeManager));

        public StradeManager()
        {
            this.connectionString = ConfigurationSettings.AppSettings["connectionStringSQL"].ToString();
        }

        public StradeManager(string Connessione)
        {
            this.connectionString = Connessione;
        }

        public OggettoEnte[] GetArrayEnti(string TypeDB, string sConnection, OggettoEnte objEnti)
        {
            try
            {
                ArrayList ArrListEnti = new ArrayList();

                DBModel objDB = new DBModel(TypeDB, sConnection);
                log.Debug("StadeManager::GetArrayEnti::connessione a::" + sConnection);

                SqlCommand Cmd = new SqlCommand();

                //Cmd.CommandText = "SELECT *";
                //Cmd.CommandText += " FROM V_ENTI";
                //Cmd.CommandText += " WHERE 1 = 1 ";
                //if (objEnti.CodBelfiore.CompareTo(string.Empty) != 0)
                //{
                //    Cmd.CommandText += " AND IDENTIFICATIVO = @CodBelfiore";
                //}
                //if (objEnti.Denominazione.CompareTo(string.Empty) != 0)
                //{
                //    Cmd.CommandText += " AND COMUNE LIKE @Denominazione";
                //}
                //if (objEnti.Provincia.CompareTo(string.Empty) != 0)
                //{
                //    Cmd.CommandText += " AND PV = @Provincia";
                //}
                //if ((objEnti.Cap.CompareTo(string.Empty) != 0) && (objEnti.Cap.CompareTo("0") != 0))
                //{
                //    Cmd.CommandText += " AND CAP = @Cap";
                //}
                //if (objEnti.CodCNC.CompareTo(string.Empty) != 0)
                //{
                //    Cmd.CommandText += " AND COD_CNC = @CodCNC";
                //}
                //if ((objEnti.CodIstat.CompareTo(string.Empty) != 0) && (objEnti.CodIstat.CompareTo("0") != 0))
                //{
                //    Cmd.CommandText += " AND COD_ISTAT = @CodIstat";
                //}
                //Cmd.CommandText += " ORDER BY COMUNE ASC";
                //Cmd.Parameters.Add("@CodBelfiore", SqlDbType.NVarChar).Value = objEnti.CodBelfiore;
                //Cmd.Parameters.Add("@Denominazione",SqlDbType.NVarChar).Value = objEnti.Denominazione.Replace("*","") + "%";
                //Cmd.Parameters.Add("@Provincia", SqlDbType.NVarChar).Value = objEnti.Provincia;
                //Cmd.Parameters.Add("@Cap", SqlDbType.NVarChar).Value = objEnti.Cap;
                //Cmd.Parameters.Add("@CodCNC", SqlDbType.NVarChar).Value = objEnti.CodCNC;
                //Cmd.Parameters.Add("@CodIstat",SqlDbType.NVarChar).Value = objEnti.CodIstat;
                string sSQL = objDB.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_GetEnti", "CodBelfiore", "Denominazione", "Provincia", "Cap", "CodCNC", "CodIstat");
                DataView DR = (DataView)objDB.GetDataView(sSQL,"TBL", objDB.GetParam("CodBelfiore", objEnti.CodBelfiore)
                    , objDB.GetParam("Denominazione", objEnti.Denominazione.Replace("*", "%"))
                    , objDB.GetParam("Provincia", objEnti.Provincia)
                    , objDB.GetParam("Cap", objEnti.Cap)
                    , objDB.GetParam("CodCNC", objEnti.CodCNC)
                    , objDB.GetParam("CodIstat", objEnti.CodIstat)
                );
                if (DR.Table!=null)
                {
                    foreach (DataRow myRow in DR.Table.Rows)
                    {
                        // POPOLO L'ARRAYLIST DEGLI ENTI
                        OggettoEnte oEnte = new OggettoEnte();

                        oEnte.ID = StringOperation.FormatInt(myRow["ID"]);
                        oEnte.Cap =StringOperation.FormatString( myRow["CAP"] );
                        oEnte.CodBelfiore = StringOperation.FormatString(myRow["IDENTIFICATIVO"]);
                        oEnte.CodCNC = StringOperation.FormatString(myRow["COD_CNC"] );
                        oEnte.CodIstat = StringOperation.FormatString(myRow["COD_ISTAT"] );
                        oEnte.Denominazione= StringOperation.FormatString( myRow["COMUNE"] );
                        oEnte.Provincia = StringOperation.FormatString(myRow["PV"] );
                        oEnte.Stradario = myRow["CODBELFIORE"] == DBNull.Value ? false : true;

                        ArrListEnti.Add(oEnte);
                    }
                }
                OggettoEnte[] ArrayEnti = (OggettoEnte[])ArrListEnti.ToArray(typeof(OggettoEnte));
                return ArrayEnti;
            }
            catch (Exception Ex)
            {
                log.Debug("Errore nell'esecuzione di GetArrayEnti :: ", Ex);
                return null;
            }
        }

        //*** 20100311 - Sostituito query secca con interrogazione su vista ***//
        //		public OggettoTipoStrada[] GetArrayTipoStrada(OggettoTipoStrada objTipoStrada){
        //			try
        //			{
        //				ArrayList ArrListTipoStrada = new ArrayList();
        //			
        //
        //				DBManager objDB = new Utility.DBManager(this.connectionString);
        //			
        //				SqlCommand Cmd = new SqlCommand();
        //
        //				// SQL
        //				Cmd.CommandText = "SELECT * FROM T_TIPO_VIE WHERE 1 = 1 ";
        //				if (objTipoStrada.CodiceEnte.CompareTo("") != 0)
        //				{
        //					Cmd.CommandText += " AND COD_ENTE LIKE @CodEnte ";
        //					Cmd.Parameters.Add("@CodEnte", SqlDbType.NVarChar).Value = "%" + objTipoStrada.CodiceEnte; 
        //				}
        //				if (objTipoStrada.TipoStrada.CompareTo("") != 0)
        //				{
        //					Cmd.CommandText += " AND TOPONIMO LIKE  @Toponimo ";
        //					Cmd.Parameters.Add("@Toponimo", SqlDbType.NVarChar).Value = "%" + objTipoStrada.TipoStrada;
        //
        //				}
        //				if (objTipoStrada.CodTipoStrada.CompareTo(-1) != 0)
        //				{
        //					Cmd.CommandText += " AND ID_TOPONIMO LIKE @IdToponimo";
        //					Cmd.Parameters.Add("@IdToponimo", SqlDbType.Int).Value = objTipoStrada.CodTipoStrada;
        //				}
        //
        //				SqlDataReader DR = objDB.GetDataReader(Cmd);
        //
        //				if (DR.HasRows)
        //				{
        //					while (DR.Read())
        //					{
        //						// POPOLO L'ARRAYLIST DELLE STRADE
        //						OggettoTipoStrada TipoStrada = new OggettoTipoStrada();
        //						TipoStrada.CodiceEnte = DR["cod_ente"].ToString();
        //						TipoStrada.CodTipoStrada = int.Parse(DR["id_toponimo"].ToString());
        //						TipoStrada.TipoStrada = DR["toponimo"].ToString();
        //				
        //						ArrListTipoStrada.Add(TipoStrada);
        //					}
        //				}
        //
        //				OggettoTipoStrada[] ArrayTipiStrade = (OggettoTipoStrada[])ArrListTipoStrada.ToArray(typeof(OggettoTipoStrada));
        //
        //
        //				return ArrayTipiStrade;
        //			}
        //			catch (Exception Ex){
        //				log.Debug("Errore nell'esecuzione di GetArrayTipoStrada :: ", Ex);
        //				return null;
        //			}
        //		}
        //
        //*** 20100311 ***//

        public OggettoTipoStrada[] GetArrayTipoStrada(string TypeDB, string sConnection, OggettoTipoStrada objTipoStrada)
        {
            try
            {
                ArrayList ArrListTipoStrada = new ArrayList();

                //DBModel objDB = new DBModel(TypeDB,sConnection);
                log.Debug("StadeManager::GetArrayTipoStrada::connessione a::" + sConnection);

                SqlCommand Cmd = new SqlCommand();
                using (DBModel ctx = new DBModel(TypeDB, sConnection))
                {
                    //Cmd.CommandText = "SELECT *";
                    //Cmd.CommandText += " FROM V_TIPO_VIE";
                    //Cmd.CommandText += " WHERE (1 = 1)";
                    //Cmd.CommandText += " AND (@CodEnte='' OR V_TIPO_VIE.COD_ENTE LIKE @CodEnte)";
                    //Cmd.CommandText += " AND (@Toponimo='' OR V_TIPO_VIE.TOPONIMO LIKE  @Toponimo)";
                    //Cmd.CommandText += " AND (@IdToponimo<=0 OR V_TIPO_VIE.ID_TOPONIMO=@IdToponimo)";
                    //if (objTipoStrada.CodiceEnte.CompareTo("") != 0)
                    //{
                    //	Cmd.CommandText += " AND V_TIPO_VIE.COD_ENTE LIKE @CodEnte ";
                    //	Cmd.Parameters.Add("@CodEnte", SqlDbType.NVarChar).Value = "%" + objTipoStrada.CodiceEnte; 
                    //}
                    //if (objTipoStrada.TipoStrada.CompareTo("") != 0)
                    //{
                    //	Cmd.CommandText += " AND V_TIPO_VIE.TOPONIMO LIKE  @Toponimo ";
                    //	Cmd.Parameters.Add("@Toponimo", SqlDbType.NVarChar).Value = "%" + objTipoStrada.TipoStrada.Replace("*","%");
                    //}
                    //if (objTipoStrada.CodTipoStrada.CompareTo(-1) != 0)
                    //{
                    //	Cmd.CommandText += " AND V_TIPO_VIE.ID_TOPONIMO LIKE @IdToponimo";
                    //	Cmd.Parameters.Add("@IdToponimo", SqlDbType.Int).Value = objTipoStrada.CodTipoStrada;
                    //}
                    string sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure, "prc_GetTipoStrade", "CodEnte", "Toponimo", "IdToponimo");
                    SqlDataReader DR = (SqlDataReader)ctx.GetDataReader(sSQL, ctx.GetParam("CodEnte", objTipoStrada.CodiceEnte)
                        , ctx.GetParam("Toponimo",objTipoStrada.TipoStrada.Replace("*", "%"))
                        , ctx.GetParam("IdToponimo", objTipoStrada.CodTipoStrada)
                    );
                    log.Debug("prc_GetTipoStrade CodEnte=" +  objTipoStrada.CodiceEnte + ", Toponimo=" + objTipoStrada.TipoStrada.Replace("*", "%") + ", IdToponimo=" + objTipoStrada.CodTipoStrada.ToString());
                    if (DR.HasRows)
                    {
                        log.Debug("ho righe " + DR.RecordsAffected.ToString());
                        while (DR.Read())
                        {
                            // POPOLO L'ARRAYLIST DELLE STRADE
                            OggettoTipoStrada TipoStrada = new OggettoTipoStrada();
                            TipoStrada.CodiceEnte = DR["cod_ente"].ToString();
                            TipoStrada.CodTipoStrada = int.Parse(DR["id_toponimo"].ToString());
                            TipoStrada.TipoStrada = DR["toponimo"].ToString();

                            ArrListTipoStrada.Add(TipoStrada);
                        }
                    }
                    DR.Close();
                    ctx.Dispose();
                }
                OggettoTipoStrada[] ArrayTipiStrade = (OggettoTipoStrada[])ArrListTipoStrada.ToArray(typeof(OggettoTipoStrada));
                log.Debug("restituisco righe " + ArrListTipoStrada.Count.ToString());
                return ArrayTipiStrade;
            }
            catch (Exception Ex)
            {
                log.Debug("Errore nell'esecuzione di GetArrayTipoStrada :: ", Ex);
                return null;
            }
        }

        //*** 20100311 - Sostituito query secca con interrogazione su vista ***//
        //		public OggettoStrada[] GetArrayOggettoStrade(OggettoStrada objStrada)
        //		{
        //			try 
        //			{
        //				ArrayList ArrListStrada = new ArrayList();
        //			
        //				DBManager objDB = new Utility.DBManager(this.connectionString);
        //				log.Debug("GetArrayOggettoStrade stringa di connessione:: "+this.connectionString);
        //			
        //				SqlCommand Cmd = new SqlCommand();
        //
        //				Cmd.CommandText = "SELECT T_STRADE.id_via, T_STRADE.cod_ente, T_STRADE.descrizione_via, T_STRADE.id_tipo_via, T_TIPO_VIE.toponimo, T_STRADE.id_frazione, T_FRAZIONI.frazione ";
        //				Cmd.CommandText += " FROM T_STRADE INNER JOIN T_FRAZIONI ON T_STRADE.id_frazione = T_FRAZIONI.id_frazione INNER JOIN ";
        //                Cmd.CommandText += " T_TIPO_VIE ON T_STRADE.id_tipo_via = T_TIPO_VIE.id_toponimo WHERE 1 = 1 ";
        //				
        //				if (objStrada.CodiceStrada.CompareTo(-1) != 0){
        //					Cmd.CommandText += " AND T_STRADE.ID_VIA = @CodiceVia";
        //					Cmd.Parameters.Add("@CodiceVia", SqlDbType.Int).Value = objStrada.CodiceStrada;
        //				}
        //
        //				if (objStrada.CodiceEnte.CompareTo("") != 0){
        //					Cmd.CommandText += " AND T_STRADE.COD_ENTE LIKE @CodEnte";
        //					Cmd.Parameters.Add("@CodEnte", SqlDbType.NVarChar).Value = "%" + objStrada.CodiceEnte;
        //				}
        //
        //				if (objStrada.DenominazioneStrada.CompareTo("") != 0){
        //					Cmd.CommandText += " AND T_STRADE.DESCRIZIONE_VIA LIKE @DescrizioneVia";
        //					Cmd.Parameters.Add("@DescrizioneVia", SqlDbType.NVarChar).Value = objStrada.DenominazioneStrada + "%";
        //				}
        //				
        //				if (objStrada.CodTipoStrada.CompareTo(-1) != 0){
        //					Cmd.CommandText += " AND T_STRADE.ID_TIPO_VIA = @CodTipoVia";
        //					Cmd.Parameters.Add("@CodTipoVia", SqlDbType.Int).Value = objStrada.CodTipoStrada;
        //				}
        //
        //				if (objStrada.CodFrazione.CompareTo(-1)!= 0){
        //					Cmd.CommandText += " AND T_STRADE.ID_FRAZIONE = @CodFrazione";
        //					Cmd.Parameters.Add("@CodFrazione", SqlDbType.Int).Value = objStrada.CodFrazione;
        //				}
        //
        //				SqlDataReader DR = objDB.GetDataReader(Cmd);
        //				if (DR.HasRows)
        //				{
        //					while (DR.Read())
        //					{
        //						// POPOLO L'ARRAYLIST DELLE STRADE
        //						OggettoStrada Strada = new OggettoStrada();
        //						Strada.CodFrazione = int.Parse(DR["id_frazione"].ToString());
        //						Strada.CodiceEnte = DR["cod_ente"].ToString();
        //						Strada.CodiceStrada = int.Parse(DR["id_via"].ToString());
        //						Strada.CodTipoStrada = int.Parse(DR["id_tipo_via"].ToString());
        //						Strada.DenominazioneStrada = DR["descrizione_via"].ToString();
        //						Strada.Frazione = DR["frazione"].ToString();
        //						Strada.TipoStrada = DR["toponimo"].ToString();
        //					
        //						ArrListStrada.Add(Strada);
        //					}
        //				}
        //					OggettoStrada[] ArrayStrade = (OggettoStrada[])ArrListStrada.ToArray(typeof(OggettoStrada));
        //					return ArrayStrade;
        //			}
        //			catch (Exception Ex)
        //			{	
        //				log.Error("Errore nell'esecuzione di GetArrayOggettoStrade :: ", Ex);
        //				return null;
        //			}
        //			
        //		}
        //
        //*** 20100311 ***//
        public OggettoStrada[] GetArrayOggettoStrade(string TypeDB, string sConnection, OggettoStrada objStrada)
        {
            try
            {
                ArrayList ArrListStrada = new ArrayList();

                DBModel ctx = new DBModel(TypeDB, sConnection);
                log.Debug("StadeManager::GetArrayOggettoStrade::connessione a::" + sConnection);

                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = new SqlConnection(sConnection);

                //Cmd.CommandText = "SELECT *";
                //Cmd.CommandText += " FROM V_STRADE";
                //Cmd.CommandText += " WHERE 1 = 1 ";
                //Cmd.CommandText += " AND (@CodiceVia<=0 OR V_STRADE.ID_VIA = @CodiceVia)";
                //Cmd.CommandText += " AND (@CodiceViaDemografico<=0 OR V_STRADE.IDVIADEMOGRAFICO = @CodiceViaDemografico)";
                //Cmd.CommandText += " AND (@CodEnte='' OR V_STRADE.COD_ENTE LIKE @CodEnte)";
                //Cmd.CommandText += " AND (@DescrizioneVia='' OR V_STRADE.DESCRIZIONE_VIA LIKE @DescrizioneVia)";
                //Cmd.CommandText += " AND (@Via='' OR V_STRADE.VIA LIKE @Via)";
                //Cmd.CommandText += " AND (@CodTipoVia<=0 OR V_STRADE.ID_TIPO_VIA = @CodTipoVia)";
                //Cmd.CommandText += " AND (@CodFrazione<=0 OR V_STRADE.ID_FRAZIONE = @CodFrazione)";
                //Cmd.CommandText += "ORDER BY COD_ENTE, TOPONIMO, DESCRIZIONE_VIA, FRAZIONE";
                //log.Debug("GetArrayOggettoStrade::SQL::" + Cmd.CommandText +"::connessione"+ Cmd.Connection.ConnectionString);
                string sSQL = ctx.GetSQL(DBModel.TypeQuery.StoredProcedure,"prc_GetStrade", "CodiceVia", "CodiceViaDemografico", "CodEnte", "DescrizioneVia", "Via", "CodTipoVia", "CodFrazione");
                SqlDataReader DR = (SqlDataReader)ctx.GetDataReader(sSQL, ctx.GetParam("CodiceVia", objStrada.CodiceStrada)
                    , ctx.GetParam("CodiceViaDemografico", objStrada.CodiceStradaDemografico)
                    , ctx.GetParam("CodEnte", objStrada.CodiceEnte)
                    , ctx.GetParam("DescrizioneVia", objStrada.DenominazioneStrada.Replace("*", "%"))
                    , ctx.GetParam("Via", objStrada.Strada.Replace("*", "%"))
                    , ctx.GetParam("CodTipoVia", objStrada.CodTipoStrada)
                    , ctx.GetParam("CodFrazione", objStrada.CodFrazione)
                );
                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        // POPOLO L'ARRAYLIST DELLE STRADE
                        OggettoStrada Strada = new OggettoStrada();
                        Strada.CodFrazione = int.Parse(DR["id_frazione"].ToString());
                        Strada.CodiceEnte = DR["cod_ente"].ToString();
                        Strada.CodiceStrada = int.Parse(DR["id_via"].ToString());
                        if (DR["IDVIADEMOGRAFICO"] != System.DBNull.Value)
                        {
                            Strada.CodiceStradaDemografico = int.Parse(DR["IDVIADEMOGRAFICO"].ToString());
                        }
                        Strada.CodTipoStrada = int.Parse(DR["id_tipo_via"].ToString());
                        Strada.DenominazioneStrada = DR["descrizione_via"].ToString();
                        Strada.Strada = DR["via"].ToString();
                        Strada.Frazione = DR["frazione"].ToString();
                        Strada.TipoStrada = DR["toponimo"].ToString();

                        ArrListStrada.Add(Strada);
                    }
                }
                OggettoStrada[] ArrayStrade = (OggettoStrada[])ArrListStrada.ToArray(typeof(OggettoStrada));
                return ArrayStrade;
            }
            catch (Exception Ex)
            {
                log.Error("Errore nell'esecuzione di GetArrayOggettoStrade :: ", Ex);
                return null;
            }
        }
    }
}
