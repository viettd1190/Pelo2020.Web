var table = null;
$(document).ready(function () {
    addActiveClass('mnuCrm');
    $('.date-picker').flatpickr({
        enableTime: false,
        nextArrow: '<i class="zmdi zmdi-long-arrow-right" />',
        prevArrow: '<i class="zmdi zmdi-long-arrow-left" />'
    });
    loadProvinces();
    initTable();
    reloadData();
});
function initTable() {
    table = $('#tblCrms').DataTable({
        deferLoading: true,
        processing: true,
        serverSide: true,
        ordering: true,
        paging: true,
        searching: true,
        ajax: {
            url: "/Crm/GetListByCustomer",
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
            },{
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
                    return '<div style="padding:10px;">' + data+ '</div></div>';
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
            .columns(0).search($('#txtCustomerId').val())            
            .draw();
    }
}