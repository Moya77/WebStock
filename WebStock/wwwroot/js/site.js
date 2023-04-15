

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

function dateDiff(fechaFabricacion, fechaVencimiento) {
    let diff = Date.parse(fechaVencimiento) - Date.parse(fechaFabricacion);
    let diffDays = diff / (1000 * 60 * 60 * 24);
    return diffDays;
}


$("#btnSubmit").click(function () {
    let formulario = document.getElementById('FormProduct');

    if (formulario.checkValidity()) {
        let fechaFabricacion = $('#DateFabri').val();
        let fechaVencimiento = $('#Caduca').val();

        if (dateDiff(fechaFabricacion,fechaVencimiento) > 0) {

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

function loadInventory() {
    let TableBody = document.getElementById("TableInventoryBody");
    let numLote = $('#ConsultNumLote').val();
    let fechaActual = new Date().toISOString().split('T')[0];
  
    $.get('/Inventario/GetInfoLote?numLote=' + numLote, function (ProductosInventario) {
        TableBody.innerHTML = "";
       
        for (const ProductoInventario of ProductosInventario) {

            let FechaVencimiento = ProductoInventario.expiracion.split(' ')[0];
            let parts = FechaVencimiento.split('/');
            fechaVencimiento = parts[2] + "-" + parts[1] + "-" + parts[0];

            let fila = TableBody.insertRow();
            let Nombre = fila.insertCell(0);
            let Cantidad = fila.insertCell(1);
            let Vencimiento = fila.insertCell(2);
            let Estado = fila.insertCell(3);
            Nombre.innerHTML = ProductoInventario.producto;
            Cantidad.innerHTML = ProductoInventario.cantidad;
            Vencimiento.innerHTML = FechaVencimiento;
            if (dateDiff(fechaActual, fechaVencimiento) <= 10) {
                Estado.innerHTML = "Apunto de vencer";
                fila.classList.add('bg-warning');
            } else if (dateDiff(fechaActual, fechaVencimiento) <= 0) {
                Estado.innerHTML = "Vencido";
                fila.classList.add('bg-danger');
            } else {
                Estado.innerHTML = "Vigente";
                fila.classList.add('miClase');
            }
        }
    });
}
    

