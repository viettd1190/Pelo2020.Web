var table = null;
$(document).ready(function () {
    addActiveClass('mnuCustomer');
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
    $('#txtAddress').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
    $('#txtEmail').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
    reloadData();
});
function initTable() {
    table = $('#tblCustomers').DataTable({
        deferLoading: true,
        processing: true,
        serverSide: true,
        ordering: true,
        paging: true,
        searching: true,
        ajax: {
            url: "/Customer/GetList",
            method: "POST"
        },
        columns: [
            { data: "id" },
            { data: "code" },
            { data: "name" },
            { data: "address" },
            { data: "description" },
            { data: "date_updated" },
            { data: "date_updated", visible: false },
            { data: "date_updated", visible: false },
            { data: "date_updated", visible: false },
            { data: "date_updated", visible: false },
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
                className: 'datatable-cell-center',
                orderable: true,
                render: function (data, type, row, meta) {
                    return '<div style="text-align:center;margin:auto;"><span class="badge badge-danger">' + isNull(data) + '</span></div>';
                }
            }, {
                targets: 2,
                data: 'name',
                orderable: true,
                render: function (data, type, row, meta) {
                    var name = '<span style="font-weight:600;font-size:16px;">' + data + '</span><br/>';
                    var phone = '<span><a href="tel:' + row.phone + '">' + row.phone+'</a></span><br/>';
                    var phone2 = '';
                    if (isNull(row.phone2)) {
                        phone2 = '<span><a href="tel:' + row.phone_2 + '">' + row.phone_2+'</a></span><br/>';
                    }
                    var phone3 = '';
                    if (isNull(row.phone3)) {
                        phone3 = '<span><a href="tel:' + row.phone_3 + '">' + row.phone_3 + '</a></span><br/>';
                    }
                    var customerGroup = '<span>Nhóm khách hàng: ' + isNull(row.customer_group) + '</span><br/>';
                    var customerVip = '<span>KHTT: ' + isNull(row.customer_vip) + '</span><br/>';
                    var email='';
                    if (isNull(row.email)) {
                        email = '<span>Email: <a href="mailto: ' + row.email + '">' + row.email+'</a></span><br/>';
                    }
                    return '<div style="padding:10px;">'+name+phone+phone2+phone3+customerGroup+customerVip+email+'</div>';
                }
            }, {
                targets: 3,
                data: 'address',
                orderable: false,
                render: function (data, type, row, meta) {
                    var address = '<span>Địa chỉ: ' + isNull(data) + '</span><br/>';
                    var province = '<span>' + isNull(row.province) + '</span><br/>';
                    var district = '<span>' + isNull(row.district) + '</span><br/>';
                    var ward = '<span>' + isNull(row.ward)+'</span>';
                    return '<div style="padding:10px;">' + address + province + district + ward + '</div>';
                }
            }, {
                targets: 5,
                data: 'date_updated',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="text-align:center;">' + moment(data).format('DD-MM-YYYY hh:mm') + '</div>';
                }
            },{
                targets: 10,
                data: 'id',
                orderable: false,
                className: 'tdButton',
                render: function (data, type, row, meta) {
                    return '<div style="display:flex;"><a class="btn btn-primary" href="/Customer/Detail/' +
                        data +
                        '"><i class="zmdi zmdi-edit"></i></a>&nbsp;<a class="btn btn-danger" href="javascript:deleteCustomer(' +
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
        table
            .columns(0).search($('#txtCode').val())
            .columns(1).search($('#txtName').val())
            .columns(2).search($('#txtProvince').val())
            .columns(3).search($('#txtDistrict').val())
            .columns(4).search($('#txtWard').val())
            .columns(5).search($('#txtAddress').val())
            .columns(6).search($('#txtPhone').val())
            .columns(7).search($('#txtEmail').val())
            .columns(8).search($('#txtCustomerGroup').val())
            .columns(9).search($('#txtCustomerVip').val())
            .draw();
    }
}
function deleteCustomer(id) {
    swal({
        title: "Cảnh báo!!!",
        text: "Bạn không thể khôi phục khách hàng này.",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Tôi chắc chắn!",
        cancelButtonText: "Hủy bỏ"
    }).then(function () {
        $.post('/Customer/Delete',
            { id: id },
            function (result) {
                showNotification(result);
                if (result.data === true) {
                    reloadData();
                }
            });
    });
}