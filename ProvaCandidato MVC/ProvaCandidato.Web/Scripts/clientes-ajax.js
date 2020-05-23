
$(function () {
    var baseUrl = window.location.origin + "/Clientes";

    $.get(baseUrl + "/GetCities", function (cities) {
        let $cityDrop = $("#CidadeId");
        let idSelect = $("#CidadeId").val();

        // Limpa opções atuais
        $cityDrop.empty();

        // Inicializa primeira opção
        $cityDrop.append($("<option></option>").attr("value", '').text('Selecione...'));

        // Carrega opções do resultado AJAX
        cities.forEach(city => {
            let option = "<option></option>";

            if (typeof idSelect !== "undefined") {
                if (idSelect.length > 0 && parseInt(idSelect) === city.Codigo) {
                    option = "<option selected></option>";
                }
            }

            $cityDrop.append($(option).attr("value", city.Codigo).text(city.Nome))
        });

    });


    // Partial View
    $('#dialog').dialog({
        autoOpen: false,
        width: 400,
        resizable: false,
        title: 'hi there',
        modal: true,
        open: function (event, ui) {
            $(this).load(baseUrl + "/CreateObservationNewPartial");
        },
        buttons: {
            "Cancelar": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#showPartialButton').click(function () {
        var opt = {
            autoOpen: false,
            modal: true,
            title: 'Observações',
            width: '50%'
        };

        $("#dialog").dialog(opt).dialog("open");

        setTimeout(function () {
            $('#addObservacaoBtn').click(function () {
                let observationObj = {
                    observacao: $("#Observacao").val(),
                    referencia: $("#Referencia").val(),
                    clienteId: parseInt($("#ClienteId").val())
                }
                $.ajax({
                    url: baseUrl + "/SaveClienteObservation",
                    type: "POST",
                    data: observationObj,
                    success: function (data) {
                        if (data.result)
                            window.location.reload();
                    }
                });
            });
        }, 1000);

    });

});