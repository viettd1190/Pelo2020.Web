var table = null;
$(document).ready(function () {
    addActiveClass('mnuUser');
    initTable();
    $('#txtCode').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
    $('#txtName').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
    $('#txtPhone').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
    reloadData();
});
function initTable() {
    table = $('#tblUsers').DataTable({
        deferLoading: true,
        processing: true,
        serverSide: true,
        ordering: true,
        paging: true,
        searching: true,
        ajax: {
            url: "/User/GetList",
            method: "POST"
        },
        columns: [
            { data: "id" },
            { data: "code" },
            { data: "full_name", sortable: false },
            { data: "branch", sortable: false },
            { data: "department", sortable: false },
            { data: "date_created" },
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
                targets: 1,
                data: 'code',
                className:'datatable-cell-center',
                orderable: true,
                render: function (data, type, row, meta) {
                    return '<div style="text-align:center;margin:auto;"><span class="badge badge-danger">' + data +'</span></div>';
                }
            }, {
                targets: 2,
                data: 'full_name',
                className: 'datatable-cell-center',
                searchable: true,
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="padding:10px;"><span>' + isNull(data) + '</span><br/><div style="padding-top:10px;"><a href="tel:' + row.phone_number + '" style="font-weight:800;">' + row.phone_number +'</a></div></div>';
                }
            }, {
                targets: 3,
                data: 'branch',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="padding:10px;"><span>' + isNull(data) + '</span></div>';
                }
            }, {
                targets: 4,
                data: 'department',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="padding:10px;"><span>Phòng ban: ' + isNull(data) + '</span><br/><div style="padding-top:10px;"><span>Nhóm quyền: ' + isNull(row.role) + '</span></div></div>';
                }
            }, {
                targets: 5,
                data: 'date_created',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="text-align:center;">'+moment(data).format('DD-MM-YYYY hh:mm')+'</div>';
                }
            }, {
                targets: 6,
                data: 'id',
                orderable: false,
                className: 'tdButton',
                render: function (data, type, row, meta) {
                    return '<div style="display:flex;"><a class="btn btn-primary" href="/User/Edit/' +
                        data +
                        '"><i class="zmdi zmdi-edit"></i></a>&nbsp;<a class="btn btn-danger" href="javascript:deleteUser(' +
                        data +
                        ')"><i class="zmdi zmdi-delete"></i></a></div>';
                }
            }
        ],
        order: [[1, 'asc']],
        rowCallback: function (row, data) {
            if (data.is_active===false) {
                $(row).addClass('inactive');
            }
        }
    });
}
function reloadData() {
    if (table !== null) {
        table
            .columns(0).search($('#txtCode').val())
            .columns(1).search($('#txtName').val())
            .columns(2).search($('#txtPhone').val())
            .columns(3).search($('#txtBranch').val())
            .columns(4).search($('#txtDepartment').val())
            .columns(5).search($('#txtRole').val())
            .columns(6).search($('#txtStatus').val())
            .draw();
    }
}
function deleteUser(id) {
    swal({
        title: "Cảnh báo!!!",
        text: "Bạn không thể khôi phục người dùng này.",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Tôi chắc chắn!",
        cancelButtonText: "Hủy bỏ"
    }).then(function () {
        $.post('/User/Delete',
            { id: id },
            function (result) {
                showNotification(result);
                if (result.data === true) {
                    reloadData();
                }
            });
    });
}