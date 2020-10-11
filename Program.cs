using System;
using System.Collections.Generic;

namespace AppLaberinto
{
    class Program
    {
        static void Main(string[] args)
        {
            string [,] arreglo={
                {"*"," ","*"," ","i","*"," ","*"},
                {"*","*","*"," ","*","*","*","*"},
                {"*"," "," "," ","*"," "," "," "},
                {"*"," ","*","*","*"," ","*"," "},
                {"*"," "," "," "," "," ","*"," "},
                {"*","*","*","*","*","*"," "," "},
                {"*","*","*","*","o"," "," "," "},
                {"*","*","*","*","*","*","*"," "}
            };
            
            var listacamino=new List<Posicion>();
            var listaparedes=new List<Posicion>();

            int columnainicio=ObtenerInicio(arreglo);
            Posicion posicion_inicio=new Posicion(){x=0,y=columnainicio};
            Console.WriteLine(posicion_inicio.x+","+posicion_inicio.y);

            bool exito=false;
            int intentos=0;

            while(!exito && intentos<30){
            Posicion posicion=NuevaPosicion(ref posicion_inicio,ref arreglo,ref listaparedes,ref exito, listacamino);
            listacamino.Add(posicion);

            posicion_inicio.x=posicion.x;
            posicion_inicio.y=posicion.y;

            // Console.WriteLine(posicion_inicio.x+","+posicion_inicio.y);
            intentos++;
            }

            foreach(var p in listacamino){
                Console.WriteLine(p.x+"-"+p.y);
            }
        }
        
        static Posicion NuevaPosicion(ref Posicion inicio, ref string[,] arreglo, ref List<Posicion> listaparedes, ref bool exito, List<Posicion> listacamino)
        {
            int m=arreglo.GetLength(0);
            int n=arreglo.GetLength(1);

            Posicion oposicion=new Posicion();

            int izquierda=inicio.y-1;
            int abajo=inicio.x+1;
            int derecha=inicio.y+1;
            int arriba=inicio.x-1;

            // Console.WriteLine("IZQ:"+izquierda);
            // Console.WriteLine("ABA:"+abajo);
            // Console.WriteLine("DER:"+derecha);
            // Console.WriteLine("ARR:"+arriba);

            if(izquierda>=0 && !HaCaminado(listacamino,new Posicion(){x=inicio.x,y=izquierda})) // izquierda, X fijo
            {
                if(arreglo[inicio.x,izquierda]==" ") // espacio 
                {
                    Console.WriteLine("libre");
                    
                    oposicion.x=inicio.x;
                    oposicion.y=izquierda;
                    return oposicion;
                }
                else if(arreglo[inicio.x,izquierda]=="*")
                {
                    listaparedes.Add(new Posicion(){x=inicio.x,y=izquierda});
                    Console.WriteLine("pared izquierda");
                }
                else if(arreglo[inicio.x,izquierda]=="o"){
                    Console.WriteLine("salidaencontrada");
                    exito=true;

                    oposicion.x=inicio.x;
                    oposicion.y=izquierda;
                    return oposicion;
                }
            }
            if(abajo<=m-1 && !HaCaminado(listacamino,new Posicion(){x=abajo,y=inicio.y})) // abajo - Y fijo
            {
                if(arreglo[abajo,inicio.y]==" ") // espacio
                {
                    Console.WriteLine("libre");

                    oposicion.x=abajo;
                    oposicion.y=inicio.y;
                    return oposicion;
                }
                else if(arreglo[inicio.x,abajo]=="*")
                {
                    listaparedes.Add(new Posicion(){x=inicio.x,y=abajo});
                    Console.WriteLine("pared abajo");
                }
                else if(arreglo[inicio.x,abajo]=="o"){
                    exito=true;

                    oposicion.x=abajo;
                    oposicion.y=inicio.y;
                    return oposicion;
                }
            }
            if(derecha<=n-1&& !HaCaminado(listacamino,new Posicion(){x=inicio.x,y=derecha})) // derecha, X fijo
            {
                if(arreglo[inicio.x,derecha]==" "){
                    Console.WriteLine("libre");

                    oposicion.x=inicio.x;
                    oposicion.y=derecha;
                    return oposicion;
                }
                else if(arreglo[inicio.x,derecha]=="*"){
                    listaparedes.Add(new Posicion(){x=inicio.x,y=derecha});
                    Console.WriteLine("pared_derecha");
                }
                else if(arreglo[inicio.x,derecha]=="o"){
                    exito=true;

                    oposicion.x=inicio.x;
                    oposicion.y=derecha;
                    return oposicion;
                }
            }
            if(arriba>=0 && !HaCaminado(listacamino,new Posicion(){x=arriba,y=inicio.y})) // arriba, Y fijo
            {
                if(arreglo[arriba,inicio.y]==" "){
                    Console.WriteLine("libre");

                    oposicion.x=arriba;
                    oposicion.y=inicio.y;
                    return oposicion;
                }    
                else if(arreglo[arriba,inicio.y]=="*"){
                    listaparedes.Add(new Posicion(){x=arriba,y=inicio.y});
                    Console.WriteLine("pared arriba");
                }
                else if(arreglo[arriba,inicio.y]=="o"){
                    exito=true;

                    oposicion.x=arriba;
                    oposicion.y=inicio.y;
                    return oposicion;
                }
            }
            
            return oposicion;
        }
        static bool HaCaminado(List<Posicion> listacamino, Posicion punto){
            bool r=false;
            foreach(Posicion p in listacamino)
            {
                if(p.x==punto.x && p.y==punto.y)
                {
                    r=true;
                    break;
                }
            }
            return r;
        }
        public static int ObtenerInicio(string [,] arreglo)
        {
            int columnainicio=0;
            int longitudx=arreglo.GetLength(0);

            for(int i=0;i<longitudx-1;i++){
                if(arreglo[0,i]=="i"){
                    columnainicio=i;
                    break;
                }

            }
            return columnainicio;
        }
    }
    class Posicion{
        
        
        public int x { get; set; }
        public int y { get; set; }
    }
}
