const setCookie = (nombre, valor, dias) => {
    var expiracion = "";
    if (dias) {
        var fecha = new Date();
        fecha.setTime(fecha.getTime() + (dias * 24 * 60 * 60 * 1000));
        expiracion = "; expires=" + fecha.toUTCString(); 
    }
    document.cookie = nombre + "=" + (valor || "") + expiracion + "; path=/";
}


const getCookie = (nombre) => {
    var nombre = nombre + "=";
    var cookies = document.cookie.split(";");
    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i];
        while (cookie.charAt(0) == ' ') {
            cookie = cookie.substring(1, cookie.length);
            if (cookie.indexOf(nombre) == 0) {
                return cookie.substring(nombre.length, cookie.length);
            }
        }
    }
}