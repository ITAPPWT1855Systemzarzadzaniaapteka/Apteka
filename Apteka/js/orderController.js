//#region Upload JS
function uploadCSV() {
    var file = document.getElementById("tariff");
    var formData = new FormData();

    formData.append('Cennik', file.files[0]);
    xhr('/Order/UploadCSV', formData, function (fName) {
        console.log('Posted', fName);
        $('#alert').toggle();
    }, function () {
        //$('#alert-danger').toggle();
        console.log('problem')
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
        console.log("Duia", $(".need-" + itemID).text());
        var lek = {
            hurtownia: result[1],
            lek: result[2],
            postac: result[3],
            dawka: result[4],
            opakowanie: result[5],
            cena: result[6],
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
})

var Apteka = angular.module('Apteka', []);
Apteka.controller('order', function OrderController($scope) {
    $scope.hurtownias = [];
    sc = $scope;
});