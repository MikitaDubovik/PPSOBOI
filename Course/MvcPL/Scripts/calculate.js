$('#CalculateButton').bind("click", function () {

    var countries = $("#DdlCountries").val();

    var sex = $("#DdlSex").val();

    var age = parseInt($("#DdlAge").val());

    var languages = $("#DdlLanguage").val();

    var totalPrice = 0;

    if (countries.length !== 0)
        totalPrice += countries.length * 30;
    else
        totalPrice += 120;

    if (sex.length !== 0)
        totalPrice += sex.length * 30;
    else
        totalPrice += 90;

    if (languages.length !== 0)
        totalPrice += languages.length * 30;
    else
        totalPrice += 150;

    totalPrice +=  age * 30;

    $("#endPrice").text(totalPrice);

    document.getElementById("PayButton").style.visibility = "visible";
    document.getElementById("beginPrice").style.visibility = "visible";
    document.getElementById("endPrice").style.visibility = "visible";

    $.post({
        type: "POST",
        url: "PostPrice",
        data: {
            Price: $("#endPrice").text(),
            Language: languages,
            Age: age,
            Countries: countries,
            Sex: sex
        }
    });
});

$(document).ready(function () {
    document.getElementById("PayButton").style.visibility = "hidden";
    document.getElementById("beginPrice").style.visibility = "hidden";
    document.getElementById("endPrice").style.visibility = "hidden";
});