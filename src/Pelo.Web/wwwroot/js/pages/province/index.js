var table = null;
$(document).ready(function () {
    addActiveClass('mnuProvince');
    initTable();
    $('#txtName').keyup(function (key) {
        if(key.keyCode===13) {
            reloadData();
        }
    });
});
function initTable() {
    table = $('#tblProvinces').DataTable({
        processing: true,
        serverSide: true,
        paging: true,
        searching: true,
        orderable:false,
        ajax: {
            url: "/Province/GetList",
            method: "POST"
        },
        columns: [
            { "data": "id" },
            { "data": "type" },
            { "data": "name" },
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
                },
            {
                targets: 3,
                data: 'id',
                className: 'tdButton',
                render: function (data, type, row, meta) {
                    return '<div style="display:flex;"><a class="btn btn-primary" href="/Province/Edit/' +
                        data +
                        '"><i class="zmdi zmdi-edit"></i></a>&nbsp;<a class="btn btn-danger" href="javascript:deleteProvince(' +
                        data +
                        ')"><i class="zmdi zmdi-delete"></i></a></div>';
                }
            }
        ]
    });
}
function reloadData() {
    if (table != null) {
        table.columns(2).search($('#txtName').val()).draw();
    }
}
function deleteProvince(id) {
    swal({
        title: "Cảnh báo!!!",
        text: "Bạn không thể khôi phục tỉnh thành này.",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Tôi chắc chắn!",
        cancelButtonText: "Hủy bỏ"
    }).then(function () {
        $.post('/Province/Delete',
            { id: id },
            function (result) {
                showNotification(result);
                if (result.data === true) {
                    reloadData();
                }
            });
    });
}