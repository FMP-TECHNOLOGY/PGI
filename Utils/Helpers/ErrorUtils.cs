using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utils.Helpers
{
    public enum Error
    {
        NOT_FOUND,
        FIELD_REQUIRED,
        UPDATE_ENTITY_NOT_FOUND,
        BAD_REQUEST,
        COMPANY_ALREADY_DEFINED
    }

    public static class ErrorUtils
    {
        public static string? Get(Error error, string? fieldName = null)
        {
            return Errors(fieldName)[error];
            /*return error switch
            {
                Error.NOT_FOUND => "No se encontraron resultados",
                Error.FIELD_REQUIRED => $"El campo {fieldName} es requerido",
                Error.BAD_REQUEST => "Algunos de los campos no han sido validados correctamente",
                _ => "Se produjo un error desconocido"
            };*/
        }

        private static IDictionary<Error, string?> Errors(string? fieldName = null)
        {
            return new Dictionary<Error, string?>()
            {
                {Error.NOT_FOUND, "No se encontraron resultados"},
                {Error.FIELD_REQUIRED, $"El campo {fieldName} es requerido"},
                {Error.UPDATE_ENTITY_NOT_FOUND, "El recurso a actualizar no existe"},
                {Error.BAD_REQUEST, "Algunos de los campos no han sido validados correctamente"},
                {Error.COMPANY_ALREADY_DEFINED, "Ya se definio una compañia"},
            };
        }
    }
}
