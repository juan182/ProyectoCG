using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GuardarDatosJugador : MonoBehaviour
{

    string rutaArchivo;
    [SerializeField]
    private GameObject PanelDatosCargados;

    [SerializeField]
    private Transform contenidoTexto;

    [SerializeField]
    private TextMeshProUGUI textoContenido;

    Historial historial = new Historial();

    private void Awake()
    {
        rutaArchivo = Path.Combine(Application.persistentDataPath, "datosJugador.json");

        if (File.Exists(rutaArchivo))
        {
            string jsonArchive = File.ReadAllText(rutaArchivo);
            historial = JsonUtility.FromJson<Historial>(jsonArchive);
        }

        //PanelDatosCargados.SetActive(false);
    }


    public void GuardarDatos()
    {
        DatosJugador datos = new DatosJugador();
        Debug.Log($"Nombre cargado: '{datos.nombreJugador}'");

        datos.nombreJugador = GameManager.Instance.nombreJugador;
        datos.carretilla = GameManager.Instance.carretilla;
        datos.goldCoin = GameManager.Instance.gold;
        datos.silverCoin = GameManager.Instance.silver;
        datos.copperCoin = GameManager.Instance.copper;
        datos.shark = GameManager.Instance.shark;
        datos.tiempoTotal = GameManager.Instance.ObtenerTiempoTotal();
        datos.fecha = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        historial.partidas.Add(datos);
        string json = JsonUtility.ToJson(historial, true);
        File.WriteAllText(rutaArchivo, json);

        Debug.Log("Datos guardados en: " + rutaArchivo);
    }

    
    public void CargarDatos()
    {
        PanelDatosCargados.SetActive(true);
        if (File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);

            historial = JsonUtility.FromJson<Historial>(json);

            foreach(Transform child in contenidoTexto)
            {
                Destroy(child.gameObject);
            }

            foreach (DatosJugador datos in historial.partidas)
            {
                
               

                TextMeshProUGUI nuevoItem = Instantiate(textoContenido, contenidoTexto);
                if (nuevoItem != null)
                {
                    nuevoItem.text = $"<b>Nombre:</b> {datos.nombreJugador}\n" +
                                $"<b>Carretilla:</b> {datos.carretilla}\n" +
                                $"<b>Shark:</b> {datos.shark}\n" +
                                $"<b>Monedas:</b>\n" +
                                $" - Oro: {datos.goldCoin}\n" +
                                $" - Plata: {datos.silverCoin}\n" +
                                $" - Cobre: {datos.copperCoin}\n" +
                                $"<b>Tiempo total:</b> {FormatearTiempo(datos.tiempoTotal)}\n\n"+
                                $"<b>Fecha de creacion:</b>{datos.fecha}\n";
                }
                else
                {
                    Debug.LogError("No se encontró TextMeshProUGUI en el prefab instanciado");
                }
                
            }

            


          //  contenidoTexto.text = textoFinal;
        }
    }

    string FormatearTiempo(float segundos)
    {
        int horas = (int)(segundos / 3600);
        int minutos = (int)((segundos % 3600) / 60);
        int seg = (int)(segundos % 60);
        return $"{horas:00}:{minutos:00}:{seg:00}";
    }

}
