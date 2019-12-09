var table = null;
$(document).ready(function () {
    addActiveClass('mnuDistrict');
    loadProvinces();
    initTable();
    $('#txtName').keyup(function (key) {
        if(key.keyCode===13) {
            reloadData();
        }
    });
    $('#txtProvince').change(function() {reloadData();});
});
function initTable() {
    table = $('#tblDistricts').DataTable({
        processing: true,
        serverSide: true,
        paging: true,
        searching: true,
        orderable: false,
        ajax: {
            url: "/District/GetList",
            method: "POST"
        },
        columns: [
            { "data": "id" },
            { "data": "province" },
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
                targets: 4,
                data: 'id',
                className: 'tdButton',
                render: function (data, type, row, meta) {
                    return '<div style="display:flex;"><a class="btn btn-primary" href="/District/Edit/' +
                        data +
                        '"><i class="zmdi zmdi-edit"></i></a>&nbsp;<a class="btn btn-danger" href="javascript:deleteDistrict(' +
                        data +
                        ')"><i class="zmdi zmdi-delete"></i></a></div>';
                }
            }
        ]
    });
}
function reloadData() {
    if (table != null) {
        table.columns(1).search($('#txtProvince').val())
            .columns(3).search($('#txtName').val()).draw();
    }
}
function loadProvinces() {
    $.post('/District/GetProvinces',
        null,
        function(result) {
            if (result) {
                for(var i=0;i<result.data.length;i++) {
                    $('#txtProvince').append('<option value="'+result.data[i].id+'">'+result.data[i].name+'</option>');
                }
            }
        });
}
function deleteDistrict(id) {
    swal({
        title: "Cảnh báo!!!",
        text: "Bạn không thể khôi phục quận huyện này.",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Tôi chắc chắn!",
        cancelButtonText: "Hủy bỏ"
    }).then(function () {
        $.post('/District/Delete',
            { id: id },
            function (result) {
                showNotification(result);
                if (result.data === true) {
                    reloadData();
                }
            });
    });
}