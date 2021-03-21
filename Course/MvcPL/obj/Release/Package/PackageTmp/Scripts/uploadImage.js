$('.new_Btn').bind("click", function () {
    $('#ImageFile').click();
});

$(document).ready(function () {
    $("#ImageFile").change(function () {
        var File = this.files;
        if (File && File[0]) {
            ReadImage(File[0]);
        }
    });
});


var ReadImage = function (file) {

    var reader = new FileReader;
    var image = new Image;

    reader.readAsDataURL(file);
    reader.onload = function (_file) {

        image.src = _file.target.result;
        image.onload = function () {

            $("#targetImg").attr('src', _file.target.result);
            $("#imgPreview").show();
            $("#UploadButton").show();
            $("#ContinueButton").show();
        };
    };
};

$('#UploadButton').bind("click", function () {

    var country = $("#DdlCountries").val();

    var sex = $("#DdlSex").val();

    var age = parseInt($("#DdlAge").val());

    var languages = $("#DdlLanguage").val();

    $.post({
        type: "POST",
        url: "GetProfileInfo",
        data: {
            Language: languages,
            Age: age,
            Country: country,
            Sex: sex
        }
    });
});