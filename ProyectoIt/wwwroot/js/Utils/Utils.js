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
    var nombreEQ = nombre + "=";
    var cookies = document.cookie.split(";");
    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i];
        if (cookie.indexOf(nombreEQ) == 0 || cookie.indexOf(nombreEQ) == 1) {

            return cookie.substring(cookie.indexOf(nombreEQ) == 1 ? nombreEQ.length +1 : nombreEQ.length, cookie.length);
        }
    }

    return null;
}