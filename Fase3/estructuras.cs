using Estructuras;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Gtk;
using System.IO;

namespace Estructuras
{
    public static class Datos
    {
        public static ListaVehiculos vehiculosLista = new ListaVehiculos();
        public static ArbolBinarioServicio serviciosArbol = new ArbolBinarioServicio();
        public static AVLRepuesto repuestosArbol = new AVLRepuesto();
        public static ListaGrafos grafoLista = new ListaGrafos();
        public static Blockchain blockchain = new Blockchain();

        // public static ArbolBFactura facturasArbol = new ArbolBFactura();
        public static ListaLogin loginLista = new ListaLogin();



        public static int idUsuarioLogin = 0;

        // Control de acceso de login
        public static string nombreUsuarioLogin = "";
        public static string entradaUsuarioLogin = "";
        public static string salidaUsuarioLogin = "";

        //Metodo para mostrar un mensaje en una ventana emergente
        public static void msg(Window parent, string msg)
        {
            using (var md = new MessageDialog(
                parent,
                DialogFlags.Modal,
                MessageType.Info,
                ButtonsType.Ok,
                msg))
            {
                md.Run();
            }
        }

    }
}