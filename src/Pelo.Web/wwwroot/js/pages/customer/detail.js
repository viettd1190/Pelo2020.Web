﻿var table = null;
var crmCounter = 0;
$(document).ready(function () {
    addActiveClass('mnuCustomer');
    initTableCrm();
    reloadData();
    //$('#txtCode').keyup(function (key) {
    //    if (key.keyCode === 13) {
    //        reloadData();
    //    }
    //});
    //$('#txtName').keyup(function (key) {
    //    if (key.keyCode === 13) {
    //        reloadData();
    //    }
    //});
    //$('#txtPhone').keyup(function (key) {
    //    if (key.keyCode === 13) {
    //        reloadData();
    //    }
    //});
    //$('#txtAddress').keyup(function (key) {
    //    if (key.keyCode === 13) {
    //        reloadData();
    //    }
    //});
    //$('#txtEmail').keyup(function (key) {
    //    if (key.keyCode === 13) {
    //        reloadData();
    //    }
    //});
    //reloadData();
});
//function initTable() {
//    table = $('#tblCustomers').DataTable({
//        deferLoading: true,
//        processing: true,
//        serverSide: true,
//        ordering: true,
//        paging: true,
//        searching: true,
//        ajax: {
//            url: "/Customer/GetList",
//            method: "POST"
//        },
//        columns: [
//            { data: "id" },
//            { data: "code" },
//            { data: "name" },
//            { data: "address" },
//            { data: "description" },
//            { data: "date_updated" },
//            { data: "date_updated", visible: false },
//            { data: "date_updated", visible: false },
//            { data: "date_updated", visible: false },
//            { data: "date_updated", visible: false },
//            { data: "id", width: 110 }
//        ],
//        columnDefs: [
//            {
//                targets: 0,
//                data: 'id',
//                orderable: false,
//                render: function (data, type, row, meta) {
//                    return meta.row + meta.settings._iDisplayStart + 1;
//                }
//            }, {
//                targets: 1,
//                data: 'code',
//                className: 'datatable-cell-center',
//                orderable: true,
//                render: function (data, type, row, meta) {
//                    return '<div style="text-align:center;margin:auto;"><span class="badge badge-danger">' + isNull(data) + '</span></div>';
//                }
//            }, {
//                targets: 2,
//                data: 'name',
//                orderable: true,
//                render: function (data, type, row, meta) {
//                    var name = '<span style="font-weight:600;font-size:16px;">' + data + '</span><br/>';
//                    var phone = '<span><a href="tel:' + row.phone + '">' + row.phone+'</a></span><br/>';
//                    var phone2 = '';
//                    if (isNull(row.phone2)) {
//                        phone2 = '<span><a href="tel:' + row.phone_2 + '">' + row.phone_2+'</a></span><br/>';
//                    }
//                    var phone3 = '';
//                    if (isNull(row.phone3)) {
//                        phone3 = '<span><a href="tel:' + row.phone_3 + '">' + row.phone_3 + '</a></span><br/>';
//                    }
//                    var customerGroup = '<span>Nhóm khách hàng: ' + isNull(row.customer_group) + '</span><br/>';
//                    var customerVip = '<span>KHTT: ' + isNull(row.customer_vip) + '</span><br/>';
//                    var email='';
//                    if (isNull(row.email)) {
//                        email = '<span>Email: <a href="mailto: ' + row.email + '">' + row.email+'</a></span><br/>';
//                    }
//                    return '<div style="padding:10px;">'+name+phone+phone2+phone3+customerGroup+customerVip+email+'</div>';
//                }
//            }, {
//                targets: 3,
//                data: 'address',
//                orderable: false,
//                render: function (data, type, row, meta) {
//                    var address = '<span>Địa chỉ: ' + isNull(data) + '</span><br/>';
//                    var province = '<span>' + isNull(row.province) + '</span><br/>';
//                    var district = '<span>' + isNull(row.district) + '</span><br/>';
//                    var ward = '<span>' + isNull(row.ward)+'</span>';
//                    return '<div style="padding:10px;">' + address + province + district + ward + '</div>';
//                }
//            }, {
//                targets: 5,
//                data: 'date_updated',
//                className: 'datatable-cell-center',
//                orderable: false,
//                render: function (data, type, row, meta) {
//                    return '<div style="text-align:center;">' + moment(data).format('DD-MM-YYYY hh:mm') + '</div>';
//                }
//            },{
//                targets: 10,
//                data: 'id',
//                orderable: false,
//                className: 'tdButton',
//                render: function (data, type, row, meta) {
//                    return '<div style="display:flex;"><a class="btn btn-primary" href="/Customer/Edit/' +
//                        data +
//                        '"><i class="zmdi zmdi-edit"></i></a>&nbsp;<a class="btn btn-danger" href="javascript:deleteCustomer(' +
//                        data +
//                        ')"><i class="zmdi zmdi-delete"></i></a></div>';
//                }
//            }
//        ],
//        order: [[1, 'asc']]
//    });
//}
//function reloadData() {
//    if (table !== null) {
//        table
//            .columns(0).search($('#txtCode').val())
//            .columns(1).search($('#txtName').val())
//            .columns(2).search($('#txtProvince').val())
//            .columns(3).search($('#txtDistrict').val())
//            .columns(4).search($('#txtWard').val())
//            .columns(5).search($('#txtAddress').val())
//            .columns(6).search($('#txtPhone').val())
//            .columns(7).search($('#txtEmail').val())
//            .columns(8).search($('#txtCustomerGroup').val())
//            .columns(9).search($('#txtCustomerVip').val())
//            .draw();
//    }
//}
//function deleteCustomer(id) {
//    swal({
//        title: "Cảnh báo!!!",
//        text: "Bạn không thể khôi phục khách hàng này.",
//        type: "warning",
//        showCancelButton: true,
//        confirmButtonClass: "btn-danger",
//        confirmButtonText: "Tôi chắc chắn!",
//        cancelButtonText: "Hủy bỏ"
//    }).then(function () {
//        $.post('/Customer/Delete',
//            { id: id },
//            function (result) {
//                showNotification(result);
//                if (result.data === true) {
//                    reloadData();
//                }
//            });
//    });
//}
function initTableCrm() {
    table = $('#tblCrms').DataTable({
        deferLoading: true,
        processing: true,
        serverSide: true,
        ordering: true,
        paging: true,
        searching: true,
        ajax: {
            url: "/GetListByCustomer",
            method: "POST"
        },
        columns: [
            { data: "id" },
            { data: "crm_status" },
            { data: "crm_priority" },
            { data: "code", sortable: false },
            { data: "customer_name", sortable: false },
            { data: "need", sortable: false },
            { data: "crm_priority" },
            { data: "customer_source" },
            { data: "user_created" },
            { data: "contact_date" },
            { data: "date_created", visible: false }
        ],
        columnDefs: [
            {
                targets: 0,
                data: 'id',
                orderable: false,
                render: function (data, type, row, meta) {
                    updateCrmCounter();
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            }, {
                targets: 1,
                data: 'crm_status',
                orderable: true,
                render: function (data, type, row, meta) {
                    return '<div style="margin:auto;"><span class="badge" style="color:#FFF; background-color: ' +
                        row.crm_status_color +
                        '">' +
                        data +
                        '</span></div>';
                }
            }, {
                targets: 2,
                data: 'crm_priority',
                orderable: true,
                render: function (data, type, row, meta) {
                    return '<div style="margin:auto;"><span class="badge" style="color:#FFF; background-color: ' +
                        row.crm_priority_color +
                        '">' +
                        data +
                        '</span></div>';
                }
            }, {
                targets: 3,
                data: 'code',
                className: 'datatable-cell-center',
                searchable: true,
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="padding:10px;"><div style="padding-top:10px;"><a href="/Crm/Update/' + row.id + '" style="font-weight:800;">' + row.code + '</a></div></div>';
                }
            }, {
                targets: 4,
                data: 'customer_name',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    var customerName = '<span style="font-weight:600;font-size:16px;">' + data + '</span><br/>';
                    var phone = '<span><a href="tel:' + row.customer_phone + '">' + row.customer_phone + '</a></span><br/>';
                    var phone2 = '';
                    if (isNull(row.customer_phone_2)) {
                        phone2 = '<span><a href="tel:' + row.customer_phone_2 + '">' + row.customer_phone_2 + '</a></span><br/>';
                    }
                    var phone3 = '';
                    if (isNull(row.customer_phone_3)) {
                        phone3 = '<span><a href="tel:' + row.customer_phone_3 + '">' + row.customer_phone_3 + '</a></span><br/>';
                    }
                    var address = '';
                    var condition = '';
                    if (isNull(row.province)) {
                        address = address + condition + row.province;
                        condition = ', ';
                    }
                    if (isNull(row.district)) {
                        address = address + condition + row.district;
                        condition = ', ';
                    }
                    if (isNull(row.ward)) {
                        address = address + condition + row.ward;
                        condition = ', ';
                    }
                    if (isNull(row.customer_address)) {
                        address = address + condition + row.customer_address;
                    }
                    if (isNull(address)) {
                        address = '<span>Địa chỉ: <span style="font-weight:700;">' + address + '</span></span><br/>';
                    }
                    var customerGroup = '';
                    if (isNull(row.customer_group)) {
                        customerGroup = '<span>Nhóm khách hàng: <span style="font-weight:700;">' +
                            row.customer_group +
                            '</span></span><br/>';
                    }
                    var customerVip = '';
                    if (isNull(row.customer_vip)) {
                        customerVip = '<span>Mức độ thân thiết: <span style="font-weight:700;">' +
                            row.customer_vip +
                            '</span></span><br/>';
                    }
                    return '<div style="padding:10px;">' +
                        customerName +
                        phone +
                        phone2 +
                        phone3 +
                        address +
                        customerGroup +
                        customerVip +
                        '</div>';
                }
            }, {
                targets: 5,
                data: 'need',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    var need = '';
                    if (isNull(row.need)) {
                        need = '<span>Nhu cầu: <span style="font-weight:700;">' + row.need + '</span></span><br/>';
                    }
                    var description = '';
                    if (isNull(row.description)) {
                        description = '<span>Ghi chú: <span style="font-weight:700;">' +
                            row.description +
                            '</span></span><br/>';
                    }
                    var productGroup = '';
                    if (isNull(row.product_group)) {
                        productGroup = '<span>Nhóm sản phẩm: <span style="font-weight:700;">' +
                            row.product_group +
                            '</span></span><br/>';
                    }
                    return '<div style="padding:10px;">' + need + description + productGroup + '</div></div>';
                }
            }, {
                targets: 6,
                data: 'customer_source',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="padding:10px;">' + data + '</div></div>';
                }
            }, {
                targets: 7,
                data: 'user_created',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    var userCreated = '<span>Người tạo: <span style="font-weight:700;">' + row.user_created + '</span></span><br/>';
                    var userCares = '';
                    var condition = '';
                    if (row.user_cares) {
                        if (row.user_cares.length > 0) {
                            for (var i = 0; i < row.user_cares.length; i++) {
                                userCares = userCares + condition + row.user_cares[i].display_name;
                                condition = ', ';
                            }
                        }
                    }
                    if (userCares) {
                        userCares = '<span>Phụ trách: <span style="font-weight:700;">' + userCares + '</span></span><br/>';
                    }
                    return '<div style="padding:10px;">' + userCreated + userCares + '</div></div>';
                }
            }, {
                targets: 8,
                data: 'contact_date',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="text-align:center;">' + moment(data).format('DD-MM-YYYY hh:mm') + '</div>';
                }
            }, {
                targets: 9,
                data: 'date_created',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="text-align:center;">' + moment(data).format('DD-MM-YYYY hh:mm') + '</div>';
                }
            }
        ],
        order: [[8, 'desc']]
    });
}
function reloadData() {
    if (table !== null) {
        table
            .columns(0).search(window.customerId)
            .draw();
    }
}
function updateCrmCounter() {
    crmCounter++;
    $('#crmCount').html('('+crmCounter+')');
}
function initTableInvoice() {
    table = $('#tblCrms').DataTable({
        deferLoading: true,
        processing: true,
        serverSide: true,
        ordering: true,
        paging: true,
        searching: true,
        ajax: {
            url: "/GetListInvoiceByCustomer",
            method: "POST"
        },
        columns: [
            { data: "id" },
            { data: "invoice_status" },
            { data: "code", sortable: false },
            { data: "customer", sortable: false },
            { data: "need", sortable: false },
            { data: "branch" },
            { data: "date_created" },
            { data: "delivery_date" }
        ],
        columnDefs: [
            {
                targets: 0,
                data: 'id',
                orderable: false,
                render: function (data, type, row, meta) {
                    updateCrmCounter();
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            }, {
                targets: 1,
                data: 'invoice_status',
                orderable: true,
                render: function (data, type, row, meta) {
                    return '<div style="margin:auto;"><span class="badge" style="color:#FFF; background-color: ' +
                        row.invoice_status_color +
                        '">' +
                        data +
                        '</span></div>';
                }
            }, {
                targets: 2,
                data: 'code',
                className: 'datatable-cell-center',
                searchable: true,
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="padding:10px;"><div style="padding-top:10px;"><a href="/Invoice/Process/' + row.id + '" style="font-weight:800;">' + row.code + '</a></div></div>';
                }
            }, {
                targets: 3,
                data: 'customer',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    var customerName = '<span style="font-weight:600;font-size:16px;">' + data + '</span><br/>';
                    var phone = '<span><a href="tel:' + row.customer_phone + '">' + row.customer_phone + '</a></span><br/>';
                    var phone2 = '';
                    if (isNull(row.customer_phone_2)) {
                        phone2 = '<span><a href="tel:' + row.customer_phone_2 + '">' + row.customer_phone_2 + '</a></span><br/>';
                    }
                    var phone3 = '';
                    if (isNull(row.customer_phone_3)) {
                        phone3 = '<span><a href="tel:' + row.customer_phone_3 + '">' + row.customer_phone_3 + '</a></span><br/>';
                    }
                    var address = '';
                    var condition = '';
                    if (isNull(row.province)) {
                        address = address + condition + row.province;
                        condition = ', ';
                    }
                    if (isNull(row.district)) {
                        address = address + condition + row.district;
                        condition = ', ';
                    }
                    if (isNull(row.ward)) {
                        address = address + condition + row.ward;
                        condition = ', ';
                    }
                    if (isNull(row.customer_address)) {
                        address = address + condition + row.customer_address;
                    }
                    if (isNull(address)) {
                        address = '<span>Địa chỉ: <span style="font-weight:700;">' + address + '</span></span><br/>';
                    }
                    return '<div style="padding:10px;">' +
                        customerName +
                        phone +
                        phone2 +
                        phone3 +
                        address +
                        '</div>';
                }
            }, {
                targets: 4,
                data: 'id',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    var result = '';
                    for (var i = 0; i < row.products.length; i++) {
                        result += '</span>'+row.products[i].Name+'</span><br/>';
                    }
                    return '<div style="padding:10px;">' + result + '</div></div>';
                }
            }, {
                targets: 5,
                data: 'branch',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="padding:10px;">' + data + '</div></div>';
                }
            }, {
                targets: 6,
                data: 'user_created',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    var userCreated = '<span>Lên đơn: <span style="font-weight:700;">' + row.user_created + '</span></span><br/>';
                    var user_sell = '<span>Bán hàng: <span style="font-weight:700;">' + row.user_sell + '</span></span><br/>';
                    return '<div style="padding:10px;">' + userCreated + user_sell + '</div></div>';
                }
            }, {
                targets: 7,
                data: 'date_created',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="text-align:center;">' + moment(data).format('DD-MM-YYYY hh:mm') + '</div>';
                }
            }, {
                targets: 8,
                data: 'delivery_date',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="text-align:center;">' + moment(data).format('DD-MM-YYYY hh:mm') + '</div>';
                }
            }
        ],
        order: [[8, 'desc']]
    });
}