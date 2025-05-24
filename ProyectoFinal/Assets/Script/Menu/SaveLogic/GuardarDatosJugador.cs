using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

/// <summary>
/// Clase encargada de guardar y cargar los datos del jugador en un archivo JSON.
/// Maneja la serialización y deserialización de un historial de partidas, 
/// mostrando los datos guardados en la UI.
/// </summary>
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


    /// <summary>
    /// Inicializa la ruta del archivo JSON y carga los datos guardados si 
    /// existen.
    /// </summary>
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


    /// <summary>
    /// Crea un nuevo registro con los datos actuales del jugador y los 
    /// guarda en un archivo JSON.
    /// </summary>
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


    /// <summary>
    /// Carga los datos guardados desde el archivo JSON y los muestra en 
    /// la interfaz de usuario.
    /// </summary>
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


    /// <summary>
    /// Convierte un tiempo en segundos a un formato de texto HH:mm:ss.
    /// </summary>
    /// <param name="segundos">Tiempo en segundos.</param>
    string FormatearTiempo(float segundos)
    {
        int horas = (int)(segundos / 3600);
        int minutos = (int)((segundos % 3600) / 60);
        int seg = (int)(segundos % 60);
        return $"{horas:00}:{minutos:00}:{seg:00}";
    }

}
