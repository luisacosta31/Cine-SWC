$(document).ready(function () {
    //  alert("Entrando al add");
    $('#add').click(function () {
        var isValid = true;
        if (isValid) {
            $('#orderItemError').empty();
            var $newRow = $('#mainrow').clone().removeAttr('id');

            $('.idTipopro', $newRow).val($('#idTipopro').val());
            $('.idTamaño', $newRow).val($('#idTamaño').val());

            //Replace add button with remove button
            $('#add', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#idTipopro,#idProducto,#idTamaño,#subtotal,#cantidad,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //append clone row
            $('#orderdetailsItems').append($newRow);

            //clear select data
            $('#idTipopro,#idProducto,#idTamaño').val('0');
            $('#subtotal,#cantidad').val('');
            $('#orderItemError').empty();
        }
    })

    $('#orderdetailsItems').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });

    $('#submit').click(function () {
        var isAllValid = true;

        $('#orderItemError').text('');
        var list = [];
        var errorItemCount = 0;
        $('#orderdetailsItems tbody tr').each(function (index, ele) {
            var orderItem = {
                idTipopro   : $('select.idTipopro', this).val(),
                idProducto  : $('select.idProducto', this).val(),
                idTamaño    : $('select.idTamaño', this).val(),
                subtotal    : $('.subtotal', this).val(),
                cantidad    : $('.cantidad', this).val(),               
            }
            list.push(orderItem);

        })

        if (isAllValid) {
            var data = {
                fecha: $('#fecha').val(),
                idEmpleado: $('#idEmpleado').val(),
                total: $('#total').val(),
                tb_DetalleVentaProducto: list
            }

            $.ajax({

                type: 'POST',
                url: '/VentaProducto/save',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (data) {
                    if (data.status) {
                        sweetAlert({
                            title: 'Correcto',
                            text: 'Se ha generado la venta.',
                            type: 'success'
                        }).then(function () {
                            window.location.reload();

                        });

                    }

                    else {
                        sweetAlert({
                            title: 'Incorrecto',
                            text: 'No se ha generado la venta.',
                            type: 'danger'
                        }).then(function () {
                            

                        });
                    }
                    $('#submit').text('Save');
                },
                error: function (error) {
                    console.log(error);
                    $('#submit').text('Save');
                }
            });
        }

    });
});