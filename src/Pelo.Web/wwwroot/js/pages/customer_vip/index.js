var table = null;
$(document).ready(function () {
    addActiveClass('mnuCustomerVip');
    initTable();
    reloadData();
});
function initTable() {
    table = $('#tblCustomerVips').DataTable({
        deferLoading: true,
        processing: true,
        serverSide: true,
        ordering: true,
        paging: true,
        searching: true,
        ajax: {
            url: "/CustomerVip/GetList",
            method: "POST"
        },
        columns: [
            { data: "id" },
            { data: "name" },
            { data: "color" },
            { data: "profit" },
            { data: "id", width: 110 }
        ],
        columnDefs: [
            {
                targets: 0,
                data: 'id',
                orderable: false,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            }, {
                targets: 2,
                data: 'color',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="padding:10px;text-align:center;margin:auto;"><span class="badge badge-danger" style="background-color:'+data+'; width: 80px;">'+data+'</span></div>';
                }
            }, {
                targets: 3,
                data: 'profit',
                className: 'datatable-cell-right',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="padding:10px;text-align:right;"><span>'+Util.number.format(data) + '</span></div>';
                }
            },{
                targets: 4,
                data: 'id',
                orderable: false,
                className: 'tdButton',
                render: function (data, type, row, meta) {
                    return '<div style="display:flex;"><a class="btn btn-primary" href="/CustomerVip/Edit/' +
                        data +
                        '"><i class="zmdi zmdi-edit"></i></a>&nbsp;<a class="btn btn-danger" href="javascript:deleteCustomerVip(' +
                        data +
                        ')"><i class="zmdi zmdi-delete"></i></a></div>';
                }
            }
        ],
        order: [[1, 'asc']]
    });
}
function reloadData() {
    if (table !== null) {
        table.draw();
    }
}
function deleteCustomerVip(id) {
    swal({
        title: "Cảnh báo!!!",
        text: "Bạn không thể khôi phục mức độ khách hàng thân thiết này.",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Tôi chắc chắn!",
        cancelButtonText: "Hủy bỏ"
    }).then(function () {
        $.post('/CustomerVip/Delete',
            { id: id },
            function (result) {
                showNotification(result);
                if (result.data === true) {
                    reloadData();
                }
            });
    });
}