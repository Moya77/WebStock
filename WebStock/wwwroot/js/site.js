

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

function CheckFormSalida() {
    let form = document.getElementById("FormSalidas");
    let buttonSubmit = $('#btnSubmitSalidas');
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
            let Lote = fila.insertCell(0);
            let Nombre = fila.insertCell(1);
            let Cantidad = fila.insertCell(2);
            let Vencimiento = fila.insertCell(3);
            let Estado = fila.insertCell(4);

            Lote.innerHTML = ProductoInventario.lote;
            Nombre.innerHTML = ProductoInventario.producto;

            if (ProductoInventario.cantidad <= 0) {
                Cantidad.innerHTML = ProductoInventario.cantidad;
                fila.classList.add("bg-info");
            } else {
                Cantidad.innerHTML = ProductoInventario.cantidad;
            }
            
            Vencimiento.innerHTML = FechaVencimiento;
            if (dateDiff(fechaActual, fechaVencimiento) <= 10) {
                Estado.innerHTML = "Apunto de vencer";
                fila.classList.add('bg-warning');
            } else if (dateDiff(fechaActual, fechaVencimiento) <= 0) {
                Estado.innerHTML = "Vencido";
                fila.classList.add('bg-danger');
            } else {
                Estado.innerHTML = "Vigente";
            }
        }
    });
}

function formatDate(FechaSinFormato) {
    let FechaFormato = FechaSinFormato.split(' ')[0];
    let parts = FechaFormato.split('/');
    if (parts[0].length == 1) {
        parts[0] = "0" + parts[0];
    }

    if (parts[1].length == 1) {
        parts[1] = "0" + parts[1];
    }
    FechaFormato = parts[2] + "-" + parts[1] + "-" + parts[0];

    return FechaFormato;
}


function GetLote() {
    let numLote = $('#NumLote').val();
    if (numLote <= 0) {
        numLote = -1;
    }

    $.get('/Inventario/GetInfoLote?numLote=' + numLote, function (InfoLote) {

        if (InfoLote.length > 0) {
            $('#Product').val(InfoLote[0].producto);
            $('#Canti').val(InfoLote[0].cantidad);
            $('#DateFabri').val(formatDate(InfoLote[0].fabricacion));
            $('#Provedor').val(InfoLote[0].provedor);
            $('#Caduca').val(formatDate(InfoLote[0].expiracion));
        } else {
            $('#Product').val("");
            $('#Canti').val(0);
            $('#DateFabri').val("");
            $('#Provedor').val("");
            $('#Caduca').val("");
        }
        
    });
}


