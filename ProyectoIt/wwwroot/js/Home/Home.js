$(function () {
    if ($("#Token").val() != "") {
        setCookie("Token", $("#Token").val(), 1);
        $("#Token").remove();
    }
})