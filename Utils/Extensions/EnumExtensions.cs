
namespace Tools.Extensions
{
    public static class EnumExtensions
    {
        public static Dictionary<string, int> ToDictionary<T>(this T e) where T : Enum
        {
            // Obtener todos los valores del enum
            var enumValues = Enum.GetValues(typeof(T)).Cast<T>();

            // Crear un diccionario clave-valor
            var enumDictionary = enumValues.ToDictionary(
                value => value.ToString().ToLower(), // Clave: nombre del valor del enum
                value => Convert.ToInt32(value)        // Valor: número equivalente
            );
            return enumDictionary;
        }
    }
}
