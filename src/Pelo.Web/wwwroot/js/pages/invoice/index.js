var table = null;
$(document).ready(function () {
    addActiveClass('mnuInvoice');
    $('.date-picker').flatpickr({
            enableTime:false,
            nextArrow:'<i class="zmdi zmdi-long-arrow-right" />',
            prevArrow:'<i class="zmdi zmdi-long-arrow-left" />'
        });
    initTable();
    $('#txtCode').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
    $('#txtCustomerCode').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
    $('#txtCustomerName').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
    $('#txtCustomerPhone').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
    reloadData();
});
function initTable() {
    table = $('#tblInvoices').DataTable({
        deferLoading: true,
        processing: true,
        serverSide: true,
        ordering: true,
        paging: true,
        searching: true,
        ajax: {
            url: "/Invoice/GetList",
            method: "POST"
        },
        columns: [
            { data: "id" },
            { data: "invoice_status" },
            { data: "code", sortable: false },
            { data: "customer", sortable: false },
            { data: "products", sortable: false },
            { data: "branch" },
            { data: "delivery_date" },
            { data: "date_created" },
            { data: "date_created", visible: false },
            { data: "date_created", visible: false },
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
                data: 'invoice_status',
                orderable: true,
                render: function (data, type, row, meta) {
                    return '<div style="margin:auto;"><span class="badge" style="color:#FFF; background-color: '+
                        row.invoice_status_color+
                        '">'+
                        data+
                        '</span></div>';
                }
            }, {
                targets: 2,
                data: 'code',
                className: 'datatable-cell-center',
                searchable: true,
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="padding:10px;"><div style="padding-top:10px;"><a href="/Invoice/Detail/'+row.id+'" style="font-weight:800;">' + row.code + '</a></div></div>';
                }
            }, {
                targets: 3,
                data: 'customer',
                //className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    var customerName = '<span style="font-weight:600;font-size:16px;">' + data + '</span><br/>';
                    var customerCode = '<span>Mã KH: <span style="font-weight:600;font-size:16px;">' + row.customer_code + '</span></span><br/>';
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
                    var condition='';
                    if(isNull(row.province)) {
                        address=address+condition+row.province_type+' '+row.province;
                        condition=', ';
                    }
                    if (isNull(row.district)) {
                        address=address+condition+row.district_type+' '+row.district;
                        condition=', ';
                    }
                    if (isNull(row.ward)) {
                        address=address+condition+row.ward_type+' '+row.ward;
                        condition = ', ';
                    }
                    if(isNull(row.customer_address)) {
                        address = address + condition + row.customer_address;
                    }
                    if(isNull(address)) {
                        address = '<span>Địa chỉ: <span style="font-weight:700;">' + address + '</span></span><br/>';
                    }
                    return '<div style="padding:10px;">'+
                        customerName+
                        phone+
                        phone2+
                        phone3+
                        address+
                        '</div>';
                }
            }, {
                targets: 4,
                data: 'products',
                //className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    var productContent = '';
                    if (data) {
                        var condition = '';
                        var index=1;
                        for (var i = 0; i < data.length; i++) {
                            var product = condition + '<span>';
                            product=product+index+'. '+data[i].name;
                            if(data[i].description) {
                                product=product+' ('+data[i].description+')';
                            }
                            product = product + '</span>';
                            condition=',<br/>';
                            productContent = productContent + product;
                            index++;
                        }
                    }
                    return '<div style="padding:10px;">'+productContent+'</div></div>';
                }
            }, {
                targets: 5,
                data: 'branch',
                //className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    var branch = '';
                    if (isNull(row.branch)) {
                        branch='<span>'+data+'</span><br/>';
                    }
                    var userCreated = '';
                    if (isNull(row.user_created)) {
                        userCreated='<span>Lên đơn: <span style="font-weight:700;"><a href="tel:'+
                            row.user_created_phone+
                            '">'+
                            row.user_created+
                            '</a></span></span><br/>';
                    }
                    var userSell = '';
                    if (isNull(row.user_sell)) {
                        userSell = '<span>Bán hàng: <span style="font-weight:700;"><a href="tel:'+row.user_sell_phone+'">' +
                            row.user_sell +
                            '</a></span></span><br/>';
                    }
                    var usersDelivery = '';
                    var condition = '';
                    if (row.users_delivery) {
                        if (row.users_delivery.length > 0) {
                            for (var i = 0; i < row.users_delivery.length; i++) {
                                usersDelivery = usersDelivery +
                                    condition +
                                    '<a href="tel:' +
                                    row.users_delivery[i].phone_number +
                                    '">' +
                                    row.users_delivery[i].display_name +
                                    '</a>';
                                condition = ', ';
                            }
                        }
                    }
                    if (usersDelivery) {
                        usersDelivery = '<span>Người giao: <span style="font-weight:700;">' + usersDelivery + '</span></span><br/>';
                    }
                    return '<div style="padding:10px;">' + branch + userCreated + userSell + usersDelivery + '</div></div>';
                }
            },  {
                targets: 6,
                data: 'delivery_date',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="text-align:center;">' + moment(data).format('DD-MM-YYYY hh:mm') + '</div>';
                }
            }, {
                targets: 7,
                data: 'date_created',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="text-align:center;">' + moment(data).format('DD-MM-YYYY hh:mm') + '</div>';
                }
            }
        ],
        order: [[7, 'desc']]
    });
}
function reloadData() {
    if (table !== null) {
        table
            .columns(0).search($('#txtCustomerCode').val())
            .columns(1).search($('#txtCustomerPhone').val())
            .columns(2).search($('#txtCustomerName').val())
            .columns(3).search($('#txtCode').val())
            .columns(4).search($('#txtBranch').val())
            .columns(5).search($('#txtInvoiceStatus').val())
            .columns(6).search($('#txtUserCreated').val())
            .columns(7).search($('#txtUserSell').val())
            .columns(8).search($('#txtUserDelivery').val())
            .columns(9).search($('#txtFromDate').val())
            .columns(10).search($('#txtToDate').val())
            .draw();
    }
}