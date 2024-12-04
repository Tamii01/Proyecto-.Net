using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class EncryptHelper
    {

        private static string hash
        {
            get
            {
                return "b14ca5898a4e4133bbce2ea2315a1916";
            }
        }

        // Paso 1: Crear una funcion para encriptar la clave
        public static string Encriptar(string clave)
        {

            //Paso 2: Crear una instancia del algoritmo AES usando using para liberar recursos en memoria una vez que finaliza
            using (var aes = Aes.Create())
            {
                //Paso 2: establecemos la clave de la encriptacion
                aes.Key = Encoding.UTF8.GetBytes(hash);
                //Paso 2: establecemos el vector de inicializacion
                aes.IV = new byte[16];

                //Paso 3: Creamos el encriptador con la clave y el IV establecidos
                var encriptador = aes.CreateEncryptor(aes.Key, aes.IV);

                //Paso 4: Creamos un MemoruStream para almacenar los datos encriptados
                using (var ms = new MemoryStream())
                {
                    //Paso 5: Creamos un CryptoStream que encripta los datos que se escriben en el CryptoStream

                    using (var cryptoStream = new CryptoStream(ms, encriptador, CryptoStreamMode.Write))
                    {
                        //Paso 6: Creamos un StreamWriter para escribir la cadena clave en el StreamWriter, que la pasa al CryptoStream y la termina encriptado
                        using (var sw = new StreamWriter(cryptoStream))
                        {
                            //La termina encriptado
                            sw.Write(clave);
                        }
                        //Convierte todo el contenido de MemoryStream que son los datos encriptados a un arreglo de bytes y luego lo retornamos como una conversion a stream
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public static string Desencriptar(string clave)
        {
            byte[] claveBytes = Convert.FromBase64String(clave);
            using (var aes = Aes.Create())
            {

                aes.Key = Encoding.UTF8.GetBytes(hash);
                aes.IV = new byte[16];

                var desencriptador = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream(claveBytes))
                {
                    using (var cryptoStream = new CryptoStream(ms, desencriptador, CryptoStreamMode.Read))
                    {
                        using (var sw = new StreamReader(cryptoStream))
                        {
                            return sw.ReadToEnd();
                        }
                    }
                }

            }
        }
    }
}
