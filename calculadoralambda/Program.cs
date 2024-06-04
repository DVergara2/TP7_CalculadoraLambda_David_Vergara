using System;
using System.Collections.Generic;

class Calculadora
{
    // Lista para almacenar los resultados de las operaciones
    static List<string> resultados = new List<string>();

    static void Main(string[] args)
    {
        // Preguntar si se desea trabajar con números enteros o decimales
        Console.WriteLine("¿Desea realizar operaciones con números enteros (1) o decimales (2)?");
        int tipoNumero = ObtenerOpcion(new[] { 1, 2 });

        // Bucle principal del programa
        while (true)
        {
            // Mostrar el menú de operaciones
            Console.WriteLine("..........-CALCULADORA- -•*.•.......");
            Console.WriteLine("\nMenú de operaciones:");
            Console.WriteLine("1. Sumar");
            Console.WriteLine("2. Restar");
            Console.WriteLine("3. Multiplicar");
            Console.WriteLine("4. Dividir");
            Console.WriteLine("5. Ver resultados anteriores");
            Console.WriteLine("0. Salir");

            // Leer la opción seleccionada
            int opcion = ObtenerOpcion(new[] { 0, 1, 2, 3, 4, 5 });

            // Realizar la operación seleccionada
            switch (opcion)
            {
                case 1:
                    RealizarOperacion(tipoNumero, (x, y) => x + y, "Sumar", "+");
                    break;
                case 2:
                    RealizarOperacion(tipoNumero, (x, y) => x - y, "Restar", "-");
                    break;
                case 3:
                    RealizarOperacion(tipoNumero, (x, y) => x * y, "Multiplicar", "*");
                    break;
                case 4:
                    RealizarOperacion(tipoNumero, (x, y) => x / y, "Dividir", "/");
                    break;
                case 5:
                    MostrarResultadosAnteriores();
                    break;
                case 0:
                    Console.WriteLine("¡Gracias por usar la calculadora!");
                    return;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }

            // Limpiar la pantalla antes de volver al menú principal
            Console.WriteLine("\nPulsa cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    // Método para obtener una opción válida del usuario
    static int ObtenerOpcion(int[] opcionesValidas)
    {
        int opcion;
        // Leer y validar la opción ingresada
        while (!int.TryParse(Console.ReadLine(), out opcion) || Array.IndexOf(opcionesValidas, opcion) == -1)
        {
            Console.WriteLine("Opción no válida. Intente de nuevo.");
        }
        return opcion;
    }

    // Método para realizar la operación seleccionada
    static void RealizarOperacion(int tipoNumero, Func<double, double, double> operacion, string operacionNombre, string operador)
    {
        Console.Clear(); // Limpiar la pantalla
        Console.WriteLine($"Ingrese el primer número ({(tipoNumero == 1 ? "entero" : "decimal")}):");
        double num1 = LeerNumero(tipoNumero);

        // Verificar si la entrada del primer número es válida
        if (double.IsNaN(num1))
        {
            GuardarOperacionInvalida("Primer número inválido", operacionNombre);
            return;
        }

        Console.WriteLine($"Ingrese el segundo número ({(tipoNumero == 1 ? "entero" : "decimal")}):");
        double num2 = LeerNumero(tipoNumero);

        // Verificar si la entrada del segundo número es válida
        if (double.IsNaN(num2))
        {
            GuardarOperacionInvalida("Segundo número inválido", operacionNombre);
            return;
        }

        // Manejar el caso de división por cero
        if (operacionNombre == "Dividir" && num2 == 0)
        {
            Console.WriteLine("¡Error! No se puede dividir por cero.");
            GuardarOperacionInvalida("División por cero", operacionNombre);
            return;
        }

        // Realizar la operación y guardar el resultado
        double resultado = operacion(num1, num2);
        GuardarOperacion(num1, num2, resultado, operador, tipoNumero); // Guardar la operación completa en la lista
        Console.WriteLine($"El resultado de {operacionNombre.ToLower()} es: {num1} {operador} {num2} = {(tipoNumero == 1 ? (int)resultado : resultado)}");
    }

    // Método para leer un número del usuario y validar la entrada
    static double LeerNumero(int tipoNumero)
    {
        double numero;
        // Intentar leer y convertir la entrada a un número
        if (!double.TryParse(Console.ReadLine(), out numero))
        {
            return double.NaN; // Valor especial para indicar entrada no válida
        }
        return tipoNumero == 1 ? (int)numero : numero;
    }

    // Método para guardar una operación exitosa en la lista de resultados
    static void GuardarOperacion(double num1, double num2, double resultado, string operador, int tipoNumero)
    {
        string operacion = $"{num1} {operador} {num2} = {(tipoNumero == 1 ? (int)resultado : resultado)}";
        resultados.Add(operacion);
    }

    // Método para guardar un mensaje de error en la lista de resultados
    static void GuardarOperacionInvalida(string mensajeError, string operacionNombre)
    {
        string operacion = $"Error en operación {operacionNombre.ToLower()}: {mensajeError}";
        resultados.Add(operacion);
        Console.WriteLine(operacion);
    }

    // Método para mostrar los resultados anteriores
    static void MostrarResultadosAnteriores()
    {
        Console.Clear(); // Limpiar la pantalla
        Console.WriteLine("Resultados anteriores:");
        if (resultados.Count == 0)
        {
            Console.WriteLine("No hay resultados anteriores.");
        }
        else
        {
            // Imprimir cada operación en la lista de resultados
            for (int i = 0; i < resultados.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {resultados[i]}");
            }
        }
    }
}
