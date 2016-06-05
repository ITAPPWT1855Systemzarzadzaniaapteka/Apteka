//#region Upload JS
function uploadCSV() {
    var file = document.getElementById("tariff");
    var formData = new FormData();
    if (!file.files.length) {
        return;
    }
    formData.append('Cennik', file.files[0]);
    $(".icon-uploading").html("<i class=\"fa fa-spinner\"></i>")
    $(".text-uploading").html("Trwa wgrywanie pliku")
    $(".text-parsing").html("")
    $(".icon-parsing").html("")
    $("#tariff").hide();
    xhr('/Order/UploadCSV', formData, function (fName) {
        $(".icon-uploading").html("<i class=\"fa fa-check\"></i>")
        $(".text-uploading").html("Plik wgrany na serwer")
        $(".icon-parsing").html("<i class=\"fa fa-spinner\"></i>")
        $(".text-parsing").html("Trwa parsowanie pliku. To może trochę potrwać.")
        $.get("/Order/transformExcel").done(function (data) {
            $(".icon-parsing").html("<i class=\"fa fa-check\"></i>")
            $(".text-parsing").html("Plik pomyślnie sparsowany. Utworzono " + data.count + " pozycji.");
            setTimeout(function () {
                location.reload();
            }, 1000);
        }).fail(function () {
            $(".icon-parsing").html("<i class=\"fa fa-times\"></i>")
            $(".text-parsing").html("Błąd parsowania. Sprawdź czy plik jest poprawny.");
        });
    }, function () {
        $(".icon-uploading").html("<i class=\"fa fa-times\"></i>")
        $(".text-uploading").html("Błąd w trakcie wgrywania pliku")
    });
}

function xhr(url, data, callback, onError) {
    var request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (request.readyState == 4 && request.status == 200) {
            callback(request.responseText);
        } else {
            console.log('problem');
            onError();
        }
    };
    request.open('POST', url);
    request.send(data);
}

function showModal() {
    console.log('show modal');
    $('#myModal').modal('show');
}
//#endregion Upload JS

var descRgx = /^(.+):(.+)\((.+),(.+),(.+)\) - ([0-9,]+)/;
var secRgx = /^.+: (.*\))/;
var sc;
$(window).on('hashchange', function () {
    $(".detail-row:not(.hidden)").addClass("hidden");
    $(".detail-row-" + window.location.hash.slice(1)).removeClass("hidden");
});

$(window).ready(function () {
    $(".detail-row:not(.hidden)").addClass("hidden");
    $(".add-btn").click(function (ev) {
        var row = $(this).parents(".detail-row");
        var itemID = row.attr("data-id");
        var toOrder = row.find(".offer-desc").text();
        var result = descRgx.exec(toOrder);
        var lek = {
            hurtownia: result[1],
            lek: result[2],
            postac: result[3],
            dawka: result[4],
            opakowanie: result[5],
            cena: result[6],
            opis: secRgx.exec(toOrder)[1],
            ilosc: $(".need-" + itemID).text()
        };
        window.location.hash = "";
        if (!_.find(sc.hurtownias, { name: lek.hurtownia })) {
            sc.hurtownias.push({ name: lek.hurtownia, produkty: [] });
        }
        var hurtownia = _.find(sc.hurtownias, { name: lek.hurtownia });
        hurtownia.produkty.push(lek);
        $(".status-cell-" + itemID).html("<i class=\"fa fa-check-circle-o\"></i>")
        sc.$apply();
    });

    $("#part-2").hide();
    $("#frw-button").click(function () {
        $("#part-1").hide();
        $("#part-2").show();
    });

    $("#back-button").click(function () {
        $("#part-1").show();
        $("#part-2").hide();
    });
})

var Apteka = angular.module('Apteka', []);
Apteka.controller('order', function OrderController($scope) {
    $scope.hurtownias = [];
    sc = $scope;

    $scope.generateProForm = function (hurtownia) {
        var suma = 0;
        _.each(hurtownia.produkty, function (i) {
            suma = suma + (+i.cena.replace(',', '.'));
        });
        hurtownia.suma = ("" + suma).replace(".", ",");
        $.post("/Order/Proform", hurtownia, function (data) {
            var win=window.open('about:blank');
            with(win.document)
            {
                open();
                write(data);
                close();
            }
        });
    }
});