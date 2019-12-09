﻿var table = null;
$(document).ready(function () {
    addActiveClass('mnuAppConfig');
    initTable();
    $('#txtName').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
    $('#txtDescription').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
});
function initTable() {
    table = $('#tblAppConfigs').DataTable({
        processing: true,
        serverSide: true,
        ordering: true,
        paging: true,
        searching: true,
        ajax: {
            url: "/AppConfig/GetList",
            method: "POST"
        },
        columns: [
            { "data": "id" },
            { "data": "name" },
            { "data": "value" },
            { "data": "description" },
            { "data": "id", width: 120 }
        ],
        columnDefs: [
            {
                targets: 0,
                data: 'id',
                searchable: false,
                orderable: false,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            }, {
                targets: 4,
                data: 'id',
                searchable: false,
                orderable: false,
                className: 'tdButton',
                render: function (data, type, row, meta) {
                    return '<div style="display:flex;"><a class="btn btn-primary" href="/AppConfig/Edit/' +
                        data +
                        '"><i class="zmdi zmdi-edit"></i></a>&nbsp;<a class="btn btn-danger" href="javascript:deleteAppConfig(' +
                        data +
                        ')"><i class="zmdi zmdi-delete"></i></a></div>';
                }
            }
        ],
        order: [[1, 'asc']]
    });
}
function reloadData() {
    if (table != null) {
        table.columns(1).search($('#txtName').val()).columns(3).search($('#txtDescription').val()).draw();
    }
}
function deleteAppConfig(id) {
    swal({
        title: "Cảnh báo!!!",
        text: "Bạn không thể khôi phục tham số này.",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Tôi chắc chắn!",
        cancelButtonText: "Hủy bỏ"
    }).then(function () {
        $.post('/AppConfig/Delete',
            { id: id },
            function (result) {
                showNotification(result);
                if (result.data === true) {
                    reloadData();
                }
            });
    });
}