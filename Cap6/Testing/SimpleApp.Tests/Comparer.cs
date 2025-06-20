using System;
using System.Collections.Generic;

namespace SimpleApp.Tests
{
    // Classe base Comparer que fornece um método estático para criar instâncias de Comparer<U?>
    public class Comparer
    {
        // Método estático que cria uma instância de Comparer<U?> com base em uma função de comparação fornecida
        public static Comparer<U?> Get<U>(Func<U?, U?, bool> func)
        {
            // Retorna uma nova instância de Comparer<U?> usando a função de comparação fornecida
            return new Comparer<U?>(func);
        }
    }

    // Classe genérica Comparer<T> que implementa IEqualityComparer<T> para comparação personalizada
    public class Comparer<T> : Comparer, IEqualityComparer<T>
    {
        // Campo privado que armazena a função de comparação personalizada
        private Func<T?, T?, bool> comparisonFunction;

        // Construtor que inicializa a função de comparação personalizada
        public Comparer(Func<T?, T?, bool> func)
        {
            comparisonFunction = func;
        }

        // Método Equals que usa a função de comparação personalizada para determinar igualdade
        public bool Equals(T? x, T? y)
        {
            return comparisonFunction(x, y);
        }

        // Método GetHashCode que retorna o código hash de um objeto ou 0 se o objeto for nulo
        public int GetHashCode(T obj)
        {
            return obj?.GetHashCode() ?? 0;
        }
    }
}