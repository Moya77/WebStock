

$(document).ready(function () {
    $('#Product').on('click', function () {
        $.get('/Product/ProductsList', function (ListProducts) {
            let datalistproducts = $('#ProductsList');
            datalistproducts.empty();
            for (const product of ListProducts) {
                datalistproducts.append(`<option value="${product}">`);
            }
        });
    });

    $('#Provedor').on('click', function () {
        $.get('/Product/ProvedorList', function (ListProvedores) {
            let datalistprovedores = $('#ProvedorsList');
            datalistprovedores.empty();
            for (const provedor of ListProvedores) {
                datalistprovedores.append(`<option value="${provedor}">`);
            }
        });
    });


});

$("#btnSubmit").click(function () {
    let formulario = document.getElementById('FormProduct');

    if (formulario.checkValidity()) {
        let fechaFabricacion = $('#DateFabri').val();
        let fechaVencimiento = $('#Caduca').val();
        let diff = Date.parse(fechaVencimiento) - Date.parse(fechaFabricacion);
        if (diff > 0) {

            $("#FormProduct").submit();

        } else {
            alert("Parece que el producto que intenta ingresar esta vencido!");
        }
    } else {
        formulario.reportValidity();
    }
});

function CheckForm() {
    let form = document.getElementById("FormProduct");
    let buttonSubmit = $('#btnSubmit');
    if (form.checkValidity()) {
        buttonSubmit.prop('disabled', false);
    } else {
        buttonSubmit.prop('disabled', true);
    }
}

$(document).ready(function () {
    $('.modal').modal('show');
});
    

